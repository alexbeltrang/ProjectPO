using ProjectPO.Database;
using SQLite;
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
    public partial class FrmCargaDatos : Form
    {
        private static string NameDatabase = ConfigurationManager.AppSettings["NameLDB"];

        private static string folder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
            "ProjectPO"
        );

        private static string dbFile = Path.Combine(folder, NameDatabase);

        public static SQLiteConnection GetConnection()
        {
            Directory.CreateDirectory(folder);

            return new SQLiteConnection(
                dbFile,
                SQLiteOpenFlags.ReadWrite |
                SQLiteOpenFlags.Create |
                SQLiteOpenFlags.FullMutex
                );
        }

        public FrmCargaDatos()
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
    }
}
