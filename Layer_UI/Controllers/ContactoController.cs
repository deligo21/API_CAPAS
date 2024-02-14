using Layer_UI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Layer_UI.Controllers
{
    public class ContactoController : Controller
    {
        private readonly HttpClient _httpClient;
        public ContactoController(IHttpClientFactory httpClientFactory)
        {
            this._httpClient = httpClientFactory.CreateClient();
            this._httpClient.BaseAddress = new Uri("http://localhost:5198/api");
        }
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/contacto");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var contactos = JsonConvert.DeserializeObject<IEnumerable<VMContacto>>(content);

                return View("Index", contactos);
            }

            return View(new List<VMContacto>());
        }

        public IActionResult Insertar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Insertar(VMContacto contacto) {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(contacto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("api/contacto", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al crear producto");
                }
            }
            return View(contacto);
        }

        public async Task<IActionResult> Editar(int id)
        {
            var response = await _httpClient.GetAsync($"api/contacto/id/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var contacto = JsonConvert.DeserializeObject<VMContacto>(content);

                return View(contacto);
            }
            else
            {
                return RedirectToAction("Detalles");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Editar(int id, VMContacto contacto)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(contacto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"api/contacto/{id}", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", new { id });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al actualizar contacto");
                }
            }
            return View(contacto);
        }

        public async Task<IActionResult> Detalles(int id)
        {
            var response = await _httpClient.GetAsync($"api/contacto/id/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync ();
                var contacto = JsonConvert.DeserializeObject<VMContacto>(content);

                return View(contacto);
            }
            else
            {
                return RedirectToAction("Detalles");
            }
        }

        public async Task<IActionResult> Borrar(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/contacto/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Error"] = "Error al eliminar producto";
                return RedirectToAction("Index");
            }
        }
    }
}