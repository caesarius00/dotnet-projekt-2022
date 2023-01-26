using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projekt.DBAccess;
using Projekt.Models;

namespace Projekt.Controllers;

[Authorize]
public class BeerReviewController : Controller{
    
    private readonly BeerAppContext _context;
    
    public BeerReviewController(BeerAppContext context){
        _context = context;
    }
    
    public IActionResult Index(){
        var beerReviews = (from br in _context.BeerReviews
            join bu in _context.BeerUsers on br.BeerUserId equals bu.Id
            join b in _context.Beers on br.BeerId equals b.Id
            orderby br.Date descending
            select new BeerReview{
                Id = br.Id,
                Beer = b,
                BeerUser = bu,
                Rating = br.Rating,
                Review = br.Review,
                Date = br.Date
            }).ToList();
        return View(beerReviews);
    }
    
    public IActionResult Create(){
        ViewBag.Beers = new SelectList(_context.Beers, "Id", "Name");
        ViewBag.Action="Create";
        if(HttpContext.Session.GetInt32("AddedInSession") != null){
            ViewBag.AddedInSession = HttpContext.Session.GetInt32("AddedInSession");
        }
        else{
            ViewBag.AddedInSession = 0;
        }
        return View();
    }
    
    [HttpPost]
    public IActionResult Create([Bind("BeerId", "Rating", "Review")] BeerReview beerReview){
        
        if (!User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Login", "BeerUser");
        }
        var nickname = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
        var userFromDb = _context.BeerUsers.FirstOrDefault(u => u.NickName == nickname);
        beerReview.BeerUserId = userFromDb.Id;
        beerReview.Date = DateTime.Now;
        if(ModelState.IsValid){
            _context.BeerReviews.Add(beerReview);
            _context.SaveChanges();
            if (HttpContext.Session.GetInt32("AddedInSession") == null){
                HttpContext.Session.SetInt32("AddedInSession", 1);
            }
            else{
                HttpContext.Session.SetInt32("AddedInSession", (int) HttpContext.Session.GetInt32("AddedInSession") + 1);
                if (HttpContext.Session.GetInt32("AddedInSession") > 4){
                    //change user role to Connoisseur if not already Connoisseur or Admin
                    var roleName = _context.BeerRoles.FirstOrDefault(r => r.Id == userFromDb.BeerRoleId).Name;
                    if (roleName != "Admin" && roleName != "Connoisseur"){
                        var roleId = _context.BeerRoles.FirstOrDefault(r => r.Name == "Connoisseur").Id;
                        userFromDb.BeerRoleId = roleId;
                        _context.BeerUsers.Update(userFromDb);
                        _context.SaveChanges();
                        
                        //"relog" user to update role
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, userFromDb.NickName),
                            new Claim(ClaimTypes.Role, "Connoisseur"),
                            new Claim(ClaimTypes.NameIdentifier, userFromDb.Id.ToString())
                        };
                        var claimsIdentity = new ClaimsIdentity(claims, "User Identity");
                        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                        HttpContext.SignInAsync(claimsPrincipal);
                    }
                }
            }

            return RedirectToAction("Index");
        }
        ViewBag.Beers = new SelectList(_context.Beers, "Id", "Name");
        ViewBag.Action="Create";
        return View(beerReview);
    }
    
    
}