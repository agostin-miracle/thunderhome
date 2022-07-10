IF OBJECT_ID ( 'dbo.TR_TBRBBBOL', 'TR' ) IS NOT NULL
    DROP TRIGGER dbo.TR_TBRBBBOL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 10/04/2022 11:51:58
 Objetivo : Trigger de Eventos da Tabela de Registro de Recebimento de Boletos
 ==================================================================================================== */
CREATE TRIGGER dbo.TR_TBRBBBOL
ON dbo.TBRBBBOL
 AFTER INSERT, UPDATE
AS
    DECLARE @NIDRBB INT =0
    SELECT @NIDRBB=NIDRBB FROM inserted
    IF(inserted.STAREC=0)
       UPDATE TBBXABOL SET STAREC = 0,
              DATUPD=GETDATE(), UPDUSU=INSERTED.UPDUSU WHERE NIDRBB = @NIDRBB AND STAREC<>9


GO

