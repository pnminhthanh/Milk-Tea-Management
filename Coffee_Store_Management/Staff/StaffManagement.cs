using System;
using System.Collections.Generic;

namespace Coffee_Store_Management
{
    class StaffManagement
    {
        private StaffView view_staff;
        private DepartmentManagement departmentcontrol;
        private List<StaffModel> listStaff;
        private static string path = "..\\StaffData.txt";

        public StaffManagement(Intro intro, DepartmentManagement department)
        {
            StaffView view = new StaffView(intro);
            department.Init(this);
            this.view_staff = view;
            this.departmentcontrol = department;
            view_staff.Init(this);
            listStaff = new List<StaffModel>();
            view_staff.OnPrintListDepartment += departmentcontrol.PrintListDepartments;
            view_staff.OnFindDepartment += departmentcontrol.FindDepartment;
            IOMethod.Instance.ReadData<StaffModel>(path, ref listStaff);
        }

        public void CreateStaffInfo(string name, DateTime birthday, GenderType gender, int idDepartment, string CMND, int salary)
        {
            if (listStaff.Count == 0)
                StaffModel.ID = 0;
            else StaffModel.ID = listStaff[listStaff.Count - 1].idNumber;
            StaffModel staff = new StaffModel(name, birthday, gender, departmentcontrol.FindDepartment(idDepartment), CMND, salary);
            IOMethod.Instance.WriteData<StaffModel>(path, staff);
            listStaff.Add(staff);
        }

        public void PrintStaffList(bool isAdmin)
        {
            view_staff.PrintListStaffs(listStaff,isAdmin);
        }

        public StaffModel FindStaff(int idstaff)
        {
            for (int i = 0; i < listStaff.Count; i++)
            {
                if (listStaff[i].idNumber == idstaff)
                    return listStaff[i];
            }
            StaffModel staff = new StaffModel();
            return staff;
        }

        public void DeleteStaffInfo(int id)
        {
            StaffModel staff_toremoved = FindStaff(id);
            listStaff.Remove(staff_toremoved);
            IOMethod.Instance.EditData<StaffModel>(path, listStaff);
        }

        public void EditStaffInfo(int id, string name, DateTime birthday, GenderType gender, int idDepartment, string CMND, int salary)
        {
            StaffModel staff = FindStaff(id);
            DepartmentModel department = departmentcontrol.FindDepartment(idDepartment);
            int index = listStaff.IndexOf(staff);
            listStaff[index].nameStaff = name;
            listStaff[index].birthday = birthday;
            listStaff[index].gender = gender;
            listStaff[index].department = department;
            listStaff[index].CMND = CMND;
            listStaff[index].salary = salary;
            IOMethod.Instance.EditData<StaffModel>(path, listStaff);
        }

        public void FilterStaffByDepartment(int idDepartment)
        {
            for (int i = 0; i < listStaff.Count; i++) 
            {
                if (listStaff[i].department.idDepartment == departmentcontrol.FindDepartment(idDepartment).idDepartment)
                    view_staff.PrintStaffInfo(listStaff[i]);
            }
        }

        public void FilterStaff(StaffModel input)
        {
            foreach (var staff in listStaff)
            {
                if (input.nameStaff != null)
                {
                    if (staff.nameStaff != input.nameStaff) 
                        continue;
                }
                if (input.birthday != default(DateTime))
                {
                    if (staff.birthday.Year != input.birthday.Year) 
                        if(staff.birthday.Month != input.birthday.Month)
                        continue;
                }
                if (input.gender != GenderType.Khong)
                {
                    if (staff.gender != input.gender)
                        continue;
                }
                if (input.department != default(DepartmentModel))
                {
                    if (staff.department.idDepartment != input.department.idDepartment)
                        continue;
                }
                if (input.CMND != null) 
                {
                    if (staff.CMND != input.CMND)
                        continue;
                }
                if (input.salary != 0)
                {
                    if (staff.salary != input.salary)
                        continue;
                }
                view_staff.PrintStaffInfo(staff);
            }
        }
    }
}
