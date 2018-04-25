USE [TrabFinalSI]
GO

/****** Object:  Table [dbo].[SessaoProposicao]    Script Date: 11/24/2013 21:15:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[SessaoProposicao](
	[IdSessaoProposicao] [int] IDENTITY(1,1) NOT NULL,
	[IdSessao] [int] NOT NULL,
	[IdProposicao] [int] NOT NULL,
	[DtInclusao] [datetime] NOT NULL,
	[Login] [varchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdSessaoProposicao] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[SessaoProposicao]  WITH CHECK ADD FOREIGN KEY([IdProposicao])
REFERENCES [dbo].[Proposicao] ([IdProposicao])
GO

ALTER TABLE [dbo].[SessaoProposicao]  WITH CHECK ADD FOREIGN KEY([IdSessao])
REFERENCES [dbo].[Sessao] ([IdSessao])
GO


