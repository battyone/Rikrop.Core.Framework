namespace Rikrop.Core.Framework.ActiveDirectory
{
    public class ADUserEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public string AccountName { get; set; }
        public string Email { get; set; }
        public string JobTitle { get; set; }

        public override string ToString()
        {
            return
                "Name: " + FirstName + "\n" +
                "LastName: " + LastName + "\n" +
                "DisplayName: " + DisplayName + "\n" +
                "AccountName: " + AccountName + "\n" +
                "Email: " + Email + "\n" +
                "JobTitle: " + JobTitle;
        }
    }
}