using Dapper;
using QlSinhVien.Data;
using QlSinhVien.Model;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Data;
using MySql.Data.MySqlClient;

namespace QlSinhVien.Responsitory
{
    public class CrudRepository : Repository<HocSinh>, ICrudRepository
    {
        private readonly IConfiguration _configuration;
        public CrudRepository(ApplicationDBContext context, IConfiguration configuration) : base(context)
        {
            _configuration = configuration;
        }
        public void CreateStore(string json)
        {
            var query = @"CALL InsertOrUpdateJSONData(@json);";
            var parameter = new DynamicParameters();
            parameter.Add("json", json);
            var kq = Context.Database.GetDbConnection().Query<string>(query, parameter);
        }

        //public void CreateTemporator(string json)
        //{
        //    var query = @"use demohocsinh; 
        //                CREATE TEMPORARY TABLE json_data (
        //                    id INT AUTO_INCREMENT PRIMARY KEY,
        //                    data JSON
        //                );
        //                CALL InsertJsonArray(@json);";
        //    var parameter = new DynamicParameters();
        //    parameter.Add("json", json);
        //    var kq = Context.Database.GetDbConnection().ExecuteScalar<string>(query, parameter);
        //}
        public IEnumerable<string> CreateTemporator(string json)
        {
            var query = @"use demohocsinh; 
                   CREATE TEMPORARY TABLE hocsinhtam (
                       id INT AUTO_INCREMENT PRIMARY KEY,
                       json_data JSON
                    );CALL InsertJsonData(@json);
                    SELECT json_data FROM demohocsinh.hocsinhtam;";
            var parameter = new DynamicParameters();
            parameter.Add("json", json);
            var kq = Context.Database.GetDbConnection().Query<string>(query, parameter);
            return kq;
        }

        public List<string> GetTemporator()
        {
            var query = @"SELECT id, data FROM demohocsinh.json_data;";
            var kq = Context.Database.GetDbConnection().Query<string>(query);
            return kq.ToList();
        }

        public List<HocSinh> SelectJsonTable(string json)
        {
            var query = @"select Id,TenHS, Tuoi, GioiTinh,IdLop
                        from JSON_TABLE(@json
                                 , '$[*]' COLUMNS (
                        Id longtext PATH '$.Id',
                        TenHS longtext PATH '$.TenHS',
                        Tuoi longtext PATH '$.Tuoi',
                        GioiTinh longtext PATH '$.GioiTinh',
                        IdLop longtext PATH '$.IdLop'
                        )
             ) as j;";
            var parameter = new DynamicParameters();
            parameter.Add("json", json);
            var kq = Context.Database.GetDbConnection().Query<HocSinh>(query, parameter);
            return kq.ToList();
        }
        public List<HocSinh> Crud(string type,string json)
        {
            var query = @"CALL HandleHocSinh(@type,@json);";
            var parameter = new DynamicParameters();
            parameter.Add("json", json);
            parameter.Add("type", type);
            var kq = Context.Database.GetDbConnection().Query<HocSinh>(query, parameter);
            return kq.ToList();
        }
        #region command
        public void command(string value1,string value2)
        {
            var connectionString = _configuration.GetConnectionString("connMSSQL");

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("HandleHocSinh", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@op_type", MySqlDbType.VarChar).Value = value1;
                    cmd.Parameters.Add("@json_input", MySqlDbType.VarChar).Value = value2;

                    con.Open();

                    // Create a MySqlDataAdapter to fill the DataTable
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                }
            }
        }
        #endregion

        #region getuser
        public DataTable GetUsers()
        {
            DataTable dt = new DataTable();
            string connectionString = _configuration.GetConnectionString("connMSSQL");

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("GetUsers", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();

                    // Create a MySqlDataAdapter to fill the DataTable
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        #endregion
    }
}
