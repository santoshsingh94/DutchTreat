﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DutchTreat.Services;
using DutchTreat.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace DutchTreat.Controllers
{
    public class AppController : Controller
    {
        public readonly IMailService _mailService;

        public AppController(IMailService mailservice)
        {
            _mailService = mailservice;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("contact")]
        public IActionResult Contact()
        {           
            return View();
        }

        [HttpPost("contact")]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Send the Email
                _mailService.SendMessage("ausantoshsingh@gmail.com", model.Subject, $"From:{model.Name} - {model.Email},Message: {model.Message}");
                ViewBag.UserMessage = "Mail Sent";
                ModelState.Clear();
            }
            else
            {
                //Show the errors   
            }
            return View();

        }

        public IActionResult About()
        {
            return View();
        }
    }
}
