using LibraryContracts.Model;
using System.Collections.Generic;
using System.ServiceModel;

namespace LibraryContracts.Contracts
{
    [ServiceContract]
    public interface IAdmin
    {
        [OperationContract]
        List<Author> GetAuthors();
      
        [OperationContract]
        void InsertAuthor(Author aut);

        [OperationContract]
        void DeleteAuthor(int id);

        [OperationContract]
        void UpdateAuthor(Author aut);

        [OperationContract]
        List<Book> GetBooks(string authorName);

        [OperationContract]
        void InsertBook(Book book);

        [OperationContract]
        void DeleteBook(int id);

        [OperationContract]
        void UpdateBook(Book book);
    }
}
