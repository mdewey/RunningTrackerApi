using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunningTrackerApi.Models;

namespace RunningTrackerApi.Controller
{
  [Route("api/[controller]")]
  [ApiController]
  public class SeedController : ControllerBase
  {
    private readonly DatabaseContext _context;

    public SeedController(DatabaseContext context)
    {
      _context = context;
    }

    // GET: api/Seed
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Run>>> Seed()
    {

      var locations = new List<string> { "Vinoy Park", "Disney World", "Weedon", "Sawgrass", "Gandy", "Fort De Soto" };

      for (int i = 0; i < 5; i++)
      {
        var run = new Run
        {
          Location = locations.OrderBy(o => Guid.NewGuid()).First(),
          Distance = new Random().Next(2, 14),
          When = DateTime.Now.AddDays(0 - i).AddHours(new Random().Next(0, 12))
        };
        _context.Add(run);
      }
      await _context.SaveChangesAsync();
      return Ok();
    }


    [HttpGet("reset")]
    public async Task<ActionResult> Reset()
    {
      _context.Runs.RemoveRange(_context.Runs);
      await _context.SaveChangesAsync();
      return Ok();
    }
  }
}