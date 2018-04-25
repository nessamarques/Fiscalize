USE [TrabFinalSI]
GO

/****** Object:  Table [dbo].[Acompanhamento]    Script Date: 11/03/2013 14:54:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING OFF
GO

CREATE TABLE [dbo].[Acompanhamento](
	[IdAcompanhamento] [int] IDENTITY(1,1) NOT NULL,
	[IdUsuario] [int] NOT NULL,
	[IdPolitico] [int] NOT NULL,
	[IsNoticia] [int] NOT NULL,
	[IsDespesa] [int] NOT NULL,
	[IsProposicao] [int] NOT NULL,
	[Login] [varchar](100) NOT NULL,
	[DtInclusao] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdAcompanhamento] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Acompanhamento]  WITH CHECK ADD FOREIGN KEY([IdPolitico])
REFERENCES [dbo].[Politico] ([IdPolitico])
GO

ALTER TABLE [dbo].[Acompanhamento]  WITH CHECK ADD FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuario] ([IdUsuario])
GO


