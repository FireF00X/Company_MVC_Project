using MahasDemo.DAL.Data.Model;
using MahasDemo.LL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MahasDemo.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;

        public DepartmentController(IUnitOfWork unitOfWork, IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _env = env;
        }
        public IActionResult Index()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll();
            return View(departments);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Department department)
        {
            if(!ModelState.IsValid)
            {
                return View(department);
            }
            try
            {
                _unitOfWork.DepartmentRepository.Add(department);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                if(_env.IsDevelopment())
                {
                    ModelState.AddModelError("",ex.Message);
                }
                else
                {
                    ModelState.AddModelError("", "An Error Has Been Occured");
                }
                return View(department);
            }
        }
        public IActionResult Details (int? id,string viewName = "Details")
        {
            if(!id.HasValue)
            {
                return BadRequest();
            }

            var dept =_unitOfWork.DepartmentRepository.GetById(id.Value);

            if(dept is  null) 
                return NotFound();

            return View(viewName,dept);

        }
        public IActionResult Edit(int? id)
        {
            return Details(id, "Edit");
        }
        [HttpPost]
        public IActionResult Edit([FromRoute]int id ,Department department)
        {
            if(id != department.Id)
            {
                return NotFound();
            }
            else if(ModelState.IsValid)
            {
                _unitOfWork.DepartmentRepository.Update(department);
                return RedirectToAction("Index");
            }
            return View(department);
        }
        public IActionResult Delete(int id)
        {
            var dept = _unitOfWork.DepartmentRepository.GetById(id);
            _unitOfWork.DepartmentRepository.Delete(dept);
            return RedirectToAction("Index");
        }
    }
}
