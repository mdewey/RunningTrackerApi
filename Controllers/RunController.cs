using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunningTrackerApi.Models;

namespace RunningTrackerApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class RunController : ControllerBase
  {
    private readonly DatabaseContext _context;

    public RunController(DatabaseContext context)
    {
      _context = context;
    }

    // GET: api/Run
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Run>>> GetRuns()
    {
      return await _context.Runs.ToListAsync();
    }

    // GET: api/Run/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Run>> GetRun(int id)
    {
      var run = await _context.Runs.FindAsync(id);

      if (run == null)
      {
        return NotFound();
      }

      return run;
    }


    // PUT: api/Run/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see https://aka.ms/RazorPagesCRUD.
    [HttpPut("{id}")]
    public async Task<IActionResult> PutRun(int id, Run run)
    {
      if (id != run.Id)
      {
        return BadRequest();
      }

      _context.Entry(run).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!RunExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return NoContent();
    }

    // POST: api/Run
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see https://aka.ms/RazorPagesCRUD.
    [HttpPost]
    public async Task<ActionResult<Run>> PostRun(Run run)
    {
      _context.Runs.Add(run);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetRun", new { id = run.Id }, run);
    }

    // DELETE: api/Run/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<Run>> DeleteRun(int id)
    {
      var run = await _context.Runs.FindAsync(id);
      if (run == null)
      {
        return NotFound();
      }

      _context.Runs.Remove(run);
      await _context.SaveChangesAsync();

      return run;
    }

    private bool RunExists(int id)
    {
      return _context.Runs.Any(e => e.Id == id);
    }
  }
}
