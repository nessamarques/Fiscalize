USE [TrabFinalSI]
GO

/****** Object:  Table [dbo].[ImagemNoticia]    Script Date: 10/29/2013 18:39:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ImagemNoticia](
	[IdImagemNoticia] [int] IDENTITY(1,1) NOT NULL,
	[IdNoticia] [int] NOT NULL,
	[Imagem] [varbinary](max) NULL,
	[IdLocalNoticia] [int] NOT NULL,
	[IsPortal] [int] NOT NULL,
	[DtInclusao] [datetime] NOT NULL,
	[Login] [varchar](100) NOT NULL,
 CONSTRAINT [PK_ImagemNoticia] PRIMARY KEY CLUSTERED 
(
	[IdImagemNoticia] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[ImagemNoticia]  WITH CHECK ADD FOREIGN KEY([IdLocalNoticia])
REFERENCES [dbo].[LocalNoticia] ([IdLocalNoticia])
GO

ALTER TABLE [dbo].[ImagemNoticia]  WITH CHECK ADD FOREIGN KEY([IdNoticia])
REFERENCES [dbo].[Noticia] ([IdNoticia])
GO


