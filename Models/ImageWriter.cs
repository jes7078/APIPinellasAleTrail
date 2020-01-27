using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Metadata.Profiles.Exif;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace APIPinellasAleTrail.Models{


public interface IImageWriter
{
  Task<string> UploadImage(IFormFile file, string orientation);
}
public class ImageWriter : IImageWriter
{
  public async Task<string> UploadImage(IFormFile file, string orientation)
  {
    var exists = CheckIfImageFile(file);
    if (exists)
    {
      return await WriteFile(file, orientation);
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

  private byte[] TransformAvatarIfNeeded(byte[] imageInBytes, string orientation)
    {
        using (var image = SixLabors.ImageSharp.Image.Load(imageInBytes))
        {
            if (orientation == null) return imageInBytes;

            RotateMode rotateMode;
            FlipMode flipMode;
            SetRotateFlipMode(orientation, out rotateMode, out flipMode);

            image.Mutate(x => x.RotateFlip(rotateMode, flipMode));
           

            var imageFormat = SixLabors.ImageSharp.Image.DetectFormat(imageInBytes);

            return ImageToByteArray(image, imageFormat);
        }
    }

    private byte[] ImageToByteArray(Image<Rgba32> image, IImageFormat imageFormat)
    {
        using (var ms = new MemoryStream())
        {
            image.Save(ms, imageFormat);
            return ms.ToArray();
        }
    }

    private void SetRotateFlipMode(string orientation, out RotateMode rotateMode, out FlipMode flipMode)
    {
     
        switch (orientation)
        {
            case "2":
                rotateMode = RotateMode.None;
                flipMode = FlipMode.Horizontal;
                break;
            case "3":
                rotateMode = RotateMode.Rotate180;
                flipMode = FlipMode.None;
                break;
            case "4":
                rotateMode = RotateMode.Rotate180;
                flipMode = FlipMode.Horizontal;
                break;
            case "5":
                rotateMode = RotateMode.Rotate90;
                flipMode = FlipMode.Horizontal;
                break;
            case "6":
                rotateMode = RotateMode.Rotate90;
                flipMode = FlipMode.None;
                break;
            case "7":
                rotateMode = RotateMode.Rotate90;
                flipMode = FlipMode.Vertical;
                break;
            case "8":
                rotateMode = RotateMode.Rotate270;
                flipMode = FlipMode.None;
                break;
            default:
                rotateMode = RotateMode.None;
                flipMode = FlipMode.None;
                break;
        }
    }
public async Task<string> WriteFile(IFormFile file, string orientation)
{
  string fileName;
  var path = string.Empty;
  try
  {
    var extension="." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
    fileName=Guid.NewGuid().ToString() + extension;
    path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
    // need to flip it
    using (var ms = new MemoryStream()){
      file.CopyTo(ms);
      var fileBytes = ms.ToArray();
      if (orientation != null){
        fileBytes = this.TransformAvatarIfNeeded(fileBytes, orientation);
      }
      await File.WriteAllBytesAsync(path, fileBytes); 
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
