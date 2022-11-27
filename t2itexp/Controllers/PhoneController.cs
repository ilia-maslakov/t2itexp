using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Text.Json;
using t2itexp.Data;
using t2itexp.Data.EF;
using t2itexp.Validators;

namespace t2itexp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PhoneController : ControllerBase
    {
        private readonly ILogger<PhoneController> _logger;
        private readonly PhoneValidator _validator;
        private readonly UnitOfWork _unitOfWork;

        public PhoneController(ILogger<PhoneController> logger, UnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _validator = new PhoneValidator();

        }

        [HttpGet(Name = "GetPhone")]
        public ContentResult Get(int id)
        {
            var res = _unitOfWork.Phone.GetAsync(id);

            string json = JsonSerializer.Serialize(res);
            return Content(json, "application/json");
        }

        [HttpGet("get/code={code}")]
        public ContentResult GetByCode(int? code)
        {
            var res = _unitOfWork.Phone.Get(c => c.Code == (code ?? default));

            string json = JsonSerializer.Serialize(res);
            return Content(json, "application/json");
        }

        [HttpGet("get/value={value}")]
        public ContentResult GetByValue(string value)
        {
            var res = _unitOfWork.Phone.Get(c => (c.Value ?? "").Contains(value));

            string json = JsonSerializer.Serialize(res);
            return Content(json, "application/json");
        }

        [HttpGet("AddPhone/{code}:{phone}")]
        public async Task<ActionResult<Phone>> AddPhone(int code, string phone)
        {
            var p = new Phone { Code = code, Value = phone };
            if (IsValid(p))
            {
                _unitOfWork.Phone.Add(p);
                try
                {
                    await _unitOfWork.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException e)
                {
                    return BadRequest($"Error: {e.Message}");
                }
            }
            else
            {
                return BadRequest("Incorect params");
            }
            return p;
        }

        [HttpPost("Delete/{id}")]
        public ContentResult Delete(int id)
        {
            _unitOfWork.Phone.Remove(id);
            return Content("OK");
        }

        /// <summary>
        /// Create a phone list.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Add
        ///     [
        ///         {
        ///             "id": 1,
        ///             "Code": "123",
        ///             "Value": "Ths is value"
        ///         },
        ///         {
        ///             "id": 0,
        ///             "Code": "321",
        ///             "Value": "Ths is onother value"
        ///         },
        ///
        /// </remarks>
        /// <param name="array of objects"></param>
        /// <returns>A newly created TodoItem</returns>
        /// <response>Returns the count of added items</response>
        [HttpPost("Add")]
        public async Task<ActionResult<int>> Add([FromBody] List<Phone> phones)
        {
            if (phones == null) {
                return 0;
            }
            foreach (var p in phones)
            {
                p.Id = 0;
                // clear Id then it is filled
                _unitOfWork.Phone.Add(p);
            }
            try
            {
                int count = await _unitOfWork.SaveChangesAsync();
                return Content($"Added {count} records");
            }
            catch (DbUpdateConcurrencyException e)
            {
                return BadRequest($"Error: {e.Message}");
            }
        }


        [HttpGet("Page/{Page}")]
        public ActionResult<IEnumerable<Phone>> GetPage(int? Page, int? PageSize = 20)
        {
            int page = Page ?? 1;
            int psize = PageSize ?? 20;

            var u = _unitOfWork.Phone.Get(page, psize);
            if (u == null)
            {
                return NotFound();
            }

            string json = JsonSerializer.Serialize(u);
            return Content(json, "application/json");
        }

        private bool IsValid(Phone phone)
        {
            var result = _validator.Validate(phone);
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    _logger.LogInformation($"{DateTime.UtcNow.ToLongTimeString()} User validation errors: {error.ErrorMessage}");
                }

                return false;
            }
            return true;
        }
    }
}