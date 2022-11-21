USE DoubleVPartners
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author, Diego Rincon>
-- Create date: <Create Date,, 20/11/2022>
-- Description:	<Description,- Procedimiento almacenado para consultar las personas creadas.>
-- =============================================
--exec ConsultPeople
CREATE OR ALTER PROCEDURE ConsultPeople
	
AS
BEGIN
	select * from DoubleVPartners.dbo.Person
END
GO


