using DocumentFormat.OpenXml.Spreadsheet;

namespace QlSinhVien.Model
{
    public class DiemHocSinh
    {
        public int IdHocSinh { get; set; }
        public int IdDiem { get; set; }
        public int SoLanHocLai { get;set; }
        public virtual HocSinh HocSinh { get; set; }
        public virtual Diem Diem { get; set; }
    }
}
