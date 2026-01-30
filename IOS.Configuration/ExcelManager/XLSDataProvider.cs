using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.IO;

namespace IOS.Configuration.ExcelManager
{
    public class XLSDataProvider
    {
        #region Constants
        public const string _CONXLS = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={FilePath};Extended Properties=Excel 8.0";
        public const string _CONXLSX = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={FilePath};Extended Properties=Excel 12.0";

        #endregion

        private string WorkBookPath;

        public XLSDataProvider(string workbookpath)
        {
            this.WorkBookPath = workbookpath;
        }

        public string[] GetDatabases()
        {
            return new String[] { Path.GetFileName(WorkBookPath) };
        }

        public string[] GetViewsAndTables(out DataSet tableCollection)
        {
            string path = this.WorkBookPath;
            string constr = GetConnectionString(path);

            List<string> TablesList = new List<string>();
            using (OleDbConnection dbCon = new OleDbConnection(constr))
            {
                if (File.Exists(path))
                {
                    dbCon.Open();
                    tableCollection = new DataSet();
                    //   DataTable table = dbCon.GetSchema();
                    DataTable tableSet = dbCon.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    foreach (DataRow row in tableSet.Rows)
                    {
                        string tablename = row["TABLE_NAME"].ToString();
                        if (!tablename.EndsWith("$") && !tablename.EndsWith("$'"))
                        {
                            continue;
                        }
                        else
                        {
                            TablesList.Add(tablename);
                            OleDbCommand command = new OleDbCommand();
                            command.CommandText = string.Format("SELECT  * FROM [{0}]", tablename);
                            command.Connection = dbCon;
                            OleDbDataAdapter adp = new OleDbDataAdapter(command);
                            DataTable dt = new DataTable();
                            dt.TableName = tablename;
                            adp.Fill(dt);
                            tableCollection.Tables.Add(dt);
                        }
                    }
                }
                else
                {
                    throw new Exception("File not found");
                }
            }
            TablesList.Sort();
            return TablesList.ToArray();
        }

        public DataSet GetAllData()
        {
            DataSet tableCollection = new DataSet();
            string[] tables = GetViewsAndTables(out tableCollection);

            //using (OleDbConnection dbCon = new OleDbConnection(GetConnectionString(this.WorkBookPath)))
            //{
            //    dbCon.Open();
            //    OleDbCommand command = new OleDbCommand();
            //    foreach (string table in tables)
            //    {
            //        command.CommandText = string.Format("SELECT  * FROM [{0}]", table);
            //        command.Connection = dbCon;
            //        OleDbDataAdapter adp = new OleDbDataAdapter(command);
            //        DataTable dt = new DataTable();
            //        dt.TableName = table;
            //        adp.Fill(dt);
            //        ds.Tables.Add(dt);
            //    }

            //}
            return tableCollection;
        }
        public string[] GetFields(string table)
        {
            List<string> fields = new List<string>();
            string constr = GetConnectionString(WorkBookPath);

            List<string> TablesList = new List<string>();
            using (OleDbConnection dbCon = new OleDbConnection(constr))
            {
                dbCon.Open();
                OleDbCommand command = new OleDbCommand();
                command.CommandText = string.Format("SELECT  * FROM [{0}]", table);
                command.Connection = dbCon;

                OleDbDataReader reader = command.ExecuteReader();
                DataTable dt = reader.GetSchemaTable();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    //BroccoliColumn bcolumn = new BroccoliColumn();
                    //bcolumn.ColumnValue = reader.GetName(i);
                    //bcolumn.IsNumeric = TypeCheckhelper.IsTypeNumeric(reader.GetFieldType(i));
                    //bcolumn.IsValid = TypeCheckhelper.IsValidFieldType(TypeCheckhelper.DataSourceType.Excel, reader.GetFieldType(i).Name);
                    fields.Add(reader.GetName(i));
                }
                reader.Close();
            }

            //fields.Sort();
            return fields.ToArray();
        }

        #region for implementing the workaround for DistictCount Option

        public string[] GetDataColumnwise(string columnName, string table)
        {
            string path = this.WorkBookPath;
            string constr = GetConnectionString(path);
            List<string> results = new List<string>();
            results.Add(columnName);
            using (OleDbConnection connection = new OleDbConnection(constr))
            {
                OleDbCommand command = new OleDbCommand();

                StringBuilder commandText = new StringBuilder();
                commandText.Append(string.Format("SELECT DISTINCT "));
                commandText.Append(string.Format("[{0}] ", columnName));
                commandText.Append(string.Format(" FROM [{0}] ", table));
                command.CommandText = commandText.ToString();
                command.Connection = connection;
                connection.Open();
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (!reader.IsDBNull(0))
                        results.Add(Convert.ToString(reader[0]));
                    else
                        results.Add("No Data");
                }
            }

            return results.ToArray();
        }

        public bool TestConnection()
        {
            string path = this.WorkBookPath;
            string constr = GetConnectionString(path);
            using (OleDbConnection connection = new OleDbConnection(constr))
            {
                connection.Open();
                return true;
            }
        }

        #endregion

        #region Helper
        private string GetConnectionString(string path)
        {
            string constr = string.Empty;
            string fileExt = System.IO.Path.GetExtension(path).ToLower();
            switch (fileExt)
            {
                case ".xls":
                    constr = _CONXLS.Replace("{FilePath}", path);
                    break;
                case ".xlsx":
                    constr = _CONXLSX.Replace("{FilePath}", path);
                    break;
            }
            return constr;
        }
        #endregion
    }
}
