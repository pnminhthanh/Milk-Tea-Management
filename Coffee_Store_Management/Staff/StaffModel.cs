using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee_Store_Management
{
    class StaffModel
    {
        public string nameStaff, CMND;
        public DateTime birthday;
        public int idNumber, salary;
        public GenderType gender;
        public static int ID;
        public DepartmentModel department;

        public StaffModel()
        {
            this.gender = GenderType.Khong;
            this.department = default(DepartmentModel);
        }

        public StaffModel(string name, DateTime birthday, GenderType gender, DepartmentModel department, string CMND, int salary)
        {
            this.idNumber = ++ID;
            this.nameStaff = name;
            this.birthday = birthday;
            this.gender = gender;
            this.department = department;
            this.CMND = CMND;
            this.salary = salary;
        }
    }
}
