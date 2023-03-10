using System.Text.RegularExpressions;

namespace MvcCoreUtilidades.Helpers
{
    public enum AllFolders { Images = 0}
    public class HelperPathImages
    {
        private readonly HelperPathProvider helperPathImages;
        private IWebHostEnvironment hostEnvironment;

        public HelperPathImages(HelperPathProvider helperPathImages, IWebHostEnvironment hostEnvironment)
        {
            this.helperPathImages = helperPathImages;
            this.hostEnvironment = hostEnvironment;
        }

        public async Task<string> UploadFileAsync(IFormFile file, string fileName, string host, AllFolders folder)
        {
            /*string type = file.FileName.Split('.')[1];*/
            /*string finalFileName = fileName + "." + type;*/

            string carpeta = "";
            if (folder == AllFolders.Images)
            {
                carpeta = "users/images";
            }

            string rootPath = this.hostEnvironment.WebRootPath;
            string path = Path.Combine(rootPath, carpeta, fileName);

            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
                return Path.Combine(host, carpeta, fileName);
            }
        }
    }
}
