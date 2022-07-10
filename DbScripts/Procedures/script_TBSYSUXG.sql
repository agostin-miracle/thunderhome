IF OBJECT_ID ( 'dbo.PRSYSUXGINS', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRSYSUXGINS;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 13/03/2022 11:54:28
 Objetivo : Inserção de Registros na Tabela TBSYSUXG
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRSYSUXGINS
        (        @CODUSU int, 
        @SYSGRP int, 
        @STAREC tinyint = 1, 
        @UPDUSU int = 0,
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0

    IF(@RETURN_VALUE=0)
                BEGIN
                    IF(EXISTS( SELECT 1 FROM TBUSUGRP (NOLOCK) WHERE CODUSU = @CODUSU AND SYSGRP = @SYSGRP))
                        SET @RETURN_VALUE=-2;
                END

    IF(@RETURN_VALUE=0)
        BEGIN
            INSERT INTO TBSYSUXG(CODUSU, SYSGRP, STAREC, UPDUSU)
                        VALUES (@CODUSU, @SYSGRP, @STAREC, @UPDUSU);
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
IF OBJECT_ID ( 'dbo.PRUSUGRPSEL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRUSUGRPSEL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 13/03/2022 11:54:28
 Objetivo : Obtêm o registro de associação do usuário e grupo
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRUSUGRPSEL
(
    @USUGRP Integer
)
AS
    SET NOCOUNT ON
    SELECT * FROM TBSYSUXG (NOLOCK) A

    WHERE     (A.USUGRP=@USUGRP)

GO

IF OBJECT_ID ( 'dbo.PRSYSGRPSELALL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRSYSGRPSELALL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 13/03/2022 11:54:28
 Objetivo : Obtêm uma lista de todos os registros de usuário x grupo existentes
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRSYSGRPSELALL
AS
    SET NOCOUNT ON
    SELECT A.*, B.NOMUSU, C.DSCGRP, DSCREC = ISNULL(D.DSCTAB,''), LGNUSU = ISNULL(E.LGNUSU,'')
    FROM TBSYSUXG (NOLOCK) A
    INNER JOIN TBCADGER (NOLOCK) B ON (B.CODUSU = A.CODUSU)
    INNER JOIN TBSYSGRP (NOLOCK) C ON (C.SYSGRP = A.SYSGRP)
    LEFT JOIN TBTABGER (NOLOCK) D ON (D.NUMTAB = 7 AND D.KEYCOD = A.STAREC)
    LEFT JOIN TBLGNUSU (NOLOCK) E ON (E.CODUSU = A.UPDUSU AND E.REGATV=1)

     ORDER BY A.SYSGRP
GO

IF OBJECT_ID ( 'dbo.PRSYSUXGUPD', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRSYSUXGUPD;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 13/03/2022 11:54:28
 Objetivo : Altera um registro da tabela TBSYSUXG (User Group)  de acordo com a chave identity
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRSYSUXGUPD
        (@USUGRP int, 
        @CODUSU int, 
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
            UPDATE TBSYSUXG
               SET STAREC=@STAREC
                  ,DATUPD=@DATUPD
                  ,UPDUSU=@UPDUSU
             WHERE (USUGRP=@USUGRP)

            IF (@@ROWCOUNT = 0)
                SET @RETURN_VALUE = -2
            ELSE
                BEGIN
                    IF (@@ERROR = 0)
                        BEGIN
                            SET @RETURN_VALUE = @USUGRP
                        END
                    ELSE
                        BEGIN
                            SET @RETURN_VALUE = -1
                        END
                END
        END
    RETURN @RETURN_VALUE
GO
