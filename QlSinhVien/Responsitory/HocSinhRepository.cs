using Dapper;
using QlSinhVien.Data;
using QlSinhVien.Helper;
using QlSinhVien.Model;
using QlSinhVien.ViewModel;
using ExcelDataReader;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OfficeOpenXml;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace QlSinhVien.Responsitory
{
    public class HocSinhRepository : Repository<HocSinh>, IHocSinhRepository
    {
        private IConfiguration configuration;
        private IWebHostEnvironment webHostEnvironment;
        public HocSinhRepository(ApplicationDBContext context,IConfiguration _configuration,IWebHostEnvironment _webHostEnvironment) : base(context)
        {
            configuration = _configuration;
            webHostEnvironment = _webHostEnvironment;
        }
        public List<HocSinhViewModel> listHocSinh(string name)
        {
            var query = @" SELECT
                            hs.TenHS as tenHocSinh,
                            lh.TenLop as tenLop,
                            k.Khoi as khoi,
                            (d.Toan + d.Ly + d.Hoa + d.Anh + d.Sinh + d.Van) / 6 AS tongDiem,
                            CASE
                                WHEN (d.Toan > 8 OR d.Van > 8) AND (d.Toan + d.Ly + d.Hoa + d.Anh + d.Sinh + d.Van) / 6 > 8 THEN 'Giỏi'
                                WHEN (d.Toan > 7 OR d.Van > 7) AND (d.Toan + d.Ly + d.Hoa + d.Anh + d.Sinh + d.Van) / 6 >= 7 THEN 'Khá'
                                ELSE 'Trung Bình'
                            END AS xeploai
                        FROM diem d
                        LEFT JOIN hocsinh hs ON hs.Id = d.IdHocSinh
                        LEFT JOIN lophoc lh ON hs.IdLop = lh.Id
                        LEFT JOIN khoilop k ON lh.IdKhoiLop = k.Id where hs.TenHS = @name";
            var parameter = new DynamicParameters();
            parameter.Add("name", name);
            var result = Context.Database.GetDbConnection().Query<HocSinhViewModel>(query, parameter);
            return result.ToList();
        }
        public List<HocSinhViewModel> listHocSinhPro()
        {
            var query = @"call GetUsers()";
            var result = Context.Database.GetDbConnection().Query<HocSinhViewModel>(query);
            return result.ToList();
        }
        #region exce;
        public async Task<(byte[], string, string)> DownloadFile(string FileName)
        {
            try
            {
                var _GetFilePath = Common.GetFilePath(FileName);
                var provider = new FileExtensionContentTypeProvider();
                if (!provider.TryGetContentType(_GetFilePath, out var _ContentType))
                {
                    _ContentType = "application/octet-stream";
                }
                var _ReadAllByteAsync = await File.ReadAllBytesAsync(_GetFilePath);
                return (_ReadAllByteAsync, _ContentType, Path.GetFileName(_GetFilePath));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //public async Task<object> UploadFile(IFormFile _IFormFile)
        //{
        //    string FileName = "";
        //    try
        //    {
        //        FileInfo _FileInfo = new FileInfo(_IFormFile.FileName);
        //        FileName = _IFormFile.FileName + "_" + DateTime.Now.Ticks.ToString() + _FileInfo.Extension;
        //        var _GetFilePath = Common.GetFilePath(FileName);
        //        using (var _FileStream = new FileStream(_GetFilePath, FileMode.Create))
        //        {
        //            await _IFormFile.CopyToAsync(_FileStream);
        //        }
        //        // Set the EPPlus LicenseContext to ensure compliance with licensing
        //        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        //        FileName = _IFormFile.FileName + "_" + DateTime.Now.Ticks.ToString() + _FileInfo.Extension;

        //        // Use EPPlus to read the Excel file
        //        using (var package = new ExcelPackage(new FileInfo(_GetFilePath)))
        //        {
        //            var worksheet = package.Workbook.Worksheets[0];
        //            var rowCount = worksheet.Dimension.Rows;
        //            var columnCount = worksheet.Dimension.Columns;

        //            var excelData = new List<List<object>>();

        //            for (int row = 1; row <= rowCount; row++)
        //            {
        //                var rowData = new List<object>();
        //                for (int col = 1; col <= columnCount; col++)
        //                {
        //                    rowData.Add(worksheet.Cells[row, col].Text);
        //                }
        //                excelData.Add(rowData);
        //            }
        //            return excelData;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        #endregion
        public async Task<string> UploadFile(IFormFile _IFormFile)
        {
            try
            {
                if (_IFormFile == null || _IFormFile.Length == 0)
                {
                    return "No file uploaded or the file is empty.";
                }

                using (var stream = _IFormFile.OpenReadStream())
                {
                    // Set the EPPlus LicenseContext to ensure compliance with licensing
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                    using (var package = new ExcelPackage(stream))
                    {
                        var worksheet = package.Workbook.Worksheets[0];
                        var rowCount = worksheet.Dimension.Rows;
                        var columnCount = worksheet.Dimension.Columns;

                        var excelData = new List<Dictionary<string, string>>();

                        // Extract the column names from the first row
                        var columnNames = Enumerable.Range(1, columnCount)
                            .Select(col => worksheet.Cells[1, col].Text)
                            .ToArray();

                        for (int row = 2; row <= rowCount; row++) // Start from row 2 to skip the header
                        {
                            var rowData = new Dictionary<string, string>();
                            for (int col = 1; col <= columnCount; col++)
                            {
                                rowData[columnNames[col - 1]] = worksheet.Cells[row, col].Text;
                            }
                            excelData.Add(rowData);
                        }
                        return JsonConvert.SerializeObject(excelData, Formatting.Indented);
                    }
                }
            }
            catch (Exception ex)
            {
                return $"An error occurred: {ex.Message}";
            }
        }
    }
}
