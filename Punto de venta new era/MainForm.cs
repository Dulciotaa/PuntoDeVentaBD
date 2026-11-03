using System;
using System.Windows.Forms;
using System.Data;
using MySql.Data.MySqlClient; // Asegúrate de tener la referencia de MySQL agregada al proyecto

namespace Punto_de_venta_new_era
{
    public partial class MainForm : Form
    {
        // Conexión a tu base de datos 'puntoVenta' según tu script
        private string connectionString = "Server=localhost;Database=puntoVenta;Uid=root;Pwd=13579135ca;";
        private string codigoBarrasAcumulado = "";

        public MainForm()
        {
            InitializeComponent();
            
            // Habilitar la lectura de teclas para el escáner de código de barras
            this.KeyPreview = true;
            this.KeyPress += new KeyPressEventHandler(MainForm_KeyPress);
            
            // Conectar el botón de Pagar con su evento
            this.btnPagar.Click += new EventHandler(btnPagar_Click);
        }

        // --- 1. LECTOR DE CÓDIGO DE BARRAS ---
        private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            // El escáner envía un "Enter" (char 13) cuando termina de leer el código
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (!string.IsNullOrEmpty(codigoBarrasAcumulado))
                {
                    BuscarProducto(codigoBarrasAcumulado);
                    codigoBarrasAcumulado = ""; // Reiniciar para el siguiente escaneo
                }
            }
            else
            {
                // Acumular los números que el escáner va escribiendo rápidamente
                codigoBarrasAcumulado += e.KeyChar;
            }
        }

        // --- 2. BUSCAR EN BD Y AGREGAR A LA TABLA ---
        private void BuscarProducto(string idProducto)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                // Consultamos la tabla 'productos' por su idProducto (Clave)
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
                            double ivaItem = subtotalItem * 0.16; // Calculamos el 16% de IVA
                            double importeItem = subtotalItem + ivaItem;

                            // Agregamos la fila al DataGridView (dgvDetalles)
                            dgvDetalles.Rows.Add(
                                idProducto, 
                                nombreProducto, 
                                precioUnitario, 
                                cantidad, 
                                ivaItem, 
                                importeItem
                            );

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

        // --- 3. CALCULAR TOTALES DE LA VENTA ---
        private void CalcularTotales()
        {
            double subtotalGeneral = 0;
            double ivaGeneral = 0;
            double totalGeneral = 0;

            foreach (DataGridViewRow row in dgvDetalles.Rows)
            {
                if (row.IsNewRow) continue;

                double precio = Convert.ToDouble(row.Cells[2].Value); // colPUnitario
                int cantidad = Convert.ToInt32(row.Cells[3].Value);   // colCantidad
                double iva = Convert.ToDouble(row.Cells[4].Value);    // colIVA
                double importe = Convert.ToDouble(row.Cells[5].Value);// colImporte

                subtotalGeneral += (precio * cantidad);
                ivaGeneral += iva;
                totalGeneral += importe;
            }

            // Actualizamos las etiquetas verdes de tu diseño
            lblSubtotalV.Text = "$" + subtotalGeneral.ToString("0.00");
            lblIvaV.Text = "$" + ivaGeneral.ToString("0.00");
            lblTotalV.Text = "$" + totalGeneral.ToString("0.00");
        }

        // --- 4. TRANSACCIÓN PARA GUARDAR LA VENTA ---
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
                    // Iniciar la transacción para proteger los datos
                    MySqlTransaction tr = conn.BeginTransaction();

                    try
                    {
                        // Obtener los totales generales quitando el símbolo "$"
                        double subTotalTotal = double.Parse(lblSubtotalV.Text.Replace("$", ""));
                        double totalIva = double.Parse(lblIvaV.Text.Replace("$", ""));
                        double total = double.Parse(lblTotalV.Text.Replace("$", ""));
                        DateTime fechaVenta = DateTime.Now;

                        // Insertar cada fila de la tabla en detallesCompra
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
                            
                            // Totales de la nota completa (según la estructura de tu BD)
                            cmdInsert.Parameters.AddWithValue("@subTotalTotal", subTotalTotal);
                            cmdInsert.Parameters.AddWithValue("@totalIva", totalIva);
                            cmdInsert.Parameters.AddWithValue("@total", total);
                            
                            // Llave foránea del producto
                            cmdInsert.Parameters.AddWithValue("@idProducto", Convert.ToInt32(row.Cells[0].Value));

                            cmdInsert.ExecuteNonQuery();
                        }

                        // Confirmar la transacción
                        tr.Commit();
                        
                        MessageBox.Show("Venta registrada con éxito.", "Cobro Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        // Limpiar la interfaz para la siguiente venta
                        dgvDetalles.Rows.Clear();
                        CalcularTotales();
                    }
                    catch (Exception exBD)
                    {
                        tr.Rollback(); // Si algo falla, se deshace todo
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