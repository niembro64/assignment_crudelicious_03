﻿using System;
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
