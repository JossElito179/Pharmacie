CREATE TABLE Patient(
   Id_Patient INT AUTO_INCREMENT,
   nom VARCHAR(50) ,
   PRIMARY KEY(Id_Patient)
);

CREATE TABLE Symptome(
   Id_Symptome INT AUTO_INCREMENT,
   nom VARCHAR(50) ,
   PRIMARY KEY(Id_Symptome)
);

CREATE TABLE Maladie(
   Id_Maladie INT AUTO_INCREMENT,
   nom VARCHAR(50) ,
   PRIMARY KEY(Id_Maladie)
);

CREATE TABLE MaladieSymptome(
   Id_MaladieSymptome INT AUTO_INCREMENT,
   etat INT,
   Id_Maladie INT NOT NULL,
   Id_Symptome INT NOT NULL,
   PRIMARY KEY(Id_MaladieSymptome),
   FOREIGN KEY(Id_Maladie) REFERENCES Maladie(Id_Maladie),
   FOREIGN KEY(Id_Symptome) REFERENCES Symptome(Id_Symptome)
);

CREATE TABLE Medicaments(
   Id_Medicaments INT AUTO_INCREMENT,
   nom VARCHAR(50) ,
   prix DOUBLE,
   PRIMARY KEY(Id_Medicaments)
);

CREATE TABLE MedicamentSymptome(
   Id_MedicamentSymptome INT AUTO_INCREMENT,
   Id_Medicaments INT NOT NULL,
   Id_Symptome INT NOT NULL,
   PRIMARY KEY(Id_MedicamentSymptome),
   FOREIGN KEY(Id_Medicaments) REFERENCES Medicaments(Id_Medicaments),
   FOREIGN KEY(Id_Symptome) REFERENCES Symptome(Id_Symptome)
);

CREATE TABLE PatientSymptome(
   Id_PatientSymptome INT AUTO_INCREMENT,
   Id_Symptome INT NOT NULL,
   Id_Patient INT NOT NULL,
   PRIMARY KEY(Id_PatientSymptome),
   FOREIGN KEY(Id_Symptome) REFERENCES Symptome(Id_Symptome),
   FOREIGN KEY(Id_Patient) REFERENCES Patient(Id_Patient)
);
