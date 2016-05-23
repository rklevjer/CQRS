using System.Collections.Generic;

namespace ViewModelOppgave.Backend.Read
{
    public interface IReadApi
    {
	    IList<MembersGridDto> GetAllMembers();
        MembersGridDto GetSelectedMember(string id);
    }
}
