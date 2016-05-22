using System.Collections.Generic;
using System.Linq;

namespace ViewModelOppgave.Backend
{
	public class DB
	{
		private readonly IList<Member> _members;

		static readonly DB _instance = new DB();

		public static DB Instance
		{
			get { return _instance; }
		}

		public DB()
		{
			_members = new List<Member>
			{
				new Member
				{
					Id = "0",
					FirstName = "John",
					LastName = "Doe",
					Age = 18,
					Sex = Sex.Male
				},
				new Member
				{
					Id = "1",
					FirstName = "Sue",
					LastName = "Doe",
					Age = 20,
					Sex = Sex.Female
				}
			};
		}

		public IList<Member> GetAllMembers()
		{
			return _members;
		}

        public void SaveNewMember(Member m)
        {
            m.Id = (_members == null ? 0 : _members.Count).ToString();
            _members.Add(m);
        }

        public Member GetMemberWithId(string id)
		{
            return _members.FirstOrDefault(m => m.Id == id);
		}
	}
}
