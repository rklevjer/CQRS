using System.Collections.Generic;

namespace ViewModelOppgave.Backend.Read
{
    public class ReadApi : IReadApi
    {
		public IList<MembersGridDto> GetAllMembers()
		{
            IList<MembersGridDto> gridMembers = new List<MembersGridDto>();
            var members = DB.Instance.GetAllMembers();
            foreach(var member in members)
            {
                gridMembers.Add(new MembersGridDto
                                {
                                    Id = member.Id,
                                    FirstName = member.FirstName,
                                    LastName = member.LastName,
                                    Age = member.Age,
                                    Sex = member.Sex
                                 }
                                );
            }
			return gridMembers;
		}

        public MembersGridDto GetSelectedMember(string id)
        {
            Member m = DB.Instance.GetMemberWithId(id);
            return new MembersGridDto
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
