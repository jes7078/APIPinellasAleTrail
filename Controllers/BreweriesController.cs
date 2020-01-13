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
  
  public class BreweriesController:ControllerBase
  {
private readonly DatabaseContext db;
public BreweriesController(DatabaseContext context)
{
  db=context;
}

[HttpGet]
public async Task<ActionResult<IEnumerable<Breweries>>> GetAllBreweries()
{
  return await db.Breweries.OrderBy(o=>o.Name).ToListAsync();
}

[HttpGet("{id}")]
public async Task<ActionResult<Breweries>> GetOneBrewery(int id)
{
 var brewery=await db.Breweries.FirstOrDefaultAsync(f=>f.Id==id);
 if (brewery==null)
 {
   return NotFound();
 }
 return brewery;
}

[HttpPost]
public async Task<ActionResult<Breweries>> CreateBrewery(Breweries Brewery)
{
 db.Breweries.Add(Brewery);
 await db.SaveChangesAsync();
 return CreatedAtAction("GetBrewery", new{id=Brewery.Id},Brewery);
}

[HttpDelete("{id}")]
public async Task<ActionResult<Breweries>> DeleteBrewery(int id)
{
var brewery= await db.Breweries.FindAsync(id);
if (brewery==null)
{
  return NotFound();
}
db.Breweries.Remove(brewery);
await db.SaveChangesAsync();
return brewery;

}

[HttpPut("{id}")]
public async Task<IActionResult> UpdateBrewery (int id, Breweries Breweries)
{
if (id !=Breweries.Id)
{
  return BadRequest();
}
db.Entry(Breweries).State=EntityState.Modified;
try
{
  await db.SaveChangesAsync();
}
catch (DbUpdateConcurrencyException)
{
  if(!BreweryExists(id))
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

private bool BreweryExists(int id)
{
  return db.Breweries.Any(e=>e.Id==id);
}

  }
}