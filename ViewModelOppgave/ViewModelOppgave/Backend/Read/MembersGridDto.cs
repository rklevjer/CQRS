namespace ViewModelOppgave.Backend.Read
{
    public class MembersGridDto
    {
        public MembersGridDto()
        {
        }

        public MembersGridDto(bool isNew = false)
        {
            IsNew = isNew;
        }

        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public Sex Sex { get; set; }

        public bool IsNew { get; set; }
    }
}
