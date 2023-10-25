using QlSinhVien.Model;

namespace QlSinhVien.Responsitory
{
    public interface IDiemHocSinhRepository : IRepository<DiemHocSinh>
    {
        void AddDiemHocSinh(string json);
    }
}
