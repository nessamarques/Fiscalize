USE [TrabFinalSI]
GO

/****** Object:  Table [dbo].[Noticia]    Script Date: 11/20/2013 11:31:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Noticia](
	[IdNoticia] [int] IDENTITY(1,1) NOT NULL,
	[Titulo] [varchar](255) NOT NULL,
	[Subtitulo] [varchar](255) NULL,
	[Resumo] [varchar](max) NOT NULL,
	[Conteudo] [varchar](max) NOT NULL,
	[IdLocalNoticia] [int] NOT NULL,
	[LinkVideo] [varchar](max) NULL,
	[Ativo] [int] NOT NULL,
	[DtInclusao] [datetime] NOT NULL,
	[Login] [varchar](100) NOT NULL,
	[Fonte] [varchar](max) NULL,
	[DtNoticia] [datetime] NULL,
 CONSTRAINT [PK_Noticia] PRIMARY KEY CLUSTERED 
(
	[IdNoticia] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Noticia]  WITH CHECK ADD FOREIGN KEY([IdLocalNoticia])
REFERENCES [dbo].[LocalNoticia] ([IdLocalNoticia])
GO

ALTER TABLE [dbo].[Noticia]  WITH CHECK ADD FOREIGN KEY([IdLocalNoticia])
REFERENCES [dbo].[LocalNoticia] ([IdLocalNoticia])
GO


