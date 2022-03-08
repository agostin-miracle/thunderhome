IF OBJECT_ID ( 'dbo.PRCADCTOINS', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADCTOINS;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 28/02/2022 19:36:47
 Objetivo : Inserção de Registros na Tabela TBCADCTO
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADCTOINS
        (        @CODUSU int, 
        @CODEND int, 
        @TIPCTO tinyint, 
        @REGATV tinyint = 0, 
        @CODPAI smallint = 55, 
        @CODOPR smallint = 0, 
        @NUMDDD varchar(4), 
        @ISWHAT bit = 0, 
        @NUMTEL varchar(15), 
        @DSCOBS varchar(100) = '', 
        @STAREC tinyint = 1, 
        @UPDUSU int = 0,
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0

    SELECT @RETURN_VALUE = ISNULL(CODCTO,0) 
          FROM TBCADCTO (NOLOCK)    
         WHERE CODPAI = @CODPAI
      	   AND CODOPR = @CODOPR
    			 AND NUMDDD = @NUMDDD
    			 AND NUMTEL = @NUMTEL
    			 AND TIPCTO = @TIPCTO
    			 AND CODUSU = @CODUSU
    			 AND CODEND = @CODEND

    IF(@RETURN_VALUE=0)
        BEGIN
            INSERT INTO TBCADCTO(CODUSU, CODEND, TIPCTO, REGATV, CODPAI, CODOPR, NUMDDD, ISWHAT, NUMTEL, DSCOBS, STAREC, UPDUSU)
                        VALUES (@CODUSU, @CODEND, @TIPCTO, @REGATV, @CODPAI, @CODOPR, @NUMDDD, @ISWHAT, @NUMTEL, @DSCOBS, @STAREC, @UPDUSU);
            IF @@ERROR = 0
                BEGIN
                    SET @RETURN_VALUE = @@IDENTITY
                END
            ELSE
                BEGIN
                    SET @RETURN_VALUE = -1
                END
    IF(@RETURN_VALUE>0)
            BEGIN
                UPDATE TBCADCTO 
                   SET REGATV = 0 
                 WHERE CODUSU = @CODUSU
                   AND TIPCTO = @TIPCTO
                   AND CODCTO <> @RETURN_VALUE
                   AND STAREC = 1
                   AND @REGATV = 1
            END
        END
    RETURN @RETURN_VALUE

GO
IF OBJECT_ID ( 'dbo.PRCADCTOSEL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADCTOSEL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 28/02/2022 19:36:47
 Objetivo : Seleciona o registro de acordo com o código do cadastro de contatos
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADCTOSEL
(
    @CODCTO Integer
)
AS
    SET NOCOUNT ON
    SELECT * FROM TBCADCTO (nolock) A

    WHERE     (A.CODCTO=@CODCTO)

GO

IF OBJECT_ID ( 'dbo.PRCADCTOSELCTO', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADCTOSELCTO;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 28/02/2022 19:36:47
 Objetivo : Obtêm o registro de contato de acordo com os parâmetros informados
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADCTOSELCTO
(
    @CODUSU Integer,
    @TIPCTO Tinyint,
    @REGATV Tinyint = 1,
    @STAREC Tinyint = 1
)
AS
    SET NOCOUNT ON
    SELECT * FROM TBCADCTO (NOLOCK) A

    WHERE     (A.CODUSU=@CODUSU)
     AND (A.TIPCTO=@TIPCTO)
     AND (A.REGATV=@REGATV)
     AND (A.STAREC=@STAREC)

GO

IF OBJECT_ID ( 'dbo.PRCADCTOUPD', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADCTOUPD;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 28/02/2022 19:36:47
 Objetivo : Altera um registro da tabela TBCADCTO ()  de acordo com a chave identity
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADCTOUPD
        (@CODCTO int, 
        @CODUSU int, 
        @CODEND int, 
        @TIPCTO tinyint, 
        @REGATV tinyint = 0, 
        @CODPAI smallint = 55, 
        @CODOPR smallint = 0, 
        @NUMDDD varchar(4), 
        @ISWHAT bit = 0, 
        @NUMTEL varchar(15), 
        @DSCOBS varchar(100) = '', 
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
            UPDATE TBCADCTO
               SET CODUSU=@CODUSU
                  ,CODEND=@CODEND
                  ,TIPCTO=@TIPCTO
                  ,REGATV=@REGATV
                  ,CODPAI=@CODPAI
                  ,CODOPR=@CODOPR
                  ,NUMDDD=@NUMDDD
                  ,ISWHAT=@ISWHAT
                  ,NUMTEL=@NUMTEL
                  ,DSCOBS=@DSCOBS
                  ,STAREC=@STAREC
                  ,DATUPD=@DATUPD
                  ,UPDUSU=@UPDUSU
             WHERE (CODCTO=@CODCTO)

            IF (@@ROWCOUNT = 0)
                SET @RETURN_VALUE = -2
            ELSE
                BEGIN
                    IF (@@ERROR = 0)
                        BEGIN
                            SET @RETURN_VALUE = @CODCTO
                        END
                    ELSE
                        BEGIN
                            SET @RETURN_VALUE = -1
                        END
                END
        END
    IF(@RETURN_VALUE>0)
          BEGIN
              UPDATE TBCADCTO
                 SET REGATV = 0
               WHERE CODUSU = @CODUSU
                 AND TIPCTO = @TIPCTO
                 AND CODCTO <> @RETURN_VALUE
                 AND STAREC = 1
                 AND @REGATV = 1
          END
    RETURN @RETURN_VALUE
GO
IF OBJECT_ID ( 'dbo.PRCADCTOSELALL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADCTOSELALL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 28/02/2022 19:36:48
 Objetivo : Seleciona todos os registros de contato do usuário fornecido
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADCTOSELALL
(
    @CODUSU Integer=NULL
)
AS
    SET NOCOUNT ON
    SELECT    
        CODCTO
        ,A.CODUSU
    	,B.NOMUSU
        ,A.CODEND
    	,C.DSCEND
        ,A.TIPCTO
    	,D.DSCCTO
        ,A.REGATV
    	,DSCATV = CASE WHEN (A.REGATV=1) THEN 'Sim' ELSE 'Não' END
        ,A.CODPAI
    	,DSCPAI = ISNULL(E.DSCTAB,'')
        ,CODOPR
        ,DSCOPR = ISNULL(F.DSCTAB,'')
        ,NUMDDD
        ,ISWHAT
        ,NUMTEL
        ,DSCOBS
        ,A.STAREC
        ,DSCREC = ISNULL(G.DSCTAB,'')
        ,DATCAD = FORMAT(A.DATCAD, 'dd/MM/yyyy HH:mm')
        ,DATUPD = FORMAT(A.DATUPD, 'dd/MM/yyyy HH:mm')    
        ,A.UPDUSU
    	,LGNUSU  = ISNULL(H.LGNUSU,'')
      FROM TBCADCTO (NOLOCK) A
     INNER JOIN TBCADGER (NOLOCK) B ON (B.CODUSU = A.CODUSU)
      LEFT JOIN TBCADEND (NOLOCK) C ON (C.CODEND = A.CODEND)
     INNER JOIN TBTIPCTO (NOLOCK) D ON (D.TIPCTO = A.TIPCTO)
     INNER JOIN TBTABGER (NOLOCK) E ON (E.NUMTAB= 1 AND E.KEYCOD = A.CODPAI)
      LEFT JOIN TBTABGER (NOLOCK) F ON (F.NUMTAB= 45 AND F.KEYCOD = A.CODOPR)
     INNER JOIN TBTABGER (NOLOCK) G ON (G.NUMTAB= 7 AND G.KEYCOD = A.STAREC)
      LEFT JOIN TBLGNUSU (NOLOCK) H ON (H.CODUSU = A.UPDUSU AND H.REGATV=1)
     WHERE       (@CODUSU IS NULL OR A.CODUSU=@CODUSU)



GO

IF OBJECT_ID ( 'dbo.PRCADCTOFIN', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADCTOFIN;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 28/02/2022 19:36:48
 Objetivo : Localiza um contato com base nos parâmetros fornecidos
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADCTOFIN
(
    @CODUSU Integer,
    @CODEND Integer,
    @TIPCTO Integer,
    @NUMTEL varchar(15),
    @CODPAI smallint=NULL,
    @CODOPR smallint=NULL,
    @NUMDDD varchar(4)=NULL,
    @REGATV Tinyint=NULL
)
AS
    SET NOCOUNT ON

    DECLARE @RETURN_VALUE INT = 0
    SELECT TOP 1 @RETURN_VALUE = COALESCE(CODCTO,0) 
      FROM TBCADCTO (NOLOCK) 
     WHERE (@CODPAI IS NULL OR CODPAI = @CODPAI)
    	 AND (@CODOPR IS NULL OR CODOPR = @CODOPR)
    	 AND (@NUMDDD IS NULL OR NUMDDD = @NUMDDD)
    	 AND (@NUMTEL IS NULL OR NUMTEL = @NUMTEL)
       AND (@REGATV IS NULL OR REGATV = @REGATV)   
    	 AND TIPCTO = @TIPCTO
    	 AND CODUSU = @CODUSU
    	 AND CODEND = @CODEND    
    RETURN @RETURN_VALUE;

GO

