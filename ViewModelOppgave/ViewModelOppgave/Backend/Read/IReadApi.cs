using System.Collections.Generic;

namespace ViewModelOppgave.Backend.Read
{
    public interface IReadApi
    {
	    IList<GridMember> GetAllMembers();
    }
}
