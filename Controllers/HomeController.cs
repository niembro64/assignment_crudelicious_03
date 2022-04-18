using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using assignment_crudelicious_03.Models;
using Microsoft.EntityFrameworkCore;

namespace assignment_crudelicious_03.Controllers
{
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;

    private MyContext _context;

    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
      _logger = logger;
      _context = context;
    }

    public IActionResult Index()
    {
      ViewBag.AllDishes = _context.Dishes.ToList();
      return View();
    }

    [HttpGet("dishes/new")]
    public IActionResult NewDish()
    {
      Console.WriteLine("ADDING PAGE");
      return View("NewDish");
    }
    [HttpPost("dishes/add")]
    public IActionResult AddDish(Dish newDish)
    {
      Console.WriteLine("ADDING FUNCTION");
      if (ModelState.IsValid)
      {
        _context.Add(newDish);
        _context.SaveChanges();

        return RedirectToAction("Index");
      }
      else
      {
        return View("NewDish");
      }
    }

    [HttpGet("dishes/view/{dishId}")]
    public IActionResult ShowDish(int dishId)
    {
      Console.WriteLine("SHOWING DISH");
      ViewBag.OneDish = _context.Dishes.FirstOrDefault(a => a.DishId == dishId);
      return View("ShowDish");
    }

    [HttpGet("dishes/remove/{dishId}")]
    public IActionResult RemoveDish(int dishId)
    {
      Console.WriteLine("DELETING DISHHHHH");
      Dish DishToRemove = _context.Dishes.SingleOrDefault(s => s.DishId == dishId);
      _context.Dishes.Remove(DishToRemove);
      _context.SaveChanges();
      return RedirectToAction("Index");
    }




    [HttpGet("dishes/edit/{dishId}")]
    public IActionResult EditDish(int dishId)
    {
      Console.WriteLine("EDITTTTTTTTTTTTTTTTTTTTTTTTTTTTTTT");
      Dish DishToUpdate = _context.Dishes.FirstOrDefault(a => a.DishId == dishId);
      ViewBag.OneDish = DishToUpdate;
      return View(DishToUpdate);
    }

    [HttpPost("dishes/update/{dishId}")]
    public IActionResult UpdateDish(int dishId, Dish updatedDish)
    {
      Console.WriteLine("UPDATEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE");
      if (ModelState.IsValid)
      {
        Dish OldDish = _context.Dishes.FirstOrDefault(a => a.DishId == dishId);
        OldDish.Name = updatedDish.Name;
        OldDish.Chef = updatedDish.Chef;
        OldDish.Tastiness = updatedDish.Tastiness;
        OldDish.Calories = updatedDish.Calories;
        OldDish.Description = updatedDish.Description;
        OldDish.UpdatedAt = DateTime.Now;
        _context.SaveChanges();
        return RedirectToAction("Index");
      }
      else
      {
        return View("EditDish", updatedDish);
      }
    }



    public IActionResult Privacy()
    {
      return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}
