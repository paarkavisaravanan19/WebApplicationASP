using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace WebApplicationASP.Pages.Books
{
    public class CreateBookModel : PageModel
    {
        public string message = "";
        public void OnGet()
        {

        }
        public void OnPost()
        {
            message = "";
            try
            {
                string CONN_STRING = "Data Source=DESKTOP-2ESDHDD;Initial Catalog=SQLProject1;Integrated Security=True;Encrypt=False";
                SqlConnection connection = new SqlConnection(CONN_STRING);
                connection.Open();

                Books books = new Books();

                books.BookCode = Request.Form["BookCode"];
                books.BookTitle = Request.Form["BookTitle"];
                books.Author = Request.Form["Author"];
                books.Category = Request.Form["Category"];
                books.Publication = Request.Form["Publication"];
                books.PublicDate = Convert.ToDateTime(Request.Form["PublicDate"]);
                books.BookEdition = Convert.ToInt32(Request.Form["BookEdition"]);
                books.Price = Convert.ToInt32(Request.Form["Price"]);

                books.RackNum = "A1";
                books.DateArrival = Convert.ToDateTime("2021-04-11");
                books.SupplierId = "S03";

                SqlCommand cmd = connection.CreateCommand();

                try
                {
                    cmd.CommandText = $"INSERT INTO LMS_BOOK_DETAILS(BOOK_CODE,BOOK_TITLE,AUTHOR,CATEGORY,PUBLICATION," +
                        $"PUBLISHED_DATE,BOOK_EDITION,PRICE,RACK_NUM,DATE_ARRIVAL,SUPPLIER_ID)  VALUES ('{books.BookCode}','{books.BookTitle}','{books.Author}'," +
                        $"'{books.Category}','{books.Publication}','{books.PublicDate}',{books.BookEdition},{books.Price},'{books.RackNum}'," +
                        $"'{books.DateArrival}','{books.SupplierId}');";

                    cmd.ExecuteNonQuery();

                    message = "Book Registered Successfully";

                }
                catch (Exception ex)
                {
                    message = ex.Message;
                }

            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

        }
    }

}

