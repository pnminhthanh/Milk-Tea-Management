using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee_Store_Management
{
    class Intro
    {
        public delegate void CallDelegate();
        public CallDelegate OnCallMenuFunction, OnCallStaffFunction, OnCallReceiptFunction, OnCallDepartmentFunction, OnCallMemberFunction, OnCallTypeMemberFunction, OnCallFilterMember, OnCallFilterStaff, OnCallFilterReceipt;

        public Intro()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.SetWindowSize(150, 30);
            Console.SetCursorPosition(20, 1);
            Console.WriteLine("Chào mừng bạn đến với chương trình quản lý tiệm Coffee");
        }

        public void ShowListFunction()
        {
            for (int i = 0; i < 150; i++)
                Console.Write("-");
            Console.WriteLine("\t\tNhấn phím số tương ứng để chọn chức năng liên quan: ");
            Console.WriteLine("\t1.\tMenu");
            Console.WriteLine("\t2.\tCác bộ phận làm việc");
            Console.WriteLine("\t3.\tNhân viên");
            Console.WriteLine("\t4.\tHóa đơn");
            Console.WriteLine("\t5.\tThành viên");
            Console.WriteLine("\t6.\tLoại thành viên");
            Console.WriteLine("\t7.\tLọc");
            Console.WriteLine("\t8.\tExit");
            Console.Write("\n");
            for (int i = 0; i < 150; i++)
                Console.Write("-");
            Console.Write("\n");
            ChooseFunction();
        }

        public void ChooseFunction()
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            switch (key.KeyChar)
            {
                case '1':
                    {
                        Console.WriteLine("MENU");
                        OnCallMenuFunction.Invoke();
                        break;
                    }
                case '2':
                    {
                        Console.WriteLine("Các bộ phận làm việc");
                        OnCallDepartmentFunction.Invoke();
                        break;
                    }
                case '3':
                    {
                        Console.WriteLine("Nhân viên");
                        OnCallStaffFunction.Invoke();
                        break;
                    }
                case '4':
                    {
                        Console.WriteLine("Hóa đơn");
                        OnCallReceiptFunction.Invoke();
                        break;
                    }
                case '5':
                    {
                        Console.WriteLine("Thành viên");
                        OnCallMemberFunction.Invoke();
                        break;
                    }
                case '6':
                    {
                        Console.WriteLine("Loại thành viên");
                        OnCallTypeMemberFunction.Invoke();
                        break;
                    }
                case '7':
                    {
                        FilterFunction();
                        break;
                    }
                case '8':
                    {
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Nhập sai mời bạn nhập lại!!");
                        ChooseFunction();
                        break;
                    }
            }
        }

        public void FilterFunction()
        {
            Console.Clear();
            Console.WriteLine("\t1.\t Lọc thành viên theo điểm tích lũy");
            Console.WriteLine("\t2.\t Lọc nhân viên");
            Console.WriteLine("\t3.\t Lọc hóa đơn theo ngày và tháng (chưa sửa lỗi xong, đừng xài)");
            Console.WriteLine("\t4.\t Quay lại Intro");
            ChooseFilterFunction();
        }

        public void ChooseFilterFunction()
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            switch (key.KeyChar)
            {
                case '1':
                    {
                        OnCallFilterMember.Invoke();
                        break;
                    }
                case '2':
                    {
                        OnCallFilterStaff.Invoke();
                        break;
                    }
                case '3':
                    {
                        OnCallFilterReceipt.Invoke();
                        break;
                    }
                case '4':
                    {
                        Console.Clear();
                        ShowListFunction();
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Nhập sai mời bạn nhập lại!!");
                        ChooseFilterFunction();
                        break;
                    }
            }
        }
    }
}
