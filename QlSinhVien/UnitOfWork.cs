using QlSinhVien.Data;
using QlSinhVien.Responsitory;
using Microsoft.EntityFrameworkCore;

namespace QlSinhVien
{
    public class UnitOfWork : IUnitOfWork
    {
        public DbContext Context { get; set; }   
        public IHocSinhRepository HocSinhRepository { get; set; }
        public ICrudRepository CrudRepository { get; set; }
        public IDiemRepository DiemRepository { get; set; }
        public IDiemHocSinhRepository DiemHocSinhRepository { get; set; }
        public IQRepository ExptQRepository { get; set; }
        public UnitOfWork(ApplicationDBContext context,
            IHocSinhRepository hocSinhRepository,
            ICrudRepository crudRepository,
            IDiemRepository diemRepository,
            IDiemHocSinhRepository diemHocSinhRepository,
            IQRepository exptQRepository)
        {
            Context = context;
            HocSinhRepository = hocSinhRepository;
            CrudRepository = crudRepository;
            ExptQRepository = exptQRepository;
            DiemRepository = diemRepository;
            DiemHocSinhRepository = diemHocSinhRepository;

        }

        public void Save()
        {
            Context.SaveChanges();
        }
    }
}
