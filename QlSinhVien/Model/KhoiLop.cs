namespace QlSinhVien.Model
{
    public class KhoiLop : BaseEntity
    {
        public int Khoi { get; set; }
        public List<LopHoc> LopHocs { get; set; }
    }
}
