using Dapper;
using QlSinhVien.Data;
using QlSinhVien.Model;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Data;
using MySql.Data.MySqlClient;

namespace QlSinhVien.Responsitory
{
    public class QRepository : Repository<Q>, IQRepository
    {
        private readonly IConfiguration _configuration;
        public QRepository(ApplicationDBContext context, IConfiguration configuration) : base(context)
        {
            _configuration = configuration;
        }
        public List<Expt> GetExpt(int idQ)
        {
            var query = @"CALL GetExpt(@idQ);";
            var parameter = new DynamicParameters();
            parameter.Add("idQ", idQ);
            var kq = Context.Database.GetDbConnection().Query<Expt>(query, parameter);
            return kq.ToList();
        }
        public void AddExptQ(int id,string json)
        {
            var query = @"call InsertExptQWithJsonTable(@id,@json);";
            var parameter = new DynamicParameters();
            parameter.Add("id", id);
            parameter.Add("json", json);
            var kq = Context.Database.GetDbConnection().Query<string>(query, parameter);
        }
    }
}
