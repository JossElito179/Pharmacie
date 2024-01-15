namespace Models
{
    public class PropositionPrix
    {
        public int Id_Symptome{get;set;}
        public string nom_medoc;
        public double Prixtotal{get;set;}
        public double Etat{get;set;}
        public double Quantite{get;set;}
        public PropositionPrix(int id_sypmt,string nom_medoc,double prix_t,double etat,double quantite){
            Id_Symptome=id_sypmt;
            this.nom_medoc=nom_medoc;
            Prixtotal=prix_t;
            Etat=etat;
            Quantite=quantite;
        }
    }
}