namespace Models
{
    public class SymptomeMaladie
    {
        public double  etat_min;
        public  double etat_max; 
        public int Id_Symptome ;
        public string symptome_nom;
        public int Id_Status;

        public SymptomeMaladie(double etat_max, double etat_min,int Id_Sympt,string nom_symptome,int id_status){
            this.etat_max = etat_max;
            this.etat_min = etat_min;
            Id_Symptome = Id_Sympt;
            symptome_nom=nom_symptome;
            Id_Status = id_status;
        }        
    
    }
}