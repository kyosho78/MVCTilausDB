CREATE TABLE [dbo].[Tuotteet] (
    [TuoteID] INT            IDENTITY (1000, 1) NOT NULL,
    [Nimi]    NVARCHAR (50)  NULL,
    [Ahinta]  NUMERIC (7, 2) NULL,
    [Kuva]    NVARCHAR(200)          NULL,
    CONSTRAINT [PK_Tuotteet] PRIMARY KEY CLUSTERED ([TuoteID] ASC)
);

