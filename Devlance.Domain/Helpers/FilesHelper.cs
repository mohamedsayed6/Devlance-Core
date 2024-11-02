using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Devlance.Domain.Helpers
{
    public class FilesHelper
    {
        private readonly IHostingEnvironment _webHostEnvironment;
        private readonly AppSettings _appSettings; 

        public FilesHelper(IHostingEnvironment webHostEnvironment, IOptions<AppSettings> appSettings)
        {
            _webHostEnvironment = webHostEnvironment;
            _appSettings = appSettings.Value; 
        }

        public async Task<string> UploadFile(IFormFile file)
        {
            try
            {
                List<string> imageExtensions =  [ ".JPG", ".JPE", ".BMP", ".GIF", ".PNG", ".JPEG", "HEIF", ".PDF" ];
                string extension = "";
                var fileGuid = Guid.NewGuid().ToString();

                if (file.Length > 2000000)
                    throw new Exception("The size of file must be less than 2 Mb");

                extension = Path.GetExtension(file.FileName).Substring(1);

                if (string.IsNullOrEmpty(extension) || !imageExtensions.Contains(extension.ToUpper()))
                    throw new Exception("Enter Valid Extension");


                string ImageGuid = fileGuid + '.' + extension;
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file.FileName);
                string ImageGuidwithName = fileNameWithoutExtension + '_' + '_' + ImageGuid;
                string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, _appSettings.FilePath, ImageGuidwithName);
                using (Stream stream = new FileStream(imagePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                    stream.Close();
                }

                /*return new ResponseResult<string> { Entity = ImageGuidwithName, IsSuccess = true };*/
                return ImageGuidwithName;
            }
            catch (Exception)
            {
                /*return new ResponseResult<string> { Entity = null, IsSuccess = false, Message = ex.Message };*/
                throw;
            }
        }
        public bool DeleteFile(string FileName)
        {
            string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, _appSettings.FilePath, FileName);

            try
            {
                if (File.Exists(imagePath))
                {
                    File.Delete(imagePath);
                }

                /*return new ResponseResult<string> { IsSuccess = true };*/
                return true;
            }
            catch (Exception)
            {
                /*return new ResponseResult<string> { IsSuccess = false };*/
                return false;
            }
        }
        public string GetFilePath(string fileName)
        {
            string filePath = Path.Combine(_webHostEnvironment.WebRootPath, _appSettings.FilePath, fileName);

            if (!File.Exists(filePath))
            {
                /*return new ResponseResult<string> { message = "File Not Found" };*/
                throw new FileNotFoundException("The specified file was not found.", fileName);
            }

            return filePath; 
        }

    }
}
