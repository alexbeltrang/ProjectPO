using ProjectPO.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectPO
{
    public partial class FrmImportSupplier : Form
    {
        string archivo = "";
        private static string NameDatabase = ConfigurationManager.AppSettings["NameLDB"];

        private static string folder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
            "ProjectPO"
        );

        private static string dbFile = Path.Combine(folder, NameDatabase);


        public FrmImportSupplier()
        {
            InitializeComponent();
            creaBaseLocal();
        }


        private void creaBaseLocal()
        {
            DatabaseHelper.CreateOrUpdateTable<Supplier>();

            //creacion del indice para las tablas 

            using (var conn = new SQLite.SQLiteConnection(dbFile))
            {
                // Búsquedas por periodo + proveedor (muy importante)
                conn.Execute(@"CREATE INDEX IF NOT EXISTS IDX_SUPPLIER_YEARMONTH_SUPPLIER 
                   ON Supplier(YearMonth, ASL_Supplier_Number)");

                // Búsqueda por orden de compra
                conn.Execute(@"CREATE INDEX IF NOT EXISTS IDX_SUPPLIER_PO_LINE 
                   ON Supplier(PO_Number, PO_Line_Number)");

                // Búsqueda por proveedor (individual)
                conn.Execute(@"CREATE INDEX IF NOT EXISTS IDX_SUPPLIER_SUPPLIER 
                   ON Supplier(ASL_Supplier_Number)");

                // Búsqueda por país + planta
                conn.Execute(@"CREATE INDEX IF NOT EXISTS IDX_SUPPLIER_COUNTRY_PLANT 
                   ON Supplier(Supplier_Country_Name, Plant_code)");

                // Búsqueda por nombre proveedor (útil para filtros UI)
                conn.Execute(@"CREATE INDEX IF NOT EXISTS IDX_SUPPLIER_NAME 
                   ON Supplier(ASL_Supplier_Name)");
            }
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Filter = "TXT|*.txt|CSV|*.csv";

            if (of.ShowDialog() == DialogResult.OK)
            {
                archivo = of.FileName;
                lblArchivo.Text = archivo;
            }
        }

        private async void btnImportar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(archivo))
            {
                MessageBox.Show("Seleccione un archivo");
                return;
            }

            btnImportar.Enabled = false; 
            progressBar1.Value = 0;
            lblEstado.Text = "Iniciando importación...";

            await Task.Run(() => ImportarUltraRapidoOptimizado());

            lblEstado.Text = "Importación completada";
            btnImportar.Enabled = true;
            progressBar1.Value = 100;

        }

        private void ImportarUltraRapidoOptimizado()
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.BeginTransaction();

                try
                {
                    var totalLineas = File.ReadLines(archivo).Count();
                    int procesadas = 0;

                    List<Supplier> lote = new List<Supplier>(1000);

                    using (var reader = new StreamReader(archivo))
                    {
                        while (!reader.EndOfStream)
                        {
                            var linea = reader.ReadLine();
                            var c = linea.Split('\t');

                            int i = 0;

                            Supplier s = new Supplier
                            {
                                ASL_Supplier_Name = GetValue(c, ref i),
                                SL_Division_Code = GetValue(c, ref i),
                                SL_Business_Line_Code = GetValue(c, ref i),
                                SL_Business_Line_Desc = GetValue(c, ref i),
                                Supplier_Manager_Name = GetValue(c, ref i),
                                Supplier_Management_Model = GetValue(c, ref i),
                                PO_Number = GetValue(c, ref i),
                                PO_Created_Date = GetValue(c, ref i),
                                PO_Line_Number = GetInt(c, ref i),
                                ASL_Category = GetValue(c, ref i),
                                ASL_Sub_Category = GetValue(c, ref i),
                                Expense_Type = GetValue(c, ref i),
                                Spend_Source = GetValue(c, ref i),
                                YearMonth = GetValue(c, ref i),
                                ASL_Supplier_Number = GetValue(c, ref i),
                                Supplier_Country_Name = GetValue(c, ref i),
                                Plant_code = GetValue(c, ref i),
                                Supplier_Entity_Code = GetValue(c, ref i),
                                Supplier_Entity_Name = GetValue(c, ref i),
                                Simulated_NIS_Line_Desc = GetValue(c, ref i),
                                Buying_Country_Code = GetValue(c, ref i),
                                Buying_Country_Name = GetValue(c, ref i),
                                SL_Ultimate_Basin_Code = GetValue(c, ref i),
                                SL_Ultimate_Geounit_Code = GetValue(c, ref i),
                                SL_Ultimate_Division_Code = GetValue(c, ref i),
                                SL_Ultimate_Business_Line_Code = GetValue(c, ref i),
                                SL_Ultimate_Business_Line_Desc = GetValue(c, ref i),
                                Watch_List_Name = GetValue(c, ref i),
                                Payment_Terms = GetValue(c, ref i),
                                Spend_PO_USD = GetDecimal(c, ref i),
                                Spend_Non_PO_USD = GetDecimal(c, ref i),
                                Spend_USD = GetDecimal(c, ref i),
                                DistinctCountInvoice_Number = GetInt(c, ref i)
                            };

                            lote.Add(s);
                            procesadas++;

                            if (lote.Count == 1000)
                            {
                                conn.InsertAll(lote);
                                lote.Clear();
                            }

                            // Actualizar UI cada 1000 registros
                            if (procesadas % 1000 == 0)
                            {
                                int progreso = (int)((procesadas * 100.0) / totalLineas);

                                Invoke(new Action(() =>
                                {
                                    progressBar1.Value = progreso;
                                    lblEstado.Text = $"Importando... {procesadas:N0} registros";
                                }));
                            }
                        }
                    }

                    // Insertar lo restante
                    if (lote.Count > 0)
                        conn.InsertAll(lote);

                    conn.Commit();
                }
                catch (Exception ex)
                {
                    conn.Rollback();

                    Invoke(new Action(() =>
                    {
                        lblEstado.Text = "Error: " + ex.Message;
                    }));
                }
            }
        }


        private string GetValue(string[] c, ref int i)
        {
            return i < c.Length ? c[i++] : "";
        }

        private int GetInt(string[] c, ref int i)
        {
            if (i < c.Length && int.TryParse(c[i], out int val))
            {
                i++;
                return val;
            }
            i++;
            return 0;
        }

        private decimal GetDecimal(string[] c, ref int i)
        {
            if (i < c.Length && decimal.TryParse(c[i], out decimal val))
            {
                i++;
                return val;
            }
            i++;
            return 0;
        }
    }
}
