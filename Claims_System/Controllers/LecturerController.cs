using Microsoft.AspNetCore.Mvc;
using Claims_System.Models.Lecturers;

namespace Claims_System.Controllers
{
    public class LecturerController : Controller
    {
        //Created a private field to hold the instance of LecturerDatabase
        //readonly to ensure it is only set in the constructor

        private readonly LecturerDatabase _lecturerDatabase;

        public LecturerController()
        {
           
            _lecturerDatabase = new LecturerDatabase();
        }
       
        public IActionResult Index()
        {
            var lecturers = _lecturerDatabase.GetAllLecturers();

            return View(lecturers);
        }
        public IActionResult ClaimForm()
        {
            return View();
        }

        [HttpGet("Lecturer/ViewClaim/{employeeNumber}")]

        public IActionResult ViewClaim(int employeeNumber)
        {
            var lecturer = _lecturerDatabase.GetLecturerByEmployeeNo(employeeNumber);

            if (lecturer == null)
            {
                return NotFound(); // if not found, return 404
            }

            return View(lecturer); // pass lecturer object into the View
        }

        public IActionResult DeleteClaim(int employeeNumber)
        {
            var lecturer = _lecturerDatabase.GetLecturerByEmployeeNo(employeeNumber);
            if (lecturer == null)
            {
                return NotFound();
            }

            // Prototype only – no actual deletion yet
            return View(lecturer);
        }


        public IActionResult EditClaim(int employeeNumber )
        {
            var lecturer = _lecturerDatabase.GetLecturerByEmployeeNo(employeeNumber);
            if (lecturer == null)
            {
                return NotFound(); // if not found, return 404
            }
            return View(lecturer); // pass lecturer object into the View
        }

    }
}
