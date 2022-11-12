using System.IO;
namespace backend.Repositories.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _environment;  
        public FileService(IWebHostEnvironment env)
        {
            _environment = env; 
        }
        public async Task<bool> DeleteImage(string imageName)
        {
            try
            {
                var path = _environment.ContentRootPath;
                var filePath = Path.Combine(path,"Upload\\",imageName);
                if (File.Exists(filePath))
                {
                    await Task.Run(()=> {
                        File.Delete(filePath);
                        });
                    return true;
                }
                return false;   
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<Tuple<int, string>> SaveImage(IFormFile file)
        {
            try
            {
                var contentPath = _environment.ContentRootPath;
                var path = Path.Combine(contentPath, "Uploads");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);    
                }
                //we gonna check the file extensions
                var extension = Path.GetExtension(file.FileName);
                var allowedExtensions = new string[]
                {
                    ".jpg",".png",".jpeg"
                };
                if(!allowedExtensions.Contains(extension))
                {
                    return new Tuple<int, string>(0, $"Only {string.Join(",", allowedExtensions)} are allowed ");
                }
                string unique = Guid.NewGuid().ToString();
                //we create an unique filename
                var newFileName = unique + extension;
                var newFilePath = Path.Combine(path, newFileName);
                var stream = new FileStream(newFilePath, FileMode.Create);  
                await file.CopyToAsync(stream);
                stream.Close();
                return new Tuple<int, string>(1, newFileName);


            }catch (Exception ex)
            {
                return new Tuple<int, string>(0, "an error occured!");
            }
        }
    }
}
