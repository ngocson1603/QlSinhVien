using QlSinhVien.Model;
using System.Data;

namespace QlSinhVien.Responsitory
{
    public interface IQRepository : IRepository<Q>
    {
        List<Expt> GetExpt(int idQ);
        void AddExptQ(int id,string json);
    }
}
