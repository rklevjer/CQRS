﻿namespace ViewModelOppgave.Backend.Read
{
    public class GridMember
    {
        public GridMember()
        {
        }

        public GridMember(bool isNew = false)
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
