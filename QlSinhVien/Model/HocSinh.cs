namespace QlSinhVien.Model
{
    public class HocSinh : BaseEntity
    {
        public int IdLop { get;set; }
        public string TenHS { get; set; }
        public int Tuoi { get; set; }
        public string GioiTinh { get; set; }
        public LopHoc LopHoc { get; set; }
        public List<DiemHocSinh> DiemHocSinhs { get; set; }
    }
}
