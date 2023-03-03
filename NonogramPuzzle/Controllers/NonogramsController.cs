using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


using NonogramPuzzle.Models;
using NonogramPuzzle.ViewModels;

namespace NonogramPuzzle.Controllers
{
  public class NonogramsController : Controller
  {
    public static List<Cell> cellList = new List<Cell>();
    public static Dictionary<int, List<int>> rowClues = new Dictionary<int, List<int>>();
    public static Dictionary<int, List<int>> colClues = new Dictionary<int, List<int>>();
    private readonly NonogramPuzzleContext _db;
    
    public NonogramsController(NonogramPuzzleContext db)
    {

      _db = db;
    }

    public ActionResult Index()
    {
      List<Nonogram> model = _db.Nonograms.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Nonogram nonogram)
    {
      if (!ModelState.IsValid)
      {
        return View(nonogram);
      }
      else
      {
        nonogram.NonogramDim = nonogram.NonogramWidth * nonogram.NonogramHeight;
        _db.Nonograms.Add(nonogram);
        _db.SaveChanges();
      }

      return RedirectToAction("Build","CellViewModels");
    }

    public ActionResult Details(int id)
    {
      Nonogram thisNonogram = _db.Nonograms.Include(nono => nono.Cells)
      .FirstOrDefault(nonogram => nonogram.NonogramId == id);

      
      Nonogram nonogramSolution= new Nonogram();
      nonogramSolution.Cells = thisNonogram.Cells;
      //calculating dimension for empty board with clues included

      int width = thisNonogram.NonogramWidth;
      int height = thisNonogram.NonogramHeight;
      int boardSize = thisNonogram.NonogramDim;//width * height;//thisNonogram.NonogramDim;
      int maxColClues = 0;
      int maxRowClues = 0;

      //calculation board height, accounting for max. number of clues in the columns
      // i = rows/Height, j = columns/width, saving clue and board location

      if (thisNonogram.Cells.Count != 0)
      {
        for(int j = 0; j < width ; j++)
        {
          int maxColCluesCount = 0;

          for(int i = j ; i <= (boardSize - (width - j)); i = (i + width ))
          {          
            
            int nextColCell = (i + width);
            int priorColCell = ( i - width);
  
            if( i == j && height == 1 && thisNonogram.Cells.ElementAt(i).CellState == 1)
            {
              maxColCluesCount++;
            }
            else if (i != (boardSize - (width - j)) && thisNonogram.Cells.ElementAt(i).CellState == 1 && thisNonogram.Cells.ElementAt(nextColCell).CellState == 0 )
            {
              maxColCluesCount++;
            }
            else if (i == (boardSize - (width - j)) &&  thisNonogram.Cells.ElementAt(i).CellState == 1 && thisNonogram.Cells.ElementAt(priorColCell).CellState != 1)
            {
              maxColCluesCount++;
            }
            else if ( (i == (boardSize - (width - j))) && (thisNonogram.Cells.ElementAt(i).CellState == 1) && (thisNonogram.Cells.ElementAt(priorColCell).CellState == 1))
            {
              maxColCluesCount++;// when prior cell has cell state = 1 and last cell has state = 1
            }
          }
          
          if (maxColClues < maxColCluesCount)
          {
            maxColClues = maxColCluesCount;
          }
        }
      }
      
      //calculation board width, account for max. clues in the rows

      int maxRowClueCount = 0;
      int endOfRow = 0;

      for (int i = 0; i < boardSize; i++)
      {
        int nextRowCell = (i + 1);
        int priorRowCell = ( i - 1);

        if( (i % width) == 0 )
        {  
          maxRowClueCount = 0;
          endOfRow = i + (width - 1);
        }
        
        if( (i % width == 0) && (width == 1) && (thisNonogram.Cells.ElementAt(i).CellState == 1))
        { 
          maxRowClueCount++;
        }
        else if( (i < endOfRow) && (thisNonogram.Cells.ElementAt(i).CellState == 1) && (thisNonogram.Cells.ElementAt(nextRowCell).CellState == 0))
        {
          maxRowClueCount++;
        }
        else if ((i == endOfRow) && (thisNonogram.Cells.ElementAt(i).CellState == 1) && (thisNonogram.Cells.ElementAt(priorRowCell).CellState != 1))
        {
          maxRowClueCount++;
        }
        else if ((i == endOfRow) && (thisNonogram.Cells.ElementAt(i).CellState == 1) && (thisNonogram.Cells.ElementAt(priorRowCell).CellState == 1))
        {
          maxRowClueCount++;
        }

        if(maxRowClues < maxRowClueCount)
          {
            maxRowClues = maxRowClueCount;
          }
      }
      
      //locating and recording number of clues in column index, j = 0,1,...,n 
      //row index, i is going to each element in each j columns. 
      // colClues.Clear();

      // List<int> colCluesList = new List<int>();
     
      // for(int j = 0; j < width ; j++)
      // {
      //   int colClueCount = 0;
      
      //   for(int i = j ; i <= (boardSize - (width - j)); i = (i + width ))
      //   {
      //     int nextColCell = i + width;
          
      //     if (thisNonogram.Cells.ElementAt(0).CellState == 1)
      //     {
      //       colClueCount++;
      //     }
          
      //     if(i < (boardSize - (width - j)) && (colClueCount != 0))
      //     {
      //       if(thisNonogram.Cells.ElementAt(nextColCell).CellState == 0 )
      //       {
      //         colCluesList.Add(colClueCount);
      //         colClueCount = 0; 
      //       }
      //     } 
          
      //     if( i == (boardSize - (width - j)) && thisNonogram.Cells.ElementAt(i).CellState == 1)
      //     {
      //       colClueCount++;
      //       colCluesList.Add(colClueCount);
      //     }
      //     else
      //     {
      //       colCluesList.Add(colClueCount);
      //     } 

      //   }
      //   colClues.Add(j,colCluesList);
      // }

      //If user submit a blank board

      // if( maxRowClues == 0)
      // {
      //   maxRowClues = 1;
      // }

      // if(maxColClues == 0)
      // {
      //   maxColClues = 1;
      // }
  
      thisNonogram.solvingBoardWidth = maxRowClues + width;
      thisNonogram.solvingBoardHeight = maxColClues + height;
      thisNonogram.solvingBoardDim = (thisNonogram.solvingBoardWidth * thisNonogram.solvingBoardHeight);

      //Saving SolvingBoard Width, Height, and Dim to data base
      _db.Nonograms.Update(thisNonogram);
      _db.SaveChanges();
      
      //clearing the static List object cellList, to preventing additional cells from being add when
      //cell states are being updated and puzzle board refreshes.
      cellList.Clear();

      if (cellList.Count < thisNonogram.solvingBoardDim)
      {
        for( int i = 0; i < thisNonogram.solvingBoardDim; i++)
        {
          cellList.Add(new Cell { CellId = i, CellState = 0, NonogramId = id});
        }

        thisNonogram.Cells = cellList;
      }

      
      //marking non-game area on top left corner

      // for( int j = 0 ; j < maxRowClues ; j++)
      // {
      //   for( int i = j; i < thisNonogram.solvingBoardHeight - maxColClues; i = i + thisNonogram.solvingBoardWidth)
      //   {
      //     thisNonogram.Cells.ElementAt(i).CellState = 2;
      //     thisNonogram.Cells.ElementAt(i).Clue = "X";
      //     thisNonogram.Cells.ElementAt(0).Clue = maxColClues.ToString();
      //     thisNonogram.Cells.ElementAt(1).Clue = maxRowClues.ToString();
      //   } 
        // this section is for testing purposes
        //thisNonogram.Cells.ElementAt(0).Clue = colClues[3].ElementAt(2).ToString();
        // thisNonogram.Cells.ElementAt(3).Clue = boardSize.ToString();
      //}

      // //saving Column clues

      // for (int j = maxWidth ; j < thisNonogram.solvingBoardWidth; j++)
      // {
      //   int dictionaryKey = j - maxWidth;
      //   for (int i = j; i <= thisNonogram.solvingBoardWidth ; i = i + thisNonogram.solvingBoardWidth)
      //   {
      //     string clue="-1";
      //     clue = colClues[dictionaryKey].ElementAt(dictionaryKey).ToString();
      //     thisNonogram.Cells.ElementAt(i).Clue = clue;
      //   }
      // }

      //This will not be saved in the database
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

    public IActionResult HandleCellClickSolve(string id, string cellNumber)
    {
      int cllNmbr = int.Parse(cellNumber);
      int nonoGramId = int.Parse(id);

      Nonogram thisNonogram = _db.Nonograms.FirstOrDefault(nonogram => nonogram.NonogramId == nonoGramId);
      
      Nonogram model = new Nonogram();
      
      cellList.ElementAt(cllNmbr).CellState = (cellList.ElementAt(cllNmbr).CellState +1) % 2;

      model.NonogramId = thisNonogram.NonogramId;
      model.solvingBoardHeight = thisNonogram.solvingBoardHeight;
      model.solvingBoardWidth = thisNonogram.solvingBoardWidth;
      model.solvingBoardDim = thisNonogram.solvingBoardDim;
      model.NonogramWidth = thisNonogram.NonogramWidth;
      model.NonogramHeight = thisNonogram.NonogramHeight;
      model.NonogramDim = thisNonogram.NonogramDim;
      model.Cells = cellList;

      return View("Details", model);
    }
  }
}