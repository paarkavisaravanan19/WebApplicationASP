using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace WebApplicationASP.Pages.Books
{
    public class UpdateBookModel : PageModel
    {
        public Books book = new Books();
        public string message = "";
        public string BookCode = "";
        public void OnGet()
        {
            try
            {
                BookCode = Request.Query["BookCode"];
                string CONN_STRING = "Data Source=DESKTOP-2ESDHDD;Initial Catalog=SQLProject1;Integrated Security=True;Encrypt=False";
                SqlConnection connection = new SqlConnection(CONN_STRING);
                connection.Open();

                SqlCommand cmd = connection.CreateCommand();

                cmd.CommandText = $"SELECT BOOK_CODE,BOOK_TITLE,AUTHOR,CATEGORY,PUBLICATION," +
                    $"PUBLISHED_DATE,BOOK_EDITION,PRICE FROM LMS_BOOK_DETAILS WHERE BOOK_CODE = '{BookCode}';";



                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    book.BookTitle = (string)reader["BOOK_TITLE"];
                    book.Author = (string)reader["AUTHOR"];
                    book.Category = (string)reader["CATEGORY"];
                    book.Publication = (string)reader["PUBLICATION"];
                    book.PublicDate = (DateTime)reader["PUBLISHED_DATE"];
                    book.BookEdition = (int)reader["BOOK_EDITION"];
                    book.Price = (int)reader["PRICE"];

                }

            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }
        }
        public void OnPost()
        {
            message = "";
            BookCode = Request.Query["BookCode"];
            try
            {
                string CONN_STRING = "Data Source=DESKTOP-2ESDHDD;Initial Catalog=SQLProject1;Integrated Security=True;Encrypt=False";
                SqlConnection connection = new SqlConnection(CONN_STRING);
                connection.Open();

                book.BookTitle = Request.Form["BookTitle"];
                book.Author = Request.Form["Author"];
                book.Category = Request.Form["Category"];
                book.Publication = Request.Form["Publication"];
                book.PublicDate = Convert.ToDateTime(Request.Form["PublicDate"]);
                book.BookEdition = Convert.ToInt32(Request.Form["BookEdition"]);
                book.Price = Convert.ToInt32(Request.Form["Price"]);

                SqlCommand cmd = connection.CreateCommand();

                Console.WriteLine(book.Category);
                Console.WriteLine(BookCode);

                try
                {
                    cmd.CommandText = $"UPDATE LMS_BOOK_DETAILS SET BOOK_TITLE ='{book.BookTitle}'," +
                        $"AUTHOR = '{book.Author}',CATEGORY = '{book.Category}', PUBLICATION = '{book.Publication}'," +
                        $"PUBLISHED_DATE = '{book.PublicDate}', BOOK_EDITION = {book.BookEdition}, PRICE = {book.Price} WHERE " +
                        $"BOOK_CODE = '{BookCode}'";

                    cmd.ExecuteNonQuery();

                    message = "Book Updated Successfully";

                }
                catch (Exception ex)
                {
                    message = ex.Message;
                    Console.WriteLine(ex.Message);
                }

            }
            catch (Exception ex)
            {
                message = ex.Message;
                Console.WriteLine(ex.Message);
            }

        }
    }
}
