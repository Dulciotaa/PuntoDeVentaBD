using System;
using System.Windows.Forms;
using System.Data;
using MySql.Data.MySqlClient;

namespace Punto_de_venta_new_era
{
    public partial class MainForm : Form
    {
        private string connectionString = "Server=localhost;Database=puntoVenta;Uid=root;Pwd=migatoola6;SslMode=None;";
        private string codigoBarrasAcumulado = "";

        public MainForm()
        {
            InitializeComponent();
            
            this.KeyPreview = true;
            this.KeyPress += new KeyPressEventHandler(MainForm_KeyPress);
            this.btnPagar.Click += new EventHandler(btnPagar_Click);

            dgvDetalles.ReadOnly = false;
            dgvDetalles.Columns[0].ReadOnly = true;
            dgvDetalles.Columns[1].ReadOnly = true;
            dgvDetalles.Columns[2].ReadOnly = true;
            dgvDetalles.Columns[3].ReadOnly = false;
            dgvDetalles.Columns[4].ReadOnly = true;
            dgvDetalles.Columns[5].ReadOnly = true;

            dgvDetalles.CellValueChanged += new DataGridViewCellEventHandler(dgvDetalles_CellValueChanged);
        }

        private void dgvDetalles_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 3)
            {
                DataGridViewRow row = dgvDetalles.Rows[e.RowIndex];

                if (row.Cells[3].Value != null)
                {
                    int nuevaCantidad;
                    bool esNumero = int.TryParse(row.Cells[3].Value.ToString(), out nuevaCantidad);

                    if (esNumero && nuevaCantidad > 0)
                    {
                        double precio = Convert.ToDouble(row.Cells[2].Value);
                        double subtotal = precio * nuevaCantidad;
                        double iva = subtotal * 0.16;
                        double importe = subtotal + iva;

                        row.Cells[4].Value = iva;
                        row.Cells[5].Value = importe;

                        CalcularTotales();
                    }
                    else
                    {
                        MessageBox.Show("Por favor ingresa una cantidad numérica válida mayor a cero.", "Cantidad Inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        row.Cells[3].Value = 1; 
                    }
                }
            }
        }

        private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (!string.IsNullOrEmpty(codigoBarrasAcumulado))
                {
                    BuscarProducto(codigoBarrasAcumulado);
                    codigoBarrasAcumulado = ""; 
                }
            }
            else
            {
                codigoBarrasAcumulado += e.KeyChar;
            }
        }

        private void BuscarProducto(string idProducto)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = "SELECT Producto, Precio FROM productos WHERE idProducto = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", idProducto);

                try
                {
                    conn.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string nombreProducto = reader["Producto"].ToString();
                            double precioUnitario = Convert.ToDouble(reader["Precio"]);
                            
                            int cantidad = 1;
                            double subtotalItem = precioUnitario * cantidad;
                            double ivaItem = subtotalItem * 0.16; 
                            double importeItem = subtotalItem + ivaItem;

                            dgvDetalles.Rows.Add(idProducto, nombreProducto, precioUnitario, cantidad, ivaItem, importeItem);
                            CalcularTotales();
                        }
                        else
                        {
                            MessageBox.Show("Producto no encontrado en la base de datos.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error de conexión: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void CalcularTotales()
        {
            double subtotalGeneral = 0;
            double ivaGeneral = 0;
            double totalGeneral = 0;

            foreach (DataGridViewRow row in dgvDetalles.Rows)
            {
                if (row.IsNewRow) continue;

                double precio = Convert.ToDouble(row.Cells[2].Value); 
                int cantidad = Convert.ToInt32(row.Cells[3].Value);   
                double iva = Convert.ToDouble(row.Cells[4].Value);    
                double importe = Convert.ToDouble(row.Cells[5].Value);

                subtotalGeneral += (precio * cantidad);
                ivaGeneral += iva;
                totalGeneral += importe;
            }

            lblSubtotalV.Text = "$" + subtotalGeneral.ToString("0.00");
            lblIvaV.Text = "$" + ivaGeneral.ToString("0.00");
            lblTotalV.Text = "$" + totalGeneral.ToString("0.00");
        }

        private void btnPagar_Click(object sender, EventArgs e)
        {
            if (dgvDetalles.Rows.Count == 0)
            {
                MessageBox.Show("Escanea al menos un producto para cobrar.", "Aviso");
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    MySqlTransaction tr = conn.BeginTransaction();

                    try
                    {
                        double subTotalTotal = double.Parse(lblSubtotalV.Text.Replace("$", ""));
                        double totalIva = double.Parse(lblIvaV.Text.Replace("$", ""));
                        double total = double.Parse(lblTotalV.Text.Replace("$", ""));
                        DateTime fechaVenta = DateTime.Now;

                        string queryInsert = @"INSERT INTO detallesCompra 
                                             (Fecha, Producto, precioUnitario, subTotal, iva, subTotalTotal, TotalIva, Total, idProducto) 
                                             VALUES 
                                             (@fecha, @producto, @precioUnitario, @subTotal, @iva, @subTotalTotal, @totalIva, @total, @idProducto)";
                        
                        MySqlCommand cmdInsert = new MySqlCommand(queryInsert, conn, tr);

                        foreach (DataGridViewRow row in dgvDetalles.Rows)
                        {
                            if (row.IsNewRow) continue;

                            cmdInsert.Parameters.Clear();
                            cmdInsert.Parameters.AddWithValue("@fecha", fechaVenta);
                            cmdInsert.Parameters.AddWithValue("@producto", row.Cells[1].Value.ToString());
                            
                            double pUnitario = Convert.ToDouble(row.Cells[2].Value);
                            int cantidad = Convert.ToInt32(row.Cells[3].Value);
                            double subTotalItem = pUnitario * cantidad;
                            double ivaItem = Convert.ToDouble(row.Cells[4].Value);
                            
                            cmdInsert.Parameters.AddWithValue("@precioUnitario", pUnitario);
                            cmdInsert.Parameters.AddWithValue("@subTotal", subTotalItem);
                            cmdInsert.Parameters.AddWithValue("@iva", ivaItem);
                            
                            cmdInsert.Parameters.AddWithValue("@subTotalTotal", subTotalTotal);
                            cmdInsert.Parameters.AddWithValue("@totalIva", totalIva);
                            cmdInsert.Parameters.AddWithValue("@total", total);
                            
                            cmdInsert.Parameters.AddWithValue("@idProducto", Convert.ToInt32(row.Cells[0].Value));

                            cmdInsert.ExecuteNonQuery();
                        }

                        tr.Commit();
                        
                        MessageBox.Show("Venta registrada con éxito.", "Cobro Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        dgvDetalles.Rows.Clear();
                        CalcularTotales();
                    }
                    catch (Exception exBD)
                    {
                        tr.Rollback(); 
                        MessageBox.Show("Error al guardar la venta: " + exBD.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception exConn)
                {
                     MessageBox.Show("No se pudo conectar a MySQL: " + exConn.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}