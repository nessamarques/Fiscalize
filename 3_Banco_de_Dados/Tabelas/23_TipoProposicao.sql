USE [TrabFinalSI]
GO

/****** Object:  Table [dbo].[TipoProposicao]    Script Date: 10/29/2013 18:47:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TipoProposicao](
	[IdTipoProposicao] [int] IDENTITY(1,1) NOT NULL,
	[Sigla] [varchar](10) NOT NULL,
	[Nome] [varchar](100) NOT NULL,
	[DtInclusao] [datetime] NOT NULL,
	[Login] [varchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdTipoProposicao] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


