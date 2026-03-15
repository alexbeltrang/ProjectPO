using SQLite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPO.Database
{
    public class DatabaseQueryLDB
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

        

        public static T ExecuteScalar<T>(string queryStr, params object[] args)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    return conn.ExecuteScalar<T>(queryStr, args);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error ejecutando scalar: {ex.Message}");
            }
        }

        public static List<T> ExecuteList<T>(string queryStr, params object[] args) where T : new()
        {
            try
            {
                using (var conn = GetConnection())
                {
                    return conn.Query<T>(queryStr, args).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error ejecutando query: {ex.Message}");
            }
        }

        public static int ExecuteNonQuery(string queryStr, params object[] args)
        {
            using (var conn = GetConnection())
            {
                return conn.Execute(queryStr, args);
            }
        }

        public static long ExecuteInsert(string queryStr, params object[] args)
        {
            using (var conn = GetConnection())
            {
                conn.Execute(queryStr, args);
                return conn.ExecuteScalar<long>("SELECT last_insert_rowid()");
            }
        }

        public static string GetDatabasePath()
        {
            return dbFile;
        }
    }
}
