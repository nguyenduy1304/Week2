using Demo_T2.Models;
using Demo_T2.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo_T2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ILogger<ValuesController> _logger;

        private IUserRepository _userRepository;
        private IUserDetailRepository _userDetailRepository;
        public ValuesController(ILogger<ValuesController> logger,
                                IUserRepository userRepository,
                                 IUserDetailRepository userDetailRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
            _userDetailRepository = userDetailRepository;
        }

        [HttpGet]
        public  ActionResult <IEnumerable<User>> GetAllUser()
        {
            return  Ok(_userRepository.GetUsers());
        }
    }
}
