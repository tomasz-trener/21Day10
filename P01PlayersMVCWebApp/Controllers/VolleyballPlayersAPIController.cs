using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using P01PlayersMVCWebApp.Configuration;
using P01PlayersMVCWebApp.Models;

namespace P01PlayersMVCWebApp.Controllers
{
    public class VolleyballPlayersAPIController : Controller
    {
        private readonly VolleyballWebContext _context;
        private readonly HttpClient _client;
        private readonly ApiSettings _apiSettings;
        private readonly string _resourcePath;
        public VolleyballPlayersAPIController(IHttpClientFactory clientFactory, IOptions<ApiSettings> apiSettings)
        {
            _client = clientFactory.CreateClient();
            _apiSettings = apiSettings.Value;
            _resourcePath = "/volleyballplayers";  // Definiowanie ścieżki zasobu
        }

        // GET: VolleyballPlayersAPI
        public async Task<IActionResult> Index()
        {
            var response = await _client.GetAsync($"{_apiSettings.BaseUrl}{_resourcePath}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var volleyballPlayers = JsonConvert.DeserializeObject<IEnumerable<VolleyballPlayer>>(content);
                return View(volleyballPlayers);
            }
            else
            {
                return Problem("Cannot acces to API");
            }


        }

        // GET: VolleyballPlayersAPI/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var response = await _client.GetAsync($"{_apiSettings.BaseUrl}{_resourcePath}/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var volleyballPlayer = JsonConvert.DeserializeObject<VolleyballPlayer>(content);
                return View(volleyballPlayer);
            }
            else
            {
                return NotFound();
            }
        }

        // GET: VolleyballPlayersAPI/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VolleyballPlayersAPI/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Position,Number")] VolleyballPlayer volleyballPlayer)
        {
            if (ModelState.IsValid)
            {
                var response = await _client.PostAsJsonAsync($"{_apiSettings.BaseUrl}{_resourcePath}", volleyballPlayer);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(volleyballPlayer);
        }

        // GET: VolleyballPlayersAPI/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var response = await _client.GetAsync($"{_apiSettings.BaseUrl}{_resourcePath}/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var volleyballPlayer = JsonConvert.DeserializeObject<VolleyballPlayer>(content);
                return View(volleyballPlayer);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: VolleyballPlayersAPI/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Position,Number")] VolleyballPlayer volleyballPlayer)
        {
            if (id != volleyballPlayer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var response = await _client.PutAsJsonAsync($"{_apiSettings.BaseUrl}{_resourcePath}/{id}", volleyballPlayer);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(volleyballPlayer);
                }
            }
            return View(volleyballPlayer);
        }

        // GET: VolleyballPlayersAPI/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var response = await _client.GetAsync($"{_apiSettings.BaseUrl}{_resourcePath}/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var volleyballPlayer = JsonConvert.DeserializeObject<VolleyballPlayer>(content);
                return View(volleyballPlayer);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: VolleyballPlayersAPI/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _client.DeleteAsync($"{_apiSettings.BaseUrl}{_resourcePath}/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return NotFound();
            }
        }

        private bool VolleyballPlayerExists(int id)
        {
          return (_context.VolleyballPlayers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
