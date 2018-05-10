using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee_Store_Management
{
    class MemberManagement
    {
        private MemberView member_view;
        private TypeMemberManagement type_management;
        private List<MemberModel> listMembers;
        private static string path = "..\\MemberData.txt";

        public MemberManagement(Intro intro, TypeMemberManagement type_management)
        {
            MemberView view = new MemberView(intro);
            type_management.Init(this);
            this.member_view = view;
            this.type_management = type_management;
            member_view.Init(this);
            listMembers = new List<MemberModel>();
            IOMethod.Instance.ReadData<MemberModel>(path, ref listMembers);
        }

        public void CreateMember(string name, GenderType gender, DateTime birth, string CMND)
        {
            if (listMembers.Count == 0)
                MemberModel.ID = 0;
            else MemberModel.ID = listMembers[listMembers.Count - 1].idMember;
            MemberModel member = new MemberModel(name, gender, birth, CMND, type_management.FindTypeMember(1)); 
            IOMethod.Instance.WriteData<MemberModel>(path, member);
            listMembers.Add(member);
        }

        public void PrintListMember()
        {
            member_view.PrintListMember(listMembers);
        }

        public MemberModel FindMember(int idMember)
        {
            for (int i = 0; i < listMembers.Count; i++)
            {
                if (listMembers[i].idMember == idMember)
                    return listMembers[i];
            }
            MemberModel member = new MemberModel();
            return member;
        }

        public void DeleteMemberInfo(int id)
        {
            MemberModel member_toremoved = FindMember(id);
            listMembers.Remove(member_toremoved);
            IOMethod.Instance.EditData<MemberModel>(path, listMembers);
        }

        public void EditMemberInfo(int id, string name, DateTime birthday, GenderType gender, string CMND)
        {
            MemberModel member = FindMember(id);
            int index = listMembers.IndexOf(member);
            listMembers[index].nameMember = name;
            listMembers[index].birthMember = birthday;
            listMembers[index].genderMember = gender;
            listMembers[index].CMND = CMND;
            IOMethod.Instance.EditData<MemberModel>(path, listMembers);
        }

        public void FilterMemberByPoint(int up_limit, int down_limit)
        {
            for(int i = 0; i< listMembers.Count;i++)
            {
                if (listMembers[i].point >= down_limit && listMembers[i].point <= up_limit)
                    member_view.PrintMemberInfo(listMembers[i]);
            }
        }

        public void UpdatePoint(int idMember, int valuetotal_receipt)
        {
            for (int i = 0; i < listMembers.Count; i++)
            {
                if (listMembers[i].idMember == idMember)
                {
                    listMembers[i].point += valuetotal_receipt * 0.05;
                    CheckPoint(listMembers[i]);
                }
            }
            IOMethod.Instance.EditData<MemberModel>(path, listMembers);
        }

        public void CheckPoint(MemberModel member)
        {
            TypeMember next_type = type_management.listTypeMember[member.typeMember.idType + 1];
            if (member.point >= next_type.requiredpoint)
                member.typeMember = next_type;
        }
    }
}
