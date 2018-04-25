USE [TrabFinalSI]
GO

/****** Object:  Table [dbo].[Presenca]    Script Date: 11/22/2013 21:04:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Presenca](
	[IdPresenca] [int] IDENTITY(1,1) NOT NULL,
	[IdSessao] [int] NOT NULL,
	[IdPolitico] [int] NOT NULL,
	[DtInclusao] [datetime] NOT NULL,
	[Login] [varchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdPresenca] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Presenca]  WITH CHECK ADD FOREIGN KEY([IdPolitico])
REFERENCES [dbo].[Politico] ([IdPolitico])
GO

ALTER TABLE [dbo].[Presenca]  WITH CHECK ADD FOREIGN KEY([IdSessao])
REFERENCES [dbo].[Sessao] ([IdSessao])
GO


