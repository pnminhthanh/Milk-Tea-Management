using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee_Store_Management
{
    class MenuView
    {

        private Intro intro;
        private MenuManagement menu_control;

        public MenuView(Intro intro)
        {
            this.intro = intro;
            intro.OnCallMenuFunction += ListMenuFunction;
        }

        public void Init(MenuManagement menu_control)
        {
            this.menu_control = menu_control;
        }

        public void ListMenuFunction()
        {
            while (true)
            {
                Console.Clear();
                ConsoleKeyInfo choice;
                Console.OutputEncoding = Encoding.UTF8;
                Console.WriteLine("Nhấn phím số tương ứng để chọn chức năng:");
                Console.WriteLine("\t1.\tNhập menu");
                Console.WriteLine("\t2.\tIn Menu");
                Console.WriteLine("\t3.\tXóa sản phẩm trong Menu");
                Console.WriteLine("\t4.\tSửa sản phẩm trong Menu");
                Console.WriteLine("\t5.\tQuay lại Intro");
                choice = Console.ReadKey(true);
                Console.Clear();
                switch (choice.KeyChar)
                {
                    case '1':
                        Console.WriteLine("\nBạn chọn Nhập Menu");
                        InsertMenu();
                        break;
                    case '2':
                        menu_control.PrintMenu(true);
                        Console.WriteLine("\nNhấn phím bất kì để thoát");
                        Console.ReadKey();
                        break;
                    case '3':
                        Console.WriteLine("\nBạn chọn Xóa các thành phần");
                        DeleteMenu();
                        break;
                    case '4':
                        Console.WriteLine("\nBạn chọn Sửa Menu");
                        EditMenu();
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

        public void InsertMenu()
        {
            Console.Write("Nhập tên: ");
            string productName = Console.ReadLine();

            Console.Write("Nhập loại (drink nếu là nước, cake nếu là bánh): ");
            string type = Console.ReadLine();

            Console.Write("Giá gốc: ");
            int costValue = Convert.ToInt32(Console.ReadLine());

            Console.Write("Giá bán: ");
            int sellValue = Convert.ToInt32(Console.ReadLine());

            menu_control.CreateMenu(type, productName, costValue, sellValue);

            Console.WriteLine("Nhấn enter để nhập tiếp ...");
            ConsoleKeyInfo choice1 = Console.ReadKey(true);
            if (choice1.Key == ConsoleKey.Enter) InsertMenu();
            else
            {
                Console.Clear();
                intro.ShowListFunction();
            }
        }

        public void PrintProductInfo(MenuModel product)
        {
            Console.WriteLine("{0}.\t{1}{2}{3}{4}", product.idProduct.ToString(), Ultils.FormatText(product.nameProduct, 20), Ultils.FormatText(product.type, 20), Ultils.FormatText(product.sellValue.ToString(), 10), product.costValue);
        }

        public void PrintListMenuInfo(List<MenuModel> listProducts, bool isAdmin = true)
        {
            Console.SetCursorPosition(30, 2);
            Console.Write("MENU\n");
            if (isAdmin)
                Console.WriteLine("{0}\t{1}{2}{3}{4}", "Mã số" , Ultils.FormatText("Tên sản phẩm", 20), Ultils.FormatText("Loại", 20), Ultils.FormatText("Giá bán", 10), "Giá gốc");
            else Console.WriteLine("{0}\t{1}{2}{3}", "Mã số", Ultils.FormatText("Tên sản phẩm", 20), Ultils.FormatText("Loại", 20), Ultils.FormatText("Giá bán", 10));
            Console.Write("---------------------------------------------------------------------------------------------------\n");
            for (int i = 0; i < listProducts.Count; i++)
            {
                if (isAdmin)
                    Console.WriteLine("{0}.\t{1}{2}{3}{4}", listProducts[i].idProduct.ToString(), Ultils.FormatText(listProducts[i].nameProduct, 20), Ultils.FormatText(listProducts[i].type, 20), Ultils.FormatText(listProducts[i].sellValue.ToString(), 10), listProducts[i].costValue);
                else Console.WriteLine("{0}.\t{1}{2}{3}", listProducts[i].idProduct.ToString(), Ultils.FormatText(listProducts[i].nameProduct, 20), Ultils.FormatText(listProducts[i].type, 20), Ultils.FormatText(listProducts[i].sellValue.ToString(), 10));
            }
            Console.Write("---------------------------------------------------------------------------------------------------\n");

        }

        public void DeleteMenu()
        {
            int id;
            while (true)
            {
                menu_control.PrintMenu(true);
                Console.Write("---------------------------------------------------------------------------------------------------\n");
                Console.Write("\nNhập ID sản phẩm muốn xóa: ");
                id = Int32.Parse(Console.ReadLine());
                if (menu_control.FindProduct(id) == null)
                {
                    Console.WriteLine("ID sai. Mời nhập lại");
                    continue;
                }
                PrintProductInfo(menu_control.FindProduct(id));
                Console.WriteLine("\nNhấn enter để xóa.....");
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine("Đã xóa thành công...");
                    System.Threading.Thread.Sleep(500);
                    break;
                }
            }
            menu_control.DeleteProductInfo(id);
        }

        public void EditMenu()
        {
            menu_control.PrintMenu(true);
            Console.Write("---------------------------------------------------------------------------------------------------\n");

            Console.Write("Nhập mã sản phẩm: ");
            int id = Int32.Parse(Console.ReadLine());

            Console.Write("Nhập tên: ");
            string productName = Console.ReadLine();

            Console.Write("Nhập loại (drink nếu là nước, cake nếu là bánh): ");
            string type = Console.ReadLine();

            Console.Write("Giá gốc: ");
            int costValue = Convert.ToInt32(Console.ReadLine());

            Console.Write("Giá bán: ");
            int sellValue = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Đã sửa thành công...");
            System.Threading.Thread.Sleep(1000);

            menu_control.EditProductInfo(id, type, productName, costValue, sellValue);
        }
    }   
}
