using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Models;

namespace MyMvcApp.Controllers;

public class UserController : Controller
{
    public static System.Collections.Generic.List<User> userlist = new System.Collections.Generic.List<User>();

    // GET: User
    public ActionResult Index()
    {
        return View(userlist);
    }

    // GET: User/Details/5
    public ActionResult Details(int id)
    {
        return View(userlist[id]);
        // Implement the details method here
    }

    // GET: User/Create
    public ActionResult Create()
    {
        return View();
        //Implement the Create method here
    }

    // POST: User/Create
    [HttpPost]
    public ActionResult Create(User user)
    {
        // This method is responsible for handling the HTTP POST request to create a new user.
        // It receives user input from the form submission and adds it to the userlist.
        // If the model state is valid, it redirects to the Index action to display the updated list of users.
        // If there are validation errors, it returns the Create view to display those errors.

        if (ModelState.IsValid)
        {
            userlist.Add(user);
            return RedirectToAction("Index");
        }

        return View(user);
    }

    // GET: User/Edit/5
    public ActionResult Edit(int id)
    {
        return View(userlist[id]);
        // This method is responsible for displaying the view to edit an existing user with the specified ID.
        // It retrieves the user from the userlist based on the provided ID and passes it to the Edit view.
    }

    // POST: User/Edit/5
    [HttpPost]
    public ActionResult Edit(int id, User user)
    {
        if (ModelState.IsValid)
        {
            var existingUser = userlist.ElementAtOrDefault(id);
            if (existingUser != null)
            {
                existingUser.Name = user.Name;
                existingUser.Email = user.Email;
                // Add other properties as needed
                return RedirectToAction("Index");
            }
            return NotFound();
        }
        return View(user);
    }

    // GET: User/Delete/5
    public ActionResult Delete(int id)
    {
        return View(userlist[id]);
        // Implement the Delete method here
    }

    // POST: User/Delete/5
    [HttpPost]
    public ActionResult Delete(int id, IFormCollection collection)
    {
        if (id >= 0 && id < userlist.Count)
        {
            userlist.RemoveAt(id);
            return RedirectToAction("Index");
        }
        return NotFound();
    }
}
