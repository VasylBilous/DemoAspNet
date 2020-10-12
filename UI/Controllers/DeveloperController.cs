using AutoMapper;
using BLL.Services.Abstraction;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.Models;

namespace UI.Controllers
{
    public class DeveloperController : Controller
    {
        private readonly IDeveloperService devService;
        private readonly IMapper mapper;

        public DeveloperController(IDeveloperService _serv, IMapper _mapper)
        {
            devService = _serv;
            mapper = _mapper;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var devs = devService.GetAllDevelopers();
            var model = mapper.Map<List<DeveloperViewModel>>(devs);
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(string newDev)
        {
            var model = new DeveloperViewModel { Name = newDev };
            var dev = mapper.Map<Developer>(model);
            devService.AddDeveloper(dev);
            return Redirect("Game/Create");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var dev = devService.Find(id);
            devService.DeleteDeveloper(dev);
            return RedirectToAction("Index");
        }
    }
}