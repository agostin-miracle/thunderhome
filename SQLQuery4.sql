IF OBJECT_ID ( 'dbo.PRTIPEXPSELALL', 'P' ) IS NOT NULL
    DROP PROCEDURE dbo.PRTIPEXPSELALL;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 13/03/2022 18:41:14
 Objetivo : Obtêm uma Lista dos Tipos de Expansão
 ==================================================================================================== */
CREATE PROCEDURE dbo.PRTIPEXPSELALL
AS
    SET NOCOUNT ON
    SELECT A.*, DSCREC= ISNULL(B.DSCTAB,''), LGNUSU = ISNULL(C.LGNUSU,''), DSCREG = D.DSCTAB
    FROM TBTIPEXP (NOLOCK) A
    INNER JOIN TBTABGER (NOLOCK) B ON (B.NUMTAB =7 AND B.KEYCOD=A.STAREC)
     LEFT JOIN TBLGNUSU (NOLOCK) C ON (C.CODUSU = A.UPDUSU AND C.REGATV =1)
    INNER JOIN TBTABGER (NOLOCK) D ON (D.NUMTAB =94 AND D.KEYCOD=A.LVLREG)
     ORDER BY A.CODEXP



GO
