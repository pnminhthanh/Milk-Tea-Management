using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee_Store_Management
{
    class MemberModel

    {
        public int idMember;
        public double point;
        public string nameMember, CMND;
        public DateTime birthMember, timeSignup;
        public GenderType genderMember;
        public TypeMember typeMember;
        public static int ID = 0;

        public MemberModel()
        {

        }

        public MemberModel(string name,GenderType gender, DateTime birth, string CMND, TypeMember type) 
        {
            this.idMember = ++ID;
            this.nameMember = name;
            this.genderMember = gender;
            this.birthMember = birth;
            this.CMND = CMND;
            this.timeSignup = DateTime.Now;
            this.point = 0;
            this.typeMember = type;
        }
    }
}
