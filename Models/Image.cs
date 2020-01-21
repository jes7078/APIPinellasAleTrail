using System;

namespace APIPinellasAleTrail.Models{

public class Image
{
  public int Id{get;set;}
  public string Url {get;set;}
  public DateTime Created {get;set;}=DateTime.UtcNow;
}
}