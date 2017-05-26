using LibraryContracts.Model;
using System.Collections.Generic;
using System.ServiceModel;

namespace LibraryContracts.Contracts
{
    [ServiceContract]
    public interface IUser
    {
        [OperationContract]
        List<RatedBook> GetAllBooks();

        [OperationContract]
        List<RatedBook> GetPersonalBooks(int id);

        [OperationContract]
        List<RatedBook> GetWantedBooks(int id);

        [OperationContract]
        void AddToRead(RatedBook book);

        [OperationContract]
        void AddToWanted(int usrID, int bID);

        [OperationContract]
        void RemoveFromWishList(int bookID);

        [OperationContract]
        void RemoveFromReadList(int bookID);
    }
}
