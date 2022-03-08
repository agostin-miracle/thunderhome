IF OBJECT_ID ( 'dbo.PRLGNUSUINS', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRLGNUSUINS;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 07/03/2022 14:23:45
 Objetivo : Inserção de Registros na Tabela TBLGNUSU
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRLGNUSUINS
        (        @CODUSU int, 
        @REGATV tinyint = 0, 
        @LGNUSU varchar(50), 
        @PSWUSU varchar(512), 
        @RSTPSW tinyint, 
        @NUMACE int, 
        @STAREC tinyint = 1, 
        @UPDUSU int = 0,
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0

    SET @RETURN_VALUE = 0      
    DECLARE @LGNTYP TINYINT=1;
    
    IF(@LGNUSU='')
       SET @RETURN_VALUE=-2;
    
    IF(@RETURN_VALUE=0)
      BEGIN
        IF(@CODUSU<=0)
           SET @RETURN_VALUE=-7;
      END
    IF(@RETURN_VALUE=0)
      BEGIN
        IF(NOT EXISTS(SELECT 1 FROM TBCADGER (NOLOCK) WHERE CODUSU=@CODUSU))
           SET @RETURN_VALUE=-7;
      END
    
    IF(@RETURN_VALUE=0)
      BEGIN
        IF(LEN(@LGNUSU)<3)
           SET @RETURN_VALUE=-3;
      END
    
    IF(@RETURN_VALUE=0)
      BEGIN
        IF(@PSWUSU='')
           SET @RETURN_VALUE=-5;
      END
    
    IF(@RETURN_VALUE=0)
       BEGIN
          IF(EXISTS( SELECT 1 FROM TBLGNUSU (NOLOCK) WHERE CODUSU<>@CODUSU AND LGNTYP=@LGNTYP AND LGNUSU=@LGNUSU ))
            SET @RETURN_VALUE=-4;
       END        
    
    IF(@RETURN_VALUE=0)
       BEGIN
          IF(EXISTS( SELECT 1 FROM TBLGNUSU (NOLOCK) WHERE CODUSU=@CODUSU AND LGNTYP=@LGNTYP AND PSWUSU=@PSWUSU ))
            SET @RETURN_VALUE=-6;
       END

    IF(@RETURN_VALUE=0)
        BEGIN
            INSERT INTO TBLGNUSU(CODUSU, REGATV, LGNUSU, PSWUSU, RSTPSW, NUMACE, STAREC, UPDUSU)
                        VALUES (@CODUSU, @REGATV, @LGNUSU, @PSWUSU, @RSTPSW, @NUMACE, @STAREC, @UPDUSU);
            IF @@ERROR = 0
                BEGIN
                    SET @RETURN_VALUE = @@IDENTITY
                END
            ELSE
                BEGIN
                    SET @RETURN_VALUE = -1
                END
    IF(@RETURN_VALUE>0 AND @REGATV=1 AND @STAREC=1)
              BEGIN
                UPDATE TBLGNUSU
                   SET REGATV = 0
                 WHERE CODUSU = @CODUSU
                   AND LGNTYP = @LGNTYP
                   AND LGNNUM <> @RETURN_VALUE
                   AND STAREC = 1
                   AND REGATV = 1
              END
        END
    RETURN @RETURN_VALUE

GO
IF OBJECT_ID ( 'dbo.PRLGNUSUSEL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRLGNUSUSEL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 07/03/2022 14:23:45
 Objetivo : Obtêm o registro de Login do Usuário
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRLGNUSUSEL
(
    @LGNNUM Integer
)
AS
    SET NOCOUNT ON
    SELECT * FROM TBLGNUSU (NOLOCK) A

    WHERE     (A.LGNNUM=@LGNNUM)

GO

IF OBJECT_ID ( 'dbo.PRLGNUSUGET', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRLGNUSUGET;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 07/03/2022 14:23:45
 Objetivo : Obtêm o registro de Login Ativo do Usuário
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRLGNUSUGET
(
    @CODUSU Integer
)
AS
    SET NOCOUNT ON
    SELECT * FROM TBLGNUSU (NOLOCK) A WHERE STAREC=1 AND REGATV=1

 AND     (A.CODUSU=@CODUSU)

GO

IF OBJECT_ID ( 'dbo.PRLGNUSUUPD', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRLGNUSUUPD;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 07/03/2022 14:23:45
 Objetivo : Altera um registro da tabela TBLGNUSU ()  de acordo com a chave identity
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRLGNUSUUPD
        (@LGNNUM int, 
        @CODUSU int, 
        @REGATV tinyint = 0, 
        @LGNUSU varchar(50), 
        @PSWUSU varchar(512), 
        @RSTPSW tinyint, 
        @NUMACE int, 
        @STAREC tinyint = 1, 
        @DATUPD datetime, 
        @UPDUSU int = 0,
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0
    SET @DATUPD=GETDATE();
    SET @RETURN_VALUE = 0;
    DECLARE @LGNTYP TINYINT=1;
    
    IF(@LGNUSU='')
       SET @RETURN_VALUE=-2;
       
    IF(@RETURN_VALUE=0)
      BEGIN
        IF(LEN(@LGNUSU)<3)
           SET @RETURN_VALUE=-3;
      END
    
    IF(@RETURN_VALUE=0)
      BEGIN
        IF(@PSWUSU='')
           SET @RETURN_VALUE=-5;
      END
    
    IF(@RETURN_VALUE=0)
       BEGIN
          IF(EXISTS( SELECT 1 FROM TBLGNUSU (NOLOCK) WHERE CODUSU<>@CODUSU AND LGNTYP=@LGNTYP AND LGNUSU=@LGNUSU ))
            SET @RETURN_VALUE=-4;
       END        
    
    IF(@RETURN_VALUE=0)
       BEGIN
          IF(EXISTS( SELECT 1 FROM TBLGNUSU (NOLOCK) WHERE CODUSU=@CODUSU AND LGNTYP=@LGNTYP AND PSWUSU=@PSWUSU ))
            SET @RETURN_VALUE=-6;
       END

    IF(@RETURN_VALUE=0)
        BEGIN
            UPDATE TBLGNUSU
               SET CODUSU=@CODUSU
                  ,REGATV=@REGATV
                  ,LGNUSU=@LGNUSU
                  ,PSWUSU=@PSWUSU
                  ,RSTPSW=@RSTPSW
                  ,NUMACE=@NUMACE
                  ,STAREC=@STAREC
                  ,DATUPD=@DATUPD
                  ,UPDUSU=@UPDUSU
             WHERE (LGNNUM=@LGNNUM)

            IF (@@ROWCOUNT = 0)
                SET @RETURN_VALUE = -2
            ELSE
                BEGIN
                    IF (@@ERROR = 0)
                        BEGIN
                            SET @RETURN_VALUE = @LGNNUM
                        END
                    ELSE
                        BEGIN
                            SET @RETURN_VALUE = -1
                        END
                END
        END
    IF(@RETURN_VALUE>0 AND @REGATV=1 AND @STAREC=1)
              BEGIN
                UPDATE TBLGNUSU
                   SET REGATV = 0
                 WHERE CODUSU = @CODUSU
                   AND LGNTYP = @LGNTYP
                   AND LGNNUM <> @RETURN_VALUE
                   AND STAREC = 1
                   AND REGATV = 1
              END
    RETURN @RETURN_VALUE
GO
IF OBJECT_ID ( 'dbo.PRLGNUSUACECTL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRLGNUSUACECTL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 07/03/2022 14:23:45
 Objetivo : Obtêm o registro de controle de acesso do usuário
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRLGNUSUACECTL
(
    @LGNNUM Integer
)
AS
    SET NOCOUNT ON

    SELECT ResetPassword = CAST (CASE WHEN RSTPSW=1 THEN 1 ELSE 0 END AS BIT)
              ,IsPointOfSale = CAST(0 AS BIT)
              ,IsTedApproval = CAST(0 AS BIT)
              ,LGNNUM
              ,IsAdministrator = CAST(CASE WHEN (B.TIPUSU = 1) THEN 1 ELSE 0 END AS BIT)
    		  ,IsSuperVisor = CAST(CASE WHEN (B.TIPUSU = 2) THEN 1 ELSE 0 END AS BIT)
              ,IsManagerProduct = CAST( CASE WHEN (B.TIPUSU = 3 OR ISNULL(C.CODUSU,0) = A.CODUSU) THEN 1 ELSE 0 END AS BIT)
       	      ,Mail = ISNULL(F.DSCEND, '')
              ,Mobile = ISNULL(G.NUMCEL, '')
          FROM TBLGNUSU (NOLOCK) A
          LEFT JOIN (SELECT CODUSU, TIPUSU FROM TBCADGER (NOLOCK) WHERE STAUSU=61) B ON (B.CODUSU = A.CODUSU)
          LEFT JOIN (SELECT DISTINCT CODUSU FROM TBUSUPRO (NOLOCK) WHERE STAREC=1) C ON (C.CODUSU = A.CODUSU)
          LEFT JOIN (SELECT TOP 1 CODUSU, DSCEND FROM TBCADEND (NOLOCK) WHERE TIPEND=3 AND STAREC=1 AND REGATV=1) F ON (F.CODUSU = A.CODUSU)
          LEFT JOIN (SELECT TOP 1 CODUSU, NUMCEL =NUMDDD + NUMTEL FROM TBCADCTO (NOLOCK) WHERE TIPCTO=3 AND STAREC=1 AND REGATV=1) G ON (G.CODUSU = A.CODUSU)
         WHERE (A.STAREC=1 AND A.REGATV=1)
		 AND (A.LGNNUM =@LGNNUM)

GO

IF OBJECT_ID ( 'dbo.PRLGNUSULOG', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRLGNUSULOG;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 07/03/2022 14:23:45
 Objetivo : Efetua um login com base nas credenciais de acesso
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRLGNUSULOG
(
    @LGNTYP Tinyint,
    @LGNUSU varchar(50),
    @PSWUSU varchar(512)
)
AS
    SET NOCOUNT ON

    DECLARE @RETURN_VALUE INT =0        
    DECLARE @LGNNUM INT=0
    DECLARE @CODUSU INT=0
    DECLARE @AUDCHG VARCHAR(100)
    /* login name */
    IF(@LGNTYP=1)
       BEGIN
           SELECT @LGNNUM = LGNNUM, @CODUSU=CODUSU FROM TBLGNUSU (NOLOCK) WHERE LGNUSU = @LGNUSU AND PSWUSU = @PSWUSU AND STAREC = 1 AND REGATV = 1
       END
    
    /*email*/
    IF(@LGNTYP=3)
       BEGIN
    		    SELECT @LGNNUM = LGNNUM, @CODUSU=L.CODUSU FROM TBLGNUSU (NOLOCK) L 
             INNER JOIN TBCADEND (NOLOCK) E ON (E.CODUSU = L.CODUSU)
            WHERE (E.DSCEND = @LGNUSU AND  E.TIPEND = 3 AND E.REGATV=1 AND E.STAREC=1)
              AND (L.PSWUSU = @PSWUSU AND L.STAREC = 1 AND L.REGATV = 1)
       END 
    /* cmf */
    IF(@LGNTYP=2)
       BEGIN
    		    SELECT @LGNNUM = LGNNUM,@CODUSU=L.CODUSU
             FROM TBLGNUSU (NOLOCK) L
    	      INNER JOIN TBCADGER (NOLOCK) A ON (A.CODUSU = L.CODUSU)
            WHERE (A.CODCMF = @LGNUSU AND  A.STAUSU =61 AND A.STAREC=1 AND L.PSWUSU = @PSWUSU)
              AND (L.STAREC = 1 AND L.REGATV = 1)
       END 
    
    /* cartão */
    IF(@LGNTYP=4)
       BEGIN
    	    SELECT @LGNNUM = LGNNUM, @CODUSU=L.CODUSU
            FROM TBLGNUSU (NOLOCK) L
           INNER JOIN TBREGCRT (NOLOCK) C1 ON (C1.ASSUSU = L.CODUSU)
           INNER JOIN TBCADCRT (NOLOCK) C2 ON (C2.CODCRT = C1.CODCRT)
           WHERE (     C1.NUMCRT = @LGNUSU AND  C1.STACRT=109 AND C1.STAREC = 1 
    	            AND C2.PSWCRT = @PSWUSU)
             AND (L.STAREC = 1 AND L.REGATV = 1)
       END
    
    SET @LGNNUM = ISNULL(@LGNNUM,0) ;
    IF(@LGNNUM>0)
      BEGIN
          UPDATE TBLGNUSU SET RSTPSW = CASE WHEN (RSTPSW=1) THEN 2 ELSE RSTPSW END, ULTACE = GETDATE(), NUMACE = NUMACE + 1 WHERE LGNNUM=@LGNNUM 
          SET @RETURN_VALUE=@LGNNUM
      END
    ELSE
        SET @RETURN_VALUE=-2;
        
        
    IF(@RETURN_VALUE>0)
        BEGIN
            SET @AUDCHG = N'O usuário efetuow o login pelo ' + CASE   
                     WHEN (@LGNTYP) = 1 THEN 'LOGIN NAME'   
                     WHEN (@LGNTYP) = 2 THEN 'CMF '   
                     WHEN (@LGNTYP) = 3 THEN 'EMAIL '  
                     WHEN (@LGNTYP) = 4 THEN 'CARTAO ' END  + @LGNUSU                    
      INSERT INTO TBAUDDAT ( UPDUSU,    AUDDAT, AUDKEY,  AUDIDR,   AUDIMS,                AUDOBJ, AUDSRC, AUDCHG) 
                    VALUES (@CODUSU, GETDATE(),      2, @CODUSU,        5, OBJECT_NAME(@@PROCID),     '', @AUDCHG)        
        END
        
       
    RETURN @RETURN_VALUE;

GO

IF OBJECT_ID ( 'dbo.PRLGNUSUREF', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRLGNUSUREF;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 07/03/2022 14:23:45
 Objetivo : Verifica se o usuario precisa fazer um refresh de senha
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRLGNUSUREF
(
    @LGNNUM Integer
)
AS
    SET NOCOUNT ON

    DECLARE @RETURN_VALUE INT =0        
    BEGIN
        IF(EXISTS(SELECT 1 FROM TBLGNUSU (NOLOCK)  WHERE LGNNUM=@LGNNUM AND RSTPSW IN (1,2) AND STAREC = 1 AND REGATV = 1))
             SET @RETURN_VALUE=1;
        ELSE
             SET @RETURN_VALUE=-1
    END
    RETURN @RETURN_VALUE;

GO

IF OBJECT_ID ( 'dbo.PRLGNUSUCHGPSW', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRLGNUSUCHGPSW;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 07/03/2022 14:23:45
 Objetivo : Efetua um login com base nas credenciais de acesso
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRLGNUSUCHGPSW
(
    @LGNUSU varchar(50),
    @PSWOLD varchar(255),
    @PSWUSU varchar(255)
)
AS
    SET NOCOUNT ON

    DECLARE @RETURN_VALUE INT =0        
    DECLARE @LGNNUM INT=0
    DECLARE @CODUSU INT=0
    DECLARE @DATUPD DATETIME = GETDATE();
    
    /* VERIFICA A SENHA ANTERIOR */
    BEGIN
      SELECT @LGNNUM = ISNULL(LGNNUM,0)
            ,@CODUSU = ISNULL(CODUSU,0)
        FROM TBLGNUSU (NOLOCK) 
    	 WHERE LGNUSU = @LGNUSU
    	   AND PSWUSU = @PSWOLD
    	   AND STAREC = 1
         AND REGATV = 1
    END
    
    IF(@LGNNUM=0)
       BEGIN
           SET @RETURN_VALUE=-7;
       END
    IF(@RETURN_VALUE=0)
       BEGIN
          IF(EXISTS (SELECT 1 
                       FROM TBLGNUSU (NOLOCK) 
    	                WHERE LGNUSU = @LGNUSU
    	                  AND PSWUSU = @PSWUSU
                        AND CODUSU = @CODUSU
    	                  AND STAREC = 1
                        AND REGATV = 1))
              SET @RETURN_VALUE=-6
       END          
     
      EXEC dbo.PRLGNUSUUPD @LGNNUM, @CODUSU, 1, @LGNUSU, @PSWUSU, 0,1,@DATUPD,@CODUSU, @RETURN_VALUE OUTPUT;  
      
    RETURN @RETURN_VALUE;

GO

IF OBJECT_ID ( 'dbo.PRLGNUSURSTPSW', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRLGNUSURSTPSW;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 07/03/2022 14:23:45
 Objetivo : Efetua um login com base nas credenciais de acesso
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRLGNUSURSTPSW
(
    @LGNNUM Integer,
    @PSWUSU varchar(512),
    @UPDUSU Integer
)
AS
    SET NOCOUNT ON

    DECLARE @RETURN_VALUE INT =0        
    DECLARE @CODUSU INT=0
    
    IF(@LGNNUM<=0)
       SET @RETURN_VALUE=-1
    
    IF(@RETURN_VALUE=0)
      BEGIN
        IF(@PSWUSU='')
            SET @RETURN_VALUE=-2
      END
    
    IF(@RETURN_VALUE=0)
      BEGIN
        IF(LEN(@PSWUSU)<3)
            SET @RETURN_VALUE=-3
      END
      
    IF(@RETURN_VALUE=0)
      BEGIN
        IF(@UPDUSU<=0)
            SET @RETURN_VALUE=-4
      END
      
    IF(@RETURN_VALUE=0)
      BEGIN
        IF(NOT EXISTS(SELECT 1 FROM TBLGNUSU (NOLOCK) WHERE LGNNUM=@LGNNUM AND STAREC=1 AND REGATV=1))
            BEGIN
                SET @RETURN_VALUE = -5
            END
      END
    
    
    IF(@RETURN_VALUE=0)
      BEGIN
         UPDATE TBLGNUSU SET PSWUSU=@PSWUSU, RSTPSW=0, UPDUSU=@UPDUSU, DATUPD=GETDATE() WHERE LGNNUM=@LGNNUM
         IF(@@ERROR<>0)
            SET @RETURN_VALUE=-6
         ELSE
            SET @RETURN_VALUE=@LGNNUM
      END        
     
    RETURN @RETURN_VALUE;

GO

