-- Crear la base de datos
CREATE DATABASE ProyectoFinal;
GO

-- Usar la base de datos
USE ProyectoFinal;
GO

-- Crear la tabla Conferencia
CREATE TABLE [dbo].[Conferencia] (
    [Id]                     INT            IDENTITY (1, 1) NOT NULL,
    [Titulo]                 NVARCHAR (MAX) NOT NULL,
    [Horario]                NVARCHAR (MAX) NOT NULL,
    [NombreConferencista]    NVARCHAR (MAX) NOT NULL,
    [ConfirmacionAsistencia] BIT            NOT NULL,
    CONSTRAINT [PK_Conferencia] PRIMARY KEY CLUSTERED ([Id] ASC)
);

-- Crear la tabla Participante
CREATE TABLE [dbo].[Participante] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [Nombre]    NVARCHAR (MAX) NOT NULL,
    [Apellidos] NVARCHAR (MAX) NOT NULL,
    [UTwitter]  NVARCHAR (MAX) NOT NULL,
    [Ocupacion] NVARCHAR (MAX) NOT NULL,
    [AvatarId]  INT            NOT NULL,
    [TYC]       BIT            NOT NULL,
    CONSTRAINT [PK_Participante] PRIMARY KEY CLUSTERED ([Id] ASC)
);

-- Crear la tabla RegistrosConferencias
CREATE TABLE [dbo].[RegistrosConferencias] (
    [Id]                     INT IDENTITY (1, 1) NOT NULL,
    [ConferenciaId]          INT NOT NULL,
    [ParticipanteId]         INT NOT NULL,
    [ConfirmacionAsistencia] BIT NOT NULL,
    CONSTRAINT [PK_RegistrosConferencias] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RegistrosConferencias_Conferencia] FOREIGN KEY ([ConferenciaId]) REFERENCES [dbo].[Conferencia] ([Id]),
    CONSTRAINT [FK_RegistrosConferencias_Participante] FOREIGN KEY ([ParticipanteId]) REFERENCES [dbo].[Participante] ([Id]),
    CONSTRAINT [CHK_ConfirmacionAsistencia] CHECK ([ConfirmacionAsistencia] IN (0, 1)) -- Restricción de valores permitidos
);

-- Crear índices no agrupados
CREATE NONCLUSTERED INDEX [IX_RegistrosConferencias_ConferenciaId]
    ON [dbo].[RegistrosConferencias]([ConferenciaId] ASC);

CREATE NONCLUSTERED INDEX [IX_RegistrosConferencias_ParticipanteId]
    ON [dbo].[RegistrosConferencias]([ParticipanteId] ASC);
