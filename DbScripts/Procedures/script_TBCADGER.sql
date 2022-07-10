IF OBJECT_ID ( 'dbo.PRCADGERINS', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADGERINS;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 23/03/2022 08:10:41
 Objetivo : Inserção de Registros na Tabela TBCADGER
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADGERINS
        (@CODUSU int, 
        @CODATR smallint = 0, 
        @SRCUSU int, 
        @NIVCFA tinyint = 3, 
        @STAUSU smallint = 2, 
        @TIPUSU tinyint, 
        @TIPPES char(1) = 'F', 
        @CODPJU char(1) = 'I', 
        @NUMIRG char(20) = '', 
        @CODCMF char(15) = '', 
        @NOMUSU varchar(70), 
        @NOMMAE varchar(70) = '', 
        @DATNAC datetime = null, 
        @ATRPPE bit = 0, 
        @DSCOCO varchar(1024) = '', 
        @CODNAC smallint = 55, 
        @STAREC tinyint = 1, 
        @UPDUSU int = 0,
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0

    SET @CODUSU =0
    EXEC @CODUSU = PRGETNXTNUM 1
    
    IF(@CODUSU>0)
       BEGIN
         IF(EXISTS (SELECT 1 FROM TBCADGER (NOLOCK) WHERE CODUSU <> @CODUSU AND CODCMF=@CODCMF AND CODATR = @CODATR AND SRCUSU=@SRCUSU AND STAREC=1))
            SET @RETURN_VALUE = -3
       END
    ELSE
       SET @RETURN_VALUE=-4

    IF(@RETURN_VALUE=0)
        BEGIN
            INSERT INTO TBCADGER(CODUSU, CODATR, SRCUSU, NIVCFA, STAUSU, TIPUSU, TIPPES, CODPJU, NUMIRG, CODCMF, NOMUSU, NOMMAE, DATNAC, ATRPPE, DSCOCO, CODNAC, STAREC, UPDUSU)
                        VALUES (@CODUSU, @CODATR, @SRCUSU, @NIVCFA, @STAUSU, @TIPUSU, @TIPPES, @CODPJU, @NUMIRG, @CODCMF, @NOMUSU, @NOMMAE, @DATNAC, @ATRPPE, @DSCOCO, @CODNAC, @STAREC, @UPDUSU);
            IF @@ERROR = 0
                BEGIN
                    SET @RETURN_VALUE = @CODUSU
                END
            ELSE
                BEGIN
                    SET @RETURN_VALUE = -1
                END
        END
    RETURN @RETURN_VALUE

GO
IF OBJECT_ID ( 'dbo.PRCADGERSEL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADGERSEL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 23/03/2022 08:10:50
 Objetivo : Seleciona o registro de acordo com o Código do Usuário
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADGERSEL
(
    @CODUSU Integer
)
AS
    SET NOCOUNT ON
    SELECT * FROM TBCADGER (NOLOCK) A

    WHERE     (A.CODUSU=@CODUSU)

GO

IF OBJECT_ID ( 'dbo.PRCADGERUPD', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADGERUPD;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 23/03/2022 08:10:50
 Objetivo : Altera um registro da tabela TBCADGER ()  de acordo com a chave primaria
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADGERUPD
        (@CODUSU int, 
        @CODATR smallint = 0, 
        @SRCUSU int, 
        @NIVCFA tinyint = 3, 
        @STAUSU smallint = 2, 
        @TIPUSU tinyint, 
        @TIPPES char(1) = 'F', 
        @CODPJU char(1) = 'I', 
        @NUMIRG char(20) = '', 
        @CODCMF char(15) = '', 
        @NOMUSU varchar(70), 
        @NOMMAE varchar(70) = '', 
        @DATNAC datetime = null, 
        @ATRPPE bit = 0, 
        @DSCOCO varchar(1024) = '', 
        @CODNAC smallint = 55, 
        @STAREC tinyint = 1, 
        @DATUPD datetime, 
        @UPDUSU int = 0,
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0
    SET @DATUPD=GETDATE();

    IF(@RETURN_VALUE=0)
        BEGIN
            UPDATE TBCADGER
               SET CODATR=@CODATR
                  ,SRCUSU=@SRCUSU
                  ,NIVCFA=@NIVCFA
                  ,STAUSU=@STAUSU
                  ,TIPUSU=@TIPUSU
                  ,TIPPES=@TIPPES
                  ,CODPJU=@CODPJU
                  ,NUMIRG=@NUMIRG
                  ,CODCMF=@CODCMF
                  ,NOMUSU=@NOMUSU
                  ,NOMMAE=@NOMMAE
                  ,DATNAC=@DATNAC
                  ,ATRPPE=@ATRPPE
                  ,DSCOCO=@DSCOCO
                  ,CODNAC=@CODNAC
                  ,STAREC=@STAREC
                  ,DATUPD=@DATUPD
                  ,UPDUSU=@UPDUSU
             WHERE (CODUSU=@CODUSU)

            IF (@@ROWCOUNT = 0)
                SET @RETURN_VALUE = -2
            ELSE
                BEGIN
                    IF (@@ERROR = 0)
                        BEGIN
                            SET @RETURN_VALUE = @CODUSU
                        END
                    ELSE
                        BEGIN
                            SET @RETURN_VALUE = -1
                        END
                END
        END
    RETURN @RETURN_VALUE
GO
