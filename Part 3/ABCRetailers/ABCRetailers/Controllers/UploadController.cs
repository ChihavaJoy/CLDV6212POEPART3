using ABCRetailers.Models;
using ABCRetailers.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace ABCRetailers.Controllers
{
    [Authorize] // Ensure only authenticated users can access
    public class UploadController : Controller
    {
        private readonly IFunctionsApi _functionsApi;

        public UploadController(IFunctionsApi functionsApi)
        {
            _functionsApi = functionsApi;
        }

        // Display the file upload form
        public IActionResult Index()
        {
            return View(new FileUploadModel());
        }

        // Handle file upload
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(FileUploadModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                if (model.ProofOfPayment != null && model.ProofOfPayment.Length > 0)
                {
                    // Use IFunctionsApi to handle upload
                    var fileUrl = await _functionsApi.UploadProofOfPaymentAsync(
                        model.ProofOfPayment,
                        model.OrderId,
                        model.CustomerName
                    );

                    TempData["Success"] = $"File uploaded successfully! URL: {fileUrl}";

                    // Reset the form
                    return View(new FileUploadModel());
                }
                else
                {
                    ModelState.AddModelError("ProofOfPayment", "Please select a file to upload.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error uploading file: {ex.Message}");
            }

            return View(model);
        }
    }
}
