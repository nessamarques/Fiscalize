USE [TrabFinalSI]
GO

/****** Object:  Table [dbo].[DetalhesPartido]    Script Date: 10/29/2013 18:38:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[DetalhesPartido](
	[IdDetalhesPartido] [int] IDENTITY(1,1) NOT NULL,
	[IdPartido] [int] NOT NULL,
	[Endereco] [varchar](255) NULL,
	[IdCidade] [int] NULL,
	[IdEstado] [int] NULL,
	[CEP] [varchar](9) NULL,
	[Telefone] [varchar](15) NULL,
	[Fax] [varchar](15) NULL,
	[Site] [varchar](255) NULL,
	[Email] [varchar](255) NULL,
	[Login] [varchar](100) NOT NULL,
	[DtInclusao] [datetime] NOT NULL,
 CONSTRAINT [PK_DetalhesPartido] PRIMARY KEY CLUSTERED 
(
	[IdDetalhesPartido] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[DetalhesPartido]  WITH CHECK ADD FOREIGN KEY([IdPartido])
REFERENCES [dbo].[Partido] ([IdPartido])
GO


