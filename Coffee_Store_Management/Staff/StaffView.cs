using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee_Store_Management
{
    class StaffView
    {
        public delegate void PrintDelegate(bool isAdmin);
        public PrintDelegate OnPrintListDepartment;
        public delegate DepartmentModel FindDelegate(int id);
        public FindDelegate OnFindDepartment;
        private Intro intro;
        private StaffManagement staff_control;

        public StaffView(Intro intro)
        {
            this.intro = intro;
            intro.OnCallStaffFunction += ListStaffFunction;
            intro.OnCallFilterStaff += FilterStaff;
        }

        public void Init(StaffManagement staff_control)
        {
            this.staff_control = staff_control;
        }

        public void ListStaffFunction()
        {
            while (true)
            {
                Console.Clear();
                ConsoleKeyInfo choice;
                Console.OutputEncoding = Encoding.UTF8;
                Console.WriteLine("Nhấn phím số tương ứng để chọn chức năng:");
                Console.WriteLine("\t1.\tNhập thông tin nhân viên");
                Console.WriteLine("\t2.\tIn danh sách nhân viên");
                Console.WriteLine("\t3.\tXóa nhân viên");
                Console.WriteLine("\t4.\tChỉnh sửa danh sách nhân viên");
                Console.WriteLine("\t5.\tQuay lại Intro");
                choice = Console.ReadKey(true);
                Console.Clear();
                switch (choice.KeyChar)
                {
                    case '1':
                        Console.Write("\nNhập thông tin nhân viên\n");
                        InsertStaff();
                        break;
                    case '2':
                        Console.Write("\nDanh sách nhân viên\n");
                        staff_control.PrintStaffList(true);
                        Console.WriteLine("\nNhấn phím bất kì để thoát");
                        Console.ReadKey();
                        break;
                    case '3':
                        Console.Write("\nXóa thông tin nhân viên\n");
                        DeleteStaff();
                        break;
                    case '4':
                        Console.Write("\nChỉnh sửa thông tin nhân viên\n");
                        EditStaff();
                        break;
                    case '5':
                        intro.ShowListFunction();
                        break;
                    default:
                        Console.WriteLine("Bạn đã nhập sai. Vui lòng nhập lại");
                        continue;
                }
            }
        }
        public void InsertStaff()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.Write("Nhập tên:  ");
            string name = Console.ReadLine();

            Console.Write("Nhập ngày sinh((MM/dd/yyyy): ");
            string date = Console.ReadLine();
            DateTime birthday = default(DateTime);
            if (date != "") 
            {
                birthday = DateTime.Parse(date);
            }

            Console.WriteLine("Giới tính: \t1.Nữ \t2.Nam");
            GenderType gender;

            ConsoleKeyInfo choice = Console.ReadKey(true);
            if (choice.KeyChar == '1')
            {
                gender = GenderType.Nu;
            }
            else if (choice.KeyChar == '2')
            {
                gender = GenderType.Nam;
            }
            else
            {
                gender = GenderType.Khong;
            }
            
            Console.WriteLine("Danh sách các bộ phận:");
            OnPrintListDepartment.Invoke(false);
            Console.Write("\nNhập ID bộ phận: ");
            int idjob = Int32.Parse(Console.ReadLine());

            Console.Write("Nhập số CMND: ");
            string CMND = Console.ReadLine();

            Console.Write("Nhập tiền lương của nhân viên: ");
            int salary = Int32.Parse(Console.ReadLine());

            staff_control.CreateStaffInfo(name, birthday, gender, idjob, CMND, salary);
            Console.WriteLine("Nhấn enter để nhập tiếp ...");
            ConsoleKeyInfo choice3 = Console.ReadKey(true);
            if (choice3.Key == ConsoleKey.Enter) InsertStaff();
            else
            {
                Console.Clear();
                intro.ShowListFunction();
            }
        }

        public void PrintStaffInfo(StaffModel staff)
        {
            Console.WriteLine("{0}{1}{2}{3}{4}{5}{6}", Ultils.FormatText(staff.idNumber.ToString(), 5), Ultils.FormatText(staff.nameStaff, 20), Ultils.FormatText(staff.department.nameDepartment, 15), Ultils.FormatText(staff.gender.ToString(), 10), Ultils.FormatText(staff.birthday.ToLongDateString(), 20),  Ultils.FormatText(staff.CMND, 20), Ultils.FormatText(staff.salary.ToString(), 10));
        }

        public void PrintListStaffs(List<StaffModel> listStaff, bool isAdmin)
        {
            Console.SetCursorPosition(30, 2);
            Console.Write("DANH SÁCH NHÂN VIÊN\n");
            if(isAdmin)
                Console.WriteLine("{0}\t{1}{2}{3}{4}{5}{6}{7}", "STT", Ultils.FormatText("ID", 5), Ultils.FormatText("Tên", 20), Ultils.FormatText("Bộ phận", 15), Ultils.FormatText("Giới tính", 10), Ultils.FormatText("Ngày sinh", 20), Ultils.FormatText("CMND", 10), "Tiền lương".ToString());
            else Console.WriteLine("{0}\t{1}{2}{3}{4}{5}{6}", "STT", Ultils.FormatText("ID", 5), Ultils.FormatText("Tên", 20), Ultils.FormatText("Bộ phận", 15), Ultils.FormatText("Giới tính", 10), Ultils.FormatText("Ngày sinh", 20), Ultils.FormatText("CMND", 10));
            Console.Write("---------------------------------------------------------------------------------------------------\n");

            for (int i = 0; i < listStaff.Count; i++)
            {
                if (isAdmin)
                    Console.WriteLine("{0}.\t{1}{2}{3}{4}{5}{6}{7}", (i + 1), Ultils.FormatText(listStaff[i].idNumber.ToString(), 5), Ultils.FormatText(listStaff[i].nameStaff, 20), Ultils.FormatText(listStaff[i].department.nameDepartment, 18), Ultils.FormatText(listStaff[i].gender.ToString(), 7), Ultils.FormatText(listStaff[i].birthday.ToLongDateString(), 20),Ultils.FormatText(listStaff[i].CMND.ToString(),10),listStaff[i].salary.ToString());
                else Console.WriteLine("{0}.\t{1}{2}{3}{4}{5}{6}", (i + 1), Ultils.FormatText(listStaff[i].idNumber.ToString(), 5), Ultils.FormatText(listStaff[i].nameStaff, 20), Ultils.FormatText(listStaff[i].department.nameDepartment, 18), Ultils.FormatText(listStaff[i].gender.ToString(), 7), Ultils.FormatText(listStaff[i].birthday.ToLongDateString(), 20));
            }
            Console.Write("---------------------------------------------------------------------------------------------------\n");
        }

        public void DeleteStaff()
        {
            int id;
            while (true)
            {
                staff_control.PrintStaffList(true);
                Console.Write("\n");
                for (int i = 0; i < 150; i++)
                    Console.Write("\nNhập ID nhân viên muốn xóa: ");
                id = Int32.Parse(Console.ReadLine());
                if(staff_control.FindStaff(id).idNumber == 0)
                {
                    Console.WriteLine("ID sai. Mời nhập lại");
                    continue;
                }

                PrintStaffInfo(staff_control.FindStaff(id));
                Console.WriteLine("\nNhấn enter để xóa.....");
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine("Đã xóa thành công...");
                    System.Threading.Thread.Sleep(500);
                    break;
                }
            }
            staff_control.DeleteStaffInfo(id);
        }

        public void EditStaff()
        {
            staff_control.PrintStaffList(true);
            Console.Write("\n");
            for (int i = 0; i < 150; i++)
                Console.Write("\nNhập ID thành viên muốn sửa: ");
            int id = Int32.Parse(Console.ReadLine());

            Console.OutputEncoding = Encoding.UTF8;
            Console.Write("Nhập tên:  ");
            string name = Console.ReadLine();

            Console.Write("Nhập ngày sinh(MM/dd/yyyy): ");
            DateTime birthday = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Giới tính: \t1.Nữ \t2.Nam");
            GenderType gender;
            while (true)
            {
                ConsoleKeyInfo choice = Console.ReadKey(true);
                if (choice.KeyChar == '1')
                {
                    gender = GenderType.Nu;
                    break;
                }
                else if (choice.KeyChar == '2')
                {
                    gender = GenderType.Nam;
                    break;
                }
                else
                {
                    Console.WriteLine("Nhập sai mời nhập lại!");                  
                }
            }
            Console.WriteLine("Danh sách các bộ phận:");
            OnPrintListDepartment.Invoke(false);
            Console.Write("\nNhập ID bộ phận: ");
            int iddepartment = Int32.Parse(Console.ReadLine());

            Console.Write("Nhập số CMND: ");
            string CMND = Console.ReadLine();

            Console.Write("Nhập tiền lương của nhân viên: ");
            int salary = Int32.Parse(Console.ReadLine());

            staff_control.EditStaffInfo(id, name, birthday, gender, iddepartment, CMND, salary);
        }

        public void FilterStaffByDepartment()
        {
            Console.Clear();
            OnPrintListDepartment.Invoke(false);
            Console.Write("Nhập ID bộ phận: ");
            int id = Int16.Parse(Console.ReadLine());
            staff_control.FilterStaffByDepartment(id);
            Console.ReadKey();
        }

        public void FilterStaff()
        {
            Console.Clear();
            Console.WriteLine("Hướng dẫn sử dụng: Nhấn phím số tương ứng để chọn thuộc tính. Nhấn SpaceBar để bắt đầu lọc");
            StaffModel input = new StaffModel();
            for (int i = 0; i < 150; i++)
                Console.Write("-");
            Console.Write("\n");
            Console.WriteLine("1.Tên\t2.Ngày sinh\t3.Giới tính\t4.Bộ phận\t5.Lương\n");
            for (int i = 0; i < 150; i++)
                Console.Write("-");

            while (true)
            {
                ConsoleKeyInfo choice = Console.ReadKey(true);

                switch (choice.KeyChar)
                {
                    case '1':
                        {
                            Console.WriteLine("LỌC THEO TÊN");
                            Console.Write("Nhập tên cần lọc: ");
                            string name = Console.ReadLine();
                            input.nameStaff = name;
                            break;
                        }
                    case '2':
                        {
                            Console.WriteLine("LỌC NGÀY SINH THEO:\t1.Tháng\t\t2.Năm");
                            choice = Console.ReadKey(true);
                            if (choice.KeyChar == '1')
                            {
                                Console.Write("Nhập tháng: ");
                                string month = Console.ReadLine();
                                input.birthday = DateTime.Parse("01/" + month + "/0001");
                            }
                            if (choice.KeyChar == '2')
                            {
                                Console.Write("Nhập năm: ");
                                string year = Console.ReadLine();
                                input.birthday = DateTime.Parse("01/01/" + year);
                            }
                            break;
                        }
                    case '3':
                        {
                            Console.WriteLine("LỌC GIỚI TÍNH THEO:\t1.Nam\t\t2.Nữ");
                            lap:
                            choice = Console.ReadKey(true);                            
                            if (choice.KeyChar == '1')
                            {
                                input.gender = GenderType.Nam;
                                Console.WriteLine("Đã chọn giới tính Nam");
                                break;
                            }
                            else if (choice.KeyChar == '2')
                            {
                                input.gender = GenderType.Nu;
                                Console.WriteLine("Đã chọn giới tính Nữ");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Nhập sai mời nhập lại!");
                                goto lap;
                            }                           
                        }
                    case '4':
                        {
                            Console.WriteLine("LỌC THEO BỘ PHẬN");
                            OnPrintListDepartment.Invoke(false);
                            Console.Write("\nNhập ID bộ phận: ");
                            int iddepartment = Int32.Parse(Console.ReadLine());
                            input.department = OnFindDepartment.Invoke(iddepartment);
                            break;
                        }
                    case '5':
                        {
                            Console.WriteLine("LỌC THEO LƯƠNG");
                            Console.Write("\nNhập số lương: ");
                            int salary = Int32.Parse(Console.ReadLine());
                            input.salary = salary;
                            break;
                        }
                }
                if (choice.Key == ConsoleKey.Spacebar) break;
            }
            Console.Write("\n");
            for (int i = 0; i < 150; i++)
                Console.Write("-");
            Console.WriteLine("\t\t\t\tKẾT QUẢ TÌM KIẾM");
            staff_control.FilterStaff(input);
            Console.ReadKey();
        }
    }
}
