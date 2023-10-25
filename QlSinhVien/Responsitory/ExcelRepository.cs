using Dapper;
using QlSinhVien.Data;
using QlSinhVien.Model;
using QlSinhVien.ViewModel;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace QlSinhVien.Responsitory
{
    public class ExcelRepository :IExcelRepositoty
    {
        private IConfiguration configuration;
        private IWebHostEnvironment webHostEnvironment;
        public ExcelRepository(IConfiguration _configuration,IWebHostEnvironment _webHostEnvironment)
        {
            configuration = _configuration;
            webHostEnvironment = _webHostEnvironment;
        }
        public DataTable CustomerDataTable(string path)
        {
            var conStr = configuration.GetConnectionString("excel");
            DataTable dataTable = new DataTable();
            conStr = string.Format(conStr, path);
            using (OleDbConnection excelconn = new OleDbConnection(conStr))
            {
                using (OleDbCommand cmd = new OleDbCommand())
                {
                    using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter())
                    {
                        excelconn.Open();
                        cmd.Connection = excelconn;
                        DataTable excelschema;
                        excelschema = excelconn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                        var sheetname = excelschema.Rows[0]["Table_name"].ToString();
                        excelconn.Close();
                        excelconn.Open();
                        cmd.CommandText = "Select * from [" + sheetname + "]";
                        dataAdapter.SelectCommand = cmd;
                        dataAdapter.Fill(dataTable);
                        excelconn.Close() ;
                    }
                }
            }
            return dataTable;
        }
        public string DocumentUpload(IFormFile formFile)
        {
            string dest_path = Path.Combine(Directory.GetCurrentDirectory(), "upload_doc");
            if (!Directory.Exists(dest_path))
            {
                Directory.CreateDirectory(dest_path);
            }
            string sourceFile = Path.GetFileName(formFile.FileName);
            string path = Path.Combine(Directory.GetCurrentDirectory(), sourceFile);
            using (FileStream fileStream = new FileStream(path,FileMode.Create))
            {
                formFile.CopyTo(fileStream);
            }
            return path;
        }
        public void Import(DataTable hocsinh)
        {
            var sqlconn = configuration.GetConnectionString("ConnectionStrings[]");
            using (MySqlConnection mysqlConnection = new MySqlConnection(sqlconn))
            {
                mysqlConnection.Open();

                // Use a MySqlCommand to execute a bulk insert query
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = mysqlConnection;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO hocsinh (Id, TenHS, Tuoi,GioiTinh,IdLop) VALUES (@Param1, @Param2, @Param3,@Param4,@Param5)";

                    // Add parameters and values
                    // Execute the query multiple times for each row in the DataTable
                    foreach (DataRow row in hocsinh.Rows)
                    {
                        cmd.Parameters["@Param1"].Value = row["SourceColumn1"];
                        cmd.Parameters["@Param2"].Value = row["SourceColumn2"];
                        cmd.Parameters["@Param3"].Value = row["SourceColumn3"];
                        cmd.Parameters["@Param4"].Value = row["SourceColumn4"];
                        cmd.Parameters["@Param5"].Value = row["SourceColumn5"];
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                }
            }
        }
        #region
        #endregion
    }
}
