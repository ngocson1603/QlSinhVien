using QlSinhVien.Model;
using QlSinhVien.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace QlSinhVien.Responsitory
{
    public interface IHocSinhRepository : IRepository<HocSinh>
    {
        public List<HocSinhViewModel> listHocSinh(string name);
        public List<HocSinhViewModel> listHocSinhPro();
        Task<string> UploadFile(IFormFile _IFormFile);
        Task<(byte[], string, string)> DownloadFile(string FileName);
    }
}
