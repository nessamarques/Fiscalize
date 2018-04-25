USE [TrabFinalSI]
GO

/****** Object:  Trigger [dbo].[trg_AuditCargo]    Script Date: 08/19/2013 19:04:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TRIGGER [dbo].[trg_AuditCargo] ON [dbo].[Cargo]
AFTER INSERT, UPDATE, DELETE
AS

	DECLARE @AcaoDesejada as char(1);
    SET @AcaoDesejada = (CASE WHEN EXISTS(SELECT * FROM INSERTED)
                         AND EXISTS(SELECT * FROM DELETED)
                        THEN 'U'  -- Update.
                        WHEN EXISTS(SELECT * FROM INSERTED)
                        THEN 'I'  -- Insert.
                        WHEN EXISTS(SELECT * FROM DELETED)
                        THEN 'D'  -- Delete.
                        ELSE NULL -- A operação não foi concluída.   
    END)
    
    IF(@AcaoDesejada IS NOT NULL)
    BEGIN
    
		IF(@AcaoDesejada = 'I')
		BEGIN			
			INSERT INTO TrabFinalSI_Audit..Audit_Cargo
			SELECT I.IdCargo, I.Nome, null, I.Descricao, null, 1, I.Login, GETDATE() FROM INSERTED I
	    END
		
		IF(@AcaoDesejada = 'U')
		BEGIN	    
			INSERT INTO TrabFinalSI_Audit..Audit_Cargo
			SELECT D.IdCargo, D.Nome, I.Nome, D.Descricao, I.Descricao, 2, D.Login, GETDATE() 
			FROM INSERTED I
			INNER JOIN DELETED D ON D.IdCargo = I.IdCargo
		END
    
		IF(@AcaoDesejada = 'D')
		BEGIN			
			INSERT INTO TrabFinalSI_Audit..Audit_Cargo
			SELECT D.IdCargo, D.Nome, null, D.Descricao, null, 3, D.Login, GETDATE() FROM DELETED D
		END
    
    END
    
    

GO

