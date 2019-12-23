using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIPinellasAleTrail.Models;

namespace APIPinellasAleTrail.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class BeersController:ControllerBase
  {
[HttpGet]
public ActionResult GetAllBeers()
{
  var db = new DatabaseContext();
  return Ok(db.Beers.OrderBy(Beers=>Beers.Name));
}



  }
}