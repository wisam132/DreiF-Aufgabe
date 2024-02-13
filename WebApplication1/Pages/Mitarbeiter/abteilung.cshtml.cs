using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data.SqlClient;
using static WebApplication1.Pages.Mitarbeiter.IndexModel;

namespace WebApplication1.Pages.Mitarbeiter
{
    public class abteilungModel : PageModel
    {
        public MitarbeiterInfo mitarbeiterInfo = new MitarbeiterInfo();
        public string fehlerMeldung = "";
        public string erfolgMeldung = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            mitarbeiterInfo.Abteilungsname = Request.Form["abteilungsname"];

            if (string.IsNullOrWhiteSpace(mitarbeiterInfo.Abteilungsname))
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
                    string sql = "INSERT INTO Abteilung (Abteilungsname) VALUES (@abteilungsname)";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@abteilungsname", mitarbeiterInfo.Abteilungsname);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                fehlerMeldung = ex.Message;
                return;
            }

            mitarbeiterInfo.Abteilungsname = "";
            erfolgMeldung = "Abteilung wurde erfolgreich hinzugefügt";
            Response.Redirect("/mitarbeiter/Index");
        }
    }
}
