using gamebaseproject.Api.Dtos;

namespace gamebaseproject.Api.Endpoints;


//static extension class
public static class GameEndPoints
{
const string GetGameEndpointName = "GetGame";


private static readonly List<GameDto> games = [
    new (1,
    "Street Fighter II",
    "Fighting",
    20.00M,
    new DateOnly(1992, 7, 15)),
     new (2,
    "Final Fantasy XIV",
    "Roleplaying",
    59.99M,
    new DateOnly(2010, 9, 30)),
     new (3,
    "FIFA 23",
    "Sports",
    39.00M,
    new DateOnly(2023, 8, 25)),
];

public static RouteGroupBuilder MapGamesEndPoints(this WebApplication app)
{
    //declaring a var of games
    var group = app.MapGroup("games");

    //Get Req
group.MapGet("/", () => games);

//Get 1 
group.MapGet("/{id}", (int id) => {

GameDto? game = games.Find(game => game.Id == id);

return game is null? Results.NotFound() : Results.Ok(game);
})
.WithName(GetGameEndpointName);

//Post add game
group.MapPost("/", (CreateGameDto newGame) => {

    // if (string.IsNullOrEmpty(newGame.Name)){
    //     return Results.BadRequest("Name cannot be empty");
    // }
    GameDto game = new(
        games.Count + 1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate
    );
    games.Add(game);

    return Results.CreatedAtRoute(GetGameEndpointName, new {id = game.Id}, game);
})
.WithParameterValidation();

//Put Update games
group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) => {
    var index = games.FindIndex(game => game.Id == id);

//break point
    if (index == -1)
    {
        return Results.NotFound();
    }

    games[index] = new GameDto(
        id,
        updatedGame.Name,
        updatedGame.Genre,
        updatedGame.Price,
        updatedGame.ReleaseDate
    );

    return Results.NoContent();
});

//Delete
group.MapDelete("/{id}", (int id) => {
  games.RemoveAll(game => game.Id == id);
  return Results.NoContent();
});
return group;
}
}
