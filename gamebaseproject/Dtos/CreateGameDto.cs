using System.ComponentModel.DataAnnotations;

namespace gamebaseproject.Api.Dtos;

//Post create
public record class CreateGameDto(
    //int Id, id not included as it will be auto provided by the system
    [Required][StringLength(50)]string Name, 
    [Required][StringLength(20)]string Genre, 
    [Range(1, 100)]decimal Price, 
    DateOnly ReleaseDate);
