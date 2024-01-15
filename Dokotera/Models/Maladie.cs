using Models.connection;
using Npgsql;

namespace Models
{
    public class Maladie
    {
        public int Id_maladie { get;set; }
        public string? Nom { get;set; }
        public List<SymptomeMaladie>? All_symptomes { get;set; }=new List<SymptomeMaladie>();
        public Maladie(int id_maladie, string nom){
            Id_maladie = id_maladie;
            Nom = nom;
        }

        public Maladie(){}

public int retrieveMaladieById_Status(BaseConnection basecon,int status){
    int rep=0;
        using (var connection = new NpgsqlConnection(basecon._connectionString))
            {
                connection.Open();
                using var command = new NpgsqlCommand("SELECT count(Id_status) FROM MaladieSymptome where id_maladie=@para1 and id_status=@para2", connection);
                command.Parameters.AddWithValue("para1", Id_maladie);
                command.Parameters.AddWithValue("para2", status);
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                   rep=reader.GetInt32(0);
                }
                connection.Close();
            }
    return rep;
}

public static List<Maladie> GetAll(BaseConnection connection)
{
    List<Maladie> result = new();
    using (NpgsqlConnection connex = new NpgsqlConnection(connection._connectionString))
    {
        connex.Open();

        using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT DISTINCT maladiesymptome.Id_maladie, maladie.nom " +
                                                    "FROM maladiesymptome JOIN maladie ON maladie.id_maladie = maladiesymptome.id_maladie;", connex))
        {
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                result.Add(new Maladie(reader.GetInt32(0), reader.GetString(1)));
            }
        }

        foreach (var maladie in result)
        {
            maladie.All_symptomes=new List<SymptomeMaladie>();
            using NpgsqlCommand cmdSymptomes = new NpgsqlCommand("SELECT * FROM symptomemaladie WHERE id_maladie = @para1", connex);
            cmdSymptomes.Parameters.AddWithValue("para1", maladie.Id_maladie);

            using var readerSymptomes = cmdSymptomes.ExecuteReader();
            while (readerSymptomes.Read()){
                maladie.All_symptomes.Add(new SymptomeMaladie(readerSymptomes.GetDouble(0),
                                                              readerSymptomes.GetDouble(1),  
                                                              readerSymptomes.GetInt32(2),
                                                              readerSymptomes.GetString(3),
                                                              readerSymptomes.GetInt32(5)));
            
                Console.WriteLine(readerSymptomes.GetDouble(5)+" counter id_status");
            }
        }

        connex.Close();
    }

    return result;
}

    }
}