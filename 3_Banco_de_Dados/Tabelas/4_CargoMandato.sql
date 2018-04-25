USE [TrabFinalSI]
GO

/****** Object:  Table [dbo].[CargoMandato]    Script Date: 10/29/2013 18:38:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CargoMandato](
	[IdCargoMandato] [int] IDENTITY(1,1) NOT NULL,
	[IdPeriodoMandato] [int] NOT NULL,
	[IdCargo] [int] NOT NULL,
	[Login] [varchar](100) NOT NULL,
	[DtInclusao] [datetime] NOT NULL,
 CONSTRAINT [PK_CargoMandato] PRIMARY KEY CLUSTERED 
(
	[IdCargoMandato] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CargoMandato]  WITH CHECK ADD FOREIGN KEY([IdPeriodoMandato])
REFERENCES [dbo].[PeriodoMandato] ([IdPeriodoMandato])
GO


