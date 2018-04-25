USE [TrabFinalSI]
GO

/****** Object:  Table [dbo].[Sessao]    Script Date: 11/22/2013 21:01:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Sessao](
	[IdSessao] [int] IDENTITY(1,1) NOT NULL,
	[DtInicial] [datetime] NOT NULL,
	[DtFinal] [datetime] NOT NULL,
	[Nome] [varchar](255) NOT NULL,
	[Descricao] [varchar](255) NOT NULL,
	[IdSituacao] [int] NOT NULL,
	[IdOrador] [int] NOT NULL,
	[Pauta] [varbinary](max) NULL,
	[DtInclusao] [datetime] NOT NULL,
	[Login] [varchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdSessao] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Sessao]  WITH CHECK ADD FOREIGN KEY([IdOrador])
REFERENCES [dbo].[Politico] ([IdPolitico])
GO

ALTER TABLE [dbo].[Sessao]  WITH CHECK ADD FOREIGN KEY([IdSituacao])
REFERENCES [dbo].[SituacaoSessao] ([IdSituacaoSessao])
GO


