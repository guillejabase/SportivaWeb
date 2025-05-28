-- query 1
CREATE DATABASE SportivaDB;
-- end query

-- query 2
USE SportivaDB;
-- end query

-- query 3
CREATE TABLE Roles (
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Nombre VARCHAR(13) NOT NULL UNIQUE
);

INSERT INTO Roles (Nombre)
VALUES ('Administrador'), ('Organizador');

CREATE TABLE Usuarios (
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Nombre VARCHAR(16) NOT NULL UNIQUE,
	Email VARCHAR(64) NOT NULL UNIQUE,
	Contra VARCHAR(16) NOT NULL,
	Rol INT NOT NULL,

	CONSTRAINT FK_Usuarios_Rol FOREIGN KEY (Rol) REFERENCES Roles(Id)
);

INSERT INTO Usuarios (Nombre, Email, Contra, Rol)
VALUES ('admin', 'admin@gmail.com', 'admin123', 1),
       ('guille', 'guille@gmail.com', 'guille12', 2),
	   ('maxi', 'maxi@gmail.com', 'maxi1234', 2);

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
	Descri VARCHAR(2048),
	FechaApeIns DATE NOT NULL,
	FechaCieIns DATE NOT NULL,
	FechaInicio DATETIME2(0) NOT NULL,
	Provincia INT NOT NULL,
	Imagen VARCHAR(128),
	Precio DECIMAL(18,2) NOT NULL,
	Usuario INT NOT NULL,

	CONSTRAINT FK_Eventos_Provincia FOREIGN KEY (Provincia) REFERENCES Provincias(Id),
	CONSTRAINT FK_Eventos_Usuario FOREIGN KEY (Usuario) REFERENCES Usuarios(Id)
);

INSERT INTO Eventos (Nombre, Descri, FechaApeIns, FechaCieIns, FechaInicio, Provincia, Imagen, Precio, Usuario)
VALUES ('ecoTrail', '', '2025-08-05', '2025-08-07', '2025-09-07T09:30:00', 1, '1.jpg', 30000, 1),
       ('Soberana', '', '2025-10-11', '2025-10-13', '2025-11-13T14:30:00', 12, '2.jpg', 32000, 3),
	   ('Brutal Race', '', '2025-08-09', '2025-08-11', '2025-09-13T09:00:00', 6, '3.jpg', 25000, 3);

CREATE TABLE Inscriptos (
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Email VARCHAR(64) NOT NULL,
	Nombres VARCHAR(32) NOT NULL,
	Apellidos VARCHAR(32) NOT NULL,
	Provincia INT NOT NULL,
	CodPostal INT NOT NULL,
	DNI INT NOT NULL,
	FechaNaci DATETIME2(0) NOT NULL,
	Sexo INT NOT NULL,
	Telefono VARCHAR(32) NOT NULL,

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
-- end query
