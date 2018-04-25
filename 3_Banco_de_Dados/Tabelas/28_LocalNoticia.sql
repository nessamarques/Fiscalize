USE [TrabFinalSI]
GO

/****** Object:  Table [dbo].[LocalNoticia]    Script Date: 10/29/2013 18:39:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[LocalNoticia](
	[IdLocalNoticia] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](255) NULL,
	[Descricao] [varchar](max) NULL,
	[DtInclusao] [datetime] NOT NULL,
	[Login] [varchar](100) NOT NULL,
 CONSTRAINT [PK_LocalNoticia] PRIMARY KEY CLUSTERED 
(
	[IdLocalNoticia] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


