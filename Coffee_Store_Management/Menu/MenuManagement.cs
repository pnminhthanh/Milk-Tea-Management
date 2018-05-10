using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee_Store_Management
{
    class MenuManagement
    {
        private MenuView menu_view;
        List<MenuModel> listProducts;
        private static string path = "..\\MenuData.txt";

        public MenuManagement()
        {

        }

        public MenuManagement(Intro intro)
        {
            MenuView view = new MenuView(intro);
            this.menu_view = view;
            menu_view.Init(this);
            listProducts = new List<MenuModel>();            
            IOMethod.Instance.ReadData<MenuModel>(path, ref listProducts);
        }

        public void CreateMenu(string type, string name, int costvalue, int sellvalue)
        {
            if (listProducts.Count == 0)
                MenuModel.ID = 0;
            else MenuModel.ID = listProducts[listProducts.Count - 1].idProduct;
            MenuModel product = new MenuModel(type, name, costvalue, sellvalue);
            IOMethod.Instance.WriteData<MenuModel>(path, product);
            listProducts.Add(product);
        }

        public void PrintMenu(bool isAdmin)
        {
            menu_view.PrintListMenuInfo(listProducts);
        }

        public MenuModel FindProduct(int idProduct)
        {
            for (int i = 0; i < listProducts.Count; i++)
            {
                if (listProducts[i].idProduct == idProduct)
                    return listProducts[i];
            }
            MenuModel product = new MenuModel();
            return product;
        }

        public MenuModel FindProduct(int idProduct)
        {
            for (int i = 0; i < listProducts.Count; i++)
            {
                if (listProducts[i].idProduct == idProduct)
                    return listProducts[i];
            }
            MenuModel product = new MenuModel();
            return product;
        }

        public void EditProductInfo(int id, string type, string name, int costvalue, int sellvalue)
        {
            int index = listProducts.IndexOf(FindProduct(id));
            listProducts[index].type = type;
            listProducts[index].nameProduct = name;
            listProducts[index].costValue = costvalue;
            listProducts[index].sellValue = sellvalue;
            IOMethod.Instance.EditData<MenuModel>(path, listProducts);
        }
    }
}
