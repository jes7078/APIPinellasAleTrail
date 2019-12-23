using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIPinellasAleTrail.Models;

namespace APIPinellasAleTrail.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class BeerStyleController:ControllerBase
  {
[HttpGet]
public ActionResult GetAllBeerStyle()
{
  var db = new DatabaseContext();
  return Ok(db.BeerStyle);
}



  }
}