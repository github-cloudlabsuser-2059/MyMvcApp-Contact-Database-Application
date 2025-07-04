using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Models;

namespace MyMvcApp.Controllers;

public class UserController : Controller
{
    public static System.Collections.Generic.List<User> userlist = new System.Collections.Generic.List<User>();

    // Constructor to add two generic users if the list is empty
    public UserController()
    {
        if (userlist.Count == 0)
        {
            userlist.Add(new User { Id = 1, Name = "Alice", Email = "alice@example.com" });
            userlist.Add(new User { Id = 2, Name = "Bob", Email = "bob@example.com" });
        }
    }

    // GET: User
    public ActionResult Index(string search)
    {
        var users = userlist.AsEnumerable();
        if (!string.IsNullOrWhiteSpace(search))
        {
            users = users.Where(u =>
                (!string.IsNullOrEmpty(u.Name) && u.Name.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                (!string.IsNullOrEmpty(u.Email) && u.Email.Contains(search, StringComparison.OrdinalIgnoreCase))
            );
        }
        ViewBag.Search = search;
        return View(users.ToList());
    }

    // GET: User/Details/5
    public ActionResult Details(int id)
    {
        var user = userlist.FirstOrDefault(u => u.Id == id);
        if (user == null)
            return NotFound();
        return View(user);
    }

    // GET: User/Create
    public ActionResult Create()
    {
        return View();
    }

    // POST: User/Create
    [HttpPost]
    public ActionResult Create(User user)
    {
        if (ModelState.IsValid)
        {
            // Assign a new ID (simple auto-increment)
            user.Id = userlist.Count > 0 ? userlist.Max(u => u.Id) + 1 : 1;
            userlist.Add(user);
            return RedirectToAction(nameof(Index));
        }
        return View(user);
    }

    // GET: User/Edit/5
    public ActionResult Edit(int id)
    {
        var user = userlist.FirstOrDefault(u => u.Id == id);
        if (user == null)
            return NotFound();
        return View(user);
    }

    // POST: User/Edit/5
    [HttpPost]
    public ActionResult Edit(int id, User user)
    {
        var existingUser = userlist.FirstOrDefault(u => u.Id == id);
        if (existingUser == null)
            return NotFound();

        if (ModelState.IsValid)
        {
            // Update user properties
            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            // Add other properties as needed
            return RedirectToAction(nameof(Index));
        }
        return View(user);
    }

    // GET: User/Delete/5
    public ActionResult Delete(int id)
    {
        var user = userlist.FirstOrDefault(u => u.Id == id);
        if (user == null)
            return NotFound();
        return View(user);
    }

    // POST: User/Delete/5
    [HttpPost]
    public ActionResult Delete(int id, IFormCollection collection)
    {
        var user = userlist.FirstOrDefault(u => u.Id == id);
        if (user == null)
            return NotFound();

        userlist.Remove(user);
        return RedirectToAction(nameof(Index));
    }
}
