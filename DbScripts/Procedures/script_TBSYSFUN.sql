IF OBJECT_ID ( 'dbo.PRSYSFUNINS', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRSYSFUNINS;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 23/03/2022 13:18:54
 Objetivo : Inserção de Registros na Tabela TBSYSFUN
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRSYSFUNINS
        (        @SYSAPL tinyint = 0, 
        @SYSTBL smallint = 0, 
        @SYSROL smallint = 0, 
        @SYSMTH varchar(100), 
        @SYSPRC varchar(100) = '', 
        @SYSDSC varchar(255), 
        @STAREC tinyint = 1, 
        @UPDUSU int = 0,
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0

    IF(EXISTS(SELECT 1 
                        FROM TBSYSFUN (NOLOCK) A 
                       WHERE A.SYSAPL = @SYSAPL
                         AND A.SYSTBL = @SYSTBL 
                         AND A.SYSROL = @SYSROL))
              BEGIN                     
                  SET @RETURN_VALUE=-2;
              END

    IF(@RETURN_VALUE=0)
        BEGIN
            INSERT INTO TBSYSFUN(SYSAPL, SYSTBL, SYSROL, SYSMTH, SYSPRC, SYSDSC, STAREC, UPDUSU)
                        VALUES (@SYSAPL, @SYSTBL, @SYSROL, @SYSMTH, @SYSPRC, @SYSDSC, @STAREC, @UPDUSU);
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
IF OBJECT_ID ( 'dbo.PRSYSFUNSEL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRSYSFUNSEL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 23/03/2022 13:18:54
 Objetivo : Obtêm o registro de uma funcionalidade de acordo com o id
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRSYSFUNSEL
(
    @SYSFUN Integer
)
AS
    SET NOCOUNT ON
    SELECT * FROM TBSYSFUN (NOLOCK) A

    WHERE     (A.SYSFUN=@SYSFUN)

GO

IF OBJECT_ID ( 'dbo.PRSYSFUNUPD', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRSYSFUNUPD;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 23/03/2022 13:18:54
 Objetivo : Altera um registro da tabela TBSYSFUN (System Features)  de acordo com a chave identity
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRSYSFUNUPD
        (@SYSFUN int, 
        @SYSAPL tinyint = 0, 
        @SYSTBL smallint = 0, 
        @SYSROL smallint = 0, 
        @SYSMTH varchar(100), 
        @SYSPRC varchar(100) = '', 
        @SYSDSC varchar(255), 
        @STAREC tinyint = 1, 
        @DATUPD datetime, 
        @UPDUSU int = 0,
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0
    IF(NOT (EXISTS(SELECT 1
                    FROM TBSYSFUN (NOLOCK) A
                   WHERE A.SYSAPL = @SYSAPL
                     AND A.SYSTBL = @SYSTBL
                     AND A.SYSROL = @SYSROL)))
        BEGIN                 
            SET @RETURN_VALUE=-2;
        END

    IF(@RETURN_VALUE=0)
        BEGIN
            UPDATE TBSYSFUN
               SET SYSAPL=@SYSAPL
                  ,SYSTBL=@SYSTBL
                  ,SYSROL=@SYSROL
                  ,SYSMTH=@SYSMTH
                  ,SYSPRC=@SYSPRC
                  ,SYSDSC=@SYSDSC
                  ,STAREC=@STAREC
                  ,DATUPD=@DATUPD
                  ,UPDUSU=@UPDUSU
             WHERE (SYSFUN=@SYSFUN)

            IF (@@ROWCOUNT = 0)
                SET @RETURN_VALUE = -2
            ELSE
                BEGIN
                    IF (@@ERROR = 0)
                        BEGIN
                            SET @RETURN_VALUE = @SYSFUN
                        END
                    ELSE
                        BEGIN
                            SET @RETURN_VALUE = -1
                        END
                END
        END
    RETURN @RETURN_VALUE
GO
IF OBJECT_ID ( 'dbo.PRSYSFUNSELALL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRSYSFUNSELALL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 23/03/2022 13:18:54
 Objetivo : Obtêm todos os registros de funcionalidades específicas para uma tabela
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRSYSFUNSELALL
(
    @SYSTBL smallint=NULL
)
AS
    SET NOCOUNT ON
    SELECT A.*, DSCREC= ISNULL(B.DSCTAB,''), LGNUSU = ISNULL(C.LGNUSU,'') FROM TBSYSFUN (NOLOCK) A
    INNER JOIN TBTABGER (NOLOCK) B ON (B.NUMTAB=7 AND B.KEYCOD=A.STAREC)
    LEFT JOIN TBLGNUSU (NOLOCK) C ON (C.CODUSU = A.UPDUSU AND C.REGATV=1)
     WHERE       (@SYSTBL IS NULL OR A.SYSTBL=@SYSTBL)



GO

