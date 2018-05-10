using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee_Store_Management
{
    class TypeMemberManagement
    {
        private TypeMemberView type_view;
        private MemberManagement member_manage;
        public List<TypeMember> listTypeMember;
        private static string path = "..\\TypeMember.txt";

        public TypeMemberManagement(Intro intro)
        {
            TypeMemberView type_View = new TypeMemberView(intro);
            listTypeMember = new List<TypeMember>();
            this.type_view = type_View;
            type_view.Init(this);
            IOMethod.Instance.ReadData<TypeMember>(path, ref listTypeMember);
        }

        public void Init(MemberManagement member)
        {
            this.member_manage = member;
        }

        public void CreateTypeMember(string NameType, double discountRate, double requirePoint)
        {
            if (listTypeMember.Count == 0)
                TypeMember.ID = 0;
            else TypeMember.ID = listTypeMember[listTypeMember.Count - 1].idType;
            TypeMember newtype = new TypeMember(NameType, discountRate, requirePoint);
            IOMethod.Instance.WriteData<TypeMember>(path, newtype);
            listTypeMember.Add(newtype);
        }

        public void PrintListTypeMember(bool isAdmin)
        {
            type_view.PrintListTypeMember(listTypeMember, true);
        }

        public TypeMember FindTypeMember(int idType)
        {
            for (int i = 0; i < listTypeMember.Count; i++)
            {
                if (listTypeMember[i].idType == idType)
                    return listTypeMember[i];
            }
            TypeMember type = new TypeMember();
            return type;
        }
    }
}
