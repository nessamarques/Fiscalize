USE [TrabFinalSI_Audit]
GO
/****** Object:  ForeignKey [FK__Audit_Car__IdTip__08EA5793]    Script Date: 08/19/2013 19:24:50 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Audit_Car__IdTip__08EA5793]') AND parent_object_id = OBJECT_ID(N'[dbo].[Audit_Cargo]'))
ALTER TABLE [dbo].[Audit_Cargo] DROP CONSTRAINT [FK__Audit_Car__IdTip__08EA5793]
GO
/****** Object:  ForeignKey [FK__Audit_Fun__IdTip__09DE7BCC]    Script Date: 08/19/2013 19:24:50 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Audit_Fun__IdTip__09DE7BCC]') AND parent_object_id = OBJECT_ID(N'[dbo].[Audit_Funcionalidade]'))
ALTER TABLE [dbo].[Audit_Funcionalidade] DROP CONSTRAINT [FK__Audit_Fun__IdTip__09DE7BCC]
GO
/****** Object:  Table [dbo].[Audit_Cargo]    Script Date: 08/19/2013 19:24:50 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Audit_Car__IdTip__08EA5793]') AND parent_object_id = OBJECT_ID(N'[dbo].[Audit_Cargo]'))
ALTER TABLE [dbo].[Audit_Cargo] DROP CONSTRAINT [FK__Audit_Car__IdTip__08EA5793]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Audit_Cargo]') AND type in (N'U'))
DROP TABLE [dbo].[Audit_Cargo]
GO
/****** Object:  Table [dbo].[Audit_Funcionalidade]    Script Date: 08/19/2013 19:24:50 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Audit_Fun__IdTip__09DE7BCC]') AND parent_object_id = OBJECT_ID(N'[dbo].[Audit_Funcionalidade]'))
ALTER TABLE [dbo].[Audit_Funcionalidade] DROP CONSTRAINT [FK__Audit_Fun__IdTip__09DE7BCC]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Audit_Funcionalidade]') AND type in (N'U'))
DROP TABLE [dbo].[Audit_Funcionalidade]
GO
/****** Object:  Table [dbo].[TipoLog]    Script Date: 08/19/2013 19:24:50 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TipoLog]') AND type in (N'U'))
DROP TABLE [dbo].[TipoLog]
GO
/****** Object:  Table [dbo].[TipoLog]    Script Date: 08/19/2013 19:24:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TipoLog]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TipoLog](
	[IdTipoLog] [int] NOT NULL,
	[DescrTipoLog] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdTipoLog] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[TipoLog] ([IdTipoLog], [DescrTipoLog]) VALUES (1, N'INSERT')
INSERT [dbo].[TipoLog] ([IdTipoLog], [DescrTipoLog]) VALUES (2, N'UPDATE')
INSERT [dbo].[TipoLog] ([IdTipoLog], [DescrTipoLog]) VALUES (3, N'DELETE')
/****** Object:  Table [dbo].[Audit_Funcionalidade]    Script Date: 08/19/2013 19:24:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Audit_Funcionalidade]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Audit_Funcionalidade](
	[IdAuditFuncionalidade] [int] IDENTITY(1,1) NOT NULL,
	[IdFuncionalidade] [int] NULL,
	[Nome] [varchar](255) NULL,
	[NomeAlterado] [varchar](255) NULL,
	[Descricao] [varchar](max) NULL,
	[DescricaoAlterada] [varchar](max) NULL,
	[IdTipoLog] [int] NOT NULL,
	[LoginResponsavel] [varchar](100) NULL,
	[DtAcao] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdAuditFuncionalidade] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Audit_Cargo]    Script Date: 08/19/2013 19:24:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Audit_Cargo]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Audit_Cargo](
	[IdAuditCargo] [int] IDENTITY(1,1) NOT NULL,
	[IdCargo] [int] NULL,
	[Nome] [varchar](255) NULL,
	[NomeAlterado] [varchar](255) NULL,
	[Descricao] [varchar](max) NULL,
	[DescricaoAlterada] [varchar](max) NULL,
	[IdTipoLog] [int] NOT NULL,
	[LoginResponsavel] [varchar](100) NULL,
	[DtAcao] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdAuditCargo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Audit_Cargo] ON
INSERT [dbo].[Audit_Cargo] ([IdAuditCargo], [IdCargo], [Nome], [NomeAlterado], [Descricao], [DescricaoAlterada], [IdTipoLog], [LoginResponsavel], [DtAcao]) VALUES (2, 16, N'TESTE', NULL, N'TESTE', NULL, 1, N'VMARQUES', CAST(0x0000A21F013D89E2 AS DateTime))
INSERT [dbo].[Audit_Cargo] ([IdAuditCargo], [IdCargo], [Nome], [NomeAlterado], [Descricao], [DescricaoAlterada], [IdTipoLog], [LoginResponsavel], [DtAcao]) VALUES (3, 16, N'TESTE', N'TESTE2', N'TESTE', N'TESTE2', 2, N'VMARQUES', CAST(0x0000A21F013DB4DA AS DateTime))
INSERT [dbo].[Audit_Cargo] ([IdAuditCargo], [IdCargo], [Nome], [NomeAlterado], [Descricao], [DescricaoAlterada], [IdTipoLog], [LoginResponsavel], [DtAcao]) VALUES (4, 16, N'TESTE2', NULL, N'TESTE2', NULL, 3, N'VMARQUES', CAST(0x0000A21F013DC45A AS DateTime))
SET IDENTITY_INSERT [dbo].[Audit_Cargo] OFF
/****** Object:  ForeignKey [FK__Audit_Car__IdTip__08EA5793]    Script Date: 08/19/2013 19:24:50 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Audit_Car__IdTip__08EA5793]') AND parent_object_id = OBJECT_ID(N'[dbo].[Audit_Cargo]'))
ALTER TABLE [dbo].[Audit_Cargo]  WITH CHECK ADD FOREIGN KEY([IdTipoLog])
REFERENCES [dbo].[TipoLog] ([IdTipoLog])
GO
/****** Object:  ForeignKey [FK__Audit_Fun__IdTip__09DE7BCC]    Script Date: 08/19/2013 19:24:50 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Audit_Fun__IdTip__09DE7BCC]') AND parent_object_id = OBJECT_ID(N'[dbo].[Audit_Funcionalidade]'))
ALTER TABLE [dbo].[Audit_Funcionalidade]  WITH CHECK ADD FOREIGN KEY([IdTipoLog])
REFERENCES [dbo].[TipoLog] ([IdTipoLog])
GO
