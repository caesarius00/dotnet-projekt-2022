using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projekt.DBAccess;
using Projekt.Models;

namespace Projekt.Controllers;

[Authorize(Roles = "Admin, Connoisseur")]
public class BeerTypeController : Controller{
    
    private readonly BeerAppContext _context;
    
    public BeerTypeController(BeerAppContext context){
        _context = context;
    }
    
    public IActionResult Index(){
        return View(_context.BeerTypes.ToList());
    }
    
    public IActionResult Create(){            
        ViewBag.Action="Create";
        return View();
    }
    
    [HttpPost]
    public IActionResult Create(BeerType type){
        if(ModelState.IsValid){
            _context.BeerTypes.Add(type);
            _context.SaveChanges();            
            return RedirectToAction("Index");
        }
        ViewBag.Action="Create";
        return View(type);
    }
    
    public IActionResult Edit(int id){
        var type = _context.BeerTypes.Find(id);
        if(type == null){
            return RedirectToAction("Index");
        }        
        ViewBag.Action="Edit";
        return View("create",type);
    }
    
    [HttpPost]
    public IActionResult Edit(BeerType type){
        if(ModelState.IsValid){
            _context.BeerTypes.Update(type);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        ViewBag.Action="Edit";
        return View("create",type);
    }
    
    public IActionResult Delete(int id){
        var type = _context.BeerTypes.Find(id);
        if(type == null){
            return RedirectToAction("Index");
        }
        _context.BeerTypes.Remove(type);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
    
    public IActionResult TypesPartialView(int id){
        // var beers = _context.Beers.Where(b => b.BeerTypeId == id).ToList();
        var beers = (from b in _context.Beers 
                where b.BeerTypeId == id
                orderby b.Name.ToLower()
                select b
                ).ToList();
        return PartialView(beers);
    }

}