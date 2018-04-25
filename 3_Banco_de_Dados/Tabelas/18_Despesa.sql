USE [TrabFinalSI]
GO

/****** Object:  Table [dbo].[Despesa]    Script Date: 11/10/2013 14:30:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Despesa](
	[IdDespesa] [int] IDENTITY(1,1) NOT NULL,
	[IdCategoria] [int] NOT NULL,
	[IdMandato] [int] NOT NULL,
	[Descricao] [varchar](255) NULL,
	[Valor] [decimal](10, 2) NULL,
	[CPF_CNPJ_Forn] [varchar](255) NULL,
	[NomeFornecedor] [varchar](255) NULL,
	[NF] [varchar](255) NULL,
	[MesDespesa] [int] NULL,
	[AnoDespesa] [int] NULL,
	[DtInclusao] [datetime] NOT NULL,
	[Login] [varchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdDespesa] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Despesa]  WITH CHECK ADD FOREIGN KEY([IdCategoria])
REFERENCES [dbo].[Categoria] ([IdCategoria])
GO

ALTER TABLE [dbo].[Despesa]  WITH CHECK ADD FOREIGN KEY([IdMandato])
REFERENCES [dbo].[Mandato] ([IdMandato])
GO


