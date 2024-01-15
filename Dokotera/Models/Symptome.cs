using Models.connection;
using Npgsql;

namespace Models
{
    public class Symptome
    {
       public int Id_Symptome { get; set; }
       public string? Nom{ get; set; }
       public Symptome(){}
       public Symptome(int Id_Sympt,string nom){
            this.Id_Symptome = Id_Sympt;
            this.Nom = nom;
       }
        public static List<Symptome> GetAll(BaseConnection basecon){
            List<Symptome> allSymptome = new();
            using (var connection = new NpgsqlConnection(basecon._connectionString))
            {
                connection.Open();
                using var command = new NpgsqlCommand("SELECT * FROM Symptome ", connection);
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    allSymptome.Add(new Symptome(reader.GetInt32(0),reader.GetString(1)));
                }
                connection.Close();
            }
            return allSymptome;
        }
        

    }
}