using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NonogramPuzzle.ViewModels;

namespace NonogramPuzzle.Controllers
{
  public class CellViewModelsController: Controller
  {
    static List<CellViewModel> cells = new List<CellViewModel>();
    //const int gridSize = 28;

    public IActionResult Index()
    {
      // if (cells.Count < gridSize)
      // {
      //   for( int i = 0; i < gridSize ; i++)
      //   {
      //     cells.Add(new CellViewModel { CellId = i, CellState = 0 });
      //   }
      // }

      return View(cells);
    }

    public IActionResult Build()
    {
      ViewBag.ShowQuestion = true;
      return View();
    }

    [HttpPost]
    public IActionResult Build(BoardViewModel model)
    {
      int gridSize = (model.Width * model.Height);
      
      if (!ModelState.IsValid)
      {
        return View(model);
      }
      else
      {
        cells.Clear();
        if (cells.Count < gridSize)
        {
          for( int i = 0; i < gridSize ; i++)
          {
            cells.Add(new CellViewModel { CellId = i, CellState = 0 });
          }
          model.CellViewModels = cells;
        }

        ViewBag.ShowQuestion = false;
        
        return View (model);
      }
    }

    public IActionResult HandleCellClick(string cellNumber, string height, string width)
    {

      int cllNmbr = int.Parse(cellNumber);

      cells.ElementAt(cllNmbr).CellState = (cells.ElementAt(cllNmbr).CellState +1) % 2;

      ViewBag.ShowQuestion = false;

      BoardViewModel model = new BoardViewModel();
      model.CellViewModels = cells;
      model.Width = int.Parse(width);
      model.Height = int.Parse(height);
      
      return View("Build", model);
    }
  }
}

