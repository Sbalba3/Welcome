using AutoMapper;
using Demo.DAl.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Welcome.Helpers;
using Welcome.ViewModels;

namespace Welcome.Controllers
{
    public class UserController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;

        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }
        public  async Task<IActionResult> Index()
        {
            var users =await _userManager.Users.Select(U => new UserViewModel() 
            {
                Id = U.Id,
                FName=U.FName,
                LName=U.LName,
                PhoneNumber=U.PhoneNumber,
                Email=U.Email,
                Roles=_userManager.GetRolesAsync(U).Result
            }).ToListAsync();
            return View(users);
        }
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var mappedUser = _mapper.Map<ApplicationUser, UserViewModel>(user);
            return View(mappedUser);
        }
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var mappedUser = _mapper.Map<ApplicationUser, UserViewModel>(user);
            return View(mappedUser);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, UserViewModel userVm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user= await _userManager.FindByIdAsync(id);
                    user.FName=userVm.FName;
                    user.LName=userVm.LName;
                    user.PhoneNumber=userVm.PhoneNumber;


                    await _userManager.UpdateAsync(user);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(userVm);

        }

        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var mappedUser = _mapper.Map<ApplicationUser, UserViewModel>(user);
            if (user == null)
            {
                return BadRequest();
            }
            return View(mappedUser);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] string id, UserViewModel userVm)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);

                await _userManager.DeleteAsync(user);
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                var mappedEmp = _mapper.Map<UserViewModel, ApplicationUser>(userVm);
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(mappedEmp);

            }
        }
    }
}
