﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EMarket.Areas.Admin.Models;

namespace EMarket.Areas.Client.Controllers
{
    //[AutoValidateAntiforgeryToken]
    [Area("Client")]
    public class HangHoaController : Controller
    {
        private readonly EMarketContext _context;

        public HangHoaController(EMarketContext context)
        {
            _context = context;
        }

        // GET: HangHoa
        public async Task<IActionResult> Index(int? page)
        {
            int pageSize = 3;
            if (page == null) page = 1;
            var eMarketContext = _context.HangHoa.Include(h => h.Loai).Include(h => h.NhaCungCap);
            return View(await PaginatedList<HangHoa>.CreateAsync(eMarketContext,page ?? 1,pageSize));
        }

        // GET: HangHoa/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hangHoa = await _context.HangHoa
                .Include(h => h.Loai)
                .Include(h => h.NhaCungCap)
                .FirstOrDefaultAsync(m => m.HangHoaId == id);
            if (hangHoa == null)
            {
                return NotFound();
            }

            return View(hangHoa);
        }

        // GET: HangHoa/Create
        
        public IActionResult Create()
        {
            ViewData["LoaiId"] = new SelectList(_context.Loai, "LoaiId", "TenLoai");
            ViewData["NhaCungCapId"] = new SelectList(_context.NhaCungCap, "NhaCungCapId", "TenNhaCungCap");
            return View();
        }

        // POST: HangHoa/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HangHoaId,TenHangHoa,NhaCungCapId,LoaiId,Gia,Hinh,MoTa")] HangHoa hangHoa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hangHoa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LoaiId"] = new SelectList(_context.Loai, "LoaiId", "TenLoai", hangHoa.LoaiId);
            ViewData["NhaCungCapId"] = new SelectList(_context.NhaCungCap, "NhaCungCapId", "TenNhaCungCap", hangHoa.NhaCungCapId);
            return View(hangHoa);
        }

        // GET: HangHoa/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hangHoa = await _context.HangHoa.FindAsync(id);
            if (hangHoa == null)
            {
                return NotFound();
            }
            ViewData["LoaiId"] = new SelectList(_context.Loai, "LoaiId", "TenLoai", hangHoa.LoaiId);
            ViewData["NhaCungCapId"] = new SelectList(_context.NhaCungCap, "NhaCungCapId", "TenNhaCungCap", hangHoa.NhaCungCapId);
            return View(hangHoa);
        }

        // POST: HangHoa/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HangHoaId,TenHangHoa,NhaCungCapId,LoaiId,Gia,Hinh,MoTa")] HangHoa hangHoa)
        {
            if (id != hangHoa.HangHoaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hangHoa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HangHoaExists(hangHoa.HangHoaId))
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
            ViewData["LoaiId"] = new SelectList(_context.Loai, "LoaiId", "TenLoai", hangHoa.LoaiId);
            ViewData["NhaCungCapId"] = new SelectList(_context.NhaCungCap, "NhaCungCapId", "TenNhaCungCap", hangHoa.NhaCungCapId);
            return View(hangHoa);
        }

        // GET: HangHoa/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hangHoa = await _context.HangHoa
                .Include(h => h.Loai)
                .Include(h => h.NhaCungCap)
                .FirstOrDefaultAsync(m => m.HangHoaId == id);
            if (hangHoa == null)
            {
                return NotFound();
            }

            return View(hangHoa);
        }

        // POST: HangHoa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hangHoa = await _context.HangHoa.FindAsync(id);
            _context.HangHoa.Remove(hangHoa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HangHoaExists(int id)
        {
            return _context.HangHoa.Any(e => e.HangHoaId == id);
        }
    }
}