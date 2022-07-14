

-- ==========================================================================================
-- Entity Name:	tblFiles_Update
-- Create date:	10/16/2015 3:40:12 PM
-- Description:	This stored procedure is intended for updating tblFiles table
-- ==========================================================================================
CREATE Procedure [dbo].[tblFiles_Update]
	@id int,
	@Name varchar(50),
	@ContentType varchar(50),
	@Data varbinary(max)
As
Begin
	Update tblFiles
	Set
		[Name] = @Name,
		[ContentType] = @ContentType,
		[Data] = @Data
	Where		
		[id] = @id
	Select 
		[id],
		[Name],
		[ContentType],
		[Data]
	From tblFiles
	Where
		[id] = @id
End

