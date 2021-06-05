using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using System.Configuration;

namespace MeetingOrganizer
{
    public class DataBaseSetting
    {
        public static IEnumerable<Spotkanie> pokazSpotkania()
        {
            using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {
                return connection.Query<Spotkanie>("select*from spotkania");
            }
        }
        public static IEnumerable<Osoba> pokazOsoby()
        {
            using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {
                return connection.Query<Osoba>("select*from Osoby");
            }
        }
        public static IEnumerable<Aktualne> pokazAktualne(Aktualne actual)
        {
            using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {
                return connection.Query<Aktualne>(@"SELECT a.IdOsoby, Imie,Nazwisko, AdresEmail, IdSpotkania
FROM dbo.Aktualne AS A INNER JOIN DBO.Osoby AS O ON A.IdOsoby = O.IdOsoby WHERE IdSpotkania=@IdSpotkania", new { IdSpotkania = actual.IdSpotkania });

            }
        }
        public static void DodajSpotkanie(Spotkanie meeting)
        {
                using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
                {
                    connection.Execute("Insert INTO dbo.Spotkania (NazwaSpotkania,GodzinaRozpoczecia,GodzinaZakonczenia,Miejsce)" + "VALUES(@NazwaSpotkania,@GodzinaRozpoczecia,@GodzinaZakonczenia,@Miejsce)", new
                    {
                        NazwaSpotkania=meeting.NazwaSpotkania,
                        GodzinaRozpoczecia=meeting.GodzinaRozpoczecia,
                        GodzinaZakonczenia=meeting.GodzinaZakonczenia,
                        Miejsce=meeting.Miejsce,
                    });
                }
        }
        public static void UsunSpotkanie(Spotkanie meeting)
        {
            using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {
                connection.Execute($"DELETE FROM dbo.Spotkania WHERE IdSpotkania=@IdSpotkania", new {IdSpotkania=meeting.IdSpotkania});
            }
        }

        public static void DodajOsobe(Osoba person)
        {
            using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {
                connection.Execute("Insert INTO dbo.Osoby (Imie,Nazwisko,AdresEmail)" + "VALUES(@Imie,@Nazwisko,@AdresEmail)", new
                {
                    Imie=person.Imie,
                    Nazwisko=person.Nazwisko,
                    AdresEmail=person.AdresEmail,
                });
            }
        }
        public static void UsunOsobe(Osoba person)
        {
            using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {
                connection.Execute($"DELETE FROM dbo.Osoby WHERE IdOsoby=@IdOsoby", new { IdOsoby = person.IdOsoby });
            }
        }
        public static void DodajAktualne(Aktualne actual)
        {
            using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {     
                connection.Execute($@"IF EXISTS(SELECT * FROM DBO.Aktualne WHERE IdOsoby=@IdOsoby AND IdSpotkania=@IdSpotkania)
RAISERROR('Istnieje już', 16, 1);

                If NOT EXISTS(SELECT * FROM DBO.Aktualne WHERE IdOsoby = @IdOsoby AND IdSpotkania = @IdSpotkania)
Insert INTO dbo.Aktualne(IdOsoby, IdSpotkania)" + "VALUES(@IdOsoby, @IdSpotkania)", new
                {
                   IdOsoby=actual.IdOsoby,
                   IdSpotkania=actual.IdSpotkania,
                });

            }
        }
        public static void UsunZeSpotkania(Aktualne actual)
        {
            using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {
                connection.Execute($"DELETE FROM dbo.Aktualne WHERE IdOsoby=@IdOsoby", new { IdOsoby = actual.IdOsoby });
            }
        }

        public static int IleOsob(Aktualne actual)
        {
            using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {
                return (int)connection.ExecuteScalar($"SELECT COUNT(*) FROM Aktualne WHERE IdSpotkania=@IdSpotkania", new { IdSpotkania = actual.IdSpotkania });
            }
        }
    }
}
