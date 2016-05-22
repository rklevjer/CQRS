namespace ViewModelOppgave.Backend.Write
{
    public class WriteApi : IWriteApi
    {
        public string AddNewMember(DetailMember updateMember)
        {
            Member member = new Member
            {
                FirstName = updateMember.FirstName,
                LastName = updateMember.LastName,
                Age = updateMember.Age,
                Sex = updateMember.Sex
            };
            DB.Instance.SaveNewMember(member);
            return member.Id;
        }

        public DetailMember GetSelectedMember(string id)
        {
            Member m = DB.Instance.GetMemberWithId(id);
            return new DetailMember
            {
                Id = m.Id,
                FirstName = m.FirstName,
                LastName = m.LastName,
                Age = m.Age,
                Sex = m.Sex
            };
        }
    }
}
