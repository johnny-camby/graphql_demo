using GraphQL.Client.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Diagnostics;
using WebXmlApp.Models;
using WebXmlApp.Services;
using XmlDataExtractManager.Interfaces;

namespace WebXmlApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBufferedFileUploadService _bufferedFileUploadService;
        private readonly IXmlDataExtractorService _xmlDataExtractorService;
        private readonly XmlDataHttpClient _httpClient;
        private readonly XmlDataGraphClient _graphQLHttpClient;

        public HomeController(ILogger<HomeController> logger,
            IBufferedFileUploadService bufferedFileUploadService,
            IXmlDataExtractorService xmlDataExtractorService,
            XmlDataHttpClient httpClient,
            XmlDataGraphClient graphQLHttpClient
            )
        {           
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _bufferedFileUploadService = bufferedFileUploadService ?? throw new ArgumentNullException(nameof(bufferedFileUploadService));
            _xmlDataExtractorService = xmlDataExtractorService ?? throw new ArgumentNullException(nameof(xmlDataExtractorService));
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _graphQLHttpClient = graphQLHttpClient ?? throw new ArgumentNullException(nameof(graphQLHttpClient));
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetCustomers();
            response.ThrowErrors();
            return View(response.Data.Customers);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var customer = await _graphQLHttpClient.GetCustomer(id);
            return View(customer);
        }


        [HttpPost]
        public async Task<IActionResult> UploadXmlAsync(IFormFile file)
        {
            if (file != null)
            {
                try
                {
                    var (isExisting, xmlfile) = await _bufferedFileUploadService.UploadFile(file);
                    if (!string.IsNullOrEmpty(xmlfile))
                    {
                        ViewBag.Message = "File Upload Successful";
                        await _xmlDataExtractorService.ProcessXmlAsync(xmlfile);
                    }
                    else
                    {
                        Log.Information("Not proper xml file");
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "File Upload Failed");
                }
            }

            return RedirectToAction(nameof(Index));
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}