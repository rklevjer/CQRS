namespace ViewModelOppgave.Backend.Write
{
    public interface IWriteApi
    {
        string AddNewMember(DetailMember updateMember);
        DetailMember GetSelectedMember(string id);
    }
}
