﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MKsEMS.Data;
using MKsEMS.Models;
using MKsEMS.Services;

namespace MKsEMS.Controllers
{
    public class UsersController : Controller
    {
        private readonly EMSDbContext _context;
        private readonly EMSDbContext _contextCredentails;
        private readonly Credentials _credentials = new();
        public UsersController(EMSDbContext context)
        {
            _context = context;
           // _contextCredentails = _cont;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            if (!CurrentUser.IsLoggedIn())
                return RedirectToAction("Index", "UserLogins"); //Only if user is not already logged in;

            return _context.Users != null ? 
                          View(await _context.Users.ToListAsync()) :
                          Problem("Entity set 'EMSDbContext.Users'  is null.");
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!CurrentUser.IsLoggedIn())
                return RedirectToAction("Index", "UserLogins"); //Only if user is not already logged in;

            
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            if (!CurrentUser.IsLoggedIn())
                return RedirectToAction("Index", "UserLogins"); //Only if user is not already logged in;

            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,SurName,Email,Title,ManagerEmail,Department,DOB,LeaveEntitement,LeaveTaken,SickLeaveTaken,Salary")] User user)
        {
            ViewData["MustAddContactToUser"] = null;
            ViewData["TheNewUser"] = null;

            if (ModelState.IsValid)
            {
                //creating email address for the user
                //NEED CHECKING IF USER WITH SAME EMAIL EXISTS
                //THEN INCREMENT BY 1
                if (_context.Companies.First().domainName != null)
                    user.Email = string.Concat(user.FirstName, ".", user.SurName, "@", _context.Companies.First().domainName);
                //Adding/creating email and temporary password into Credentials table for the user                 
                _credentials.UserEmail = user.Email;
                string pass = GenerateRandomPass.GeTempPassword();

                     
                _credentials.EncPass = EncDecPassword.Enc64bitsPass(GenerateRandomPass.GeTempPassword());
                
                await _context.AddAsync(_credentials);
                _context.SaveChangesAsync();

                //now creating a record in Users table for the user
                _context.Add(user);                
                await _context.SaveChangesAsync();
                
                ViewData["MustAddContactToUser"] = "Please ensure you fill in the contact details for the new user";
                ViewData["UserEmail"] = user.Email;
                
                return RedirectToAction("Create", "Contacts");
            }
            return View(user);
        }

        

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!CurrentUser.IsLoggedIn())
                return RedirectToAction("Index", "UserLogins"); //Only if user is not already logged in;

            
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,SurName,Email,Title,ManagerEmail,Department,DOB,LeaveEntitement,LeaveTaken,SickLeaveTaken,Salary")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!CurrentUser.IsLoggedIn())
                return RedirectToAction("Index", "UserLogins"); //Only if user is not already logged in;

            
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'EMSDbContext.Users'  is null.");
            }
            var user = await _context.Users.FindAsync(id);
            var userCredentials =  _context.Credentials.Where(uc => uc.UserEmail == user.Email).FirstOrDefault();
            if (user != null)
            {
                _context.Users.Remove(user);
                
                if(userCredentials !=null)
                    _context.Credentials.Remove(userCredentials);
            }   
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
          return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
