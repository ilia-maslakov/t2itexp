using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using t2itexp.Data;
using t2itexp.Data.EF;
using t2itexp.Models;
using t2itexp.Validators;

namespace t2itexp.Controllers
{
    [Controller]
    public class ClientController : Controller
    {
        private readonly ILogger<ApiController> _logger;
        private readonly UnitOfWork _unitOfWork;
        public ClientController(ILogger<ApiController> logger, UnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        // GET: ClientController
        [HttpGet("")]
        public ActionResult IndexAsync()
        {
            return RedirectToAction("Page", "Client", new { page = 0 });
        }

        // pagination
        [HttpGet("Client/Page/{page}")]
        public ActionResult Page([FromRoute] int? page = 0)
        {
            int p = page ?? default;
            int pageSize = 3;
            if (p < 0)
            {
                p = 0;
            }
            var phones = _unitOfWork.Phone.Get(p, pageSize);
            int lastPage = _unitOfWork.Phone.Count() / pageSize;
            int pagesOnScreen = Math.Min(5, lastPage);
            PhoneList model = new() { Phones = phones, CurrentPage = p, PageSize = pageSize, LastPage = lastPage, PagesOnScreen = pagesOnScreen };

            return View("Index", model);
        }

        // GET: ClientController/Create
        public ActionResult Create()
        {
            return RedirectToAction("Page", "Client", new { page = 0 });
        }

        // POST: ClientController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(IFormCollection collection)
        {
            try
            {
                string? json = collection["jsonTextarea"];
                string? clearTable = collection["delCheck"];

                if (json != null) {
                    List<Phone>? phones = JsonSerializer.Deserialize<List<Phone>>(json);
                    int count = 0;
                    if (phones != null)
                    {
                        
                        if (clearTable != null)
                        {
                            // clear table
                            _unitOfWork.Phone.Remove(x => x.Id > 0);
                        }
                        count = await _unitOfWork.Phone.AddRange(phones);
                    }
                }
            }
            catch (Exception error)
            {
                _logger.LogInformation("{Time} User validation errors: {Message}", DateTime.UtcNow.ToLongTimeString(), error.Message);
            }
            return RedirectToAction("Page", "Client", new { page = 0 });
        }
    }
}
