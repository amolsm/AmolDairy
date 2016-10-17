using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    
    public class Employee
    {
        public int EmployeeId { get; set; }
        [Required]
        [DisplayName("Employee Code")]
        public string EmployeeCode { get; set; }
        [Required]
        [DisplayName("Name")]
        public string EmployeeName { get; set; }
        [Required]
        public bool IsActive { get; set; }


        //public string FatherName { get; set; }
        //public string PfNumber { get; set; }
        //[DataType(DataType.Date)]
        //public DateTime JoiningDate { get; set; }

        //public string Address1 { get; set; }
        //public string Address2 { get; set; }
        //public string Address3 { get; set; }
        //public string Email  { get; set; }

        //public string Mobile { get; set; }
        //public string Phone { get; set; }


        //public int LocationId { get; set; }
        //public string AccountNo { get; set; }
        //public string BankName { get; set; }
        //public string IfscCode { get; set; }
        //public int DesignationId { get; set; }
        //public int DepartmentId { get; set; }

        //public int CreatedBy { get; set; }
        //public DateTime CreatedDate { get; set; }
        //public DateTime ModifiedDate { get; set; }



    }
}
