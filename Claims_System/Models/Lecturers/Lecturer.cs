namespace Claims_System.Models.Lecturers
{
    public class Lecturer
    {
        public string FullName { get; set; }   
        public int EmployeeNumber { get; set; } 
        public string Department { get; set; }   
       
        public string Submitted { get; set; } // Date of submission
        public decimal hoursWorked { get; set; }
        public decimal Rate = 500.00M;
        public string status { get; set; } // e.g. "Pending", "Approved", "Rejected"
        public string Month { get; set; }
        public double Amount { get; set; }

        public double CalculateAmount()
        {
            return (double)(hoursWorked * Rate);
        }
        public string CoordinatorStatus { get; set; } 
        public string ManagerStatus { get; set; } 

    }
}

