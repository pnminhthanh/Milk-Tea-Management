using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee_Store_Management
{
    class ReceiptView
    {
        public delegate void PrintDelegate(bool isAdmin);
        public PrintDelegate  OnPrintMenu;

        private Intro intro;
        private ReceiptManagement receipt_control;

        public ReceiptView(Intro intro)
        {
            this.intro = intro;
            intro.OnCallReceiptFunction += ListReceiptFunction;
            intro.OnCallFilterReceipt += FilterReceipt;
        }

        public void Init(ReceiptManagement receipt_control)
        {
            this.receipt_control = receipt_control;
        }

        public void ListReceiptFunction()
        {
            while (true)
            {
                Console.Clear();
                Console.OutputEncoding = Encoding.UTF8;
                Console.WriteLine("Nhấn phím số tương ứng để chọn chức năng:");
                Console.WriteLine("\t1.\tTạo hóa đơn");
                Console.WriteLine("\t2.\tIn hóa đơn");
                Console.WriteLine("\t3.\tXóa hóa đơn");
                Console.WriteLine("\t4.\tSửa hóa đơn");
                Console.WriteLine("\tEsc.\tQuay lại Intro");
                ConsoleKeyInfo choice = Console.ReadKey(true);
                Console.Clear();
                switch (choice.Key)
                {
                    case ConsoleKey.D1:
                        Console.Write("\nBạn chọn Tạo hóa đơn\n");
                        OnPrintMenu.Invoke(false);
                        InsertReceipt();
                        break;
                    case ConsoleKey.D2:
                        Console.Write("\nBạn chọn In hóa đơn\n");
                        receipt_control.PrintReceiptsList(true);
                        Console.WriteLine("\nNhấn phím bất kì để thoát");
                        Console.ReadKey();
                        break;
                    case ConsoleKey.D3:
                        Console.WriteLine("\nBạn chọn Xóa hóa đơn\n");
                        DeleteReceipt();
                        break;
                    case ConsoleKey.D4:
                        Console.WriteLine("\nBạn chọn Sửa hóa đơn\n");
                        EditReceipt();
                        break;
                    case ConsoleKey.Escape:
                        Console.Clear();
                        intro.ShowListFunction();
                        break;
                    default:
                        Console.WriteLine("Bạn đã nhập sai. Vui lòng nhập lại");
                        continue;
                }
            }
        }

        public void InsertReceipt()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.Write("Nhập ID sản phẩm: ");
            int idProduct = Int32.Parse(Console.ReadLine());

            Console.Write("Nhập số lượng: ");
            int qtyProduct = Int32.Parse(Console.ReadLine());

            receipt_control.CreateReceiptDetailInfo(idProduct, qtyProduct);

            Console.WriteLine("Nhấn Spacebar để thêm, nhấn Enter để tạo hóa đơn");
            ConsoleKeyInfo choice = Console.ReadKey(true);
            if (choice.Key == ConsoleKey.Spacebar) InsertReceipt();
            else if (choice.Key == ConsoleKey.Enter)
            {
                Console.Write("Nhập ID nhân viên: ");
                int idStaff = Int32.Parse(Console.ReadLine());

                Console.Write("Nhập ID thành viên: ");
                int idMember = Int32.Parse(Console.ReadLine());

                receipt_control.CreateReceiptInfo(idMember, idStaff);
            }
            else
            {
                Console.Clear();
                intro.ShowListFunction();
            }
        }

        public void PrintListReceiptDetail(List<ReceiptDetail> listReceiptDetail, bool isAdmin = true)
        {
            Console.WriteLine("{0}\t{1}{2}{3}{4}", "Số thứ tự", Ultils.FormatText("Mã hóa đơn", 20), Ultils.FormatText("Tên sản phẩm", 20), Ultils.FormatText("Nhân viên xử lý", 20), Ultils.FormatText("Tổng tiền".ToString(), 10));
            Console.Write("---------------------------------------------------------------------------------------------------\n");
            for (int i = 0; i < listReceiptDetail.Count; i++)
            {
                Console.WriteLine("{0}.\t\t{1}{2}{3}{4}", (i + 1), Ultils.FormatText(listReceiptDetail[i].idRecept.ToString(), 20), Ultils.FormatText(listReceiptDetail[i].product.nameProduct, 20), Ultils.FormatText(listReceiptDetail[i].staff.nameStaff, 20), Ultils.FormatText(listReceiptDetail[i].total.ToString(), 10));
            }
        }

        public void PrintReceipt(Receipt receipt)
        {
            Console.WriteLine("{0}\t{1}{2}{3}", "Số thứ tự", Ultils.FormatText("Tên sản phẩm", 20), Ultils.FormatText("Số lượng", 20), Ultils.FormatText("Tổng tiền".ToString(), 10));
            Console.Write("---------------------------------------------------------------------------------------------------\n");
            for (int i=0; i < receipt.listReceiptDetail.Count; i++)
            {
                Console.WriteLine("{0}\t\t{1}{2}{3}", (i + 1), Ultils.FormatText(receipt.listReceiptDetail[i].product.nameProduct, 20), Ultils.FormatText(receipt.listReceiptDetail[i].qtyProduct.ToString(), 20),Ultils.FormatText(receipt.listReceiptDetail[i].total.ToString(), 10));
            }
            Console.Write("---------------------------------------------------------------------------------------------------\n");
            Console.Write("Nhân viên xử lý: {0}Tổng tiền:{1}\n", Ultils.FormatText(receipt.staff.nameStaff, 20), Ultils.FormatText(receipt.total.ToString()));
            Console.Write("Ngày tạo hóa đơn: " + receipt.date.ToLongDateString());
            Console.ReadKey();
        }

        public void PrintListReceipt(List<Receipt> listReceipts, bool isAdmin)
        {
            Console.WriteLine("STT\t" + Ultils.FormatText("Mã hóa đơn", 15) + Ultils.FormatText("Tổng tiền", 20) + Ultils.FormatText("NV tạo HD", 20) + Ultils.FormatText("Tên thành viên"));

            if (isAdmin)
                for (int i = 0; i < listReceipts.Count; i++)
                {
                    Console.WriteLine((i + 1) +"\t" + Ultils.FormatText(listReceipts[i].idReceipt.ToString(), 15) + Ultils.FormatText(listReceipts[i].total.ToString(), 20) + Ultils.FormatText(listReceipts[i].staff.nameStaff, 20) + Ultils.FormatText(listReceipts[i].Member.nameMember));
                }       
        }

        public void DeleteReceipt()
        {
            int id;
            while (true)
            {
                receipt_control.PrintReceiptsList(true);
                Console.Write("---------------------------------------------------------------------------------------------------\n");
                Console.Write("\nNhập ID hóa đơn muốn xóa: ");
                id = Int32.Parse(Console.ReadLine());
                if (receipt_control.FindReceipt(id).idRecept == 0)
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
        }

        public void EditReceipt()
        {
            receipt_control.PrintReceiptsList(true);
            Console.Write("---------------------------------------------------------------------------------------------------\n");
            Console.Write("\nNhập mã hóa đơn muốn sửa: ");
            int id = Int32.Parse(Console.ReadLine());

            Console.OutputEncoding = Encoding.UTF8;
            Console.Write("Nhập ID nhân viên: ");
            int idStaff = Int32.Parse(Console.ReadLine());

            Console.Write("Nhập ID sản phẩm: ");
            int idProduct = Int32.Parse(Console.ReadLine());

            Console.Write("Nhập số lượng: ");
            int qtyProduct = Int32.Parse(Console.ReadLine());

            Console.WriteLine("Đã sửa thành công...");
            System.Threading.Thread.Sleep(1000);

            receipt_control.EditReceipt(id, idStaff, idProduct, qtyProduct);
        }

        public void FilterReceipt()
        {
            Console.Clear();
            int up_Limit = 0, down_Limit = 0;
            DateTime Date = DateTime.Now.Date;/*up_Date, down_Date;*/
            Console.WriteLine("Lọc theo tổng giá tiền và thời gian");
            Console.WriteLine("Chọn khoảng tìm TỔNG GIÁ TIỀN:\t1.Lớn hơn mức giá\t\t2.Nhỏ hơn mức giá\t\t3.Tìm trong khoảng định mức");
            ConsoleKeyInfo choice = Console.ReadKey(true);
            switch (choice.KeyChar)
            {
                case '1':
                    Console.Write("Nhập điểm: ");
                    down_Limit = Int32.Parse(Console.ReadLine());
                    up_Limit = 1000000000;
                    break;
                case '2':
                    Console.Write("Nhập điểm: ");
                    up_Limit = Int32.Parse(Console.ReadLine());
                    down_Limit = 0;
                    break;
                case '3':
                    Console.Write("Từ: ");
                    down_Limit = Int32.Parse(Console.ReadLine());
                    Console.Write("Đến: ");
                    up_Limit = Int32.Parse(Console.ReadLine());
                    break;
            }
            Console.WriteLine("Chọn cách tìm:\t1.Tìm theo ngày\t2.Khoảng thời gian");
            choice = Console.ReadKey(true);
            switch (choice.KeyChar)
            {
                case '1':
                    Console.Write("Nhập ngày (MM/dd/yyyy): ");
                    Date = DateTime.Parse(Console.ReadLine());
                    //Console.WriteLine(Date.ToLongDateString());
                    break;
                case '2':
                    Console.Write("Từ ngày: ");
                    //down_Date = DateTime.Parse(Console.ReadLine());
                    Console.Write("Đến ngày: ");
                    //up_Date = DateTime.Parse(Console.ReadLine());
                    break;
            }
            receipt_control.FilterReceipt(up_Limit, down_Limit,/* up_Date, down_Date, */Date);
            Console.WriteLine("aaa");
            Console.ReadLine();
        }
    }
}
