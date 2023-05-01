﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MKsEMS.Data;
using MKsEMS.ViewModels;
using MKsEMS.Models;
using MKsEMS.Services;

namespace MKsEMS.Controllers
{
    public class ResetPassesController : Controller
    {
        private readonly EMSDbContext _context;
        private readonly CurrentUser2 _currentUser;

        public ResetPassesController(EMSDbContext context, CurrentUser2 currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        
        public IActionResult ResetPassword()
        {
            TempData["NoMatchingPass"] = "";
            return View();
        }

        // POST: ResetPasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// ResetPassword() allows a user to reset his/her password or an Adminstrator to reset Password for a User
        /// Upon receipt of a valid state, and all creteria are met, the password gets encrypted and stored into the database
        /// </summary>
        /// <param name="resetPass">ResetPasses object to be processed</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword([Bind("Id,Email,CurrentPassword,NewPassword,ReEnterNewPassword")] ResetPass resetPass)
        {
            TempData["NoMatchingPass"] = "";

            if (ModelState.IsValid)
            {

                //return _context.ResetPasses != null ?
                //          View(await _context.ResetPasses.ToListAsync()) :
                //          Problem("Entity set 'EMSDbContext.ResetPasses'  is null.");

               if( resetPass.NewPassword != resetPass.ReEnterNewPassword)
                {
                    TempData["NoMatchingPass"] = "New passwords do not match";

                    return View(resetPass);
                }
                else
                {
                    var credentials =  await _context.Credentials.Where(c => c.UserEmail == resetPass.Email).FirstOrDefaultAsync();
                    
                    if( credentials == null ||
                        EncDecPassword.DecodeFrom64(credentials.EncPass) != resetPass.CurrentPassword)
                    {
                        TempData["NoMatchingPass"] = "Username or password you entered is incorrect";
                        return View(resetPass);
                    }
                    
                    credentials.EncPass = EncDecPassword.Enc64bitsPass(resetPass.NewPassword);
                    await _context.SaveChangesAsync();
                    
                    TempData["NoMatchingPass"] = "Password reset successfully";

                    return View();                   
                } 
            }
            return View(resetPass);
        }

        
    }
}
