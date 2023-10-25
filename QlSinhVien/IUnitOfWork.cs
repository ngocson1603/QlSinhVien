using Microsoft.EntityFrameworkCore;
using QlSinhVien.Responsitory;

namespace QlSinhVien
{
    public interface IUnitOfWork
    {
        DbContext Context { get; }       
        IHocSinhRepository HocSinhRepository { get; }
        ICrudRepository CrudRepository { get; }
        IDiemRepository DiemRepository { get; }
        IDiemHocSinhRepository DiemHocSinhRepository { get; }
        IQRepository ExptQRepository { get; }
        void Save();
    }
}
