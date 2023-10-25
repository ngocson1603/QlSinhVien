using QlSinhVien.Model;
using System.Data;

namespace QlSinhVien.Responsitory
{
    public interface ICrudRepository : IRepository<HocSinh>
    {
        void CreateStore(string json);
        IEnumerable<string> CreateTemporator(string json);
        List<string> GetTemporator();
        List<HocSinh> SelectJsonTable(string json);
        List<HocSinh> Crud(string type, string json);
        void command(string value1, string value2);
        DataTable GetUsers();
    }
}
