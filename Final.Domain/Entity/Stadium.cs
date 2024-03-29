﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Final.Domain.Entity;

public class Stadium
{
    public long? Id { get; set; }


    public string? Name { get; set; }


    public string? Adress { get; set; }
    public List<StadiumPhotos> StadiumPhoto { get; set; }
    public string StadiumLocation { get; set; }
    public string StadiumNumber { get; set; }
}
