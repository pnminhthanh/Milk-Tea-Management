using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee_Store_Management
{
    class MemberView
    {
        private Intro intro;
        private MemberManagement mem_control;

        public MemberView(Intro intro)
        {
            this.intro = intro;
            intro.OnCallMemberFunction += ListMemberFunction;
            intro.OnCallFilterMember += FilterMemberByPoint;
        }

        public void Init(MemberManagement mem_control)
        {
            this.mem_control = mem_control;
        }
        public void ListMemberFunction()
        {
            while (true)
            {
                Console.Clear();
                ConsoleKeyInfo choice;
                Console.OutputEncoding = Encoding.UTF8;
                Console.WriteLine("Nhấn phím số tương ứng để chọn chức năng:");
                Console.WriteLine("\t1.\tTạo thông tin thành viên");
                Console.WriteLine("\t2.\tXem danh sách thành viên");
                Console.WriteLine("\t3.\tXóa thông tin thành viên");
                Console.WriteLine("\t4.\tSửa thông tin thành viên");
                Console.WriteLine("\t5.\tQuay lại Intro");
                choice = Console.ReadKey(true);
                Console.Clear();
                switch (choice.KeyChar)
                {
                    case '1':
                        Console.Write("\nTạo thông tin thành viên\n");
                        InsertMember();
                        break;
                    case '2':
                        Console.Write("\nXem danh sách thành viên\n");
                        mem_control.PrintListMember();
                        Console.WriteLine("\nNhấn phím bất kì để thoát");
                        Console.ReadKey();
                        break;
                    case '3':
                        Console.Write("\nXóa thông tin\n");
                        DeleteMember();
                        break;
                    case '4':
                        Console.Write("\nSửa thông tin\n");
                        EditMember();
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
        public void InsertMember()
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

            Console.Write("Nhập số CMND: ");
            string CMND = Console.ReadLine();

            mem_control.CreateMember(name, gender, birthday, CMND);

            Console.WriteLine("Nhấn enter để nhập tiếp ...");
            ConsoleKeyInfo choice3 = Console.ReadKey(true);
            if (choice3.Key == ConsoleKey.Enter) InsertMember();
            else
            {
                Console.Clear();
                intro.ShowListFunction();
            }
        }

        public void PrintMemberInfo(MemberModel member)
        {
            Console.WriteLine("{0}\t{1}{2}{3}{4}{5}{6}{7}{8}", "Số thứ tự", Ultils.FormatText("ID", 10), Ultils.FormatText("Tên thành viên", 20), Ultils.FormatText("Giới tính", 10), Ultils.FormatText("Ngày sinh"), Ultils.FormatText("Số CMND",15), Ultils.FormatText("Ngày đăng kí", 20), Ultils.FormatText("Điểm tích lũy",20),Ultils.FormatText("Loại thành viên"));
            Console.Write("\n");
            for (int i = 0; i < 150; i++)
                Console.WriteLine(Ultils.FormatText(member.idMember.ToString(), 10) + Ultils.FormatText(member.nameMember, 20) + Ultils.FormatText((member.genderMember.ToString()), 10) + Ultils.FormatText(member.birthMember.ToShortDateString(), 15), Ultils.FormatText(member.CMND.ToString()) + Ultils.FormatText(member.timeSignup.ToShortDateString(), 20) + Ultils.FormatText(member.typeMember.nameType));
        }

        public void PrintListMember(List<MemberModel> listMember, bool isAdmin = true)
        {
            if (isAdmin)
                Console.WriteLine("{0}\t{1}{2}{3}{4}{5}{6}{7}{8}", "Số thứ tự", Ultils.FormatText("ID", 10), Ultils.FormatText("Tên thành viên", 20), Ultils.FormatText("Giới tính", 10), Ultils.FormatText("Ngày sinh"), Ultils.FormatText("Số CMND", 15), Ultils.FormatText("Ngày đăng kí",20),Ultils.FormatText("Điểm tích lũy"), Ultils.FormatText("Loại thành viên"));
            for (int i = 0; i < 150; i++)
                Console.Write("-");
            for (int i = 0; i < listMember.Count; i++)
            {
                if (isAdmin)
                    Console.WriteLine("{0}.\t\t{1}{2}{3}{4}{5}{6}{7}{8}", (i + 1), Ultils.FormatText(listMember[i].idMember.ToString(), 10), Ultils.FormatText(listMember[i].nameMember, 20), Ultils.FormatText((listMember[i].genderMember.ToString()), 10), Ultils.FormatText(listMember[i].birthMember.ToShortDateString(), 15), Ultils.FormatText(listMember[i].CMND.ToString()), Ultils.FormatText(listMember[i].timeSignup.ToLongDateString(), 20),Ultils.FormatText(listMember[i].point.ToString()), Ultils.FormatText(listMember[i].typeMember.nameType));
                else Console.WriteLine("Tổng số thành viên là {0}", listMember.Count);
            }
        }

        public void DeleteMember()
        {
            int id;
            while (true)
            {
                mem_control.PrintListMember();
                Console.Write("---------------------------------------------------------------------------------------------------\n");
                Console.Write("\nNhập ID thành viên muốn xóa: ");
                id = Int32.Parse(Console.ReadLine());
                if (mem_control.FindMember(id).idMember == 0)
                {
                    Console.WriteLine("ID sai. Mời nhập lại");
                    continue;
                }
                Console.WriteLine("\nNhấn enter để xóa.....");
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine("Đã xóa thành công...");
                    System.Threading.Thread.Sleep(500);
                    break;
                }
            }
            mem_control.DeleteMemberInfo(id);
        }

        public void EditMember()
        {
            mem_control.PrintListMember();
            for (int i = 0; i < 150; i++)
                Console.Write("-");
            Console.Write("\nNhập ID thành viên muốn sửa: ");
            int id = Int32.Parse(Console.ReadLine());

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
            Console.Write("Nhập số CMND: ");
            string CMND = Console.ReadLine();

            Console.WriteLine("Đã sửa thành công...");
            System.Threading.Thread.Sleep(1000);

            mem_control.EditMemberInfo(id, name, birthday, gender, CMND);
        }

        public void FilterMemberByPoint()
        {
            Console.Clear();
            int up_limit = 0, down_limit = 0;
            Console.WriteLine("Chọn khoảng tìm kiếm:\t1.Lớn hơn điểm\t\t2.Nhỏ hơn điểm\t\t3.Tìm trong khoảng điểm");
            ConsoleKeyInfo choice = Console.ReadKey(true);
            switch (choice.KeyChar)
            {
                case '1':
                    Console.Write("Nhập điểm: ");
                    down_limit= Int32.Parse(Console.ReadLine());
                    up_limit = 1000000000;
                    mem_control.FilterMemberByPoint(up_limit, down_limit);
                    break;
                case '2':
                    Console.Write("Nhập điểm: ");
                    up_limit = Int32.Parse(Console.ReadLine());
                    down_limit = 0;
                    mem_control.FilterMemberByPoint(up_limit, down_limit);
                    break;
                case '3':
                    Console.Write("Từ: ");
                    down_limit = Int32.Parse(Console.ReadLine());
                    Console.Write("Đến: ");
                    up_limit = Int32.Parse(Console.ReadLine());
                    break;
            }
            Console.ReadKey();
        }
    }    
}
