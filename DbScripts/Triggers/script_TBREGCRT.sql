IF OBJECT_ID ( 'dbo.TR_TBREGCRT_CADCTA', 'TR' ) IS NOT NULL
    DROP TRIGGER dbo.TR_TBREGCRT_CADCTA;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 07/03/2022 18:52:55
 Objetivo : Trigger de Eventos da Tabela de Registro de Cartoes
 ==================================================================================================== */
CREATE TRIGGER dbo.TR_TBREGCRT_CADCTA
ON dbo.TBREGCRT
 AFTER INSERT, UPDATE
AS
    DECLARE @CODCRT INT =0, @NIDCEV INT =0, @ASSUSU INT =0, @NIDCTA INT =0, @USUPRO INT =0, @STACRT SMALLINT =0;
    DECLARE @DATVAL DATETIME = GETDATE(), @CODCMF VARCHAR(15), @NOMUSU VARCHAR(70);
    DECLARE @DATFIN DATETIME = DATEADD(YEAR, 2, GETDATE());
    SELECT @ASSUSU = I.ASSUSU, @STACRT=I.STACRT,@USUPRO = I.USUPRO, @CODCRT = I.CODCRT, @NIDCEV=NIDCEV FROM INSERTED I
    IF(@ASSUSU>0 AND @STACRT IN (109,103) AND @USUPRO>0 AND @NIDCEV=0)
      BEGIN
          IF(EXISTS(SELECT 1 FROM TBUSUPRO (NOLOCK) WHERE USUPRO = @USUPRO AND APLCES=1))
             BEGIN
                IF(NOT EXISTS(SELECT 1 FROM TBCADCTA (NOLOCK) WHERE CODUSU=@ASSUSU AND ORGCTA=3))
                    BEGIN
                       SELECT @CODCMF = CODCMF, @NOMUSU = NOMUSU FROM TBCADUSU (NOLOCK) WHERE CODUSU =@ASSUSU
                       EXEC PRCADCTAINS '0001', '000', '', '', 3, 1, 1, @ASSUSU, 1, @ASSUSU, 250, @DATVAL, @DATFIN, 1000, 1, 1, @CODCMF, @NOMUSU, @NIDCTA OUTPUT
                       IF(@NIDCTA>0)
                          UPDATE TBREGCRT SET NIDCEV=@NIDCTA WHERE CODCRT=@CODCRT
                    END
             END
      END


GO

IF OBJECT_ID ( 'dbo.TR_TBREGCRT_UPDATE', 'TR' ) IS NOT NULL
    DROP TRIGGER dbo.TR_TBREGCRT_UPDATE;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 07/03/2022 18:52:55
 Objetivo : Trigger de Eventos de ajuste de status da Tabela de Registro de Cartoes
 ==================================================================================================== */
CREATE TRIGGER dbo.TR_TBREGCRT_UPDATE
ON dbo.TBREGCRT
 AFTER INSERT, UPDATE
AS
    BEGIN
        DECLARE @UPDUSU INT
    		DECLARE @CODCRT INT
    		DECLARE @STACRT SMALLINT
    		SELECT @UPDUSU = UPDUSU, @CODCRT = CODCRT, @STACRT=STACRT
    		  FROM inserted
    
    		if(@STACRT IN (120,123,129))
    			BEGIN
    				UPDATE TBCTRMEN
    				   SET STAREC = 13,
    	                   DATUPD=GETDATE(),
    	                   UPDUSU=@UPDUSU
    				 WHERE STAREC=1
    				   AND STAMEN in (260,261)
    				   AND CODCRT =@CODCRT
    				   AND LOTFIN = 0
    			END
    	END


GO

