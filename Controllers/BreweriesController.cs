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

[HttpGet("{id}")]
public ActionResult GetOneBrewery(int id)
{
  var db = new DatabaseContext();
  var brewery = db.Breweries.FirstOrDefault(Brew=>Brew.Id==id);
  if (brewery==null)
  {
    return NotFound();
  }
  else
  {
    return Ok (brewery);
  }
}

[HttpPost]
public ActionResult CreateBrewery(Breweries Brewery)
{
  var db = new DatabaseContext();
  Brewery.Id=0;
  db.Breweries.Add(Brewery);
  db.SaveChanges();
  return Ok(Brewery);
}

[HttpDelete("{id}")]
public ActionResult DeleteBrewery(int id)
{
var db=new DatabaseContext();
  var brewery = db.Breweries.FirstOrDefault(Bre=>Bre.Id==id);
  if (brewery==null)
  {
    return NotFound();
  }
  else
  {
    db.Breweries.Remove(brewery);
    db.SaveChanges();
    return Ok();
  }

}


  }
}