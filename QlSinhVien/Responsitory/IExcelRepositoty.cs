using QlSinhVien.Model;
using System.Data;

namespace QlSinhVien.Responsitory
{
    public interface IExcelRepositoty
    {
        DataTable CustomerDataTable(string path);
        string DocumentUpload(IFormFile formFile);
        void Import(DataTable hocsinh);
    }
}
