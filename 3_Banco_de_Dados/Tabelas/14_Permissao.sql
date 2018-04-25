USE [TrabFinalSI]
GO

/****** Object:  Table [dbo].[Permissao]    Script Date: 10/29/2013 18:39:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Permissao](
	[IdPermissao] [int] IDENTITY(1,1) NOT NULL,
	[IdGrupo] [int] NOT NULL,
	[IdFuncionalidade] [int] NOT NULL,
	[PermissaoIncluir] [int] NOT NULL,
	[PermissaoAlterar] [int] NOT NULL,
	[PermissaoConsultar] [int] NOT NULL,
	[PermissaoExcluir] [int] NOT NULL,
	[Login] [varchar](10) NOT NULL,
	[DtInclusao] [datetime] NOT NULL,
 CONSTRAINT [PK_GrupoFuncionalidade] PRIMARY KEY CLUSTERED 
(
	[IdPermissao] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Permissao]  WITH CHECK ADD FOREIGN KEY([IdFuncionalidade])
REFERENCES [dbo].[Funcionalidade] ([IdFuncionalidade])
GO

ALTER TABLE [dbo].[Permissao]  WITH CHECK ADD FOREIGN KEY([IdGrupo])
REFERENCES [dbo].[Grupo] ([IdGrupo])
GO


