using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee_Store_Management
{
    class Program
    {
        static void Main(string[] args)
        {
            Intro intro = new Intro();
            DepartmentManagement department = new DepartmentManagement(intro);
            MenuManagement menu = new MenuManagement(intro);
            TypeMemberManagement type = new TypeMemberManagement(intro);
            MemberManagement member = new MemberManagement(intro,type);
            StaffManagement staff = new StaffManagement(intro,department);
            ReceiptManagement receipt = new ReceiptManagement(intro, menu, staff, member);
            intro.ShowListFunction();
        }
    }
}
