using System;
using System.Windows.Forms;
using System.Data;
using System.Globalization;
using MySql.Data.MySqlClient;

namespace Punto_de_venta_new_era
{
    public partial class MainForm : Form
    {
        private string connectionString =
            "Server=localhost;Database=puntoVenta;Uid=root;Pwd=13579135ca;";

        private string codigoBarrasAcumulado = "";

        private decimal porcentajeDescuento = 0m;
        private string nombreCliente = "Público en general";

        public MainForm()
        {
            InitializeComponent();

            this.KeyPreview = true;
            this.KeyPress += new KeyPressEventHandler(MainForm_KeyPress);

            this.btnPagar.Click += new EventHandler(btnPagar_Click);
            this.btnBorrarItem.Click += new EventHandler(btnBorrarItem_Click);
            this.btnDescuento.Click += new EventHandler(btnDescuento_Click);
            this.btnCliente.Click += new EventHandler(btnCliente_Click);

            dgvDetalles.ReadOnly = false;

            dgvDetalles.Columns[0].ReadOnly = true;
            dgvDetalles.Columns[1].ReadOnly = true;
            dgvDetalles.Columns[2].ReadOnly = true;
            dgvDetalles.Columns[3].ReadOnly = false;
            dgvDetalles.Columns[4].ReadOnly = true;
            dgvDetalles.Columns[5].ReadOnly = true;

            dgvDetalles.CellValueChanged +=
                new DataGridViewCellEventHandler(dgvDetalles_CellValueChanged);
        }

        private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;

                if (!string.IsNullOrEmpty(codigoBarrasAcumulado))
                {
                    BuscarProducto(codigoBarrasAcumulado);
                    codigoBarrasAcumulado = "";
                }
            }
            else if (!char.IsControl(e.KeyChar))
            {
                codigoBarrasAcumulado += e.KeyChar;
            }
        }

        private void BuscarProducto(string idProducto)
        {
            idProducto = idProducto.Trim();

            using (MySqlConnection conn =
                   new MySqlConnection(connectionString))
            {
                string query =
                    "SELECT Producto, Precio FROM productos " +
                    "WHERE idProducto = @id";

                MySqlCommand cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@id", idProducto);

                try
                {
                    conn.Open();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string nombreProducto =
                                reader["Producto"].ToString();

                            decimal precioUnitario =
                                Convert.ToDecimal(reader["Precio"]);

                            bool encontrado = false;

                            foreach (DataGridViewRow row in dgvDetalles.Rows)
                            {
                                if (row.IsNewRow)
                                    continue;

                                if (row.Cells[0].Value != null &&
                                    row.Cells[0].Value.ToString() == idProducto)
                                {
                                    int cantidad =
                                        Convert.ToInt32(row.Cells[3].Value);

                                    row.Cells[3].Value = cantidad + 1;

                                    encontrado = true;
                                    break;
                                }
                            }

                            if (!encontrado)
                            {
                                dgvDetalles.Rows.Add(
                                    idProducto,
                                    nombreProducto,
                                    precioUnitario,
                                    1,
                                    0,
                                    0
                                );
                            }

                            CalcularTotales();
                        }
                        else
                        {
                            MessageBox.Show(
                                "Producto no encontrado en la base de datos.\nCódigo: " +
                                idProducto,
                                "Aviso",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning
                            );
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Error al buscar el producto: " + ex.Message,
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
        }

        private void dgvDetalles_CellValueChanged(
            object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != 3)
                return;

            DataGridViewRow row = dgvDetalles.Rows[e.RowIndex];

            if (row.IsNewRow || row.Cells[3].Value == null)
                return;

            int nuevaCantidad;

            if (int.TryParse(row.Cells[3].Value.ToString(),
                            out nuevaCantidad) &&
                nuevaCantidad > 0)
            {
                CalcularTotales();
            }
            else
            {
                MessageBox.Show(
                    "Ingresa una cantidad válida mayor a cero.",
                    "Cantidad inválida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                row.Cells[3].Value = 1;

                CalcularTotales();
            }
        }

        private void CalcularTotales()
        {
            decimal subtotalGeneral = 0m;
            decimal ivaGeneral = 0m;
            decimal totalGeneral = 0m;

            foreach (DataGridViewRow row in dgvDetalles.Rows)
            {
                if (row.IsNewRow)
                    continue;

                if (row.Cells[2].Value == null ||
                    row.Cells[3].Value == null)
                    continue;

                decimal precio =
                    Convert.ToDecimal(row.Cells[2].Value);

                int cantidad =
                    Convert.ToInt32(row.Cells[3].Value);

                decimal subtotalItem = precio * cantidad;

                decimal descuento =
                    subtotalItem * porcentajeDescuento / 100m;

                subtotalItem = Math.Round(
                    subtotalItem - descuento, 2);

                decimal ivaItem = Math.Round(
                    subtotalItem * 0.16m, 2);

                decimal importeItem = subtotalItem + ivaItem;

                row.Cells[4].Value = ivaItem;
                row.Cells[5].Value = importeItem;

                subtotalGeneral += subtotalItem;
                ivaGeneral += ivaItem;
                totalGeneral += importeItem;
            }

            lblSubtotalV.Text =
                "$" + subtotalGeneral.ToString("0.00");

            lblIvaV.Text =
                "$" + ivaGeneral.ToString("0.00");

            lblTotalV.Text =
                "$" + totalGeneral.ToString("0.00");
        }

        private void btnDescuento_Click(object sender, EventArgs e)
        {
            string entrada = MostrarInputBox(
                "Ingresa el porcentaje de descuento:",
                "Aplicar descuento"
            );

            decimal porcentaje;

            if (decimal.TryParse(
                entrada,
                NumberStyles.Number,
                CultureInfo.CurrentCulture,
                out porcentaje))
            {
                if (porcentaje >= 0 && porcentaje <= 100)
                {
                    porcentajeDescuento = porcentaje;

                    CalcularTotales();

                    MessageBox.Show(
                        "Descuento del " +
                        porcentajeDescuento.ToString("0.##") +
                        "% aplicado correctamente."
                    );
                }
                else
                {
                    MessageBox.Show(
                        "El descuento debe estar entre 0 y 100."
                    );
                }
            }
        }

        private void btnCliente_Click(object sender, EventArgs e)
        {
            string entrada = MostrarInputBox(
                "Escribe el nombre del cliente:",
                "Seleccionar cliente"
            );

            if (!string.IsNullOrWhiteSpace(entrada))
            {
                nombreCliente = entrada.Trim();

                MessageBox.Show(
                    "Cliente seleccionado: " + nombreCliente,
                    "Cliente registrado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
        }

        private string MostrarInputBox(string mensaje, string titulo)
        {
            using (Form ventana = new Form())
            {
                ventana.Text = titulo;
                ventana.Width = 320;
                ventana.Height = 160;
                ventana.StartPosition =
                    FormStartPosition.CenterParent;

                Label etiqueta = new Label();
                etiqueta.Text = mensaje;
                etiqueta.Left = 10;
                etiqueta.Top = 10;
                etiqueta.Width = 280;

                TextBox txtEntrada = new TextBox();
                txtEntrada.Left = 10;
                txtEntrada.Top = 40;
                txtEntrada.Width = 280;

                Button btnAceptar = new Button();
                btnAceptar.Text = "Aceptar";
                btnAceptar.Left = 110;
                btnAceptar.Top = 80;
                btnAceptar.DialogResult = DialogResult.OK;

                Button btnCancelar = new Button();
                btnCancelar.Text = "Cancelar";
                btnCancelar.Left = 195;
                btnCancelar.Top = 80;
                btnCancelar.DialogResult = DialogResult.Cancel;

                ventana.Controls.Add(etiqueta);
                ventana.Controls.Add(txtEntrada);
                ventana.Controls.Add(btnAceptar);
                ventana.Controls.Add(btnCancelar);

                ventana.AcceptButton = btnAceptar;
                ventana.CancelButton = btnCancelar;

                if (ventana.ShowDialog(this) == DialogResult.OK)
                {
                    return txtEntrada.Text;
                }

                return "";
            }
        }

        private void btnBorrarItem_Click(object sender, EventArgs e)
        {
            if (dgvDetalles.CurrentRow != null &&
                !dgvDetalles.CurrentRow.IsNewRow)
            {
                DialogResult respuesta = MessageBox.Show(
                    "¿Estás seguro de que deseas eliminar este producto?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (respuesta == DialogResult.Yes)
                {
                    dgvDetalles.Rows.Remove(
                        dgvDetalles.CurrentRow
                    );

                    CalcularTotales();
                }
            }
            else
            {
                MessageBox.Show(
                    "Primero selecciona el producto que deseas eliminar."
                );
            }
        } // <--- Llave de cierre corregida para btnBorrarItem_Click

        private void btnPagar_Click(object sender, EventArgs e)
        {
            dgvDetalles.EndEdit();

            bool hayProductos = false;

            foreach (DataGridViewRow row in dgvDetalles.Rows)
            {
                if (!row.IsNewRow)
                {
                    hayProductos = true;
                    break;
                }
            }

            if (!hayProductos)
            {
                MessageBox.Show(
                    "Agrega al menos un producto antes de pagar.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            using (MySqlConnection conn =
                   new MySqlConnection(connectionString))
            {
                MySqlTransaction tr = null;

                try
                {
                    conn.Open();

                    tr = conn.BeginTransaction();

                    decimal subtotalGeneral = 0m;
                    decimal ivaGeneral = 0m;
                    decimal totalGeneral = 0m;

                    decimal descuento =
                        porcentajeDescuento / 100m;

                    foreach (DataGridViewRow row in dgvDetalles.Rows)
                    {
                        if (row.IsNewRow)
                            continue;

                        string idProducto =
                            Convert.ToString(row.Cells[0].Value);

                        decimal precio =
                            Convert.ToDecimal(row.Cells[2].Value);

                        int cantidad =
                            Convert.ToInt32(row.Cells[3].Value);

                        if (cantidad <= 0)
                        {
                            throw new Exception(
                                "La cantidad de un producto no es válida."
                            );
                        }

                        string queryStock = @"
                            SELECT Cantidad
                            FROM productos
                            WHERE idProducto = @idProducto
                            FOR UPDATE";

                        MySqlCommand cmdStock =
                            new MySqlCommand(queryStock, conn, tr);

                        cmdStock.Parameters.AddWithValue(
                            "@idProducto", idProducto);

                        object resultado =
                            cmdStock.ExecuteScalar();

                        if (resultado == null)
                        {
                            throw new Exception(
                                "No se encontró el producto: " + idProducto
                            );
                        }

                        int stock = Convert.ToInt32(resultado);

                        if (stock < cantidad)
                        {
                            throw new Exception(
                                "Existencia insuficiente para el producto: " +
                                idProducto +
                                "\nDisponible: " + stock
                            );
                        }

                        decimal subtotalItem = precio * cantidad;

                        subtotalItem = Math.Round(
                            subtotalItem * (1m - descuento), 2);

                        decimal ivaItem = Math.Round(
                            subtotalItem * 0.16m, 2);

                        decimal importeItem =
                            subtotalItem + ivaItem;

                        subtotalGeneral += subtotalItem;
                        ivaGeneral += ivaItem;
                        totalGeneral += importeItem;
                    }

                    string queryVenta = @"
                        INSERT INTO ventas
                        (fecha, cliente, subtotal, iva, total)
                        VALUES
                        (@fecha, @cliente, @subtotal, @iva, @total)";

                    MySqlCommand cmdVenta =
                        new MySqlCommand(queryVenta, conn, tr);

                    cmdVenta.Parameters.AddWithValue(
                        "@fecha", DateTime.Now);

                    cmdVenta.Parameters.AddWithValue(
                        "@cliente", nombreCliente);

                    cmdVenta.Parameters.AddWithValue(
                        "@subtotal", subtotalGeneral);

                    cmdVenta.Parameters.AddWithValue(
                        "@iva", ivaGeneral);

                    cmdVenta.Parameters.AddWithValue(
                        "@total", totalGeneral);

                    cmdVenta.ExecuteNonQuery();

                    MySqlCommand cmdId = new MySqlCommand(
                        "SELECT LAST_INSERT_ID()", conn, tr);

                    int idVenta =
                        Convert.ToInt32(cmdId.ExecuteScalar());

                    foreach (DataGridViewRow row in dgvDetalles.Rows)
                    {
                        if (row.IsNewRow)
                            continue;

                        string idProducto =
                            Convert.ToString(row.Cells[0].Value);

                        decimal precio =
                            Convert.ToDecimal(row.Cells[2].Value);

                        int cantidad =
                            Convert.ToInt32(row.Cells[3].Value);

                        decimal subtotalItem = precio * cantidad;

                        subtotalItem = Math.Round(
                            subtotalItem * (1m - descuento), 2);

                        decimal ivaItem = Math.Round(
                            subtotalItem * 0.16m, 2);

                        decimal importeItem =
                            subtotalItem + ivaItem;

                        string queryDetalle = @"
                            INSERT INTO detallesCompra
                            (idVenta, idProducto, precio, cantidad, importe)
                            VALUES
                            (@idVenta, @idProducto, @precio, @cantidad, @importe)";

                        MySqlCommand cmdDetalle =
                            new MySqlCommand(queryDetalle, conn, tr);

                        cmdDetalle.Parameters.AddWithValue(
                            "@idVenta", idVenta);

                        cmdDetalle.Parameters.AddWithValue(
                            "@idProducto", idProducto);

                        cmdDetalle.Parameters.AddWithValue(
                            "@precio", precio);

                        cmdDetalle.Parameters.AddWithValue(
                            "@cantidad", cantidad);

                        cmdDetalle.Parameters.AddWithValue(
                            "@importe", importeItem);

                        cmdDetalle.ExecuteNonQuery();

                        string queryActualizar = @"
                            UPDATE productos
                            SET Cantidad = Cantidad - @cantidad
                            WHERE idProducto = @idProducto";

                        MySqlCommand cmdActualizar =
                            new MySqlCommand(queryActualizar, conn, tr);

                        cmdActualizar.Parameters.AddWithValue(
                            "@cantidad", cantidad);

                        cmdActualizar.Parameters.AddWithValue(
                            "@idProducto", idProducto);

                        cmdActualizar.ExecuteNonQuery();
                    }

                    tr.Commit();

                    MessageBox.Show(
                        "¡Venta registrada correctamente!\n\n" +
                        "Cliente: " + nombreCliente +
                        "\nFolio: " + idVenta +
                        "\nSubtotal: $" + subtotalGeneral.ToString("0.00") +
                        "\nIVA: $" + ivaGeneral.ToString("0.00") +
                        "\nTotal: $" + totalGeneral.ToString("0.00"),
                        "Venta exitosa",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    dgvDetalles.Rows.Clear();
                    nombreCliente = "Público en general";
                    porcentajeDescuento = 0m;
                    CalcularTotales();
                }
                catch (Exception ex)
                {
                    if (tr != null)
                    {
                        try
                        {
                            tr.Rollback();
                        }
                        catch
                        {
                        }
                    }

                    MessageBox.Show(
                        "No se pudo completar la venta:\n" + ex.Message,
                        "Error al pagar",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
        }

        private void txtBuscador_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                
                string codigoManual = txtBuscador.Text.Trim();

                if (!string.IsNullOrEmpty(codigoManual))
                {
                    BuscarProducto(codigoManual);
                    txtBuscador.Clear();
                }
            }
        }
    }
}
