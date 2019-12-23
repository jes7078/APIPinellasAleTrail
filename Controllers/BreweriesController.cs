using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIPinellasAleTrail.Models;

namespace APIPinellasAleTrail.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class BreweriesController:ControllerBase
  {
[HttpGet]
public ActionResult GetAllBreweries()
{
  var db = new DatabaseContext();
  return Ok(db.Breweries);
}



  }
}