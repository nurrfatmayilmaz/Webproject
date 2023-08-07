using Microsoft.AspNetCore.Mvc;
using AnimalStoreMvc.Models.DTO;
using AnimalStoreMvc.Repositories.Abstract;
using System.Xml.Linq;
using System.Data;

namespace AnimalStoreMvc.Controllers
{
    public class UserAuthenticationController : Controller
    {
        private IUserAuthenticationService authService;
        public UserAuthenticationController(IUserAuthenticationService authService)
        {
            this.authService = authService;
        }
        /* We will create a user with admin rights, after that we are going
          to comment this method because we need only
          one user in this application 
          If you need other users ,you can implement this registration method with view
          I have create a complete tutorial for this, you can check the link in description box
         */

        public async Task<IActionResult> Register123()
        {
            var model = new RegistrationModel
            {
                Email = "Fatmanur@gmail.com",
                Username = "B201210067@sakarya.edu.tr",
                Name = "Fatmanur",
                Password = "Sau@123",
                PasswordConfirm = "Sau@123",
                Role = "Admin"
            };
           // if you want to register with user , Change Role = "User"
            var result = await authService.RegisterAsync(model);
            return Ok(result.Message);
        }

        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationModel model)
        {
            model.Email = "Fatma@gmail.com";
            model.Name = "Fatma";
            model.Role = "User";
            var result = await authService.RegisterAsync(model);

            return Ok(result.Message);

           // return RedirectToAction(nameof(Login));
            
          
        }

        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await authService.LoginAsync(model);
            if (result.StatusCode == 1)
                return RedirectToAction("Index", "Home");
            else
            {
                TempData["msg"] = "Could not logged in..";
                return RedirectToAction(nameof(Login));
            }
        }

        public async Task<IActionResult> Logout()
        {
            await authService.LogoutAsync();
            return RedirectToAction(nameof(Login));
        }

    }
}