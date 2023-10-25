namespace QlSinhVien.Model
{
    public class LopHoc:BaseEntity
    {
        public int IdKhoiLop { get;set; }
        public string TenLop { get; set; }
        public KhoiLop Khoi { get; set; }
        public List<HocSinh> HocSinhs { get; set; }
    }
}
