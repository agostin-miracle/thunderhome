
IF OBJECT_ID ( 'dbo.FormatCard', 'FN' ) IS NOT NULL
    DROP FUNCTION dbo.FormatCard;
GO
/* Formato um número de cartão */
CREATE FUNCTION [dbo].[FormatCard] (@NUMCRT VARCHAR(20)=NULL)
RETURNS VARCHAR(100)
AS
	BEGIN
		DECLARE @fmtd VARCHAR(20)
		SET @fmtd=N''
				
		IF(LEN(@NUMCRT)>0)
			BEGIN
				DECLARE @NCRT VARCHAR(20)
				SELECT @NCRT = SUBSTRING(ISNULL(@NUMCRT,' ')+ SPACE(16),1,16) 
				SET @fmtd = SUBSTRING(@NCRT,1,4)+'-XXXX-XXXX-'+ SUBSTRING(@NCRT,13,4)
			END
		RETURN @fmtd
	END


GO


IF OBJECT_ID ( 'dbo.CreateAccountBase', 'FN' ) IS NOT NULL
    DROP FUNCTION dbo.CreateAccountBase;
GO

CREATE FUNCTION [dbo].[CreateAccountBase](@NEXTID INT)
RETURNS VARCHAR(11)
AS
BEGIN
	DECLARE @BASEACCOUNT VARCHAR(11)
	SET @BASEACCOUNT = RIGHT(RIGHT('000' + CAST(DATEPART(s,GETDATE()) AS VARCHAR),3),2) 
	+ RIGHT('000' + CAST(DATEPART(ms,GETDATE()) AS VARCHAR),3) + RIGHT('000000' + CAST(@NEXTID AS VARCHAR),6)
	RETURN @BASEACCOUNT
END

GO


IF OBJECT_ID ( 'dbo.FormatCMF', 'FN' ) IS NOT NULL
    DROP FUNCTION dbo.FormatCMF;
GO

CREATE FUNCTION [dbo].[FormatCMF] (@CODCMF VARCHAR(15)=NULL)
RETURNS VARCHAR(100)
AS
	BEGIN
		DECLARE @RST VARCHAR(20)=N''

		SET @CODCMF = LTRIM(RTRIM(@CODCMF))	
		IF(LEN(@CODCMF)=14)
			BEGIN
				SET @RST = SUBSTRING(@CODCMF,1,2)+ '.'
				SET @RST = @RST + SUBSTRING(@CODCMF,3,3)+ '.'
				SET @RST = @RST + SUBSTRING(@CODCMF,6,3)+ '/'
				SET @RST = @RST + SUBSTRING(@CODCMF,9,4)+ '-'
				SET @RST = @RST + SUBSTRING(@CODCMF,13,2)
			END						
		IF(LEN(@CODCMF)=15)
			BEGIN
				SET @RST = SUBSTRING(@CODCMF,1,3)+ '.'
				SET @RST = @RST + SUBSTRING(@CODCMF,4,3)+ '.'
				SET @RST = @RST + SUBSTRING(@CODCMF,7,3)+ '/'
				SET @RST = @RST + SUBSTRING(@CODCMF,10,4)+ '-'
				SET @RST = @RST + SUBSTRING(@CODCMF,14,2)
			END
		IF(LEN(@CODCMF)=11)
			BEGIN
				SET @RST = SUBSTRING(@CODCMF,1,3)+ '.'
				SET @RST = @RST + SUBSTRING(@CODCMF,4,3)+ '.'
				SET @RST = @RST + SUBSTRING(@CODCMF,7,3)+ '-'
				SET @RST = @RST + SUBSTRING(@CODCMF,10,2)
			END

		RETURN @RST
	END
GO



IF OBJECT_ID ( 'dbo.FormatCEP', 'FN' ) IS NOT NULL
    DROP FUNCTION dbo.FormatCEP;
GO

CREATE FUNCTION [dbo].[FormatCEP] (@CODCEP VARCHAR(8)=NULL)
RETURNS VARCHAR(20)
AS
	BEGIN
		DECLARE @RST VARCHAR(20)=N''
		SET @CODCEP = LTRIM(RTRIM(@CODCEP))	
		IF(LEN(@CODCEP)=8)
			BEGIN
			    IF(@CODCEP<> '00000000')
				   BEGIN
						SET @RST = SUBSTRING(@CODCEP,1,5)+ '-'
						SET @RST = @RST + SUBSTRING(@CODCEP,6,3)
				   END
			END						
		RETURN @RST
	END
GO




IF OBJECT_ID ( 'dbo.GetUserAccess', 'FN' ) IS NOT NULL
    DROP FUNCTION dbo.GetUserAccess;
GO


CREATE FUNCTION [dbo].[GetUserAcess](@SYSFUN BIGINT, @CODUSU INT )
RETURNS INT

/*
vvERIFICA O ACESSO DE UM USUARIO

*/
AS
BEGIN
     DECLARE @RETURN_VALUE INT =0;
 	 IF(EXISTS(SELECT 1 
            FROM RLSYSGRP (NOLOCK) A
			WHERE SYSFUN = @SYSFUN
			  AND STAREC = 1
			  AND SYSGRP IN (SELECT SYSGRP
			                   FROM TBUSUGRP
							  WHERE CODUSU=@CODUSU AND STAREC=1)))
			SET @RETURN_VALUE=1
	RETURN @RETURN_VALUE;
END
GO


IF OBJECT_ID ( 'dbo.LastDate', 'FN' ) IS NOT NULL
    DROP FUNCTION dbo.LastDate;
GO

CREATE FUNCTION [dbo].[LastDate](@DATVAL DATE)
RETURNS DATE
AS
BEGIN
	DECLARE @ANO INT
	DECLARE @MES INT
	DECLARE @SDATE VARCHAR(10)
	DECLARE @OPERACAO TINYINT
	DECLARE @DATERETURN DATETIME
	SET @DATERETURN = NULL
	IF(@DATVAL IS NOT NULL)
		BEGIN
			SET @MES = DATEPART(mm, @DATVAL)
			SET @ANO= DATEPART(yy, @DATVAL)
			SET @SDATE = CAST(@ANO AS VARCHAR(4))
			SET @OPERACAO=0
			IF(@MES=2)
				BEGIN
					IF ((@ANO % 4 = 0) and (@ANO % 100 != 0) or (@ANO % 400 = 0))
						SET @OPERACAO=1
					ELSE
						SET @OPERACAO=2
				END
			IF(@OPERACAO=0)
				BEGIN
					IF(@MES = 1 OR @MES = 3 OR @MES = 5 OR @MES=7 OR @MES=8 OR @MES=10 OR @MES=12)
						SET @OPERACAO=3
				END
			IF(@OPERACAO=0)
				SET @OPERACAO=4
			IF @OPERACAO=1
				SET @SDATE = @SDATE + '0229'
			IF @OPERACAO=2
				SET @SDATE = @SDATE + '0228'
			IF @OPERACAO=3
				SET @SDATE = @SDATE + RIGHT('00' +  CAST(@MES AS VARCHAR),2) + '31'
			IF @OPERACAO=4
				SET @SDATE = @SDATE + RIGHT('00' +  CAST(@MES AS VARCHAR),2) + '30'
			SET @DATERETURN = CAST(@SDATE AS DATE)
		END
	RETURN @DATERETURN
END

GO


IF OBJECT_ID ( 'dbo.ExpandPeriod', 'FN' ) IS NOT NULL
    DROP FUNCTION dbo.CreateAccountBase;
GO

CREATE FUNCTION  ExpandPeriod(@date1 DATETIME, @date2 DATETIME)
RETURNS @tb TABLE (id int IDENTITY,  VALDAT DATETIME)
/*
    Expande um intrvalo de dados em períodos mensais
*/
AS
	BEGIN 
       DECLARE @DAY INT =DAY(@DATE1);
	   DECLARE @START DATETIME =@date1;
	   DECLARE @C INT=1;
	   WHILE (@C =1)
	      BEGIN

		      INSERT @TB (VALDAT) VALUES (@START)
			  SET @start = DATEADD(day,1,dbo.LASTDATE(@START));
			  SET @START = DATEADD(DAY,@DAY-1,@START);
			  IF(@start>@date2)
			     SET @c=0;
		  END
    
	   RETURN
  end
GO

  

IF OBJECT_ID ( 'dbo.ValidadeCartao', 'FN' ) IS NOT NULL
    DROP FUNCTION dbo.ValidadeCartao;
GO

CREATE FUNCTION [dbo].[ValidadeCartao] (@VALCRT VARCHAR(5))
RETURNS VARCHAR(10) 
AS
	BEGIN
		DECLARE @MONTH TINYINT =0
		DECLARE @RETURN_VALUE VARCHAR(10)= N''
	    IF(@VALCRT IS NULL)
		   SET @VALCRT = '' 
	    IF(@VALCRT <>'')
			BEGIN 
				SET @MONTH = CAST(SUBSTRING(@VALCRT,1,2) AS TINYINT)
				DECLARE @YEAR SMALLINT=0;
				DECLARE @DAY TINYINT=0;
				DECLARE @ISLEAP TINYINT =0;
				SET @ISLEAP = case when
					(
						(@year % 4 = 0) and (@year % 100 != 0) or
						(@year % 400 = 0)
					) then 29 else 28 end

				SET @DAY = CASE   
					WHEN (@MONTH IN (1,3,5,7,8,10,12)) THEN 31   
					WHEN (@MONTH IN (4,6,9,11)) THEN 30
					ELSE @ISLEAP END   

               SET @RETURN_VALUE = CONVERT(VARCHAR(4),2000 + CAST(SUBSTRING(@VALCRT,4,2) AS SMALLINT)) + SUBSTRING(@VALCRT,1,2) + CONVERT(VARCHAR(2),@DAY)
			END

		RETURN @RETURN_VALUE

	END



GO


IF OBJECT_ID ( 'dbo.Split', 'FN' ) IS NOT NULL
    DROP FUNCTION dbo.Split;
GO


create function Split (@string varchar(max), @separador char(1)) 
returns table as return
    with a as (
        select
            id = 1,
            len_string = len(@string) + 1,
            ini = 1,
            fim = coalesce(nullif(charindex(@separador, @string, 1), 0), len(@string) + 1),
            elemento = ltrim(rtrim(substring(@string, 1, coalesce(nullif(charindex(@separador, @string, 1), 0), len(@string) + 1)-1)))
        union all
        select
            id + 1,
            len(@string) + 1,
            convert(int, fim) + 1,
            coalesce(nullif(charindex(@separador, @string, fim + 1), 0), len_string), 
            ltrim(rtrim(substring(@string, fim + 1, coalesce(nullif(charindex(@separador, @string, fim + 1), 0), len_string)-fim-1)))
        from a where fim < len_string)
    select id, elemento from a 

go

IF OBJECT_ID ( 'dbo.GetLoginName', 'FN' ) IS NOT NULL
    DROP FUNCTION dbo.GetLoginName;
GO

CREATE FUNCTION dbo.GetLoginName(@NOMUSU VARCHAR(100))
RETURNS VARCHAR(50)
AS
 BEGIN
	   DECLARE @TABELA TABLE (ID INT IDENTITY, NOME VARCHAR(50))
	   INSERT INTO @TABELA (NOME) SELECT ELEMENTO FROM dbo.Split(@NOMUSU, ' ') WHERE LEN(elemento)> 2

	   DECLARE @MAX_ITEM INT= 0
	   SELECT @MAX_ITEM = MAX(ID) FROM @TABELA
	   DECLARE @NOME VARCHAR(50)
	   DECLARE @RV VARCHAR(50)=''
	   DECLARE @CT INT=1
	   WHILE (@CT <= @MAX_ITEM)
	       BEGIN
		        IF(@CT=@MAX_ITEM)
					SELECT @NOME = UPPER(NOME) FROM @TABELA WHERE ID=@CT 		       
				ELSE
					SELECT @NOME = LEFT(UPPER(NOME),1) FROM @TABELA WHERE ID=@CT 		       
				SET @CT=@CT+1
				SELECT @RV = @RV + @NOME
		   END
	   RETURN @RV
 END

GO


CREATE FUNCTION [dbo].[GetNextDueDate](@DATVCT DATETIME)
RETURNS DATETIME
AS
BEGIN
	DECLARE @DOW int
	DECLARE @LOOP1 TINYINT = 1
	WHILE (@LOOP1=1)
		BEGIN
			IF(EXISTS(SELECT 1 FROM TBCADFER (NOLOCK) WHERE DATMOV = CONVERT(varchar,@DATVCT,112)))
				BEGIN
					SET @DATVCT = DATEADD(day,1,@DATVCT)
				END
			ELSE
				SET @LOOP1=0
		END

	SET @DOW = DATEPART(DW,@DATVCT)	
	IF(@DOW=7)
		SET @DATVCT = DATEADD(day,2,@DATVCT)
	IF(@DOW=1)
		SET @DATVCT = DATEADD(day,1,@DATVCT)
	SET @LOOP1 = 1
	WHILE (@LOOP1=1)
		BEGIN
			IF(EXISTS(SELECT 1 FROM TBCADFER (NOLOCK) WHERE DATMOV = CONVERT(varchar,@DATVCT,112)))
				BEGIN
					SET @DATVCT = DATEADD(day,1,@DATVCT)
				END
			ELSE
				SET @LOOP1=0
		END

	RETURN @DATVCT
END
