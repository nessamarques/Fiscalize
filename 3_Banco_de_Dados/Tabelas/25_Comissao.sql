USE [TrabFinalSI]
GO

/****** Object:  Table [dbo].[Comissao]    Script Date: 11/04/2013 01:05:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Comissao](
	[IdComissao] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](100) NOT NULL,
	[Sigla] [varchar](10) NOT NULL,
	[Local] [varchar](100) NOT NULL,
	[Telefone] [varchar](11) NOT NULL,
	[Fax] [varchar](11) NOT NULL,
	[Secretario] [varchar](100) NOT NULL,
	[DtInclusao] [datetime] NOT NULL,
	[Login] [varchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdComissao] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


