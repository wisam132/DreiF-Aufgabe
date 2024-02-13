using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static WebApplication1.Pages.Mitarbeiter.IndexModel;

namespace WebApplication1.Pages.Mitarbeiter
{
    public class projektModel : PageModel
    {

        public MitarbeiterInfo mitarbeiterInfo = new MitarbeiterInfo();
        public string fehlerMeldung = "";
        public string erfolgMeldung = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            mitarbeiterInfo.Projektname = Request.Form["projektname"];
            mitarbeiterInfo.Startdatum = Request.Form["startdatum"];
            mitarbeiterInfo.Enddatum = Request.Form["enddatum"];
            mitarbeiterInfo.MitarbeiterID = Request.Form["mitarbeiterID"];

            if (string.IsNullOrWhiteSpace(mitarbeiterInfo.Projektname) ||
                string.IsNullOrWhiteSpace(mitarbeiterInfo.Startdatum) ||
                string.IsNullOrWhiteSpace(mitarbeiterInfo.Enddatum) ||
                string.IsNullOrWhiteSpace(mitarbeiterInfo.MitarbeiterID))
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
                    string sql = "INSERT INTO Projekt (Projektname, Startdatum, Enddatum, MitarbeiterID) VALUES (@projektname, @startdatum, @enddatum, @mitarbeiterID)";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@projektname", mitarbeiterInfo.Projektname);
                        command.Parameters.AddWithValue("@startdatum", mitarbeiterInfo.Startdatum);
                        command.Parameters.AddWithValue("@enddatum", mitarbeiterInfo.Enddatum);
                        command.Parameters.AddWithValue("@mitarbeiterID", mitarbeiterInfo.MitarbeiterID);
                        command.ExecuteNonQuery();
                    }
                }

                mitarbeiterInfo.Projektname = "";
                mitarbeiterInfo.Startdatum = "";
                mitarbeiterInfo.Enddatum = "";
                mitarbeiterInfo.MitarbeiterID = "";
                erfolgMeldung = "Projekt wurde erfolgreich hinzugefügt";
                Response.Redirect("/mitarbeiter/Index");
            }
            catch (Exception ex)
            {
                fehlerMeldung = ex.Message;
                return;
            }
        }
    }
}
