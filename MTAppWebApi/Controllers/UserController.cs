using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MTAPP.DAL.Model;
using MTAPP.DAL.Repository;
using MTAPP.Model;
using MTAppWebApi.Utilities;

namespace MTAppWebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IGenericRepository<AppUser> _userRepository;

        public UserController(IGenericRepository<AppUser> userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        public IActionResult GetUsers()
        {
            var userdetails = _userRepository.Get().Include(x => x.role).AsEnumerable().Select(x => UserServiceUtility.ConvertToModel(x));
            return Ok(userdetails);
        }

        [HttpPost]
        public IActionResult GetUsersById(long userid)
        {
            var userdetail = _userRepository.Get().Include(x => x.role).Where(x => x.userid == userid)?.FirstOrDefault();
            return Ok(UserServiceUtility.ConvertToModel(userdetail));
        }

        [HttpPost]
        public IActionResult SaveUser(UserModel appUser)
        {
            var userdetails = _userRepository.Get().Where(x => x.username.Equals(appUser.username))?.SingleOrDefault();
            if (userdetails != null && appUser.userid == 0)
                return BadRequest("UserName is already exists");
            var dbusermodel = UserServiceUtility.ConvertToDBModel(appUser);
            if (appUser.userid == 0)
                _userRepository.Insert(dbusermodel);
            else
                _userRepository.Update(dbusermodel);
            return Ok(UserServiceUtility.ConvertToModel(dbusermodel));
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult RegisterUser(UserModel appUser)
        {
            var userdetails = _userRepository.Get().Where(x => x.username.Equals(appUser.username))?.SingleOrDefault();
            if (userdetails != null)
                return BadRequest("UserName is already exists");
            var dbusermodel = UserServiceUtility.ConvertToDBModel(appUser);
            _userRepository.Insert(dbusermodel);
            return Ok(UserServiceUtility.ConvertToModel(dbusermodel));
        }

        [HttpPost]
        public void DeleteUsersById(long userid)
        {
            var user = _userRepository.Get().Where(x => x.userid == userid)?.FirstOrDefault();
            if (user != null)
                _userRepository.Delete(user);
        }
    }
}
