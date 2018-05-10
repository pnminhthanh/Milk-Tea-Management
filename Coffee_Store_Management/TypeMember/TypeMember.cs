using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee_Store_Management
{
    class TypeMember
    {
        public int idType;
        public double discountrate, requiredpoint;
        public string nameType;
        public static int ID;

        public TypeMember()
        {

        }

        public TypeMember(string NameType, double discountRate, double requirePoint)
        {
            this.idType = ++ID;
            this.nameType = NameType;
            this.discountrate = discountRate;
            this.requiredpoint = requirePoint;
        }
    }
}
