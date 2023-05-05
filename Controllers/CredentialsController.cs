using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MKsEMS.Data;
using MKsEMS.Models;

namespace MKsEMS.Controllers
{
    public class CredentialsController : Controller
    {
        private readonly EMSDbContext _context;
        private CurrentUser2 _currentUser;

        public CredentialsController(EMSDbContext context, CurrentUser2 currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        // GET: Credentials
        /// <summary>
        /// Only Adminstrators have access to the list of all Credentials
        /// Any other user should should only see his/her own credentials
        /// </summary>
        /// <returns>List of Credentials</returns>
        public async Task<IActionResult> Index()
        {
            
            if (!_currentUser.IsLoggedIn())
                return RedirectToAction("Index", "UserLogins"); //Only if user is not already logged in;
                      
            if(_context.Credentials != null)
            { 
                return _currentUser.GetLoggedInUser().IsAdmin ? 
                        View(await _context.Credentials.ToListAsync()) :
                        View(await _context.Credentials.Where(c => c.UserEmail == _currentUser.GetLoggedInUser().Email).ToListAsync());
            }

            return Problem("Entity set 'EMSDbContext.Credentials'  is null.");
        }


        // GET: Credentials/Edit/5
       
        private bool CredentialsExists(int id)
        {
          return (_context.Credentials?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
