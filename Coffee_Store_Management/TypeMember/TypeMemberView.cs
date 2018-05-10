using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee_Store_Management
{
    class TypeMemberView
    {
        private Intro intro;
        private TypeMemberManagement type_control;

        public TypeMemberView(Intro intro)
        {
            this.intro = intro;
            intro.OnCallTypeMemberFunction += TypeMemberFunction;
        }

        public void Init(TypeMemberManagement type_control)
        {
            this.type_control = type_control;
        }

        public void TypeMemberFunction()
        {
            while (true)
            {
                Console.Clear();
                ConsoleKeyInfo choice;
                Console.OutputEncoding = Encoding.UTF8;
                Console.WriteLine("Nhấn phím số tương ứng để chọn chức năng:");
                Console.WriteLine("\t1.\tTạo thông tin Loại thành viên");
                Console.WriteLine("\t2.\tXem danh sách Loại thành viên");
                Console.WriteLine("\t3.\tSửa thông tin Loại thành viên");
                Console.WriteLine("\t4.\tXóa thông tin Loại thành viên");
                Console.WriteLine("\t6.\tQuay lại Intro");
                choice = Console.ReadKey(true);
                Console.Clear();
                switch (choice.KeyChar)
                {
                    case '1':
                        Console.Write("\nTạo thông tin Loại thành viên\n");
                        InsertTypeMember();
                        break;
                    case '2':
                        Console.Write("\nXem danh sách Loại thành viên\n");
                        type_control.PrintListTypeMember(true);
                        Console.WriteLine("\nNhấn phím bất kì để thoát");
                        Console.ReadKey();
                        break;
                    case '3':
                        Console.Write("\nSửa thông tin Loại thành viên\n");
                        break;
                    case '4':
                        Console.Write("\nXóa thông tin Loại thành viên\n");
                        break;
                    case '6':
                        intro.ShowListFunction();
                        break;
                    default:
                        Console.WriteLine("Bạn đã nhập sai. Vui lòng nhập lại");
                        continue;
                }
            }
        }

        public void InsertTypeMember()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.Write("Nhập tên:  ");
            string name = Console.ReadLine();

            Console.Write("Nhập tỷ lệ giảm giá:  ");
            double discountrate = double.Parse(Console.ReadLine());

            Console.Write("Nhập số điểm tích lũy yêu cầu để thành viên được thăng cấp: ");
            double requirepoint = double.Parse(Console.ReadLine());

            type_control.CreateTypeMember(name, discountrate, requirepoint);

            Console.WriteLine("Nhấn enter để tạo tiếp ...");
            ConsoleKeyInfo choice3 = Console.ReadKey(true);
            if (choice3.Key == ConsoleKey.Enter) InsertTypeMember();
            else
            {
                Console.Clear();
                intro.ShowListFunction();
            }
        }

        public void PrintListTypeMember(List<TypeMember> listTypeMember, bool isAdmin)
        {
            if (isAdmin)
            {
                Console.WriteLine("Số thứ tự\t"+ Ultils.FormatText("ID", 5)+ Ultils.FormatText("Loại thành viên", 20) + Ultils.FormatText("Tỷ lệ giảm giá", 20)+Ultils.FormatText("Điểm tích lũy yêu cầu", 10));
                Console.Write("---------------------------------------------------------------------------------------------------\n");

                for (int i = 0; i < listTypeMember.Count; i++)
                {
                    Console.WriteLine("\t"+(i+1)+"\t" + Ultils.FormatText(listTypeMember[i].idType.ToString(), 5) + Ultils.FormatText(listTypeMember[i].nameType, 25) + Ultils.FormatText(listTypeMember[i].discountrate.ToString(), 20) + Ultils.FormatText(listTypeMember[i].requiredpoint.ToString(), 10));
                }
                Console.Write("---------------------------------------------------------------------------------------------------\n");
            }
            else
            {
                Console.WriteLine("Số thứ tự\t" + Ultils.FormatText("ID", 5) + Ultils.FormatText("Loại thành viên", 10) + Ultils.FormatText("Tỷ lệ giảm giá", 10) + Ultils.FormatText("Điểm tích lũy yêu cầu", 10));
                for (int i = 0; i < listTypeMember.Count; i++)
                {
                    Console.WriteLine((i + 1) + "\t" + Ultils.FormatText(listTypeMember[i].idType.ToString(), 5) + Ultils.FormatText(listTypeMember[i].nameType, 10) + Ultils.FormatText(listTypeMember[i].requiredpoint.ToString(), 10) + Ultils.FormatText(listTypeMember[i].requiredpoint.ToString(), 10));
                }
            }
        }
    }
}
