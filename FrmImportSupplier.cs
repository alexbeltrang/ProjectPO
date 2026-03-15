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
                conn.Execute("CREATE INDEX IF NOT EXISTS IDX_SUPPLIER_YEARMONTH_SUPPLIER ON Supplier(YearMonth, ASL_Supplier_Number)");

                conn.Execute("CREATE INDEX IF NOT EXISTS IDX_SUPPLIER_PO_LINE ON Supplier(PO_Number, PO_Line_Number)");

                conn.Execute("CREATE INDEX IF NOT EXISTS IDX_SUPPLIER_DATE_SUPPLIER ON Supplier(Spend_Date, ASL_Supplier_Number)");

                conn.Execute("CREATE INDEX IF NOT EXISTS IDX_SUPPLIER_COUNTRY_PLANT ON Supplier(Supplier_Country, Plant)");
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

            await Task.Run(() => ImportarUltraRapido());

            btnImportar.Enabled = true;

            MessageBox.Show("Importación terminada");
        }

        private void ImportarUltraRapido()
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.BeginTransaction();

                try
                {
                    using (var reader = new StreamReader(archivo))
                    {
                        int contador = 0;

                        while (!reader.EndOfStream)
                        {
                            var linea = reader.ReadLine();

                            var c = linea.Split('\t');

                            Supplier s = new Supplier();

                            int i = 0;

                            s.ASL_Supplier_Name = c[i++];
                            s.Supplier_Manager_Name = c[i++];
                            s.Supplier_Management_Model = c[i++];
                            s.PO_Number = c[i++];
                            s.PO_Created_Date = c[i++];
                            s.PO_Type = c[i++];

                            int.TryParse(c[i++], out int line);
                            s.PO_Line_Number = line;

                            s.ASL_Category = c[i++];
                            s.ASL_Sub_Category = c[i++];
                            s.Expense_Type = c[i++];
                            s.Spend_Source = c[i++];
                            s.YearMonth = c[i++];
                            s.ASL_Supplier_Number = c[i++];
                            s.Supplier_Country = c[i++];
                            s.Item_Code = c[i++];
                            s.Item_Description = c[i++];
                            s.Plant = c[i++];
                            s.Invoice_Number = c[i++];

                            DateTime.TryParse(c[i++], out DateTime fecha);
                            s.Spend_Date = fecha;

                            s.Supplier_Entity_Code = c[i++];
                            s.Supplier_Entity_Name = c[i++];
                            s.Buying_Country_Code = c[i++];
                            s.Buying_Country_Name = c[i++];
                            s.SL_Ultimate_Basin = c[i++];
                            s.SL_Ultimate_Geounit = c[i++];
                            s.SL_Ultimate_Division = c[i++];
                            s.SL_Ultimate_Business_Line = c[i++];
                            s.Hybrid_Category = c[i++];
                            s.Hybrid_Sub_Category = c[i++];
                            s.Hybrid_Family_Desc = c[i++];
                            s.Hybrid_Commodity_Code = c[i++];
                            s.Payment_Terms = c[i++];
                            s.Invoice_Currency = c[i++];

                            decimal.TryParse(c[i++], out decimal po);
                            s.Spend_PO_USD = po;

                            decimal.TryParse(c[i++], out decimal nonpo);
                            s.Spend_Non_PO_USD = nonpo;

                            decimal.TryParse(c[i++], out decimal total);
                            s.Spend_USD = total;

                            conn.Insert(s);

                            contador++;

                            if (contador % 1000 == 0)
                            {
                                Invoke(new Action(() =>
                                {
                                    lblEstado.Text = $"Registros importados: {contador}";
                                }));
                            }
                        }
                    }

                    conn.Commit();
                }
                catch
                {
                    conn.Rollback();
                    throw;
                }
            }

            Invoke(new Action(() =>
            {
                progressBar1.Value = 100;
                lblEstado.Text = "Importación completada";
            }));
        }

    }
}
