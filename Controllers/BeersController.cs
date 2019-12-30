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
  return Ok(db.Beers);
}

[HttpGet("{id}")]
public ActionResult GetOneBeer(int id)
{
  var db = new DatabaseContext();
  var beer = db.Beers.FirstOrDefault(Br=>Br.Id==id);
  if (beer==null)
  {
    return NotFound();
  }
  else
  {
    return Ok (beer);
  }
}

[HttpPost]
public ActionResult CreateBeer(Beers Beer)
{
  var db = new DatabaseContext();
  var sty = db.BeerStyle.FirstOrDefault(st=>st.Style==Beer.Style);
  Beer.BeerStyleId=sty.Id;
  var bre = db.Breweries.FirstOrDefault(br=> br.Name==Beer.Brewery);
  Beer.BreweriesId=bre.Id;
  Beer.Id=0;
  db.Beers.Add(Beer);
  db.SaveChanges();
  return Ok(Beer);
}

[HttpDelete("{id}")]
public ActionResult DeleteBeer(int id)
{
var db=new DatabaseContext();
  var beer = db.Beers.FirstOrDefault(Br=>Br.Id==id);
  if (beer==null)
  {
    return NotFound();
  }
  else
  {
    db.Beers.Remove(beer);
    db.SaveChanges();
    return Ok();
  }

}

[HttpPut("{id}")]
public ActionResult UpdateBeer (Beers Beer)
{
  var db= new DatabaseContext();
  var prevBeer=db.Beers.FirstOrDefault(br=>br.Id==Beer.Id);
  if (prevBeer == null)
  {
    return NotFound();
  }
  else 
  {
    var sty = db.BeerStyle.FirstOrDefault(st=>st.Style==Beer.Style);
    var bre = db.Breweries.FirstOrDefault(br=> br.Name==Beer.Brewery);
    prevBeer.Name=Beer.Name;
    prevBeer.Brewery=Beer.Brewery;
    prevBeer.Style=Beer.Style;
    prevBeer.Description=Beer.Description;
    prevBeer.BeerURL=Beer.BeerURL;
    prevBeer.ABV=Beer.ABV;
    prevBeer.BeerStyleId=sty.Id;
    prevBeer.BreweriesId=bre.Id;
    db.SaveChanges();
    return Ok(prevBeer);
  }
}

  }
}