﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Projekt.DBAccess;
using Projekt.Models;

namespace Projekt.Controllers;

public class HomeController : Controller{
    // private readonly ILogger<HomeController> _logger;
    //
    // public HomeController(ILogger<HomeController> logger){
    //     _logger = logger;
    // }

    private readonly BeerAppContext _context;

    public HomeController(BeerAppContext context){
        _context = context;
    }

    public IActionResult Index(){
        return View();
    }
    
    public IActionResult Error(){
        return View("Error");
    }


    // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    // public IActionResult Error(){
    //     return View(new ErrorViewModel{ RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    // }
}