IF OBJECT_ID ( 'dbo.PRTARMOVINS', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRTARMOVINS;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 13/03/2022 14:04:02
 Objetivo : Inserção de Registros na Tabela TBTARMOV
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRTARMOVINS
        (        @CODTAR smallint, 
        @CODMOV smallint, 
        @IDEPRE smallint = 0, 
        @STAREC tinyint = 1, 
        @UPDUSU int = 0,
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0

    IF(@CODTAR<=0)
       SET @RETURN_VALUE=-2
    IF(@CODMOV<0)
       SET @RETURN_VALUE=-3

    IF(@RETURN_VALUE=0)
        BEGIN
            INSERT INTO TBTARMOV(CODTAR, CODMOV, IDEPRE, STAREC, UPDUSU)
                        VALUES (@CODTAR, @CODMOV, @IDEPRE, @STAREC, @UPDUSU);
            IF @@ERROR = 0
                BEGIN
                    SET @RETURN_VALUE = @@IDENTITY
                END
            ELSE
                BEGIN
                    SET @RETURN_VALUE = -1
                END
        END
    RETURN @RETURN_VALUE

GO
IF OBJECT_ID ( 'dbo.PRTARMOVSEL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRTARMOVSEL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 13/03/2022 14:04:02
 Objetivo : Seleciona o registro de acordo com o codigo do produto
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRTARMOVSEL
(
    @NIDTXM Integer
)
AS
    SET NOCOUNT ON
    SELECT A.*, DSCREC = ISNULL(B.DSCTAB,''), LGNUSU = ISNULL(C.LGNUSU,''), D.DSCTAR, E.DSCMOV
      FROM TBTARMOV (NOLOCK) A
     INNER JOIN TBTABGER (NOLOCK) B ON (B.NUMTAB =  7 AND B.KEYCOD = A.STAREC)
      LEFT JOIN TBLGNUSU (NOLOCK) C ON (C.CODUSU = A.UPDUSU AND C.REGATV = 1)
     INNER JOIN TBTIPTAR (NOLOCK) D ON (D.CODTAR = A.CODTAR)
     INNER JOIN TBTIPMOV (NOLOCK) E ON (E.CODMOV = A.CODMOV)

    WHERE     (A.NIDTXM=@NIDTXM)

GO

IF OBJECT_ID ( 'dbo.PRTARMOVUPD', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRTARMOVUPD;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 13/03/2022 14:04:02
 Objetivo : Altera um registro da tabela TBTARMOV ()  de acordo com a chave identity
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRTARMOVUPD
        (@NIDTXM int, 
        @CODTAR smallint, 
        @CODMOV smallint, 
        @IDEPRE smallint = 0, 
        @STAREC tinyint = 1, 
        @DATUPD datetime, 
        @UPDUSU int = 0,
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0
    SET @DATUPD=GETDATE()
    IF(@CODTAR<=0)
       SET @RETURN_VALUE=-2
    IF(@CODMOV<0)
       SET @RETURN_VALUE=-3

    IF(@RETURN_VALUE=0)
        BEGIN
            UPDATE TBTARMOV
               SET IDEPRE=@IDEPRE
                  ,STAREC=@STAREC
                  ,DATUPD=@DATUPD
                  ,UPDUSU=@UPDUSU
             WHERE (NIDTXM=@NIDTXM)

            IF (@@ROWCOUNT = 0)
                SET @RETURN_VALUE = -2
            ELSE
                BEGIN
                    IF (@@ERROR = 0)
                        BEGIN
                            SET @RETURN_VALUE = @NIDTXM
                        END
                    ELSE
                        BEGIN
                            SET @RETURN_VALUE = -1
                        END
                END
        END
    RETURN @RETURN_VALUE
GO
IF OBJECT_ID ( 'dbo.PRTARMOVSELALL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRTARMOVSELALL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 13/03/2022 14:04:02
 Objetivo : Seleciona todos os registros de expansão de tarifa do id de tarifação fornecido
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRTARMOVSELALL
AS
    SET NOCOUNT ON
    SELECT A.*, DSCREC = ISNULL(B.DSCTAB,''), LGNUSU = ISNULL(C.LGNUSU,''), D.DSCTAR, E.DSCMOV
      FROM TBTARMOV (NOLOCK) A
     INNER JOIN TBTABGER (NOLOCK) B ON (B.NUMTAB =  7 AND B.KEYCOD = A.STAREC)
      LEFT JOIN TBLGNUSU (NOLOCK) C ON (C.CODUSU = A.UPDUSU AND C.REGATV = 1)
     INNER JOIN TBTIPTAR (NOLOCK) D ON (D.CODTAR = A.CODTAR)
     INNER JOIN TBTIPMOV (NOLOCK) E ON (E.CODMOV = A.CODMOV)



GO

