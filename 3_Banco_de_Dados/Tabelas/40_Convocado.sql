USE [TrabFinalSI]
GO

/****** Object:  Table [dbo].[Convocado]    Script Date: 11/24/2013 21:21:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Convocado](
	[IdConvocado] [int] IDENTITY(1,1) NOT NULL,
	[IdSessao] [int] NOT NULL,
	[IdCargo] [int] NOT NULL,
	[DtInclusao] [datetime] NOT NULL,
	[Login] [varchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdConvocado] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Convocado]  WITH CHECK ADD FOREIGN KEY([IdCargo])
REFERENCES [dbo].[Cargo] ([IdCargo])
GO

ALTER TABLE [dbo].[Convocado]  WITH CHECK ADD FOREIGN KEY([IdSessao])
REFERENCES [dbo].[Sessao] ([IdSessao])
GO


