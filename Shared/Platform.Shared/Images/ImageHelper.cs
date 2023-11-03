using Microsoft.Extensions.Hosting;
using System.Drawing;
using System.Drawing.Imaging;

namespace Platform.Shared.Images
{
    public class ImageHelper
    {
        //public static string SaveUrl= AppDomain.CurrentDomain.BaseDirectory.Split("Microservice")[0];
        public static string SaveUrl = "wwwroot\\Images";



        public static string SaveImage(string webRootPath, string imageBase64)
        {
            try
            {
            string createdFileName = string.Empty;
            if (!string.IsNullOrEmpty(imageBase64))
            {
                byte[] fileBytes = Convert.FromBase64String(imageBase64);
                var imageFormat = GetRawImageFormat(fileBytes);

                if (!imageFormat.Equals(ImageFormat.Jpeg) && !imageFormat.Equals(ImageFormat.Png) && !imageFormat.Equals(ImageFormat.Gif))
                {
                    throw new Exception("IncorrectImageFormat");
                }

                string fileStorePath = webRootPath;
                if (!Directory.Exists(fileStorePath))
                {
                    Directory.CreateDirectory(fileStorePath);
                }
                Guid fileId = Guid.NewGuid();
                //string fileType = getFileType(imageBase64.Split(',')[0]);
                string fileName = fileId.ToString() +"."+ imageFormat.ToString().ToLower();
                string fullFilename = @fileStorePath + "\\" + fileName;
                FileStream file = File.Create(fullFilename);

                file.Write(fileBytes, 0, fileBytes.Length);
                file.Close();
                createdFileName = fileName;
            }
            return createdFileName;
            }
            catch (FormatException ex){
                return "";
            }
        }

        public static string getFileType(string fileHeader)
        {
            string fileType = "";
            if (fileHeader.ToLower().Contains("png"))
                fileType = ".png";
            else if (fileHeader.ToLower().Contains("jpeg"))
                fileType = ".jpeg";
            else if (fileHeader.ToLower().Contains("jpg"))
                fileType = ".jpg";
            return fileType;
        }

        public static ImageFormat GetRawImageFormat(byte[] fileBytes)
        {
            using (var ms = new MemoryStream(fileBytes))
            {
                var fileImage = Image.FromStream(ms);
                return fileImage.RawFormat;
            }
        }

        public static bool DeleteImage(string webRootPath, string filePath)
        {
            if (File.Exists(webRootPath+"\\"+filePath)) // Check if the file exists
            {
                File.Delete(webRootPath+"\\"+filePath); // Delete the file
                return true;
            }
            return false;
        }
    }
}
