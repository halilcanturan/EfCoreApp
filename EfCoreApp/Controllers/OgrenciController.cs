using EfCoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EfCoreApp.Controllers
{
    public class OgrenciController : Controller
    {
        private readonly DataContext _context;
        public OgrenciController(DataContext context)
        {

            _context = context;

        }
        public async Task<IActionResult> Index()
        {
            var ogrenciler = await _context.Ogrencis.ToListAsync();
            return View(ogrenciler);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var ogr = await _context.Ogrencis
                .Include(o => o.KursKayitlari)
                .ThenInclude(o=>o.Kurs)//üst satırda içine girdiğimiz tablodan bu tabloya geçiyoruz.
                .FirstOrDefaultAsync(o => o.OgrenciId == id); //Findasync sayesinde id ile bulabiliyoruz.
            //FirstOrDefaultAsync kullanarak id ye göre buluyoruz, id yerine farklı kritere görede arama yapılabilir.
            //var ogr = await _context.Ogrencis.FirstOrDefaultAsync(o =>o.OgrenciId == id);  
            return View(ogr);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]//güvenlik önlemi
        public async Task<IActionResult> Edit(int id, Ogrenci model)
        {
            if (id != model.OgrenciId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Ogrencis.Any(o => o.OgrenciId == model.OgrenciId))
                    {
                        return NotFound();
                    }
                    else
                    { throw; }
                }
            }
            return RedirectToAction("Index");
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Ogrenci model)
        {
            _context.Ogrencis.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ogrenci = await _context.Ogrencis.FindAsync(id);

            return View(ogrenci);
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int id)
        {
            var ogrenci = await _context.Ogrencis.FindAsync(id);
            _context.Ogrencis.Remove(ogrenci);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
