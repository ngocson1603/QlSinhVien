using QlSinhVien.Data;
using QlSinhVien.Model;

namespace QlSinhVien.Responsitory
{
    public class DiemRepository : Repository<Diem>, IDiemRepository
    {
        public DiemRepository(ApplicationDBContext context) : base(context)
        {

        }
    }
}
