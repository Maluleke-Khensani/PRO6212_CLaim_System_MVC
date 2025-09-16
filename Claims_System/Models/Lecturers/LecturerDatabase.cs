using System.Collections.Generic;
using System.Linq;

namespace Claims_System.Models.Lecturers
{
    public class LecturerDatabase
    {
        private List<Lecturer> lecturers = new List<Lecturer>();


        
        public LecturerDatabase() 
        {
            lecturers.Add(new Lecturer { EmployeeNumber= 1, FullName = "Khensani Maluleke", Department = "Computer Science", status= "Rejected", CoordinatorStatus="Rejected", ManagerStatus="Rejected", Amount= 80000.00, hoursWorked = 200, Submitted= "2025-10-22",Month= "August 2025" });
            lecturers.Add(new Lecturer { EmployeeNumber = 2, FullName = "Jane Doe", Department = "Law", status = "Approved", CoordinatorStatus="Approved", ManagerStatus="Approved", Amount = 80000.00, hoursWorked = 200, Submitted = "2025-11-22", Month = "September 2025" });
            lecturers.Add(new Lecturer { EmployeeNumber = 3, FullName = "Kim Loe", Department = "Education", status = "Approved", CoordinatorStatus="Approved", ManagerStatus="Approved", Amount = 80000.00, hoursWorked = 200, Submitted = "2025-12-22", Month = "October 2025" });
            lecturers.Add(new Lecturer { EmployeeNumber = 4, FullName = "Jayden Barron", Department = "Law", status = "Under Review", CoordinatorStatus="Rejected", ManagerStatus="Approved", Amount = 80000.00, hoursWorked = 200, Submitted = "2026-01-22", Month = "November 2025" });
            lecturers.Add(new Lecturer {EmployeeNumber = 5, FullName = "Josiah Maluleke", Department = "Computer Science", status = "Pending", CoordinatorStatus="Approved", ManagerStatus="Pending",  Amount = 80000.00, hoursWorked = 200, Submitted = "2026-02-22", Month = "December 2025" });

        }

        // Method to retrieve all lecturers
        public List<Lecturer> GetAllLecturers()
        {
            return lecturers;
        }
        //total number of objects ion the list
        public int totalNumOfClaims()
        {
            int total= lecturers.Count();

            return total;

        }


        // Method to find a lecturer by EmployeeNumber
        public Lecturer GetLecturerByEmployeeNo(int employeeNumber)
        {
            return lecturers.FirstOrDefault(l => l.EmployeeNumber == employeeNumber);
        }

    }
}
