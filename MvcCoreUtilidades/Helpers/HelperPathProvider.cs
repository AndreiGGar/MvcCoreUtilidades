﻿namespace MvcCoreUtilidades.Helpers
{
    public enum Folders { Images = 0}
    public class HelperPathImages
    {
        private IWebHostEnvironment hostEnvironment;

        public HelperPathImages(IWebHostEnvironment hostEnvironment)
        {
            this.hostEnvironment = hostEnvironment;
        }

        public string MapPath(string fileName, Folders folder)
        {
            string carpeta = "";
            if (folder == Folders.Images)
            {
                carpeta = "users/images";
            }
            string rootPath = this.hostEnvironment.WebRootPath;
            string path = Path.Combine(rootPath, carpeta, fileName);
            return path;
        }
    }
}
