using System.ComponentModel.DataAnnotations;

namespace QlSinhVien.Model
{
    public class Expt
    {
        public int IdExpt { get; set; }
        public int IdQ { get; set; }
        public List<Q> Qs { get; set; }
    }
}
