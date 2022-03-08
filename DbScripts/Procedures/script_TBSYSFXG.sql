IF OBJECT_ID ( 'dbo.PRSYSFXGINS', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRSYSFXGINS;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 04/03/2022 21:11:49
 Objetivo : Inserção de Registros na Tabela TBSYSFXG
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRSYSFXGINS
        (        @SYSFUN int, 
        @SYSGRP int, 
        @STAREC tinyint = 1, 
        @UPDUSU int = 0,
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0

    IF(@RETURN_VALUE=0)
                BEGIN
                    IF(EXISTS( SELECT 1 FROM TBSYSFXG (NOLOCK) WHERE SYSFUN = @SYSFUN AND SYSGRP = @SYSGRP))
                        SET @RETURN_VALUE=-2;
                END

    IF(@RETURN_VALUE=0)
        BEGIN
            INSERT INTO TBSYSFXG(SYSFUN, SYSGRP, STAREC, UPDUSU)
                        VALUES (@SYSFUN, @SYSGRP, @STAREC, @UPDUSU);
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
IF OBJECT_ID ( 'dbo.PRSYSFXGSEL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRSYSFXGSEL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 04/03/2022 21:11:49
 Objetivo : Obtêm o registro de associação do funcionalidade x grupo
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRSYSFXGSEL
(
    @FUNGRP Integer
)
AS
    SET NOCOUNT ON
    SELECT * FROM TBSYSFXG (NOLOCK) A

    WHERE     (A.FUNGRP=@FUNGRP)

GO

IF OBJECT_ID ( 'dbo.PRSYSFXGSELALL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRSYSFXGSELALL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 04/03/2022 21:11:49
 Objetivo : Obtêm uma lista de todos os registros de usuário x grupo existentes
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRSYSFXGSELALL
(
    @SYSGRP Integer
)
AS
    SET NOCOUNT ON
    SELECT A.*, B.SYSDSC, C.DSCGRP, DSCREC = ISNULL(D.DSCTAB,''), LGNUSU = ISNULL(E.LGNUSU,'') FROM TBSYSFXG (NOLOCK) A
          INNER JOIN TBSYSFUN (NOLOCK) B ON (B.SYSFUN = a.SYSFUN)
          INNER JOIN TBSYSGRP (NOLOCK) C ON (C.SYSGRP = A.SYSGRP)
          INNER JOIN TBTABGER (NOLOCK) D ON (D.NUMTAB = 7 AND D.KEYCOD = A.STAREC)
          LEFT JOIN TBLGNUSU (NOLOCK) E ON (E.CODUSU = A.UPDUSU AND E.REGATV = 1)

    WHERE     (A.SYSGRP=@SYSGRP)
     ORDER BY A.SYSGRP
GO

IF OBJECT_ID ( 'dbo.PRSYSFXGUPD', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRSYSFXGUPD;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 04/03/2022 21:11:49
 Objetivo : Altera um registro da tabela TBSYSFXG (Features x Group)  de acordo com a chave identity
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRSYSFXGUPD
        (@FUNGRP int, 
        @SYSFUN int, 
        @SYSGRP int, 
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
                    IF(NOT (EXISTS( SELECT 1 FROM TBSYSFXG (NOLOCK) WHERE SYSFUN = @SYSFUN AND SYSGRP = @SYSGRP)))
                        SET @RETURN_VALUE=-2;
                END

    IF(@RETURN_VALUE=0)
        BEGIN
            UPDATE TBSYSFXG
               SET STAREC=@STAREC
                  ,DATUPD=@DATUPD
                  ,UPDUSU=@UPDUSU
             WHERE (FUNGRP=@FUNGRP)

            IF (@@ROWCOUNT = 0)
                SET @RETURN_VALUE = -2
            ELSE
                BEGIN
                    IF (@@ERROR = 0)
                        BEGIN
                            SET @RETURN_VALUE = @FUNGRP
                        END
                    ELSE
                        BEGIN
                            SET @RETURN_VALUE = -1
                        END
                END
        END
    RETURN @RETURN_VALUE
GO
