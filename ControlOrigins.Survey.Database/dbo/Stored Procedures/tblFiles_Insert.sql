

-- ==========================================================================================
-- Entity Name:	tblFiles_Insert
-- Create date:	10/16/2015 3:40:12 PM
-- Description:	This stored procedure is intended for inserting values to tblFiles table
-- ==========================================================================================
CREATE Procedure [dbo].[tblFiles_Insert]
	@Name varchar(50),
	@ContentType varchar(50),
	@Data varbinary(max)
As
Begin
	Insert Into tblFiles
		([Name],[ContentType],[Data])
	Values
		(@Name,@ContentType,@Data)

	Declare @id int
	Select @id = @@IDENTITY
	Select 
		[id],
		[Name],
		[ContentType],
		[Data]
	From tblFiles
	Where
		[id] = @id
End

