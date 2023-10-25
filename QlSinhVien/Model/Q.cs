using QlSinhVien.Model;
using System.ComponentModel.DataAnnotations;

namespace QlSinhVien.Model
{
    public class Q
    {
        public int IdQ { get; set; }
        public int IdExpt { get; set; }
        public List<Expt> Expts { get; set; }

    }
}
