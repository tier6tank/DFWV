using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using DFWV.WorldClasses;

namespace DFWV
{
    internal static class Database
    {
        private static SQLiteConnection _connection;
        private static SQLiteCommand _command;
        private static SQLiteDataReader Reader;

        public static void SetConnection(string dbPath)
        {
            _connection = new SQLiteConnection
                ("Data Source=" + dbPath + ";Version=3;New=False;Compress=True;");
            _connection.Open();
        }

        public static void CloseConnection()
        {
            _command.Dispose();
            _connection.Close();
            _connection.Dispose();
            _connection = null;
            _command = null;
            Reader = null;
        }

        private static void ExecuteNonQuery(string txtQuery)
        {
            _command = _connection.CreateCommand();
            _command.CommandText = txtQuery;
            _command.ExecuteNonQuery();
        }

        private static void ExecuteQuery(string txtQuery)
        {
            _command = _connection.CreateCommand();
            _command.CommandText = txtQuery;
            Reader = _command.ExecuteReader();
        }

        public static void BeginTransaction()
        {
            ExecuteNonQuery("BEGIN");
        }

        public static void CommitTransaction()
        {
            ExecuteNonQuery("COMMIT");
        }

        public static void EmptyAllTables()
        {
            ExecuteQuery("Select tbl_name from sqlite_master");
            var dt = new DataTable();
            dt.Load(Reader);

            BeginTransaction();
            foreach (DataRow row in dt.Rows)
            {
                _command = _connection.CreateCommand();
                _command.CommandText = "DELETE FROM [" + row["tbl_name"] + "]";
                _command.ExecuteNonQuery();
            
            }
            CommitTransaction();
            Reader.Dispose();
        }

/*
        public static Bitmap BlobToImage(object blob)
        {
            MemoryStream mStream = new MemoryStream();
            byte[] pData = (byte[])blob;
            mStream.Write(pData, 0, Convert.ToInt32(pData.Length));
            Bitmap bm = new Bitmap(mStream, false);
            mStream.Dispose();
            return bm;
        }
*/

        public static byte[] ImageToBlob(Image image)
        {
            var ms = new MemoryStream();
            image.Save(ms, ImageFormat.Png);
            return ms.ToArray();
        }

        internal static void ExportWorldItem(string table, List<object> vals)
        {
            _command = _connection.CreateCommand();

            _command.CommandText = "INSERT INTO [" + table + "] values (";
            for (var i = 0; i < vals.Count; i++)
            {
                _command.CommandText += " @" + i + ",";
                _command.Parameters.AddWithValue("@" + i, vals[i]);
            }
            _command.CommandText = _command.CommandText.TrimEnd(',') + ")";


            _command.ExecuteNonQuery();
        }


        /// <summary>
        /// These functions take a given field of an object to be exported and properly parse it for export, including replacing nulls with DBNulls.
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        #region DBExport
        internal static object DBExport(this string field)
        {
            if (field != null)
                return field.Replace("'", "''");
            else
                return DBNull.Value;
        }

        internal static object DBExport(this int? field)
        {
            if (field.HasValue)
                return field.Value;
            else
                return DBNull.Value;
        }

        internal static object DBExport(this int? field, List<string> LookupTable)
        {
            if (field.HasValue && LookupTable != null && LookupTable.Count > field.Value)
                return LookupTable[field.Value];
            else
                return DBNull.Value;
        }

        internal static object DBExport(this int field, List<string> LookupTable)
        {
            if (LookupTable != null && LookupTable.Count > field)
                return LookupTable[field];
            else
                return DBNull.Value;
        }

        internal static object DBExport(this Race race)
        {
            if (race != null)
                return race.ToString();
            else
                return DBNull.Value;
        }

        internal static object DBExport(this XMLObject xmlobject)
        {
            if (xmlobject != null)
                return xmlobject.ID;
            else
                return DBNull.Value;
        }

        internal static object DBExport(this WorldTime time, bool year)
        {
            if (time != null)
            {
                if (year)
                    return time.Year;
                else
                    return time.TotalSeconds;
            }
            else
                return DBNull.Value;
        }

        internal static object DBExport(this List<int> field, List<string> LookupTable)
        {
            if (field != null)
            {
                var exportText = field.Aggregate("", (current, curItem) => current + (LookupTable[curItem] + ","));
                exportText = exportText.TrimEnd(',');
                return exportText;
            }
            else
                return DBNull.Value;
        }

        internal static object DBExport(this List<int> field)
        {
            if (field != null)
            {
                return String.Join(",",field);
            }
            else
                return DBNull.Value;
        }

        internal static object DBExport(this Point pt)
        {
            if (pt.IsEmpty)
                return pt.X + "," + pt.Y;
            else
                return DBNull.Value;
        }
        #endregion
    }
}