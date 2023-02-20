using NonogramPuzzle.Models;

namespace NonogramPuzzle.ViewModels
{
  public class CellViewModel
  {
    public int CellId { get; set; }
    public int CellState { get; set; }

    //public virtual List<Nonogram> nonograms {get; set; }
    public int NonogramId { get; set; }
  }
}