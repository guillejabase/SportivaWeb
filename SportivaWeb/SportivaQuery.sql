CREATE DATABASE SportivaDB;

USE SportivaDB;

CREATE TABLE Usuarios (
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Nombre VARCHAR(16) NOT NULL UNIQUE,
	Email VARCHAR(128) NOT NULL UNIQUE,
	Contra VARCHAR(16) NOT NULL
);

INSERT INTO Usuarios (Nombre, Email, Contra)
VALUES ('admin', 'admin@gmail.com', 'admin123');

CREATE TABLE Provincias (
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Nombre VARCHAR(19) NOT NULL UNIQUE
);

INSERT INTO Provincias (Nombre)
VALUES ('Buenos Aires'), ('Capital Federal'), ('Catamarca'), ('Chaco'),
       ('Chubut'), ('Córdoba'), ('Corrientes'), ('Entre Ríos'),
	   ('Formosa'), ('Jujuy'), ('La Pampa'), ('La Rioja'),
	   ('Mendoza'), ('Misiones'), ('Neuquén'), ('Río Negro'),
	   ('Salta'), ('San Juan'), ('San Luis'), ('Santa Cruz'),
	   ('Santa Fe'), ('Santiago del Estero'), ('Tierra del Fuego'),
	   ('Tucumán');

CREATE TABLE Sexos (
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Nombre VARCHAR(6) NOT NULL UNIQUE
);

INSERT INTO Sexos (Nombre)
VALUES ('Hombre'), ('Mujer');

CREATE TABLE Eventos (
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Nombre VARCHAR(64) NOT NULL,
	Descri VARCHAR(1024),
	FechaApeIns DATE NOT NULL,
	FechaCieIns DATE NOT NULL,
	FechaInicio DATETIME2(0) NOT NULL,
	Provincia INT NOT NULL,
	Imagen VARCHAR(128),

	CONSTRAINT FK_Eventos_Provincia FOREIGN KEY (Provincia) REFERENCES Provincias(Id)
);

INSERT INTO Eventos (Nombre, Descri, FechaApeIns, FechaCieIns, FechaInicio, Provincia, Imagen)
VALUES ('EcoTrail', '', '2025-08-05', '2025-08-07', '2025-09-07T09:30:00', 1, '1.png'),
       ('Soberana', '', '2025-10-11', '2025-10-13', '2025-11-13T14:30:00', 12, '2.png');

CREATE TABLE Inscriptos (
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Usuario INT NOT NULL,
	Nombres VARCHAR(32) NOT NULL,
	Apellidos VARCHAR(32) NOT NULL,
	Provincia INT NOT NULL,
	CodPostal INT NOT NULL,
	Documento INT NOT NULL,
	FechaNaci DATETIME2(0) NOT NULL,
	Sexo INT NOT NULL,
	Telefono VARCHAR(16) NOT NULL,

	CONSTRAINT FK_Inscriptos_Usuario FOREIGN KEY (Usuario) REFERENCES Usuarios(Id),
	CONSTRAINT FK_Inscriptos_Provincia FOREIGN KEY (Provincia) REFERENCES Provincias(Id),
	CONSTRAINT FK_Inscriptos_Sexo FOREIGN KEY (Sexo) REFERENCES Sexos(Id)
);

CREATE TABLE Inscripciones (
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Inscripto INT NOT NULL UNIQUE,
	Evento INT NOT NULL,
	Fecha DATETIME2(0) NOT NULL,

	CONSTRAINT FK_Inscripciones_Inscripto FOREIGN KEY (Inscripto) REFERENCES Inscriptos(Id),
	CONSTRAINT FK_Inscripciones_Evento FOREIGN KEY (Evento) REFERENCES Eventos(Id)
);
