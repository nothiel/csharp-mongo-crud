using PlayerApiMongo.Models;
using PlayerApiMongo.Services;
using Microsoft.AspNetCore.Mvc;

namespace PlayerApiMongo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlayersController : ControllerBase
{
    private readonly PlayersService _PlayersService;

    public PlayersController(PlayersService playersService) =>
        _PlayersService = playersService;

    [HttpGet]
    public async Task<List<Player>> Get() =>
        await _PlayersService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Player>> Get(string id)
    {
        var player = await _PlayersService.GetAsync(id);

        if (player is null)
        {
            return NotFound();
        }

        return player;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Player newPlayer)
    {
        await _PlayersService.CreateAsync(newPlayer);

        return CreatedAtAction(nameof(Get), new { id = newPlayer.Id }, newPlayer);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Player updatedPlayer)
    {
        var Player = await _PlayersService.GetAsync(id);

        if (Player is null)
        {
            return NotFound();
        }

        updatedPlayer.Id = Player.Id;

        await _PlayersService.UpdateAsync(id, updatedPlayer);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var Player = await _PlayersService.GetAsync(id);

        if (Player is null)
        {
            return NotFound();
        }

        await _PlayersService.RemoveAsync(id);

        return NoContent();
    }
}