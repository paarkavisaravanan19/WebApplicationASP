using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace WebApplicationASP.Pages.Books
{
    public class DeletePageModel : PageModel
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
        public void Onpost()
        {
            BookCode = Request.Query["bookCode"];

            try
            {
                string CONN_STRING = "Data Source=DESKTOP-2ESDHDD;Initial Catalog=SQLProject1;Integrated Security=True;Encrypt=False";
                SqlConnection conn = new SqlConnection(CONN_STRING);
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                Console.WriteLine(BookCode);

                cmd.CommandText = $"DELETE FROM LMS_BOOK_DETAILS WHERE BOOK_CODE = '{BookCode}'";
                int rowsAffected = cmd.ExecuteNonQuery();
                Console.WriteLine(rowsAffected);
                if (rowsAffected > 0)
                {
                    Response.Redirect("/Books/IndexPage");
                }
            }
            catch (SqlException se)
            {
                Console.WriteLine(se.Message);
            }
        }
    }
    }
    

