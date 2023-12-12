using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreFront.DATA.EF.Models;
using System.Drawing;
using StoreFront.Utilities;

namespace StoreFront.Controllers
{

    public class WeaponsController : Controller
    {
        private readonly StorefrontContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public WeaponsController(StorefrontContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Weapons
        public async Task<IActionResult> Index()
        {
            var storefrontContext = _context.Weapons.Include(w => w.Manufacturer).Include(w => w.Universe);
            return View(await storefrontContext.ToListAsync());
        }

        // GET: Weapons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Weapons == null)
            {
                return NotFound();
            }

            var weapon = await _context.Weapons
                .Include(w => w.Manufacturer)
                .Include(w => w.Universe)
                .FirstOrDefaultAsync(m => m.WeaponId == id);
            if (weapon == null)
            {
                return NotFound();
            }

            return View(weapon);
        }

        // GET: Weapons/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var m = _context.Manufacturers.ToList();
            ViewData["Manufacturers"] = new SelectList(_context.Manufacturers, "ManufacturerId", "CompanyName");
            ViewData["Universes"] = new SelectList(_context.Universes, "UniverseId", "UniverseName");
            return View();
        }

        // POST: Weapons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("WeaponId,WeaponName,Description,Price,ManufacturerId,UniverseId,InStock,Discontinued,WeaponImage,Image")] Weapon weapon)//Added Image to the Bind properties
        {
            if (ModelState.IsValid)
            {
                #region File Upload - CREATE
                if (weapon.Image == null)
                {
                    weapon.WeaponImage = "noimage.png";
                    goto end;
                }
                //else, a file was uploaded
                //check the file type
                string ext = Path.GetExtension(weapon.Image.FileName);
                List<string> validExt = new() { ".jpeg", ".jpg", ".gif", ".png" };
                // varify the upload file has an extension matching one of the valid extensions
                if (validExt.Contains(ext.ToLower()) && weapon.Image.Length < 4_194_303)
                {
                    //generate a unique filename
                    weapon.WeaponImage = Guid.NewGuid() + ext;

                    //save the image file to the web server
                    //path to webroot
                    string webrootPath = _webHostEnvironment.WebRootPath;
                    string fullImagePath = webrootPath + "/assets/img/";

                    //Create a MemoryStream to read the image into the server memory
                    using (var memoryStream = new MemoryStream())
                    {
                        await weapon.Image.CopyToAsync(memoryStream);//transferring from object to server
                                                                            //only do this next using if you're uploading images.
                        using (var img = Image.FromStream(memoryStream))
                        {
                            //Image utility
                            //1) (int) max image size
                            //2) (int max thumbnail size
                            //3) (string) full path
                            //4) (Image) an image
                            //5) (string) filename
                            int maxImage = 500;//in pixels
                            int maxThumbSize = 100;//in pixels
                            ImageUtilities.ResizeImage(fullImagePath, weapon.WeaponImage, img, maxImage, maxThumbSize);
                            //for NOT images:
                            //fileName("path/to/folder","filename");
                        }
                    }
                }
            #endregion
            end:
                _context.Add(weapon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "ManufacturerId", "CompanyName", weapon.ManufacturerId);
            ViewData["UniverseId"] = new SelectList(_context.Universes, "UniverseId", "UniverseName", weapon.UniverseId);
            return View(weapon);
        }


        // GET: Weapons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Weapons == null)
            {
                return NotFound();
            }

            var weapon = await _context.Weapons.FindAsync(id);
            if (weapon == null)
            {
                return NotFound();
            }
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "ManufacturerId", "CompanyName", weapon.ManufacturerId);
            ViewData["UniverseId"] = new SelectList(_context.Universes, "UniverseId", "UniverseName", weapon.UniverseId);
            return View(weapon);
        }

        // POST: Weapons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WeaponId,WeaponName,Description,Price,ManufacturerId,UniverseId,InStock,Discontinued,WeaponImage, Image")] Weapon weapon)
        {
            if (id != weapon.WeaponId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                #region File Upload - EDIT
                //save the old image so we can delete if necessary
                string oldImageName = weapon.WeaponImage;
                if (weapon.Image != null)
                {
                    string ext = Path.GetExtension(weapon.Image.FileName);
                    //list valid extensions:
                    List<string> validExts = new() { ".jpeg", ".jpg", ".png", ".gif" };

                    if (validExts.Contains(ext.ToLower()) && weapon.Image.Length < 4_194_303)
                    {
                        //Get new image name
                        weapon.WeaponImage = Guid.NewGuid() + ext;
                        string fullPath = _webHostEnvironment.WebRootPath + "/assets/img/";
                        //delete the old image
                        if (!oldImageName.ToLower().Contains("noimage"))
                        {
                            ImageUtilities.Delete(fullPath, oldImageName);
                        }
                        //save the new image
                        using (var memoryStream = new MemoryStream())
                        {
                            await weapon.Image.CopyToAsync(memoryStream);
                            using var img = Image.FromStream(memoryStream);
                            int maxImgSize = 500;
                            int maxThumbSize = 100;
                            ImageUtilities.ResizeImage(fullPath, weapon.WeaponImage, img, maxImgSize, maxThumbSize);
                        }  
                    }
                }
                #endregion
                try
                {
                    _context.Update(weapon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WeaponExists(weapon.WeaponId))
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
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "ManufacturerId", "CompanyName", weapon.ManufacturerId);
            ViewData["UniverseId"] = new SelectList(_context.Universes, "UniverseId", "UniverseName", weapon.UniverseId);
            return View(weapon);
        }

        // GET: Weapons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Weapons == null)
            {
                return NotFound();
            }

            var weapon = await _context.Weapons
                .Include(w => w.Manufacturer)
                .Include(w => w.Universe)
                .FirstOrDefaultAsync(m => m.WeaponId == id);
            if (weapon == null)
            {
                return NotFound();
            }

            return View(weapon);
        }

        // POST: Weapons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Weapons == null)
            {
                return Problem("Entity set 'StorefrontContext.Weapons'  is null.");
            }
            var weapon = await _context.Weapons.FindAsync(id);
            if (weapon != null)
            {
                //get the file name
                string oldImageName = weapon.WeaponImage;
                string fullPath = _webHostEnvironment.WebRootPath + "/assets/img/";
                if (!oldImageName.ToLower().Contains("noimage"))
                {
                    ImageUtilities.Delete(fullPath, oldImageName);
                }
                _context.Weapons.Remove(weapon);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WeaponExists(int id)
        {
          return (_context.Weapons?.Any(e => e.WeaponId == id)).GetValueOrDefault();
        }
    }
}
