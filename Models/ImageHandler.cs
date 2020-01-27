using System.IO;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

namespace APIPinellasAleTrail.Models{
public interface IImageHandler
{
  Task<string> UploadImage(IFormFile file,string orientation);
  Task DeleteFile(string path);
}

public class ImageHandler : IImageHandler
{
  private readonly IImageWriter _imageWriter;
  public ImageHandler(IImageWriter imageWriter)
  {
    _imageWriter = imageWriter;
  }
  public async Task DeleteFile(string path)
  {
    File.Delete(path);
  }
  public async Task<string> UploadImage(IFormFile file, string orientation)
  { 
    var result = await _imageWriter.UploadImage(file,orientation);
    return result;
  }
}
}