using Microsoft.AspNetCore.Mvc;
using RunGroubweb.Interfaces;
using RunGroubweb.ViewModels;

namespace RunGroubweb.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;   
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        
        }
        [HttpGet("users")]
        public async Task <IActionResult> Index()
        {
            var users = await _userRepository.GetAllUsers();
            List<UserViewModel> Result = new List<UserViewModel>();
            foreach (var user in users)
            {
                var UserViewModel = new UserViewModel()
                {
                    Id = user.Id,
                    UserName= user.UserName,
                    Pace = user.Pace,
                    Mileage = user.MIleage,
                    ProfileImageUrl = user.ProfileImageUrl

                };
                Result.Add(UserViewModel);
            
            }
            return View(Result);
        }
        public async Task<IActionResult> Detail(string id)
        {
            var user = await _userRepository.GetUserById(id);
            var userDetailViewModel = new UserDetailViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                Pace = user.Pace,
                Mileage = user.MIleage

            };
            return View(userDetailViewModel);
        }
    }
}
