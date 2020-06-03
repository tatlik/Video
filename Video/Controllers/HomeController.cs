using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using Video.Models;

namespace Video.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationContext _context;

        public HomeController(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var qry = _context.Infovideos.AsNoTracking().OrderBy(p => p.Name);
            var model = await PagingList.CreateAsync(qry, 3, page);
            return View(model);
           // return View(await _context.Infovideos.ToListAsync());
        }

        //Просмотр одного выбранного фильма GET: Infovideos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var infovideo = await _context.Infovideos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (infovideo == null)
            {
                return NotFound();
            }

            return View(infovideo);
        }
        public IActionResult Create()
        {
            return View();
        }


        // POST: Infovideos/Create
        // Создание нового фильма
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm]Infovideo infovideo, IFormFile Poster)
        {
          
            if (Poster.Length > 0)

            //Convert Image to byte and save to database
            {

                byte[] p1 = null;
                using (var fs1 = Poster.OpenReadStream())
                using (var ms1 = new MemoryStream())
                {
                    fs1.CopyTo(ms1);
                    p1 = ms1.ToArray();
                }           

            // установка массива байтов
            infovideo.Poster = p1;

 }
        _context.Add(infovideo);
                await _context.SaveChangesAsync();


            return RedirectToAction(nameof(Index), "Home/Index");
        }

        // GET: Infovideos/Edit/5
        public async Task<IActionResult> Edit(int? Id, string User)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var infovideo = await _context.Infovideos.FindAsync(Id);
            if (infovideo.User == User)
            {
                return View(infovideo);
               
            }
            return PartialView("Privacy");
          //  return RedirectToAction(nameof(Index), "Home/Edit");
        }

        // POST: Infovideos/Edit/5
        // Изменение данных
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Opisanie,Date,Producer,User,Poster")] Infovideo infovideo, IFormFile Poster)
        {
            if (id != infovideo.Id)
            {
                return NotFound();
            }
            var infovideo1 = await _context.Infovideos.FindAsync(id);

            if (Poster!=null)

                    //Convert Image to byte and save to database
                    {

                        byte[] p1 = null;
                        using (var fs1 = Poster.OpenReadStream())
                        using (var ms1 = new MemoryStream())
                        {
                            fs1.CopyTo(ms1);
                            p1 = ms1.ToArray();
                        }

                        // установка массива байтов
                        infovideo.Poster = p1;

                    }else
                    {  
                    infovideo.Poster = infovideo1.Poster; }


           
            _context.Entry(infovideo1).CurrentValues.SetValues(infovideo);
            await _context.SaveChangesAsync();

           
            
            return RedirectToAction(nameof(Index), "Home/Index");
        }
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
          private bool InfovideoExists(int id)
        {
            return _context.Infovideos.Any(e => e.Id == id);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
