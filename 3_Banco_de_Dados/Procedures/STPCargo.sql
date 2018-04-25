USE [TrabFinalSI]
GO

/****** Object:  StoredProcedure [dbo].[STPCARGO]    Script Date: 08/13/2013 18:32:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[STPCARGO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[STPCARGO]
GO

USE [TrabFinalSI]
GO

/****** Object:  StoredProcedure [dbo].[STPCARGO]    Script Date: 08/13/2013 18:32:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/************************************************************************************************************************
* Criado Por.....: paulo.barea
* Data de Criação: 27/3/2013 16:33:02
* Objetivo.......: Inserir um registro na tabela CARGO
* Parâmetros.....: @Nome = 
*                  @Descricao = 
*                  @Login = 
*                  @DtInclusao = 
*                  @RUUsuarioLogado = RU do Usuário Logado. Utilizado para armazenamento do LOG.
* Alterações.....: 
************************************************************************************************************************/
CREATE PROCEDURE [dbo].[STPCARGO]
(
@Nome VarChar(255),
@Descricao VarChar(MAX),
@Login VarChar(100),
@DtInclusao DateTime	
)
AS
INSERT INTO Cargo
(
Nome,
Descricao,
Login,
DtInclusao
)
VALUES
(
@Nome,
@Descricao,
@Login,
@DtInclusao
)

/************************************************************************************************************************/






GO

/****** Object:  NumberedStoredProcedure [dbo].[STPCARGO];2    Script Date: 08/13/2013 18:32:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






/************************************************************************************************************************
* Criado Por.....: paulo.barea
* Data de Criação: 27/3/2013 16:33:02
* Objetivo.......: Alterar um registro na tabela CARGO
* Parâmetros.....: @IdCargo = 
*                  @Nome = 
*                  @Descricao = 
*                  @Login = 
*                  @DtInclusao = 
*                  @RUUsuarioLogado = RU do Usuário Logado. Utilizado para armazenamento do LOG.
* Alterações.....: 
************************************************************************************************************************/
CREATE PROCEDURE [dbo].[STPCARGO];2
(
@IdCargo Int,
@Nome VarChar(255),
@Descricao VarChar(MAX),
@Login VarChar(100),
@DtInclusao DateTime
)
AS	
UPDATE CARGO SET
Nome = @Nome,
Descricao = @Descricao,
Login = @Login,
DtInclusao = @DtInclusao
WHERE 
IdCargo = @IdCargo

/************************************************************************************************************************/






GO

/****** Object:  NumberedStoredProcedure [dbo].[STPCARGO];3    Script Date: 08/13/2013 18:32:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






/************************************************************************************************************************
* Criado Por.....: paulo.barea
* Data de Criação: 27/3/2013 16:33:02
* Objetivo.......: Remover um registro na tabela CARGO
* Parâmetros.....: @IdCargo = 
*                  @RUUsuarioLogado = RU do Usuário Logado. Utilizado para armazenamento do LOG.
* Alterações.....: 
************************************************************************************************************************/
CREATE PROCEDURE [dbo].[STPCARGO];3
(
@IdCargo Int
)
AS
DELETE FROM CARGO
WHERE IdCargo = @IdCargo

/************************************************************************************************************************/






GO

/****** Object:  NumberedStoredProcedure [dbo].[STPCARGO];4    Script Date: 08/13/2013 18:32:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






/************************************************************************************************************************
* Criado Por.....: paulo.barea
* Data de Criação: 27/3/2013 16:33:02
* Objetivo.......: Obter todos os registros da tabela CARGO
* Parâmetros.....: 
* Alterações.....: 
************************************************************************************************************************/
CREATE PROCEDURE [dbo].[STPCARGO];4
AS
SET NOCOUNT ON

SELECT 
IdCargo,
Nome,
Descricao,
Login,
DtInclusao
FROM 
CARGO

/************************************************************************************************************************/






GO

/****** Object:  NumberedStoredProcedure [dbo].[STPCARGO];5    Script Date: 08/13/2013 18:32:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






/************************************************************************************************************************
* Criado Por.....: paulo.barea
* Data de Criação: 27/3/2013 16:33:02
* Objetivo.......: Obter um registro da tabela CARGO
* Parâmetros.....: @IdCargo = .
* Alterações.....: 
************************************************************************************************************************/
CREATE PROCEDURE [dbo].[STPCARGO];5
(
@IdCargo Int
)
AS
SET NOCOUNT ON

SELECT 
IdCargo,
Nome,
Descricao,
Login,
DtInclusao
FROM 
CARGO
WHERE 
IdCargo = @IdCargo

/************************************************************************************************************************/






GO

/****** Object:  NumberedStoredProcedure [dbo].[STPCARGO];6    Script Date: 08/13/2013 18:32:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






/************************************************************************************************************************
* Criado Por.....: paulo.barea
* Data de Criação: 27/3/2013 16:33:02
* Objetivo.......: Obter um registro da tabela CARGO
* Parâmetros.....: pString = String para consulta
* Alterações.....: 
************************************************************************************************************************/
CREATE PROCEDURE [dbo].[STPCARGO];6
(
@pString VarChar(50)
)
AS
SET NOCOUNT ON

SELECT 
IdCargo,
Nome,
Descricao,
Login,
DtInclusao
FROM 
CARGO
WHERE 
Nome LIKE @pString + '%'

/************************************************************************************************************************/



GO


