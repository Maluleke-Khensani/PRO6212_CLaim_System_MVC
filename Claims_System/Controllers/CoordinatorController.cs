using Claims_System.Models.Lecturers;
using Microsoft.AspNetCore.Mvc;

namespace Claims_System.Controllers
{
    public class CoordinatorController : Controller
    {
          private readonly LecturerDatabase _lecturerDatabase;

            public CoordinatorController()
            {
                _lecturerDatabase = new LecturerDatabase();
            }

            public IActionResult Index()
            {
                var lecturers = _lecturerDatabase.GetAllLecturers();
                return View(lecturers);
            }

            public IActionResult ReviewClaim2(int employeeNumber)
            {
                var claim = _lecturerDatabase.GetLecturerByEmployeeNo(employeeNumber);

                if (claim == null)
                {
                    return NotFound(); // if not found, return 404
                }

                return View(claim); // pass lecturer object into the View
            }

        }
    }

