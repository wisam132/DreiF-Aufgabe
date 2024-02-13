using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace WebApplication1.Pages.Mitarbeiter
{
    public class IndexModel : PageModel
    {
        public List<MitarbeiterInfo> mitarbeiterListe = new List<MitarbeiterInfo>();

        public void OnGet()
        {
            try
            {
                string ConnectionString = "Data Source=WISAMSCOMPUTER;Initial Catalog=projekt;Integrated Security=True;";

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    string sql = "SELECT Mitarbeiter.MitarbeiterID, Mitarbeiter.AbteilungID, Mitarbeiter.Vorname, Mitarbeiter.Nachname, Abteilung.Abteilungsname, projekt.Projektname, projekt.Startdatum, projekt.Enddatum " +
                                 "FROM Mitarbeiter " +
                                 "LEFT JOIN Abteilung ON Mitarbeiter.AbteilungID = Abteilung.AbteilungID " +
                                 "LEFT JOIN projekt ON Mitarbeiter.MitarbeiterID = projekt.MitarbeiterID " +
                                 "ORDER BY Abteilung.Abteilungsname, Mitarbeiter.Vorname, Mitarbeiter.Nachname;";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                MitarbeiterInfo info = new MitarbeiterInfo();
                                info.MitarbeiterID = reader.GetInt32(0).ToString();
                                info.AbteilungID = reader.GetInt32(1).ToString();
                                info.Vorname = reader.GetString(2);
                                info.Nachname = reader.GetString(3);
                                info.Abteilungsname = reader.GetString(4);
                                info.Projektname = reader.IsDBNull(5) ? "" : reader.GetString(5);
                                info.Startdatum = reader.IsDBNull(6) ? "" : reader.GetDateTime(6).ToString("yyyy-MM-dd");
                                info.Enddatum = reader.IsDBNull(7) ? "" : reader.GetDateTime(7).ToString("yyyy-MM-dd");

                                mitarbeiterListe.Add(info);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fehler: " + ex.ToString());
            }
        }

        public class MitarbeiterInfo
        {
            public string MitarbeiterID;
            public string Vorname;
            public string Nachname;
            public string AbteilungID;
            public string Abteilungsname;
            public string Projektname;
            public string Startdatum;
            public string Enddatum;
        }
    }
}
