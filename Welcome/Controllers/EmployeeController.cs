using AutoMapper;
using Demo.BLL.Interfaces;
using Demo.DAl.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Welcome.Helpers;
using Welcome.ViewModels;

namespace Welcome.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
       
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public EmployeeController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;


        }

        public async Task< IActionResult> Index()
        {
            IEnumerable<Employee> employees =await _unitOfWork.EmployeeRepo.GetAll();
            var mappedEmployee = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
            return View(mappedEmployee);
        }
        public async Task< IActionResult> Create( )
        {  
            ViewData["Departments"]=await _unitOfWork.DepartmentRepo.GetAll();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Create(EmployeeViewModel employeeVm)
        {
            if (ModelState.IsValid)
            {
                
                employeeVm.ImageName = DocumentSettinga.UploadFile(employeeVm.Image, "Images");
                var mappedEmp=_mapper.Map<EmployeeViewModel, Employee>(employeeVm);
               await _unitOfWork.EmployeeRepo.Add(mappedEmp);
               await _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Departments"] =await _unitOfWork.DepartmentRepo.GetAll();
            return View(employeeVm);
        }
        public async Task< IActionResult> Details(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Employee employee =await _unitOfWork.EmployeeRepo.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }
            var mappedEmployee=_mapper.Map<Employee,EmployeeViewModel>(employee);
            return View(mappedEmployee);
        }
        public async Task< IActionResult> Edit(int id)
        {
            ViewBag.Departments =await _unitOfWork.DepartmentRepo.GetAll();
            Employee employee =await _unitOfWork.EmployeeRepo.GetById(id);
            var mappedEmp = _mapper.Map<Employee,EmployeeViewModel>(employee);
            return View(mappedEmp);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Edit([FromRoute] int id, EmployeeViewModel employeeVm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    employeeVm.ImageName = DocumentSettinga.UploadFile(employeeVm.Image, "Images");
                    var mappedEmp =_mapper.Map<EmployeeViewModel, Employee>(employeeVm);
                    _unitOfWork.EmployeeRepo.Update(mappedEmp);
                   await _unitOfWork.Save();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(employeeVm);

        }
        public async Task< IActionResult> Delete(int id)
        {
            Employee employee =await _unitOfWork.EmployeeRepo.GetById(id);
            var mappedEmp = _mapper.Map<Employee, EmployeeViewModel>(employee);
            if (employee == null)
            {
                return BadRequest();
            }
            return View(mappedEmp);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Delete([FromRoute] int id, EmployeeViewModel employeeVm)
        {
            try
            {
                var mappedEmployee = _mapper.Map<EmployeeViewModel,Employee> (employeeVm);
                _unitOfWork.EmployeeRepo.Delete(mappedEmployee);
               await _unitOfWork.Save();
                DocumentSettinga.DeleteFile(mappedEmployee.ImageName, "Images");
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                var mappedEmp=_mapper.Map<EmployeeViewModel,Employee>(employeeVm);
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(mappedEmp);

            }
        }
        public async Task<IActionResult> ChangeState(int id)
        {
            Employee employee=await _unitOfWork.EmployeeRepo.GetById(id);
            if(employee == null)
            {
                return NotFound();
            }
            else
            {
                employee.IsActive=!employee.IsActive;
                _unitOfWork.Save();
                return Ok();
            }

        }
    }
}
