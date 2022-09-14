using Demo_T2.DAL;
using Demo_T2.Models;
using Demo_T2.Repository;
using Microsoft.AspNetCore.Mvc;
using static Demo_T2.Models.Hash;
using System.Data;

namespace Demo_T2.Controllers
{
    public class GenericRepositoryController : Controller
    {
        private readonly ILogger<GenericRepositoryController> _logger;

        private UnitOfWork unitOfWork;

        public GenericRepositoryController(UnitOfWork unitOfWork, ILogger<GenericRepositoryController> logger)
        {
            this.unitOfWork = unitOfWork;
            _logger = logger;   
        }
        public ActionResult Index()
        {
            var user = unitOfWork.UserRepository.Get(includeProperties:"UserDetail").ToList();
            return View(user);
        }

        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUser(IFormCollection collection)
        {
            try
            {
                #region GET Values
                String username = collection["Username"];
                String password = collection["Password"];
                String email = collection["Email"];

                String firstName = collection["UserDetail.FirstName"];
                String lastName = collection["UserDetail.LastName"];
                String phoneNumber = collection["UserDetail.PhoneNumber"];
                String address = collection["UserDetail.Address"];
                #endregion

                if (!String.IsNullOrEmpty(username) &&
                !String.IsNullOrEmpty(email) &&
                !String.IsNullOrEmpty(password))
                {
                    #region User
                    User user = new User();
                    user.Id = Guid.NewGuid().ToString();
                    user.Username = username;
                    user.Email = email;
                    user.Password = Hash.GetHash(password, HashType.MD5);
                    unitOfWork.UserRepository.Insert(user);
                    #endregion

                    #region UserDetail
                    UserDetail userDetail = new UserDetail();
                    userDetail.IdUser = user.Id;
                    userDetail.FirstName = firstName;
                    userDetail.LastName = lastName;
                    userDetail.PhoneNumber = phoneNumber;
                    userDetail.Address = address;
                    unitOfWork.UserDetailRepository.Insert(userDetail);
                    #endregion
                    unitOfWork.Save();
                    return RedirectToAction("Index");
                    _logger.LogInformation("Create new user successfully.");

                }
                else
                {
                    ViewBag.Error = "Please enter full information";
                    return View();
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

            var userid = unitOfWork.UserRepository.Get(includeProperties: "UserDetail").SingleOrDefault(c => c.Id.Equals(id));
            return View(userid);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditUser([Bind("Id, Username, Email, Password, UserDetail")] User user)
        {
            unitOfWork.UserRepository.Update(user);
            unitOfWork.Save();
            return RedirectToAction("Index");

        }

        public IActionResult Delete(String id)
        {
            try
            {
                unitOfWork.UserRepository.Delete(id);
                unitOfWork.Save();
                ViewBag.Error = "Delete user successfully";
            }
            catch (DataException)
            {
                ViewBag.Error = "Delete user Failed";
                return View();
            }
            return RedirectToAction("Index");
        }
    }
}
