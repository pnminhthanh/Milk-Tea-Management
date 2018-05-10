using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee_Store_Management
{
    class MenuModel
    {
        public string nameProduct, type;
        public int idProduct, costValue, sellValue;
        public static int ID;

        public MenuModel()
        {

        }

        public MenuModel(string type, string name, int costValue, int sellValue)
        {
            this.idProduct = ++ID;
            this.type = type;
            this.nameProduct = name;
            this.costValue = costValue;
            this.sellValue = sellValue;
        }
    }
}
