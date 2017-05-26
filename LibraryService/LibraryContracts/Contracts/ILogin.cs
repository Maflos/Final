using LibraryContracts.Model;
using System.ServiceModel;

namespace LibraryContracts.Contracts
{
    [ServiceContract]
    public interface ILogin
    {
        [OperationContract]
        User FindUser(string username, string password);
    }
}
