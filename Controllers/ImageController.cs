using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIPinellasAleTrail;
using APIPinellasAleTrail.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace content.Controllers
{
  [Route("api/[controller]")]
  public class ImageController : Controller
  {
    private readonly IImageHandler _imageHandler;
    private readonly IOptions<CloudinaryKeys> _options;
    private readonly DatabaseContext _context;
    public ImageController(IImageHandler imageHandler, IOptions<CloudinaryKeys> options, DatabaseContext context)
    {
      _imageHandler = imageHandler;
      _options=options;
      _context = context;
      Console.WriteLine(_options.Value.CloudName);
    }

[HttpPost]
public async Task<ActionResult> UploadImage(IFormFile file, [FromForm] string Name)
{
  var path = await _imageHandler.UploadImage(file);
  var rv = new CloudinaryStorage(_options.Value).UploadFile(path);

  var image = new Image{
    Url=rv.SecureUri.AbsoluteUri
  };
  this._context.Images.Add(image);
  await this._context.SaveChangesAsync();
  await _imageHandler.DeleteFile(path);
  return Ok(new {path,image, Name});
}

  }
}