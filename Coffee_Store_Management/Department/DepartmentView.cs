using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee_Store_Management
{
    class DepartmentView
    {
        private Intro intro;
        private DepartmentManagement de_control;

        public DepartmentView(Intro intro)
        {
            this.intro = intro;
            intro.OnCallDepartmentFunction += ListDepartmentFunction;
        }

        public void Init(DepartmentManagement de_control)
        {
            this.de_control = de_control;
        }

        public void ListDepartmentFunction()
        {
            while (true)
            {
                Console.Clear();
                ConsoleKeyInfo choice;
                Console.OutputEncoding = Encoding.UTF8;
                Console.WriteLine("Nhấn phím số tương ứng để chọn chức năng:");
                Console.WriteLine("\t1.\tNhập thông tin các bộ phận");
                Console.WriteLine("\t2.\tIn danh sách thông tin các bộ phận");
                Console.WriteLine("\t3.\tXóa thông tin bộ phận");
                Console.WriteLine("\t4.\tSửa thông tin bộ phận");
                Console.WriteLine("\t5.\tQuay lại Intro");
                choice = Console.ReadKey(true);
                Console.Clear();
                switch (choice.KeyChar)
                {
                    case '1':
                        Console.WriteLine("\nBạn chọn Nhập thông tin các bộ phận");
                        InsertDepartmentInfo();
                        break;
                    case '2':
                        Console.WriteLine("\nBạn chọn In danh sách thông tin các bộ phận");
                        de_control.PrintListDepartments(true);
                        Console.WriteLine("\nNhấn phím bất kì để thoát");
                        Console.ReadKey();
                        break;
                    case '3':
                        Console.WriteLine("\nBạn chọn Xóa thông tin bộ phận");
                        DeleteDepartment();
                        break;
                    case '4':
                        Console.WriteLine("\nBạn chọn Sửa thông tin bộ phận");
                        EditDepartment();
                        break;
                    case '5':
                        Console.Clear();
                        intro.ShowListFunction();
                        break;
                    default:
                        Console.WriteLine("Bạn đã nhập sai. Vui lòng nhập lại");
                        continue;
                }
            }
        }

        public void InsertDepartmentInfo()
        {
            Console.Write("Nhập tên bộ phận:\t");
            string name = Console.ReadLine();
            Console.Write("Nhập số lượng thành viên:\t");
            int numbermember = Int32.Parse(Console.ReadLine());
            //Console.Write("Nhập mã nhà quản lý:\t");
            //int idmanager = Int32.Parse(Console.ReadLine());
            de_control.CreateDepartment(name, numbermember/*, idmanager*/);

            Console.WriteLine("Nhấn enter để nhập tiếp...");
            ConsoleKeyInfo choice = Console.ReadKey(true);
            if (choice.Key == ConsoleKey.Enter) InsertDepartmentInfo();
            else
            {
                Console.Clear();
                intro.ShowListFunction();
            }
        }

        public void PrintDepartment(DepartmentModel department)
        {
            Console.WriteLine("\nMã bộ phận:\t" + department.idDepartment + "\nBộ phận:\t" + department.nameDepartment /*+ "\nMã trưởng phòng:\t" + department.manager.idNumber*/ + "\nSố lượng thành viên:\t" + department.numberStaff);           
        }

        public void PrintListDepartments(List<DepartmentModel> listDepartments, bool isAdmin)
        {
            if (isAdmin)
            {
                for (int i = 0; i < listDepartments.Count; i++)
                {
                    Console.WriteLine("\nMã bộ phận:\t" + listDepartments[i].idDepartment + "\nBộ phận:\t" + listDepartments[i].nameDepartment /*+ "\nMã trưởng phòng:\t" + listDepartments[i].manager.idNumber*/ + "\nSố lượng thành viên:\t" + listDepartments[i].numberStaff);
                }
            }
            else
            {
                Console.WriteLine(Ultils.FormatText("ID", 10) + "Tên bộ phận");
                for (int i = 0; i < listDepartments.Count; i++)
                {
                    Console.WriteLine(Ultils.FormatText(listDepartments[i].idDepartment.ToString(), 10) + Ultils.FormatText(listDepartments[i].nameDepartment));
                }
            }
        }

        public void DeleteDepartment()
        {
            int id;
            while (true)
            {
                de_control.PrintListDepartments(true);
                for (int i = 0; i < 150; i++)
                    Console.Write("-");
                Console.Write("\nNhập ID bộ phận muốn xóa: ");
                id = Int32.Parse(Console.ReadLine());
                if (de_control.FindDepartment(id).idDepartment == 0)
                {
                    Console.WriteLine("ID sai. Mời nhập lại");
                    continue;
                }

                PrintDepartment(de_control.FindDepartment(id));
                Console.WriteLine("\nNhấn enter để xóa.....");
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine("Đã xóa thành công...");
                    System.Threading.Thread.Sleep(500);
                    break;
                }
            }
            de_control.DeleteDepartmentInfo(id);
        }

        public void EditDepartment()
        {
            de_control.PrintListDepartments(true);
            for (int i = 0; i < 150; i++)
                Console.Write("-");
            Console.Write("\nNhập mã bộ phận muốn sửa: ");
            int id = Int32.Parse(Console.ReadLine());
            Console.Write("Nhập tên bộ phận:\t");
            string name = Console.ReadLine();
            Console.Write("Nhập số lượng thành viên:\t");
            int numbermember = Int32.Parse(Console.ReadLine());
            //Console.Write("Nhập mã nhà quản lý:\t");
            //int idmanager = Int32.Parse(Console.ReadLine());
            
            Console.WriteLine("Đã sửa thành công...");
            System.Threading.Thread.Sleep(1000);

            de_control.EditDepartmentInfo(id, name, numbermember/*, idmanager*/);
        }
    } 
}
