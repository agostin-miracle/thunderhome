IF OBJECT_ID ( 'dbo.PRMODTARINS', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRMODTARINS;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 15/03/2022 19:56:49
 Objetivo : Inserção de Registros na Tabela TBMODTAR
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRMODTARINS
        (@MODCRT smallint = 0, 
        @DSCMOD varchar(25), 
        @PARINI tinyint = 0, 
        @PARFIN tinyint = 0, 
        @STAREC tinyint = 1, 
        @UPDUSU int = 0,
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0
    SELECT @MODCRT = ISNULL(MAX(MODCRT),0)+1 from TBMODTAR WITH (NOLOCK)

    IF(@RETURN_VALUE=0)
        BEGIN
            INSERT INTO TBMODTAR(MODCRT, DSCMOD, PARINI, PARFIN, STAREC, UPDUSU)
                        VALUES (@MODCRT, @DSCMOD, @PARINI, @PARFIN, @STAREC, @UPDUSU);
            IF @@ERROR = 0
                BEGIN
                    SET @RETURN_VALUE = @MODCRT
                END
            ELSE
                BEGIN
                    SET @RETURN_VALUE = -1
                END
        END
    RETURN @RETURN_VALUE

GO
IF OBJECT_ID ( 'dbo.PRMODTARSEL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRMODTARSEL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 15/03/2022 19:56:49
 Objetivo : Seleciona o registro de acordo com o codigo dA modalidade
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRMODTARSEL
(
    @MODCRT Integer
)
AS
    SET NOCOUNT ON
    SELECT A.MODCRT,
      A.DSCMOD,
      A.PARINI,
      A.PARFIN,
      EXTMOD =
          CASE
    		WHEN PARINI = 0 AND PARFIN=0 THEN ' '
    		WHEN (PARINI >0 AND PARFIN=0) THEN 'EM ' + CONVERT(VARCHAR,PARINI) + ' VEZES'
    		WHEN (PARINI >0 AND PARFIN>0) THEN 'DE ' + CONVERT(VARCHAR,PARINI) + ' A '  + CONVERT(VARCHAR,PARFIN) + ' VEZES'
    		WHEN (PARINI =0 AND PARFIN>0) THEN 'ATE ' + CONVERT(VARCHAR,PARFIN) + ' VEZES'
    	END,
      A.STAREC,
      DSCREC = ISNULL(B.DSCTAB,''),
      A.DATCAD,
      A.DATUPD,
      A.UPDUSU,
      LGNUSU = ISNULL(C.LGNUSU,'')
      FROM TBMODTAR (NOLOCK) A
     INNER JOIN TBTABGER (NOLOCK) B ON (B.NUMTAB =  7 AND B.KEYCOD = A.STAREC)
      LEFT JOIN TBLGNUSU (NOLOCK) C ON (C.CODUSU = A.UPDUSU AND C.REGATV=1)

    WHERE     (A.MODCRT=@MODCRT)

GO

IF OBJECT_ID ( 'dbo.PRMODTARUPD', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRMODTARUPD;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 15/03/2022 19:56:49
 Objetivo : Altera um registro da tabela TBMODTAR (Tariff Modality)  de acordo com a chave primaria
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRMODTARUPD
        (@MODCRT smallint = 0, 
        @DSCMOD varchar(25), 
        @PARINI tinyint = 0, 
        @PARFIN tinyint = 0, 
        @STAREC tinyint = 1, 
        @DATUPD datetime, 
        @UPDUSU int = 0,
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0
    SET @DATUPD=GETDATE()

    IF(@RETURN_VALUE=0)
        BEGIN
            UPDATE TBMODTAR
               SET DSCMOD=@DSCMOD
                  ,PARINI=@PARINI
                  ,PARFIN=@PARFIN
                  ,STAREC=@STAREC
                  ,DATUPD=@DATUPD
                  ,UPDUSU=@UPDUSU
             WHERE (MODCRT=@MODCRT)

            IF (@@ROWCOUNT = 0)
                SET @RETURN_VALUE = -2
            ELSE
                BEGIN
                    IF (@@ERROR = 0)
                        BEGIN
                            SET @RETURN_VALUE = @MODCRT
                        END
                    ELSE
                        BEGIN
                            SET @RETURN_VALUE = -1
                        END
                END
        END
    RETURN @RETURN_VALUE
GO
IF OBJECT_ID ( 'dbo.PRMODTARSELALL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRMODTARSELALL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 15/03/2022 19:56:49
 Objetivo : Seleciona todos os registros de modalidade de tarifa existentes
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRMODTARSELALL
AS
    SET NOCOUNT ON
    SELECT A.MODCRT,
      A.DSCMOD,
      A.PARINI,
      A.PARFIN,
      EXTMOD = 
          CASE 
    		WHEN PARINI = 0 AND PARFIN=0 THEN ' '
    		WHEN (PARINI >0 AND PARFIN=0) THEN 'EM ' + CONVERT(VARCHAR,PARINI) + ' VEZES'
    		WHEN (PARINI >0 AND PARFIN>0) THEN 'DE ' + CONVERT(VARCHAR,PARINI) + ' A '  + CONVERT(VARCHAR,PARFIN) + ' VEZES'
    		WHEN (PARINI =0 AND PARFIN>0) THEN 'ATE ' + CONVERT(VARCHAR,PARFIN) + ' VEZES'
    	END, 
      A.STAREC,
      DSCREC = ISNULL(B.DSCTAB,''),
      A.DATCAD,
      A.DATUPD,
      A.UPDUSU,
      LGNUSU = ISNULL(C.LGNUSU,'')
      FROM TBMODTAR (NOLOCK) A 
     INNER JOIN TBTABGER (NOLOCK) B ON (B.NUMTAB =  7 AND B.KEYCOD = A.STAREC)
      LEFT JOIN TBLGNUSU (NOLOCK) C ON (C.CODUSU = A.UPDUSU AND C.REGATV=1)



GO

