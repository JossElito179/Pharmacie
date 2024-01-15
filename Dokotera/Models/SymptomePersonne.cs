using Models.connection;
using Npgsql;

namespace Models
{
    public class SymptomePersonne
    {

        public int Id_Symptome{ get; set; }
        public int Etat{ get; set; }
        public List<PropositionPrix> Allprix{get;set;}=new List<PropositionPrix>();
        public PropositionPrix? Prix_etat_vrai{get; set;}
        public SymptomePersonne(int Id_Sympt, int Etat){
            Id_Symptome=Id_Sympt;
            this.Etat= Etat;
        }
        public SymptomePersonne(){}
        public void GetPEVrai(string priorite){
            Prix_etat_vrai=Allprix[0];
            if (priorite.Equals("Prix"))
            {
                foreach (var item in Allprix)
                {
                    if (Prix_etat_vrai.Prixtotal>item.Prixtotal)
                    {
                        Prix_etat_vrai=item;
                    }
                }   
            }else if(priorite.Equals("Efficacite"))
            {
                foreach (var item in Allprix)
                {
                    if (Prix_etat_vrai.Quantite/Prix_etat_vrai.Prixtotal>item.Quantite/item.Prixtotal)
                    {
                        Prix_etat_vrai=item;
                    }
                }   
            }
        }    
    
        public List<MedicamentSymptome> GetMedicamentBy(BaseConnection basecon){
            List<MedicamentSymptome> allMedc=new();
            using (var connection = new NpgsqlConnection(basecon._connectionString))
            {
                connection.Open();
                using var command = new NpgsqlCommand("SELECT * FROM SymptomeMedocNom where id_symptome=@para1", connection);
                command.Parameters.AddWithValue("para1",Id_Symptome);
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    allMedc.Add(new MedicamentSymptome(reader.GetInt32(0),
                                                       reader.GetDouble(1),
                                                       reader.GetDouble(2),
                                                       reader.GetInt32(3),
                                                       reader.GetInt32(4),
                                                       reader.GetString(5),
                                                       reader.GetDouble(6)));
                    Console.WriteLine(reader.GetInt32(0)+","+
                                    reader.GetDouble(1)+","+
                                    reader.GetDouble(2)+","+
                                    reader.GetInt32(3)+","+
                                    reader.GetInt32(4)+","+
                                    reader.GetString(5)+","+
                                    reader.GetDouble(6)+",");
                }
                connection.Close();
            }
            return allMedc;
        }
    }
}