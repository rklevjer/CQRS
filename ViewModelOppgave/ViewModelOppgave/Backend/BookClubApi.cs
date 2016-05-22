using System.Collections.Generic;

namespace ViewModelOppgave.Backend
{
	public class BookClubApi : IBookClubApi
	{
		private static int _nextId;

		private readonly List<Member> _members = new List<Member>
		{
			new Member
			{
				Id = (_nextId++).ToString(),
				FirstName = "John",
				LastName = "Doe",
				Age = 18,
				Sex = Sex.Male
			},
			new Member
			{
				Id = (_nextId++).ToString(),
				FirstName = "Sue",
				LastName = "Doe",
				Age = 20,
				Sex = Sex.Female
			}
		};

		public List<Member> GetAllMembers()
		{
			return _members;
		}

		public string AddNewMember(Member member)
		{
			var m = new Member
			{
				Id = (_nextId++).ToString(),
				FirstName = member.FirstName,
				LastName = member.LastName,
				Age = member.Age,
				Sex = member.Sex
			};

			_members.Add(m);
			return m.Id;
		}
	}
}