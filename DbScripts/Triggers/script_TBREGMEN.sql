IF OBJECT_ID ( 'dbo.TR_TBREGCRT_TBREGOPE', 'TR' ) IS NOT NULL
    DROP TRIGGER dbo.TR_TBREGCRT_TBREGOPE;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 29/03/2022 14:14:55
 Objetivo : Trigger de Eventos da Tabela de Registro de Mensalidades
 ==================================================================================================== */
CREATE TRIGGER dbo.TR_TBREGCRT_TBREGOPE
ON dbo.TBREGMEN
 AFTER INSERT, UPDATE
AS
    BEGIN
         INSERT INTO TBREGOPE(SUBSYS,NIDREF,DATOPE,CODMOV,VLRBAS,VLRPCT,SIGOPE,VLRPCP,VLROPE,STAREC,UPDUSU)
         SELECT inserted.SUBSYS,inserted.NIDBOL,inserted.DATCAD,500,inserted.VLRCOB,100,1,1,inserted.VLRCOB,inserted.STAREC,inserted.UPDUSU FROM inserted WHERE inserted.VLRCOB>0
    END


GO

