CREATE DATABASE SportivaDB;

USE SportivaDB;

CREATE TABLE Usuarios (
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Email VARCHAR(MAX) NOT NULL,
	Contra VARCHAR(16) NOT NULL
);

INSERT INTO Usuarios (Email, Contra)
VALUES ('admin@gmail.com', 'admin123'), ('guille@gmail.com', 'guille123');

CREATE TABLE Eventos (
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Nombre VARCHAR(32) NOT NULL,
	Fecha DATETIME NOT NULL,
	Ubi VARCHAR(32) NOT NULL,
);

INSERT INTO Eventos (Nombre, Fecha, Ubi)
VALUES ('EcoTrail', '2025-05-01 09:30:00', 'CABA, Buenos Aires');

CREATE TABLE Inscripciones (
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Usuario INT FOREIGN KEY REFERENCES Usuarios,
	Evento INT FOREIGN KEY REFERENCES Eventos,
	Fecha DATETIME NOT NULL
);

INSERT INTO Inscripciones (Usuario, Evento, Fecha)
VALUES (2, 1, '2025-04-01 15:45:00');

SELECT * FROM Usuarios;
SELECT * FROM Eventos;
SELECT * FROM Inscripciones;
