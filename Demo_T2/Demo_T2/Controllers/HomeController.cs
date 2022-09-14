using Demo_T2.DAL;
using Demo_T2.Models;
using Demo_T2.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Diagnostics;
using static Demo_T2.Models.Hash;
using PageList;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Demo_T2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private IUserRepository _userRepository;
        private IUserDetailRepository _userDetailRepository;
        public HomeController(ILogger<HomeController> logger,
                                IUserRepository userRepository,
                                 IUserDetailRepository userDetailRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
            _userDetailRepository = userDetailRepository;
        }

        public IActionResult Index(int pn)
        {
            var user = _userRepository.GetUsers();
            ViewBag.Paging = Set_Paging(pn, 2, user.Count(), "activeLink", Url.Action("Index", "Home"), "disableLink");   
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
                    _userRepository.InsertUser(user);
                    #endregion

                    #region UserDetail
                    UserDetail userDetail = new UserDetail();
                    userDetail.IdUser = user.Id;
                    userDetail.FirstName = firstName;
                    userDetail.LastName = lastName;
                    userDetail.PhoneNumber = phoneNumber;
                    userDetail.Address = address;
                    _userDetailRepository.InsertUserDetail(userDetail);
                    #endregion

                    _userRepository.Save();
                    return RedirectToAction("Index");
                    _logger.LogInformation("Create new user successfully.");

                }
                else
                {
                    ViewBag.Error = "Please enter full information";
                    return View();
                }
            }
            catch(DataException)
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




        public string Set_Paging(Int32 PageNumber, int PageSize, Int64 TotalRecords, string ClassName, string PageUrl, string DisableClassName)
        {
            string ReturnValue = "";
            try
            {
                Int64 TotalPages = Convert.ToInt64(Math.Ceiling((double)TotalRecords / PageSize));
                if (PageNumber > 1)
                {
                    if (PageNumber == 2)
                        ReturnValue = ReturnValue + "<a href='" + PageUrl.Trim() + "?pn=" + Convert.ToString(PageNumber - 1) + "' class='" + ClassName + "'>Previous</a>&nbsp;&nbsp;&nbsp;";
                    else
                    {
                        ReturnValue = ReturnValue + "<a href='" + PageUrl.Trim();
                        if (PageUrl.Contains("?"))
                            ReturnValue = ReturnValue + "&";
                        else
                            ReturnValue = ReturnValue + "?";
                        ReturnValue = ReturnValue + "pn=" + Convert.ToString(PageNumber - 1) + "' class='" + ClassName + "'>Previous</a>&nbsp;&nbsp;&nbsp;";
                    }
                }
                else
                    ReturnValue = ReturnValue + "<span class='" + DisableClassName + "'>Previous</span>&nbsp;&nbsp;&nbsp;";
                if ((PageNumber - 3) > 1)
                    ReturnValue = ReturnValue + "<a href='" + PageUrl.Trim() + "' class='" + ClassName + "'>1</a>&nbsp;.....&nbsp;|&nbsp;";
                for (int i = PageNumber - 3; i <= PageNumber; i++)
                    if (i >= 1)
                    {
                        if (PageNumber != i)
                        {
                            ReturnValue = ReturnValue + "<a href='" + PageUrl.Trim();
                            if (PageUrl.Contains("?"))
                                ReturnValue = ReturnValue + "&";
                            else
                                ReturnValue = ReturnValue + "?";
                            ReturnValue = ReturnValue + "pn=" + i.ToString() + "' class='" + ClassName + "'>" + i.ToString() + "</a>&nbsp;|&nbsp;";
                        }
                        else
                        {
                            ReturnValue = ReturnValue + "<span style='font-weight:bold;'>" + i + "</span>&nbsp;|&nbsp;";
                        }
                    }
                for (int i = PageNumber + 1; i <= PageNumber + 3; i++)
                    if (i <= TotalPages)
                    {
                        if (PageNumber != i)
                        {
                            ReturnValue = ReturnValue + "<a href='" + PageUrl.Trim();
                            if (PageUrl.Contains("?"))
                                ReturnValue = ReturnValue + "&";
                            else
                                ReturnValue = ReturnValue + "?";
                            ReturnValue = ReturnValue + "pn=" + i.ToString() + "' class='" + ClassName + "'>" + i.ToString() + "</a>&nbsp;|&nbsp;";
                        }
                        else
                        {
                            ReturnValue = ReturnValue + "<span style='font-weight:bold;'>" + i + "</span>&nbsp;|&nbsp;";
                        }
                    }
                if ((PageNumber + 3) < TotalPages)
                {
                    ReturnValue = ReturnValue + ".....&nbsp;<a href='" + PageUrl.Trim();
                    if (PageUrl.Contains("?"))
                        ReturnValue = ReturnValue + "&";
                    else
                        ReturnValue = ReturnValue + "?";
                    ReturnValue = ReturnValue + "pn=" + TotalPages.ToString() + "' class='" + ClassName + "'>" + TotalPages.ToString() + "</a>";
                }
                if (PageNumber < TotalPages)
                {
                    ReturnValue = ReturnValue + "&nbsp;&nbsp;&nbsp;<a href='" + PageUrl.Trim();
                    if (PageUrl.Contains("?"))
                        ReturnValue = ReturnValue + "&";
                    else
                        ReturnValue = ReturnValue + "?";
                    ReturnValue = ReturnValue + "pn=" + Convert.ToString(PageNumber + 1) + "' class='" + ClassName + "'>Next</a>";
                }
                else
                    ReturnValue = ReturnValue + "&nbsp;&nbsp;&nbsp;<span class='" + DisableClassName + "'>Next</span>";
            }
            catch (Exception ex)
            {
            }
            return (ReturnValue);
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