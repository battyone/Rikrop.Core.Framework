using System.Collections.Generic;

namespace Rikrop.Core.Framework.ActiveDirectory
{
    public interface IADManager
    {
        IEnumerable<ADUserEntity> GetUsersInGroup(string groupName, bool recursive);
        IEnumerable<ADUserEntity> GetAllUsers();
        IEnumerable<ADUserEntity> GetUsersWithNameAndLastName();
        IEnumerable<ADGroupEntity> GetUserGroups(string userIdentity);
        IEnumerable<ADGroupEntity> GetAllGroups();
        bool ValidateAuthorization();
    }
}