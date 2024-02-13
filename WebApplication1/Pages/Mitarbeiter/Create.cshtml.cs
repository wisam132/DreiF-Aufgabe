using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static WebApplication1.Pages.Mitarbeiter.IndexModel;

namespace WebApplication1.Pages.Mitarbeiter
{
    public class CreateModel : PageModel
    {
        public MitarbeiterInfo mitarbeiterInfo = new MitarbeiterInfo();
        public string fehlerMeldung = "";
        public string erfolgMeldung = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            mitarbeiterInfo.Vorname = Request.Form["vorname"];
            mitarbeiterInfo.Nachname = Request.Form["nachname"];
            mitarbeiterInfo.AbteilungID = Request.Form["abteilungID"];

            if (string.IsNullOrWhiteSpace(mitarbeiterInfo.Vorname) ||
                string.IsNullOrWhiteSpace(mitarbeiterInfo.Nachname) ||
                string.IsNullOrWhiteSpace(mitarbeiterInfo.AbteilungID))
            {
                fehlerMeldung = "Bitte alle Felder ausfüllen";
                return;
            }



            try
            {
                string ConnectionString = "Data Source=WISAMSCOMPUTER;Initial Catalog=projekt;Integrated Security=True;";

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO Mitarbeiter (Vorname, Nachname, AbteilungID) VALUES (@vorname, @nachname, @abteilungID);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        command.Parameters.AddWithValue("@vorname", mitarbeiterInfo.Vorname);
                        command.Parameters.AddWithValue("@nachname", mitarbeiterInfo.Nachname);
                        command.Parameters.AddWithValue("@abteilungID", mitarbeiterInfo.AbteilungID);
                        command.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                fehlerMeldung = ex.Message;
                return;
            }

            mitarbeiterInfo.Vorname = "";
            mitarbeiterInfo.Nachname = "";
            mitarbeiterInfo.AbteilungID = "";
            erfolgMeldung = "Mitarbeiter wurde erfolgreich hinzugefügt";
            Response.Redirect("/mitarbeiter/Index");

        }

    }
}
