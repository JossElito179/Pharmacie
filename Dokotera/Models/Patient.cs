using System.Collections;
using Models.connection;
using Npgsql;

namespace Models
{
    public class Patient
    {
        public int Id_Patient{ get; set;}
        public string? Nom{ get; set;}
        public int Id_Status{ get; set;}
        public List<SymptomePersonne> Allsymptome { get; set; }= new List<SymptomePersonne>();
        public List<Maladie> Maladies{get;set;}=new List<Maladie>();
        public List<SymptomePersonne> Allcontre { get; set; }= new List<SymptomePersonne>();
        public List<SymptomePersonne> AllSymptomeMedoc{ get; set; }= new List<SymptomePersonne>();
        public Patient(int id,string nom,int Id_Status){
            Id_Patient=id;
            Nom=nom;
            this.Id_Status=Id_Status;
        }
        
        public Patient ()
        {
        }

        public static List<Patient> GetAll(BaseConnection basecon){
            List<Patient> allPatient = new();
            using (var connection = new NpgsqlConnection(basecon._connectionString))
            {
                connection.Open();
                using var command = new NpgsqlCommand("SELECT * FROM Patient ", connection);
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    allPatient.Add(new Patient(reader.GetInt32(0),reader.GetString(1),reader.GetInt32(2)));
                }
                connection.Close();
            }
            return allPatient;
        }

        public static Patient FindById(BaseConnection basecon,int id){
            Patient patient=new();
           using (var connection = new NpgsqlConnection(basecon._connectionString))
            {
                connection.Open();
                using var command = new NpgsqlCommand("SELECT * FROM Patient where id_patient=@para1", connection);
                command.Parameters.AddWithValue("@para1",id);
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    patient=new Patient(reader.GetInt32(0),reader.GetString(1),reader.GetInt32(2));
                }
                connection.Close();
            }
            return patient;
        }
        public void GetAllSymptome(BaseConnection basecon){
            List<SymptomePersonne> allSymptome = new();
            using (var connection = new NpgsqlConnection(basecon._connectionString))
            {
                connection.Open();
                using var command = new NpgsqlCommand("SELECT * FROM PatientSymptome where id_patient=@para1", connection);
                command.Parameters.AddWithValue("para1", this.Id_Patient);
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    allSymptome.Add(new SymptomePersonne(reader.GetInt32(1),reader.GetInt32(3)));
                }
                connection.Close();
            }
            Allsymptome=allSymptome;
        }

        public void GetMaladies(BaseConnection basecon){
            int var=0;
            List<Maladie> allmaladies=Maladie.GetAll(basecon);
            List<Maladie> maladies_atteint=new();
            GetAllSymptome(basecon);
            foreach (var allmaladie in allmaladies)
            {
                var=0;
#pragma warning disable CS8602 
                if (allmaladie.All_symptomes.Count == Allsymptome.Count)
                {   
                    foreach (var maladie in allmaladie.All_symptomes)
                    {
                        foreach (var symptome1 in Allsymptome)
                        {
                            if ( maladie.Id_Status==Id_Status)
                            {
                                if ( symptome1.Id_Symptome == maladie.Id_Symptome && symptome1.Etat >= maladie.etat_min && symptome1.Etat <= maladie.etat_max)
                                {
                                    var++;
                                }   
                            }
                        }
                    }
                }
#pragma warning restore CS8602
                if (var==allmaladie.retrieveMaladieById_Status(basecon,this.Id_Status) && var>0)
                {
                    maladies_atteint.Add(allmaladie);
                }
            }
            Maladies=maladies_atteint;
        }

        public void GetAllMedoc(BaseConnection basecon,string priorite){
            foreach (var symptome in Allsymptome)
            {
                List<MedicamentSymptome> medicaments=symptome.GetMedicamentBy(basecon);
                foreach (var medoc in medicaments)
                {
                    double etat_provisoir=0;
                    while (etat_provisoir<symptome.Etat)
                    {
                        etat_provisoir+=medoc.Etat_symptome/medoc.Quantite_medoc;
                    }
                    double Quantite=(medoc.Quantite_medoc/medoc.Etat_symptome)*etat_provisoir;
                    double Prixtotal=Quantite*medoc.prixu;
                    Console.WriteLine(medoc.prixu+" , "+ medoc.Nom_medoc);
        symptome.Allprix.Add(new PropositionPrix(symptome.Id_Symptome,medoc.Nom_medoc,Prixtotal,etat_provisoir,Quantite));
                }
                symptome.GetPEVrai(priorite);
            }
        }

        public void SetAllSymptContre(BaseConnection connex){
            foreach (var item in Allsymptome)
            {
#pragma warning disable CS8602 
                if (item.Prix_etat_vrai.Etat>item.Etat)
                {
                    Allcontre.Add(new SymptomePersonne(item.GetContreIndication(connex),Int32.Parse(item.Prix_etat_vrai.Etat-item.Etat+"")));
                }
#pragma warning restore CS8602 
            }
        }

        public void GetAllMedoContre(BaseConnection basecon,string priorite){
            foreach (var symptome in Allcontre)
            {
                List<MedicamentSymptome> medicaments=symptome.GetMedicamentBy(basecon);
                foreach (var medoc in medicaments)
                {
                    double etat_provisoir=0;
                    while (etat_provisoir<symptome.Etat)
                    {
                        etat_provisoir+=medoc.Etat_symptome/medoc.Quantite_medoc;
                    }
                    double Quantite=(medoc.Quantite_medoc/medoc.Etat_symptome)*etat_provisoir;
                    double Prixtotal=Quantite*medoc.prixu;
                    Console.WriteLine(medoc.prixu+" , "+ medoc.Nom_medoc);
        symptome.Allprix.Add(new PropositionPrix(symptome.Id_Symptome,medoc.Nom_medoc,Prixtotal,etat_provisoir,Quantite));
                }
                symptome.GetPEVrai(priorite);
            }
        }

        public void GetAllNecessary(BaseConnection connex,string priorite){
            GetAllMedoc(connex, priorite);
            SetAllSymptContre(connex);
            GetAllMedoContre(connex, priorite);
        }

        }
    }