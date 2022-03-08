IF OBJECT_ID ( 'dbo.PRAUDDATINS', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRAUDDATINS;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 11/02/2022 16:58:59
 Objetivo : Inserção de Registros na Tabela TBAUDDAT
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRAUDDATINS
        (        @UPDUSU int = 0, 
        @AUDDAT datetime, 
        @AUDKEY int = 0, 
        @AUDIDR int = 0, 
        @AUDIMS int = 0, 
        @AUDTSK varchar(100) = null, 
        @AUDOBJ varchar(100) = null, 
        @AUDSRC varchar(4000) = null, 
        @AUDCHG varchar(4000) = null, 
        @NIDTOK int = 0, 
        @NUMIPA varchar(50) = '',
        @RETURN_VALUE int OUTPUT)
    AS
    SET NOCOUNT ON
    SET @RETURN_VALUE = 0

    IF(@RETURN_VALUE=0)
        BEGIN
            INSERT INTO TBAUDDAT(UPDUSU, AUDDAT, AUDKEY, AUDIDR, AUDIMS, AUDTSK, AUDOBJ, AUDSRC, AUDCHG, NIDTOK, NUMIPA)
                        VALUES (@UPDUSU, @AUDDAT, @AUDKEY, @AUDIDR, @AUDIMS, @AUDTSK, @AUDOBJ, @AUDSRC, @AUDCHG, @NIDTOK, @NUMIPA);
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
