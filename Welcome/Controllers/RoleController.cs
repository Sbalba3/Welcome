using AutoMapper;
using Demo.DAl.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Welcome.ViewModels;

namespace Welcome.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public RoleController(RoleManager<IdentityRole> roleManager,IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.Select(R => new RoleViewModel()
            {
                Id = R.Id,
                RoleName=R.Name
               
            }).ToListAsync();
            return View(roles);
        }
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            var mappedRole = _mapper.Map<IdentityRole, RoleViewModel>(role);
            return View(mappedRole);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task< IActionResult> Create(RoleViewModel roleVm)
        {
            if (ModelState.IsValid)
            {
                var roleMapped=_mapper.Map<RoleViewModel,IdentityRole>(roleVm);
                await _roleManager.CreateAsync(roleMapped);
                return RedirectToAction(nameof(Index));

            }
            return View(roleVm);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            var mappedRole = _mapper.Map<IdentityRole, RoleViewModel>(role);
            return View(mappedRole);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, RoleViewModel roleVm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var role = await _roleManager.FindByIdAsync(id);
                    role.Name = roleVm.RoleName;
                    await _roleManager.UpdateAsync(role);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(roleVm);

        }

        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            var mappedRole = _mapper.Map<IdentityRole, RoleViewModel>(role);
            if (role == null)
            {
                return BadRequest();
            }
            return View(mappedRole);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] string id, RoleViewModel roleVm)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(id);

                await _roleManager.DeleteAsync(role);
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                var mappedRole = _mapper.Map<RoleViewModel, IdentityRole>(roleVm);
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(mappedRole);

            }
        }
    }

}
