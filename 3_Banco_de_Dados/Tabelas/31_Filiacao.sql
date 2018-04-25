USE [TrabFinalSI]
GO

/****** Object:  Table [dbo].[Filiacao]    Script Date: 10/29/2013 18:38:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING OFF
GO

CREATE TABLE [dbo].[Filiacao](
	[IdFiliacao] [int] IDENTITY(1,1) NOT NULL,
	[IdPolitico] [int] NOT NULL,
	[NomeMae] [varchar](255) NULL,
	[NomePai] [varchar](255) NULL,
	[Login] [varchar](100) NOT NULL,
	[DtInclusao] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdFiliacao] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Filiacao]  WITH CHECK ADD FOREIGN KEY([IdPolitico])
REFERENCES [dbo].[Politico] ([IdPolitico])
GO

ALTER TABLE [dbo].[Filiacao]  WITH CHECK ADD FOREIGN KEY([IdPolitico])
REFERENCES [dbo].[Politico] ([IdPolitico])
GO

ALTER TABLE [dbo].[Filiacao]  WITH CHECK ADD FOREIGN KEY([IdPolitico])
REFERENCES [dbo].[Politico] ([IdPolitico])
GO


