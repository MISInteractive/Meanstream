USE [Meanstream-Build]
GO
/****** Object:  Table [dbo].[meanstream_dynamics_ItemAttribute]    Script Date: 01/23/2012 20:51:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[meanstream_dynamics_ItemAttribute](
	[ItemId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_meanstream_dyanmics_ItemAttribute] PRIMARY KEY CLUSTERED 
(
	[ItemId] ASC,
	[Name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[meanstream_dynamics_Item]    Script Date: 01/23/2012 20:51:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[meanstream_dynamics_Item](
	[Id] [uniqueidentifier] NOT NULL,
	[Type] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_meanstream_dynamics_Item] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[meanstream_dynamics_UpdateItemType]    Script Date: 01/23/2012 20:51:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[meanstream_dynamics_UpdateItemType] 
	@OldType nvarchar(255),
	@NewType nvarchar(255)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    update meanstream_dynamics_Item set Type = @NewType where TYPE = @OldType
END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_dynamics_UpdateItemAttributeValue]    Script Date: 01/23/2012 20:51:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[meanstream_dynamics_UpdateItemAttributeValue]
	@Id uniqueidentifier,
	@Name nvarchar(255),
	@Value nvarchar(max)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    update meanstream_dynamics_ItemAttribute set Value = @Value where ItemId = @Id AND Name = @Name
END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_dynamics_UpdateItemAttributeName]    Script Date: 01/23/2012 20:51:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[meanstream_dynamics_UpdateItemAttributeName] 
	-- Add the parameters for the stored procedure here
	@Type as varchar(255),
	@OldName as varchar(255),
	@NewName as varchar(255)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Declare @count Int
	Declare @i Int
	Declare @Id uniqueidentifier
	
	DECLARE @Temp TABLE (ID int IDENTITY(1,1) NOT NULL ,ItemId uniqueidentifier NOT NULL)
	DECLARE @Exists int
	
	INSERT INTO @Temp
	SELECT Id from meanstream_dynamics_Item WHERE TYPE = @Type

	SET @count = @@rowcount
	SET @i = 1

	WHILE @i <= @count
	BEGIN
		SELECT @Id = ItemId from @Temp where ID = @i
		update meanstream_dynamics_ItemAttribute set Name = @NewName where ItemId = @Id and Name = @OldName
		set @i = @i + 1
	END
	--end sync
		
END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_dynamics_SyncItemAttributes]    Script Date: 01/23/2012 20:51:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[meanstream_dynamics_SyncItemAttributes]
	-- Add the parameters for the stored procedure here
	@Type as varchar(255)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    Declare @Id uniqueidentifier
	Declare @count Int
	Declare @i Int
	DECLARE @Exists int

	DECLARE @select_list AS varchar(max) -- Leave NULL for COALESCE technique  
	DECLARE @Pos as int
	DECLARE @NextPos as int
	DECLARE @Field as varchar(max)

	SELECT @select_list = COALESCE(@select_list + ',', '') + ' '+ PIVOT_CODE        
	FROM (     
	SELECT DISTINCT Name AS PIVOT_CODE     
		FROM meanstream_dynamics_ItemAttribute, meanstream_dynamics_Item where itemid = id and Type = @Type
	) 
	AS PIVOT_CODES 

	DECLARE @Temp TABLE (ID int IDENTITY(1,1) NOT NULL ,ItemId uniqueidentifier NOT NULL)
	INSERT INTO @Temp
	SELECT Id from meanstream_dynamics_Item WHERE Type = @Type

	SET @count = @@rowcount
	SET @i = 1

	WHILE @i <= @count
	BEGIN
	SELECT @Id = ItemId from @Temp where ID = @i

	--iterate thru delimeted list
	SET @Pos = 1

	WHILE(@Pos <= LEN(@select_list))
	BEGIN
		SELECT @NextPos = CHARINDEX(N',', @select_list,  @Pos)
		IF (@NextPos = 0 OR @NextPos IS NULL)
			SELECT @NextPos = LEN(@select_list) + 1
			SELECT @Field = RTRIM(LTRIM(SUBSTRING(@select_list, @Pos, @NextPos - @Pos)))
			SELECT @Pos = @NextPos+1
		
			--sync other field names
			select @Exists = COUNT(itemId) from meanstream_dynamics_ItemAttribute where ItemId = @Id and Name = @Field	
			IF @Exists = 0
			BEGIN	
				insert into meanstream_dynamics_ItemAttribute (ItemId, Name, Value) values (@Id, @Field, NULL)
			END
		END
		set @i = @i + 1
	END
END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_dynamics_InsertItemAttribute]    Script Date: 01/23/2012 20:51:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[meanstream_dynamics_InsertItemAttribute] 
	-- Add the parameters for the stored procedure here
	@Type nvarchar(255),
	@Name nvarchar(255),
	@Value nvarchar(max)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

		Declare @Id uniqueidentifier
		
		--Select Distinct @Id = Id from meanstream_dynamics_Item Where Type = @Type
		
		--initial insert
		--insert into meanstream_dynamics_ItemAttribute (ItemId, Name, Value) values (@Id, @Name, @Value)
		
		--look for other rows of item type and sync columns if needed
		Declare @count Int
		Declare @i Int
		--Declare @ItemId uniqueidentifier
		DECLARE @Temp TABLE (ID int IDENTITY(1,1) NOT NULL ,ItemId uniqueidentifier NOT NULL)
		DECLARE @Exists int
		
		INSERT INTO @Temp
		SELECT Id from meanstream_dynamics_Item WHERE Type = @Type

		SET @count = @@rowcount
		SET @i = 1

		WHILE @i <= @count
		BEGIN
		SELECT @Id = ItemId from @Temp where ID = @i
		--check attribute exists
		select @Exists = COUNT(itemId) from meanstream_dynamics_ItemAttribute where ItemId = @Id and Name = @Name	
		IF @Exists = 0
		BEGIN
			insert into meanstream_dynamics_ItemAttribute (ItemId, Name, Value) values (@Id, @Name, @Value)
		END
		set @i = @i + 1
		END
		--end sync

END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_dynamics_InsertItem]    Script Date: 01/23/2012 20:51:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[meanstream_dynamics_InsertItem] 
	@Id uniqueidentifier,
	@Type nvarchar(255)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	insert into meanstream_dynamics_Item (Id, Type) Values (@Id, @Type)
	
END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_dynamics_GetItems]    Script Date: 01/23/2012 20:51:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[meanstream_dynamics_GetItems] 
	@Type as varchar(255),
	@WhereClause nvarchar(max)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	IF (LEN(@WhereClause) > 0)
		BEGIN
			Set @WhereClause = ' and ' + @WhereClause
		END
		
	DECLARE @sql AS varchar(max) 
	DECLARE @pivot_list AS varchar(max) -- Leave NULL for COALESCE technique 
	DECLARE @select_list AS varchar(max) -- Leave NULL for COALESCE technique  

	DECLARE @from_list AS varchar(max) -- Leave NULL for COALESCE technique 
	DECLARE @where_list AS varchar(max) -- Leave NULL for COALESCE technique
	DECLARE @where_list2 AS varchar(max) -- Leave NULL for COALESCE technique
	
	SELECT @select_list = COALESCE(@select_list + ', ', '') + ' '+ PIVOT_CODE + '.value as ' + PIVOT_CODE        
	,@from_list = COALESCE(@from_list + ', ', '') + 'meanstream_dynamics_ItemAttribute ' + PIVOT_CODE  
	,@where_list = COALESCE(@where_list + ', ', '') + PIVOT_CODE 
	,@where_list2 = COALESCE(@where_list2 + ' and ', '') + PIVOT_CODE + '.Name=''' + PIVOT_CODE + ''''
	FROM (     
	SELECT DISTINCT Name AS PIVOT_CODE     
		FROM meanstream_dynamics_ItemAttribute, meanstream_dynamics_Item where itemid = id and TYPE LIKE '' + @Type + ''
	) 
	AS PIVOT_CODES 

	DECLARE @Num INT
	DECLARE @Pos INT
	DECLARE @ColumnCount INT
	DECLARE @ColPos INT
	DECLARE @NextPos	int
	DECLARE @Values		nvarchar(MAX)
	DECLARE @Value		nvarchar(MAX)
	DECLARE @RowCount INT
	DECLARE @where nvarchar(Max)

	--get column count
	SET @Pos = 1
	SET @ColumnCount = 0

	WHILE(@Pos <= LEN(@where_list))
	BEGIN
	SELECT @NextPos = CHARINDEX(N',', @where_list,  @Pos)
		IF (@NextPos = 0 OR @NextPos IS NULL)
			SELECT @NextPos = LEN(@where_list) + 1
		SELECT @Value = RTRIM(LTRIM(SUBSTRING(@where_list, @Pos, @NextPos - @Pos)))
		SELECT @Pos = @NextPos+1
		
		IF(@ColumnCount = 0)
			BEGIN
				select @where = '(e.id = ' + @Value + '.ItemId) and ' + @Value + '.itemid='
			END
		ELSE IF(@ColumnCount > 0 AND LEN(@where_list) > @NextPos)
			BEGIN
				select @where = @where + @Value + '.itemid and ' + @Value + '.itemid='
				set @ColumnCount = @ColumnCount
			END	
		ELSE 
			BEGIN
				select @where = @where + @Value + '.itemid'
			END
			
	SET @ColumnCount = @ColumnCount + 1
	END
		
	set @sql = 'select e.id, ' + @select_list + ' from ' + @from_list + ', meanstream_dynamics_Item e where ' + @where + ' and ' + @where_list2 + ' and e.type like ''' + @Type + ''' ' + @WhereClause+''
	exec(@sql)
	--print @sql
END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_dynamics_GetItemById]    Script Date: 01/23/2012 20:51:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[meanstream_dynamics_GetItemById]
	@Id uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE @sql AS varchar(max) 
	DECLARE @pivot_list AS varchar(max) -- Leave NULL for COALESCE technique 
	DECLARE @select_list AS varchar(max) -- Leave NULL for COALESCE technique  

	DECLARE @from_list AS varchar(max) -- Leave NULL for COALESCE technique 
	DECLARE @where_list AS varchar(max) -- Leave NULL for COALESCE technique
	DECLARE @where_list2 AS varchar(max) -- Leave NULL for COALESCE technique
	
	SELECT @select_list = COALESCE(@select_list + ', ', '') + ' '+ PIVOT_CODE + '.value as ' + PIVOT_CODE        
	,@from_list = COALESCE(@from_list + ', ', '') + 'meanstream_dynamics_ItemAttribute ' + PIVOT_CODE  
	,@where_list = COALESCE(@where_list + ', ', '') + PIVOT_CODE 
	,@where_list2 = COALESCE(@where_list2 + ' and ', '') + PIVOT_CODE + '.Name=''' + PIVOT_CODE + ''''
	FROM (     
	SELECT DISTINCT Name AS PIVOT_CODE     
		FROM meanstream_dynamics_ItemAttribute, meanstream_dynamics_Item where itemid = id and Id = @Id
	) 
	AS PIVOT_CODES 

	DECLARE @Num INT
	DECLARE @Pos INT
	DECLARE @ColumnCount INT
	DECLARE @ColPos INT
	DECLARE @NextPos	int
	DECLARE @Values		nvarchar(MAX)
	DECLARE @Value		nvarchar(MAX)
	DECLARE @RowCount INT
	DECLARE @where nvarchar(Max)

	--get column count
	SET @Pos = 1
	SET @ColumnCount = 0

	WHILE(@Pos <= LEN(@where_list))
	BEGIN
	SELECT @NextPos = CHARINDEX(N',', @where_list,  @Pos)
		IF (@NextPos = 0 OR @NextPos IS NULL)
			SELECT @NextPos = LEN(@where_list) + 1
		SELECT @Value = RTRIM(LTRIM(SUBSTRING(@where_list, @Pos, @NextPos - @Pos)))
		SELECT @Pos = @NextPos+1
		
		IF(@ColumnCount = 0)
			BEGIN
				select @where = '(e.id = ' + @Value + '.ItemId) and ' + @Value + '.itemid='
			END
		ELSE IF(@ColumnCount > 0 AND LEN(@where_list) > @NextPos)
			BEGIN
				select @where = @where + @Value + '.itemid and ' + @Value + '.itemid='
				set @ColumnCount = @ColumnCount
			END	
		ELSE 
			BEGIN
				select @where = @where + @Value + '.itemid'
			END
			
	SET @ColumnCount = @ColumnCount + 1
	END
		
	declare @ItemId as nvarchar(36) = @Id
		
	set @sql = 'select e.id, ' + @select_list + ' from ' + @from_list + ', meanstream_dynamics_Item e where ' + @where + ' and ' + @where_list2 + ' and e.id = '''+ @ItemId + ''' '
	exec(@sql)
	--print @sql
END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_dynamics_DeleteItemAttribute]    Script Date: 01/23/2012 20:51:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[meanstream_dynamics_DeleteItemAttribute] 
	@Type nvarchar(255),
	@Name nvarchar(255)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    delete from meanstream_dynamics_ItemAttribute where Name = @Name and ItemId in (select Id from meanstream_dynamics_Item where type = @Type)
    
END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_dynamics_DeleteItem]    Script Date: 01/23/2012 20:51:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[meanstream_dynamics_DeleteItem]
	@Id uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    delete from meanstream_dynamics_ItemAttribute where ItemId = @Id
    delete from meanstream_dynamics_Item where Id = @Id
END
GO
