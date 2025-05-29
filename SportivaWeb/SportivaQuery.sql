-- query 1
CREATE DATABASE SportivaDB;
-- end query

-- query 2
USE SportivaDB;
-- end query

-- query 3
CREATE TABLE Roles (
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Nombre VARCHAR(16) NOT NULL UNIQUE
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
	Nombre VARCHAR(32) NOT NULL UNIQUE
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
	Nombre VARCHAR(8) NOT NULL UNIQUE
);

INSERT INTO Sexos (Nombre)
VALUES ('Hombre'), ('Mujer');

CREATE TABLE Eventos (
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Nombre VARCHAR(16) NOT NULL UNIQUE,
	Descri VARCHAR(2048),
	FechaApeIns DATE NOT NULL,
	FechaCieIns DATE NOT NULL,
	FechaInicio DATETIME2(0) NOT NULL,
	Provincia INT NOT NULL,
	CodPostal VARCHAR(4) NOT NULL,
	Direccion VARCHAR(64) NOT NULL,
	Precio DECIMAL(16, 2) NOT NULL,
	Imagen VARCHAR(128),
	Usuario INT NOT NULL,

	CONSTRAINT FK_Eventos_Provincia FOREIGN KEY (Provincia) REFERENCES Provincias(Id),
	CONSTRAINT FK_Eventos_Usuario FOREIGN KEY (Usuario) REFERENCES Usuarios(Id)
);

INSERT INTO Eventos (Nombre, Descri, FechaApeIns, FechaCieIns, FechaInicio, Provincia, CodPostal, Direccion, Precio, Imagen, Usuario)
VALUES ('ecoTrail', '', '2025-08-05', '2025-08-07', '2025-09-07T09:30:00', 1, '1428', 'Av. Pres. Figueroa Alcorta 7597', 30000, '1.jpg', 1),
       ('Soberana', '', '2025-10-11', '2025-10-13', '2025-11-13T14:30:00', 12, '5300', 'Av. Ortiz de Ocampo 1700', 32000, '2.jpg', 3),
	   ('Brutal Race', '', '2025-08-09', '2025-08-11', '2025-09-13T09:00:00', 6, '5000', 'Bv. Perón 450', 25000, '3.jpg', 3);

CREATE TABLE Inscriptos (
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Email VARCHAR(64) NOT NULL UNIQUE,
	Nombres VARCHAR(32) NOT NULL,
	Apellidos VARCHAR(32) NOT NULL,
	FechaNaci DATETIME2(0) NOT NULL,
	Sexo INT NOT NULL,
	DNI VARCHAR(8) NOT NULL UNIQUE,
	Provincia INT NOT NULL,
	CodPostal VARCHAR(4) NOT NULL,
	Telefono VARCHAR(32) NOT NULL,

	CONSTRAINT FK_Inscriptos_Provincia FOREIGN KEY (Provincia) REFERENCES Provincias(Id),
	CONSTRAINT FK_Inscriptos_Sexo FOREIGN KEY (Sexo) REFERENCES Sexos(Id)
);

CREATE TABLE Inscripciones (
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Inscripto INT NOT NULL,
	Evento INT NOT NULL,
	Fecha DATETIME2(0) NOT NULL,

	CONSTRAINT FK_Inscripciones_Inscripto FOREIGN KEY (Inscripto) REFERENCES Inscriptos(Id),
	CONSTRAINT FK_Inscripciones_Evento FOREIGN KEY (Evento) REFERENCES Eventos(Id),
	CONSTRAINT UQ_Inscripciones_Inscripto_Evento UNIQUE(Inscripto, Evento)
);
-- end query
