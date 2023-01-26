using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projekt.DBAccess;
using Projekt.Models;

namespace Projekt.Controllers;

[Authorize(Roles = "Admin, Connoisseur")]
public class BeerController : Controller{ 
    private readonly BeerAppContext _context;
    
    public BeerController(BeerAppContext context){
        _context = context;
    }
    
    // public IActionResult Index(){
    //     return View();
    // }
    
    [Authorize]
    [HttpGet]
    public IActionResult Index(){
        var beers = (from b in _context.Beers
            join br in _context.Breweries on b.BreweryId equals br.Id
            join bt in _context.BeerTypes on b.BeerTypeId equals bt.Id
            orderby b.Name.ToLower()
            select new Beer{
                Id = b.Id,
                Name = b.Name,
                Alcohol = b.Alcohol,
                Brewery = br,
                BeerType = bt
            }).ToList();

        return View(beers);
    }
    
    [HttpGet]
    public IActionResult Create(){
        ViewBag.Breweries = new SelectList(_context.Breweries, "Id", "Name");
        ViewBag.BeerTypes = new SelectList(_context.BeerTypes, "Id", "Name");
        ViewBag.Action="Create";
        return View();
    }

    [HttpPost]
    public IActionResult Create(Beer beer){
        if (ModelState.IsValid){
            _context.Beers.Add(beer);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        else{
            ViewBag.Breweries = new SelectList(_context.Breweries, "Id", "Name", beer.Brewery);
            ViewBag.BeerTypes = new SelectList(_context.BeerTypes, "Id", "Name", beer.BeerType);
            ViewBag.Action="Create";
            return View(beer);
        }
    }

    [HttpPost]
    public IActionResult Delete(int Id){
        var beer = _context.Beers.Find(Id);
        if(beer != null){
            _context.Beers.Remove(beer);
            _context.SaveChanges();
        }
        return RedirectToAction("Index");
    }
    
    [HttpGet]
    public IActionResult Edit(int Id){
        var beer = _context.Beers.Find(Id);
        if(beer != null){
            ViewBag.Breweries = new SelectList(_context.Breweries, "Id", "Name");
            ViewBag.BeerTypes = new SelectList(_context.BeerTypes, "Id", "Name");
            ViewBag.Action = "Edit";
            return View("create",beer);
        }
        return RedirectToAction("Index");
    }
    
    [HttpPost]
    public IActionResult Edit(Beer beer){
        if (ModelState.IsValid){
            _context.Beers.Update(beer);
            _context.SaveChanges();
            // ViewBag.Action="Edit";
            return RedirectToAction("Index");
        }
        else{
            ViewBag.Breweries = new SelectList(_context.Breweries, "Id", "Name", beer.Brewery);
            ViewBag.BeerTypes = new SelectList(_context.BeerTypes, "Id", "Name", beer.BeerType);
            ViewBag.Action="Edit";
            return View("create",beer);
        }
    }
    
    //remote validation for beer alcohol and BeerTypes min and max alvohol
    [AcceptVerbs("Get", "Post")]
    public IActionResult VerifyAlcohol(int Alcohol, int BeerTypeId){
        var beerType = _context.BeerTypes.Find(BeerTypeId);
        if(Alcohol < beerType.MinAlcohol || Alcohol > beerType.MaxAlcohol){
            return Json($"{beerType.Name} must have alcohol between {beerType.MinAlcohol} and {beerType.MaxAlcohol}.");
        }
        return Json(true);
    }


}