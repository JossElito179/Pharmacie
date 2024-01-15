namespace Models
{
    public class MedicamentSymptome
    {
        public int Id_MedicamentSymptome{get;set;}
        public double Etat_symptome {get;set;}
        public double Quantite_medoc {get;set;}
        public int Id_Medicaments {get;set;}
        public int Id_Symptome {get;set;}
        public string Nom_medoc {get;set;}
        public double prixu;


    
        public MedicamentSymptome(int idmec,double etat_s,double quantite_medoc,int id_medoc,int id_sympt,string nom,double prixu){
            Id_MedicamentSymptome = idmec;
            Etat_symptome = etat_s;
            Quantite_medoc=quantite_medoc;
            Id_Medicaments=id_medoc;
            Id_Symptome=id_sympt;
            Nom_medoc=nom;
            this.prixu = prixu;
        }
    
    }
}