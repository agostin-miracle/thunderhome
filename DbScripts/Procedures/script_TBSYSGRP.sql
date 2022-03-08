IF OBJECT_ID ( 'dbo.PRSYSGRPINS', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRSYSGRPINS;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 04/03/2022 15:11:15
 Objetivo : Inserção de Registros na Tabela TBSYSGRP
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRSYSGRPINS
        (@SYSGRP int, 
        @DSCGRP varchar(30), 
        @STAREC tinyint = 1, 
        @UPDUSU int = 0,
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0
    SELECT @SYSGRP = ISNULL(MAX(SYSGRP),0)+1 from TBSYSGRP WITH (NOLOCK)

    IF((@DSCGRP IS NULL) OR (@DSCGRP = ''))
               SET @RETURN_VALUE=-3;
    
            IF(@RETURN_VALUE=0)
                BEGIN
                    IF(EXISTS( SELECT 1 FROM TBSYSGRP (NOLOCK) WHERE DSCGRP=@DSCGRP))
                        SET @RETURN_VALUE=-2;
                END

    IF(@RETURN_VALUE=0)
        BEGIN
            INSERT INTO TBSYSGRP(SYSGRP, DSCGRP, STAREC, UPDUSU)
                        VALUES (@SYSGRP, @DSCGRP, @STAREC, @UPDUSU);
            IF @@ERROR = 0
                BEGIN
                    SET @RETURN_VALUE = @SYSGRP
                END
            ELSE
                BEGIN
                    SET @RETURN_VALUE = -1
                END
        END
    RETURN @RETURN_VALUE

GO
IF OBJECT_ID ( 'dbo.PRSYSGRPSEL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRSYSGRPSEL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 04/03/2022 15:11:15
 Objetivo : Obtêm o registro do grupo
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRSYSGRPSEL
(
    @SYSGRP Integer
)
AS
    SET NOCOUNT ON
    SELECT * FROM TBSYSGRP (NOLOCK) A

    WHERE     (A.SYSGRP=@SYSGRP)

GO

IF OBJECT_ID ( 'dbo.PRSYSGRPSELALL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRSYSGRPSELALL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 04/03/2022 15:11:15
 Objetivo : Obtêm uma lista de todos os grupos existentes
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRSYSGRPSELALL
AS
    SET NOCOUNT ON
    SELECT A.*, DSCREC= ISNULL(B.DSCTAB,''), LGNUSU= ISNULL(C.LGNUSU,'') FROM TBSYSGRP (NOLOCK) A INNER JOIN TBTABGER (NOLOCK) B ON (B.NUMTAB=7 AND B.KEYCOD = A.STAREC) LEFT JOIN TBLGNUSU (NOLOCK) C ON (C.CODUSU = A.UPDUSU AND C.REGATV=1)

     ORDER BY A.SYSGRP
GO

IF OBJECT_ID ( 'dbo.PRSYSGRPUPD', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRSYSGRPUPD;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 04/03/2022 15:11:15
 Objetivo : Altera um registro da tabela TBSYSGRP (Groups)  de acordo com a chave primaria
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRSYSGRPUPD
        (@SYSGRP int, 
        @DSCGRP varchar(30), 
        @STAREC tinyint = 1, 
        @DATUPD datetime, 
        @UPDUSU int = 0,
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0
    SET @DATUPD=GETDATE();
            
            IF((@DSCGRP IS NULL) OR (@DSCGRP = ''))
               SET @RETURN_VALUE=-3;
    
            IF(@RETURN_VALUE=0)
                BEGIN
                    IF(EXISTS( SELECT 1 FROM TBSYSGRP (NOLOCK) WHERE DSCGRP=@DSCGRP AND SYSGRP<>@SYSGRP) )
                        SET @RETURN_VALUE=-2;
                END

    IF(@RETURN_VALUE=0)
        BEGIN
            UPDATE TBSYSGRP
               SET DSCGRP=@DSCGRP
                  ,STAREC=@STAREC
                  ,DATUPD=@DATUPD
                  ,UPDUSU=@UPDUSU
             WHERE (SYSGRP=@SYSGRP)

            IF (@@ROWCOUNT = 0)
                SET @RETURN_VALUE = -2
            ELSE
                BEGIN
                    IF (@@ERROR = 0)
                        BEGIN
                            SET @RETURN_VALUE = @SYSGRP
                        END
                    ELSE
                        BEGIN
                            SET @RETURN_VALUE = -1
                        END
                END
        END
    RETURN @RETURN_VALUE
GO
