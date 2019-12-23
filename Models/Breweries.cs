using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BreweriesAPI.Models
{
  public class Breweries
  {
    public int Id{get;set;}

    public string Name{get;set;}

    public string Url{get;set;}

    public string Address{get;set;}

    public string PhoneNumber{get;set;}

    public string Website{get;set;}

  }
}