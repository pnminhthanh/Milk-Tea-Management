using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Coffee_Store_Management
{
    class DepartmentManagement
    {
        private DepartmentView department_view;
        private List<DepartmentModel> listDepartments;
        private static string path = "..\\DepartmentData.txt";
        StaffManagement staffcontrol;

        public DepartmentManagement(Intro intro)
        {
            DepartmentView view = new DepartmentView(intro);
            this.department_view = view;
            department_view.Init(this);
            listDepartments = new List<DepartmentModel>();
            IOMethod.Instance.ReadData<DepartmentModel>(path, ref listDepartments);
        }

        public void Init(StaffManagement staff)
        {
            this.staffcontrol = staff;
        }
        
        public void CreateDepartment(string namedepartment, int numberstaff/*, int idmanager*/)
        {
            if (listDepartments.Count == 0)
                DepartmentModel.ID = 0;
            else DepartmentModel.ID = listDepartments[listDepartments.Count - 1].idDepartment;
            DepartmentModel department = new DepartmentModel(namedepartment, numberstaff/*, staffcontrol.FindStaff(idmanager)*/);
            IOMethod.Instance.WriteData<DepartmentModel>(path, department);
            listDepartments.Add(department);
        }

        public DepartmentModel FindDepartment(int iddepartment)
        {
            for (int i = 0; i < listDepartments.Count; i++)
            {
                if (listDepartments[i].idDepartment == iddepartment)
                    return listDepartments[i];
            }
            DepartmentModel department = new DepartmentModel();
            return department;
        }

        public void PrintDepartment(int id)
        {
            department_view.PrintDepartment(FindDepartment(id));
        }

        public void PrintListDepartments(bool isAdmin)
        {
            department_view.PrintListDepartments(listDepartments, isAdmin);
        }

        public void DeleteDepartmentInfo(int id)
        {
            DepartmentModel department = FindDepartment(id);
            listDepartments.Remove(department);
            IOMethod.Instance.EditData<DepartmentModel>(path, listDepartments);
        }

        public void EditDepartmentInfo(int id, string namedepartment, int numberstaff/*, int idmanager*/)
        {
            int index = listDepartments.IndexOf(FindDepartment(id));
            listDepartments[index].nameDepartment = namedepartment;
            listDepartments[index].numberStaff = numberstaff;
            //listDepartments[index].manager = staffcontrol.FindStaff(idmanager);
            IOMethod.Instance.EditData<DepartmentModel>(path, listDepartments);
            department_view.PrintListDepartments(listDepartments, true);
        }
       
    }
}
