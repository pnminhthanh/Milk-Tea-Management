using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee_Store_Management
{
    class ReceiptDetail
    {
        public static int ID;
        public int idRecept, qtyProduct, total;
        public StaffModel staff;
        public MenuModel product;

        public ReceiptDetail()
        {

        }

        public ReceiptDetail(MenuModel product, int qtyproduct, int total)
        {
            this.idRecept = ++ID;
            this.product = product;
            this.qtyProduct = qtyproduct;
            this.total = total;
        }
    }
}
