USE [TrabFinalSI]
GO

/****** Object:  Table [dbo].[Mandato]    Script Date: 10/29/2013 18:39:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Mandato](
	[IdMandato] [int] IDENTITY(1,1) NOT NULL,
	[IdPeriodoMandato] [int] NOT NULL,
	[IdPolitico] [int] NOT NULL,
	[IdCargo] [int] NOT NULL,
	[IdPartido] [int] NOT NULL,
	[Login] [varchar](100) NOT NULL,
	[DtInclusao] [datetime] NOT NULL,
	[IsMandatoAtual] [bit] NULL,
 CONSTRAINT [PK_Mandato] PRIMARY KEY CLUSTERED 
(
	[IdMandato] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Mandato]  WITH CHECK ADD FOREIGN KEY([IdCargo])
REFERENCES [dbo].[Cargo] ([IdCargo])
GO

ALTER TABLE [dbo].[Mandato]  WITH CHECK ADD FOREIGN KEY([IdPartido])
REFERENCES [dbo].[Partido] ([IdPartido])
GO

ALTER TABLE [dbo].[Mandato]  WITH CHECK ADD FOREIGN KEY([IdPeriodoMandato])
REFERENCES [dbo].[PeriodoMandato] ([IdPeriodoMandato])
GO

ALTER TABLE [dbo].[Mandato]  WITH CHECK ADD FOREIGN KEY([IdPolitico])
REFERENCES [dbo].[Politico] ([IdPolitico])
GO


