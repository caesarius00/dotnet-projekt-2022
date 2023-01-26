using System.Drawing.Printing;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Projekt.DBAccess;
using Projekt.Models;

namespace Projekt.Controllers;

public class BeerUserController : Controller{
    
    private readonly BeerAppContext _context;
    
    public BeerUserController(BeerAppContext context){
        _context = context;
    }
    
    [Authorize(Roles = "Admin")]
    public IActionResult Edit(string id){
        var user = _context.BeerUsers.Find(id);
        if(user == null){
            return RedirectToAction("Index");
        }
        ViewBag.BeerRoles = new SelectList(_context.BeerRoles, "Id", "Name");
        ViewBag.Action="Edit";
        return View("Create",user);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public IActionResult Edit(BeerUser user){
        if(ModelState.IsValid){
            //leave password as is, update other fields
            var userInDb = _context.BeerUsers.Find(user.Id);
            userInDb.NickName = user.NickName;
            userInDb.Email = user.Email;
            userInDb.BeerRoleId = user.BeerRoleId;
            if(user.Password != null){
                var hasher = new PasswordHasher<BeerUser>();
                userInDb.Password = hasher.HashPassword(userInDb, user.Password);
            }
            _context.BeerUsers.Update(userInDb);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        ViewBag.BeerRoles = new SelectList(_context.BeerRoles, "Id", "Name");
        ViewBag.Action="Edit";
        return View("Create",user);
    }
    
    [Authorize(Roles = "Admin")]
    public IActionResult Index(){
        // var beerUsers = _context.BeerUsers.ToList();
        //beer users with roles
        var beerUsers = _context.BeerUsers
            .Select(u => new BeerUser{
                Id = u.Id,
                NickName = u.NickName,
                Email = u.Email,
                BeerRoleId = u.BeerRoleId,
                BeerRole = _context.BeerRoles.FirstOrDefault(r => r.Id == u.BeerRoleId)
            }).ToList();
        
        return View(beerUsers);
    }
    public IActionResult Login(){
        return View();
    }
    
    public IActionResult Logout(){
        HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }
    
    public IActionResult Register(){
        ViewBag.BeerRoles = new SelectList(_context.BeerRoles, "Id", "Name");
        ViewBag.Action="Register";
        return View("Create");
    }
      
    
    [HttpPost]
    public IActionResult Register(BeerUser user){
        if(ModelState.IsValid){
            var hasher = new PasswordHasher<BeerUser>();
            user.Password = hasher.HashPassword(user, user.Password);
            _context.BeerUsers.Add(user);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        ViewBag.BeerRoles = new SelectList(_context.BeerRoles, "Id", "Name");
        ViewBag.Action = "Register";
        return View("Create",user);
    }
    
    [HttpPost]
    public IActionResult Login([Bind("NickName","Password")] BeerUser user){
        var hasher = new PasswordHasher<BeerUser>();
        var userFromDb = _context.BeerUsers.FirstOrDefault(u => u.NickName == user.NickName);
        if(userFromDb != null){
            var result = hasher.VerifyHashedPassword(user, userFromDb.Password, user.Password);
            if(result == PasswordVerificationResult.Success){
                //get name of role
                var role = _context.BeerRoles.FirstOrDefault(r => r.Id == userFromDb.BeerRoleId);
                // Sign in the user
                var claims = new List<Claim>{
                    new Claim(ClaimTypes.Name, user.NickName),
                    new Claim(ClaimTypes.Role, role.Name),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties{
                    AllowRefresh = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                    IsPersistent = true,
                    IssuedUtc = DateTimeOffset.UtcNow
                };
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("Password", "Password is incorrect");
            
        }
        else{
            ModelState.AddModelError("NickName", "User not found");
        }
        ModelState.ClearValidationState("BeerRoleId");
        return View();
        }
    
    [HttpPost]
    public IActionResult Delete(string Id){
        var user = _context.BeerUsers.Find(Id);
        if(user != null){
            _context.BeerUsers.Remove(user);
            _context.SaveChanges();
        }
        return RedirectToAction("Index");
    }
    
}