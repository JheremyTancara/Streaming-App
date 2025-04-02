using System.ComponentModel.DataAnnotations.Schema;
using Api.Models.Interface;

namespace Api.Models;

[Table("movies")]
public class Movie : IMovie
{
    [Column("movieid")]
    public int MovieID { get; set; } 
    
    [Column("title")]
    public string Title { get; set; } = string.Empty;    
    
    [Column("genre")]
    public List<Genre> Genre { get; set; } = new(); 
    
    [Column("releasedate")]
    public DateTime ReleaseDate { get; set; }
    
    [Column("duration")]
    public double Duration { get; set; }
    
    [Column("rating")]
    public double Rating { get; set; }
    
    [Column("description")]
    public string Description { get; set; } = string.Empty;
    
    [Column("cast")]
    public string[] Cast { get; set; } = Array.Empty<string>(); 
    
    [Column("director")]
    public string Director { get; set; } = string.Empty;
    
    [Column("imageurl")]
    public string ImageUrl { get; set; } = string.Empty;
    
    [Column("trailerurl")]
    public string TrailerUrl { get; set; } = string.Empty;
    
    [Column("type")]
    public ContentType Type { get; set; }
    
    [Column("views")]
    public int Views { get; set; }
    
    [Column("agerestriction")]
    public int AgeRestriction { get; set; }
}