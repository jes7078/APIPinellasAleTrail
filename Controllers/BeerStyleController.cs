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
public ActionResult GetAllBeerStyles()
{
  var db = new DatabaseContext();
  return Ok(db.BeerStyle);
}

[HttpGet("{id}")]
public ActionResult GetOneBeerStyle(int id)
{
  var db = new DatabaseContext();
  var style = db.BeerStyle.FirstOrDefault(Sty=>Sty.Id==id);
  if (style==null)
  {
    return NotFound();
  }
  else
  {
    return Ok (style);
  }
}

[HttpPost]
public ActionResult CreateStyle(BeerStyle BeerStyle)
{
  var db = new DatabaseContext();
  BeerStyle.Id=0;
  db.BeerStyle.Add(BeerStyle);
  db.SaveChanges();
  return Ok(BeerStyle);
}

[HttpDelete("{id}")]
public ActionResult DeleteStyle(int id)
{
var db=new DatabaseContext();
  var style = db.BeerStyle.FirstOrDefault(St=>St.Id==id);
  if (style==null)
  {
    return NotFound();
  }
  else
  {
    db.BeerStyle.Remove(style);
    db.SaveChanges();
    return Ok();
  }

}



  }
}