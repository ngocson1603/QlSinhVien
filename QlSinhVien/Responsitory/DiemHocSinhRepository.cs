using Dapper;
using QlSinhVien.Data;
using QlSinhVien.Model;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Data;
using MySql.Data.MySqlClient;

namespace QlSinhVien.Responsitory
{
    public class DiemHocSinhRepository : Repository<DiemHocSinh>, IDiemHocSinhRepository
    {
        public DiemHocSinhRepository(ApplicationDBContext context) : base(context)
        {
        }
        public void AddDiemHocSinh(string json)
        {
            var query = @"call insertdiemhocsinh(@json);";
            var parameter = new DynamicParameters();
            parameter.Add("json", json);
            var kq = Context.Database.GetDbConnection().Query<string>(query, parameter);
        }
    }
}