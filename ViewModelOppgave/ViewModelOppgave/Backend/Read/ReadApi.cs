using System.Collections.Generic;

namespace ViewModelOppgave.Backend.Read
{
    public class ReadApi : IReadApi
    {
		public IList<GridMember> GetAllMembers()
		{
            IList<GridMember> listMembers = new List<GridMember>();
            var members = DB.Instance.GetAllMembers();
            foreach(var member in members)
            {
                listMembers.Add(new GridMember
                                {
                                    Id = member.Id,
                                    FirstName = member.FirstName,
                                    LastName = member.LastName,
                                    Age = member.Age,
                                    Sex = member.Sex
                                 }
                                );
            }
			return listMembers;
		}       
	}
}
