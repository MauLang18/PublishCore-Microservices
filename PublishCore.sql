CREATE DATABASE DB_PUBLISHCORE;
GO
USE DB_PUBLISHCORE;
GO
CREATE SCHEMA  PUBLISHCORE;
GO
CREATE TABLE PUBLISHCORE.TB_EMPRESA(
	Id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	Empresa VARCHAR(255) NOT NULL,
	UsuarioCreacionAuditoria INT NOT NULL,
	FechaCreacionAuditoria DATETIME2(7) NOT NULL,
	UsuarioActualizacionAuditoria INT NULL,
	FechaActualizacionAuditoria DATETIME2(7) NULL,
	UsuarioEliminacionAuditoria INT NULL,
	FechaEliminacionAuditoria DATETIME2(7) NULL,
	Estado INT NOT NULL
);
GO
CREATE TABLE PUBLISHCORE.TB_USUARIO(
	Id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	Usuario VARCHAR(100) NOT NULL,
	Pass VARCHAR(MAX) NOT NULL,
	UsuarioCreacionAuditoria INT NOT NULL,
	FechaCreacionAuditoria DATETIME2(7) NOT NULL,
	UsuarioActualizacionAuditoria INT NULL,
	FechaActualizacionAuditoria DATETIME2(7) NULL,
	UsuarioEliminacionAuditoria INT NULL,
	FechaEliminacionAuditoria DATETIME2(7) NULL,
	Estado INT NOT NULL
);
GO
CREATE TABLE PUBLISHCORE.TB_BANNER_PRINCIPAL(
	Id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	Nombre VARCHAR(100) NOT NULL,
	Imagen VARCHAR(MAX) NOT NULL,
	EmpresaId INT NOT NULL,
	UsuarioCreacionAuditoria INT NOT NULL,
	FechaCreacionAuditoria DATETIME2(7) NOT NULL,
	UsuarioActualizacionAuditoria INT NULL,
	FechaActualizacionAuditoria DATETIME2(7) NULL,
	UsuarioEliminacionAuditoria INT NULL,
	FechaEliminacionAuditoria DATETIME2(7) NULL,
	Programacion INT NULL,
	FechaProgramacion DATETIME2(7) NULL,
	Estado INT NOT NULL,
	CONSTRAINT FK_EMPRESA_BANNER_PRINCIPAL FOREIGN KEY (EmpresaId) REFERENCES PUBLISHCORE.TB_EMPRESA(Id)
);
GO
CREATE TABLE PUBLISHCORE.TB_BOLETIN(
	Id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	Nombre VARCHAR(100) NOT NULL,
	Imagen VARCHAR(MAX) NOT NULL,
	EmpresaId INT NOT NULL,
	UsuarioCreacionAuditoria INT NOT NULL,
	FechaCreacionAuditoria DATETIME2(7) NOT NULL,
	UsuarioActualizacionAuditoria INT NULL,
	FechaActualizacionAuditoria DATETIME2(7) NULL,
	UsuarioEliminacionAuditoria INT NULL,
	FechaEliminacionAuditoria DATETIME2(7) NULL,
	Programacion INT NULL,
	FechaProgramacion DATETIME2(7) NULL,
	Estado INT NOT NULL,
	CONSTRAINT FK_EMPRESA_BOLETIN FOREIGN KEY (EmpresaId) REFERENCES PUBLISHCORE.TB_EMPRESA(Id)
);
GO
CREATE TABLE PUBLISHCORE.TB_SERVICIO_BENEFICIO(
	Id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	Titulo VARCHAR(100) NOT NULL,
	Descripcion VARCHAR(MAX) NOT NULL,
	Imagen VARCHAR(MAX) NOT NULL,
	EmpresaId INT NOT NULL,
	UsuarioCreacionAuditoria INT NOT NULL,
	FechaCreacionAuditoria DATETIME2(7) NOT NULL,
	UsuarioActualizacionAuditoria INT NULL,
	FechaActualizacionAuditoria DATETIME2(7) NULL,
	UsuarioEliminacionAuditoria INT NULL,
	FechaEliminacionAuditoria DATETIME2(7) NULL,
	Programacion INT NULL,
	FechaProgramacion DATETIME2(7) NULL,
	Estado INT NOT NULL,
	CONSTRAINT FK_EMPRESA_SERVICIO_BENEFICIO FOREIGN KEY (EmpresaId) REFERENCES PUBLISHCORE.TB_EMPRESA(Id)
);
GO
CREATE TABLE PUBLISHCORE.TB_PREGUNTA_FRECUENTE(
	Id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	Titulo VARCHAR(100) NOT NULL,
	Descripcion VARCHAR(MAX) NOT NULL,
	UsuarioCreacionAuditoria INT NOT NULL,
	FechaCreacionAuditoria DATETIME2(7) NOT NULL,
	UsuarioActualizacionAuditoria INT NULL,
	FechaActualizacionAuditoria DATETIME2(7) NULL,
	UsuarioEliminacionAuditoria INT NULL,
	FechaEliminacionAuditoria DATETIME2(7) NULL,
	Programacion INT NULL,
	FechaProgramacion DATETIME2(7) NULL,
	Estado INT NOT NULL
);
GO
CREATE TABLE PUBLISHCORE.TB_POLITICA(
	Id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	Titulo VARCHAR(100) NOT NULL,
	Descripcion VARCHAR(MAX) NOT NULL,
	UsuarioCreacionAuditoria INT NOT NULL,
	FechaCreacionAuditoria DATETIME2(7) NOT NULL,
	UsuarioActualizacionAuditoria INT NULL,
	FechaActualizacionAuditoria DATETIME2(7) NULL,
	UsuarioEliminacionAuditoria INT NULL,
	FechaEliminacionAuditoria DATETIME2(7) NULL,
	Programacion INT NULL,
	FechaProgramacion DATETIME2(7) NULL,
	Estado INT NOT NULL
);
GO
CREATE TABLE PUBLISHCORE.TB_PARAMETRO(
	Id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	Parametro VARCHAR(100) NOT NULL,
	Descripcion VARCHAR(MAX) NOT NULL,
	Valor VARCHAR(MAX) NOT NULL,
	EmpresaId INT NOT NULL,
	UsuarioCreacionAuditoria INT NOT NULL,
	FechaCreacionAuditoria DATETIME2(7) NOT NULL,
	UsuarioActualizacionAuditoria INT NULL,
	FechaActualizacionAuditoria DATETIME2(7) NULL,
	UsuarioEliminacionAuditoria INT NULL,
	FechaEliminacionAuditoria DATETIME2(7) NULL,
	Estado INT NOT NULL,
	CONSTRAINT FK_EMPRESA_PARAMETRO FOREIGN KEY (EmpresaId) REFERENCES PUBLISHCORE.TB_EMPRESA(Id)
);