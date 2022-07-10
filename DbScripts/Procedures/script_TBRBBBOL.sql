IF OBJECT_ID ( 'dbo.PRRBBBOLINS', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRRBBBOLINS;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 10/04/2022 11:51:58
 Objetivo : Inserção de Registros na Tabela TBRBBBOL
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRRBBBOLINS
        (        @NIDLIM int = 0, 
        @TIPBXA tinyint = 1, 
        @DATPGT date, 
        @FILNAM varchar(255) = '', 
        @NUMROW smallint, 
        @USUBCO int, 
        @NUMREX varchar(25) = '', 
        @TOTPAG money = 0, 
        @TOTJUR money = 0, 
        @TOTDES money = 0, 
        @TOTTAR money = 0, 
        @TOTTEX money = 0, 
        @TOTDCP money = 0, 
        @TOTLIQ money = 0, 
        @STAREC tinyint = 1, 
        @UPDUSU int = 0,
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0

    IF(@NUMREX<>'')
        BEGIN
           IF(EXISTS(SELECT 1 FROM TBRBBBOL (NOLOCK) WHERE NUMREX=@NUMREX AND STAREC=1))
               SET @RETURN_VALUE=-3
        END

    IF(@RETURN_VALUE=0)
        BEGIN
            INSERT INTO TBRBBBOL(NIDLIM, TIPBXA, DATPGT, FILNAM, NUMROW, USUBCO, NUMREX, TOTPAG, TOTJUR, TOTDES, TOTTAR, TOTTEX, TOTDCP, TOTLIQ, STAREC, UPDUSU)
                        VALUES (@NIDLIM, @TIPBXA, @DATPGT, @FILNAM, @NUMROW, @USUBCO, @NUMREX, @TOTPAG, @TOTJUR, @TOTDES, @TOTTAR, @TOTTEX, @TOTDCP, @TOTLIQ, @STAREC, @UPDUSU);
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
IF OBJECT_ID ( 'dbo.PRRBBBOLSEL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRRBBBOLSEL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 10/04/2022 11:51:58
 Objetivo : Seleciona o registro de acordo com o id de registro do conta corrente
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRRBBBOLSEL
(
    @NIDRBB Integer
)
AS
    SET NOCOUNT ON
    SELECT * FROM TBRBBBOL (NOLOCK) A

    WHERE     (A.NIDRBB=@NIDRBB)

GO

IF OBJECT_ID ( 'dbo.PRRBBBOLUPD', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRRBBBOLUPD;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 10/04/2022 11:51:58
 Objetivo : Altera um registro da tabela TBRBBBOL (Ticket Receipt Record)  de acordo com a chave identity
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRRBBBOLUPD
        (@NIDRBB int, 
        @NIDLIM int = 0, 
        @TIPBXA tinyint = 1, 
        @DATPGT date, 
        @FILNAM varchar(255) = '', 
        @NUMROW smallint, 
        @USUBCO int, 
        @NUMREX varchar(25) = '', 
        @TOTPAG money = 0, 
        @TOTJUR money = 0, 
        @TOTDES money = 0, 
        @TOTTAR money = 0, 
        @TOTTEX money = 0, 
        @TOTDCP money = 0, 
        @TOTLIQ money = 0, 
        @STAREC tinyint = 1, 
        @DATUPD datetime, 
        @UPDUSU int = 0,
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0
    SET @DATUPD =GETDATE();

    IF(@RETURN_VALUE=0)
        BEGIN
            UPDATE TBRBBBOL
               SET NIDLIM=@NIDLIM
                  ,TIPBXA=@TIPBXA
                  ,DATPGT=@DATPGT
                  ,FILNAM=@FILNAM
                  ,NUMROW=@NUMROW
                  ,USUBCO=@USUBCO
                  ,NUMREX=@NUMREX
                  ,TOTPAG=@TOTPAG
                  ,TOTJUR=@TOTJUR
                  ,TOTDES=@TOTDES
                  ,TOTTAR=@TOTTAR
                  ,TOTTEX=@TOTTEX
                  ,TOTDCP=@TOTDCP
                  ,TOTLIQ=@TOTLIQ
                  ,STAREC=@STAREC
                  ,DATUPD=@DATUPD
                  ,UPDUSU=@UPDUSU
             WHERE (NIDRBB=@NIDRBB)

            IF (@@ROWCOUNT = 0)
                SET @RETURN_VALUE = -2
            ELSE
                BEGIN
                    IF (@@ERROR = 0)
                        BEGIN
                            SET @RETURN_VALUE = @NIDRBB
                        END
                    ELSE
                        BEGIN
                            SET @RETURN_VALUE = -1
                        END
                END
        END
    RETURN @RETURN_VALUE
GO
IF OBJECT_ID ( 'dbo.PRRBBBOLCLOBAT', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRRBBBOLCLOBAT;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 10/04/2022 11:51:58
 Objetivo : Executa o fechamento de um lote de Registro de Recebimento de Boleto
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRRBBBOLCLOBAT
(
    @NIDRBB Integer
)
AS
    SET NOCOUNT ON

    UPDATE TBRBBBOL SET STAREC = 9 
     WHERE NUMROW = (SELECT COUNT(*) 
      FROM TBBXABOL WHERE NIDRBB=@NIDRBB 
        AND STAREC=9)
    AND NIDRBB=@NIDRBB

GO

