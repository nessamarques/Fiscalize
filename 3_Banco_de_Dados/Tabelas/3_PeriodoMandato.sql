USE [TrabFinalSI]
GO

/****** Object:  Table [dbo].[PeriodoMandato]    Script Date: 10/29/2013 18:39:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[PeriodoMandato](
	[IdPeriodoMandato] [int] IDENTITY(1,1) NOT NULL,
	[Situacao] [int] NOT NULL,
	[DtInicio] [datetime] NOT NULL,
	[DtFim] [datetime] NOT NULL,
	[Login] [varchar](100) NOT NULL,
	[DtInclusao] [datetime] NOT NULL,
 CONSTRAINT [PK_PeriodoMandato] PRIMARY KEY CLUSTERED 
(
	[IdPeriodoMandato] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


