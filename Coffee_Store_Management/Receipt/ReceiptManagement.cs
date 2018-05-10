using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee_Store_Management
{
    class ReceiptManagement
    {
        private ReceiptView receipt_view;
        StaffManagement staff_manage;
        MenuManagement menu_manage;
        MemberManagement member_manage;
        private static string pathDetail = "..\\DetailReceiptData.txt";
        private static string path = "..\\ReceiptData.txt";


        List<ReceiptDetail> listReceiptDetail, tempListReceiptDetail;
        List<Receipt> listReceipt;

        public ReceiptManagement(Intro intro, MenuManagement menu, StaffManagement staff, MemberManagement member)
        {
            ReceiptView view_receipt = new ReceiptView(intro);
            this.receipt_view = view_receipt;
            this.staff_manage = staff;
            this.menu_manage = menu;
            this.member_manage = member;
            receipt_view.Init(this);
            listReceiptDetail = new List<ReceiptDetail>();
            tempListReceiptDetail = new List<ReceiptDetail>();
            listReceipt = new List<Receipt>();
            view_receipt.OnPrintMenu += menu.PrintMenu;
            IOMethod.Instance.ReadData<ReceiptDetail>(pathDetail, ref listReceiptDetail);
            IOMethod.Instance.ReadData<Receipt>(path, ref listReceipt);
        }

        public void CreateReceiptDetailInfo(int idProduct, int qtyProduct)
        {
            if (listReceiptDetail.Count == 0)
                ReceiptDetail.ID = 0;
            else ReceiptDetail.ID = listReceiptDetail[listReceiptDetail.Count - 1].idRecept;
            int total = qtyProduct * menu_manage.FindProduct(idProduct).sellValue;
            ReceiptDetail receiptdetail = new ReceiptDetail(menu_manage.FindProduct(idProduct), qtyProduct, total);
            ReceiptDetail tempreceiptdetail = new ReceiptDetail((menu_manage.FindProduct(idProduct)), qtyProduct, total);
            IOMethod.Instance.WriteData<ReceiptDetail>(pathDetail, receiptdetail);
            listReceiptDetail.Add(receiptdetail);
            tempListReceiptDetail.Add(tempreceiptdetail);
        }

        public void CreateReceiptInfo(int idMember, int idStaff)
        {
            int total_receipt = 0;
            for (int i = 0; i < tempListReceiptDetail.Count; i++) 
            {
                total_receipt += tempListReceiptDetail[i].total;
            }
            Receipt receipt = new Receipt(staff_manage.FindStaff(idStaff),tempListReceiptDetail, member_manage.FindMember(idMember),total_receipt);
            receipt_view.PrintReceipt(receipt);
            IOMethod.Instance.WriteData<Receipt>(path, receipt);
            member_manage.UpdatePoint(idMember, total_receipt);
            listReceipt.Add(receipt);
            tempListReceiptDetail = new List<ReceiptDetail>();
        }

        public void PrintReceiptsList(bool isAdmin)
        {
            receipt_view.PrintListReceipt(listReceipt, isAdmin);
        }

        public ReceiptDetail FindReceipt(int idreceipt)
        {
            for (int i = 0; i < listReceiptDetail.Count; i++)
            {
                if (listReceiptDetail[i].idRecept == idreceipt)
                    return listReceiptDetail[i];
            }
            ReceiptDetail receipt = new ReceiptDetail();
            return receipt;
        }

        public void DeleteReceipt(int id)
        {
            ReceiptDetail receipt = FindReceipt(id);
            listReceiptDetail.Remove(receipt);
            IOMethod.Instance.EditData<ReceiptDetail>(path, listReceiptDetail);
        }

        public void EditReceipt(int id, int idStaff, int idProduct, int qtyProduct)
        {
            int index = listReceiptDetail.IndexOf(FindReceipt(id));
            listReceiptDetail[index].staff = staff_manage.FindStaff(idStaff);
            listReceiptDetail[index].product = menu_manage.FindProduct(idProduct);
            listReceiptDetail[index].total = qtyProduct * menu_manage.FindProduct(idProduct).sellValue;
            IOMethod.Instance.EditData<ReceiptDetail>(path, listReceiptDetail);
        }

        public void FilterReceipt(int up_limit, int down_limit,/* DateTime up_Date, DateTime down_Date,*/ DateTime Date)
        {
            //DateTime up_date, down_date;
            //if (up_Date != null)
            //    up_date = DateTime.Parse(up_Date);
            //else up_date = DateTime.Now;
            //if (down_Date != null)
            //    down_date = DateTime.Parse(down_Date);
            //else down_date = DateTime.Parse("01/01/2017");
            List<Receipt> listFilter = new List<Receipt>();
            if (Date != null) 
            {
                //DateTime date = DateTime.Parse(Date);
                //Console.Write(date.ToLongDateString());
                for (int i = 0; i < listReceipt.Count; i++)
                {
                    if (listReceipt[i].total > down_limit && listReceipt[i].total < up_limit && listReceipt[i].date.Date == Date.Date)
                        receipt_view.PrintReceipt(listReceipt[i]);
                }
            }
            //else
            //for (int i = 0; i< listReceipt.Count;i++)
            //{
            //    if (listReceipt[i].total > down_limit && listReceipt[i].total < up_limit && listReceipt[i].date.Date < up_Date.Date && listReceipt[i].date.Date > down_Date.Date)
            //        receipt_view.PrintReceipt(listReceipt[i]);
            //}
        }
    }
}
