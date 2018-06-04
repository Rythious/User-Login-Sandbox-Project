using Microsoft.AspNetCore.Mvc;
using System;
using UserApi.DataAbstraction;

namespace UserApi.Controllers
{
    [Route("")]
    public class UserController : Controller
    {
        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]UserPost user)
        {
            if (!ModelState.IsValid)
            {
                BadRequest(ModelState);
            }

            var userAccount = UserAccount.FromUserNameAndPassword(user.UserName, user.Password, user.EmailAddress);

            var userAccountManager = new UserAccountManager();
            userAccountManager.Add(userAccount);

            return CreatedAtAction(nameof(Get), userAccount);
        }

        [HttpPost("/Login")]
        public IActionResult Post([FromBody]UserLoginAttempt loginAttempt)
        {
            if (!ModelState.IsValid)
            {
                BadRequest(ModelState);
            }

            if (PasswordChecker.PasswordIsValid(loginAttempt))
            {
                var userAccountManager = new UserAccountManager();
                var userAccount = userAccountManager.GetByUserName(loginAttempt.UserName);

                var lastLogin = userAccount.LastLogin;

                userAccount.LastLogin = DateTime.UtcNow;

                userAccountManager.Update(userAccount);

                var timeSpan = userAccount.LastLogin - lastLogin;

                return Ok($"Last successful login: {timeSpan.Days} days, {timeSpan.Hours} hours, {timeSpan.Minutes} minutes, and {timeSpan.Seconds} seconds.");
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            var userAccountManager = new UserAccountManager();
            var userAccount = userAccountManager.GetById(id);

            return Ok(userAccount);
        }
    }
}
