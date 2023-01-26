using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Projekt.DBAccess;
using Projekt.Models;

namespace Projekt.Controllers;

[Authorize(Roles = "Admin")]
public class BeerRoleController : Controller{
    private readonly BeerAppContext _context;

    public BeerRoleController(BeerAppContext context){
        _context = context;
    }

    public IActionResult Index(){
        var beerRoles = _context.BeerRoles.ToList();
        return View(beerRoles);
    }

    public IActionResult Create(){
        ViewBag.BeerRoles = new SelectList(_context.BeerRoles, "Id", "Name");
        ViewBag.Action="Create";
        return View();
    }
    
    [HttpPost]
    public IActionResult Create(BeerRole role)
    {
        if(ModelState.IsValid)
        {
            _context.BeerRoles.Add(role);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        ViewBag.BeerRoles = new SelectList(_context.BeerRoles, "Id", "Name");
        ViewBag.Action="Create";
        return View(role);
    }
    
    public IActionResult Edit(string id){
        var role = _context.BeerRoles.Find(id);
        if(role == null){
            return RedirectToAction("Index");
        }
        ViewBag.BeerRoles = new SelectList(_context.BeerRoles, "Id", "Name");
        ViewBag.Action="Edit";
        return View("Create",role);
    }
    
    [HttpPost]
    public IActionResult Edit(BeerRole role){
        if(ModelState.IsValid){
            _context.BeerRoles.Update(role);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        ViewBag.BeerRoles = new SelectList(_context.BeerRoles, "Id", "Name");
        ViewBag.Action="Edit";
        return View("Create",role);
    }
    
    public IActionResult Delete(string id){
        var role = _context.BeerRoles.Find(id);
        if(role == null){
            return RedirectToAction("Index");
        }
        _context.BeerRoles.Remove(role);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
    
    [HttpPost]
    public IActionResult Delete(BeerRole role){
        _context.BeerRoles.Remove(role);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
    
}