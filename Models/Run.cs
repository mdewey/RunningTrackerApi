using System;

namespace RunningTrackerApi.Models
{
  public class Run
  {
    public int Id { get; set; }
    public string Location { get; set; }
    public double Distance { get; set; }
    public DateTime When { get; set; } = DateTime.Now;
  }
}