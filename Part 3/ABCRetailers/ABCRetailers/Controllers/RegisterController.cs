using ABCRetailers.Models;
using ABCRetailers.Services;
using Microsoft.AspNetCore.Mvc;

using ABCRetailers.Data;
using ABCRetailers.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ABCRetailers.Controllers
{
    public class RegisterController : Controller
    {
        private readonly AuthDbContext _db;
        private readonly IFunctionsApi _functionsApi;

        public RegisterController(AuthDbContext db, IFunctionsApi functionsApi)
        {
            _db = db;
            _functionsApi = functionsApi;
        }

        // GET: /Register
        public IActionResult Index()
        {
            return View();
        }

        // POST: /Register
        [HttpPost]
        public async Task<IActionResult> Index(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Check if username already exists
            var exists = await _db.Users.AnyAsync(u => u.Username == model.Username);
            if (exists)
            {
                ViewBag.Error = "Username already exists.";
                return View(model);
            }

            //Save to SQL: create User
            var user = new User
            {
                Username = model.Username,
                PasswordHash = model.Password, //Plaintext — replace with hashed version in production
                Role = model.Role
            };
            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            //Save to Azure Table Storage via Azure Function
            var customer = new Customer
            {
                Username = model.Username,
                Name = model.Name,
                Surname = model.Surname,
                Email = model.Email,
                ShippingAddress = model.ShippingAddress
            };
            await _functionsApi.CreateCustomerAsync(customer);

            TempData["Success"] = "Account created successfully. Please login.";
            return RedirectToAction("Index", "Login");
        }
    }
}
