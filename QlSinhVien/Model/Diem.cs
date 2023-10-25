namespace QlSinhVien.Model
{
    public class Diem: BaseEntity
    {
        public float Toan { get; set; }
        public float Ly { get; set; }
        public float Hoa { get; set; }
        public float Anh { get; set; }
        public float Sinh { get; set; }
        public float Van { get; set; }
        public List<DiemHocSinh> DiemHocSinhs { get; set; }

    }
}
