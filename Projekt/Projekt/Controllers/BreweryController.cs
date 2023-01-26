using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projekt.DBAccess;
using Projekt.Models;

namespace Projekt.Controllers;

[Authorize(Roles = "Admin, Connoisseur")]
public class BreweryController : Controller{
    private readonly BeerAppContext _context;
    
    public BreweryController(BeerAppContext context){
        _context = context;
    }
    
    public IActionResult Index(){
        return View(_context.Breweries.ToList());
    }
    
    public IActionResult Create(){            
        ViewBag.Action="Create";
        return View();
    }
    
    [HttpPost]
    public IActionResult Create(Brewery brewery){
        if(ModelState.IsValid){
            _context.Breweries.Add(brewery);
            _context.SaveChanges();            
            return RedirectToAction("Index");
        }
        ViewBag.Action="Create";
        return View(brewery);
    }
    
    public IActionResult Edit(int id){
        var brewery = _context.Breweries.Find(id);
        if(brewery == null){
            return NotFound();
        }        
        ViewBag.Action="Edit";
        return View("create",brewery);
    }
    
    [HttpPost]
    public IActionResult Edit(Brewery brewery){
        if(ModelState.IsValid){
            _context.Breweries.Update(brewery);
            _context.SaveChanges();
            // ViewBag.Action="Edit";
            return RedirectToAction("Index");
        }
        ViewBag.Action="Edit";
        return View("create",brewery);
    }
    
    public IActionResult Delete(int id){
        var brewery = _context.Breweries.Find(id);
        if(brewery == null){
            return RedirectToAction("Index");
        }
        _context.Breweries.Remove(brewery);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

}