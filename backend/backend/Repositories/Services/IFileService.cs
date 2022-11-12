namespace backend.Repositories.Services
{
    public interface IFileService
    {
        public Task<Tuple<int,string>> SaveImage(IFormFile file);
        public Task<bool> DeleteImage(string imageName);
    }
}
