using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data;
using System.Drawing;
using System.IO;


namespace DFWV
{

    class Database
    {
        public static SQLiteConnection Connection;
        public static SQLiteCommand Command;
        //static SQLiteDataAdapter DataAdapter;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();
        public static SQLiteDataReader Reader;

        public static void SetConnection(string dbPath)
        {
            Connection = new SQLiteConnection
                ("Data Source=" + dbPath + ";Version=3;New=False;Compress=True;");
            Connection.Open();
        }

        public static void CloseConnection()
        {
            Connection.Close();
            Connection.Dispose();
        }

        public static void ExecuteNonQuery(string txtQuery)
        {
            Command = Connection.CreateCommand();
            Command.CommandText = txtQuery;
            Command.ExecuteNonQuery();

        }

        public static void ExecuteQuery(string txtQuery)
        {
            Command = Connection.CreateCommand();
            Command.CommandText = txtQuery;
            Reader = Command.ExecuteReader();

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
            DataTable dt = new DataTable();
            dt.Load(Database.Reader);

            foreach (DataRow row in dt.Rows)
            {
                Command = Connection.CreateCommand();
                Command.CommandText = "DELETE FROM [" + row["tbl_name"] + "]";
                Command.ExecuteNonQuery(); 
            }


        }

        public static Bitmap BlobToImage(object blob)
        {
            System.IO.MemoryStream mStream = new System.IO.MemoryStream();
            byte[] pData = (byte[])blob;
            mStream.Write(pData, 0, Convert.ToInt32(pData.Length));
            Bitmap bm = new Bitmap(mStream, false);
            mStream.Dispose();
            return bm;
        }

        public static byte[] ImageToBlob(string filePath)
        {
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            byte[] bm = br.ReadBytes(Convert.ToInt32(fs.Length));
            br.Close();
            fs.Close();
            return bm;

        }

        public static byte[] ImageToBlob(Image image)
        {
            MemoryStream ms = new MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms.ToArray();
        }

        internal static void ExportWorldItem(string table, List<object> vals)
        {

            Command = Connection.CreateCommand();

            Command.CommandText = "INSERT INTO [" + table + "] values (";
            for (int i = 0; i < vals.Count; i++)
			{
                Command.CommandText += " @" + i + ",";
                Command.Parameters.AddWithValue("@" + i, vals[i]);
			}
            Command.CommandText = Command.CommandText.TrimEnd(',') + ")";


            Command.ExecuteNonQuery();
        }
    }
}
