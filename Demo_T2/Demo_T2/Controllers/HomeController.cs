using Demo_T2.DAL;
using Demo_T2.Models;
using Demo_T2.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Diagnostics;
using static Demo_T2.Models.Hash;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Drawing.Printing;
using Demo_T2.Services;

namespace Demo_T2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private IUserRepository _userRepository;
        private IUserDetailRepository _userDetailRepository;
        private readonly IUserSevice _userSevice;
        public HomeController(ILogger<HomeController> logger,
                                IUserRepository userRepository,
                                 IUserDetailRepository userDetailRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
            _userDetailRepository = userDetailRepository;
        }

        public IActionResult Index(int? pageNumber)
        {
            int pageSize = 2;
            var user = _userRepository.GetUsers();

            return View(PaginatedList<User>.CreateAsync(user, pageNumber ?? 1, pageSize));
        }

        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUser(CreateUserRequest user)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    _userSevice.CreateUser(user);

                    _logger.LogInformation("Create new user successfully.");

                    return RedirectToAction("Index");

                }
                else
                {
                   return View(user);
                }

            }
            catch (DataException)
            {
                _logger.LogError("Create new user Failed");
                return View();
            }
        }

        [HttpGet]
        public IActionResult EditUser(String id)
        {
            var userid = _userRepository.GetUserByID(id);
            return View(userid);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditUser([Bind("Id, Username, Email, Password, UserDetail")] User user)
        {
            _userRepository.UpdateUser(user);


            _userRepository.Save();
            return RedirectToAction("Index");

        }

        public IActionResult Delete(String id)
        {
            try
            {
                _userRepository.DeleteUser(id);
                _userRepository.Save();
                ViewBag.Error = "Delete user successfully";
            }
            catch (DataException)
            {
                ViewBag.Error = "Delete user Failed";
                return View();
            }
            return RedirectToAction("Index");
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