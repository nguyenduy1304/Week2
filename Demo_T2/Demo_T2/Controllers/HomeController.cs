using Demo_T2.DAL;
using Demo_T2.Models;
using Demo_T2.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
namespace Demo_T2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private UserRepository userRepository;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Xin chào LogInformation nhá !!!");
            return View();
        }
        [HttpGet]
        public IActionResult Add_User()
        {
            var students = from s in userRepository.GetUsers()
                           select s;
            return View(students);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult New_User(FormCollection collection)
        {
            #region

            #endregion

            return View();
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