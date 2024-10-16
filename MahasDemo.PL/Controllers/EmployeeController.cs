using AutoMapper;
using MahasDemo.DAL.Data.Model;
using MahasDemo.LL.Interfaces;
using MahasDemo.PL.Models.EmployeeViews;
using MahasDemo.PL.Models.helper;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace MahasDemo.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;

        public EmployeeController(IUnitOfWork unitOfWork, IWebHostEnvironment env,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _env = env;
            _mapper = mapper;
        }
        public IActionResult Index(string SearchInput)
        {
            dynamic employees;
            if (string.IsNullOrEmpty(SearchInput))
            {
                employees = _unitOfWork.EmployeeRepository.GetAll();
            }
            else
            {
                employees = _unitOfWork.EmployeeRepository.GetEmployeeByName(SearchInput);
            }
            var empVM = _mapper.Map<IEnumerable<Employee>,IEnumerable<EmployeeViewModel>>(employees);
            return View(empVM);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeViewModel empVM)
            {
            if (!ModelState.IsValid)
            {
                return View(empVM);
            }
            empVM.ImageName = DocumentSettings.UploadFile(empVM.ImageFile, "Images");
            var emp = _mapper.Map<EmployeeViewModel,Employee>(empVM);
            try
            {
                _unitOfWork.EmployeeRepository.Add(emp);
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                {
                    ModelState.AddModelError("", ex.Message);
                }
                else
                {
                    ModelState.AddModelError("", "An Error Occured while Creating");
                }
                return View(empVM);
            }
        }
        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (id is null)
                return BadRequest();

            var emp = _unitOfWork.EmployeeRepository.GetById(id.Value);

            if (emp == null)
                return NotFound();
            var empVM = _mapper.Map<Employee,EmployeeViewModel>(emp);
            return View(viewName, empVM);
        }
        public IActionResult Edit(int? id)
        {
            return Details(id, "Edit");
        }
        [HttpPost]
        public IActionResult Edit([FromRoute] int id, EmployeeViewModel empVM)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            empVM.ImageName = DocumentSettings.UploadFile(empVM.ImageFile, "Images");
            var emp = _mapper.Map<EmployeeViewModel,Employee>(empVM);
            if (id != emp.Id)
                return NotFound();
            try
            {
                _unitOfWork.EmployeeRepository.Update(emp);
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                {
                    ModelState.AddModelError("", ex.Message);
                }
                else
                {
                    ModelState.AddModelError("", "An Error Occured while Creating");
                }
                return View(emp);
            }
        }
        public IActionResult Delete(int? id)
        {
            if(id is null)
                return BadRequest();

            var emp = _unitOfWork.EmployeeRepository.GetById(id.Value);

            if(emp == null)
                return NotFound();

            try
            {
                DocumentSettings.DeleteFile(emp.ImageName, "Images");
                _unitOfWork.EmployeeRepository.Delete(emp);
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                {
                    ModelState.AddModelError("", ex.Message);
                }
                else
                {
                    ModelState.AddModelError("", "An Error Occured while Creating");
                }
                return View(emp);
            }
        }
    }
}
