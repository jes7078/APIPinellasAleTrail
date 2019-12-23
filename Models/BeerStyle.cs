using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeerStyleAPI.Models
{
  public class BeerStyle
  {
public int Id {get;set;}

public string Style {get;set;}

public string Description {get;set;}

public string StyleURL {get;set;}


  }
}