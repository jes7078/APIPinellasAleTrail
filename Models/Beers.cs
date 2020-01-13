using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APIPinellasAleTrail.Models
{
  public class Beers 
  {
    public int Id {get;set;}

    public string Name {get;set;}

    public string Description {get;set;}

    public string BeerURL {get;set;}

    public string ABV {get;set;}

    public int BeerStyleId {get;set;}

    public int BreweriesId {get;set;}

    public BeerStyle BeerStyle {get;set;}

    public Breweries Breweries {get;set;}
  }
}