using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace WebApplicationASP.Pages.Books
{
    public class IndexPageModel : PageModel
    {
        public List<Books> booksList;
        public void OnGet()

        {
            try
            {
                booksList = new List<Books>();
                string CONN_STRING = "Data Source=DESKTOP-2ESDHDD;Initial Catalog=SQLProject1;Integrated Security=True;Encrypt=False";
                SqlConnection connection = new SqlConnection(CONN_STRING);
                connection.Open();

                SqlCommand cmd = connection.CreateCommand();

                cmd.CommandText = "SELECT BOOK_CODE,BOOK_TITLE,CATEGORY, AUTHOR,PUBLICATION,PRICE FROM LMS_BOOK_DETAILS;";

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Books book = new Books();

                    book.BookCode = (string)reader["BOOK_CODE"];
                    book.BookTitle = (string)reader["BOOK_TITLE"];
                    book.Category = (string)reader["CATEGORY"];
                    book.Author = (string)reader["AUTHOR"];
                    book.Publication = (string)reader["PUBLICATION"];
                    book.Price = (int)reader["PRICE"];

                    booksList.Add(book);
                }

            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }
        }
    }

public class Books
{
    public string BookCode { get; set; }
    public string BookTitle { get; set; }
    public string Author { get; set; }
    public string Category { get; set; }
    public string Publication { get; set; }
    public DateTime PublicDate { get; set; }
    public int BookEdition { get; set; }
    public int Price { get; set; }
    public string RackNum { get; set; }
    public DateTime DateArrival { get; set; }
    public string SupplierId { get; set; }
}
}

