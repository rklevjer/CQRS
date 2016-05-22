using System.Collections.Generic;

namespace ViewModelOppgave.Backend
{
	public interface IBookClubApi
	{
		List<Member> GetAllMembers();

		string AddNewMember(Member member);
	}
}