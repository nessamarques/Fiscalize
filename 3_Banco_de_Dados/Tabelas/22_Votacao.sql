USE [TrabFinalSI]
GO

/****** Object:  Table [dbo].[Votacao]    Script Date: 11/24/2013 22:48:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Votacao](
	[IdVotacao] [int] IDENTITY(1,1) NOT NULL,
	[IdSessaoProposicao] [int] NOT NULL,
	[IdVoto] [int] NOT NULL,
	[IdMandato] [int] NOT NULL,
	[DtInclusao] [datetime] NOT NULL,
	[Login] [varchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdVotacao] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Votacao]  WITH CHECK ADD FOREIGN KEY([IdMandato])
REFERENCES [dbo].[Mandato] ([IdMandato])
GO

ALTER TABLE [dbo].[Votacao]  WITH CHECK ADD FOREIGN KEY([IdSessaoProposicao])
REFERENCES [dbo].[SessaoProposicao] ([IdSessaoProposicao])
GO

ALTER TABLE [dbo].[Votacao]  WITH CHECK ADD FOREIGN KEY([IdVoto])
REFERENCES [dbo].[Voto] ([IdVoto])
GO


