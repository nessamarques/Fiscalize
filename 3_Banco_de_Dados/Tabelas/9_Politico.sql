USE [TrabFinalSI]
GO

/****** Object:  Table [dbo].[Politico]    Script Date: 10/29/2013 18:46:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING OFF
GO

CREATE TABLE [dbo].[Politico](
	[IdPolitico] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](100) NOT NULL,
	[IdCidade] [int] NOT NULL,
	[IdEstado] [int] NOT NULL,
	[Sexo] [varchar](1) NOT NULL,
	[Foto] [varbinary](max) NULL,
	[DtNascimento] [datetime] NOT NULL,
	[IdCidadeNaturalidade] [int] NOT NULL,
	[IdEstadoNaturalidade] [int] NOT NULL,
	[IdPaisNaturalidade] [int] NOT NULL,
	[Gabinete] [int] NULL,
	[Anexo] [varchar](255) NULL,
	[Telefone] [varchar](10) NULL,
	[Fax] [varchar](10) NULL,
	[Email] [varchar](255) NOT NULL,
	[Login] [varchar](100) NOT NULL,
	[DtInclusao] [datetime] NOT NULL,
	[IdEscolaridade] [int] NOT NULL,
	[Endereco] [varchar](max) NULL,
	[CEP] [varchar](9) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdPolitico] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Politico]  WITH CHECK ADD FOREIGN KEY([IdCidadeNaturalidade])
REFERENCES [dbo].[Cidade] ([IdCidade])
GO

ALTER TABLE [dbo].[Politico]  WITH CHECK ADD FOREIGN KEY([IdCidade])
REFERENCES [dbo].[Cidade] ([IdCidade])
GO

ALTER TABLE [dbo].[Politico]  WITH CHECK ADD FOREIGN KEY([IdEscolaridade])
REFERENCES [dbo].[Escolaridade] ([IdEscolaridade])
GO

ALTER TABLE [dbo].[Politico]  WITH CHECK ADD FOREIGN KEY([IdEstadoNaturalidade])
REFERENCES [dbo].[Estado] ([IdEstado])
GO

ALTER TABLE [dbo].[Politico]  WITH CHECK ADD FOREIGN KEY([IdEstado])
REFERENCES [dbo].[Estado] ([IdEstado])
GO

ALTER TABLE [dbo].[Politico]  WITH CHECK ADD FOREIGN KEY([IdPaisNaturalidade])
REFERENCES [dbo].[Pais] ([IdPais])
GO


