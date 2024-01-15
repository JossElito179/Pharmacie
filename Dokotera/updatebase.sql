CREATE database pharmacie;

\c pharmacie;

CREATE TABLE Patient(
   Id_Patient SERIAL,
   nom VARCHAR(50) ,
   PRIMARY KEY(Id_Patient)
);

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

CREATE TABLE MaladieSymptome(
   Id_MaladieSymptome SERIAL,
   etat_min DOUBLE PRECISION,
   etat_max DOUBLE PRECISION,
   Id_Maladie INTEGER NOT NULL,
   Id_Symptome INTEGER NOT NULL,
   PRIMARY KEY(Id_MaladieSymptome),
   FOREIGN KEY(Id_Maladie) REFERENCES Maladie(Id_Maladie),
   FOREIGN KEY(Id_Symptome) REFERENCES Symptome(Id_Symptome)
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

CREATE TABLE PatientSymptome(
   Id_PatientSymptome SERIAL,
   Id_Symptome INTEGER NOT NULL,
   Id_Patient INTEGER NOT NULL,
   etat INTEGER NOT NULL,
   PRIMARY KEY(Id_PatientSymptome),
   FOREIGN KEY(Id_Symptome) REFERENCES Symptome(Id_Symptome),
   FOREIGN KEY(Id_Patient) REFERENCES Patient(Id_Patient)
);

INSERT INTO Patient (nom) VALUES ('Patient1'), ('Patient2'), ('Patient3');

INSERT INTO Symptome (nom) VALUES ('Symptome1'), ('Symptome2'), ('Symptome3');

INSERT INTO Maladie (nom) VALUES ('Maladie1'), ('Maladie2'), ('Maladie3');

INSERT INTO MaladieSymptome (etat_min, etat_max, Id_Maladie, Id_Symptome) VALUES 
    (3.0, 6.0, 1, 1),
    (4.0, 7.0, 1, 2),
    (1.5, 7.5, 2, 2),
    (5.0, 9.0, 3, 3);

INSERT INTO Medicaments (nom, prix) VALUES ('Medicament1', 10.0), ('Medicament2', 20.0), ('Medicament3', 30.0);

INSERT INTO MedicamentSymptome (etat_symptome, quantite_medoc, Id_Medicaments, Id_Symptome) VALUES 
    (1, 5.0, 1, 1),
    (3.5, 2.0, 2, 2),
    (2.0, 10.0, 3, 3);

INSERT INTO PatientSymptome (Id_Symptome, Id_Patient, etat) VALUES 
    (1, 1, 4),
    (2, 1, 5),
    (3, 2, 2),
    (1, 3, 1);

CREATE or replace VIEW SymptomeMaladie as select 
   etat_max,etat_min,MaladieSymptome.Id_Symptome , Maladie.nom,
   MaladieSymptome.Id_Maladie
   from MaladieSymptome join Maladie on 
   MaladieSymptome.Id_Maladie = Maladie.Id_Maladie;

SELECT DISTINCT maladiesymptome.Id_maladie, maladie.nom FROM maladiesymptome JOIN maladie ON maladie.id_maladie = maladiesymptome.id_maladie;