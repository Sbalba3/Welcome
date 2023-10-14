using Demo.BLL.Interfaces;
using Demo.DAl.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Welcome.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public DepartmentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task< IActionResult> Index()
        {
            IEnumerable<Department> departments =await _unitOfWork.DepartmentRepo.GetAll();
            return View(departments);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Create(Department department)
        {
            if (ModelState.IsValid)
            {
               await _unitOfWork.DepartmentRepo.Add(department);
               await _unitOfWork.Save();
                TempData["Message"] = "Department is created Successfuly";
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }
        public async Task< IActionResult> Details(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Department department =await _unitOfWork.DepartmentRepo.GetById(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }
        public async Task< IActionResult> Edit(int id )
        {
            Department department=await _unitOfWork.DepartmentRepo.GetById(id);
            return View(department);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Edit([FromRoute]int id,Department department)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.DepartmentRepo.Update(department);
                   await _unitOfWork.Save();
                    return RedirectToAction(nameof(Index));
                }catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(department);
           
        }
        public async Task< IActionResult> Delete(int id)
        {
           Department department=await _unitOfWork.DepartmentRepo.GetById(id);
            if(department == null)
            {
                return BadRequest();
            }
            return View(department);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Delete([FromRoute]int id,Department department)
        {
            try
            {
                _unitOfWork.DepartmentRepo.Delete(department);
               await _unitOfWork.Save();
                return RedirectToAction(nameof(Index));

            }catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(department);

            }
        }

        public IActionResult EmployeesCount(int id)
        {
            _unitOfWork.DepartmentRepo.GetDepartmentEmployees(id);
            return View();


        }


    }
}
