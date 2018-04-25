USE [TrabFinalSI]
GO

/****** Object:  Table [dbo].[Proposicao]    Script Date: 11/10/2013 13:29:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Proposicao](
	[IdProposicao] [int] IDENTITY(1,1) NOT NULL,
	[IdTipo] [int] NOT NULL,
	[IdMandato] [int] NOT NULL,
	[Numero] [varchar](10) NOT NULL,
	[Ano] [varchar](4) NOT NULL,
	[Situacao] [varchar](255) NOT NULL,
	[DtApresentacao] [datetime] NOT NULL,
	[Ementa] [varchar](255) NOT NULL,
	[DtInclusao] [datetime] NOT NULL,
	[Login] [varchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdProposicao] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Proposicao]  WITH CHECK ADD FOREIGN KEY([IdMandato])
REFERENCES [dbo].[Mandato] ([IdMandato])
GO

ALTER TABLE [dbo].[Proposicao]  WITH CHECK ADD FOREIGN KEY([IdTipo])
REFERENCES [dbo].[TipoProposicao] ([IdTipoProposicao])
GO


