USE [TrabFinalSI]
GO

/****** Object:  Table [dbo].[MembroComissao]    Script Date: 10/29/2013 18:39:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[MembroComissao](
	[IdMembroComissao] [int] IDENTITY(1,1) NOT NULL,
	[IdPolitico] [int] NOT NULL,
	[IdCargoComissao] [int] NOT NULL,
	[IdComissao] [int] NOT NULL,
	[DtInclusao] [datetime] NOT NULL,
	[Login] [varchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdMembroComissao] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[MembroComissao]  WITH CHECK ADD FOREIGN KEY([IdCargoComissao])
REFERENCES [dbo].[CargoComissao] ([IdCargoComissao])
GO

ALTER TABLE [dbo].[MembroComissao]  WITH CHECK ADD FOREIGN KEY([IdComissao])
REFERENCES [dbo].[Comissao] ([IdComissao])
GO

ALTER TABLE [dbo].[MembroComissao]  WITH CHECK ADD FOREIGN KEY([IdPolitico])
REFERENCES [dbo].[Politico] ([IdPolitico])
GO


