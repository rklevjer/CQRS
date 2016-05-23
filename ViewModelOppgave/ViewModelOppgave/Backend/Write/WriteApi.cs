namespace ViewModelOppgave.Backend.Write
{
    public class WriteApi : IWriteApi
    {
        public string AddNewMember(MemberDetailsDto updateMember)
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
    }
}
