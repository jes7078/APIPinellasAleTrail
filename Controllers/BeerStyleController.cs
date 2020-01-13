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
  public class BeerStyleController:ControllerBase
  {
    private readonly DatabaseContext db;
    public BeerStyleController(DatabaseContext context)
    {
      db=context;
    }




  [HttpGet]
public async Task<ActionResult<IEnumerable<BeerStyle>>> GetAllBeerStyles()
{
 
  return await db.BeerStyle.OrderBy(o=>o.Style).ToListAsync();
}






[HttpGet("{id}")]
public async Task<ActionResult<BeerStyle>> GetOneStyle(int id)
{
  var beerstyle = await db.BeerStyle.FirstOrDefaultAsync(f=>f.Id==id);
  if (beerstyle==null)
  {
    return NotFound();
  }
    return beerstyle;
}




[HttpPost]
public async Task<ActionResult<BeerStyle>> CreateStyle(BeerStyle beerStyle)
{
 db.BeerStyle.Add(beerStyle);
 await db.SaveChangesAsync();
 return CreatedAtAction("GetOneStyle", new{id =beerStyle.Id},beerStyle);
}







[HttpDelete("{id}")]
public async Task<ActionResult<BeerStyle>> DeleteBeerStyle(int id)
{
  var beerStyle=await db.BeerStyle.FindAsync(id);
  if (beerStyle==null)
  {
    return NotFound();
  }

  db.BeerStyle.Remove(beerStyle);
  await db.SaveChangesAsync();
  return Ok(beerStyle);
}







[HttpPut("{id}")]
public async Task<IActionResult> UpdateBeerStyle (int id,BeerStyle beerStyle)
{
 if(id!=beerStyle.Id)
 {
   return BadRequest();
 }
 db.Entry(beerStyle).State=EntityState.Modified;

try
{
    await db.SaveChangesAsync();
}
catch (DbUpdateConcurrencyException)
{
  if(!BeerStyleExists(id))
  {
    return NotFound();
  }
  else
  {
    throw;
  }
}

return Ok();
}














private bool BeerStyleExists(int id)
{
  return db.BeerStyle.Any(e=>e.Id==id);
}



  }
}