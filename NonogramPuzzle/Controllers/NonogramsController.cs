using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

using NonogramPuzzle.Models;
using NonogramPuzzle.ViewModels;

namespace NonogramPuzzle.Controllers
{
  //[Authorize]
  public class NonogramsController : Controller
  {
    private readonly NonogramPuzzleContext _db;
    //private readonly UserManager<ApplicationUser> _userManager;
    
    public NonogramsController(NonogramPuzzleContext db)
    {
      //_userManager = userManager;
      _db = db;
    }

    public ActionResult Index()
    {
      //string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      //ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);

      return View();
    }

    [HttpPost]
    public ActionResult Create(BoardViewModel boardViewModel)
    {
      // if (!ModelState.IsValid)
      // {
      //   return View(nonogram);
      // }
      // else
      // {
        // string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        // ApplicationUser currentUser = await _userManager.FindByIdAsync(userId); 
        // nonogram.User = currentUser;
        Nonogram nonogram = new Nonogram();
//      List<int> Cells = ;
        nonogram.NonogramHeight = boardViewModel.Height;
        nonogram.NonogramWidth = boardViewModel.Width;
        
        for (int i = 0; i < boardViewModel.CellViewModels.Count; i++)
        {
          Cell cell = new Cell();
          cell.CellId = i;
          cell.CellState = boardViewModel.CellViewModels[i].CellState;
          nonogram.Cells.Add(cell);
        }  
        //nonogram.CellViewModels = boardViewModel.CellViewModels;

        _db.Nonograms.Add(nonogram);
        _db.SaveChanges();
        
        return RedirectToAction("Index");
      // }
    }

    public ActionResult Details(int id)
    {
      Nonogram thisNonogram = _db.Nonograms
        .Include(nonogram => nonogram.JoinEntities)
        //.ThenInclude(join => join.Player)
        .FirstOrDefault(nonogram => nonogram.NonogramId == id);
      return View(thisNonogram);
    }

    public ActionResult Edit(int id)
    {
      Nonogram thisNonogram = _db.Nonograms.FirstOrDefault(nonogram => nonogram.NonogramId == id);
      return View(thisNonogram);
    }

    [HttpPost]
    public ActionResult Edit (Nonogram nonogram)
    {
      _db.Nonograms.Update(nonogram);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Nonogram thisNonogram = _db.Nonograms.FirstOrDefault(nonogram => nonogram.NonogramId == id);
      return View(thisNonogram);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Nonogram thisNonogram = _db.Nonograms.FirstOrDefault(nonogram => nonogram.NonogramId == id);
      _db.Nonograms.Remove(thisNonogram);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}