IF OBJECT_ID ( 'dbo.PRCADCVLINS', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADCVLINS;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 26/03/2022 17:55:25
 Objetivo : Inserção de Registros na Tabela TBCADCVL
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADCVLINS
        (        @CODUSU int, 
        @NIDCTA int, 
        @STAREC tinyint = 1, 
        @DATUPD datetime, 
        @UPDUSU int = 0,
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0

    DECLARE @EXIREC TINYINT=0
    IF(EXISTS(SELECT 1 FROM TBCADCTA (NOLOCK) WHERE NIDCTA =@NIDCTA))
       BEGIN
          IF(EXISTS(SELECT 1 FROM TBCADCVL (NOLOCK) WHERE CODUSU=@CODUSU AND NIDCTA =@NIDCTA))
              BEGIN
                  SELECT @EXIREC = STAREC FROM TBCADCVL (NOLOCK) WHERE CODUSU=@CODUSU AND NIDCTA =@NIDCTA
                  IF(@EXIREC=0)
                      BEGIN
                          UPDATE TBCADCVL SET STAREC=1, DATUPD = GETDATE(), UPDUSU=@UPDUSU WHERE CODUSU=@CODUSU AND NIDCTA =@NIDCTA
                          SET @RETURN_VALUE= 2
                      END
              END                  
         END        
    ELSE
        SET @RETURN_VALUE = -3

    IF(@RETURN_VALUE=0)
        BEGIN
            INSERT INTO TBCADCVL(CODUSU, NIDCTA, STAREC, DATUPD, UPDUSU)
                        VALUES (@CODUSU, @NIDCTA, @STAREC, @DATUPD, @UPDUSU);
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
IF OBJECT_ID ( 'dbo.PRCADCVLUPD', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADCVLUPD;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 26/03/2022 17:55:25
 Objetivo : Altera um registro da tabela TBCADCVL (Linked Account)  de acordo com a chave identity
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADCVLUPD
        (@NIDCVL int, 
        @CODUSU int, 
        @NIDCTA int, 
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
            UPDATE TBCADCVL
               SET STAREC=@STAREC
                  ,DATUPD=@DATUPD
                  ,UPDUSU=@UPDUSU
             WHERE (NIDCVL=@NIDCVL)

            IF (@@ROWCOUNT = 0)
                SET @RETURN_VALUE = -2
            ELSE
                BEGIN
                    IF (@@ERROR = 0)
                        BEGIN
                            SET @RETURN_VALUE = @NIDCVL
                        END
                    ELSE
                        BEGIN
                            SET @RETURN_VALUE = -1
                        END
                END
        END
    RETURN @RETURN_VALUE
GO
IF OBJECT_ID ( 'dbo.PRCADCVLREMLNK', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADCVLREMLNK;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 26/03/2022 17:55:25
 Objetivo : Remove um conta vinculada da lista
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADCVLREMLNK
(
    @CODUSU Integer,
    @NIDCTA Integer,
    @UPDUSU Integer
)
AS
    SET NOCOUNT ON

    DECLARE @RETURN_VALUE INT =0          
    DECLARE @CANEXE INT =0          
    IF(@UPDUSU<=0)
       SET @RETURN_VALUE = -2
    
    IF(@RETURN_VALUE>0 AND @CANEXE=1)  
       BEGIN
          IF(EXISTS(SELECT 1 FROM TBCADCVL (NOLOCK) WHERE CODUSU = @CODUSU AND NIDCTA=@NIDCTA ) AND @RETURN_VALUE=0)         
              BEGIN
                  UPDATE TBCADCVL SET STAREC=0, UPDUSU=@UPDUSU, DATUPD=GETDATE() WHERE CODUSU=@CODUSU AND NIDCTA = @NIDCTA
                  SET @RETURN_VALUE=1
              END
       END          
    ELSE
       SET @RETURN_VALUE=-1
    RETURN @RETURN_VALUE

GO

IF OBJECT_ID ( 'dbo.PRCADCVLLOCACT', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCADCVLLOCACT;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 26/03/2022 17:55:25
 Objetivo : Localiza uma conta digital conforme os parâmetros fornecidos
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCADCVLLOCACT
(
    @METPSQ Tinyint,
    @NOMPSQ varchar(25),
    @CODUSU Integer
    ,@RETURN_VALUE INT OUTPUT
)
AS
    SET NOCOUNT ON

    SET @RETURN_VALUE=0
    DECLARE @NIDCTA INT=0
    SET @NOMPSQ= LTRIM(RTRIM(@NOMPSQ))
    IF(@CODUSU IS NULL OR @CODUSU <=0)
       SET @RETURN_VALUE=-2
       
    IF(@NOMPSQ<>'' AND @RETURN_VALUE=0)
    		BEGIN
    			IF(@METPSQ=1)
    				SELECT @NIDCTA = NIDCTA 
     				  FROM TBCADCTA (NOLOCK) 
             WHERE ORGCTA = 1
               AND stacta = 250
               AND starec = 1
               AND codusu in (SELECT CODUSU FROM TBCADGER (NOLOCK) WHERE CODCMF = @NOMPSQ AND STAREC=1 AND STAUSU=61)     	
    			ELSE
    				SELECT @NIDCTA = NIDCTA FROM TBCADCTA (NOLOCK) where orgcta=1 AND STACTA=250 AND STAREC=1 and NUMCTA = @NOMPSQ
    				  SET @RETURN_VALUE = ISNULL(@NIDCTA,0)
    		END
    
    /*
    IF(@NIDCTA>0 AND @RETURN_VALUE=0)
    		BEGIN
    			IF (EXISTS(SELECT 1 FROM TBCADCVL (NOLOCK) WHERE CODUSU=@CODUSU AND NIDCTA = @NIDCTA AND STAREC=1))
    			    SET @RETURN_VALUE = -1
    			ELSE
    				  SET @RETURN_VALUE = @NIDCTA
    		END
    */    
    RETURN @RETURN_VALUE

GO

