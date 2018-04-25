USE [TrabFinalSI]
GO

/****** Object:  Table [dbo].[TipoRedeSocial]    Script Date: 10/29/2013 18:47:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING OFF
GO

CREATE TABLE [dbo].[TipoRedeSocial](
	[IdTipoRedeSocial] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](255) NULL,
	[Endereco] [varchar](255) NULL,
	[Login] [varchar](100) NOT NULL,
	[DtInclusao] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdTipoRedeSocial] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


