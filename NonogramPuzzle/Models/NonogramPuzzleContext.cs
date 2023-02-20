using Microsoft.EntityFrameworkCore;
using NonogramPuzzle.ViewModels;

namespace NonogramPuzzle.Models
{
  public class NonogramPuzzleContext : DbContext
  {
    public DbSet<Nonogram> Nonograms { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    modelBuilder.Entity<CellViewModel>().HasNoKey().ToView("NonogramPuzzle");
    
    }

    public DbSet<CellViewModel> CellViewModels { get; set; }
    public NonogramPuzzleContext(DbContextOptions options) : base(options) { }
  }
}