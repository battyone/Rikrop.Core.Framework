using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;

namespace Rikrop.Core.Framework.ActiveDirectory
{
    public class ADManager : IADManager
    {
        private readonly string _domain;
        private readonly string _login;
        private readonly string _password;


        public ADManager(string domain, string login, string password)
        {
            _domain = domain;
            _login = login;
            _password = password;
        }

        public static bool ValidateAuthorization(string domain, string login, string password)
        {
            using (var context = new PrincipalContext(ContextType.Domain, domain))
            {
                return context.ValidateCredentials(login, password);
            }
        }

        public IEnumerable<ADUserEntity> GetUsersInGroup(string groupName, bool recursive = false)
        {
            var users = new List<ADUserEntity>();

            using (var context = CreatePrincipalContext())
            {
                using (var group = GroupPrincipal.FindByIdentity(context, IdentityType.SamAccountName, groupName))
                {
                    if (group != null)
                    {
                        users.AddRange(@group.GetMembers(recursive).Select(p => p.GetUnderlyingObject()).OfType<DirectoryEntry>().Select(directoryEntry => directoryEntry.CreateUserEntity()));
                    }
                }
            }

            return users;
        }

        public IEnumerable<ADGroupEntity> GetAllGroups()
        {
            var groups = new List<ADGroupEntity>();
            using (var context = CreatePrincipalContext())
            {
                var group = new GroupPrincipal(context);

                using (var searcher = new PrincipalSearcher(group))
                {
                    groups.AddRange(searcher.FindAll().OfType<GroupPrincipal>().Select(found => found.GetUnderlyingObject()).OfType<DirectoryEntry>().Select(directoryEntry => directoryEntry.CreateGroupEntity()));
                }
            }
            return groups;
        }

        public IEnumerable<ADGroupEntity> GetUserGroups(string userIdentity)
        {
            var userGroups = new List<ADGroupEntity>();

            using (var context = CreatePrincipalContext())
            {
                using (var user = UserPrincipal.FindByIdentity(context, userIdentity))
                {
                    if (user != null)
                    {
                        using (var groups = user.GetAuthorizationGroups())
                        {
                            userGroups
                                .AddRange(groups.Select(group => new ADGroupEntity {Name = group.Name}));
                        }
                    }
                }
            }

            return userGroups;
        }

        public IEnumerable<ADUserEntity> GetAllUsers()
        {
            var users = new List<ADUserEntity>();

            using (var context = CreatePrincipalContext())
            {
                using (var searcher = new PrincipalSearcher(new UserPrincipal(context)))
                {
                    users.AddRange(searcher.FindAll().Select(result => result.GetUnderlyingObject() as DirectoryEntry).Select(directoryEntry => directoryEntry.CreateUserEntity()));
                }
            }

            return users;
        }

        public ADUserEntity GetUserByAccountName(string accountName)
        {
            using (var context = CreatePrincipalContext())
            {
                using (var user = UserPrincipal.FindByIdentity(context, accountName))
                {
                    if (user != null)
                    {
                        var directoryEntry = user.GetUnderlyingObject() as DirectoryEntry;
                        if(directoryEntry != null)
                        {
                            return directoryEntry.CreateUserEntity();
                        }
                    }
                }
            }
            return null;
        }

        public IEnumerable<ADUserEntity> GetUsersWithNameAndLastName()
        {
            return GetAllUsers().Where(user => (!String.IsNullOrEmpty(user.FirstName) && !String.IsNullOrEmpty(user.LastName)));
        }

        public bool ValidateAuthorization()
        {
            using (var context = CreatePrincipalContext())
            {
                return context.ValidateCredentials(_login, _password);
            }
        }

        private PrincipalContext CreatePrincipalContext()
        {
            return new PrincipalContext(ContextType.Domain, _domain, _login, _password);
        }
    }
}