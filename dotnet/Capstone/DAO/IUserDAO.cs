using System.Collections.Generic;
using Capstone.Models;

namespace Capstone.DAO
{
    public interface IUserDAO
    {
        User GetUser(string username);
        User AddUser(string username, string password, string role);
        User AddNewFamilyMember(string username, string password, string role, int parentId);
        List<ReturnUser> GetFamilyChildList(int userId);
    }
}
