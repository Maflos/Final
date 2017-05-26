using LibraryContracts.Model;
using System.Collections.Generic;
using System.ServiceModel;

namespace LibraryContracts.Contracts
{
    [ServiceContract]
    public interface IUserAdmin
    {
        [OperationContract]
        List<User> GetUsers();

        [OperationContract]
        void InsertUser(User aut);

        [OperationContract]
        void DeleteUser(int id);

        [OperationContract]
        void UpdateUser(User user);

        [OperationContract]
        List<RatedBook> GetRatedBooks(int userID);

        [OperationContract]
        void GenerateReports(string content);
    }
}
