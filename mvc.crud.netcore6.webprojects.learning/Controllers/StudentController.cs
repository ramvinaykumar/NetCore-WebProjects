using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mvc.crud.netcore6.webprojects.learning.Data;
using mvc.crud.netcore6.webprojects.learning.Models;
using mvc.crud.netcore6.webprojects.learning.Models.Domain;

namespace mvc.crud.netcore6.webprojects.learning.Controllers
{
    public class StudentController : Controller
    {
        private readonly MvcDbContext _dbContext;

        public StudentController(MvcDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEditStudentViewModel requestDto)
        {
            var newStudent = new Student
            {
                Id = Guid.NewGuid(),
                Name = requestDto.Name,
                Email = requestDto.Email,
                EnrollmentNumber = requestDto.EnrollmentNumber,
                MobileNo = requestDto.MobileNo,
                Section = requestDto.Section,
                Grade = requestDto.Grade,
                BloodGroup = requestDto.BloodGroup,
                HouseGroup = requestDto.HouseGroup,
                Dob = requestDto.DateOfBirth,
                Gender = requestDto.Gender,
            };

            await _dbContext.Students.AddAsync(newStudent);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _dbContext.Students.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var student = await _dbContext.Students.FirstOrDefaultAsync(x => x.Id == id);
            if (student != null)
            {
                var viewStudent = new UpdateStudentViewModel
                {
                    Id = student.Id,
                    Name = student.Name,
                    Email = student.Email,
                    EnrollmentNumber = student.EnrollmentNumber,
                    MobileNo = student.MobileNo,
                    Section = student.Section,
                    Grade = student.Grade,
                    BloodGroup = student.BloodGroup,
                    HouseGroup = student.HouseGroup,
                    Dob = student.Dob,
                    Gender = student.Gender,
                };
                return View(viewStudent);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateStudentViewModel requestDto)
        {
            var updateStudent = await _dbContext.Students.FindAsync(requestDto.Id);
            if (updateStudent != null)
            {
                updateStudent.Name = requestDto.Name;
                updateStudent.Email = requestDto.Email;
                updateStudent.EnrollmentNumber = requestDto.EnrollmentNumber;
                updateStudent.MobileNo = requestDto.MobileNo;
                updateStudent.Section = requestDto.Section;
                updateStudent.Grade = requestDto.Grade;
                updateStudent.BloodGroup = requestDto.BloodGroup;
                updateStudent.HouseGroup = requestDto.HouseGroup;
                updateStudent.Dob = requestDto.Dob;
                updateStudent.Gender = requestDto.Gender;

                _dbContext.Students.Update(updateStudent);
                await _dbContext.SaveChangesAsync();

            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var updateStudent = await _dbContext.Students.FindAsync(id);
            if (updateStudent != null)
            {
                _dbContext.Students.Remove(updateStudent);
                await _dbContext.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
