using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIPinellasAleTrail.Models;

namespace APIPinellasAleTrail.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class BeersController:ControllerBase
  {
    private readonly DatabaseContext db;
    public BeersController(DatabaseContext context)
    {
      db=context;
    }








[HttpGet]
public async Task<ActionResult<IEnumerable<Beers>>> GetAllBeers()
{
 
  return await db.Beers.Include(i => i.BeerStyle).Include(i=>i.Breweries).OrderBy(o=>o.Name).ToListAsync();
}








[HttpGet("{id}")]
public async Task<ActionResult<Beers>> GetOneBeer(int id)
{
  var beer = await db.Beers.Include(i => i.BeerStyle).Include(i=>i.Breweries).FirstOrDefaultAsync(f=>f.Id==id);
  if (beer==null)
  {
    return NotFound();
  }
    return beer;
}






[HttpPost]
public async Task<ActionResult<Beers>> CreateBeer(Beers Beer)
{
 db.Beers.Add(Beer);
 await db.SaveChangesAsync();
 return CreatedAtAction("GetOneBeer", new{id =Beer.Id},Beer);
}







[HttpDelete("{id}")]
public async Task<ActionResult<Beers>> DeleteBeer(int id)
{
var beer=await db.Beers.FindAsync(id);
if (beer==null)
{
  return NotFound();
}
db.Beers.Remove(beer);
await db.SaveChangesAsync();
return beer;
}




[HttpPut("{id}")]
public async Task<IActionResult> UpdateBeer (int id, Beers Beer)
{
  if (id!=Beer.Id)
  {
    return BadRequest();
  }
  db.Entry(Beer).State=EntityState.Modified;

  try
  {
    await db.SaveChangesAsync();
  }
  catch(DbUpdateConcurrencyException)
  {
    if(!BeerExists(id))
    {
      return NotFound();
    }
    else
    {
      throw;
    }
  }
 return NoContent();
}

private bool BeerExists(int id)
{
  return db.Beers.Any(e=>e.Id==id);
}

  }
}