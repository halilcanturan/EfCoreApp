using EfCoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EfCoreApp.Controllers
{
    public class KursController : Controller
    {
        private readonly DataContext _context;
        public KursController(DataContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            var kurs = await _context.Kurslar
                .Include(k => k.Ogretmen)
                .ToListAsync();
            return View(kurs);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Ogretmenler = new SelectList(await _context.Ogretmenler.ToListAsync(), "OgretmenId", "AdSoyad");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(KursViewModel model)
        {
            if(ModelState.IsValid)
            { 
            _context.Kurslar.Add(new Kurs()
            {
                KursId = model.KursId,
                Baslik = model.Baslik,
                OgretmenId = model.OgretmenId});
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
            }
            ViewBag.Ogretmenler = new SelectList(await _context.Ogretmenler.ToListAsync(), "OgretmenId", "AdSoyad");
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var krs = await _context
                .Kurslar
                .Include(o => o.KursKayitlari)
                .ThenInclude(o => o.Ogrenci)
                .Select(o => new KursViewModel
                {
                    KursId = o.KursId,
                    Baslik = o.Baslik,
                    OgretmenId = o.OgretmenId,
                    KursKayitlari = o.KursKayitlari,
                })
                .FirstOrDefaultAsync(o => o.KursId == id);

            if (krs == null)
            {
                return NotFound();
            }

            ViewBag.Ogretmenler = new SelectList(await _context.Ogretmenler.ToListAsync(), "OgretmenId", "AdSoyad");

            return View(krs);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]//güvenlik önlemi
        public async Task<IActionResult> Edit(int id, KursViewModel model)
        {
            if (id != model.KursId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(new Kurs()
                    {
                        KursId = model.KursId,
                        Baslik = model.Baslik,
                        OgretmenId = model.OgretmenId
                    });
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    if (!_context.Kurslar.Any(o => o.KursId == model.KursId))
                    {
                        return NotFound();
                    }
                    else
                    { throw; 
                    }
                }
                return RedirectToAction("Index");
            }
            ViewBag.Ogretmenler = new SelectList(await _context.Ogretmenler.ToListAsync(), "OgretmenId", "AdSoyad");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kurs = await _context.Kurslar.FindAsync(id);

            return View(kurs);
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int id)
        {
            var kurs = await _context.Kurslar.FindAsync(id);
            _context.Kurslar.Remove(kurs);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
