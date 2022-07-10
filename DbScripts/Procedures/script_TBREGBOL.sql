IF OBJECT_ID ( 'dbo.PRREGBOLINS', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRREGBOLINS;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 10/04/2022 09:40:38
 Objetivo : Inserção de Registros na Tabela TBREGBOL
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRREGBOLINS
        (        @MODCAL tinyint, 
        @NIDCBL int = 0, 
        @USUPRO int, 
        @USUCED int, 
        @USUSAC int, 
        @CODAVA int = 0, 
        @CODEND int = 0, 
        @CODMOE int = 1, 
        @STABOL smallint, 
        @TIPBOL tinyint = 1, 
        @TIPBXA tinyint = 0, 
        @TIPENT tinyint = 1, 
        @DATEMI datetime, 
        @DATVCT date, 
        @DATPGT date = null, 
        @DATVAL date, 
        @DATREG datetime, 
        @NUMPCL smallint = 1, 
        @CODCED varchar(25) = null, 
        @NIDLIM int = 0, 
        @NUMIPA varchar(50) = '', 
        @ORGBOL int = 1, 
        @INSBC1 varchar(30) = '', 
        @INSBC2 varchar(30) = '', 
        @INSBC3 varchar(30) = '', 
        @DSCBOL varchar(255) = null, 
        @URLRET varchar(255) = null, 
        @CODRET int = 0, 
        @FILNAM varchar(100) = null, 
        @NIDCTA int = 0, 
        @NUMDVF smallint = 10, 
        @PEDCLI varchar(15) = null, 
        @LINDIG varchar(64) = null, 
        @CODESP varchar(3) = '', 
        @NUMCTR varchar(3) = '', 
        @NOSNUM int = 0, 
        @IMGSAV bit = 0, 
        @INDCNC bit = 0, 
        @APLCAL tinyint = 0, 
        @REGPRO int = 0, 
        @RSPTAR tinyint = 1, 
        @VLRBOL money = 0, 
        @APLTAR tinyint = 0, 
        @VLRTAR money = 0, 
        @APLTDP tinyint = 0, 
        @VLRTDP money = 0, 
        @STAREC int = 1, 
        @UPDUSU int = 0,
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0

    DECLARE @NIDCAL INT
    DECLARE @PRETAR MONEY
    DECLARE @PRETDP MONEY
    IF(@CODAVA=0)
       SET @CODAVA=@USUCED
    
    IF(@CODAVA>0 AND @CODEND=0)
      BEGIN
          IF(EXISTS(SELECT 1 FROM TBCADEND (NOLOCK) WHERE CODUSU = @CODAVA AND STAREC=1 AND REGATV=1))
            SELECT @CODEND = ISNULL(CODEND,0) FROM TBCADEND (NOLOCK) WHERE CODUSU = @CODAVA AND STAREC=1 AND REGATV=1
      END
    
    /* CALCULO DE TARIFAS POR PROCESSO DE INTEGRACAO (API, MASSIVO) */
    IF(@MODCAL = 1)
       BEGIN
            /* LOCALIZA CONFIGURACAO */ 
            EXEC @NIDCBL = PRCFGBOLOCREC @USUPRO, @USUCED, @TIPBOL        
            IF(@NIDCBL>0)
               BEGIN
       			      DECLARE @CODTAR INT, @TARTDP INT
                  SELECT @CODTAR = CODTAR,@TARTDP=TARTDP, @APLTAR=APLTAR, @APLTDP=@APLTDP FROM TBCFGBOL WHERE NIDCBL=@NIDCBL
                  /* VALOR DA TARIFA */
                  IF(@CODTAR > 0 AND @APLTAR=1)
                    BEGIN
                      EXEC @NIDCAL = PRCADTARCALV2 @USUPRO, @USUCED, @CODTAR,@VLRBOL, @UPDUSU,1,1, NULL,NULL,1,0,0,0,0
                      IF(@NIDCAL>0)
                        SELECT @PRETAR = EXTVLR FROM TBCALTAR WHERE NIDCAL = @NIDCAL
                    END
                  /* VALOR DO TDP */                
                  IF(@TARTDP>0 AND @APLTDP=1)
                    BEGIN
                      EXEC @NIDCAL = PRCADTARCALV2 @USUPRO, @USUCED, @TARTDP,@VLRBOL, @UPDUSU,1,1, NULL,NULL,1,0,0,0,0
                      IF(@NIDCAL>0)
                         SELECT @PRETDP = EXTVLR FROM TBCALTAR WHERE NIDCAL = @NIDCAL
                    END
              END              
       END
       
    IF(@MODCAL=1 AND @RSPTAR=2) -- SACADO
       BEGIN
           SET @VLRTAR = @PRETAR
           SET @VLRTDP = @PRETDP       
       END

    IF(@RETURN_VALUE=0)
        BEGIN
            INSERT INTO TBREGBOL(MODCAL, NIDCBL, USUPRO, USUCED, USUSAC, CODAVA, CODEND, CODMOE, STABOL, TIPBOL, TIPBXA, TIPENT, DATEMI, DATVCT, DATPGT, DATVAL, DATREG, NUMPCL, CODCED, NIDLIM, NUMIPA, ORGBOL, INSBC1, INSBC2, INSBC3, DSCBOL, URLRET, CODRET, FILNAM, NIDCTA, NUMDVF, PEDCLI, LINDIG, CODESP, NUMCTR, NOSNUM, IMGSAV, INDCNC, APLCAL, REGPRO, RSPTAR, VLRBOL, APLTAR, VLRTAR, APLTDP, VLRTDP, STAREC, UPDUSU)
                        VALUES (@MODCAL, @NIDCBL, @USUPRO, @USUCED, @USUSAC, @CODAVA, @CODEND, @CODMOE, @STABOL, @TIPBOL, @TIPBXA, @TIPENT, @DATEMI, @DATVCT, @DATPGT, @DATVAL, @DATREG, @NUMPCL, @CODCED, @NIDLIM, @NUMIPA, @ORGBOL, @INSBC1, @INSBC2, @INSBC3, @DSCBOL, @URLRET, @CODRET, @FILNAM, @NIDCTA, @NUMDVF, @PEDCLI, @LINDIG, @CODESP, @NUMCTR, @NOSNUM, @IMGSAV, @INDCNC, @APLCAL, @REGPRO, @RSPTAR, @VLRBOL, @APLTAR, @VLRTAR, @APLTDP, @VLRTDP, @STAREC, @UPDUSU);
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
    IF(@NOSNUM=0)
       UPDATE TBREGBOL SET NOSNUM = @RETURN_VALUE WHERE NIDBOL=@RETURN_VALUE;
  END
IF(@MODCAL=1 AND @RSPTAR=2 AND (@RETURN_VALUE > 0)) 
   BEGIN
			IF( NOT EXISTS (SELECT 1 
				             FROM TBREGOPE (NOLOCK)
                            WHERE SUBSYS=2
                              AND NIDREF= @RETURN_VALUE
                              AND CODMOV = 510) AND @PRETAR > 0 )
				BEGIN
					INSERT INTO TBREGOPE (SUBSYS, NIDREF, DATOPE,CODMOV,  VLRBAS,VLRPCT,SIGOPE,VLRPCP,  VLROPE, STAREC, UPDUSU,USELCT)
							VALUES (     2,@RETURN_VALUE,GETDATE(),   510, @VLRBOL,     0,    -1,     0, @PRETAR,      2,@UPDUSU,-1)
				END

			IF( NOT EXISTS (SELECT 1 
				             FROM TBREGOPE (NOLOCK)
                            WHERE SUBSYS=2
                              AND NIDREF= @RETURN_VALUE
                              AND CODMOV = 530) AND @PRETDP > 0 )
				BEGIN
					INSERT INTO TBREGOPE (SUBSYS, NIDREF, DATOPE,CODMOV,  VLRBAS,VLRPCT,SIGOPE,VLRPCP,  VLROPE, STAREC, UPDUSU,USELCT)
							VALUES (     2,@RETURN_VALUE,GETDATE(),   530, @VLRBOL,     0,    -1,     0, @PRETDP,      2,@UPDUSU,-1)
				END

   END
        END
    RETURN @RETURN_VALUE

GO
IF OBJECT_ID ( 'dbo.PRREGBOLSEL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRREGBOLSEL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 10/04/2022 09:40:39
 Objetivo : Seleciona o registro de Boleto de acordo com o ID
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRREGBOLSEL
(
    @NIDBOL Integer
)
AS
    SET NOCOUNT ON
    SELECT A.*,  VLRLIQ= ISNULL(B.VLRLIQ,0)
    	  FROM TBREGBOL (NOLOCK) A INNER JOIN (SELECT NIDREF,
    	                                              VLRLIQ = SUM(VLROPE*SIGOPE)
    										     FROM TBREGOPE (NOLOCK)
    											 WHERE SUBSYS =2 AND STAREC IN (1,2)
    											 GROUP BY NIDREF) B ON (B.NIDREF= A.NIDBOL)
         WHERE (1=1)

 AND     (A.NIDBOL=@NIDBOL)

GO

IF OBJECT_ID ( 'dbo.PRCFGBOLUPD', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRCFGBOLUPD;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 10/04/2022 09:40:39
 Objetivo : Altera um registro da tabela TBREGBOL ()  de acordo com a chave identity
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRCFGBOLUPD
        (@NIDBOL int, 
        @MODCAL tinyint, 
        @NIDCBL int = 0, 
        @USUPRO int, 
        @USUCED int, 
        @USUSAC int, 
        @CODAVA int = 0, 
        @CODEND int = 0, 
        @CODMOE int = 1, 
        @STABOL smallint, 
        @TIPBOL tinyint = 1, 
        @TIPBXA tinyint = 0, 
        @TIPENT tinyint = 1, 
        @DATEMI datetime, 
        @DATVCT date, 
        @DATPGT date = null, 
        @DATVAL date, 
        @DATREG datetime, 
        @NUMPCL smallint = 1, 
        @CODCED varchar(25) = null, 
        @NIDLIM int = 0, 
        @NUMIPA varchar(50) = '', 
        @ORGBOL int = 1, 
        @INSBC1 varchar(30) = '', 
        @INSBC2 varchar(30) = '', 
        @INSBC3 varchar(30) = '', 
        @DSCBOL varchar(255) = null, 
        @URLRET varchar(255) = null, 
        @CODRET int = 0, 
        @FILNAM varchar(100) = null, 
        @NIDCTA int = 0, 
        @NUMDVF smallint = 10, 
        @PEDCLI varchar(15) = null, 
        @LINDIG varchar(64) = null, 
        @CODESP varchar(3) = '', 
        @NUMCTR varchar(3) = '', 
        @NOSNUM int = 0, 
        @IMGSAV bit = 0, 
        @INDCNC bit = 0, 
        @APLCAL tinyint = 0, 
        @REGPRO int = 0, 
        @RSPTAR tinyint = 1, 
        @VLRBOL money = 0, 
        @APLTAR tinyint = 0, 
        @VLRTAR money = 0, 
        @APLTDP tinyint = 0, 
        @VLRTDP money = 0, 
        @STAREC int = 1, 
        @DATUPD datetime, 
        @UPDUSU int = 0,
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0
    SET @DATUPD=GETDATE()
    IF(@CODAVA=0)
       SET @CODAVA=@USUCED
    
    IF(@CODAVA>0 AND @CODEND=0)
       BEGIN
          IF(EXISTS(SELECT 1 FROM TBCADEND (NOLOCK) WHERE CODUSU = @CODAVA AND STAREC=1 AND REGATV=1))
              SELECT @CODEND = ISNULL(CODEND,0) FROM TBCADEND (NOLOCK) WHERE CODUSU = @CODAVA AND STAREC=1 AND REGATV=1
       END

    IF(@RETURN_VALUE=0)
        BEGIN
            UPDATE TBREGBOL
               SET MODCAL=@MODCAL
                  ,NIDCBL=@NIDCBL
                  ,USUPRO=@USUPRO
                  ,USUCED=@USUCED
                  ,USUSAC=@USUSAC
                  ,CODAVA=@CODAVA
                  ,CODEND=@CODEND
                  ,CODMOE=@CODMOE
                  ,STABOL=@STABOL
                  ,TIPBOL=@TIPBOL
                  ,TIPBXA=@TIPBXA
                  ,TIPENT=@TIPENT
                  ,DATEMI=@DATEMI
                  ,DATVCT=@DATVCT
                  ,DATPGT=@DATPGT
                  ,DATVAL=@DATVAL
                  ,DATREG=@DATREG
                  ,NUMPCL=@NUMPCL
                  ,CODCED=@CODCED
                  ,NIDLIM=@NIDLIM
                  ,NUMIPA=@NUMIPA
                  ,ORGBOL=@ORGBOL
                  ,INSBC1=@INSBC1
                  ,INSBC2=@INSBC2
                  ,INSBC3=@INSBC3
                  ,DSCBOL=@DSCBOL
                  ,URLRET=@URLRET
                  ,CODRET=@CODRET
                  ,FILNAM=@FILNAM
                  ,NIDCTA=@NIDCTA
                  ,NUMDVF=@NUMDVF
                  ,PEDCLI=@PEDCLI
                  ,LINDIG=@LINDIG
                  ,CODESP=@CODESP
                  ,NUMCTR=@NUMCTR
                  ,NOSNUM=@NOSNUM
                  ,IMGSAV=@IMGSAV
                  ,INDCNC=@INDCNC
                  ,APLCAL=@APLCAL
                  ,REGPRO=@REGPRO
                  ,RSPTAR=@RSPTAR
                  ,VLRBOL=@VLRBOL
                  ,APLTAR=@APLTAR
                  ,VLRTAR=@VLRTAR
                  ,APLTDP=@APLTDP
                  ,VLRTDP=@VLRTDP
                  ,STAREC=@STAREC
                  ,DATUPD=@DATUPD
                  ,UPDUSU=@UPDUSU
             WHERE (NIDBOL=@NIDBOL)

            IF (@@ROWCOUNT = 0)
                SET @RETURN_VALUE = -2
            ELSE
                BEGIN
                    IF (@@ERROR = 0)
                        BEGIN
                            SET @RETURN_VALUE = @NIDBOL
                        END
                    ELSE
                        BEGIN
                            SET @RETURN_VALUE = -1
                        END
                END
        END
    RETURN @RETURN_VALUE
GO
IF OBJECT_ID ( 'dbo.PRREGBOLCLOTKT', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRREGBOLCLOTKT;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 10/04/2022 09:40:39
 Objetivo : Encerra um boleto
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRREGBOLCLOTKT
(
    @NIDBOL Integer,
    @STAREC Tinyint,
    @TIPBXA smallint,
    @STABOL smallint,
    @DATPGT DateTime,
    @UPDUSU Integer
)
AS
    SET NOCOUNT ON

    DECLARE @RETURN_VALUE INT=0          
    IF(NOT EXISTS(SELECT 1 FROM TBREGBOL (NOLOCK) WHERE NIDBOL=@NIDBOL))
       SET @RETURN_VALUE=-5;
    IF(@TIPBXA<=0 AND @RETURN_VALUE=0)
       SET @RETURN_VALUE = -1
    IF(@STABOL<=0 AND @RETURN_VALUE=0)
       SET @RETURN_VALUE = -2
    IF(@UPDUSU<=0 AND @RETURN_VALUE=0)
       SET @RETURN_VALUE = -3
    IF(@DATPGT='' OR @DATPGT IS NULL AND (@RETURN_VALUE=0))
       SET @RETURN_VALUE = -4
      
    IF(@RETURN_VALUE=0)   
      BEGIN          
          UPDATE TBREGBOL 
             SET TIPBXA=@TIPBXA, 
                 STABOL=@STABOL, 
                 DATPGT=@DATPGT, 
                 UPDUSU=@UPDUSU, 
                 DATUPD=GETDATE(),
                 STAREC=@STAREC 
           WHERE NIDBOL =@NIDBOL          
          IF(@@ERROR=0)
              SET @RETURN_VALUE=@NIDBOL
      END
    RETURN @RETURN_VALUE;

GO

