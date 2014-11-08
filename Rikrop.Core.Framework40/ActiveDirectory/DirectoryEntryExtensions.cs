using System.DirectoryServices;

namespace Rikrop.Core.Framework.ActiveDirectory
{
    public static class DirectoryEntryExtensions
    {
        public static ADUserEntity CreateUserEntity(this DirectoryEntry directoryEntry)
        {
            return new ADUserEntity
                       {
                           AccountName = directoryEntry.GetAccountName(),
                           DisplayName = directoryEntry.GetDisplayName(),
                           Email = directoryEntry.GetMail(),
                           LastName = directoryEntry.GetLastName(),
                           FirstName = directoryEntry.GetFirstName(),
                           JobTitle = directoryEntry.GetJobTitle()
                       };
        }


        public static ADGroupEntity CreateGroupEntity(this DirectoryEntry directoryEntry)
        {
            return new ADGroupEntity {Name = directoryEntry.Name.Replace("CN=", "")};
        }

        private static string GetFirstName(this DirectoryEntry directoryEntry)
        {
            return directoryEntry.GetValueFromPropertyCollection("givenName");
        }

        private static string GetLastName(this DirectoryEntry directoryEntry)
        {
            return directoryEntry.GetValueFromPropertyCollection("sn");
        }

        private static string GetMail(this DirectoryEntry directoryEntry)
        {
            return directoryEntry.GetValueFromPropertyCollection("mail");
        }

        private static string GetAccountName(this DirectoryEntry directoryEntry)
        {
            return directoryEntry.GetValueFromPropertyCollection("samAccountName");
        }

        private static string GetDisplayName(this DirectoryEntry directoryEntry)
        {
            return directoryEntry.GetValueFromPropertyCollection("displayName");
        }

        private static string GetJobTitle(this DirectoryEntry directoryEntry)
        {
            return directoryEntry.GetValueFromPropertyCollection("title");
        }

        private static string GetValueFromPropertyCollection(this DirectoryEntry directoryEntry, string index)
        {
            var value = directoryEntry.Properties[index].Value;
            return value == null
                       ? null
                       : value.ToString();
        }
    }
}