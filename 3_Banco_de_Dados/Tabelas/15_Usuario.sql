USE [TrabFinalSI]
GO

/****** Object:  Table [dbo].[Usuario]    Script Date: 10/29/2013 18:47:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Usuario](
	[IdUsuario] [int] IDENTITY(1,1) NOT NULL,
	[IdGrupo] [int] NOT NULL,
	[Nome] [varchar](255) NOT NULL,
	[CPF] [varchar](11) NOT NULL,
	[IdCidade] [int] NOT NULL,
	[IdEstado] [int] NOT NULL,
	[Telefone] [varchar](10) NULL,
	[Celular] [varchar](10) NULL,
	[Email] [varchar](100) NOT NULL,
	[UsuarioLogin] [varchar](10) NOT NULL,
	[Senha] [varchar](10) NOT NULL,
	[Login] [varchar](10) NOT NULL,
	[DtInclusao] [datetime] NOT NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD FOREIGN KEY([IdCidade])
REFERENCES [dbo].[Cidade] ([IdCidade])
GO

ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD FOREIGN KEY([IdEstado])
REFERENCES [dbo].[Estado] ([IdEstado])
GO

ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD FOREIGN KEY([IdGrupo])
REFERENCES [dbo].[Grupo] ([IdGrupo])
GO


