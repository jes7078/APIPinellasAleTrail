using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace APIPinellasAleTrail.Models{


public interface IImageWriter
{
  Task<string> UploadImage(IFormFile file);
}
public class ImageWriter : IImageWriter
{
  public async Task<string> UploadImage(IFormFile file)
  {
    var exists = CheckIfImageFile(file);
    if (exists)
    {
      return await WriteFile(file);
    }
    return "Invalid image file";
  }

private bool CheckIfImageFile(IFormFile file)
{
  byte[] fileBytes;
  using (var ms = new MemoryStream())
  {
    file.CopyTo(ms);
    fileBytes = ms.ToArray();
  }
  return WriterHelper.GetImageFormat(fileBytes) != WriterHelper.ImageFormat.unknown;
}

public async Task<string> WriteFile(IFormFile file)
{
  string fileName;
  var path = string.Empty;
  try
  {
    var extension="." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
    fileName=Guid.NewGuid().ToString() + extension;
    path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
    using (var bits = new FileStream(path, FileMode.Create))
    {
      await file.CopyToAsync(bits);
    }
  }
  catch (Exception e)
  {
    return e.Message;
  }
  return path;
}

public class WriterHelper
{
  public enum ImageFormat
  {
      bmp,
      jpeg,
      gif,
      tiff,
      png,
      unknown
  }
  public static ImageFormat GetImageFormat(byte[] bytes)
  {
    var bmp = Encoding.ASCII.GetBytes("BM");
    var gif = Encoding.ASCII.GetBytes("GIF");
    var png = new byte[] {137,80,78,71};
    var tiff = new byte[] {73,73,42};
    var tiff2 = new byte[] {77,77,42};
    var jpeg = new byte [] {255,216,255,224};
    var jpeg2= new byte [] {255,216,255,225};

    if (bmp.SequenceEqual(bytes.Take(bmp.Length)))
    return ImageFormat.bmp;

    if (gif.SequenceEqual(bytes.Take(gif.Length)))
    return ImageFormat.gif;

    if (png.SequenceEqual(bytes.Take(png.Length)))
    return ImageFormat.png;

    if (tiff.SequenceEqual(bytes.Take(tiff.Length)))
    return ImageFormat.tiff;

    if (tiff2.SequenceEqual(bytes.Take(tiff2.Length)))
    return ImageFormat.tiff;

    if (jpeg.SequenceEqual(bytes.Take(jpeg.Length)))
    return ImageFormat.jpeg;

    if (jpeg2.SequenceEqual(bytes.Take(jpeg2.Length)))
    return ImageFormat.jpeg;

    return ImageFormat.unknown;

  }
}
}

}
