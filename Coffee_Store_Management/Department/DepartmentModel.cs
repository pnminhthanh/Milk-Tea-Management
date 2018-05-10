using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee_Store_Management
{
    class DepartmentModel
    {
        public int idDepartment, numberStaff;
        public string nameDepartment;
        //public StaffModel manager;
        public static int ID;

        public DepartmentModel()
        {

        }

        public DepartmentModel(string nameDepartment, int numberStaff/*, StaffModel manager*/)
        {
            this.idDepartment = ++ID;
            this.nameDepartment = nameDepartment;
            this.numberStaff = numberStaff;
            //this.manager = manager;
        }                       
    }    
}
