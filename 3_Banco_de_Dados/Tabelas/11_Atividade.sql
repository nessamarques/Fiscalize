USE [TrabFinalSI]
GO

/****** Object:  Table [dbo].[Atividade]    Script Date: 10/29/2013 18:36:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING OFF
GO

CREATE TABLE [dbo].[Atividade](
	[IdAtividade] [int] IDENTITY(1,1) NOT NULL,
	[IdPolitico] [int] NOT NULL,
	[NomeComissao] [varchar](100) NOT NULL,
	[IdSituacao] [int] NOT NULL,
	[DtInicio] [datetime] NOT NULL,
	[DtFIm] [datetime] NOT NULL,
	[Login] [varchar](100) NOT NULL,
	[DtInclusao] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdAtividade] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Atividade]  WITH CHECK ADD FOREIGN KEY([IdPolitico])
REFERENCES [dbo].[Politico] ([IdPolitico])
GO

ALTER TABLE [dbo].[Atividade]  WITH CHECK ADD FOREIGN KEY([IdSituacao])
REFERENCES [dbo].[Situacao] ([IdSituacao])
GO


