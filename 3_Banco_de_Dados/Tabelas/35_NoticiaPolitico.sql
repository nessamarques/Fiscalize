USE [TrabFinalSI]
GO

/****** Object:  Table [dbo].[NoticiaPolitico]    Script Date: 10/29/2013 18:39:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[NoticiaPolitico](
	[IdNoticiaPolitico] [int] IDENTITY(1,1) NOT NULL,
	[IdNoticia] [int] NOT NULL,
	[IdPolitico] [int] NOT NULL,
	[DtInclusao] [datetime] NOT NULL,
	[Login] [varchar](100) NOT NULL,
 CONSTRAINT [PK_NoticiaPolitico] PRIMARY KEY CLUSTERED 
(
	[IdNoticiaPolitico] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[NoticiaPolitico]  WITH CHECK ADD FOREIGN KEY([IdNoticia])
REFERENCES [dbo].[Noticia] ([IdNoticia])
GO

ALTER TABLE [dbo].[NoticiaPolitico]  WITH CHECK ADD FOREIGN KEY([IdPolitico])
REFERENCES [dbo].[Politico] ([IdPolitico])
GO


