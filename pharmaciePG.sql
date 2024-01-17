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
   etat INTEGER,
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
   PRIMARY KEY(Id_PatientSymptome),
   FOREIGN KEY(Id_Symptome) REFERENCES Symptome(Id_Symptome),
   FOREIGN KEY(Id_Patient) REFERENCES Patient(Id_Patient)
);
