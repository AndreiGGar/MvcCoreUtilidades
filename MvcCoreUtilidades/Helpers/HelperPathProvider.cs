namespace MvcCoreUtilidades.Helpers
{
    public enum AllFolders { Images = 0}
    public class HelperPathImages
    {
        private IWebHostEnvironment hostEnvironment;

        public HelperPathImages(IWebHostEnvironment hostEnvironment)
        {
            this.hostEnvironment = hostEnvironment;
        }

        public string MapPath(string fileName, AllFolders folder)
        {
            string carpeta = "";
            if (folder == AllFolders.Images)
            {
                carpeta = "users/images";
            }
            string rootPath = this.hostEnvironment.WebRootPath;
            string path = Path.Combine(rootPath, carpeta, fileName);
            return path;
        }
    }
}
