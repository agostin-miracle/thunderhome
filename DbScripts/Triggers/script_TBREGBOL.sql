IF OBJECT_ID ( 'dbo.TR_TBREGBOL_TBREGOPE', 'TR' ) IS NOT NULL
    DROP TRIGGER dbo.TR_TBREGBOL_TBREGOPE;
GO
/* ===================================================================================================
   Author : Agostin
     Date : 10/04/2022 09:40:39
 Objetivo : Efetua a atualização da tabela de registro de operacoes para o subsistema 2 - boletos
 ==================================================================================================== */
CREATE TRIGGER dbo.TR_TBREGBOL_TBREGOPE
ON dbo.TBREGBOL
 AFTER INSERT, UPDATE
AS
    BEGIN
           DECLARE @SIGOPE SMALLINT =1
           DECLARE @SUBSYS TINYINT, @RSPTAR TINYINT, @STAREC TINYINT, @UPDATED TINYINT
           DECLARE @NIDREF INT, @UPDUSU INT
    	   DECLARE @VLRBOL FLOAT, @VLRTAR FLOAT, @VLRTDP FLOAT
    
    	   SELECT @RSPTAR = RSPTAR,
    	          @SUBSYS = SUBSYS,
    			  @NIDREF = NIDBOL,
    			  @STAREC = STAREC,
    			  @VLRBOL = VLRBOL,
    			  @VLRTAR = VLRTAR,
    			  @UPDUSU = UPDUSU,
    			  @VLRTDP = VLRTDP
    	    FROM inserted
    
    	   SET @UPDATED=0;
           IF( EXISTS (SELECT 1 FROM TBREGOPE (NOLOCK)
                                WHERE SUBSYS=@SUBSYS
                                  AND NIDREF= @NIDREF
                                  AND CODMOV = 501))
              BEGIN
                UPDATE TBREGOPE
                   SET VLROPE = @VLRBOL,
                       STAREC = @STAREC,
                       UPDUSU = @UPDUSU,
    				   DATUPD = GETDATE()
                WHERE SUBSYS =@SUBSYS
    			  AND NIDREF = @NIDREF
    			  AND CODMOV = 501
    			  AND STAREC <> @STAREC
    			  AND VLROPE <> @VLRBOL
              END
           ELSE
                BEGIN
                    INSERT INTO TBREGOPE(SUBSYS,NIDREF,DATOPE,CODMOV,VLRBAS,VLRPCT,SIGOPE,VLRPCP,VLROPE,STAREC,UPDUSU)
                    SELECT inserted.SUBSYS,inserted.NIDBOL,inserted.DATCAD,501,inserted.VLRBOL,100,1,1,inserted.VLRBOL,inserted.STAREC,inserted.UPDUSU FROM inserted WHERE inserted.VLRBOL>0
                END
    
    	   IF(@VLRTAR>0)
    			BEGIN
    			   IF(@RSPTAR=1)
    				  SET @SIGOPE=0;
    
    			   IF( EXISTS (SELECT 1 FROM TBREGOPE (NOLOCK)
    									WHERE SUBSYS = @SUBSYS
    									  AND NIDREF = @NIDREF
    									  AND CODMOV = 510))
    				  BEGIN
    					UPDATE TBREGOPE
    					   SET
    						   VLROPE = @VLRTAR,
    						   STAREC = @STAREC,
    						   UPDUSU = @UPDUSU,
    		   				   DATUPD = GETDATE()
    					WHERE SUBSYS =@SUBSYS
    					  AND NIDREF = @NIDREF
    					  AND CODMOV = 510
    					  AND STAREC <> @STAREC
    					  AND VLROPE <> @VLRTAR
    
    				  END
    			   ELSE
    					BEGIN
    					   INSERT INTO TBREGOPE(SUBSYS,NIDREF,DATOPE,CODMOV,VLRBAS,VLRPCT,SIGOPE,VLRPCP,VLROPE,STAREC,UPDUSU)
    						SELECT inserted.SUBSYS,inserted.NIDBOL,inserted.DATCAD,510,inserted.VLRBOL,0,@SIGOPE,1,inserted.VLRTAR,inserted.STAREC,inserted.UPDUSU FROM inserted WHERE inserted.VLRTAR>0
    					END
    			END
    
    	IF(@VLRTDP>0)
    		BEGIN
    			IF( EXISTS (SELECT 1 FROM TBREGOPE (NOLOCK)
    	 					 WHERE SUBSYS = @SUBSYS
    						   AND NIDREF = @NIDREF
    						   AND CODMOV = 530))
    				  BEGIN
    					UPDATE TBREGOPE
    					   SET VLROPE = @VLRTAR,
    						   STAREC = @STAREC,
    						   UPDUSU = @UPDUSU,
    		   				   DATUPD = GETDATE()
    					WHERE SUBSYS =@SUBSYS
    					  AND NIDREF = @NIDREF
    					  AND CODMOV = 530
    					  AND STAREC <> @STAREC
    					  AND VLROPE <> @VLRTDP
    				  END
    		   ELSE
    				BEGIN
    					INSERT INTO TBREGOPE(SUBSYS,NIDREF,DATOPE,CODMOV,VLRBAS,VLRPCT,SIGOPE,VLRPCP,VLROPE,STAREC,UPDUSU)
    					SELECT inserted.SUBSYS,inserted.NIDBOL,inserted.DATCAD,530,0,0,1,1,inserted.VLRTDP,inserted.STAREC,inserted.UPDUSU FROM inserted WHERE inserted.VLRTDP>0
    				END
    		END
     END


GO

