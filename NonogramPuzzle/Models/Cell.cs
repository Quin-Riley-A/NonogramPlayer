using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NonogramPuzzle.Models
{
  public class Cell
  {
    public int CellId { get; set; }
    public int CellState { get; set; }
    public int NonogramId { get; set; }
    public Nonogram Nonogram { get; set; }
  }
}