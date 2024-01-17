CREATE database pharmacie;

\c pharmacie;

CREATE TABLE Symptome(
   Id_Symptome SERIAL,
   nom VARCHAR(50) ,
   PRIMARY KEY(Id_Symptome)
);

CREATE TABLE Maladie(
   Id_Maladie SERIAL,
   nom VARCHAR(50) ,
   PRIMARY KEY(Id_Maladie)
);

CREATE TABLE Medicaments(
   Id_Medicaments SERIAL,
   nom VARCHAR(50) ,
   prix DOUBLE PRECISION,
   PRIMARY KEY(Id_Medicaments)
);

CREATE TABLE MedicamentSymptome(
   Id_MedicamentSymptome SERIAL,
   etat_symptome DOUBLE PRECISION,
   quantite_medoc DOUBLE PRECISION,
   Id_Medicaments INTEGER NOT NULL,
   Id_Symptome INTEGER NOT NULL,
   PRIMARY KEY(Id_MedicamentSymptome),
   FOREIGN KEY(Id_Medicaments) REFERENCES Medicaments(Id_Medicaments),
   FOREIGN KEY(Id_Symptome) REFERENCES Symptome(Id_Symptome)
);

CREATE TABLE Status(
   Id_Status SERIAL,
   nom VARCHAR(50) ,
   PRIMARY KEY(Id_Status)
);

CREATE TABLE Patient(
   Id_Patient SERIAL,
   nom VARCHAR(50) ,
   Id_Status INTEGER NOT NULL,
   PRIMARY KEY(Id_Patient),
   FOREIGN KEY(Id_Status) REFERENCES Status(Id_Status)
);

CREATE TABLE MaladieSymptome(
   Id_MaladieSymptome SERIAL,
   etat_min DOUBLE PRECISION,
   etat_max DOUBLE PRECISION,
   Id_Status INTEGER NOT NULL,
   Id_Maladie INTEGER NOT NULL,
   Id_Symptome INTEGER NOT NULL,
   PRIMARY KEY(Id_MaladieSymptome),
   FOREIGN KEY(Id_Status) REFERENCES Status(Id_Status),
   FOREIGN KEY(Id_Maladie) REFERENCES Maladie(Id_Maladie),
   FOREIGN KEY(Id_Symptome) REFERENCES Symptome(Id_Symptome)
);

CREATE TABLE PatientSymptome(
   Id_PatientSymptome SERIAL,
   Id_Symptome INTEGER NOT NULL,
   Id_Patient INTEGER NOT NULL,
   etat INTEGER NOT NULL,
   PRIMARY KEY(Id_PatientSymptome),
   FOREIGN KEY(Id_Symptome) REFERENCES Symptome(Id_Symptome),
   FOREIGN KEY(Id_Patient) REFERENCES Patient(Id_Patient)
);

CREATE TABLE ContreIndication(
   Id_ContreIndication SERIAL PRIMARY KEY,
   Id_SymptomeSource int not null,
   Id_SymptomeCible int not null,
   FOREIGN KEY (Id_SymptomeSource) REFERENCES Symptome (Id_Symptome),
   FOREIGN KEY (Id_SymptomeCible) REFERENCES Symptome (Id_Symptome) 
);

INSERT INTO Status (nom) VALUES ('adulte'), ('enfant');

INSERT INTO Patient (nom,Id_Status) VALUES ('Patient1',1), ('Patient2',1), ('Patient3',2);

INSERT INTO Symptome (nom) VALUES ('Symptome1'), ('Symptome2'), ('Symptome3');

INSERT INTO Maladie (nom) VALUES ('Maladie1'), ('Maladie2'), ('Maladie3');

INSERT INTO MaladieSymptome (etat_min, etat_max, Id_Maladie, Id_Symptome,Id_Status) VALUES 
    (1.0, 6.0, 1, 1 , 1),
    (4.0, 7.0, 1, 2 , 1),
    (2.0, 8.5, 2, 1 , 2),
    (1.0, 6.0, 2, 2 , 2);

INSERT INTO Medicaments (nom, prix) VALUES ('Medicament1', 10.0), ('Medicament2', 20.0), ('Medicament3', 30.0);

INSERT INTO MedicamentSymptome (etat_symptome, quantite_medoc, Id_Medicaments, Id_Symptome) VALUES 
    (1, 1.0, 1, 1),
    (3, 1.0, 2, 2),
    (2, 1.0, 3, 2),
    (2, 1.0, 3, 3);

INSERT INTO PatientSymptome (Id_Symptome, Id_Patient, etat) VALUES 
    (1, 1, 5),
    (2, 1, 7),
    (3, 2, 2),
    (1, 3, 3),
    (2, 3, 5);

INSERT INTO ContreIndication VALUES 
    (default,1,2),
    (default,2,3);

CREATE or replace VIEW SymptomeMaladie as select 
   etat_max,etat_min,MaladieSymptome.Id_Symptome , Maladie.nom,
   MaladieSymptome.Id_Maladie,MaladieSymptome.Id_Status
   from MaladieSymptome join Maladie on 
   MaladieSymptome.Id_Maladie = Maladie.Id_Maladie;

CREATE VIEW SymptomeMedocNom as select 
   MedicamentSymptome.Id_MedicamentSymptome , MedicamentSymptome.etat_symptome, MedicamentSymptome.quantite_medoc, MedicamentSymptome.Id_Medicaments, MedicamentSymptome.Id_Symptome,
   Medicaments.nom , Medicaments.prix
   from MedicamentSymptome join Medicaments on
   MedicamentSymptome.Id_Medicaments =Medicaments.Id_Medicaments;