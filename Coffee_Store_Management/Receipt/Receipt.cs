using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee_Store_Management
{
    class Receipt
    {
        public static int ID;
        public int idReceipt, total;
        public List<ReceiptDetail> listReceiptDetail;
        public StaffModel staff;
        public MemberModel Member;
        public DateTime date;

        public Receipt(StaffModel staff, List<ReceiptDetail> listReceiptDetail, MemberModel member, int total)
        {
            this.idReceipt = ++ID;
            this.listReceiptDetail = listReceiptDetail;
            this.Member = member;
            this.total = total;
            this.staff = staff;
            this.date = DateTime.Now.Date;
        }
    }
}
