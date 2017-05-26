using LibraryContracts.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using LibraryContracts.Model;
using LibraryContracts.Reports;

namespace LibraryService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "LibraryService" in both code and config file together.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class LibraryService : ILogin, IUser, IAdmin, IUserAdmin
    {
        //Login stuff
        public User FindUser(string username, string password)
        {
            User user = null;

            using (libraryEntities database = new libraryEntities())
            {
                user usr = database.user.First((u) => u.name == username && u.password == password);
                user = new User()
                {
                    UserID = usr.userID,
                    Name = usr.name,
                    Password = usr.password,
                    Email = usr.email,
                    Type = usr.type
                };
            }

            return user;
        }
       
        //Admin stuff
        public List<Author> GetAuthors()
        {
            List<Author> authorList = new List<Author>();

            using (libraryEntities database = new libraryEntities())
            {
                foreach (author a in database.author)
                {
                    Author aut = new Author()
                    {
                        AuthorName = a.authorName,
                        AuthorID = a.authorID
                    };

                    authorList.Add(aut);
                }
            }

            return authorList;
        }

        public List<Book> GetBooks(string authorName)
        {
            List<Book> bookList = new List<Book>();

            using (libraryEntities database = new libraryEntities())
            {
                foreach (book b in database.book)
                {
                    if (authorName == b.authorName)
                    {
                        Book bo = new Book()
                        {
                            BookID = b.bookID,
                            AuthorName = b.authorName,
                            BookName = b.bookName
                        };

                        bookList.Add(bo);
                    }            
                }
            }

            return bookList;
        }

        public void InsertAuthor(Author aut)
        {
            using (var context = new libraryEntities())
            {
                var a = new author
                {
                    authorName = aut.AuthorName
                };

                context.author.Add(a);
                context.SaveChanges();
            }
        }

        public void InsertBook(Book book)
        {
            using (var context = new libraryEntities())
            {
                var b = new book
                {
                    bookName = book.BookName,
                    authorName = book.AuthorName
                };

                context.book.Add(b);
                context.SaveChanges();
            }
        }

        public void UpdateAuthor(Author aut)
        {
            using (var context = new libraryEntities())
            {
                var result = context.author.First(a => a.authorID == aut.AuthorID);

                result.authorName = aut.AuthorName;

                context.SaveChanges();
            }
        }

        public void UpdateBook(Book book)
        {
            using (var context = new libraryEntities())
            {
                var result = context.book.First(b => b.bookID == book.BookID);

                result.bookName = book.BookName;
                result.authorName = book.AuthorName;

                context.SaveChanges();
            }
        }

        public void DeleteAuthor(int id)
        {
            using (var context = new libraryEntities())
            {
                author a = new author() { authorID = id };
                context.author.Attach(a);
                context.author.Remove(a);
                context.SaveChanges();
            }
        }

        public void DeleteBook(int id)
        {
            using (var context = new libraryEntities())
            {
                book b = new book() { bookID = id };
                context.book.Attach(b);
                context.book.Remove(b);
                context.SaveChanges();
            }
        }

        public void GenerateReports(string content)
        {
            Report[] reports = new Report[2];
            reports[0] = new ReportCSV();
            reports[1] = new ReportPDF();

            foreach (Report report in reports)
            {
                DocumentGeneral doc = report.CreateReport();
                doc.Text = content;
                doc.GenerateReport();
            }
        }

        //UserAdmin stuff
        public List<User> GetUsers()
        {
            List<User> userList = new List<User>();

            using (libraryEntities database = new libraryEntities())
            {
                foreach (user u in database.user)
                {
                    User usr = new User()
                    {
                        UserID = u.userID,
                        Name = u.name,
                        Password = u.password,
                        Email = u.email,
                        Type = u.type  
                    };

                    userList.Add(usr);
                }
            }

            return userList;
        }

        public List<RatedBook> GetRatedBooks(int userID)
        {
            List<RatedBook> rBookList = new List<RatedBook>();

            using (libraryEntities database = new libraryEntities())
            {
                foreach (readbook rb in database.readbook)
                {
                    if(rb.userID == userID)
                    {
                        RatedBook r = new RatedBook()
                        {
                            RatedBookID = rb.readBookID,
                            UserID = rb.userID,
                            BookID = rb.bookID,
                            Rating = rb.rating,
                            Review = rb.review,
                        };

                        rBookList.Add(r);
                    }
                }

                foreach (RatedBook rb in rBookList)
                {
                    book b = database.book.First((fb) => fb.bookID == rb.BookID);

                    rb.AuthorName = b.authorName;
                    rb.BookName = b.bookName;
                }
            }          

            return rBookList;
        }

        public void InsertUser(User usr)
        {
            using (var context = new libraryEntities())
            {
                var u = new user
                {
                    name = usr.Name,
                    password = usr.Password,
                    email = usr.Email,
                    type = usr.Type
                };

                context.user.Add(u);
                context.SaveChanges();
            }
        }

        public void DeleteUser(int id)
        {
            using (var context = new libraryEntities())
            {
                user u = new user() { userID = id };
                context.user.Attach(u);
                context.user.Remove(u);
                context.SaveChanges();
            }
        }

        public void UpdateUser(User usr)
        {
            using (var context = new libraryEntities())
            {
                var result = context.user.First(u => u.userID == usr.UserID);

                result.name = usr.Name;
                result.password = usr.Password;
                result.email = usr.Email;
                result.type = usr.Type;

                context.SaveChanges();
            }
        }

        //User stuff
        public List<RatedBook> GetAllBooks()
        {
            List<RatedBook> bookList = new List<RatedBook>();
            int rating = 0;
            int nr = 0;

            using (libraryEntities database = new libraryEntities())
            {
                foreach (book b in database.book)
                {
                    RatedBook bo = new RatedBook()
                    {
                        BookID = b.bookID,
                        AuthorName = b.authorName,
                        BookName = b.bookName,
                        Rating = 0
                    };

                    bookList.Add(bo);
                }

                foreach (RatedBook rb in bookList)
                {
                    nr = 0;
                    rating = 0;

                    foreach (readbook b in database.readbook)
                    {
                        if (rb.BookID == b.bookID)
                        {
                            rating += b.rating;
                            nr++;
                        }
                    }

                    if (nr != 0)
                    {
                        rb.Rating = rating / nr;
                    }

                }                
            }

            return bookList;
        }

        public List<RatedBook> GetPersonalBooks(int id)
        {
            List<RatedBook> rBookList = new List<RatedBook>();

            using (libraryEntities database = new libraryEntities())
            {
                foreach (readbook rb in database.readbook)
                {
                    if (rb.userID == id)
                    {
                        RatedBook r = new RatedBook()
                        {
                            RatedBookID = rb.readBookID,
                            UserID = rb.userID,
                            BookID = rb.bookID,
                            Rating = rb.rating,
                            Review = rb.review,
                        };

                        rBookList.Add(r);
                    }
                }

                foreach (RatedBook rb in rBookList)
                {
                    book b = database.book.First((fb) => fb.bookID == rb.BookID);

                    rb.AuthorName = b.authorName;
                    rb.BookName = b.bookName;
                }
            }

            return rBookList;
        }

        public List<RatedBook> GetWantedBooks(int id)
        {
            List<RatedBook> bookList = GetAllBooks();
            List<RatedBook> result = new List<RatedBook>();

            using (libraryEntities database = new libraryEntities())
            {
                foreach (wishlist b in database.wishlist)
                {
                    RatedBook rb = bookList.Find(r => r.BookID == b.bookID && b.userID == id);
                    rb.RatedBookID = b.wishlistID;
                    result.Add(rb);
                }
            }

            return result;
        }

        public void AddToRead(RatedBook book)
        {
            using (var context = new libraryEntities())
            {
                var rb = new readbook
                {
                   bookID = book.BookID,
                   userID = book.UserID,
                   rating = book.Rating,
                   review = book.Review
                };

                context.readbook.Add(rb);
                context.SaveChanges();
            }
        }

        public void AddToWanted(int usrID, int bID)
        {
            using (var context = new libraryEntities())
            {
                var w = new wishlist
                {
                   bookID = bID,
                   userID = usrID 
                };

                context.wishlist.Add(w);
                context.SaveChanges();
            }
        }

        public void RemoveFromWishList(int bookID)
        {
            using (var context = new libraryEntities())
            {
                wishlist w = new wishlist() { wishlistID = bookID };
                context.wishlist.Attach(w);
                context.wishlist.Remove(w);
                context.SaveChanges();
            }
        }

        public void RemoveFromReadList(int bookID)
        {
            using (var context = new libraryEntities())
            {
                readbook r = new readbook() { readBookID = bookID };
                context.readbook.Attach(r);
                context.readbook.Remove(r);
                context.SaveChanges();
            }
        }
    }
}
