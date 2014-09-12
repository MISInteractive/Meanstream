/****** Object:  Database [Meanstream-Build] Script Date: 10/11/2011 19:20:04 ******/
CREATE DATABASE [Meanstream-Build] ON  PRIMARY 
( NAME = N'Meanstream-Build', FILENAME = N'd:\data\Meanstream-Build.mdf' , SIZE = 9216KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Meanstream-Build_log', FILENAME = N'd:\data\Meanstream-Build_1.ldf' , SIZE = 97024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Meanstream-Build] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Meanstream-Build].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Meanstream-Build] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [Meanstream-Build] SET ANSI_NULLS OFF
GO
ALTER DATABASE [Meanstream-Build] SET ANSI_PADDING OFF
GO
ALTER DATABASE [Meanstream-Build] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [Meanstream-Build] SET ARITHABORT OFF
GO
ALTER DATABASE [Meanstream-Build] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [Meanstream-Build] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [Meanstream-Build] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [Meanstream-Build] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [Meanstream-Build] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [Meanstream-Build] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [Meanstream-Build] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [Meanstream-Build] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [Meanstream-Build] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [Meanstream-Build] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [Meanstream-Build] SET  DISABLE_BROKER
GO
ALTER DATABASE [Meanstream-Build] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [Meanstream-Build] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [Meanstream-Build] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [Meanstream-Build] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [Meanstream-Build] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [Meanstream-Build] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [Meanstream-Build] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [Meanstream-Build] SET  READ_WRITE
GO
ALTER DATABASE [Meanstream-Build] SET RECOVERY FULL
GO
ALTER DATABASE [Meanstream-Build] SET  MULTI_USER
GO
ALTER DATABASE [Meanstream-Build] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [Meanstream-Build] SET DB_CHAINING OFF
GO

USE [Meanstream-Build]
GO
/****** Object:  Role [aspnet_Membership_BasicAccess]    Script Date: 10/11/2011 11:07:42 ******/
CREATE ROLE [aspnet_Membership_BasicAccess] AUTHORIZATION [dbo]
GO
/****** Object:  Role [aspnet_Membership_FullAccess]    Script Date: 10/11/2011 11:07:42 ******/
CREATE ROLE [aspnet_Membership_FullAccess] AUTHORIZATION [dbo]
GO
/****** Object:  Role [aspnet_Membership_ReportingAccess]    Script Date: 10/11/2011 11:07:42 ******/
CREATE ROLE [aspnet_Membership_ReportingAccess] AUTHORIZATION [dbo]
GO
/****** Object:  Role [aspnet_Personalization_BasicAccess]    Script Date: 10/11/2011 11:07:42 ******/
CREATE ROLE [aspnet_Personalization_BasicAccess] AUTHORIZATION [dbo]
GO
/****** Object:  Role [aspnet_Personalization_FullAccess]    Script Date: 10/11/2011 11:07:42 ******/
CREATE ROLE [aspnet_Personalization_FullAccess] AUTHORIZATION [dbo]
GO
/****** Object:  Role [aspnet_Personalization_ReportingAccess]    Script Date: 10/11/2011 11:07:42 ******/
CREATE ROLE [aspnet_Personalization_ReportingAccess] AUTHORIZATION [dbo]
GO
/****** Object:  Role [aspnet_Profile_BasicAccess]    Script Date: 10/11/2011 11:07:42 ******/
CREATE ROLE [aspnet_Profile_BasicAccess] AUTHORIZATION [dbo]
GO
/****** Object:  Role [aspnet_Profile_FullAccess]    Script Date: 10/11/2011 11:07:42 ******/
CREATE ROLE [aspnet_Profile_FullAccess] AUTHORIZATION [dbo]
GO
/****** Object:  Role [aspnet_Profile_ReportingAccess]    Script Date: 10/11/2011 11:07:42 ******/
CREATE ROLE [aspnet_Profile_ReportingAccess] AUTHORIZATION [dbo]
GO
/****** Object:  Role [aspnet_Roles_BasicAccess]    Script Date: 10/11/2011 11:07:42 ******/
CREATE ROLE [aspnet_Roles_BasicAccess] AUTHORIZATION [dbo]
GO
/****** Object:  Role [aspnet_Roles_FullAccess]    Script Date: 10/11/2011 11:07:42 ******/
CREATE ROLE [aspnet_Roles_FullAccess] AUTHORIZATION [dbo]
GO
/****** Object:  Role [aspnet_Roles_ReportingAccess]    Script Date: 10/11/2011 11:07:42 ******/
CREATE ROLE [aspnet_Roles_ReportingAccess] AUTHORIZATION [dbo]
GO
/****** Object:  Role [aspnet_WebEvent_FullAccess]    Script Date: 10/11/2011 11:07:42 ******/
CREATE ROLE [aspnet_WebEvent_FullAccess] AUTHORIZATION [dbo]
GO
/****** Object:  Schema [aspnet_Membership_BasicAccess]    Script Date: 10/11/2011 11:07:42 ******/
CREATE SCHEMA [aspnet_Membership_BasicAccess] AUTHORIZATION [aspnet_Membership_BasicAccess]
GO
/****** Object:  Schema [aspnet_Membership_FullAccess]    Script Date: 10/11/2011 11:07:42 ******/
CREATE SCHEMA [aspnet_Membership_FullAccess] AUTHORIZATION [aspnet_Membership_FullAccess]
GO
/****** Object:  Schema [aspnet_Membership_ReportingAccess]    Script Date: 10/11/2011 11:07:42 ******/
CREATE SCHEMA [aspnet_Membership_ReportingAccess] AUTHORIZATION [aspnet_Membership_ReportingAccess]
GO
/****** Object:  Schema [aspnet_Personalization_BasicAccess]    Script Date: 10/11/2011 11:07:42 ******/
CREATE SCHEMA [aspnet_Personalization_BasicAccess] AUTHORIZATION [aspnet_Personalization_BasicAccess]
GO
/****** Object:  Schema [aspnet_Personalization_FullAccess]    Script Date: 10/11/2011 11:07:42 ******/
CREATE SCHEMA [aspnet_Personalization_FullAccess] AUTHORIZATION [aspnet_Personalization_FullAccess]
GO
/****** Object:  Schema [aspnet_Personalization_ReportingAccess]    Script Date: 10/11/2011 11:07:42 ******/
CREATE SCHEMA [aspnet_Personalization_ReportingAccess] AUTHORIZATION [aspnet_Personalization_ReportingAccess]
GO
/****** Object:  Schema [aspnet_Profile_BasicAccess]    Script Date: 10/11/2011 11:07:42 ******/
CREATE SCHEMA [aspnet_Profile_BasicAccess] AUTHORIZATION [aspnet_Profile_BasicAccess]
GO
/****** Object:  Schema [aspnet_Profile_FullAccess]    Script Date: 10/11/2011 11:07:42 ******/
CREATE SCHEMA [aspnet_Profile_FullAccess] AUTHORIZATION [aspnet_Profile_FullAccess]
GO
/****** Object:  Schema [aspnet_Profile_ReportingAccess]    Script Date: 10/11/2011 11:07:42 ******/
CREATE SCHEMA [aspnet_Profile_ReportingAccess] AUTHORIZATION [aspnet_Profile_ReportingAccess]
GO
/****** Object:  Schema [aspnet_Roles_BasicAccess]    Script Date: 10/11/2011 11:07:42 ******/
CREATE SCHEMA [aspnet_Roles_BasicAccess] AUTHORIZATION [aspnet_Roles_BasicAccess]
GO
/****** Object:  Schema [aspnet_Roles_FullAccess]    Script Date: 10/11/2011 11:07:42 ******/
CREATE SCHEMA [aspnet_Roles_FullAccess] AUTHORIZATION [aspnet_Roles_FullAccess]
GO
/****** Object:  Schema [aspnet_Roles_ReportingAccess]    Script Date: 10/11/2011 11:07:42 ******/
CREATE SCHEMA [aspnet_Roles_ReportingAccess] AUTHORIZATION [aspnet_Roles_ReportingAccess]
GO
/****** Object:  Schema [aspnet_WebEvent_FullAccess]    Script Date: 10/11/2011 11:07:42 ******/
CREATE SCHEMA [aspnet_WebEvent_FullAccess] AUTHORIZATION [aspnet_WebEvent_FullAccess]
GO
/****** Object:  StoredProcedure [dbo].[vw_aspnet_UsersInRoles_Get]    Script Date: 10/11/2011 11:07:42 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets records from the vw_aspnet_UsersInRoles view passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[vw_aspnet_UsersInRoles_Get]
(

	@WhereClause varchar (2000)  ,

	@OrderBy varchar (2000)  
)
AS


				
				BEGIN

				-- Build the sql query
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = ' SELECT * FROM [dbo].[vw_aspnet_UsersInRoles]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Execution the query
				EXEC sp_executesql @SQL
				
				-- Return total count
				SELECT @@ROWCOUNT AS TotalRowCount
				
				END
GO
/****** Object:  StoredProcedure [dbo].[vw_aspnet_Users_Get]    Script Date: 10/11/2011 11:07:42 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets records from the vw_aspnet_Users view passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[vw_aspnet_Users_Get]
(

	@WhereClause varchar (2000)  ,

	@OrderBy varchar (2000)  
)
AS


				
				BEGIN

				-- Build the sql query
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = ' SELECT * FROM [dbo].[vw_aspnet_Users]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Execution the query
				EXEC sp_executesql @SQL
				
				-- Return total count
				SELECT @@ROWCOUNT AS TotalRowCount
				
				END
GO
/****** Object:  StoredProcedure [dbo].[vw_aspnet_MembershipUsers_Get]    Script Date: 10/11/2011 11:07:42 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets records from the vw_aspnet_MembershipUsers view passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[vw_aspnet_MembershipUsers_Get]
(

	@WhereClause varchar (2000)  ,

	@OrderBy varchar (2000)  
)
AS


				
				BEGIN

				-- Build the sql query
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = ' SELECT * FROM [dbo].[vw_aspnet_MembershipUsers]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Execution the query
				EXEC sp_executesql @SQL
				
				-- Return total count
				SELECT @@ROWCOUNT AS TotalRowCount
				
				END
GO
/****** Object:  StoredProcedure [dbo].[vw_meanstream_RecentEdits_Get]    Script Date: 10/11/2011 11:07:42 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets records from the vw_meanstream_RecentEdits view passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[vw_meanstream_RecentEdits_Get]
(

	@WhereClause varchar (2000)  ,

	@OrderBy varchar (2000)  
)
AS


				
				BEGIN

				-- Build the sql query
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = ' SELECT * FROM [dbo].[vw_meanstream_RecentEdits]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Execution the query
				EXEC sp_executesql @SQL
				
				-- Return total count
				SELECT @@ROWCOUNT AS TotalRowCount
				
				END
GO
/****** Object:  StoredProcedure [dbo].[vw_aspnet_Profiles_Get]    Script Date: 10/11/2011 11:07:42 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets records from the vw_aspnet_Profiles view passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[vw_aspnet_Profiles_Get]
(

	@WhereClause varchar (2000)  ,

	@OrderBy varchar (2000)  
)
AS


				
				BEGIN

				-- Build the sql query
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = ' SELECT * FROM [dbo].[vw_aspnet_Profiles]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Execution the query
				EXEC sp_executesql @SQL
				
				-- Return total count
				SELECT @@ROWCOUNT AS TotalRowCount
				
				END
GO
/****** Object:  StoredProcedure [dbo].[vw_aspnet_Roles_Get]    Script Date: 10/11/2011 11:07:42 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets records from the vw_aspnet_Roles view passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[vw_aspnet_Roles_Get]
(

	@WhereClause varchar (2000)  ,

	@OrderBy varchar (2000)  
)
AS


				
				BEGIN

				-- Build the sql query
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = ' SELECT * FROM [dbo].[vw_aspnet_Roles]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Execution the query
				EXEC sp_executesql @SQL
				
				-- Return total count
				SELECT @@ROWCOUNT AS TotalRowCount
				
				END
GO
/****** Object:  StoredProcedure [dbo].[vw_aspnet_Applications_Get]    Script Date: 10/11/2011 11:07:42 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets records from the vw_aspnet_Applications view passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[vw_aspnet_Applications_Get]
(

	@WhereClause varchar (2000)  ,

	@OrderBy varchar (2000)  
)
AS


				
				BEGIN

				-- Build the sql query
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = ' SELECT * FROM [dbo].[vw_aspnet_Applications]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Execution the query
				EXEC sp_executesql @SQL
				
				-- Return total count
				SELECT @@ROWCOUNT AS TotalRowCount
				
				END
GO
/****** Object:  Table [dbo].[meanstream_UserControl]    Script Date: 10/11/2011 11:07:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[meanstream_UserControl](
	[Id] [uniqueidentifier] NOT NULL,
	[ModuleId] [uniqueidentifier] NULL,
	[ModuleVersionId] [uniqueidentifier] NULL,
	[Name] [nvarchar](255) NOT NULL,
	[VirtualPath] [nvarchar](500) NOT NULL,
	[Visible] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[LastModifiedDate] [datetime] NOT NULL,
	[LastModifiedBy] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_meanstream_UserControl] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[meanstream_Tracing]    Script Date: 10/11/2011 11:07:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[meanstream_Tracing](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TraceDateTime] [datetime] NOT NULL,
	[TraceCategory] [nvarchar](50) NOT NULL,
	[TraceDescription] [nvarchar](1024) NULL,
	[StackTrace] [nvarchar](max) NULL,
	[DetailedErrorDescription] [nvarchar](max) NULL,
 CONSTRAINT [PK_meanstream_Tracing] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[meanstream_Skins]    Script Date: 10/11/2011 11:07:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[meanstream_Skins](
	[Id] [uniqueidentifier] NOT NULL,
	[PortalId] [uniqueidentifier] NOT NULL,
	[SkinRoot] [nvarchar](50) NOT NULL,
	[SkinSrc] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_meanstream_Skins] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[meanstream_UserPreference]    Script Date: 10/11/2011 11:07:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[meanstream_UserPreference](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[PreferenceId] [uniqueidentifier] NOT NULL,
	[ParamValue] [ntext] NULL,
 CONSTRAINT [PK_meanstream_UserPreference] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[meanstream_PagePermissionVersion]    Script Date: 10/11/2011 11:07:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[meanstream_PagePermissionVersion](
	[Id] [uniqueidentifier] NOT NULL,
	[VersionId] [uniqueidentifier] NOT NULL,
	[PermissionId] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_meanstream_PagePermissionVersion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[meanstream_Permission]    Script Date: 10/11/2011 11:07:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[meanstream_Permission](
	[Id] [uniqueidentifier] NOT NULL,
	[PermissionCode] [varchar](50) NOT NULL,
	[PermissionKey] [varchar](20) NOT NULL,
	[PermissionName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_meanstream_Permission_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[meanstream_Portals]    Script Date: 10/11/2011 11:07:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[meanstream_Portals](
	[Id] [uniqueidentifier] NOT NULL,
	[ApplicationId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Domain] [nvarchar](255) NOT NULL,
	[Root] [nvarchar](255) NOT NULL,
	[HomePageUrl] [nvarchar](255) NOT NULL,
	[LoginPageUrl] [nvarchar](255) NOT NULL,
	[Theme] [nvarchar](255) NULL,
 CONSTRAINT [PK_meanstream_Portals_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[meanstream_Preference]    Script Date: 10/11/2011 11:07:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[meanstream_Preference](
	[Id] [uniqueidentifier] NOT NULL,
	[PreferenceId] [uniqueidentifier] NOT NULL,
	[Name] [varchar](500) NOT NULL,
 CONSTRAINT [PK_meanstream_Preference] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[meanstream_Roles]    Script Date: 10/11/2011 11:07:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[meanstream_Roles](
	[Id] [uniqueidentifier] NOT NULL,
	[RoleName] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[IsPublic] [bit] NOT NULL,
	[AutoAssignment] [bit] NOT NULL,
 CONSTRAINT [PK_meanstream_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[meanstream_ScheduledTasks]    Script Date: 10/11/2011 11:07:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[meanstream_ScheduledTasks](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Interval] [int] NOT NULL,
	[NextRunTime] [datetime] NULL,
	[Status] [nvarchar](50) NOT NULL,
	[StartupType] [nvarchar](50) NOT NULL,
	[LastRunSuccessful] [bit] NULL,
	[LastRunResult] [nvarchar](max) NULL,
	[LastRunDate] [datetime] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[LastModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_meanstream_ScheduledTasks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[meanstream_SkinPane]    Script Date: 10/11/2011 11:07:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[meanstream_SkinPane](
	[Id] [uniqueidentifier] NOT NULL,
	[SkinId] [uniqueidentifier] NOT NULL,
	[Pane] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_meanstream_SkinPane] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Settings_Update]    Script Date: 10/11/2011 11:07:42 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Updates a record in the meanstream_Settings table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Settings_Update]
(

	@Id int   ,

	@Param nvarchar (MAX)  ,

	@Value nvarchar (MAX)  
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[meanstream_Settings]
				SET
					[Param] = @Param
					,[Value] = @Value
				WHERE
[Id] = @Id
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Settings_Insert]    Script Date: 10/11/2011 11:07:42 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Inserts a record into the meanstream_Settings table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Settings_Insert]
(

	@Id int    OUTPUT,

	@Param nvarchar (MAX)  ,

	@Value nvarchar (MAX)  
)
AS


				
				INSERT INTO [dbo].[meanstream_Settings]
					(
					[Param]
					,[Value]
					)
				VALUES
					(
					@Param
					,@Value
					)
				
				-- Get the identity value
				SET @Id = SCOPE_IDENTITY()
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Settings_GetPaged]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets records from the meanstream_Settings table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Settings_GetPaged]
(

	@WhereClause varchar (2000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [Id] int 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([Id])'
				SET @SQL = @SQL + ' SELECT'
				IF @PageSize > 0
				BEGIN
					SET @SQL = @SQL + ' TOP ' + CONVERT(nvarchar, @PageUpperBound)
				END
				SET @SQL = @SQL + ' [Id]'
				SET @SQL = @SQL + ' FROM [dbo].[meanstream_Settings]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Return paged results
				SELECT O.[Id], O.[Param], O.[Value]
				FROM
				    [dbo].[meanstream_Settings] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[Id] = PageIndex.[Id]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[meanstream_Settings]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Settings_GetById]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Select records from the meanstream_Settings table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Settings_GetById]
(

	@Id int   
)
AS


				SELECT
					[Id],
					[Param],
					[Value]
				FROM
					[dbo].[meanstream_Settings]
				WHERE
					[Id] = @Id
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Settings_Get_List]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets all records from the meanstream_Settings table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Settings_Get_List]

AS


				
				SELECT
					[Id],
					[Param],
					[Value]
				FROM
					[dbo].[meanstream_Settings]
					
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Settings_Find]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Finds records in the meanstream_Settings table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Settings_Find]
(

	@SearchUsingOR bit   = null ,

	@Id int   = null ,

	@Param nvarchar (MAX)  = null ,

	@Value nvarchar (MAX)  = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [Id]
	, [Param]
	, [Value]
    FROM
	[dbo].[meanstream_Settings]
    WHERE 
	 ([Id] = @Id OR @Id IS NULL)
	AND ([Param] = @Param OR @Param IS NULL)
	AND ([Value] = @Value OR @Value IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [Id]
	, [Param]
	, [Value]
    FROM
	[dbo].[meanstream_Settings]
    WHERE 
	 ([Id] = @Id AND @Id is not null)
	OR ([Param] = @Param AND @Param is not null)
	OR ([Value] = @Value AND @Value is not null)
	SELECT @@ROWCOUNT			
  END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Settings_Delete]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Deletes a record in the meanstream_Settings table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Settings_Delete]
(

	@Id int   
)
AS


				DELETE FROM [dbo].[meanstream_Settings] WITH (ROWLOCK) 
				WHERE
					[Id] = @Id
GO
/****** Object:  Table [dbo].[aspnet_SchemaVersions]    Script Date: 10/11/2011 11:07:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[aspnet_SchemaVersions](
	[Feature] [nvarchar](128) NOT NULL,
	[CompatibleSchemaVersion] [nvarchar](128) NOT NULL,
	[IsCurrentVersion] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Feature] ASC,
	[CompatibleSchemaVersion] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[aspnet_Applications]    Script Date: 10/11/2011 11:07:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[aspnet_Applications](
	[ApplicationName] [nvarchar](256) NOT NULL,
	[LoweredApplicationName] [nvarchar](256) NOT NULL,
	[ApplicationId] [uniqueidentifier] NOT NULL,
	[Description] [nvarchar](256) NULL,
PRIMARY KEY NONCLUSTERED 
(
	[ApplicationId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[LoweredApplicationName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[ApplicationName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Setup_RestorePermissions]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Setup_RestorePermissions]
    @name   sysname
AS
BEGIN
    DECLARE @object sysname
    DECLARE @protectType char(10)
    DECLARE @action varchar(60)
    DECLARE @grantee sysname
    DECLARE @cmd nvarchar(500)
    DECLARE c1 cursor FORWARD_ONLY FOR
        SELECT Object, ProtectType, [Action], Grantee FROM #aspnet_Permissions where Object = @name

    OPEN c1

    FETCH c1 INTO @object, @protectType, @action, @grantee
    WHILE (@@fetch_status = 0)
    BEGIN
        SET @cmd = @protectType + ' ' + @action + ' on ' + @object + ' TO [' + @grantee + ']'
        EXEC (@cmd)
        FETCH c1 INTO @object, @protectType, @action, @grantee
    END

    CLOSE c1
    DEALLOCATE c1
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Setup_RemoveAllRoleMembers]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Setup_RemoveAllRoleMembers]
    @name   sysname
AS
BEGIN
    CREATE TABLE #aspnet_RoleMembers
    (
        Group_name      sysname,
        Group_id        smallint,
        Users_in_group  sysname,
        User_id         smallint
    )

    INSERT INTO #aspnet_RoleMembers
    EXEC sp_helpuser @name

    DECLARE @user_id smallint
    DECLARE @cmd nvarchar(500)
    DECLARE c1 cursor FORWARD_ONLY FOR
        SELECT User_id FROM #aspnet_RoleMembers

    OPEN c1

    FETCH c1 INTO @user_id
    WHILE (@@fetch_status = 0)
    BEGIN
        SET @cmd = 'EXEC sp_droprolemember ' + '''' + @name + ''', ''' + USER_NAME(@user_id) + ''''
        EXEC (@cmd)
        FETCH c1 INTO @user_id
    END

    CLOSE c1
    DEALLOCATE c1
END
GO
/****** Object:  Table [dbo].[aspnet_WebEvent_Events]    Script Date: 10/11/2011 11:07:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[aspnet_WebEvent_Events](
	[EventId] [char](32) NOT NULL,
	[EventTimeUtc] [datetime] NOT NULL,
	[EventTime] [datetime] NOT NULL,
	[EventType] [nvarchar](256) NOT NULL,
	[EventSequence] [decimal](19, 0) NOT NULL,
	[EventOccurrence] [decimal](19, 0) NOT NULL,
	[EventCode] [int] NOT NULL,
	[EventDetailCode] [int] NOT NULL,
	[Message] [nvarchar](1024) NULL,
	[ApplicationPath] [nvarchar](256) NULL,
	[ApplicationVirtualPath] [nvarchar](256) NULL,
	[MachineName] [nvarchar](256) NOT NULL,
	[RequestUrl] [nvarchar](1024) NULL,
	[ExceptionType] [nvarchar](256) NULL,
	[Details] [ntext] NULL,
PRIMARY KEY CLUSTERED 
(
	[EventId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[meanstream_ApplicationMessaging]    Script Date: 10/11/2011 11:07:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[meanstream_ApplicationMessaging](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SenderAppicationId] [uniqueidentifier] NOT NULL,
	[TargetApplicationId] [uniqueidentifier] NULL,
	[MessageType] [nvarchar](255) NOT NULL,
	[Message] [nvarchar](max) NOT NULL,
	[DateCreated] [datetime] NULL,
 CONSTRAINT [PK_meanstream_ApplicationMessaging] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[meanstream_ApplicationSettings]    Script Date: 10/11/2011 11:07:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[meanstream_ApplicationSettings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Param] [nvarchar](max) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_meanstream_Settings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[meanstream_ApplicationMessagingInstance]    Script Date: 10/11/2011 11:07:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[meanstream_ApplicationMessagingInstance](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationId] [uniqueidentifier] NOT NULL,
	[DomainName] [nvarchar](255) NOT NULL,
	[MachineName] [nvarchar](255) NOT NULL,
	[RegisteredDate] [datetime] NOT NULL,
	[LastUpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_meanstream_ApplicationMessageInstance] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[meanstream_Content]    Script Date: 10/11/2011 11:07:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[meanstream_Content](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ContentType] [nvarchar](255) NULL,
	[Title] [nvarchar](500) NULL,
	[Author] [nvarchar](255) NULL,
	[UserId] [uniqueidentifier] NULL,
	[Text] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[LastUpdateDate] [datetime] NOT NULL,
	[IsShared] [bit] NOT NULL,
 CONSTRAINT [PK_meanstream_Content_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[meanstream_Attributes]    Script Date: 10/11/2011 11:07:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[meanstream_Attributes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ComponentId] [uniqueidentifier] NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Value] [ntext] NULL,
	[DataType] [varchar](50) NOT NULL,
 CONSTRAINT [PK_meanstream_Attributes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[meanstream_ContentType]    Script Date: 10/11/2011 11:07:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[meanstream_ContentType](
	[Type] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_meanstream_ContentType] PRIMARY KEY CLUSTERED 
(
	[Type] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[meanstream_FreeText]    Script Date: 10/11/2011 11:07:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[meanstream_FreeText](
	[Id] [uniqueidentifier] NOT NULL,
	[ContentId] [int] NOT NULL,
	[ModuleId] [uniqueidentifier] NULL,
	[ModuleVersionId] [uniqueidentifier] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[LastModifiedDate] [datetime] NOT NULL,
	[LastModifiedBy] [uniqueidentifier] NOT NULL,
	[IsLocked] [bit] NOT NULL,
	[CheckedOutBy] [uniqueidentifier] NULL,
 CONSTRAINT [PK_meanstream_FreeText] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[meanstream_Messaging]    Script Date: 10/11/2011 11:07:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[meanstream_Messaging](
	[Id] [uniqueidentifier] NOT NULL,
	[MessageType] [nvarchar](50) NOT NULL,
	[Sender] [uniqueidentifier] NULL,
	[Recipient] [uniqueidentifier] NULL,
	[Subject] [nvarchar](500) NULL,
	[Body] [nvarchar](max) NULL,
	[SentOn] [datetime] NULL,
	[ReceivedOn] [datetime] NULL,
	[Status] [nvarchar](50) NOT NULL,
	[IsQueued] [bit] NOT NULL,
	[OpenedOn] [datetime] NULL,
	[Opened] [bit] NOT NULL,
 CONSTRAINT [PK_meanstream_Messaging_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[meanstream_MessagingMessageType]    Script Date: 10/11/2011 11:07:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[meanstream_MessagingMessageType](
	[MessageType] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_meanstream_MessagingMessageType_1] PRIMARY KEY CLUSTERED 
(
	[MessageType] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[meanstream_Module]    Script Date: 10/11/2011 11:07:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[meanstream_Module](
	[Id] [uniqueidentifier] NOT NULL,
	[ModuleDefId] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](256) NULL,
	[AllPages] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedBy] [nvarchar](100) NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[SkinPaneId] [uniqueidentifier] NOT NULL,
	[DisplayOrder] [int] NOT NULL,
	[PageId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_meanstream_Module] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[meanstream_ModuleControls]    Script Date: 10/11/2011 11:07:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[meanstream_ModuleControls](
	[Id] [uniqueidentifier] NOT NULL,
	[ModuleDefId] [uniqueidentifier] NOT NULL,
	[ControlKey] [nvarchar](50) NOT NULL,
	[ControlPath] [nvarchar](256) NOT NULL,
	[ControlTitle] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_meanstream_ModuleControls] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[meanstream_ModuleDefinitions]    Script Date: 10/11/2011 11:07:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[meanstream_ModuleDefinitions](
	[Id] [uniqueidentifier] NOT NULL,
	[FriendlyName] [nvarchar](128) NOT NULL,
	[Enabled] [bit] NOT NULL,
	[IsDefault] [bit] NOT NULL,
 CONSTRAINT [PK_meanstream_ModuleDefinitions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[meanstream_ModuleVersion]    Script Date: 10/11/2011 11:07:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[meanstream_ModuleVersion](
	[Id] [uniqueidentifier] NOT NULL,
	[PageVersionId] [uniqueidentifier] NOT NULL,
	[ModuleDefId] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](255) NULL,
	[AllPages] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedBy] [nvarchar](100) NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[SkinPaneId] [uniqueidentifier] NOT NULL,
	[SharedId] [uniqueidentifier] NOT NULL,
	[DisplayOrder] [int] NOT NULL,
	[DeletedDate] [nvarchar](50) NULL,
	[LastModifiedDate] [datetime] NOT NULL,
	[LastModifiedBy] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_meanstream_ModuleVersion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[meanstream_ModulePermission]    Script Date: 10/11/2011 11:07:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[meanstream_ModulePermission](
	[Id] [uniqueidentifier] NOT NULL,
	[ModuleId] [uniqueidentifier] NOT NULL,
	[PermissionId] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_meanstream_ModulePermission] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[meanstream_ModuleVersionPermission]    Script Date: 10/11/2011 11:07:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[meanstream_ModuleVersionPermission](
	[Id] [uniqueidentifier] NOT NULL,
	[PageVersionId] [uniqueidentifier] NOT NULL,
	[ModuleId] [uniqueidentifier] NOT NULL,
	[PermissionId] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_meanstream_ModuleVersionPermission] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[meanstream_Page]    Script Date: 10/11/2011 11:07:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[meanstream_Page](
	[Id] [uniqueidentifier] NOT NULL,
	[DisplayOrder] [int] NOT NULL,
	[PortalId] [uniqueidentifier] NULL,
	[Name] [nvarchar](500) NOT NULL,
	[IsVisible] [bit] NOT NULL,
	[ParentId] [uniqueidentifier] NULL,
	[DisableLink] [bit] NOT NULL,
	[Title] [nvarchar](200) NULL,
	[Description] [nvarchar](500) NULL,
	[KeyWords] [nvarchar](500) NULL,
	[IsDeleted] [bit] NOT NULL,
	[Url] [nvarchar](255) NULL,
	[Type] [int] NULL,
	[SkinId] [uniqueidentifier] NOT NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[PublishedDate] [datetime] NULL,
	[IsHome] [bit] NULL,
	[IsPublished] [bit] NULL,
	[VersionId] [uniqueidentifier] NOT NULL,
	[Author] [varchar](500) NULL,
	[EnableCaching] [bit] NULL,
	[EnableViewState] [bit] NULL,
	[Index] [bit] NULL,
 CONSTRAINT [PK_meanstream_Page] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[meanstream_PagePermission]    Script Date: 10/11/2011 11:07:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[meanstream_PagePermission](
	[Id] [uniqueidentifier] NOT NULL,
	[PageId] [uniqueidentifier] NOT NULL,
	[PermissionId] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_meanstream_PagePermission] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[meanstream_PageVersion]    Script Date: 10/11/2011 11:07:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[meanstream_PageVersion](
	[Id] [uniqueidentifier] NOT NULL,
	[PageId] [uniqueidentifier] NULL,
	[DisplayOrder] [int] NULL,
	[PortalId] [uniqueidentifier] NULL,
	[Name] [nvarchar](500) NOT NULL,
	[IsVisible] [bit] NOT NULL,
	[ParentId] [uniqueidentifier] NULL,
	[DisableLink] [bit] NOT NULL,
	[Title] [nvarchar](200) NULL,
	[Description] [nvarchar](500) NULL,
	[KeyWords] [nvarchar](500) NULL,
	[IsDeleted] [bit] NOT NULL,
	[Url] [nvarchar](255) NULL,
	[Type] [int] NULL,
	[SkinId] [uniqueidentifier] NOT NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[IsPublished] [bit] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[LastSavedDate] [datetime] NULL,
	[Approved] [bit] NULL,
	[IsPublishedVersion] [bit] NOT NULL,
	[Author] [varchar](500) NULL,
	[EnableCaching] [bit] NULL,
	[EnableViewState] [bit] NULL,
	[Index] [bit] NULL,
 CONSTRAINT [PK_meanstream_PageVersion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[meanstream_PagePermissionVersion_Update]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Updates a record in the meanstream_PagePermissionVersion table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_PagePermissionVersion_Update]
(

	@Id uniqueidentifier   ,

	@OriginalId uniqueidentifier   ,

	@VersionId uniqueidentifier   ,

	@PermissionId uniqueidentifier   ,

	@RoleId uniqueidentifier   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[meanstream_PagePermissionVersion]
				SET
					[Id] = @Id
					,[VersionId] = @VersionId
					,[PermissionId] = @PermissionId
					,[RoleId] = @RoleId
				WHERE
[Id] = @OriginalId
GO
/****** Object:  StoredProcedure [dbo].[meanstream_PagePermissionVersion_Insert]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Inserts a record into the meanstream_PagePermissionVersion table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_PagePermissionVersion_Insert]
(

	@Id uniqueidentifier   ,

	@VersionId uniqueidentifier   ,

	@PermissionId uniqueidentifier   ,

	@RoleId uniqueidentifier   
)
AS


				
				INSERT INTO [dbo].[meanstream_PagePermissionVersion]
					(
					[Id]
					,[VersionId]
					,[PermissionId]
					,[RoleId]
					)
				VALUES
					(
					@Id
					,@VersionId
					,@PermissionId
					,@RoleId
					)
GO
/****** Object:  StoredProcedure [dbo].[meanstream_PagePermissionVersion_GetPaged]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets records from the meanstream_PagePermissionVersion table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_PagePermissionVersion_GetPaged]
(

	@WhereClause varchar (2000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [Id] uniqueidentifier 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([Id])'
				SET @SQL = @SQL + ' SELECT'
				IF @PageSize > 0
				BEGIN
					SET @SQL = @SQL + ' TOP ' + CONVERT(nvarchar, @PageUpperBound)
				END
				SET @SQL = @SQL + ' [Id]'
				SET @SQL = @SQL + ' FROM [dbo].[meanstream_PagePermissionVersion]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Return paged results
				SELECT O.[Id], O.[VersionId], O.[PermissionId], O.[RoleId]
				FROM
				    [dbo].[meanstream_PagePermissionVersion] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[Id] = PageIndex.[Id]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[meanstream_PagePermissionVersion]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_PagePermissionVersion_GetById]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Select records from the meanstream_PagePermissionVersion table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_PagePermissionVersion_GetById]
(

	@Id uniqueidentifier   
)
AS


				SELECT
					[Id],
					[VersionId],
					[PermissionId],
					[RoleId]
				FROM
					[dbo].[meanstream_PagePermissionVersion]
				WHERE
					[Id] = @Id
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[meanstream_PagePermissionVersion_Get_List]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets all records from the meanstream_PagePermissionVersion table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_PagePermissionVersion_Get_List]

AS


				
				SELECT
					[Id],
					[VersionId],
					[PermissionId],
					[RoleId]
				FROM
					[dbo].[meanstream_PagePermissionVersion]
					
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[meanstream_PagePermissionVersion_Find]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Finds records in the meanstream_PagePermissionVersion table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_PagePermissionVersion_Find]
(

	@SearchUsingOR bit   = null ,

	@Id uniqueidentifier   = null ,

	@VersionId uniqueidentifier   = null ,

	@PermissionId uniqueidentifier   = null ,

	@RoleId uniqueidentifier   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [Id]
	, [VersionId]
	, [PermissionId]
	, [RoleId]
    FROM
	[dbo].[meanstream_PagePermissionVersion]
    WHERE 
	 ([Id] = @Id OR @Id IS NULL)
	AND ([VersionId] = @VersionId OR @VersionId IS NULL)
	AND ([PermissionId] = @PermissionId OR @PermissionId IS NULL)
	AND ([RoleId] = @RoleId OR @RoleId IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [Id]
	, [VersionId]
	, [PermissionId]
	, [RoleId]
    FROM
	[dbo].[meanstream_PagePermissionVersion]
    WHERE 
	 ([Id] = @Id AND @Id is not null)
	OR ([VersionId] = @VersionId AND @VersionId is not null)
	OR ([PermissionId] = @PermissionId AND @PermissionId is not null)
	OR ([RoleId] = @RoleId AND @RoleId is not null)
	SELECT @@ROWCOUNT			
  END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_PagePermissionVersion_Delete]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Deletes a record in the meanstream_PagePermissionVersion table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_PagePermissionVersion_Delete]
(

	@Id uniqueidentifier   
)
AS


				DELETE FROM [dbo].[meanstream_PagePermissionVersion] WITH (ROWLOCK) 
				WHERE
					[Id] = @Id
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Page_Update]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Updates a record in the meanstream_Page table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Page_Update]
(

	@Id uniqueidentifier   ,

	@OriginalId uniqueidentifier   ,

	@DisplayOrder int   ,

	@PortalId uniqueidentifier   ,

	@Name nvarchar (500)  ,

	@IsVisible bit   ,

	@ParentId uniqueidentifier   ,

	@DisableLink bit   ,

	@Title nvarchar (200)  ,

	@Description nvarchar (500)  ,

	@KeyWords nvarchar (500)  ,

	@IsDeleted bit   ,

	@Url nvarchar (255)  ,

	@Type int   ,

	@SkinId uniqueidentifier   ,

	@StartDate datetime   ,

	@EndDate datetime   ,

	@PublishedDate datetime   ,

	@IsHome bit   ,

	@IsPublished bit   ,

	@VersionId uniqueidentifier   ,

	@Author varchar (500)  ,

	@EnableCaching bit   ,

	@EnableViewState bit   ,

	@Index bit   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[meanstream_Page]
				SET
					[Id] = @Id
					,[DisplayOrder] = @DisplayOrder
					,[PortalId] = @PortalId
					,[Name] = @Name
					,[IsVisible] = @IsVisible
					,[ParentId] = @ParentId
					,[DisableLink] = @DisableLink
					,[Title] = @Title
					,[Description] = @Description
					,[KeyWords] = @KeyWords
					,[IsDeleted] = @IsDeleted
					,[Url] = @Url
					,[Type] = @Type
					,[SkinId] = @SkinId
					,[StartDate] = @StartDate
					,[EndDate] = @EndDate
					,[PublishedDate] = @PublishedDate
					,[IsHome] = @IsHome
					,[IsPublished] = @IsPublished
					,[VersionId] = @VersionId
					,[Author] = @Author
					,[EnableCaching] = @EnableCaching
					,[EnableViewState] = @EnableViewState
					,[Index] = @Index
				WHERE
[Id] = @OriginalId
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Page_Insert]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Inserts a record into the meanstream_Page table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Page_Insert]
(

	@Id uniqueidentifier   ,

	@DisplayOrder int   ,

	@PortalId uniqueidentifier   ,

	@Name nvarchar (500)  ,

	@IsVisible bit   ,

	@ParentId uniqueidentifier   ,

	@DisableLink bit   ,

	@Title nvarchar (200)  ,

	@Description nvarchar (500)  ,

	@KeyWords nvarchar (500)  ,

	@IsDeleted bit   ,

	@Url nvarchar (255)  ,

	@Type int   ,

	@SkinId uniqueidentifier   ,

	@StartDate datetime   ,

	@EndDate datetime   ,

	@PublishedDate datetime   ,

	@IsHome bit   ,

	@IsPublished bit   ,

	@VersionId uniqueidentifier   ,

	@Author varchar (500)  ,

	@EnableCaching bit   ,

	@EnableViewState bit   ,

	@Index bit   
)
AS


				
				INSERT INTO [dbo].[meanstream_Page]
					(
					[Id]
					,[DisplayOrder]
					,[PortalId]
					,[Name]
					,[IsVisible]
					,[ParentId]
					,[DisableLink]
					,[Title]
					,[Description]
					,[KeyWords]
					,[IsDeleted]
					,[Url]
					,[Type]
					,[SkinId]
					,[StartDate]
					,[EndDate]
					,[PublishedDate]
					,[IsHome]
					,[IsPublished]
					,[VersionId]
					,[Author]
					,[EnableCaching]
					,[EnableViewState]
					,[Index]
					)
				VALUES
					(
					@Id
					,@DisplayOrder
					,@PortalId
					,@Name
					,@IsVisible
					,@ParentId
					,@DisableLink
					,@Title
					,@Description
					,@KeyWords
					,@IsDeleted
					,@Url
					,@Type
					,@SkinId
					,@StartDate
					,@EndDate
					,@PublishedDate
					,@IsHome
					,@IsPublished
					,@VersionId
					,@Author
					,@EnableCaching
					,@EnableViewState
					,@Index
					)
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Page_GetPaged]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets records from the meanstream_Page table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Page_GetPaged]
(

	@WhereClause varchar (2000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [Id] uniqueidentifier 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([Id])'
				SET @SQL = @SQL + ' SELECT'
				IF @PageSize > 0
				BEGIN
					SET @SQL = @SQL + ' TOP ' + CONVERT(nvarchar, @PageUpperBound)
				END
				SET @SQL = @SQL + ' [Id]'
				SET @SQL = @SQL + ' FROM [dbo].[meanstream_Page]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Return paged results
				SELECT O.[Id], O.[DisplayOrder], O.[PortalId], O.[Name], O.[IsVisible], O.[ParentId], O.[DisableLink], O.[Title], O.[Description], O.[KeyWords], O.[IsDeleted], O.[Url], O.[Type], O.[SkinId], O.[StartDate], O.[EndDate], O.[PublishedDate], O.[IsHome], O.[IsPublished], O.[VersionId], O.[Author], O.[EnableCaching], O.[EnableViewState], O.[Index]
				FROM
				    [dbo].[meanstream_Page] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[Id] = PageIndex.[Id]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[meanstream_Page]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Page_GetById]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Select records from the meanstream_Page table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Page_GetById]
(

	@Id uniqueidentifier   
)
AS


				SELECT
					[Id],
					[DisplayOrder],
					[PortalId],
					[Name],
					[IsVisible],
					[ParentId],
					[DisableLink],
					[Title],
					[Description],
					[KeyWords],
					[IsDeleted],
					[Url],
					[Type],
					[SkinId],
					[StartDate],
					[EndDate],
					[PublishedDate],
					[IsHome],
					[IsPublished],
					[VersionId],
					[Author],
					[EnableCaching],
					[EnableViewState],
					[Index]
				FROM
					[dbo].[meanstream_Page]
				WHERE
					[Id] = @Id
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Page_Get_List]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets all records from the meanstream_Page table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Page_Get_List]

AS


				
				SELECT
					[Id],
					[DisplayOrder],
					[PortalId],
					[Name],
					[IsVisible],
					[ParentId],
					[DisableLink],
					[Title],
					[Description],
					[KeyWords],
					[IsDeleted],
					[Url],
					[Type],
					[SkinId],
					[StartDate],
					[EndDate],
					[PublishedDate],
					[IsHome],
					[IsPublished],
					[VersionId],
					[Author],
					[EnableCaching],
					[EnableViewState],
					[Index]
				FROM
					[dbo].[meanstream_Page]
					
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Page_Find]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Finds records in the meanstream_Page table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Page_Find]
(

	@SearchUsingOR bit   = null ,

	@Id uniqueidentifier   = null ,

	@DisplayOrder int   = null ,

	@PortalId uniqueidentifier   = null ,

	@Name nvarchar (500)  = null ,

	@IsVisible bit   = null ,

	@ParentId uniqueidentifier   = null ,

	@DisableLink bit   = null ,

	@Title nvarchar (200)  = null ,

	@Description nvarchar (500)  = null ,

	@KeyWords nvarchar (500)  = null ,

	@IsDeleted bit   = null ,

	@Url nvarchar (255)  = null ,

	@Type int   = null ,

	@SkinId uniqueidentifier   = null ,

	@StartDate datetime   = null ,

	@EndDate datetime   = null ,

	@PublishedDate datetime   = null ,

	@IsHome bit   = null ,

	@IsPublished bit   = null ,

	@VersionId uniqueidentifier   = null ,

	@Author varchar (500)  = null ,

	@EnableCaching bit   = null ,

	@EnableViewState bit   = null ,

	@Index bit   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [Id]
	, [DisplayOrder]
	, [PortalId]
	, [Name]
	, [IsVisible]
	, [ParentId]
	, [DisableLink]
	, [Title]
	, [Description]
	, [KeyWords]
	, [IsDeleted]
	, [Url]
	, [Type]
	, [SkinId]
	, [StartDate]
	, [EndDate]
	, [PublishedDate]
	, [IsHome]
	, [IsPublished]
	, [VersionId]
	, [Author]
	, [EnableCaching]
	, [EnableViewState]
	, [Index]
    FROM
	[dbo].[meanstream_Page]
    WHERE 
	 ([Id] = @Id OR @Id IS NULL)
	AND ([DisplayOrder] = @DisplayOrder OR @DisplayOrder IS NULL)
	AND ([PortalId] = @PortalId OR @PortalId IS NULL)
	AND ([Name] = @Name OR @Name IS NULL)
	AND ([IsVisible] = @IsVisible OR @IsVisible IS NULL)
	AND ([ParentId] = @ParentId OR @ParentId IS NULL)
	AND ([DisableLink] = @DisableLink OR @DisableLink IS NULL)
	AND ([Title] = @Title OR @Title IS NULL)
	AND ([Description] = @Description OR @Description IS NULL)
	AND ([KeyWords] = @KeyWords OR @KeyWords IS NULL)
	AND ([IsDeleted] = @IsDeleted OR @IsDeleted IS NULL)
	AND ([Url] = @Url OR @Url IS NULL)
	AND ([Type] = @Type OR @Type IS NULL)
	AND ([SkinId] = @SkinId OR @SkinId IS NULL)
	AND ([StartDate] = @StartDate OR @StartDate IS NULL)
	AND ([EndDate] = @EndDate OR @EndDate IS NULL)
	AND ([PublishedDate] = @PublishedDate OR @PublishedDate IS NULL)
	AND ([IsHome] = @IsHome OR @IsHome IS NULL)
	AND ([IsPublished] = @IsPublished OR @IsPublished IS NULL)
	AND ([VersionId] = @VersionId OR @VersionId IS NULL)
	AND ([Author] = @Author OR @Author IS NULL)
	AND ([EnableCaching] = @EnableCaching OR @EnableCaching IS NULL)
	AND ([EnableViewState] = @EnableViewState OR @EnableViewState IS NULL)
	AND ([Index] = @Index OR @Index IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [Id]
	, [DisplayOrder]
	, [PortalId]
	, [Name]
	, [IsVisible]
	, [ParentId]
	, [DisableLink]
	, [Title]
	, [Description]
	, [KeyWords]
	, [IsDeleted]
	, [Url]
	, [Type]
	, [SkinId]
	, [StartDate]
	, [EndDate]
	, [PublishedDate]
	, [IsHome]
	, [IsPublished]
	, [VersionId]
	, [Author]
	, [EnableCaching]
	, [EnableViewState]
	, [Index]
    FROM
	[dbo].[meanstream_Page]
    WHERE 
	 ([Id] = @Id AND @Id is not null)
	OR ([DisplayOrder] = @DisplayOrder AND @DisplayOrder is not null)
	OR ([PortalId] = @PortalId AND @PortalId is not null)
	OR ([Name] = @Name AND @Name is not null)
	OR ([IsVisible] = @IsVisible AND @IsVisible is not null)
	OR ([ParentId] = @ParentId AND @ParentId is not null)
	OR ([DisableLink] = @DisableLink AND @DisableLink is not null)
	OR ([Title] = @Title AND @Title is not null)
	OR ([Description] = @Description AND @Description is not null)
	OR ([KeyWords] = @KeyWords AND @KeyWords is not null)
	OR ([IsDeleted] = @IsDeleted AND @IsDeleted is not null)
	OR ([Url] = @Url AND @Url is not null)
	OR ([Type] = @Type AND @Type is not null)
	OR ([SkinId] = @SkinId AND @SkinId is not null)
	OR ([StartDate] = @StartDate AND @StartDate is not null)
	OR ([EndDate] = @EndDate AND @EndDate is not null)
	OR ([PublishedDate] = @PublishedDate AND @PublishedDate is not null)
	OR ([IsHome] = @IsHome AND @IsHome is not null)
	OR ([IsPublished] = @IsPublished AND @IsPublished is not null)
	OR ([VersionId] = @VersionId AND @VersionId is not null)
	OR ([Author] = @Author AND @Author is not null)
	OR ([EnableCaching] = @EnableCaching AND @EnableCaching is not null)
	OR ([EnableViewState] = @EnableViewState AND @EnableViewState is not null)
	OR ([Index] = @Index AND @Index is not null)
	SELECT @@ROWCOUNT			
  END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Page_Delete]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Deletes a record in the meanstream_Page table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Page_Delete]
(

	@Id uniqueidentifier   
)
AS


				DELETE FROM [dbo].[meanstream_Page] WITH (ROWLOCK) 
				WHERE
					[Id] = @Id
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ModuleVersionPermission_Update]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Updates a record in the meanstream_ModuleVersionPermission table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ModuleVersionPermission_Update]
(

	@Id uniqueidentifier   ,

	@OriginalId uniqueidentifier   ,

	@PageVersionId uniqueidentifier   ,

	@ModuleId uniqueidentifier   ,

	@PermissionId uniqueidentifier   ,

	@RoleId uniqueidentifier   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[meanstream_ModuleVersionPermission]
				SET
					[Id] = @Id
					,[PageVersionId] = @PageVersionId
					,[ModuleId] = @ModuleId
					,[PermissionId] = @PermissionId
					,[RoleId] = @RoleId
				WHERE
[Id] = @OriginalId
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ModuleVersionPermission_Insert]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Inserts a record into the meanstream_ModuleVersionPermission table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ModuleVersionPermission_Insert]
(

	@Id uniqueidentifier   ,

	@PageVersionId uniqueidentifier   ,

	@ModuleId uniqueidentifier   ,

	@PermissionId uniqueidentifier   ,

	@RoleId uniqueidentifier   
)
AS


				
				INSERT INTO [dbo].[meanstream_ModuleVersionPermission]
					(
					[Id]
					,[PageVersionId]
					,[ModuleId]
					,[PermissionId]
					,[RoleId]
					)
				VALUES
					(
					@Id
					,@PageVersionId
					,@ModuleId
					,@PermissionId
					,@RoleId
					)
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ModuleVersionPermission_GetPaged]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets records from the meanstream_ModuleVersionPermission table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ModuleVersionPermission_GetPaged]
(

	@WhereClause varchar (2000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [Id] uniqueidentifier 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([Id])'
				SET @SQL = @SQL + ' SELECT'
				IF @PageSize > 0
				BEGIN
					SET @SQL = @SQL + ' TOP ' + CONVERT(nvarchar, @PageUpperBound)
				END
				SET @SQL = @SQL + ' [Id]'
				SET @SQL = @SQL + ' FROM [dbo].[meanstream_ModuleVersionPermission]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Return paged results
				SELECT O.[Id], O.[PageVersionId], O.[ModuleId], O.[PermissionId], O.[RoleId]
				FROM
				    [dbo].[meanstream_ModuleVersionPermission] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[Id] = PageIndex.[Id]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[meanstream_ModuleVersionPermission]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ModuleVersionPermission_GetById]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Select records from the meanstream_ModuleVersionPermission table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ModuleVersionPermission_GetById]
(

	@Id uniqueidentifier   
)
AS


				SELECT
					[Id],
					[PageVersionId],
					[ModuleId],
					[PermissionId],
					[RoleId]
				FROM
					[dbo].[meanstream_ModuleVersionPermission]
				WHERE
					[Id] = @Id
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ModuleVersionPermission_Get_List]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets all records from the meanstream_ModuleVersionPermission table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ModuleVersionPermission_Get_List]

AS


				
				SELECT
					[Id],
					[PageVersionId],
					[ModuleId],
					[PermissionId],
					[RoleId]
				FROM
					[dbo].[meanstream_ModuleVersionPermission]
					
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ModuleVersionPermission_Find]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Finds records in the meanstream_ModuleVersionPermission table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ModuleVersionPermission_Find]
(

	@SearchUsingOR bit   = null ,

	@Id uniqueidentifier   = null ,

	@PageVersionId uniqueidentifier   = null ,

	@ModuleId uniqueidentifier   = null ,

	@PermissionId uniqueidentifier   = null ,

	@RoleId uniqueidentifier   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [Id]
	, [PageVersionId]
	, [ModuleId]
	, [PermissionId]
	, [RoleId]
    FROM
	[dbo].[meanstream_ModuleVersionPermission]
    WHERE 
	 ([Id] = @Id OR @Id IS NULL)
	AND ([PageVersionId] = @PageVersionId OR @PageVersionId IS NULL)
	AND ([ModuleId] = @ModuleId OR @ModuleId IS NULL)
	AND ([PermissionId] = @PermissionId OR @PermissionId IS NULL)
	AND ([RoleId] = @RoleId OR @RoleId IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [Id]
	, [PageVersionId]
	, [ModuleId]
	, [PermissionId]
	, [RoleId]
    FROM
	[dbo].[meanstream_ModuleVersionPermission]
    WHERE 
	 ([Id] = @Id AND @Id is not null)
	OR ([PageVersionId] = @PageVersionId AND @PageVersionId is not null)
	OR ([ModuleId] = @ModuleId AND @ModuleId is not null)
	OR ([PermissionId] = @PermissionId AND @PermissionId is not null)
	OR ([RoleId] = @RoleId AND @RoleId is not null)
	SELECT @@ROWCOUNT			
  END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ModuleVersionPermission_Delete]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Deletes a record in the meanstream_ModuleVersionPermission table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ModuleVersionPermission_Delete]
(

	@Id uniqueidentifier   
)
AS


				DELETE FROM [dbo].[meanstream_ModuleVersionPermission] WITH (ROWLOCK) 
				WHERE
					[Id] = @Id
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ModuleVersion_Update]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Updates a record in the meanstream_ModuleVersion table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ModuleVersion_Update]
(

	@Id uniqueidentifier   ,

	@OriginalId uniqueidentifier   ,

	@PageVersionId uniqueidentifier   ,

	@ModuleDefId uniqueidentifier   ,

	@Title nvarchar (255)  ,

	@AllPages bit   ,

	@IsDeleted bit   ,

	@CreatedBy nvarchar (100)  ,

	@StartDate datetime   ,

	@EndDate datetime   ,

	@SkinPaneId uniqueidentifier   ,

	@SharedId uniqueidentifier   ,

	@DisplayOrder int   ,

	@DeletedDate nvarchar (50)  ,

	@LastModifiedDate datetime   ,

	@LastModifiedBy nvarchar (100)  
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[meanstream_ModuleVersion]
				SET
					[Id] = @Id
					,[PageVersionId] = @PageVersionId
					,[ModuleDefId] = @ModuleDefId
					,[Title] = @Title
					,[AllPages] = @AllPages
					,[IsDeleted] = @IsDeleted
					,[CreatedBy] = @CreatedBy
					,[StartDate] = @StartDate
					,[EndDate] = @EndDate
					,[SkinPaneId] = @SkinPaneId
					,[SharedId] = @SharedId
					,[DisplayOrder] = @DisplayOrder
					,[DeletedDate] = @DeletedDate
					,[LastModifiedDate] = @LastModifiedDate
					,[LastModifiedBy] = @LastModifiedBy
				WHERE
[Id] = @OriginalId
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ModuleVersion_Insert]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Inserts a record into the meanstream_ModuleVersion table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ModuleVersion_Insert]
(

	@Id uniqueidentifier   ,

	@PageVersionId uniqueidentifier   ,

	@ModuleDefId uniqueidentifier   ,

	@Title nvarchar (255)  ,

	@AllPages bit   ,

	@IsDeleted bit   ,

	@CreatedBy nvarchar (100)  ,

	@StartDate datetime   ,

	@EndDate datetime   ,

	@SkinPaneId uniqueidentifier   ,

	@SharedId uniqueidentifier   ,

	@DisplayOrder int   ,

	@DeletedDate nvarchar (50)  ,

	@LastModifiedDate datetime   ,

	@LastModifiedBy nvarchar (100)  
)
AS


				
				INSERT INTO [dbo].[meanstream_ModuleVersion]
					(
					[Id]
					,[PageVersionId]
					,[ModuleDefId]
					,[Title]
					,[AllPages]
					,[IsDeleted]
					,[CreatedBy]
					,[StartDate]
					,[EndDate]
					,[SkinPaneId]
					,[SharedId]
					,[DisplayOrder]
					,[DeletedDate]
					,[LastModifiedDate]
					,[LastModifiedBy]
					)
				VALUES
					(
					@Id
					,@PageVersionId
					,@ModuleDefId
					,@Title
					,@AllPages
					,@IsDeleted
					,@CreatedBy
					,@StartDate
					,@EndDate
					,@SkinPaneId
					,@SharedId
					,@DisplayOrder
					,@DeletedDate
					,@LastModifiedDate
					,@LastModifiedBy
					)
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ModuleVersion_GetPaged]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets records from the meanstream_ModuleVersion table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ModuleVersion_GetPaged]
(

	@WhereClause varchar (2000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [Id] uniqueidentifier 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([Id])'
				SET @SQL = @SQL + ' SELECT'
				IF @PageSize > 0
				BEGIN
					SET @SQL = @SQL + ' TOP ' + CONVERT(nvarchar, @PageUpperBound)
				END
				SET @SQL = @SQL + ' [Id]'
				SET @SQL = @SQL + ' FROM [dbo].[meanstream_ModuleVersion]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Return paged results
				SELECT O.[Id], O.[PageVersionId], O.[ModuleDefId], O.[Title], O.[AllPages], O.[IsDeleted], O.[CreatedBy], O.[StartDate], O.[EndDate], O.[SkinPaneId], O.[SharedId], O.[DisplayOrder], O.[DeletedDate], O.[LastModifiedDate], O.[LastModifiedBy]
				FROM
				    [dbo].[meanstream_ModuleVersion] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[Id] = PageIndex.[Id]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[meanstream_ModuleVersion]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ModuleVersion_GetById]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Select records from the meanstream_ModuleVersion table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ModuleVersion_GetById]
(

	@Id uniqueidentifier   
)
AS


				SELECT
					[Id],
					[PageVersionId],
					[ModuleDefId],
					[Title],
					[AllPages],
					[IsDeleted],
					[CreatedBy],
					[StartDate],
					[EndDate],
					[SkinPaneId],
					[SharedId],
					[DisplayOrder],
					[DeletedDate],
					[LastModifiedDate],
					[LastModifiedBy]
				FROM
					[dbo].[meanstream_ModuleVersion]
				WHERE
					[Id] = @Id
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ModuleVersion_Get_List]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets all records from the meanstream_ModuleVersion table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ModuleVersion_Get_List]

AS


				
				SELECT
					[Id],
					[PageVersionId],
					[ModuleDefId],
					[Title],
					[AllPages],
					[IsDeleted],
					[CreatedBy],
					[StartDate],
					[EndDate],
					[SkinPaneId],
					[SharedId],
					[DisplayOrder],
					[DeletedDate],
					[LastModifiedDate],
					[LastModifiedBy]
				FROM
					[dbo].[meanstream_ModuleVersion]
					
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ModuleVersion_Find]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Finds records in the meanstream_ModuleVersion table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ModuleVersion_Find]
(

	@SearchUsingOR bit   = null ,

	@Id uniqueidentifier   = null ,

	@PageVersionId uniqueidentifier   = null ,

	@ModuleDefId uniqueidentifier   = null ,

	@Title nvarchar (255)  = null ,

	@AllPages bit   = null ,

	@IsDeleted bit   = null ,

	@CreatedBy nvarchar (100)  = null ,

	@StartDate datetime   = null ,

	@EndDate datetime   = null ,

	@SkinPaneId uniqueidentifier   = null ,

	@SharedId uniqueidentifier   = null ,

	@DisplayOrder int   = null ,

	@DeletedDate nvarchar (50)  = null ,

	@LastModifiedDate datetime   = null ,

	@LastModifiedBy nvarchar (100)  = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [Id]
	, [PageVersionId]
	, [ModuleDefId]
	, [Title]
	, [AllPages]
	, [IsDeleted]
	, [CreatedBy]
	, [StartDate]
	, [EndDate]
	, [SkinPaneId]
	, [SharedId]
	, [DisplayOrder]
	, [DeletedDate]
	, [LastModifiedDate]
	, [LastModifiedBy]
    FROM
	[dbo].[meanstream_ModuleVersion]
    WHERE 
	 ([Id] = @Id OR @Id IS NULL)
	AND ([PageVersionId] = @PageVersionId OR @PageVersionId IS NULL)
	AND ([ModuleDefId] = @ModuleDefId OR @ModuleDefId IS NULL)
	AND ([Title] = @Title OR @Title IS NULL)
	AND ([AllPages] = @AllPages OR @AllPages IS NULL)
	AND ([IsDeleted] = @IsDeleted OR @IsDeleted IS NULL)
	AND ([CreatedBy] = @CreatedBy OR @CreatedBy IS NULL)
	AND ([StartDate] = @StartDate OR @StartDate IS NULL)
	AND ([EndDate] = @EndDate OR @EndDate IS NULL)
	AND ([SkinPaneId] = @SkinPaneId OR @SkinPaneId IS NULL)
	AND ([SharedId] = @SharedId OR @SharedId IS NULL)
	AND ([DisplayOrder] = @DisplayOrder OR @DisplayOrder IS NULL)
	AND ([DeletedDate] = @DeletedDate OR @DeletedDate IS NULL)
	AND ([LastModifiedDate] = @LastModifiedDate OR @LastModifiedDate IS NULL)
	AND ([LastModifiedBy] = @LastModifiedBy OR @LastModifiedBy IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [Id]
	, [PageVersionId]
	, [ModuleDefId]
	, [Title]
	, [AllPages]
	, [IsDeleted]
	, [CreatedBy]
	, [StartDate]
	, [EndDate]
	, [SkinPaneId]
	, [SharedId]
	, [DisplayOrder]
	, [DeletedDate]
	, [LastModifiedDate]
	, [LastModifiedBy]
    FROM
	[dbo].[meanstream_ModuleVersion]
    WHERE 
	 ([Id] = @Id AND @Id is not null)
	OR ([PageVersionId] = @PageVersionId AND @PageVersionId is not null)
	OR ([ModuleDefId] = @ModuleDefId AND @ModuleDefId is not null)
	OR ([Title] = @Title AND @Title is not null)
	OR ([AllPages] = @AllPages AND @AllPages is not null)
	OR ([IsDeleted] = @IsDeleted AND @IsDeleted is not null)
	OR ([CreatedBy] = @CreatedBy AND @CreatedBy is not null)
	OR ([StartDate] = @StartDate AND @StartDate is not null)
	OR ([EndDate] = @EndDate AND @EndDate is not null)
	OR ([SkinPaneId] = @SkinPaneId AND @SkinPaneId is not null)
	OR ([SharedId] = @SharedId AND @SharedId is not null)
	OR ([DisplayOrder] = @DisplayOrder AND @DisplayOrder is not null)
	OR ([DeletedDate] = @DeletedDate AND @DeletedDate is not null)
	OR ([LastModifiedDate] = @LastModifiedDate AND @LastModifiedDate is not null)
	OR ([LastModifiedBy] = @LastModifiedBy AND @LastModifiedBy is not null)
	SELECT @@ROWCOUNT			
  END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ModuleVersion_Delete]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Deletes a record in the meanstream_ModuleVersion table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ModuleVersion_Delete]
(

	@Id uniqueidentifier   
)
AS


				DELETE FROM [dbo].[meanstream_ModuleVersion] WITH (ROWLOCK) 
				WHERE
					[Id] = @Id
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ModuleDefinitions_Update]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Updates a record in the meanstream_ModuleDefinitions table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ModuleDefinitions_Update]
(

	@Id uniqueidentifier   ,

	@OriginalId uniqueidentifier   ,

	@FriendlyName nvarchar (128)  ,

	@Enabled bit   ,

	@IsDefault bit   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[meanstream_ModuleDefinitions]
				SET
					[Id] = @Id
					,[FriendlyName] = @FriendlyName
					,[Enabled] = @Enabled
					,[IsDefault] = @IsDefault
				WHERE
[Id] = @OriginalId
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ModuleDefinitions_Insert]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Inserts a record into the meanstream_ModuleDefinitions table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ModuleDefinitions_Insert]
(

	@Id uniqueidentifier   ,

	@FriendlyName nvarchar (128)  ,

	@Enabled bit   ,

	@IsDefault bit   
)
AS


				
				INSERT INTO [dbo].[meanstream_ModuleDefinitions]
					(
					[Id]
					,[FriendlyName]
					,[Enabled]
					,[IsDefault]
					)
				VALUES
					(
					@Id
					,@FriendlyName
					,@Enabled
					,@IsDefault
					)
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ModuleDefinitions_GetPaged]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets records from the meanstream_ModuleDefinitions table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ModuleDefinitions_GetPaged]
(

	@WhereClause varchar (2000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [Id] uniqueidentifier 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([Id])'
				SET @SQL = @SQL + ' SELECT'
				IF @PageSize > 0
				BEGIN
					SET @SQL = @SQL + ' TOP ' + CONVERT(nvarchar, @PageUpperBound)
				END
				SET @SQL = @SQL + ' [Id]'
				SET @SQL = @SQL + ' FROM [dbo].[meanstream_ModuleDefinitions]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Return paged results
				SELECT O.[Id], O.[FriendlyName], O.[Enabled], O.[IsDefault]
				FROM
				    [dbo].[meanstream_ModuleDefinitions] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[Id] = PageIndex.[Id]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[meanstream_ModuleDefinitions]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ModuleDefinitions_GetById]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Select records from the meanstream_ModuleDefinitions table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ModuleDefinitions_GetById]
(

	@Id uniqueidentifier   
)
AS


				SELECT
					[Id],
					[FriendlyName],
					[Enabled],
					[IsDefault]
				FROM
					[dbo].[meanstream_ModuleDefinitions]
				WHERE
					[Id] = @Id
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ModuleDefinitions_Get_List]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets all records from the meanstream_ModuleDefinitions table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ModuleDefinitions_Get_List]

AS


				
				SELECT
					[Id],
					[FriendlyName],
					[Enabled],
					[IsDefault]
				FROM
					[dbo].[meanstream_ModuleDefinitions]
					
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ModuleDefinitions_Find]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Finds records in the meanstream_ModuleDefinitions table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ModuleDefinitions_Find]
(

	@SearchUsingOR bit   = null ,

	@Id uniqueidentifier   = null ,

	@FriendlyName nvarchar (128)  = null ,

	@Enabled bit   = null ,

	@IsDefault bit   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [Id]
	, [FriendlyName]
	, [Enabled]
	, [IsDefault]
    FROM
	[dbo].[meanstream_ModuleDefinitions]
    WHERE 
	 ([Id] = @Id OR @Id IS NULL)
	AND ([FriendlyName] = @FriendlyName OR @FriendlyName IS NULL)
	AND ([Enabled] = @Enabled OR @Enabled IS NULL)
	AND ([IsDefault] = @IsDefault OR @IsDefault IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [Id]
	, [FriendlyName]
	, [Enabled]
	, [IsDefault]
    FROM
	[dbo].[meanstream_ModuleDefinitions]
    WHERE 
	 ([Id] = @Id AND @Id is not null)
	OR ([FriendlyName] = @FriendlyName AND @FriendlyName is not null)
	OR ([Enabled] = @Enabled AND @Enabled is not null)
	OR ([IsDefault] = @IsDefault AND @IsDefault is not null)
	SELECT @@ROWCOUNT			
  END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ModuleDefinitions_Delete]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Deletes a record in the meanstream_ModuleDefinitions table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ModuleDefinitions_Delete]
(

	@Id uniqueidentifier   
)
AS


				DELETE FROM [dbo].[meanstream_ModuleDefinitions] WITH (ROWLOCK) 
				WHERE
					[Id] = @Id
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ModulePermission_Update]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Updates a record in the meanstream_ModulePermission table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ModulePermission_Update]
(

	@Id uniqueidentifier   ,

	@OriginalId uniqueidentifier   ,

	@ModuleId uniqueidentifier   ,

	@PermissionId uniqueidentifier   ,

	@RoleId uniqueidentifier   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[meanstream_ModulePermission]
				SET
					[Id] = @Id
					,[ModuleId] = @ModuleId
					,[PermissionId] = @PermissionId
					,[RoleId] = @RoleId
				WHERE
[Id] = @OriginalId
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ModulePermission_Insert]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Inserts a record into the meanstream_ModulePermission table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ModulePermission_Insert]
(

	@Id uniqueidentifier   ,

	@ModuleId uniqueidentifier   ,

	@PermissionId uniqueidentifier   ,

	@RoleId uniqueidentifier   
)
AS


				
				INSERT INTO [dbo].[meanstream_ModulePermission]
					(
					[Id]
					,[ModuleId]
					,[PermissionId]
					,[RoleId]
					)
				VALUES
					(
					@Id
					,@ModuleId
					,@PermissionId
					,@RoleId
					)
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ModulePermission_GetPaged]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets records from the meanstream_ModulePermission table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ModulePermission_GetPaged]
(

	@WhereClause varchar (2000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [Id] uniqueidentifier 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([Id])'
				SET @SQL = @SQL + ' SELECT'
				IF @PageSize > 0
				BEGIN
					SET @SQL = @SQL + ' TOP ' + CONVERT(nvarchar, @PageUpperBound)
				END
				SET @SQL = @SQL + ' [Id]'
				SET @SQL = @SQL + ' FROM [dbo].[meanstream_ModulePermission]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Return paged results
				SELECT O.[Id], O.[ModuleId], O.[PermissionId], O.[RoleId]
				FROM
				    [dbo].[meanstream_ModulePermission] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[Id] = PageIndex.[Id]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[meanstream_ModulePermission]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ModulePermission_GetById]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Select records from the meanstream_ModulePermission table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ModulePermission_GetById]
(

	@Id uniqueidentifier   
)
AS


				SELECT
					[Id],
					[ModuleId],
					[PermissionId],
					[RoleId]
				FROM
					[dbo].[meanstream_ModulePermission]
				WHERE
					[Id] = @Id
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ModulePermission_Get_List]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets all records from the meanstream_ModulePermission table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ModulePermission_Get_List]

AS


				
				SELECT
					[Id],
					[ModuleId],
					[PermissionId],
					[RoleId]
				FROM
					[dbo].[meanstream_ModulePermission]
					
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ModulePermission_Find]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Finds records in the meanstream_ModulePermission table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ModulePermission_Find]
(

	@SearchUsingOR bit   = null ,

	@Id uniqueidentifier   = null ,

	@ModuleId uniqueidentifier   = null ,

	@PermissionId uniqueidentifier   = null ,

	@RoleId uniqueidentifier   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [Id]
	, [ModuleId]
	, [PermissionId]
	, [RoleId]
    FROM
	[dbo].[meanstream_ModulePermission]
    WHERE 
	 ([Id] = @Id OR @Id IS NULL)
	AND ([ModuleId] = @ModuleId OR @ModuleId IS NULL)
	AND ([PermissionId] = @PermissionId OR @PermissionId IS NULL)
	AND ([RoleId] = @RoleId OR @RoleId IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [Id]
	, [ModuleId]
	, [PermissionId]
	, [RoleId]
    FROM
	[dbo].[meanstream_ModulePermission]
    WHERE 
	 ([Id] = @Id AND @Id is not null)
	OR ([ModuleId] = @ModuleId AND @ModuleId is not null)
	OR ([PermissionId] = @PermissionId AND @PermissionId is not null)
	OR ([RoleId] = @RoleId AND @RoleId is not null)
	SELECT @@ROWCOUNT			
  END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ModulePermission_Delete]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Deletes a record in the meanstream_ModulePermission table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ModulePermission_Delete]
(

	@Id uniqueidentifier   
)
AS


				DELETE FROM [dbo].[meanstream_ModulePermission] WITH (ROWLOCK) 
				WHERE
					[Id] = @Id
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ModuleControls_Update]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Updates a record in the meanstream_ModuleControls table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ModuleControls_Update]
(

	@Id uniqueidentifier   ,

	@OriginalId uniqueidentifier   ,

	@ModuleDefId uniqueidentifier   ,

	@ControlKey nvarchar (50)  ,

	@ControlPath nvarchar (256)  ,

	@ControlTitle nvarchar (50)  
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[meanstream_ModuleControls]
				SET
					[Id] = @Id
					,[ModuleDefId] = @ModuleDefId
					,[ControlKey] = @ControlKey
					,[ControlPath] = @ControlPath
					,[ControlTitle] = @ControlTitle
				WHERE
[Id] = @OriginalId
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ModuleControls_Insert]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Inserts a record into the meanstream_ModuleControls table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ModuleControls_Insert]
(

	@Id uniqueidentifier   ,

	@ModuleDefId uniqueidentifier   ,

	@ControlKey nvarchar (50)  ,

	@ControlPath nvarchar (256)  ,

	@ControlTitle nvarchar (50)  
)
AS


				
				INSERT INTO [dbo].[meanstream_ModuleControls]
					(
					[Id]
					,[ModuleDefId]
					,[ControlKey]
					,[ControlPath]
					,[ControlTitle]
					)
				VALUES
					(
					@Id
					,@ModuleDefId
					,@ControlKey
					,@ControlPath
					,@ControlTitle
					)
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ModuleControls_GetPaged]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets records from the meanstream_ModuleControls table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ModuleControls_GetPaged]
(

	@WhereClause varchar (2000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [Id] uniqueidentifier 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([Id])'
				SET @SQL = @SQL + ' SELECT'
				IF @PageSize > 0
				BEGIN
					SET @SQL = @SQL + ' TOP ' + CONVERT(nvarchar, @PageUpperBound)
				END
				SET @SQL = @SQL + ' [Id]'
				SET @SQL = @SQL + ' FROM [dbo].[meanstream_ModuleControls]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Return paged results
				SELECT O.[Id], O.[ModuleDefId], O.[ControlKey], O.[ControlPath], O.[ControlTitle]
				FROM
				    [dbo].[meanstream_ModuleControls] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[Id] = PageIndex.[Id]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[meanstream_ModuleControls]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ModuleControls_GetById]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Select records from the meanstream_ModuleControls table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ModuleControls_GetById]
(

	@Id uniqueidentifier   
)
AS


				SELECT
					[Id],
					[ModuleDefId],
					[ControlKey],
					[ControlPath],
					[ControlTitle]
				FROM
					[dbo].[meanstream_ModuleControls]
				WHERE
					[Id] = @Id
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ModuleControls_Get_List]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets all records from the meanstream_ModuleControls table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ModuleControls_Get_List]

AS


				
				SELECT
					[Id],
					[ModuleDefId],
					[ControlKey],
					[ControlPath],
					[ControlTitle]
				FROM
					[dbo].[meanstream_ModuleControls]
					
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ModuleControls_Find]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Finds records in the meanstream_ModuleControls table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ModuleControls_Find]
(

	@SearchUsingOR bit   = null ,

	@Id uniqueidentifier   = null ,

	@ModuleDefId uniqueidentifier   = null ,

	@ControlKey nvarchar (50)  = null ,

	@ControlPath nvarchar (256)  = null ,

	@ControlTitle nvarchar (50)  = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [Id]
	, [ModuleDefId]
	, [ControlKey]
	, [ControlPath]
	, [ControlTitle]
    FROM
	[dbo].[meanstream_ModuleControls]
    WHERE 
	 ([Id] = @Id OR @Id IS NULL)
	AND ([ModuleDefId] = @ModuleDefId OR @ModuleDefId IS NULL)
	AND ([ControlKey] = @ControlKey OR @ControlKey IS NULL)
	AND ([ControlPath] = @ControlPath OR @ControlPath IS NULL)
	AND ([ControlTitle] = @ControlTitle OR @ControlTitle IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [Id]
	, [ModuleDefId]
	, [ControlKey]
	, [ControlPath]
	, [ControlTitle]
    FROM
	[dbo].[meanstream_ModuleControls]
    WHERE 
	 ([Id] = @Id AND @Id is not null)
	OR ([ModuleDefId] = @ModuleDefId AND @ModuleDefId is not null)
	OR ([ControlKey] = @ControlKey AND @ControlKey is not null)
	OR ([ControlPath] = @ControlPath AND @ControlPath is not null)
	OR ([ControlTitle] = @ControlTitle AND @ControlTitle is not null)
	SELECT @@ROWCOUNT			
  END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ModuleControls_Delete]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Deletes a record in the meanstream_ModuleControls table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ModuleControls_Delete]
(

	@Id uniqueidentifier   
)
AS


				DELETE FROM [dbo].[meanstream_ModuleControls] WITH (ROWLOCK) 
				WHERE
					[Id] = @Id
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Module_Update]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Updates a record in the meanstream_Module table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Module_Update]
(

	@Id uniqueidentifier   ,

	@OriginalId uniqueidentifier   ,

	@ModuleDefId uniqueidentifier   ,

	@Title nvarchar (256)  ,

	@AllPages bit   ,

	@IsDeleted bit   ,

	@CreatedBy nvarchar (100)  ,

	@StartDate datetime   ,

	@EndDate datetime   ,

	@SkinPaneId uniqueidentifier   ,

	@DisplayOrder int   ,

	@PageId uniqueidentifier   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[meanstream_Module]
				SET
					[Id] = @Id
					,[ModuleDefId] = @ModuleDefId
					,[Title] = @Title
					,[AllPages] = @AllPages
					,[IsDeleted] = @IsDeleted
					,[CreatedBy] = @CreatedBy
					,[StartDate] = @StartDate
					,[EndDate] = @EndDate
					,[SkinPaneId] = @SkinPaneId
					,[DisplayOrder] = @DisplayOrder
					,[PageId] = @PageId
				WHERE
[Id] = @OriginalId
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Module_Insert]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Inserts a record into the meanstream_Module table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Module_Insert]
(

	@Id uniqueidentifier   ,

	@ModuleDefId uniqueidentifier   ,

	@Title nvarchar (256)  ,

	@AllPages bit   ,

	@IsDeleted bit   ,

	@CreatedBy nvarchar (100)  ,

	@StartDate datetime   ,

	@EndDate datetime   ,

	@SkinPaneId uniqueidentifier   ,

	@DisplayOrder int   ,

	@PageId uniqueidentifier   
)
AS


				
				INSERT INTO [dbo].[meanstream_Module]
					(
					[Id]
					,[ModuleDefId]
					,[Title]
					,[AllPages]
					,[IsDeleted]
					,[CreatedBy]
					,[StartDate]
					,[EndDate]
					,[SkinPaneId]
					,[DisplayOrder]
					,[PageId]
					)
				VALUES
					(
					@Id
					,@ModuleDefId
					,@Title
					,@AllPages
					,@IsDeleted
					,@CreatedBy
					,@StartDate
					,@EndDate
					,@SkinPaneId
					,@DisplayOrder
					,@PageId
					)
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Module_GetPaged]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets records from the meanstream_Module table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Module_GetPaged]
(

	@WhereClause varchar (2000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [Id] uniqueidentifier 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([Id])'
				SET @SQL = @SQL + ' SELECT'
				IF @PageSize > 0
				BEGIN
					SET @SQL = @SQL + ' TOP ' + CONVERT(nvarchar, @PageUpperBound)
				END
				SET @SQL = @SQL + ' [Id]'
				SET @SQL = @SQL + ' FROM [dbo].[meanstream_Module]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Return paged results
				SELECT O.[Id], O.[ModuleDefId], O.[Title], O.[AllPages], O.[IsDeleted], O.[CreatedBy], O.[StartDate], O.[EndDate], O.[SkinPaneId], O.[DisplayOrder], O.[PageId]
				FROM
				    [dbo].[meanstream_Module] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[Id] = PageIndex.[Id]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[meanstream_Module]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Module_GetById]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Select records from the meanstream_Module table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Module_GetById]
(

	@Id uniqueidentifier   
)
AS


				SELECT
					[Id],
					[ModuleDefId],
					[Title],
					[AllPages],
					[IsDeleted],
					[CreatedBy],
					[StartDate],
					[EndDate],
					[SkinPaneId],
					[DisplayOrder],
					[PageId]
				FROM
					[dbo].[meanstream_Module]
				WHERE
					[Id] = @Id
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Module_Get_List]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets all records from the meanstream_Module table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Module_Get_List]

AS


				
				SELECT
					[Id],
					[ModuleDefId],
					[Title],
					[AllPages],
					[IsDeleted],
					[CreatedBy],
					[StartDate],
					[EndDate],
					[SkinPaneId],
					[DisplayOrder],
					[PageId]
				FROM
					[dbo].[meanstream_Module]
					
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Module_Find]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Finds records in the meanstream_Module table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Module_Find]
(

	@SearchUsingOR bit   = null ,

	@Id uniqueidentifier   = null ,

	@ModuleDefId uniqueidentifier   = null ,

	@Title nvarchar (256)  = null ,

	@AllPages bit   = null ,

	@IsDeleted bit   = null ,

	@CreatedBy nvarchar (100)  = null ,

	@StartDate datetime   = null ,

	@EndDate datetime   = null ,

	@SkinPaneId uniqueidentifier   = null ,

	@DisplayOrder int   = null ,

	@PageId uniqueidentifier   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [Id]
	, [ModuleDefId]
	, [Title]
	, [AllPages]
	, [IsDeleted]
	, [CreatedBy]
	, [StartDate]
	, [EndDate]
	, [SkinPaneId]
	, [DisplayOrder]
	, [PageId]
    FROM
	[dbo].[meanstream_Module]
    WHERE 
	 ([Id] = @Id OR @Id IS NULL)
	AND ([ModuleDefId] = @ModuleDefId OR @ModuleDefId IS NULL)
	AND ([Title] = @Title OR @Title IS NULL)
	AND ([AllPages] = @AllPages OR @AllPages IS NULL)
	AND ([IsDeleted] = @IsDeleted OR @IsDeleted IS NULL)
	AND ([CreatedBy] = @CreatedBy OR @CreatedBy IS NULL)
	AND ([StartDate] = @StartDate OR @StartDate IS NULL)
	AND ([EndDate] = @EndDate OR @EndDate IS NULL)
	AND ([SkinPaneId] = @SkinPaneId OR @SkinPaneId IS NULL)
	AND ([DisplayOrder] = @DisplayOrder OR @DisplayOrder IS NULL)
	AND ([PageId] = @PageId OR @PageId IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [Id]
	, [ModuleDefId]
	, [Title]
	, [AllPages]
	, [IsDeleted]
	, [CreatedBy]
	, [StartDate]
	, [EndDate]
	, [SkinPaneId]
	, [DisplayOrder]
	, [PageId]
    FROM
	[dbo].[meanstream_Module]
    WHERE 
	 ([Id] = @Id AND @Id is not null)
	OR ([ModuleDefId] = @ModuleDefId AND @ModuleDefId is not null)
	OR ([Title] = @Title AND @Title is not null)
	OR ([AllPages] = @AllPages AND @AllPages is not null)
	OR ([IsDeleted] = @IsDeleted AND @IsDeleted is not null)
	OR ([CreatedBy] = @CreatedBy AND @CreatedBy is not null)
	OR ([StartDate] = @StartDate AND @StartDate is not null)
	OR ([EndDate] = @EndDate AND @EndDate is not null)
	OR ([SkinPaneId] = @SkinPaneId AND @SkinPaneId is not null)
	OR ([DisplayOrder] = @DisplayOrder AND @DisplayOrder is not null)
	OR ([PageId] = @PageId AND @PageId is not null)
	SELECT @@ROWCOUNT			
  END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Module_Delete]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Deletes a record in the meanstream_Module table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Module_Delete]
(

	@Id uniqueidentifier   
)
AS


				DELETE FROM [dbo].[meanstream_Module] WITH (ROWLOCK) 
				WHERE
					[Id] = @Id
GO
/****** Object:  StoredProcedure [dbo].[meanstream_MessagingMessageType_Update]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Updates a record in the meanstream_MessagingMessageType table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_MessagingMessageType_Update]
(

	@MessageType nvarchar (50)  ,

	@OriginalMessageType nvarchar (50)  
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[meanstream_MessagingMessageType]
				SET
					[MessageType] = @MessageType
				WHERE
[MessageType] = @OriginalMessageType
GO
/****** Object:  StoredProcedure [dbo].[meanstream_MessagingMessageType_Insert]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Inserts a record into the meanstream_MessagingMessageType table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_MessagingMessageType_Insert]
(

	@MessageType nvarchar (50)  
)
AS


				
				INSERT INTO [dbo].[meanstream_MessagingMessageType]
					(
					[MessageType]
					)
				VALUES
					(
					@MessageType
					)
GO
/****** Object:  StoredProcedure [dbo].[meanstream_MessagingMessageType_GetPaged]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets records from the meanstream_MessagingMessageType table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_MessagingMessageType_GetPaged]
(

	@WhereClause varchar (2000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [MessageType] nvarchar(50) COLLATE database_default  
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([MessageType])'
				SET @SQL = @SQL + ' SELECT'
				IF @PageSize > 0
				BEGIN
					SET @SQL = @SQL + ' TOP ' + CONVERT(nvarchar, @PageUpperBound)
				END
				SET @SQL = @SQL + ' [MessageType]'
				SET @SQL = @SQL + ' FROM [dbo].[meanstream_MessagingMessageType]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Return paged results
				SELECT O.[MessageType]
				FROM
				    [dbo].[meanstream_MessagingMessageType] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[MessageType] = PageIndex.[MessageType]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[meanstream_MessagingMessageType]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_MessagingMessageType_GetByMessageType]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Select records from the meanstream_MessagingMessageType table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_MessagingMessageType_GetByMessageType]
(

	@MessageType nvarchar (50)  
)
AS


				SELECT
					[MessageType]
				FROM
					[dbo].[meanstream_MessagingMessageType]
				WHERE
					[MessageType] = @MessageType
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[meanstream_MessagingMessageType_Get_List]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets all records from the meanstream_MessagingMessageType table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_MessagingMessageType_Get_List]

AS


				
				SELECT
					[MessageType]
				FROM
					[dbo].[meanstream_MessagingMessageType]
					
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[meanstream_MessagingMessageType_Find]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Finds records in the meanstream_MessagingMessageType table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_MessagingMessageType_Find]
(

	@SearchUsingOR bit   = null ,

	@MessageType nvarchar (50)  = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [MessageType]
    FROM
	[dbo].[meanstream_MessagingMessageType]
    WHERE 
	 ([MessageType] = @MessageType OR @MessageType IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [MessageType]
    FROM
	[dbo].[meanstream_MessagingMessageType]
    WHERE 
	 ([MessageType] = @MessageType AND @MessageType is not null)
	SELECT @@ROWCOUNT			
  END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_MessagingMessageType_Delete]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Deletes a record in the meanstream_MessagingMessageType table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_MessagingMessageType_Delete]
(

	@MessageType nvarchar (50)  
)
AS


				DELETE FROM [dbo].[meanstream_MessagingMessageType] WITH (ROWLOCK) 
				WHERE
					[MessageType] = @MessageType
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Messaging_Update]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Updates a record in the meanstream_Messaging table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Messaging_Update]
(

	@Id uniqueidentifier   ,

	@OriginalId uniqueidentifier   ,

	@MessageType nvarchar (50)  ,

	@Sender uniqueidentifier   ,

	@Recipient uniqueidentifier   ,

	@Subject nvarchar (500)  ,

	@Body nvarchar (MAX)  ,

	@SentOn datetime   ,

	@ReceivedOn datetime   ,

	@Status nvarchar (50)  ,

	@IsQueued bit   ,

	@OpenedOn datetime   ,

	@Opened bit   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[meanstream_Messaging]
				SET
					[Id] = @Id
					,[MessageType] = @MessageType
					,[Sender] = @Sender
					,[Recipient] = @Recipient
					,[Subject] = @Subject
					,[Body] = @Body
					,[SentOn] = @SentOn
					,[ReceivedOn] = @ReceivedOn
					,[Status] = @Status
					,[IsQueued] = @IsQueued
					,[OpenedOn] = @OpenedOn
					,[Opened] = @Opened
				WHERE
[Id] = @OriginalId
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Messaging_Insert]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Inserts a record into the meanstream_Messaging table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Messaging_Insert]
(

	@Id uniqueidentifier   ,

	@MessageType nvarchar (50)  ,

	@Sender uniqueidentifier   ,

	@Recipient uniqueidentifier   ,

	@Subject nvarchar (500)  ,

	@Body nvarchar (MAX)  ,

	@SentOn datetime   ,

	@ReceivedOn datetime   ,

	@Status nvarchar (50)  ,

	@IsQueued bit   ,

	@OpenedOn datetime   ,

	@Opened bit   
)
AS


				
				INSERT INTO [dbo].[meanstream_Messaging]
					(
					[Id]
					,[MessageType]
					,[Sender]
					,[Recipient]
					,[Subject]
					,[Body]
					,[SentOn]
					,[ReceivedOn]
					,[Status]
					,[IsQueued]
					,[OpenedOn]
					,[Opened]
					)
				VALUES
					(
					@Id
					,@MessageType
					,@Sender
					,@Recipient
					,@Subject
					,@Body
					,@SentOn
					,@ReceivedOn
					,@Status
					,@IsQueued
					,@OpenedOn
					,@Opened
					)
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Messaging_GetPaged]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets records from the meanstream_Messaging table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Messaging_GetPaged]
(

	@WhereClause varchar (2000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [Id] uniqueidentifier 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([Id])'
				SET @SQL = @SQL + ' SELECT'
				IF @PageSize > 0
				BEGIN
					SET @SQL = @SQL + ' TOP ' + CONVERT(nvarchar, @PageUpperBound)
				END
				SET @SQL = @SQL + ' [Id]'
				SET @SQL = @SQL + ' FROM [dbo].[meanstream_Messaging]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Return paged results
				SELECT O.[Id], O.[MessageType], O.[Sender], O.[Recipient], O.[Subject], O.[Body], O.[SentOn], O.[ReceivedOn], O.[Status], O.[IsQueued], O.[OpenedOn], O.[Opened]
				FROM
				    [dbo].[meanstream_Messaging] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[Id] = PageIndex.[Id]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[meanstream_Messaging]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Messaging_GetById]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Select records from the meanstream_Messaging table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Messaging_GetById]
(

	@Id uniqueidentifier   
)
AS


				SELECT
					[Id],
					[MessageType],
					[Sender],
					[Recipient],
					[Subject],
					[Body],
					[SentOn],
					[ReceivedOn],
					[Status],
					[IsQueued],
					[OpenedOn],
					[Opened]
				FROM
					[dbo].[meanstream_Messaging]
				WHERE
					[Id] = @Id
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Messaging_Get_List]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets all records from the meanstream_Messaging table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Messaging_Get_List]

AS


				
				SELECT
					[Id],
					[MessageType],
					[Sender],
					[Recipient],
					[Subject],
					[Body],
					[SentOn],
					[ReceivedOn],
					[Status],
					[IsQueued],
					[OpenedOn],
					[Opened]
				FROM
					[dbo].[meanstream_Messaging]
					
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Messaging_Find]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Finds records in the meanstream_Messaging table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Messaging_Find]
(

	@SearchUsingOR bit   = null ,

	@Id uniqueidentifier   = null ,

	@MessageType nvarchar (50)  = null ,

	@Sender uniqueidentifier   = null ,

	@Recipient uniqueidentifier   = null ,

	@Subject nvarchar (500)  = null ,

	@Body nvarchar (MAX)  = null ,

	@SentOn datetime   = null ,

	@ReceivedOn datetime   = null ,

	@Status nvarchar (50)  = null ,

	@IsQueued bit   = null ,

	@OpenedOn datetime   = null ,

	@Opened bit   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [Id]
	, [MessageType]
	, [Sender]
	, [Recipient]
	, [Subject]
	, [Body]
	, [SentOn]
	, [ReceivedOn]
	, [Status]
	, [IsQueued]
	, [OpenedOn]
	, [Opened]
    FROM
	[dbo].[meanstream_Messaging]
    WHERE 
	 ([Id] = @Id OR @Id IS NULL)
	AND ([MessageType] = @MessageType OR @MessageType IS NULL)
	AND ([Sender] = @Sender OR @Sender IS NULL)
	AND ([Recipient] = @Recipient OR @Recipient IS NULL)
	AND ([Subject] = @Subject OR @Subject IS NULL)
	AND ([Body] = @Body OR @Body IS NULL)
	AND ([SentOn] = @SentOn OR @SentOn IS NULL)
	AND ([ReceivedOn] = @ReceivedOn OR @ReceivedOn IS NULL)
	AND ([Status] = @Status OR @Status IS NULL)
	AND ([IsQueued] = @IsQueued OR @IsQueued IS NULL)
	AND ([OpenedOn] = @OpenedOn OR @OpenedOn IS NULL)
	AND ([Opened] = @Opened OR @Opened IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [Id]
	, [MessageType]
	, [Sender]
	, [Recipient]
	, [Subject]
	, [Body]
	, [SentOn]
	, [ReceivedOn]
	, [Status]
	, [IsQueued]
	, [OpenedOn]
	, [Opened]
    FROM
	[dbo].[meanstream_Messaging]
    WHERE 
	 ([Id] = @Id AND @Id is not null)
	OR ([MessageType] = @MessageType AND @MessageType is not null)
	OR ([Sender] = @Sender AND @Sender is not null)
	OR ([Recipient] = @Recipient AND @Recipient is not null)
	OR ([Subject] = @Subject AND @Subject is not null)
	OR ([Body] = @Body AND @Body is not null)
	OR ([SentOn] = @SentOn AND @SentOn is not null)
	OR ([ReceivedOn] = @ReceivedOn AND @ReceivedOn is not null)
	OR ([Status] = @Status AND @Status is not null)
	OR ([IsQueued] = @IsQueued AND @IsQueued is not null)
	OR ([OpenedOn] = @OpenedOn AND @OpenedOn is not null)
	OR ([Opened] = @Opened AND @Opened is not null)
	SELECT @@ROWCOUNT			
  END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Messaging_Delete]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Deletes a record in the meanstream_Messaging table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Messaging_Delete]
(

	@Id uniqueidentifier   
)
AS


				DELETE FROM [dbo].[meanstream_Messaging] WITH (ROWLOCK) 
				WHERE
					[Id] = @Id
GO
/****** Object:  StoredProcedure [dbo].[meanstream_FreeText_Update]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Updates a record in the meanstream_FreeText table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_FreeText_Update]
(

	@Id uniqueidentifier   ,

	@OriginalId uniqueidentifier   ,

	@ContentId int   ,

	@ModuleId uniqueidentifier   ,

	@ModuleVersionId uniqueidentifier   ,

	@CreatedDate datetime   ,

	@CreatedBy uniqueidentifier   ,

	@LastModifiedDate datetime   ,

	@LastModifiedBy uniqueidentifier   ,

	@IsLocked bit   ,

	@CheckedOutBy uniqueidentifier   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[meanstream_FreeText]
				SET
					[Id] = @Id
					,[ContentId] = @ContentId
					,[ModuleId] = @ModuleId
					,[ModuleVersionId] = @ModuleVersionId
					,[CreatedDate] = @CreatedDate
					,[CreatedBy] = @CreatedBy
					,[LastModifiedDate] = @LastModifiedDate
					,[LastModifiedBy] = @LastModifiedBy
					,[IsLocked] = @IsLocked
					,[CheckedOutBy] = @CheckedOutBy
				WHERE
[Id] = @OriginalId
GO
/****** Object:  StoredProcedure [dbo].[meanstream_FreeText_Insert]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Inserts a record into the meanstream_FreeText table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_FreeText_Insert]
(

	@Id uniqueidentifier   ,

	@ContentId int   ,

	@ModuleId uniqueidentifier   ,

	@ModuleVersionId uniqueidentifier   ,

	@CreatedDate datetime   ,

	@CreatedBy uniqueidentifier   ,

	@LastModifiedDate datetime   ,

	@LastModifiedBy uniqueidentifier   ,

	@IsLocked bit   ,

	@CheckedOutBy uniqueidentifier   
)
AS


				
				INSERT INTO [dbo].[meanstream_FreeText]
					(
					[Id]
					,[ContentId]
					,[ModuleId]
					,[ModuleVersionId]
					,[CreatedDate]
					,[CreatedBy]
					,[LastModifiedDate]
					,[LastModifiedBy]
					,[IsLocked]
					,[CheckedOutBy]
					)
				VALUES
					(
					@Id
					,@ContentId
					,@ModuleId
					,@ModuleVersionId
					,@CreatedDate
					,@CreatedBy
					,@LastModifiedDate
					,@LastModifiedBy
					,@IsLocked
					,@CheckedOutBy
					)
GO
/****** Object:  StoredProcedure [dbo].[meanstream_FreeText_GetPaged]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets records from the meanstream_FreeText table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_FreeText_GetPaged]
(

	@WhereClause varchar (2000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [Id] uniqueidentifier 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([Id])'
				SET @SQL = @SQL + ' SELECT'
				IF @PageSize > 0
				BEGIN
					SET @SQL = @SQL + ' TOP ' + CONVERT(nvarchar, @PageUpperBound)
				END
				SET @SQL = @SQL + ' [Id]'
				SET @SQL = @SQL + ' FROM [dbo].[meanstream_FreeText]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Return paged results
				SELECT O.[Id], O.[ContentId], O.[ModuleId], O.[ModuleVersionId], O.[CreatedDate], O.[CreatedBy], O.[LastModifiedDate], O.[LastModifiedBy], O.[IsLocked], O.[CheckedOutBy]
				FROM
				    [dbo].[meanstream_FreeText] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[Id] = PageIndex.[Id]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[meanstream_FreeText]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_FreeText_GetById]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Select records from the meanstream_FreeText table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_FreeText_GetById]
(

	@Id uniqueidentifier   
)
AS


				SELECT
					[Id],
					[ContentId],
					[ModuleId],
					[ModuleVersionId],
					[CreatedDate],
					[CreatedBy],
					[LastModifiedDate],
					[LastModifiedBy],
					[IsLocked],
					[CheckedOutBy]
				FROM
					[dbo].[meanstream_FreeText]
				WHERE
					[Id] = @Id
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[meanstream_FreeText_Get_List]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets all records from the meanstream_FreeText table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_FreeText_Get_List]

AS


				
				SELECT
					[Id],
					[ContentId],
					[ModuleId],
					[ModuleVersionId],
					[CreatedDate],
					[CreatedBy],
					[LastModifiedDate],
					[LastModifiedBy],
					[IsLocked],
					[CheckedOutBy]
				FROM
					[dbo].[meanstream_FreeText]
					
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[meanstream_FreeText_Find]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Finds records in the meanstream_FreeText table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_FreeText_Find]
(

	@SearchUsingOR bit   = null ,

	@Id uniqueidentifier   = null ,

	@ContentId int   = null ,

	@ModuleId uniqueidentifier   = null ,

	@ModuleVersionId uniqueidentifier   = null ,

	@CreatedDate datetime   = null ,

	@CreatedBy uniqueidentifier   = null ,

	@LastModifiedDate datetime   = null ,

	@LastModifiedBy uniqueidentifier   = null ,

	@IsLocked bit   = null ,

	@CheckedOutBy uniqueidentifier   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [Id]
	, [ContentId]
	, [ModuleId]
	, [ModuleVersionId]
	, [CreatedDate]
	, [CreatedBy]
	, [LastModifiedDate]
	, [LastModifiedBy]
	, [IsLocked]
	, [CheckedOutBy]
    FROM
	[dbo].[meanstream_FreeText]
    WHERE 
	 ([Id] = @Id OR @Id IS NULL)
	AND ([ContentId] = @ContentId OR @ContentId IS NULL)
	AND ([ModuleId] = @ModuleId OR @ModuleId IS NULL)
	AND ([ModuleVersionId] = @ModuleVersionId OR @ModuleVersionId IS NULL)
	AND ([CreatedDate] = @CreatedDate OR @CreatedDate IS NULL)
	AND ([CreatedBy] = @CreatedBy OR @CreatedBy IS NULL)
	AND ([LastModifiedDate] = @LastModifiedDate OR @LastModifiedDate IS NULL)
	AND ([LastModifiedBy] = @LastModifiedBy OR @LastModifiedBy IS NULL)
	AND ([IsLocked] = @IsLocked OR @IsLocked IS NULL)
	AND ([CheckedOutBy] = @CheckedOutBy OR @CheckedOutBy IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [Id]
	, [ContentId]
	, [ModuleId]
	, [ModuleVersionId]
	, [CreatedDate]
	, [CreatedBy]
	, [LastModifiedDate]
	, [LastModifiedBy]
	, [IsLocked]
	, [CheckedOutBy]
    FROM
	[dbo].[meanstream_FreeText]
    WHERE 
	 ([Id] = @Id AND @Id is not null)
	OR ([ContentId] = @ContentId AND @ContentId is not null)
	OR ([ModuleId] = @ModuleId AND @ModuleId is not null)
	OR ([ModuleVersionId] = @ModuleVersionId AND @ModuleVersionId is not null)
	OR ([CreatedDate] = @CreatedDate AND @CreatedDate is not null)
	OR ([CreatedBy] = @CreatedBy AND @CreatedBy is not null)
	OR ([LastModifiedDate] = @LastModifiedDate AND @LastModifiedDate is not null)
	OR ([LastModifiedBy] = @LastModifiedBy AND @LastModifiedBy is not null)
	OR ([IsLocked] = @IsLocked AND @IsLocked is not null)
	OR ([CheckedOutBy] = @CheckedOutBy AND @CheckedOutBy is not null)
	SELECT @@ROWCOUNT			
  END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_FreeText_Delete]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Deletes a record in the meanstream_FreeText table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_FreeText_Delete]
(

	@Id uniqueidentifier   
)
AS


				DELETE FROM [dbo].[meanstream_FreeText] WITH (ROWLOCK) 
				WHERE
					[Id] = @Id
GO
/****** Object:  StoredProcedure [dbo].[meanstream_DeinitializeMessaging]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[meanstream_DeinitializeMessaging] 
	-- Add the parameters for the stored procedure here
	@ApplicationId uniqueidentifier
AS
BEGIN
	
   
	DELETE FROM [dbo].[meanstream_ApplicationMessaging] WHERE [TargetApplicationId] = @ApplicationId
	DELETE FROM [dbo].[meanstream_ApplicationMessagingInstance] WHERE [ApplicationId] = @ApplicationId
		
END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ContentType_Update]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Updates a record in the meanstream_ContentType table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ContentType_Update]
(

	@Type nvarchar (255)  ,

	@OriginalType nvarchar (255)  
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[meanstream_ContentType]
				SET
					[Type] = @Type
				WHERE
[Type] = @OriginalType
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ContentType_Insert]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Inserts a record into the meanstream_ContentType table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ContentType_Insert]
(

	@Type nvarchar (255)  
)
AS


				
				INSERT INTO [dbo].[meanstream_ContentType]
					(
					[Type]
					)
				VALUES
					(
					@Type
					)
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ContentType_GetPaged]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets records from the meanstream_ContentType table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ContentType_GetPaged]
(

	@WhereClause varchar (2000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [Type] nvarchar(255) COLLATE database_default  
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([Type])'
				SET @SQL = @SQL + ' SELECT'
				IF @PageSize > 0
				BEGIN
					SET @SQL = @SQL + ' TOP ' + CONVERT(nvarchar, @PageUpperBound)
				END
				SET @SQL = @SQL + ' [Type]'
				SET @SQL = @SQL + ' FROM [dbo].[meanstream_ContentType]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Return paged results
				SELECT O.[Type]
				FROM
				    [dbo].[meanstream_ContentType] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[Type] = PageIndex.[Type]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[meanstream_ContentType]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ContentType_GetByType]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Select records from the meanstream_ContentType table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ContentType_GetByType]
(

	@Type nvarchar (255)  
)
AS


				SELECT
					[Type]
				FROM
					[dbo].[meanstream_ContentType]
				WHERE
					[Type] = @Type
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ContentType_Get_List]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets all records from the meanstream_ContentType table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ContentType_Get_List]

AS


				
				SELECT
					[Type]
				FROM
					[dbo].[meanstream_ContentType]
					
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ContentType_Find]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Finds records in the meanstream_ContentType table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ContentType_Find]
(

	@SearchUsingOR bit   = null ,

	@Type nvarchar (255)  = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [Type]
    FROM
	[dbo].[meanstream_ContentType]
    WHERE 
	 ([Type] = @Type OR @Type IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [Type]
    FROM
	[dbo].[meanstream_ContentType]
    WHERE 
	 ([Type] = @Type AND @Type is not null)
	SELECT @@ROWCOUNT			
  END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ContentType_Delete]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Deletes a record in the meanstream_ContentType table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ContentType_Delete]
(

	@Type nvarchar (255)  
)
AS


				DELETE FROM [dbo].[meanstream_ContentType] WITH (ROWLOCK) 
				WHERE
					[Type] = @Type
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Content_Update]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Updates a record in the meanstream_Content table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Content_Update]
(

	@Id int   ,

	@ContentType nvarchar (255)  ,

	@Title nvarchar (500)  ,

	@Author nvarchar (255)  ,

	@UserId uniqueidentifier   ,

	@Text nvarchar (MAX)  ,

	@CreatedDate datetime   ,

	@LastUpdateDate datetime   ,

	@IsShared bit   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[meanstream_Content]
				SET
					[ContentType] = @ContentType
					,[Title] = @Title
					,[Author] = @Author
					,[UserId] = @UserId
					,[Text] = @Text
					,[CreatedDate] = @CreatedDate
					,[LastUpdateDate] = @LastUpdateDate
					,[IsShared] = @IsShared
				WHERE
[Id] = @Id
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Content_Insert]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Inserts a record into the meanstream_Content table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Content_Insert]
(

	@Id int    OUTPUT,

	@ContentType nvarchar (255)  ,

	@Title nvarchar (500)  ,

	@Author nvarchar (255)  ,

	@UserId uniqueidentifier   ,

	@Text nvarchar (MAX)  ,

	@CreatedDate datetime   ,

	@LastUpdateDate datetime   ,

	@IsShared bit   
)
AS


				
				INSERT INTO [dbo].[meanstream_Content]
					(
					[ContentType]
					,[Title]
					,[Author]
					,[UserId]
					,[Text]
					,[CreatedDate]
					,[LastUpdateDate]
					,[IsShared]
					)
				VALUES
					(
					@ContentType
					,@Title
					,@Author
					,@UserId
					,@Text
					,@CreatedDate
					,@LastUpdateDate
					,@IsShared
					)
				
				-- Get the identity value
				SET @Id = SCOPE_IDENTITY()
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Content_GetPaged]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets records from the meanstream_Content table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Content_GetPaged]
(

	@WhereClause varchar (2000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [Id] int 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([Id])'
				SET @SQL = @SQL + ' SELECT'
				IF @PageSize > 0
				BEGIN
					SET @SQL = @SQL + ' TOP ' + CONVERT(nvarchar, @PageUpperBound)
				END
				SET @SQL = @SQL + ' [Id]'
				SET @SQL = @SQL + ' FROM [dbo].[meanstream_Content]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Return paged results
				SELECT O.[Id], O.[ContentType], O.[Title], O.[Author], O.[UserId], O.[Text], O.[CreatedDate], O.[LastUpdateDate], O.[IsShared]
				FROM
				    [dbo].[meanstream_Content] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[Id] = PageIndex.[Id]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[meanstream_Content]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Content_GetById]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Select records from the meanstream_Content table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Content_GetById]
(

	@Id int   
)
AS


				SELECT
					[Id],
					[ContentType],
					[Title],
					[Author],
					[UserId],
					[Text],
					[CreatedDate],
					[LastUpdateDate],
					[IsShared]
				FROM
					[dbo].[meanstream_Content]
				WHERE
					[Id] = @Id
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Content_Get_List]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets all records from the meanstream_Content table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Content_Get_List]

AS


				
				SELECT
					[Id],
					[ContentType],
					[Title],
					[Author],
					[UserId],
					[Text],
					[CreatedDate],
					[LastUpdateDate],
					[IsShared]
				FROM
					[dbo].[meanstream_Content]
					
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Content_Find]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Finds records in the meanstream_Content table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Content_Find]
(

	@SearchUsingOR bit   = null ,

	@Id int   = null ,

	@ContentType nvarchar (255)  = null ,

	@Title nvarchar (500)  = null ,

	@Author nvarchar (255)  = null ,

	@UserId uniqueidentifier   = null ,

	@Text nvarchar (MAX)  = null ,

	@CreatedDate datetime   = null ,

	@LastUpdateDate datetime   = null ,

	@IsShared bit   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [Id]
	, [ContentType]
	, [Title]
	, [Author]
	, [UserId]
	, [Text]
	, [CreatedDate]
	, [LastUpdateDate]
	, [IsShared]
    FROM
	[dbo].[meanstream_Content]
    WHERE 
	 ([Id] = @Id OR @Id IS NULL)
	AND ([ContentType] = @ContentType OR @ContentType IS NULL)
	AND ([Title] = @Title OR @Title IS NULL)
	AND ([Author] = @Author OR @Author IS NULL)
	AND ([UserId] = @UserId OR @UserId IS NULL)
	AND ([Text] = @Text OR @Text IS NULL)
	AND ([CreatedDate] = @CreatedDate OR @CreatedDate IS NULL)
	AND ([LastUpdateDate] = @LastUpdateDate OR @LastUpdateDate IS NULL)
	AND ([IsShared] = @IsShared OR @IsShared IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [Id]
	, [ContentType]
	, [Title]
	, [Author]
	, [UserId]
	, [Text]
	, [CreatedDate]
	, [LastUpdateDate]
	, [IsShared]
    FROM
	[dbo].[meanstream_Content]
    WHERE 
	 ([Id] = @Id AND @Id is not null)
	OR ([ContentType] = @ContentType AND @ContentType is not null)
	OR ([Title] = @Title AND @Title is not null)
	OR ([Author] = @Author AND @Author is not null)
	OR ([UserId] = @UserId AND @UserId is not null)
	OR ([Text] = @Text AND @Text is not null)
	OR ([CreatedDate] = @CreatedDate AND @CreatedDate is not null)
	OR ([LastUpdateDate] = @LastUpdateDate AND @LastUpdateDate is not null)
	OR ([IsShared] = @IsShared AND @IsShared is not null)
	SELECT @@ROWCOUNT			
  END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Content_Delete]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Deletes a record in the meanstream_Content table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Content_Delete]
(

	@Id int   
)
AS


				DELETE FROM [dbo].[meanstream_Content] WITH (ROWLOCK) 
				WHERE
					[Id] = @Id
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ApplicationSettings_Update]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Updates a record in the meanstream_ApplicationSettings table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ApplicationSettings_Update]
(

	@Id int   ,

	@Param nvarchar (MAX)  ,

	@Value nvarchar (MAX)  
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[meanstream_ApplicationSettings]
				SET
					[Param] = @Param
					,[Value] = @Value
				WHERE
[Id] = @Id
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ApplicationSettings_Insert]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Inserts a record into the meanstream_ApplicationSettings table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ApplicationSettings_Insert]
(

	@Id int    OUTPUT,

	@Param nvarchar (MAX)  ,

	@Value nvarchar (MAX)  
)
AS


				
				INSERT INTO [dbo].[meanstream_ApplicationSettings]
					(
					[Param]
					,[Value]
					)
				VALUES
					(
					@Param
					,@Value
					)
				
				-- Get the identity value
				SET @Id = SCOPE_IDENTITY()
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ApplicationSettings_GetPaged]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets records from the meanstream_ApplicationSettings table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ApplicationSettings_GetPaged]
(

	@WhereClause varchar (2000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [Id] int 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([Id])'
				SET @SQL = @SQL + ' SELECT'
				IF @PageSize > 0
				BEGIN
					SET @SQL = @SQL + ' TOP ' + CONVERT(nvarchar, @PageUpperBound)
				END
				SET @SQL = @SQL + ' [Id]'
				SET @SQL = @SQL + ' FROM [dbo].[meanstream_ApplicationSettings]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Return paged results
				SELECT O.[Id], O.[Param], O.[Value]
				FROM
				    [dbo].[meanstream_ApplicationSettings] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[Id] = PageIndex.[Id]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[meanstream_ApplicationSettings]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ApplicationSettings_GetById]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Select records from the meanstream_ApplicationSettings table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ApplicationSettings_GetById]
(

	@Id int   
)
AS


				SELECT
					[Id],
					[Param],
					[Value]
				FROM
					[dbo].[meanstream_ApplicationSettings]
				WHERE
					[Id] = @Id
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ApplicationSettings_Get_List]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets all records from the meanstream_ApplicationSettings table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ApplicationSettings_Get_List]

AS


				
				SELECT
					[Id],
					[Param],
					[Value]
				FROM
					[dbo].[meanstream_ApplicationSettings]
					
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ApplicationSettings_Find]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Finds records in the meanstream_ApplicationSettings table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ApplicationSettings_Find]
(

	@SearchUsingOR bit   = null ,

	@Id int   = null ,

	@Param nvarchar (MAX)  = null ,

	@Value nvarchar (MAX)  = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [Id]
	, [Param]
	, [Value]
    FROM
	[dbo].[meanstream_ApplicationSettings]
    WHERE 
	 ([Id] = @Id OR @Id IS NULL)
	AND ([Param] = @Param OR @Param IS NULL)
	AND ([Value] = @Value OR @Value IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [Id]
	, [Param]
	, [Value]
    FROM
	[dbo].[meanstream_ApplicationSettings]
    WHERE 
	 ([Id] = @Id AND @Id is not null)
	OR ([Param] = @Param AND @Param is not null)
	OR ([Value] = @Value AND @Value is not null)
	SELECT @@ROWCOUNT			
  END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ApplicationSettings_Delete]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Deletes a record in the meanstream_ApplicationSettings table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ApplicationSettings_Delete]
(

	@Id int   
)
AS


				DELETE FROM [dbo].[meanstream_ApplicationSettings] WITH (ROWLOCK) 
				WHERE
					[Id] = @Id
GO
/****** Object:  StoredProcedure [dbo].[meanstream_CleanTracing]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[meanstream_CleanTracing] 
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DELETE FROM meanstream_Tracing
END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Attributes_Update]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Updates a record in the meanstream_Attributes table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Attributes_Update]
(

	@Id int   ,

	@ComponentId uniqueidentifier   ,

	@Name varchar (200)  ,

	@Value ntext   ,

	@DataType varchar (50)  
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[meanstream_Attributes]
				SET
					[ComponentId] = @ComponentId
					,[Name] = @Name
					,[Value] = @Value
					,[DataType] = @DataType
				WHERE
[Id] = @Id
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Attributes_Insert]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Inserts a record into the meanstream_Attributes table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Attributes_Insert]
(

	@Id int    OUTPUT,

	@ComponentId uniqueidentifier   ,

	@Name varchar (200)  ,

	@Value ntext   ,

	@DataType varchar (50)  
)
AS


				
				INSERT INTO [dbo].[meanstream_Attributes]
					(
					[ComponentId]
					,[Name]
					,[Value]
					,[DataType]
					)
				VALUES
					(
					@ComponentId
					,@Name
					,@Value
					,@DataType
					)
				
				-- Get the identity value
				SET @Id = SCOPE_IDENTITY()
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Attributes_GetPaged]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets records from the meanstream_Attributes table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Attributes_GetPaged]
(

	@WhereClause varchar (2000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [Id] int 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([Id])'
				SET @SQL = @SQL + ' SELECT'
				IF @PageSize > 0
				BEGIN
					SET @SQL = @SQL + ' TOP ' + CONVERT(nvarchar, @PageUpperBound)
				END
				SET @SQL = @SQL + ' [Id]'
				SET @SQL = @SQL + ' FROM [dbo].[meanstream_Attributes]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Return paged results
				SELECT O.[Id], O.[ComponentId], O.[Name], O.[Value], O.[DataType]
				FROM
				    [dbo].[meanstream_Attributes] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[Id] = PageIndex.[Id]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[meanstream_Attributes]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Attributes_GetById]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Select records from the meanstream_Attributes table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Attributes_GetById]
(

	@Id int   
)
AS


				SELECT
					[Id],
					[ComponentId],
					[Name],
					[Value],
					[DataType]
				FROM
					[dbo].[meanstream_Attributes]
				WHERE
					[Id] = @Id
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Attributes_Get_List]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets all records from the meanstream_Attributes table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Attributes_Get_List]

AS


				
				SELECT
					[Id],
					[ComponentId],
					[Name],
					[Value],
					[DataType]
				FROM
					[dbo].[meanstream_Attributes]
					
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Attributes_Find]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Finds records in the meanstream_Attributes table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Attributes_Find]
(

	@SearchUsingOR bit   = null ,

	@Id int   = null ,

	@ComponentId uniqueidentifier   = null ,

	@Name varchar (200)  = null ,

	@Value ntext   = null ,

	@DataType varchar (50)  = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [Id]
	, [ComponentId]
	, [Name]
	, [Value]
	, [DataType]
    FROM
	[dbo].[meanstream_Attributes]
    WHERE 
	 ([Id] = @Id OR @Id IS NULL)
	AND ([ComponentId] = @ComponentId OR @ComponentId IS NULL)
	AND ([Name] = @Name OR @Name IS NULL)
	AND ([DataType] = @DataType OR @DataType IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [Id]
	, [ComponentId]
	, [Name]
	, [Value]
	, [DataType]
    FROM
	[dbo].[meanstream_Attributes]
    WHERE 
	 ([Id] = @Id AND @Id is not null)
	OR ([ComponentId] = @ComponentId AND @ComponentId is not null)
	OR ([Name] = @Name AND @Name is not null)
	OR ([DataType] = @DataType AND @DataType is not null)
	SELECT @@ROWCOUNT			
  END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Attributes_Delete]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Deletes a record in the meanstream_Attributes table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Attributes_Delete]
(

	@Id int   
)
AS


				DELETE FROM [dbo].[meanstream_Attributes] WITH (ROWLOCK) 
				WHERE
					[Id] = @Id
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ApplicationMessaging_Update]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Updates a record in the meanstream_ApplicationMessaging table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ApplicationMessaging_Update]
(

	@Id int   ,

	@SenderAppicationId uniqueidentifier   ,

	@TargetApplicationId uniqueidentifier   ,

	@MessageType nvarchar (255)  ,

	@Message nvarchar (MAX)  
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[meanstream_ApplicationMessaging]
				SET
					[SenderAppicationId] = @SenderAppicationId
					,[TargetApplicationId] = @TargetApplicationId
					,[MessageType] = @MessageType
					,[Message] = @Message
				WHERE
[Id] = @Id
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ApplicationMessaging_Insert]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Inserts a record into the meanstream_ApplicationMessaging table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ApplicationMessaging_Insert]
(

	@Id int    OUTPUT,

	@SenderAppicationId uniqueidentifier   ,

	@TargetApplicationId uniqueidentifier   ,

	@MessageType nvarchar (255)  ,

	@Message nvarchar (MAX)  
)
AS


				
				INSERT INTO [dbo].[meanstream_ApplicationMessaging]
					(
					[SenderAppicationId]
					,[TargetApplicationId]
					,[MessageType]
					,[Message]
					)
				VALUES
					(
					@SenderAppicationId
					,@TargetApplicationId
					,@MessageType
					,@Message
					)
				
				-- Get the identity value
				SET @Id = SCOPE_IDENTITY()
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ApplicationMessaging_GetPaged]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets records from the meanstream_ApplicationMessaging table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ApplicationMessaging_GetPaged]
(

	@WhereClause varchar (2000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [Id] int 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([Id])'
				SET @SQL = @SQL + ' SELECT'
				IF @PageSize > 0
				BEGIN
					SET @SQL = @SQL + ' TOP ' + CONVERT(nvarchar, @PageUpperBound)
				END
				SET @SQL = @SQL + ' [Id]'
				SET @SQL = @SQL + ' FROM [dbo].[meanstream_ApplicationMessaging]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Return paged results
				SELECT O.[Id], O.[SenderAppicationId], O.[TargetApplicationId], O.[MessageType], O.[Message]
				FROM
				    [dbo].[meanstream_ApplicationMessaging] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[Id] = PageIndex.[Id]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[meanstream_ApplicationMessaging]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ApplicationMessaging_GetById]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Select records from the meanstream_ApplicationMessaging table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ApplicationMessaging_GetById]
(

	@Id int   
)
AS


				SELECT
					[Id],
					[SenderAppicationId],
					[TargetApplicationId],
					[MessageType],
					[Message]
				FROM
					[dbo].[meanstream_ApplicationMessaging]
				WHERE
					[Id] = @Id
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ApplicationMessaging_Get_List]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets all records from the meanstream_ApplicationMessaging table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ApplicationMessaging_Get_List]

AS


				
				SELECT
					[Id],
					[SenderAppicationId],
					[TargetApplicationId],
					[MessageType],
					[Message]
				FROM
					[dbo].[meanstream_ApplicationMessaging]
					
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ApplicationMessaging_Find]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Finds records in the meanstream_ApplicationMessaging table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ApplicationMessaging_Find]
(

	@SearchUsingOR bit   = null ,

	@Id int   = null ,

	@SenderAppicationId uniqueidentifier   = null ,

	@TargetApplicationId uniqueidentifier   = null ,

	@MessageType nvarchar (255)  = null ,

	@Message nvarchar (MAX)  = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [Id]
	, [SenderAppicationId]
	, [TargetApplicationId]
	, [MessageType]
	, [Message]
    FROM
	[dbo].[meanstream_ApplicationMessaging]
    WHERE 
	 ([Id] = @Id OR @Id IS NULL)
	AND ([SenderAppicationId] = @SenderAppicationId OR @SenderAppicationId IS NULL)
	AND ([TargetApplicationId] = @TargetApplicationId OR @TargetApplicationId IS NULL)
	AND ([MessageType] = @MessageType OR @MessageType IS NULL)
	AND ([Message] = @Message OR @Message IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [Id]
	, [SenderAppicationId]
	, [TargetApplicationId]
	, [MessageType]
	, [Message]
    FROM
	[dbo].[meanstream_ApplicationMessaging]
    WHERE 
	 ([Id] = @Id AND @Id is not null)
	OR ([SenderAppicationId] = @SenderAppicationId AND @SenderAppicationId is not null)
	OR ([TargetApplicationId] = @TargetApplicationId AND @TargetApplicationId is not null)
	OR ([MessageType] = @MessageType AND @MessageType is not null)
	OR ([Message] = @Message AND @Message is not null)
	SELECT @@ROWCOUNT			
  END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ApplicationMessaging_Delete]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Deletes a record in the meanstream_ApplicationMessaging table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ApplicationMessaging_Delete]
(

	@Id int   
)
AS


				DELETE FROM [dbo].[meanstream_ApplicationMessaging] WITH (ROWLOCK) 
				WHERE
					[Id] = @Id
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ApplicationMessagingInstance_Update]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Updates a record in the meanstream_ApplicationMessagingInstance table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ApplicationMessagingInstance_Update]
(

	@Id int   ,

	@ApplicationId uniqueidentifier   ,

	@DomainName nvarchar (255)  ,

	@MachineName nvarchar (255)  ,

	@RegisteredDate datetime   ,

	@LastUpdateDate datetime   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[meanstream_ApplicationMessagingInstance]
				SET
					[ApplicationId] = @ApplicationId
					,[DomainName] = @DomainName
					,[MachineName] = @MachineName
					,[RegisteredDate] = @RegisteredDate
					,[LastUpdateDate] = @LastUpdateDate
				WHERE
[Id] = @Id
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ApplicationMessagingInstance_Insert]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Inserts a record into the meanstream_ApplicationMessagingInstance table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ApplicationMessagingInstance_Insert]
(

	@Id int    OUTPUT,

	@ApplicationId uniqueidentifier   ,

	@DomainName nvarchar (255)  ,

	@MachineName nvarchar (255)  ,

	@RegisteredDate datetime   ,

	@LastUpdateDate datetime   
)
AS


				
				INSERT INTO [dbo].[meanstream_ApplicationMessagingInstance]
					(
					[ApplicationId]
					,[DomainName]
					,[MachineName]
					,[RegisteredDate]
					,[LastUpdateDate]
					)
				VALUES
					(
					@ApplicationId
					,@DomainName
					,@MachineName
					,@RegisteredDate
					,@LastUpdateDate
					)
				
				-- Get the identity value
				SET @Id = SCOPE_IDENTITY()
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ApplicationMessagingInstance_GetPaged]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets records from the meanstream_ApplicationMessagingInstance table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ApplicationMessagingInstance_GetPaged]
(

	@WhereClause varchar (2000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [Id] int 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([Id])'
				SET @SQL = @SQL + ' SELECT'
				IF @PageSize > 0
				BEGIN
					SET @SQL = @SQL + ' TOP ' + CONVERT(nvarchar, @PageUpperBound)
				END
				SET @SQL = @SQL + ' [Id]'
				SET @SQL = @SQL + ' FROM [dbo].[meanstream_ApplicationMessagingInstance]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Return paged results
				SELECT O.[Id], O.[ApplicationId], O.[DomainName], O.[MachineName], O.[RegisteredDate], O.[LastUpdateDate]
				FROM
				    [dbo].[meanstream_ApplicationMessagingInstance] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[Id] = PageIndex.[Id]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[meanstream_ApplicationMessagingInstance]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ApplicationMessagingInstance_GetById]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Select records from the meanstream_ApplicationMessagingInstance table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ApplicationMessagingInstance_GetById]
(

	@Id int   
)
AS


				SELECT
					[Id],
					[ApplicationId],
					[DomainName],
					[MachineName],
					[RegisteredDate],
					[LastUpdateDate]
				FROM
					[dbo].[meanstream_ApplicationMessagingInstance]
				WHERE
					[Id] = @Id
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ApplicationMessagingInstance_Get_List]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets all records from the meanstream_ApplicationMessagingInstance table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ApplicationMessagingInstance_Get_List]

AS


				
				SELECT
					[Id],
					[ApplicationId],
					[DomainName],
					[MachineName],
					[RegisteredDate],
					[LastUpdateDate]
				FROM
					[dbo].[meanstream_ApplicationMessagingInstance]
					
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ApplicationMessagingInstance_Find]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Finds records in the meanstream_ApplicationMessagingInstance table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ApplicationMessagingInstance_Find]
(

	@SearchUsingOR bit   = null ,

	@Id int   = null ,

	@ApplicationId uniqueidentifier   = null ,

	@DomainName nvarchar (255)  = null ,

	@MachineName nvarchar (255)  = null ,

	@RegisteredDate datetime   = null ,

	@LastUpdateDate datetime   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [Id]
	, [ApplicationId]
	, [DomainName]
	, [MachineName]
	, [RegisteredDate]
	, [LastUpdateDate]
    FROM
	[dbo].[meanstream_ApplicationMessagingInstance]
    WHERE 
	 ([Id] = @Id OR @Id IS NULL)
	AND ([ApplicationId] = @ApplicationId OR @ApplicationId IS NULL)
	AND ([DomainName] = @DomainName OR @DomainName IS NULL)
	AND ([MachineName] = @MachineName OR @MachineName IS NULL)
	AND ([RegisteredDate] = @RegisteredDate OR @RegisteredDate IS NULL)
	AND ([LastUpdateDate] = @LastUpdateDate OR @LastUpdateDate IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [Id]
	, [ApplicationId]
	, [DomainName]
	, [MachineName]
	, [RegisteredDate]
	, [LastUpdateDate]
    FROM
	[dbo].[meanstream_ApplicationMessagingInstance]
    WHERE 
	 ([Id] = @Id AND @Id is not null)
	OR ([ApplicationId] = @ApplicationId AND @ApplicationId is not null)
	OR ([DomainName] = @DomainName AND @DomainName is not null)
	OR ([MachineName] = @MachineName AND @MachineName is not null)
	OR ([RegisteredDate] = @RegisteredDate AND @RegisteredDate is not null)
	OR ([LastUpdateDate] = @LastUpdateDate AND @LastUpdateDate is not null)
	SELECT @@ROWCOUNT			
  END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ApplicationMessagingInstance_Delete]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Deletes a record in the meanstream_ApplicationMessagingInstance table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ApplicationMessagingInstance_Delete]
(

	@Id int   
)
AS


				DELETE FROM [dbo].[meanstream_ApplicationMessagingInstance] WITH (ROWLOCK) 
				WHERE
					[Id] = @Id
GO
/****** Object:  StoredProcedure [dbo].[meanstream_AddTrace]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[meanstream_AddTrace]

@m_TraceDateTime	datetime,
@m_TraceCategory	nvarchar(50),
@m_TraceDescription	nvarchar(1024),
@m_StackTrace		nvarchar(MAX),
@m_DetailedErrorDescription	nvarchar(MAX)

as
begin
	insert into [dbo].[meanstream_Tracing] 	(TraceDateTime, TraceCategory, TraceDescription, StackTrace, DetailedErrorDescription)
	values
			(@m_TraceDateTime, @m_TraceCategory, @m_TraceDescription, @m_StackTrace, @m_DetailedErrorDescription)
	return @@error
end
GO
/****** Object:  StoredProcedure [dbo].[aspnet_WebEvent_LogEvent]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_WebEvent_LogEvent]
        @EventId         char(32),
        @EventTimeUtc    datetime,
        @EventTime       datetime,
        @EventType       nvarchar(256),
        @EventSequence   decimal(19,0),
        @EventOccurrence decimal(19,0),
        @EventCode       int,
        @EventDetailCode int,
        @Message         nvarchar(1024),
        @ApplicationPath nvarchar(256),
        @ApplicationVirtualPath nvarchar(256),
        @MachineName    nvarchar(256),
        @RequestUrl      nvarchar(1024),
        @ExceptionType   nvarchar(256),
        @Details         ntext
AS
BEGIN
    INSERT
        dbo.aspnet_WebEvent_Events
        (
            EventId,
            EventTimeUtc,
            EventTime,
            EventType,
            EventSequence,
            EventOccurrence,
            EventCode,
            EventDetailCode,
            Message,
            ApplicationPath,
            ApplicationVirtualPath,
            MachineName,
            RequestUrl,
            ExceptionType,
            Details
        )
    VALUES
    (
        @EventId,
        @EventTimeUtc,
        @EventTime,
        @EventType,
        @EventSequence,
        @EventOccurrence,
        @EventCode,
        @EventDetailCode,
        @Message,
        @ApplicationPath,
        @ApplicationVirtualPath,
        @MachineName,
        @RequestUrl,
        @ExceptionType,
        @Details
    )
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_WebEvent_Events_Update]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Updates a record in the aspnet_WebEvent_Events table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_WebEvent_Events_Update]
(

	@EventId char (32)  ,

	@OriginalEventId char (32)  ,

	@EventTimeUtc datetime   ,

	@EventTime datetime   ,

	@EventType nvarchar (256)  ,

	@EventSequence decimal (19, 0)  ,

	@EventOccurrence decimal (19, 0)  ,

	@EventCode int   ,

	@EventDetailCode int   ,

	@Message nvarchar (1024)  ,

	@ApplicationPath nvarchar (256)  ,

	@ApplicationVirtualPath nvarchar (256)  ,

	@MachineName nvarchar (256)  ,

	@RequestUrl nvarchar (1024)  ,

	@ExceptionType nvarchar (256)  ,

	@Details ntext   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[aspnet_WebEvent_Events]
				SET
					[EventId] = @EventId
					,[EventTimeUtc] = @EventTimeUtc
					,[EventTime] = @EventTime
					,[EventType] = @EventType
					,[EventSequence] = @EventSequence
					,[EventOccurrence] = @EventOccurrence
					,[EventCode] = @EventCode
					,[EventDetailCode] = @EventDetailCode
					,[Message] = @Message
					,[ApplicationPath] = @ApplicationPath
					,[ApplicationVirtualPath] = @ApplicationVirtualPath
					,[MachineName] = @MachineName
					,[RequestUrl] = @RequestUrl
					,[ExceptionType] = @ExceptionType
					,[Details] = @Details
				WHERE
[EventId] = @OriginalEventId
GO
/****** Object:  StoredProcedure [dbo].[aspnet_WebEvent_Events_Insert]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Inserts a record into the aspnet_WebEvent_Events table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_WebEvent_Events_Insert]
(

	@EventId char (32)  ,

	@EventTimeUtc datetime   ,

	@EventTime datetime   ,

	@EventType nvarchar (256)  ,

	@EventSequence decimal (19, 0)  ,

	@EventOccurrence decimal (19, 0)  ,

	@EventCode int   ,

	@EventDetailCode int   ,

	@Message nvarchar (1024)  ,

	@ApplicationPath nvarchar (256)  ,

	@ApplicationVirtualPath nvarchar (256)  ,

	@MachineName nvarchar (256)  ,

	@RequestUrl nvarchar (1024)  ,

	@ExceptionType nvarchar (256)  ,

	@Details ntext   
)
AS


				
				INSERT INTO [dbo].[aspnet_WebEvent_Events]
					(
					[EventId]
					,[EventTimeUtc]
					,[EventTime]
					,[EventType]
					,[EventSequence]
					,[EventOccurrence]
					,[EventCode]
					,[EventDetailCode]
					,[Message]
					,[ApplicationPath]
					,[ApplicationVirtualPath]
					,[MachineName]
					,[RequestUrl]
					,[ExceptionType]
					,[Details]
					)
				VALUES
					(
					@EventId
					,@EventTimeUtc
					,@EventTime
					,@EventType
					,@EventSequence
					,@EventOccurrence
					,@EventCode
					,@EventDetailCode
					,@Message
					,@ApplicationPath
					,@ApplicationVirtualPath
					,@MachineName
					,@RequestUrl
					,@ExceptionType
					,@Details
					)
GO
/****** Object:  StoredProcedure [dbo].[aspnet_WebEvent_Events_GetPaged]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets records from the aspnet_WebEvent_Events table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_WebEvent_Events_GetPaged]
(

	@WhereClause varchar (2000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [EventId] char(32) COLLATE database_default  
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([EventId])'
				SET @SQL = @SQL + ' SELECT'
				IF @PageSize > 0
				BEGIN
					SET @SQL = @SQL + ' TOP ' + CONVERT(nvarchar, @PageUpperBound)
				END
				SET @SQL = @SQL + ' [EventId]'
				SET @SQL = @SQL + ' FROM [dbo].[aspnet_WebEvent_Events]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Return paged results
				SELECT O.[EventId], O.[EventTimeUtc], O.[EventTime], O.[EventType], O.[EventSequence], O.[EventOccurrence], O.[EventCode], O.[EventDetailCode], O.[Message], O.[ApplicationPath], O.[ApplicationVirtualPath], O.[MachineName], O.[RequestUrl], O.[ExceptionType], O.[Details]
				FROM
				    [dbo].[aspnet_WebEvent_Events] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[EventId] = PageIndex.[EventId]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[aspnet_WebEvent_Events]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_WebEvent_Events_GetByEventId]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Select records from the aspnet_WebEvent_Events table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_WebEvent_Events_GetByEventId]
(

	@EventId char (32)  
)
AS


				SELECT
					[EventId],
					[EventTimeUtc],
					[EventTime],
					[EventType],
					[EventSequence],
					[EventOccurrence],
					[EventCode],
					[EventDetailCode],
					[Message],
					[ApplicationPath],
					[ApplicationVirtualPath],
					[MachineName],
					[RequestUrl],
					[ExceptionType],
					[Details]
				FROM
					[dbo].[aspnet_WebEvent_Events]
				WHERE
					[EventId] = @EventId
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[aspnet_WebEvent_Events_Get_List]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets all records from the aspnet_WebEvent_Events table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_WebEvent_Events_Get_List]

AS


				
				SELECT
					[EventId],
					[EventTimeUtc],
					[EventTime],
					[EventType],
					[EventSequence],
					[EventOccurrence],
					[EventCode],
					[EventDetailCode],
					[Message],
					[ApplicationPath],
					[ApplicationVirtualPath],
					[MachineName],
					[RequestUrl],
					[ExceptionType],
					[Details]
				FROM
					[dbo].[aspnet_WebEvent_Events]
					
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[aspnet_WebEvent_Events_Find]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Finds records in the aspnet_WebEvent_Events table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_WebEvent_Events_Find]
(

	@SearchUsingOR bit   = null ,

	@EventId char (32)  = null ,

	@EventTimeUtc datetime   = null ,

	@EventTime datetime   = null ,

	@EventType nvarchar (256)  = null ,

	@EventSequence decimal (19, 0)  = null ,

	@EventOccurrence decimal (19, 0)  = null ,

	@EventCode int   = null ,

	@EventDetailCode int   = null ,

	@Message nvarchar (1024)  = null ,

	@ApplicationPath nvarchar (256)  = null ,

	@ApplicationVirtualPath nvarchar (256)  = null ,

	@MachineName nvarchar (256)  = null ,

	@RequestUrl nvarchar (1024)  = null ,

	@ExceptionType nvarchar (256)  = null ,

	@Details ntext   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [EventId]
	, [EventTimeUtc]
	, [EventTime]
	, [EventType]
	, [EventSequence]
	, [EventOccurrence]
	, [EventCode]
	, [EventDetailCode]
	, [Message]
	, [ApplicationPath]
	, [ApplicationVirtualPath]
	, [MachineName]
	, [RequestUrl]
	, [ExceptionType]
	, [Details]
    FROM
	[dbo].[aspnet_WebEvent_Events]
    WHERE 
	 ([EventId] = @EventId OR @EventId IS NULL)
	AND ([EventTimeUtc] = @EventTimeUtc OR @EventTimeUtc IS NULL)
	AND ([EventTime] = @EventTime OR @EventTime IS NULL)
	AND ([EventType] = @EventType OR @EventType IS NULL)
	AND ([EventSequence] = @EventSequence OR @EventSequence IS NULL)
	AND ([EventOccurrence] = @EventOccurrence OR @EventOccurrence IS NULL)
	AND ([EventCode] = @EventCode OR @EventCode IS NULL)
	AND ([EventDetailCode] = @EventDetailCode OR @EventDetailCode IS NULL)
	AND ([Message] = @Message OR @Message IS NULL)
	AND ([ApplicationPath] = @ApplicationPath OR @ApplicationPath IS NULL)
	AND ([ApplicationVirtualPath] = @ApplicationVirtualPath OR @ApplicationVirtualPath IS NULL)
	AND ([MachineName] = @MachineName OR @MachineName IS NULL)
	AND ([RequestUrl] = @RequestUrl OR @RequestUrl IS NULL)
	AND ([ExceptionType] = @ExceptionType OR @ExceptionType IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [EventId]
	, [EventTimeUtc]
	, [EventTime]
	, [EventType]
	, [EventSequence]
	, [EventOccurrence]
	, [EventCode]
	, [EventDetailCode]
	, [Message]
	, [ApplicationPath]
	, [ApplicationVirtualPath]
	, [MachineName]
	, [RequestUrl]
	, [ExceptionType]
	, [Details]
    FROM
	[dbo].[aspnet_WebEvent_Events]
    WHERE 
	 ([EventId] = @EventId AND @EventId is not null)
	OR ([EventTimeUtc] = @EventTimeUtc AND @EventTimeUtc is not null)
	OR ([EventTime] = @EventTime AND @EventTime is not null)
	OR ([EventType] = @EventType AND @EventType is not null)
	OR ([EventSequence] = @EventSequence AND @EventSequence is not null)
	OR ([EventOccurrence] = @EventOccurrence AND @EventOccurrence is not null)
	OR ([EventCode] = @EventCode AND @EventCode is not null)
	OR ([EventDetailCode] = @EventDetailCode AND @EventDetailCode is not null)
	OR ([Message] = @Message AND @Message is not null)
	OR ([ApplicationPath] = @ApplicationPath AND @ApplicationPath is not null)
	OR ([ApplicationVirtualPath] = @ApplicationVirtualPath AND @ApplicationVirtualPath is not null)
	OR ([MachineName] = @MachineName AND @MachineName is not null)
	OR ([RequestUrl] = @RequestUrl AND @RequestUrl is not null)
	OR ([ExceptionType] = @ExceptionType AND @ExceptionType is not null)
	SELECT @@ROWCOUNT			
  END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_WebEvent_Events_Delete]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Deletes a record in the aspnet_WebEvent_Events table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_WebEvent_Events_Delete]
(

	@EventId char (32)  
)
AS


				DELETE FROM [dbo].[aspnet_WebEvent_Events] WITH (ROWLOCK) 
				WHERE
					[EventId] = @EventId
GO
/****** Object:  StoredProcedure [dbo].[aspnet_SchemaVersions_Update]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Updates a record in the aspnet_SchemaVersions table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_SchemaVersions_Update]
(

	@Feature nvarchar (128)  ,

	@OriginalFeature nvarchar (128)  ,

	@CompatibleSchemaVersion nvarchar (128)  ,

	@OriginalCompatibleSchemaVersion nvarchar (128)  ,

	@IsCurrentVersion bit   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[aspnet_SchemaVersions]
				SET
					[Feature] = @Feature
					,[CompatibleSchemaVersion] = @CompatibleSchemaVersion
					,[IsCurrentVersion] = @IsCurrentVersion
				WHERE
[Feature] = @OriginalFeature 
AND [CompatibleSchemaVersion] = @OriginalCompatibleSchemaVersion
GO
/****** Object:  StoredProcedure [dbo].[aspnet_SchemaVersions_Insert]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Inserts a record into the aspnet_SchemaVersions table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_SchemaVersions_Insert]
(

	@Feature nvarchar (128)  ,

	@CompatibleSchemaVersion nvarchar (128)  ,

	@IsCurrentVersion bit   
)
AS


				
				INSERT INTO [dbo].[aspnet_SchemaVersions]
					(
					[Feature]
					,[CompatibleSchemaVersion]
					,[IsCurrentVersion]
					)
				VALUES
					(
					@Feature
					,@CompatibleSchemaVersion
					,@IsCurrentVersion
					)
GO
/****** Object:  StoredProcedure [dbo].[aspnet_SchemaVersions_GetPaged]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets records from the aspnet_SchemaVersions table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_SchemaVersions_GetPaged]
(

	@WhereClause varchar (2000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [Feature] nvarchar(128) COLLATE database_default , [CompatibleSchemaVersion] nvarchar(128) COLLATE database_default  
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([Feature], [CompatibleSchemaVersion])'
				SET @SQL = @SQL + ' SELECT'
				IF @PageSize > 0
				BEGIN
					SET @SQL = @SQL + ' TOP ' + CONVERT(nvarchar, @PageUpperBound)
				END
				SET @SQL = @SQL + ' [Feature], [CompatibleSchemaVersion]'
				SET @SQL = @SQL + ' FROM [dbo].[aspnet_SchemaVersions]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Return paged results
				SELECT O.[Feature], O.[CompatibleSchemaVersion], O.[IsCurrentVersion]
				FROM
				    [dbo].[aspnet_SchemaVersions] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[Feature] = PageIndex.[Feature]
					AND O.[CompatibleSchemaVersion] = PageIndex.[CompatibleSchemaVersion]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[aspnet_SchemaVersions]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_SchemaVersions_GetByFeatureCompatibleSchemaVersion]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Select records from the aspnet_SchemaVersions table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_SchemaVersions_GetByFeatureCompatibleSchemaVersion]
(

	@Feature nvarchar (128)  ,

	@CompatibleSchemaVersion nvarchar (128)  
)
AS


				SELECT
					[Feature],
					[CompatibleSchemaVersion],
					[IsCurrentVersion]
				FROM
					[dbo].[aspnet_SchemaVersions]
				WHERE
					[Feature] = @Feature
					AND [CompatibleSchemaVersion] = @CompatibleSchemaVersion
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[aspnet_SchemaVersions_Get_List]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets all records from the aspnet_SchemaVersions table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_SchemaVersions_Get_List]

AS


				
				SELECT
					[Feature],
					[CompatibleSchemaVersion],
					[IsCurrentVersion]
				FROM
					[dbo].[aspnet_SchemaVersions]
					
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[aspnet_SchemaVersions_Find]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Finds records in the aspnet_SchemaVersions table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_SchemaVersions_Find]
(

	@SearchUsingOR bit   = null ,

	@Feature nvarchar (128)  = null ,

	@CompatibleSchemaVersion nvarchar (128)  = null ,

	@IsCurrentVersion bit   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [Feature]
	, [CompatibleSchemaVersion]
	, [IsCurrentVersion]
    FROM
	[dbo].[aspnet_SchemaVersions]
    WHERE 
	 ([Feature] = @Feature OR @Feature IS NULL)
	AND ([CompatibleSchemaVersion] = @CompatibleSchemaVersion OR @CompatibleSchemaVersion IS NULL)
	AND ([IsCurrentVersion] = @IsCurrentVersion OR @IsCurrentVersion IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [Feature]
	, [CompatibleSchemaVersion]
	, [IsCurrentVersion]
    FROM
	[dbo].[aspnet_SchemaVersions]
    WHERE 
	 ([Feature] = @Feature AND @Feature is not null)
	OR ([CompatibleSchemaVersion] = @CompatibleSchemaVersion AND @CompatibleSchemaVersion is not null)
	OR ([IsCurrentVersion] = @IsCurrentVersion AND @IsCurrentVersion is not null)
	SELECT @@ROWCOUNT			
  END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_SchemaVersions_Delete]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Deletes a record in the aspnet_SchemaVersions table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_SchemaVersions_Delete]
(

	@Feature nvarchar (128)  ,

	@CompatibleSchemaVersion nvarchar (128)  
)
AS


				DELETE FROM [dbo].[aspnet_SchemaVersions] WITH (ROWLOCK) 
				WHERE
					[Feature] = @Feature
					AND [CompatibleSchemaVersion] = @CompatibleSchemaVersion
GO
/****** Object:  Table [dbo].[aspnet_Paths]    Script Date: 10/11/2011 11:07:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[aspnet_Paths](
	[ApplicationId] [uniqueidentifier] NOT NULL,
	[PathId] [uniqueidentifier] NOT NULL,
	[Path] [nvarchar](256) NOT NULL,
	[LoweredPath] [nvarchar](256) NOT NULL,
PRIMARY KEY NONCLUSTERED 
(
	[PathId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Personalization_GetApplicationId]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Personalization_GetApplicationId] (
    @ApplicationName NVARCHAR(256),
    @ApplicationId UNIQUEIDENTIFIER OUT)
AS
BEGIN
    SELECT @ApplicationId = ApplicationId FROM dbo.aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_CheckSchemaVersion]    Script Date: 10/11/2011 11:07:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_CheckSchemaVersion]
    @Feature                   nvarchar(128),
    @CompatibleSchemaVersion   nvarchar(128)
AS
BEGIN
    IF (EXISTS( SELECT  *
                FROM    dbo.aspnet_SchemaVersions
                WHERE   Feature = LOWER( @Feature ) AND
                        CompatibleSchemaVersion = @CompatibleSchemaVersion ))
        RETURN 0

    RETURN 1
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Applications_Update]    Script Date: 10/11/2011 11:07:38 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Updates a record in the aspnet_Applications table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_Applications_Update]
(

	@ApplicationName nvarchar (256)  ,

	@LoweredApplicationName nvarchar (256)  ,

	@ApplicationId uniqueidentifier   ,

	@OriginalApplicationId uniqueidentifier   ,

	@Description nvarchar (256)  
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[aspnet_Applications]
				SET
					[ApplicationName] = @ApplicationName
					,[LoweredApplicationName] = @LoweredApplicationName
					,[ApplicationId] = @ApplicationId
					,[Description] = @Description
				WHERE
[ApplicationId] = @OriginalApplicationId
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Applications_Insert]    Script Date: 10/11/2011 11:07:38 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Inserts a record into the aspnet_Applications table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_Applications_Insert]
(

	@ApplicationName nvarchar (256)  ,

	@LoweredApplicationName nvarchar (256)  ,

	@ApplicationId uniqueidentifier    OUTPUT,

	@Description nvarchar (256)  
)
AS


				
				INSERT INTO [dbo].[aspnet_Applications]
					(
					[ApplicationName]
					,[LoweredApplicationName]
					,[ApplicationId]
					,[Description]
					)
				VALUES
					(
					@ApplicationName
					,@LoweredApplicationName
					,@ApplicationId
					,@Description
					)
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Applications_GetPaged]    Script Date: 10/11/2011 11:07:38 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets records from the aspnet_Applications table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_Applications_GetPaged]
(

	@WhereClause varchar (2000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [ApplicationId] uniqueidentifier 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([ApplicationId])'
				SET @SQL = @SQL + ' SELECT'
				IF @PageSize > 0
				BEGIN
					SET @SQL = @SQL + ' TOP ' + CONVERT(nvarchar, @PageUpperBound)
				END
				SET @SQL = @SQL + ' [ApplicationId]'
				SET @SQL = @SQL + ' FROM [dbo].[aspnet_Applications]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Return paged results
				SELECT O.[ApplicationName], O.[LoweredApplicationName], O.[ApplicationId], O.[Description]
				FROM
				    [dbo].[aspnet_Applications] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[ApplicationId] = PageIndex.[ApplicationId]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[aspnet_Applications]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Applications_GetByLoweredApplicationName]    Script Date: 10/11/2011 11:07:38 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Select records from the aspnet_Applications table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_Applications_GetByLoweredApplicationName]
(

	@LoweredApplicationName nvarchar (256)  
)
AS


				SELECT
					[ApplicationName],
					[LoweredApplicationName],
					[ApplicationId],
					[Description]
				FROM
					[dbo].[aspnet_Applications]
				WHERE
					[LoweredApplicationName] = @LoweredApplicationName
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Applications_GetByApplicationName]    Script Date: 10/11/2011 11:07:38 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Select records from the aspnet_Applications table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_Applications_GetByApplicationName]
(

	@ApplicationName nvarchar (256)  
)
AS


				SELECT
					[ApplicationName],
					[LoweredApplicationName],
					[ApplicationId],
					[Description]
				FROM
					[dbo].[aspnet_Applications]
				WHERE
					[ApplicationName] = @ApplicationName
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Applications_GetByApplicationId]    Script Date: 10/11/2011 11:07:38 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Select records from the aspnet_Applications table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_Applications_GetByApplicationId]
(

	@ApplicationId uniqueidentifier   
)
AS


				SELECT
					[ApplicationName],
					[LoweredApplicationName],
					[ApplicationId],
					[Description]
				FROM
					[dbo].[aspnet_Applications]
				WHERE
					[ApplicationId] = @ApplicationId
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Applications_Get_List]    Script Date: 10/11/2011 11:07:38 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets all records from the aspnet_Applications table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_Applications_Get_List]

AS


				
				SELECT
					[ApplicationName],
					[LoweredApplicationName],
					[ApplicationId],
					[Description]
				FROM
					[dbo].[aspnet_Applications]
					
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Applications_Find]    Script Date: 10/11/2011 11:07:38 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Finds records in the aspnet_Applications table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_Applications_Find]
(

	@SearchUsingOR bit   = null ,

	@ApplicationName nvarchar (256)  = null ,

	@LoweredApplicationName nvarchar (256)  = null ,

	@ApplicationId uniqueidentifier   = null ,

	@Description nvarchar (256)  = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [ApplicationName]
	, [LoweredApplicationName]
	, [ApplicationId]
	, [Description]
    FROM
	[dbo].[aspnet_Applications]
    WHERE 
	 ([ApplicationName] = @ApplicationName OR @ApplicationName IS NULL)
	AND ([LoweredApplicationName] = @LoweredApplicationName OR @LoweredApplicationName IS NULL)
	AND ([ApplicationId] = @ApplicationId OR @ApplicationId IS NULL)
	AND ([Description] = @Description OR @Description IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [ApplicationName]
	, [LoweredApplicationName]
	, [ApplicationId]
	, [Description]
    FROM
	[dbo].[aspnet_Applications]
    WHERE 
	 ([ApplicationName] = @ApplicationName AND @ApplicationName is not null)
	OR ([LoweredApplicationName] = @LoweredApplicationName AND @LoweredApplicationName is not null)
	OR ([ApplicationId] = @ApplicationId AND @ApplicationId is not null)
	OR ([Description] = @Description AND @Description is not null)
	SELECT @@ROWCOUNT			
  END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Applications_Delete]    Script Date: 10/11/2011 11:07:38 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Deletes a record in the aspnet_Applications table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_Applications_Delete]
(

	@ApplicationId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[aspnet_Applications] WITH (ROWLOCK) 
				WHERE
					[ApplicationId] = @ApplicationId
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Applications_CreateApplication]    Script Date: 10/11/2011 11:07:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Applications_CreateApplication]
    @ApplicationName      nvarchar(256),
    @ApplicationId        uniqueidentifier OUTPUT
AS
BEGIN
    SELECT  @ApplicationId = ApplicationId FROM dbo.aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName

    IF(@ApplicationId IS NULL)
    BEGIN
        DECLARE @TranStarted   bit
        SET @TranStarted = 0

        IF( @@TRANCOUNT = 0 )
        BEGIN
	        BEGIN TRANSACTION
	        SET @TranStarted = 1
        END
        ELSE
    	    SET @TranStarted = 0

        SELECT  @ApplicationId = ApplicationId
        FROM dbo.aspnet_Applications WITH (UPDLOCK, HOLDLOCK)
        WHERE LOWER(@ApplicationName) = LoweredApplicationName

        IF(@ApplicationId IS NULL)
        BEGIN
            SELECT  @ApplicationId = NEWID()
            INSERT  dbo.aspnet_Applications (ApplicationId, ApplicationName, LoweredApplicationName)
            VALUES  (@ApplicationId, @ApplicationName, LOWER(@ApplicationName))
        END


        IF( @TranStarted = 1 )
        BEGIN
            IF(@@ERROR = 0)
            BEGIN
	        SET @TranStarted = 0
	        COMMIT TRANSACTION
            END
            ELSE
            BEGIN
                SET @TranStarted = 0
                ROLLBACK TRANSACTION
            END
        END
    END
END
GO
/****** Object:  Table [dbo].[aspnet_Users]    Script Date: 10/11/2011 11:07:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[aspnet_Users](
	[ApplicationId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
	[LoweredUserName] [nvarchar](256) NOT NULL,
	[MobileAlias] [nvarchar](16) NULL,
	[IsAnonymous] [bit] NOT NULL,
	[LastActivityDate] [datetime] NOT NULL,
PRIMARY KEY NONCLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[aspnet_UnRegisterSchemaVersion]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_UnRegisterSchemaVersion]
    @Feature                   nvarchar(128),
    @CompatibleSchemaVersion   nvarchar(128)
AS
BEGIN
    DELETE FROM dbo.aspnet_SchemaVersions
        WHERE   Feature = LOWER(@Feature) AND @CompatibleSchemaVersion = CompatibleSchemaVersion
END
GO
/****** Object:  Table [dbo].[aspnet_Roles]    Script Date: 10/11/2011 11:07:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[aspnet_Roles](
	[ApplicationId] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
	[RoleName] [nvarchar](256) NOT NULL,
	[LoweredRoleName] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](256) NULL,
PRIMARY KEY NONCLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[aspnet_RegisterSchemaVersion]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_RegisterSchemaVersion]
    @Feature                   nvarchar(128),
    @CompatibleSchemaVersion   nvarchar(128),
    @IsCurrentVersion          bit,
    @RemoveIncompatibleSchema  bit
AS
BEGIN
    IF( @RemoveIncompatibleSchema = 1 )
    BEGIN
        DELETE FROM dbo.aspnet_SchemaVersions WHERE Feature = LOWER( @Feature )
    END
    ELSE
    BEGIN
        IF( @IsCurrentVersion = 1 )
        BEGIN
            UPDATE dbo.aspnet_SchemaVersions
            SET IsCurrentVersion = 0
            WHERE Feature = LOWER( @Feature )
        END
    END

    INSERT  dbo.aspnet_SchemaVersions( Feature, CompatibleSchemaVersion, IsCurrentVersion )
    VALUES( LOWER( @Feature ), @CompatibleSchemaVersion, @IsCurrentVersion )
END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ScheduledTasks_Update]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Updates a record in the meanstream_ScheduledTasks table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ScheduledTasks_Update]
(

	@Id uniqueidentifier   ,

	@OriginalId uniqueidentifier   ,

	@Name nvarchar (255)  ,

	@Interval int   ,

	@NextRunTime datetime   ,

	@Status nvarchar (50)  ,

	@StartupType nvarchar (50)  ,

	@LastRunSuccessful bit   ,

	@LastRunResult nvarchar (MAX)  ,

	@LastRunDate datetime   ,

	@CreatedDate datetime   ,

	@LastModifiedDate datetime   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[meanstream_ScheduledTasks]
				SET
					[Id] = @Id
					,[Name] = @Name
					,[Interval] = @Interval
					,[NextRunTime] = @NextRunTime
					,[Status] = @Status
					,[StartupType] = @StartupType
					,[LastRunSuccessful] = @LastRunSuccessful
					,[LastRunResult] = @LastRunResult
					,[LastRunDate] = @LastRunDate
					,[CreatedDate] = @CreatedDate
					,[LastModifiedDate] = @LastModifiedDate
				WHERE
[Id] = @OriginalId
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ScheduledTasks_Insert]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Inserts a record into the meanstream_ScheduledTasks table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ScheduledTasks_Insert]
(

	@Id uniqueidentifier   ,

	@Name nvarchar (255)  ,

	@Interval int   ,

	@NextRunTime datetime   ,

	@Status nvarchar (50)  ,

	@StartupType nvarchar (50)  ,

	@LastRunSuccessful bit   ,

	@LastRunResult nvarchar (MAX)  ,

	@LastRunDate datetime   ,

	@CreatedDate datetime   ,

	@LastModifiedDate datetime   
)
AS


				
				INSERT INTO [dbo].[meanstream_ScheduledTasks]
					(
					[Id]
					,[Name]
					,[Interval]
					,[NextRunTime]
					,[Status]
					,[StartupType]
					,[LastRunSuccessful]
					,[LastRunResult]
					,[LastRunDate]
					,[CreatedDate]
					,[LastModifiedDate]
					)
				VALUES
					(
					@Id
					,@Name
					,@Interval
					,@NextRunTime
					,@Status
					,@StartupType
					,@LastRunSuccessful
					,@LastRunResult
					,@LastRunDate
					,@CreatedDate
					,@LastModifiedDate
					)
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ScheduledTasks_GetPaged]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets records from the meanstream_ScheduledTasks table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ScheduledTasks_GetPaged]
(

	@WhereClause varchar (2000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [Id] uniqueidentifier 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([Id])'
				SET @SQL = @SQL + ' SELECT'
				IF @PageSize > 0
				BEGIN
					SET @SQL = @SQL + ' TOP ' + CONVERT(nvarchar, @PageUpperBound)
				END
				SET @SQL = @SQL + ' [Id]'
				SET @SQL = @SQL + ' FROM [dbo].[meanstream_ScheduledTasks]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Return paged results
				SELECT O.[Id], O.[Name], O.[Interval], O.[NextRunTime], O.[Status], O.[StartupType], O.[LastRunSuccessful], O.[LastRunResult], O.[LastRunDate], O.[CreatedDate], O.[LastModifiedDate]
				FROM
				    [dbo].[meanstream_ScheduledTasks] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[Id] = PageIndex.[Id]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[meanstream_ScheduledTasks]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ScheduledTasks_GetById]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Select records from the meanstream_ScheduledTasks table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ScheduledTasks_GetById]
(

	@Id uniqueidentifier   
)
AS


				SELECT
					[Id],
					[Name],
					[Interval],
					[NextRunTime],
					[Status],
					[StartupType],
					[LastRunSuccessful],
					[LastRunResult],
					[LastRunDate],
					[CreatedDate],
					[LastModifiedDate]
				FROM
					[dbo].[meanstream_ScheduledTasks]
				WHERE
					[Id] = @Id
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ScheduledTasks_Get_List]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets all records from the meanstream_ScheduledTasks table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ScheduledTasks_Get_List]

AS


				
				SELECT
					[Id],
					[Name],
					[Interval],
					[NextRunTime],
					[Status],
					[StartupType],
					[LastRunSuccessful],
					[LastRunResult],
					[LastRunDate],
					[CreatedDate],
					[LastModifiedDate]
				FROM
					[dbo].[meanstream_ScheduledTasks]
					
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ScheduledTasks_Find]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Finds records in the meanstream_ScheduledTasks table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ScheduledTasks_Find]
(

	@SearchUsingOR bit   = null ,

	@Id uniqueidentifier   = null ,

	@Name nvarchar (255)  = null ,

	@Interval int   = null ,

	@NextRunTime datetime   = null ,

	@Status nvarchar (50)  = null ,

	@StartupType nvarchar (50)  = null ,

	@LastRunSuccessful bit   = null ,

	@LastRunResult nvarchar (MAX)  = null ,

	@LastRunDate datetime   = null ,

	@CreatedDate datetime   = null ,

	@LastModifiedDate datetime   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [Id]
	, [Name]
	, [Interval]
	, [NextRunTime]
	, [Status]
	, [StartupType]
	, [LastRunSuccessful]
	, [LastRunResult]
	, [LastRunDate]
	, [CreatedDate]
	, [LastModifiedDate]
    FROM
	[dbo].[meanstream_ScheduledTasks]
    WHERE 
	 ([Id] = @Id OR @Id IS NULL)
	AND ([Name] = @Name OR @Name IS NULL)
	AND ([Interval] = @Interval OR @Interval IS NULL)
	AND ([NextRunTime] = @NextRunTime OR @NextRunTime IS NULL)
	AND ([Status] = @Status OR @Status IS NULL)
	AND ([StartupType] = @StartupType OR @StartupType IS NULL)
	AND ([LastRunSuccessful] = @LastRunSuccessful OR @LastRunSuccessful IS NULL)
	AND ([LastRunResult] = @LastRunResult OR @LastRunResult IS NULL)
	AND ([LastRunDate] = @LastRunDate OR @LastRunDate IS NULL)
	AND ([CreatedDate] = @CreatedDate OR @CreatedDate IS NULL)
	AND ([LastModifiedDate] = @LastModifiedDate OR @LastModifiedDate IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [Id]
	, [Name]
	, [Interval]
	, [NextRunTime]
	, [Status]
	, [StartupType]
	, [LastRunSuccessful]
	, [LastRunResult]
	, [LastRunDate]
	, [CreatedDate]
	, [LastModifiedDate]
    FROM
	[dbo].[meanstream_ScheduledTasks]
    WHERE 
	 ([Id] = @Id AND @Id is not null)
	OR ([Name] = @Name AND @Name is not null)
	OR ([Interval] = @Interval AND @Interval is not null)
	OR ([NextRunTime] = @NextRunTime AND @NextRunTime is not null)
	OR ([Status] = @Status AND @Status is not null)
	OR ([StartupType] = @StartupType AND @StartupType is not null)
	OR ([LastRunSuccessful] = @LastRunSuccessful AND @LastRunSuccessful is not null)
	OR ([LastRunResult] = @LastRunResult AND @LastRunResult is not null)
	OR ([LastRunDate] = @LastRunDate AND @LastRunDate is not null)
	OR ([CreatedDate] = @CreatedDate AND @CreatedDate is not null)
	OR ([LastModifiedDate] = @LastModifiedDate AND @LastModifiedDate is not null)
	SELECT @@ROWCOUNT			
  END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_ScheduledTasks_Delete]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Deletes a record in the meanstream_ScheduledTasks table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_ScheduledTasks_Delete]
(

	@Id uniqueidentifier   
)
AS


				DELETE FROM [dbo].[meanstream_ScheduledTasks] WITH (ROWLOCK) 
				WHERE
					[Id] = @Id
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Roles_Update]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Updates a record in the meanstream_Roles table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Roles_Update]
(

	@Id uniqueidentifier   ,

	@OriginalId uniqueidentifier   ,

	@RoleName nvarchar (50)  ,

	@Description nvarchar (1000)  ,

	@IsPublic bit   ,

	@AutoAssignment bit   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[meanstream_Roles]
				SET
					[Id] = @Id
					,[RoleName] = @RoleName
					,[Description] = @Description
					,[IsPublic] = @IsPublic
					,[AutoAssignment] = @AutoAssignment
				WHERE
[Id] = @OriginalId
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Roles_Insert]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Inserts a record into the meanstream_Roles table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Roles_Insert]
(

	@Id uniqueidentifier   ,

	@RoleName nvarchar (50)  ,

	@Description nvarchar (1000)  ,

	@IsPublic bit   ,

	@AutoAssignment bit   
)
AS


				
				INSERT INTO [dbo].[meanstream_Roles]
					(
					[Id]
					,[RoleName]
					,[Description]
					,[IsPublic]
					,[AutoAssignment]
					)
				VALUES
					(
					@Id
					,@RoleName
					,@Description
					,@IsPublic
					,@AutoAssignment
					)
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Roles_GetPaged]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets records from the meanstream_Roles table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Roles_GetPaged]
(

	@WhereClause varchar (2000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [Id] uniqueidentifier 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([Id])'
				SET @SQL = @SQL + ' SELECT'
				IF @PageSize > 0
				BEGIN
					SET @SQL = @SQL + ' TOP ' + CONVERT(nvarchar, @PageUpperBound)
				END
				SET @SQL = @SQL + ' [Id]'
				SET @SQL = @SQL + ' FROM [dbo].[meanstream_Roles]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Return paged results
				SELECT O.[Id], O.[RoleName], O.[Description], O.[IsPublic], O.[AutoAssignment]
				FROM
				    [dbo].[meanstream_Roles] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[Id] = PageIndex.[Id]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[meanstream_Roles]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Roles_GetById]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Select records from the meanstream_Roles table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Roles_GetById]
(

	@Id uniqueidentifier   
)
AS


				SELECT
					[Id],
					[RoleName],
					[Description],
					[IsPublic],
					[AutoAssignment]
				FROM
					[dbo].[meanstream_Roles]
				WHERE
					[Id] = @Id
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Roles_Get_List]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets all records from the meanstream_Roles table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Roles_Get_List]

AS


				
				SELECT
					[Id],
					[RoleName],
					[Description],
					[IsPublic],
					[AutoAssignment]
				FROM
					[dbo].[meanstream_Roles]
					
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Roles_Find]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Finds records in the meanstream_Roles table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Roles_Find]
(

	@SearchUsingOR bit   = null ,

	@Id uniqueidentifier   = null ,

	@RoleName nvarchar (50)  = null ,

	@Description nvarchar (1000)  = null ,

	@IsPublic bit   = null ,

	@AutoAssignment bit   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [Id]
	, [RoleName]
	, [Description]
	, [IsPublic]
	, [AutoAssignment]
    FROM
	[dbo].[meanstream_Roles]
    WHERE 
	 ([Id] = @Id OR @Id IS NULL)
	AND ([RoleName] = @RoleName OR @RoleName IS NULL)
	AND ([Description] = @Description OR @Description IS NULL)
	AND ([IsPublic] = @IsPublic OR @IsPublic IS NULL)
	AND ([AutoAssignment] = @AutoAssignment OR @AutoAssignment IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [Id]
	, [RoleName]
	, [Description]
	, [IsPublic]
	, [AutoAssignment]
    FROM
	[dbo].[meanstream_Roles]
    WHERE 
	 ([Id] = @Id AND @Id is not null)
	OR ([RoleName] = @RoleName AND @RoleName is not null)
	OR ([Description] = @Description AND @Description is not null)
	OR ([IsPublic] = @IsPublic AND @IsPublic is not null)
	OR ([AutoAssignment] = @AutoAssignment AND @AutoAssignment is not null)
	SELECT @@ROWCOUNT			
  END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Roles_Delete]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Deletes a record in the meanstream_Roles table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Roles_Delete]
(

	@Id uniqueidentifier   
)
AS


				DELETE FROM [dbo].[meanstream_Roles] WITH (ROWLOCK) 
				WHERE
					[Id] = @Id
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Preference_Update]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Updates a record in the meanstream_Preference table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Preference_Update]
(

	@Id uniqueidentifier   ,

	@OriginalId uniqueidentifier   ,

	@PreferenceId uniqueidentifier   ,

	@Name varchar (500)  
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[meanstream_Preference]
				SET
					[Id] = @Id
					,[PreferenceId] = @PreferenceId
					,[Name] = @Name
				WHERE
[Id] = @OriginalId
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Preference_Insert]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Inserts a record into the meanstream_Preference table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Preference_Insert]
(

	@Id uniqueidentifier   ,

	@PreferenceId uniqueidentifier   ,

	@Name varchar (500)  
)
AS


				
				INSERT INTO [dbo].[meanstream_Preference]
					(
					[Id]
					,[PreferenceId]
					,[Name]
					)
				VALUES
					(
					@Id
					,@PreferenceId
					,@Name
					)
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Preference_GetPaged]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets records from the meanstream_Preference table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Preference_GetPaged]
(

	@WhereClause varchar (2000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [Id] uniqueidentifier 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([Id])'
				SET @SQL = @SQL + ' SELECT'
				IF @PageSize > 0
				BEGIN
					SET @SQL = @SQL + ' TOP ' + CONVERT(nvarchar, @PageUpperBound)
				END
				SET @SQL = @SQL + ' [Id]'
				SET @SQL = @SQL + ' FROM [dbo].[meanstream_Preference]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Return paged results
				SELECT O.[Id], O.[PreferenceId], O.[Name]
				FROM
				    [dbo].[meanstream_Preference] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[Id] = PageIndex.[Id]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[meanstream_Preference]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Preference_GetById]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Select records from the meanstream_Preference table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Preference_GetById]
(

	@Id uniqueidentifier   
)
AS


				SELECT
					[Id],
					[PreferenceId],
					[Name]
				FROM
					[dbo].[meanstream_Preference]
				WHERE
					[Id] = @Id
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Preference_Get_List]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets all records from the meanstream_Preference table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Preference_Get_List]

AS


				
				SELECT
					[Id],
					[PreferenceId],
					[Name]
				FROM
					[dbo].[meanstream_Preference]
					
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Preference_Find]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Finds records in the meanstream_Preference table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Preference_Find]
(

	@SearchUsingOR bit   = null ,

	@Id uniqueidentifier   = null ,

	@PreferenceId uniqueidentifier   = null ,

	@Name varchar (500)  = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [Id]
	, [PreferenceId]
	, [Name]
    FROM
	[dbo].[meanstream_Preference]
    WHERE 
	 ([Id] = @Id OR @Id IS NULL)
	AND ([PreferenceId] = @PreferenceId OR @PreferenceId IS NULL)
	AND ([Name] = @Name OR @Name IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [Id]
	, [PreferenceId]
	, [Name]
    FROM
	[dbo].[meanstream_Preference]
    WHERE 
	 ([Id] = @Id AND @Id is not null)
	OR ([PreferenceId] = @PreferenceId AND @PreferenceId is not null)
	OR ([Name] = @Name AND @Name is not null)
	SELECT @@ROWCOUNT			
  END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Preference_Delete]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Deletes a record in the meanstream_Preference table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Preference_Delete]
(

	@Id uniqueidentifier   
)
AS


				DELETE FROM [dbo].[meanstream_Preference] WITH (ROWLOCK) 
				WHERE
					[Id] = @Id
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Portals_Update]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Updates a record in the meanstream_Portals table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Portals_Update]
(

	@Id uniqueidentifier   ,

	@OriginalId uniqueidentifier   ,

	@ApplicationId uniqueidentifier   ,

	@Name nvarchar (255)  ,

	@Domain nvarchar (255)  ,

	@Root nvarchar (255)  ,

	@HomePageUrl nvarchar (255)  ,

	@LoginPageUrl nvarchar (255)  
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[meanstream_Portals]
				SET
					[Id] = @Id
					,[ApplicationId] = @ApplicationId
					,[Name] = @Name
					,[Domain] = @Domain
					,[Root] = @Root
					,[HomePageUrl] = @HomePageUrl
					,[LoginPageUrl] = @LoginPageUrl
				WHERE
[Id] = @OriginalId
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Portals_Insert]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Inserts a record into the meanstream_Portals table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Portals_Insert]
(

	@Id uniqueidentifier   ,

	@ApplicationId uniqueidentifier   ,

	@Name nvarchar (255)  ,

	@Domain nvarchar (255)  ,

	@Root nvarchar (255)  ,

	@HomePageUrl nvarchar (255)  ,

	@LoginPageUrl nvarchar (255)  
)
AS


				
				INSERT INTO [dbo].[meanstream_Portals]
					(
					[Id]
					,[ApplicationId]
					,[Name]
					,[Domain]
					,[Root]
					,[HomePageUrl]
					,[LoginPageUrl]
					)
				VALUES
					(
					@Id
					,@ApplicationId
					,@Name
					,@Domain
					,@Root
					,@HomePageUrl
					,@LoginPageUrl
					)
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Portals_GetPaged]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets records from the meanstream_Portals table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Portals_GetPaged]
(

	@WhereClause varchar (2000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [Id] uniqueidentifier 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([Id])'
				SET @SQL = @SQL + ' SELECT'
				IF @PageSize > 0
				BEGIN
					SET @SQL = @SQL + ' TOP ' + CONVERT(nvarchar, @PageUpperBound)
				END
				SET @SQL = @SQL + ' [Id]'
				SET @SQL = @SQL + ' FROM [dbo].[meanstream_Portals]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Return paged results
				SELECT O.[Id], O.[ApplicationId], O.[Name], O.[Domain], O.[Root], O.[HomePageUrl], O.[LoginPageUrl]
				FROM
				    [dbo].[meanstream_Portals] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[Id] = PageIndex.[Id]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[meanstream_Portals]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Portals_GetById]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Select records from the meanstream_Portals table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Portals_GetById]
(

	@Id uniqueidentifier   
)
AS


				SELECT
					[Id],
					[ApplicationId],
					[Name],
					[Domain],
					[Root],
					[HomePageUrl],
					[LoginPageUrl]
				FROM
					[dbo].[meanstream_Portals]
				WHERE
					[Id] = @Id
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Portals_Get_List]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets all records from the meanstream_Portals table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Portals_Get_List]

AS


				
				SELECT
					[Id],
					[ApplicationId],
					[Name],
					[Domain],
					[Root],
					[HomePageUrl],
					[LoginPageUrl]
				FROM
					[dbo].[meanstream_Portals]
					
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Portals_Find]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Finds records in the meanstream_Portals table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Portals_Find]
(

	@SearchUsingOR bit   = null ,

	@Id uniqueidentifier   = null ,

	@ApplicationId uniqueidentifier   = null ,

	@Name nvarchar (255)  = null ,

	@Domain nvarchar (255)  = null ,

	@Root nvarchar (255)  = null ,

	@HomePageUrl nvarchar (255)  = null ,

	@LoginPageUrl nvarchar (255)  = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [Id]
	, [ApplicationId]
	, [Name]
	, [Domain]
	, [Root]
	, [HomePageUrl]
	, [LoginPageUrl]
    FROM
	[dbo].[meanstream_Portals]
    WHERE 
	 ([Id] = @Id OR @Id IS NULL)
	AND ([ApplicationId] = @ApplicationId OR @ApplicationId IS NULL)
	AND ([Name] = @Name OR @Name IS NULL)
	AND ([Domain] = @Domain OR @Domain IS NULL)
	AND ([Root] = @Root OR @Root IS NULL)
	AND ([HomePageUrl] = @HomePageUrl OR @HomePageUrl IS NULL)
	AND ([LoginPageUrl] = @LoginPageUrl OR @LoginPageUrl IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [Id]
	, [ApplicationId]
	, [Name]
	, [Domain]
	, [Root]
	, [HomePageUrl]
	, [LoginPageUrl]
    FROM
	[dbo].[meanstream_Portals]
    WHERE 
	 ([Id] = @Id AND @Id is not null)
	OR ([ApplicationId] = @ApplicationId AND @ApplicationId is not null)
	OR ([Name] = @Name AND @Name is not null)
	OR ([Domain] = @Domain AND @Domain is not null)
	OR ([Root] = @Root AND @Root is not null)
	OR ([HomePageUrl] = @HomePageUrl AND @HomePageUrl is not null)
	OR ([LoginPageUrl] = @LoginPageUrl AND @LoginPageUrl is not null)
	SELECT @@ROWCOUNT			
  END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Portals_Delete]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Deletes a record in the meanstream_Portals table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Portals_Delete]
(

	@Id uniqueidentifier   
)
AS


				DELETE FROM [dbo].[meanstream_Portals] WITH (ROWLOCK) 
				WHERE
					[Id] = @Id
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Permission_Update]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Updates a record in the meanstream_Permission table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Permission_Update]
(

	@Id uniqueidentifier   ,

	@OriginalId uniqueidentifier   ,

	@PermissionCode varchar (50)  ,

	@PermissionKey varchar (20)  ,

	@PermissionName varchar (50)  
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[meanstream_Permission]
				SET
					[Id] = @Id
					,[PermissionCode] = @PermissionCode
					,[PermissionKey] = @PermissionKey
					,[PermissionName] = @PermissionName
				WHERE
[Id] = @OriginalId
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Permission_Insert]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Inserts a record into the meanstream_Permission table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Permission_Insert]
(

	@Id uniqueidentifier   ,

	@PermissionCode varchar (50)  ,

	@PermissionKey varchar (20)  ,

	@PermissionName varchar (50)  
)
AS


				
				INSERT INTO [dbo].[meanstream_Permission]
					(
					[Id]
					,[PermissionCode]
					,[PermissionKey]
					,[PermissionName]
					)
				VALUES
					(
					@Id
					,@PermissionCode
					,@PermissionKey
					,@PermissionName
					)
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Permission_GetPaged]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets records from the meanstream_Permission table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Permission_GetPaged]
(

	@WhereClause varchar (2000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [Id] uniqueidentifier 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([Id])'
				SET @SQL = @SQL + ' SELECT'
				IF @PageSize > 0
				BEGIN
					SET @SQL = @SQL + ' TOP ' + CONVERT(nvarchar, @PageUpperBound)
				END
				SET @SQL = @SQL + ' [Id]'
				SET @SQL = @SQL + ' FROM [dbo].[meanstream_Permission]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Return paged results
				SELECT O.[Id], O.[PermissionCode], O.[PermissionKey], O.[PermissionName]
				FROM
				    [dbo].[meanstream_Permission] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[Id] = PageIndex.[Id]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[meanstream_Permission]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Permission_GetById]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Select records from the meanstream_Permission table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Permission_GetById]
(

	@Id uniqueidentifier   
)
AS


				SELECT
					[Id],
					[PermissionCode],
					[PermissionKey],
					[PermissionName]
				FROM
					[dbo].[meanstream_Permission]
				WHERE
					[Id] = @Id
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Permission_Get_List]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets all records from the meanstream_Permission table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Permission_Get_List]

AS


				
				SELECT
					[Id],
					[PermissionCode],
					[PermissionKey],
					[PermissionName]
				FROM
					[dbo].[meanstream_Permission]
					
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Permission_Find]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Finds records in the meanstream_Permission table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Permission_Find]
(

	@SearchUsingOR bit   = null ,

	@Id uniqueidentifier   = null ,

	@PermissionCode varchar (50)  = null ,

	@PermissionKey varchar (20)  = null ,

	@PermissionName varchar (50)  = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [Id]
	, [PermissionCode]
	, [PermissionKey]
	, [PermissionName]
    FROM
	[dbo].[meanstream_Permission]
    WHERE 
	 ([Id] = @Id OR @Id IS NULL)
	AND ([PermissionCode] = @PermissionCode OR @PermissionCode IS NULL)
	AND ([PermissionKey] = @PermissionKey OR @PermissionKey IS NULL)
	AND ([PermissionName] = @PermissionName OR @PermissionName IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [Id]
	, [PermissionCode]
	, [PermissionKey]
	, [PermissionName]
    FROM
	[dbo].[meanstream_Permission]
    WHERE 
	 ([Id] = @Id AND @Id is not null)
	OR ([PermissionCode] = @PermissionCode AND @PermissionCode is not null)
	OR ([PermissionKey] = @PermissionKey AND @PermissionKey is not null)
	OR ([PermissionName] = @PermissionName AND @PermissionName is not null)
	SELECT @@ROWCOUNT			
  END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Permission_Delete]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Deletes a record in the meanstream_Permission table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Permission_Delete]
(

	@Id uniqueidentifier   
)
AS


				DELETE FROM [dbo].[meanstream_Permission] WITH (ROWLOCK) 
				WHERE
					[Id] = @Id
GO
/****** Object:  StoredProcedure [dbo].[meanstream_PageVersion_Update]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Updates a record in the meanstream_PageVersion table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_PageVersion_Update]
(

	@Id uniqueidentifier   ,

	@OriginalId uniqueidentifier   ,

	@PageId uniqueidentifier   ,

	@DisplayOrder int   ,

	@PortalId uniqueidentifier   ,

	@Name nvarchar (500)  ,

	@IsVisible bit   ,

	@ParentId uniqueidentifier   ,

	@DisableLink bit   ,

	@Title nvarchar (200)  ,

	@Description nvarchar (500)  ,

	@KeyWords nvarchar (500)  ,

	@IsDeleted bit   ,

	@Url nvarchar (255)  ,

	@Type int   ,

	@SkinId uniqueidentifier   ,

	@StartDate datetime   ,

	@EndDate datetime   ,

	@IsPublished bit   ,

	@CreatedDate datetime   ,

	@LastSavedDate datetime   ,

	@Approved bit   ,

	@IsPublishedVersion bit   ,

	@Author varchar (500)  ,

	@EnableCaching bit   ,

	@EnableViewState bit   ,

	@Index bit   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[meanstream_PageVersion]
				SET
					[Id] = @Id
					,[PageId] = @PageId
					,[DisplayOrder] = @DisplayOrder
					,[PortalId] = @PortalId
					,[Name] = @Name
					,[IsVisible] = @IsVisible
					,[ParentId] = @ParentId
					,[DisableLink] = @DisableLink
					,[Title] = @Title
					,[Description] = @Description
					,[KeyWords] = @KeyWords
					,[IsDeleted] = @IsDeleted
					,[Url] = @Url
					,[Type] = @Type
					,[SkinId] = @SkinId
					,[StartDate] = @StartDate
					,[EndDate] = @EndDate
					,[IsPublished] = @IsPublished
					,[CreatedDate] = @CreatedDate
					,[LastSavedDate] = @LastSavedDate
					,[Approved] = @Approved
					,[IsPublishedVersion] = @IsPublishedVersion
					,[Author] = @Author
					,[EnableCaching] = @EnableCaching
					,[EnableViewState] = @EnableViewState
					,[Index] = @Index
				WHERE
[Id] = @OriginalId
GO
/****** Object:  StoredProcedure [dbo].[meanstream_PageVersion_Insert]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Inserts a record into the meanstream_PageVersion table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_PageVersion_Insert]
(

	@Id uniqueidentifier   ,

	@PageId uniqueidentifier   ,

	@DisplayOrder int   ,

	@PortalId uniqueidentifier   ,

	@Name nvarchar (500)  ,

	@IsVisible bit   ,

	@ParentId uniqueidentifier   ,

	@DisableLink bit   ,

	@Title nvarchar (200)  ,

	@Description nvarchar (500)  ,

	@KeyWords nvarchar (500)  ,

	@IsDeleted bit   ,

	@Url nvarchar (255)  ,

	@Type int   ,

	@SkinId uniqueidentifier   ,

	@StartDate datetime   ,

	@EndDate datetime   ,

	@IsPublished bit   ,

	@CreatedDate datetime   ,

	@LastSavedDate datetime   ,

	@Approved bit   ,

	@IsPublishedVersion bit   ,

	@Author varchar (500)  ,

	@EnableCaching bit   ,

	@EnableViewState bit   ,

	@Index bit   
)
AS


				
				INSERT INTO [dbo].[meanstream_PageVersion]
					(
					[Id]
					,[PageId]
					,[DisplayOrder]
					,[PortalId]
					,[Name]
					,[IsVisible]
					,[ParentId]
					,[DisableLink]
					,[Title]
					,[Description]
					,[KeyWords]
					,[IsDeleted]
					,[Url]
					,[Type]
					,[SkinId]
					,[StartDate]
					,[EndDate]
					,[IsPublished]
					,[CreatedDate]
					,[LastSavedDate]
					,[Approved]
					,[IsPublishedVersion]
					,[Author]
					,[EnableCaching]
					,[EnableViewState]
					,[Index]
					)
				VALUES
					(
					@Id
					,@PageId
					,@DisplayOrder
					,@PortalId
					,@Name
					,@IsVisible
					,@ParentId
					,@DisableLink
					,@Title
					,@Description
					,@KeyWords
					,@IsDeleted
					,@Url
					,@Type
					,@SkinId
					,@StartDate
					,@EndDate
					,@IsPublished
					,@CreatedDate
					,@LastSavedDate
					,@Approved
					,@IsPublishedVersion
					,@Author
					,@EnableCaching
					,@EnableViewState
					,@Index
					)
GO
/****** Object:  StoredProcedure [dbo].[meanstream_PageVersion_GetPaged]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets records from the meanstream_PageVersion table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_PageVersion_GetPaged]
(

	@WhereClause varchar (2000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [Id] uniqueidentifier 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([Id])'
				SET @SQL = @SQL + ' SELECT'
				IF @PageSize > 0
				BEGIN
					SET @SQL = @SQL + ' TOP ' + CONVERT(nvarchar, @PageUpperBound)
				END
				SET @SQL = @SQL + ' [Id]'
				SET @SQL = @SQL + ' FROM [dbo].[meanstream_PageVersion]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Return paged results
				SELECT O.[Id], O.[PageId], O.[DisplayOrder], O.[PortalId], O.[Name], O.[IsVisible], O.[ParentId], O.[DisableLink], O.[Title], O.[Description], O.[KeyWords], O.[IsDeleted], O.[Url], O.[Type], O.[SkinId], O.[StartDate], O.[EndDate], O.[IsPublished], O.[CreatedDate], O.[LastSavedDate], O.[Approved], O.[IsPublishedVersion], O.[Author], O.[EnableCaching], O.[EnableViewState], O.[Index]
				FROM
				    [dbo].[meanstream_PageVersion] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[Id] = PageIndex.[Id]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[meanstream_PageVersion]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_PageVersion_GetById]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Select records from the meanstream_PageVersion table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_PageVersion_GetById]
(

	@Id uniqueidentifier   
)
AS


				SELECT
					[Id],
					[PageId],
					[DisplayOrder],
					[PortalId],
					[Name],
					[IsVisible],
					[ParentId],
					[DisableLink],
					[Title],
					[Description],
					[KeyWords],
					[IsDeleted],
					[Url],
					[Type],
					[SkinId],
					[StartDate],
					[EndDate],
					[IsPublished],
					[CreatedDate],
					[LastSavedDate],
					[Approved],
					[IsPublishedVersion],
					[Author],
					[EnableCaching],
					[EnableViewState],
					[Index]
				FROM
					[dbo].[meanstream_PageVersion]
				WHERE
					[Id] = @Id
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[meanstream_PageVersion_Get_List]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets all records from the meanstream_PageVersion table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_PageVersion_Get_List]

AS


				
				SELECT
					[Id],
					[PageId],
					[DisplayOrder],
					[PortalId],
					[Name],
					[IsVisible],
					[ParentId],
					[DisableLink],
					[Title],
					[Description],
					[KeyWords],
					[IsDeleted],
					[Url],
					[Type],
					[SkinId],
					[StartDate],
					[EndDate],
					[IsPublished],
					[CreatedDate],
					[LastSavedDate],
					[Approved],
					[IsPublishedVersion],
					[Author],
					[EnableCaching],
					[EnableViewState],
					[Index]
				FROM
					[dbo].[meanstream_PageVersion]
					
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[meanstream_PageVersion_Find]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Finds records in the meanstream_PageVersion table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_PageVersion_Find]
(

	@SearchUsingOR bit   = null ,

	@Id uniqueidentifier   = null ,

	@PageId uniqueidentifier   = null ,

	@DisplayOrder int   = null ,

	@PortalId uniqueidentifier   = null ,

	@Name nvarchar (500)  = null ,

	@IsVisible bit   = null ,

	@ParentId uniqueidentifier   = null ,

	@DisableLink bit   = null ,

	@Title nvarchar (200)  = null ,

	@Description nvarchar (500)  = null ,

	@KeyWords nvarchar (500)  = null ,

	@IsDeleted bit   = null ,

	@Url nvarchar (255)  = null ,

	@Type int   = null ,

	@SkinId uniqueidentifier   = null ,

	@StartDate datetime   = null ,

	@EndDate datetime   = null ,

	@IsPublished bit   = null ,

	@CreatedDate datetime   = null ,

	@LastSavedDate datetime   = null ,

	@Approved bit   = null ,

	@IsPublishedVersion bit   = null ,

	@Author varchar (500)  = null ,

	@EnableCaching bit   = null ,

	@EnableViewState bit   = null ,

	@Index bit   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [Id]
	, [PageId]
	, [DisplayOrder]
	, [PortalId]
	, [Name]
	, [IsVisible]
	, [ParentId]
	, [DisableLink]
	, [Title]
	, [Description]
	, [KeyWords]
	, [IsDeleted]
	, [Url]
	, [Type]
	, [SkinId]
	, [StartDate]
	, [EndDate]
	, [IsPublished]
	, [CreatedDate]
	, [LastSavedDate]
	, [Approved]
	, [IsPublishedVersion]
	, [Author]
	, [EnableCaching]
	, [EnableViewState]
	, [Index]
    FROM
	[dbo].[meanstream_PageVersion]
    WHERE 
	 ([Id] = @Id OR @Id IS NULL)
	AND ([PageId] = @PageId OR @PageId IS NULL)
	AND ([DisplayOrder] = @DisplayOrder OR @DisplayOrder IS NULL)
	AND ([PortalId] = @PortalId OR @PortalId IS NULL)
	AND ([Name] = @Name OR @Name IS NULL)
	AND ([IsVisible] = @IsVisible OR @IsVisible IS NULL)
	AND ([ParentId] = @ParentId OR @ParentId IS NULL)
	AND ([DisableLink] = @DisableLink OR @DisableLink IS NULL)
	AND ([Title] = @Title OR @Title IS NULL)
	AND ([Description] = @Description OR @Description IS NULL)
	AND ([KeyWords] = @KeyWords OR @KeyWords IS NULL)
	AND ([IsDeleted] = @IsDeleted OR @IsDeleted IS NULL)
	AND ([Url] = @Url OR @Url IS NULL)
	AND ([Type] = @Type OR @Type IS NULL)
	AND ([SkinId] = @SkinId OR @SkinId IS NULL)
	AND ([StartDate] = @StartDate OR @StartDate IS NULL)
	AND ([EndDate] = @EndDate OR @EndDate IS NULL)
	AND ([IsPublished] = @IsPublished OR @IsPublished IS NULL)
	AND ([CreatedDate] = @CreatedDate OR @CreatedDate IS NULL)
	AND ([LastSavedDate] = @LastSavedDate OR @LastSavedDate IS NULL)
	AND ([Approved] = @Approved OR @Approved IS NULL)
	AND ([IsPublishedVersion] = @IsPublishedVersion OR @IsPublishedVersion IS NULL)
	AND ([Author] = @Author OR @Author IS NULL)
	AND ([EnableCaching] = @EnableCaching OR @EnableCaching IS NULL)
	AND ([EnableViewState] = @EnableViewState OR @EnableViewState IS NULL)
	AND ([Index] = @Index OR @Index IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [Id]
	, [PageId]
	, [DisplayOrder]
	, [PortalId]
	, [Name]
	, [IsVisible]
	, [ParentId]
	, [DisableLink]
	, [Title]
	, [Description]
	, [KeyWords]
	, [IsDeleted]
	, [Url]
	, [Type]
	, [SkinId]
	, [StartDate]
	, [EndDate]
	, [IsPublished]
	, [CreatedDate]
	, [LastSavedDate]
	, [Approved]
	, [IsPublishedVersion]
	, [Author]
	, [EnableCaching]
	, [EnableViewState]
	, [Index]
    FROM
	[dbo].[meanstream_PageVersion]
    WHERE 
	 ([Id] = @Id AND @Id is not null)
	OR ([PageId] = @PageId AND @PageId is not null)
	OR ([DisplayOrder] = @DisplayOrder AND @DisplayOrder is not null)
	OR ([PortalId] = @PortalId AND @PortalId is not null)
	OR ([Name] = @Name AND @Name is not null)
	OR ([IsVisible] = @IsVisible AND @IsVisible is not null)
	OR ([ParentId] = @ParentId AND @ParentId is not null)
	OR ([DisableLink] = @DisableLink AND @DisableLink is not null)
	OR ([Title] = @Title AND @Title is not null)
	OR ([Description] = @Description AND @Description is not null)
	OR ([KeyWords] = @KeyWords AND @KeyWords is not null)
	OR ([IsDeleted] = @IsDeleted AND @IsDeleted is not null)
	OR ([Url] = @Url AND @Url is not null)
	OR ([Type] = @Type AND @Type is not null)
	OR ([SkinId] = @SkinId AND @SkinId is not null)
	OR ([StartDate] = @StartDate AND @StartDate is not null)
	OR ([EndDate] = @EndDate AND @EndDate is not null)
	OR ([IsPublished] = @IsPublished AND @IsPublished is not null)
	OR ([CreatedDate] = @CreatedDate AND @CreatedDate is not null)
	OR ([LastSavedDate] = @LastSavedDate AND @LastSavedDate is not null)
	OR ([Approved] = @Approved AND @Approved is not null)
	OR ([IsPublishedVersion] = @IsPublishedVersion AND @IsPublishedVersion is not null)
	OR ([Author] = @Author AND @Author is not null)
	OR ([EnableCaching] = @EnableCaching AND @EnableCaching is not null)
	OR ([EnableViewState] = @EnableViewState AND @EnableViewState is not null)
	OR ([Index] = @Index AND @Index is not null)
	SELECT @@ROWCOUNT			
  END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_PageVersion_Delete]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Deletes a record in the meanstream_PageVersion table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_PageVersion_Delete]
(

	@Id uniqueidentifier   
)
AS


				DELETE FROM [dbo].[meanstream_PageVersion] WITH (ROWLOCK) 
				WHERE
					[Id] = @Id
GO
/****** Object:  StoredProcedure [dbo].[meanstream_PagePermission_Update]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Updates a record in the meanstream_PagePermission table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_PagePermission_Update]
(

	@Id uniqueidentifier   ,

	@OriginalId uniqueidentifier   ,

	@PageId uniqueidentifier   ,

	@PermissionId uniqueidentifier   ,

	@RoleId uniqueidentifier   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[meanstream_PagePermission]
				SET
					[Id] = @Id
					,[PageId] = @PageId
					,[PermissionId] = @PermissionId
					,[RoleId] = @RoleId
				WHERE
[Id] = @OriginalId
GO
/****** Object:  StoredProcedure [dbo].[meanstream_PagePermission_Insert]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Inserts a record into the meanstream_PagePermission table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_PagePermission_Insert]
(

	@Id uniqueidentifier   ,

	@PageId uniqueidentifier   ,

	@PermissionId uniqueidentifier   ,

	@RoleId uniqueidentifier   
)
AS


				
				INSERT INTO [dbo].[meanstream_PagePermission]
					(
					[Id]
					,[PageId]
					,[PermissionId]
					,[RoleId]
					)
				VALUES
					(
					@Id
					,@PageId
					,@PermissionId
					,@RoleId
					)
GO
/****** Object:  StoredProcedure [dbo].[meanstream_PagePermission_GetPaged]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets records from the meanstream_PagePermission table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_PagePermission_GetPaged]
(

	@WhereClause varchar (2000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [Id] uniqueidentifier 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([Id])'
				SET @SQL = @SQL + ' SELECT'
				IF @PageSize > 0
				BEGIN
					SET @SQL = @SQL + ' TOP ' + CONVERT(nvarchar, @PageUpperBound)
				END
				SET @SQL = @SQL + ' [Id]'
				SET @SQL = @SQL + ' FROM [dbo].[meanstream_PagePermission]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Return paged results
				SELECT O.[Id], O.[PageId], O.[PermissionId], O.[RoleId]
				FROM
				    [dbo].[meanstream_PagePermission] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[Id] = PageIndex.[Id]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[meanstream_PagePermission]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_PagePermission_GetById]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Select records from the meanstream_PagePermission table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_PagePermission_GetById]
(

	@Id uniqueidentifier   
)
AS


				SELECT
					[Id],
					[PageId],
					[PermissionId],
					[RoleId]
				FROM
					[dbo].[meanstream_PagePermission]
				WHERE
					[Id] = @Id
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[meanstream_PagePermission_Get_List]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets all records from the meanstream_PagePermission table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_PagePermission_Get_List]

AS


				
				SELECT
					[Id],
					[PageId],
					[PermissionId],
					[RoleId]
				FROM
					[dbo].[meanstream_PagePermission]
					
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[meanstream_PagePermission_Find]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Finds records in the meanstream_PagePermission table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_PagePermission_Find]
(

	@SearchUsingOR bit   = null ,

	@Id uniqueidentifier   = null ,

	@PageId uniqueidentifier   = null ,

	@PermissionId uniqueidentifier   = null ,

	@RoleId uniqueidentifier   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [Id]
	, [PageId]
	, [PermissionId]
	, [RoleId]
    FROM
	[dbo].[meanstream_PagePermission]
    WHERE 
	 ([Id] = @Id OR @Id IS NULL)
	AND ([PageId] = @PageId OR @PageId IS NULL)
	AND ([PermissionId] = @PermissionId OR @PermissionId IS NULL)
	AND ([RoleId] = @RoleId OR @RoleId IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [Id]
	, [PageId]
	, [PermissionId]
	, [RoleId]
    FROM
	[dbo].[meanstream_PagePermission]
    WHERE 
	 ([Id] = @Id AND @Id is not null)
	OR ([PageId] = @PageId AND @PageId is not null)
	OR ([PermissionId] = @PermissionId AND @PermissionId is not null)
	OR ([RoleId] = @RoleId AND @RoleId is not null)
	SELECT @@ROWCOUNT			
  END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_PagePermission_Delete]    Script Date: 10/11/2011 11:07:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Deletes a record in the meanstream_PagePermission table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_PagePermission_Delete]
(

	@Id uniqueidentifier   
)
AS


				DELETE FROM [dbo].[meanstream_PagePermission] WITH (ROWLOCK) 
				WHERE
					[Id] = @Id
GO
/****** Object:  StoredProcedure [dbo].[meanstream_UserControl_Update]    Script Date: 10/11/2011 11:07:42 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Updates a record in the meanstream_UserControl table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_UserControl_Update]
(

	@Id uniqueidentifier   ,

	@OriginalId uniqueidentifier   ,

	@ModuleId uniqueidentifier   ,

	@ModuleVersionId uniqueidentifier   ,

	@Name nvarchar (255)  ,

	@VirtualPath nvarchar (500)  ,

	@Visible bit   ,

	@CreatedDate datetime   ,

	@CreatedBy uniqueidentifier   ,

	@LastModifiedDate datetime   ,

	@LastModifiedBy uniqueidentifier   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[meanstream_UserControl]
				SET
					[Id] = @Id
					,[ModuleId] = @ModuleId
					,[ModuleVersionId] = @ModuleVersionId
					,[Name] = @Name
					,[VirtualPath] = @VirtualPath
					,[Visible] = @Visible
					,[CreatedDate] = @CreatedDate
					,[CreatedBy] = @CreatedBy
					,[LastModifiedDate] = @LastModifiedDate
					,[LastModifiedBy] = @LastModifiedBy
				WHERE
[Id] = @OriginalId
GO
/****** Object:  StoredProcedure [dbo].[meanstream_UserControl_Insert]    Script Date: 10/11/2011 11:07:42 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Inserts a record into the meanstream_UserControl table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_UserControl_Insert]
(

	@Id uniqueidentifier   ,

	@ModuleId uniqueidentifier   ,

	@ModuleVersionId uniqueidentifier   ,

	@Name nvarchar (255)  ,

	@VirtualPath nvarchar (500)  ,

	@Visible bit   ,

	@CreatedDate datetime   ,

	@CreatedBy uniqueidentifier   ,

	@LastModifiedDate datetime   ,

	@LastModifiedBy uniqueidentifier   
)
AS


				
				INSERT INTO [dbo].[meanstream_UserControl]
					(
					[Id]
					,[ModuleId]
					,[ModuleVersionId]
					,[Name]
					,[VirtualPath]
					,[Visible]
					,[CreatedDate]
					,[CreatedBy]
					,[LastModifiedDate]
					,[LastModifiedBy]
					)
				VALUES
					(
					@Id
					,@ModuleId
					,@ModuleVersionId
					,@Name
					,@VirtualPath
					,@Visible
					,@CreatedDate
					,@CreatedBy
					,@LastModifiedDate
					,@LastModifiedBy
					)
GO
/****** Object:  StoredProcedure [dbo].[meanstream_UserControl_GetPaged]    Script Date: 10/11/2011 11:07:42 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets records from the meanstream_UserControl table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_UserControl_GetPaged]
(

	@WhereClause varchar (2000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [Id] uniqueidentifier 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([Id])'
				SET @SQL = @SQL + ' SELECT'
				IF @PageSize > 0
				BEGIN
					SET @SQL = @SQL + ' TOP ' + CONVERT(nvarchar, @PageUpperBound)
				END
				SET @SQL = @SQL + ' [Id]'
				SET @SQL = @SQL + ' FROM [dbo].[meanstream_UserControl]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Return paged results
				SELECT O.[Id], O.[ModuleId], O.[ModuleVersionId], O.[Name], O.[VirtualPath], O.[Visible], O.[CreatedDate], O.[CreatedBy], O.[LastModifiedDate], O.[LastModifiedBy]
				FROM
				    [dbo].[meanstream_UserControl] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[Id] = PageIndex.[Id]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[meanstream_UserControl]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_UserControl_GetById]    Script Date: 10/11/2011 11:07:42 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Select records from the meanstream_UserControl table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_UserControl_GetById]
(

	@Id uniqueidentifier   
)
AS


				SELECT
					[Id],
					[ModuleId],
					[ModuleVersionId],
					[Name],
					[VirtualPath],
					[Visible],
					[CreatedDate],
					[CreatedBy],
					[LastModifiedDate],
					[LastModifiedBy]
				FROM
					[dbo].[meanstream_UserControl]
				WHERE
					[Id] = @Id
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[meanstream_UserControl_Get_List]    Script Date: 10/11/2011 11:07:42 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets all records from the meanstream_UserControl table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_UserControl_Get_List]

AS


				
				SELECT
					[Id],
					[ModuleId],
					[ModuleVersionId],
					[Name],
					[VirtualPath],
					[Visible],
					[CreatedDate],
					[CreatedBy],
					[LastModifiedDate],
					[LastModifiedBy]
				FROM
					[dbo].[meanstream_UserControl]
					
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[meanstream_UserControl_Find]    Script Date: 10/11/2011 11:07:42 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Finds records in the meanstream_UserControl table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_UserControl_Find]
(

	@SearchUsingOR bit   = null ,

	@Id uniqueidentifier   = null ,

	@ModuleId uniqueidentifier   = null ,

	@ModuleVersionId uniqueidentifier   = null ,

	@Name nvarchar (255)  = null ,

	@VirtualPath nvarchar (500)  = null ,

	@Visible bit   = null ,

	@CreatedDate datetime   = null ,

	@CreatedBy uniqueidentifier   = null ,

	@LastModifiedDate datetime   = null ,

	@LastModifiedBy uniqueidentifier   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [Id]
	, [ModuleId]
	, [ModuleVersionId]
	, [Name]
	, [VirtualPath]
	, [Visible]
	, [CreatedDate]
	, [CreatedBy]
	, [LastModifiedDate]
	, [LastModifiedBy]
    FROM
	[dbo].[meanstream_UserControl]
    WHERE 
	 ([Id] = @Id OR @Id IS NULL)
	AND ([ModuleId] = @ModuleId OR @ModuleId IS NULL)
	AND ([ModuleVersionId] = @ModuleVersionId OR @ModuleVersionId IS NULL)
	AND ([Name] = @Name OR @Name IS NULL)
	AND ([VirtualPath] = @VirtualPath OR @VirtualPath IS NULL)
	AND ([Visible] = @Visible OR @Visible IS NULL)
	AND ([CreatedDate] = @CreatedDate OR @CreatedDate IS NULL)
	AND ([CreatedBy] = @CreatedBy OR @CreatedBy IS NULL)
	AND ([LastModifiedDate] = @LastModifiedDate OR @LastModifiedDate IS NULL)
	AND ([LastModifiedBy] = @LastModifiedBy OR @LastModifiedBy IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [Id]
	, [ModuleId]
	, [ModuleVersionId]
	, [Name]
	, [VirtualPath]
	, [Visible]
	, [CreatedDate]
	, [CreatedBy]
	, [LastModifiedDate]
	, [LastModifiedBy]
    FROM
	[dbo].[meanstream_UserControl]
    WHERE 
	 ([Id] = @Id AND @Id is not null)
	OR ([ModuleId] = @ModuleId AND @ModuleId is not null)
	OR ([ModuleVersionId] = @ModuleVersionId AND @ModuleVersionId is not null)
	OR ([Name] = @Name AND @Name is not null)
	OR ([VirtualPath] = @VirtualPath AND @VirtualPath is not null)
	OR ([Visible] = @Visible AND @Visible is not null)
	OR ([CreatedDate] = @CreatedDate AND @CreatedDate is not null)
	OR ([CreatedBy] = @CreatedBy AND @CreatedBy is not null)
	OR ([LastModifiedDate] = @LastModifiedDate AND @LastModifiedDate is not null)
	OR ([LastModifiedBy] = @LastModifiedBy AND @LastModifiedBy is not null)
	SELECT @@ROWCOUNT			
  END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_UserControl_Delete]    Script Date: 10/11/2011 11:07:42 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Deletes a record in the meanstream_UserControl table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_UserControl_Delete]
(

	@Id uniqueidentifier   
)
AS


				DELETE FROM [dbo].[meanstream_UserControl] WITH (ROWLOCK) 
				WHERE
					[Id] = @Id
GO
/****** Object:  StoredProcedure [dbo].[meanstream_SkinPane_Update]    Script Date: 10/11/2011 11:07:42 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Updates a record in the meanstream_SkinPane table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_SkinPane_Update]
(

	@Id uniqueidentifier   ,

	@OriginalId uniqueidentifier   ,

	@SkinId uniqueidentifier   ,

	@Pane nvarchar (100)  
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[meanstream_SkinPane]
				SET
					[Id] = @Id
					,[SkinId] = @SkinId
					,[Pane] = @Pane
				WHERE
[Id] = @OriginalId
GO
/****** Object:  StoredProcedure [dbo].[meanstream_SkinPane_Insert]    Script Date: 10/11/2011 11:07:42 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Inserts a record into the meanstream_SkinPane table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_SkinPane_Insert]
(

	@Id uniqueidentifier   ,

	@SkinId uniqueidentifier   ,

	@Pane nvarchar (100)  
)
AS


				
				INSERT INTO [dbo].[meanstream_SkinPane]
					(
					[Id]
					,[SkinId]
					,[Pane]
					)
				VALUES
					(
					@Id
					,@SkinId
					,@Pane
					)
GO
/****** Object:  StoredProcedure [dbo].[meanstream_SkinPane_GetPaged]    Script Date: 10/11/2011 11:07:42 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets records from the meanstream_SkinPane table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_SkinPane_GetPaged]
(

	@WhereClause varchar (2000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [Id] uniqueidentifier 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([Id])'
				SET @SQL = @SQL + ' SELECT'
				IF @PageSize > 0
				BEGIN
					SET @SQL = @SQL + ' TOP ' + CONVERT(nvarchar, @PageUpperBound)
				END
				SET @SQL = @SQL + ' [Id]'
				SET @SQL = @SQL + ' FROM [dbo].[meanstream_SkinPane]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Return paged results
				SELECT O.[Id], O.[SkinId], O.[Pane]
				FROM
				    [dbo].[meanstream_SkinPane] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[Id] = PageIndex.[Id]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[meanstream_SkinPane]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_SkinPane_GetById]    Script Date: 10/11/2011 11:07:42 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Select records from the meanstream_SkinPane table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_SkinPane_GetById]
(

	@Id uniqueidentifier   
)
AS


				SELECT
					[Id],
					[SkinId],
					[Pane]
				FROM
					[dbo].[meanstream_SkinPane]
				WHERE
					[Id] = @Id
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[meanstream_SkinPane_Get_List]    Script Date: 10/11/2011 11:07:42 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets all records from the meanstream_SkinPane table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_SkinPane_Get_List]

AS


				
				SELECT
					[Id],
					[SkinId],
					[Pane]
				FROM
					[dbo].[meanstream_SkinPane]
					
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[meanstream_SkinPane_Find]    Script Date: 10/11/2011 11:07:42 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Finds records in the meanstream_SkinPane table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_SkinPane_Find]
(

	@SearchUsingOR bit   = null ,

	@Id uniqueidentifier   = null ,

	@SkinId uniqueidentifier   = null ,

	@Pane nvarchar (100)  = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [Id]
	, [SkinId]
	, [Pane]
    FROM
	[dbo].[meanstream_SkinPane]
    WHERE 
	 ([Id] = @Id OR @Id IS NULL)
	AND ([SkinId] = @SkinId OR @SkinId IS NULL)
	AND ([Pane] = @Pane OR @Pane IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [Id]
	, [SkinId]
	, [Pane]
    FROM
	[dbo].[meanstream_SkinPane]
    WHERE 
	 ([Id] = @Id AND @Id is not null)
	OR ([SkinId] = @SkinId AND @SkinId is not null)
	OR ([Pane] = @Pane AND @Pane is not null)
	SELECT @@ROWCOUNT			
  END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_SkinPane_Delete]    Script Date: 10/11/2011 11:07:42 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Deletes a record in the meanstream_SkinPane table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_SkinPane_Delete]
(

	@Id uniqueidentifier   
)
AS


				DELETE FROM [dbo].[meanstream_SkinPane] WITH (ROWLOCK) 
				WHERE
					[Id] = @Id
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Skins_Update]    Script Date: 10/11/2011 11:07:42 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Updates a record in the meanstream_Skins table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Skins_Update]
(

	@Id uniqueidentifier   ,

	@OriginalId uniqueidentifier   ,

	@PortalId uniqueidentifier   ,

	@SkinRoot nvarchar (50)  ,

	@SkinSrc nvarchar (200)  
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[meanstream_Skins]
				SET
					[Id] = @Id
					,[PortalId] = @PortalId
					,[SkinRoot] = @SkinRoot
					,[SkinSrc] = @SkinSrc
				WHERE
[Id] = @OriginalId
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Skins_Insert]    Script Date: 10/11/2011 11:07:42 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Inserts a record into the meanstream_Skins table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Skins_Insert]
(

	@Id uniqueidentifier   ,

	@PortalId uniqueidentifier   ,

	@SkinRoot nvarchar (50)  ,

	@SkinSrc nvarchar (200)  
)
AS


				
				INSERT INTO [dbo].[meanstream_Skins]
					(
					[Id]
					,[PortalId]
					,[SkinRoot]
					,[SkinSrc]
					)
				VALUES
					(
					@Id
					,@PortalId
					,@SkinRoot
					,@SkinSrc
					)
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Skins_GetPaged]    Script Date: 10/11/2011 11:07:42 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets records from the meanstream_Skins table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Skins_GetPaged]
(

	@WhereClause varchar (2000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [Id] uniqueidentifier 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([Id])'
				SET @SQL = @SQL + ' SELECT'
				IF @PageSize > 0
				BEGIN
					SET @SQL = @SQL + ' TOP ' + CONVERT(nvarchar, @PageUpperBound)
				END
				SET @SQL = @SQL + ' [Id]'
				SET @SQL = @SQL + ' FROM [dbo].[meanstream_Skins]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Return paged results
				SELECT O.[Id], O.[PortalId], O.[SkinRoot], O.[SkinSrc]
				FROM
				    [dbo].[meanstream_Skins] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[Id] = PageIndex.[Id]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[meanstream_Skins]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Skins_GetById]    Script Date: 10/11/2011 11:07:42 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Select records from the meanstream_Skins table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Skins_GetById]
(

	@Id uniqueidentifier   
)
AS


				SELECT
					[Id],
					[PortalId],
					[SkinRoot],
					[SkinSrc]
				FROM
					[dbo].[meanstream_Skins]
				WHERE
					[Id] = @Id
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Skins_Get_List]    Script Date: 10/11/2011 11:07:42 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets all records from the meanstream_Skins table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Skins_Get_List]

AS


				
				SELECT
					[Id],
					[PortalId],
					[SkinRoot],
					[SkinSrc]
				FROM
					[dbo].[meanstream_Skins]
					
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Skins_Find]    Script Date: 10/11/2011 11:07:42 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Finds records in the meanstream_Skins table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Skins_Find]
(

	@SearchUsingOR bit   = null ,

	@Id uniqueidentifier   = null ,

	@PortalId uniqueidentifier   = null ,

	@SkinRoot nvarchar (50)  = null ,

	@SkinSrc nvarchar (200)  = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [Id]
	, [PortalId]
	, [SkinRoot]
	, [SkinSrc]
    FROM
	[dbo].[meanstream_Skins]
    WHERE 
	 ([Id] = @Id OR @Id IS NULL)
	AND ([PortalId] = @PortalId OR @PortalId IS NULL)
	AND ([SkinRoot] = @SkinRoot OR @SkinRoot IS NULL)
	AND ([SkinSrc] = @SkinSrc OR @SkinSrc IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [Id]
	, [PortalId]
	, [SkinRoot]
	, [SkinSrc]
    FROM
	[dbo].[meanstream_Skins]
    WHERE 
	 ([Id] = @Id AND @Id is not null)
	OR ([PortalId] = @PortalId AND @PortalId is not null)
	OR ([SkinRoot] = @SkinRoot AND @SkinRoot is not null)
	OR ([SkinSrc] = @SkinSrc AND @SkinSrc is not null)
	SELECT @@ROWCOUNT			
  END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_Skins_Delete]    Script Date: 10/11/2011 11:07:42 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Deletes a record in the meanstream_Skins table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_Skins_Delete]
(

	@Id uniqueidentifier   
)
AS


				DELETE FROM [dbo].[meanstream_Skins] WITH (ROWLOCK) 
				WHERE
					[Id] = @Id
GO
/****** Object:  View [dbo].[vw_aspnet_Applications]    Script Date: 10/11/2011 11:07:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE VIEW [dbo].[vw_aspnet_Applications]
  AS SELECT [dbo].[aspnet_Applications].[ApplicationName], [dbo].[aspnet_Applications].[LoweredApplicationName], [dbo].[aspnet_Applications].[ApplicationId], [dbo].[aspnet_Applications].[Description]
  FROM [dbo].[aspnet_Applications]
GO
/****** Object:  StoredProcedure [dbo].[meanstream_UserPreference_Update]    Script Date: 10/11/2011 11:07:42 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Updates a record in the meanstream_UserPreference table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_UserPreference_Update]
(

	@Id uniqueidentifier   ,

	@OriginalId uniqueidentifier   ,

	@UserId uniqueidentifier   ,

	@PreferenceId uniqueidentifier   ,

	@ParamValue ntext   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[meanstream_UserPreference]
				SET
					[Id] = @Id
					,[UserId] = @UserId
					,[PreferenceId] = @PreferenceId
					,[ParamValue] = @ParamValue
				WHERE
[Id] = @OriginalId
GO
/****** Object:  StoredProcedure [dbo].[meanstream_UserPreference_Insert]    Script Date: 10/11/2011 11:07:42 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Inserts a record into the meanstream_UserPreference table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_UserPreference_Insert]
(

	@Id uniqueidentifier   ,

	@UserId uniqueidentifier   ,

	@PreferenceId uniqueidentifier   ,

	@ParamValue ntext   
)
AS


				
				INSERT INTO [dbo].[meanstream_UserPreference]
					(
					[Id]
					,[UserId]
					,[PreferenceId]
					,[ParamValue]
					)
				VALUES
					(
					@Id
					,@UserId
					,@PreferenceId
					,@ParamValue
					)
GO
/****** Object:  StoredProcedure [dbo].[meanstream_UserPreference_GetPaged]    Script Date: 10/11/2011 11:07:42 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets records from the meanstream_UserPreference table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_UserPreference_GetPaged]
(

	@WhereClause varchar (2000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [Id] uniqueidentifier 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([Id])'
				SET @SQL = @SQL + ' SELECT'
				IF @PageSize > 0
				BEGIN
					SET @SQL = @SQL + ' TOP ' + CONVERT(nvarchar, @PageUpperBound)
				END
				SET @SQL = @SQL + ' [Id]'
				SET @SQL = @SQL + ' FROM [dbo].[meanstream_UserPreference]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Return paged results
				SELECT O.[Id], O.[UserId], O.[PreferenceId], O.[ParamValue]
				FROM
				    [dbo].[meanstream_UserPreference] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[Id] = PageIndex.[Id]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[meanstream_UserPreference]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_UserPreference_GetById]    Script Date: 10/11/2011 11:07:42 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Select records from the meanstream_UserPreference table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_UserPreference_GetById]
(

	@Id uniqueidentifier   
)
AS


				SELECT
					[Id],
					[UserId],
					[PreferenceId],
					[ParamValue]
				FROM
					[dbo].[meanstream_UserPreference]
				WHERE
					[Id] = @Id
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[meanstream_UserPreference_Get_List]    Script Date: 10/11/2011 11:07:42 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets all records from the meanstream_UserPreference table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_UserPreference_Get_List]

AS


				
				SELECT
					[Id],
					[UserId],
					[PreferenceId],
					[ParamValue]
				FROM
					[dbo].[meanstream_UserPreference]
					
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[meanstream_UserPreference_Find]    Script Date: 10/11/2011 11:07:42 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Finds records in the meanstream_UserPreference table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_UserPreference_Find]
(

	@SearchUsingOR bit   = null ,

	@Id uniqueidentifier   = null ,

	@UserId uniqueidentifier   = null ,

	@PreferenceId uniqueidentifier   = null ,

	@ParamValue ntext   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [Id]
	, [UserId]
	, [PreferenceId]
	, [ParamValue]
    FROM
	[dbo].[meanstream_UserPreference]
    WHERE 
	 ([Id] = @Id OR @Id IS NULL)
	AND ([UserId] = @UserId OR @UserId IS NULL)
	AND ([PreferenceId] = @PreferenceId OR @PreferenceId IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [Id]
	, [UserId]
	, [PreferenceId]
	, [ParamValue]
    FROM
	[dbo].[meanstream_UserPreference]
    WHERE 
	 ([Id] = @Id AND @Id is not null)
	OR ([UserId] = @UserId AND @UserId is not null)
	OR ([PreferenceId] = @PreferenceId AND @PreferenceId is not null)
	SELECT @@ROWCOUNT			
  END
GO
/****** Object:  StoredProcedure [dbo].[meanstream_UserPreference_Delete]    Script Date: 10/11/2011 11:07:42 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Deletes a record in the meanstream_UserPreference table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[meanstream_UserPreference_Delete]
(

	@Id uniqueidentifier   
)
AS


				DELETE FROM [dbo].[meanstream_UserPreference] WITH (ROWLOCK) 
				WHERE
					[Id] = @Id
GO
/****** Object:  View [dbo].[vw_meanstream_RecentEdits]    Script Date: 10/11/2011 11:07:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_meanstream_RecentEdits]
AS
SELECT     dbo.meanstream_ModuleDefinitions.Id, dbo.meanstream_ModuleDefinitions.FriendlyName, dbo.meanstream_ModuleVersion.Id AS ModuleVersionId, 
                      dbo.meanstream_ModuleVersion.Title, dbo.meanstream_ModuleVersion.LastModifiedDate, dbo.meanstream_PageVersion.Id AS PageVersionId, 
                      dbo.meanstream_PageVersion.PageId, dbo.meanstream_PageVersion.Name, dbo.meanstream_PageVersion.Url, 
                      dbo.meanstream_PageVersion.IsPublishedVersion, dbo.meanstream_ModuleVersion.LastModifiedBy, 
                      dbo.meanstream_PageVersion.IsPublished
FROM         dbo.meanstream_ModuleDefinitions INNER JOIN
                      dbo.meanstream_ModuleVersion ON dbo.meanstream_ModuleDefinitions.Id = dbo.meanstream_ModuleVersion.ModuleDefId INNER JOIN
                      dbo.meanstream_PageVersion ON dbo.meanstream_ModuleVersion.PageVersionId = dbo.meanstream_PageVersion.Id
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "meanstream_ModuleDefinitions"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 114
               Right = 189
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "meanstream_ModuleVersion"
            Begin Extent = 
               Top = 6
               Left = 227
               Bottom = 114
               Right = 390
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "meanstream_PageVersion"
            Begin Extent = 
               Top = 6
               Left = 428
               Bottom = 114
               Right = 597
            End
            DisplayFlags = 280
            TopColumn = 22
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 13
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vw_meanstream_RecentEdits'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vw_meanstream_RecentEdits'
GO
/****** Object:  StoredProcedure [dbo].[vw_meanstream_RecentEdits_Get_List]    Script Date: 10/11/2011 11:07:42 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets all records from the vw_meanstream_RecentEdits view
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[vw_meanstream_RecentEdits_Get_List]

AS


				
				SELECT
					[Id],
					[FriendlyName],
					[ModuleVersionId],
					[Title],
					[LastModifiedDate],
					[PageVersionId],
					[PageId],
					[Name],
					[Url],
					[IsPublishedVersion],
					[LastModifiedBy],
					[IsPublished]
				FROM
					[dbo].[vw_meanstream_RecentEdits]
					
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[vw_aspnet_Applications_Get_List]    Script Date: 10/11/2011 11:07:42 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets all records from the vw_aspnet_Applications view
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[vw_aspnet_Applications_Get_List]

AS


				
				SELECT
					[ApplicationName],
					[LoweredApplicationName],
					[ApplicationId],
					[Description]
				FROM
					[dbo].[vw_aspnet_Applications]
					
				SELECT @@ROWCOUNT
GO
/****** Object:  View [dbo].[vw_aspnet_WebPartState_Paths]    Script Date: 10/11/2011 11:07:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE VIEW [dbo].[vw_aspnet_WebPartState_Paths]
  AS SELECT [dbo].[aspnet_Paths].[ApplicationId], [dbo].[aspnet_Paths].[PathId], [dbo].[aspnet_Paths].[Path], [dbo].[aspnet_Paths].[LoweredPath]
  FROM [dbo].[aspnet_Paths]
GO
/****** Object:  View [dbo].[vw_aspnet_Users]    Script Date: 10/11/2011 11:07:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE VIEW [dbo].[vw_aspnet_Users]
  AS SELECT [dbo].[aspnet_Users].[ApplicationId], [dbo].[aspnet_Users].[UserId], [dbo].[aspnet_Users].[UserName], [dbo].[aspnet_Users].[LoweredUserName], [dbo].[aspnet_Users].[MobileAlias], [dbo].[aspnet_Users].[IsAnonymous], [dbo].[aspnet_Users].[LastActivityDate]
  FROM [dbo].[aspnet_Users]
GO
/****** Object:  View [dbo].[vw_aspnet_Roles]    Script Date: 10/11/2011 11:07:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE VIEW [dbo].[vw_aspnet_Roles]
  AS SELECT [dbo].[aspnet_Roles].[ApplicationId], [dbo].[aspnet_Roles].[RoleId], [dbo].[aspnet_Roles].[RoleName], [dbo].[aspnet_Roles].[LoweredRoleName], [dbo].[aspnet_Roles].[Description]
  FROM [dbo].[aspnet_Roles]
GO
/****** Object:  Table [dbo].[aspnet_PersonalizationPerUser]    Script Date: 10/11/2011 11:07:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[aspnet_PersonalizationPerUser](
	[Id] [uniqueidentifier] NOT NULL,
	[PathId] [uniqueidentifier] NULL,
	[UserId] [uniqueidentifier] NULL,
	[PageSettings] [image] NOT NULL,
	[LastUpdatedDate] [datetime] NOT NULL,
PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[aspnet_Profile]    Script Date: 10/11/2011 11:07:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[aspnet_Profile](
	[UserId] [uniqueidentifier] NOT NULL,
	[PropertyNames] [ntext] NOT NULL,
	[PropertyValuesString] [ntext] NOT NULL,
	[PropertyValuesBinary] [image] NOT NULL,
	[LastUpdatedDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Roles_Update]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Updates a record in the aspnet_Roles table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_Roles_Update]
(

	@ApplicationId uniqueidentifier   ,

	@RoleId uniqueidentifier   ,

	@OriginalRoleId uniqueidentifier   ,

	@RoleName nvarchar (256)  ,

	@LoweredRoleName nvarchar (256)  ,

	@Description nvarchar (256)  
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[aspnet_Roles]
				SET
					[ApplicationId] = @ApplicationId
					,[RoleId] = @RoleId
					,[RoleName] = @RoleName
					,[LoweredRoleName] = @LoweredRoleName
					,[Description] = @Description
				WHERE
[RoleId] = @OriginalRoleId
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Roles_RoleExists]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Roles_RoleExists]
    @ApplicationName  nvarchar(256),
    @RoleName         nvarchar(256)
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN(0)
    IF (EXISTS (SELECT RoleName FROM dbo.aspnet_Roles WHERE LOWER(@RoleName) = LoweredRoleName AND ApplicationId = @ApplicationId ))
        RETURN(1)
    ELSE
        RETURN(0)
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Roles_Insert]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Inserts a record into the aspnet_Roles table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_Roles_Insert]
(

	@ApplicationId uniqueidentifier   ,

	@RoleId uniqueidentifier    OUTPUT,

	@RoleName nvarchar (256)  ,

	@LoweredRoleName nvarchar (256)  ,

	@Description nvarchar (256)  
)
AS


				
				INSERT INTO [dbo].[aspnet_Roles]
					(
					[ApplicationId]
					,[RoleId]
					,[RoleName]
					,[LoweredRoleName]
					,[Description]
					)
				VALUES
					(
					@ApplicationId
					,@RoleId
					,@RoleName
					,@LoweredRoleName
					,@Description
					)
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Roles_GetPaged]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets records from the aspnet_Roles table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_Roles_GetPaged]
(

	@WhereClause varchar (2000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [RoleId] uniqueidentifier 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([RoleId])'
				SET @SQL = @SQL + ' SELECT'
				IF @PageSize > 0
				BEGIN
					SET @SQL = @SQL + ' TOP ' + CONVERT(nvarchar, @PageUpperBound)
				END
				SET @SQL = @SQL + ' [RoleId]'
				SET @SQL = @SQL + ' FROM [dbo].[aspnet_Roles]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Return paged results
				SELECT O.[ApplicationId], O.[RoleId], O.[RoleName], O.[LoweredRoleName], O.[Description]
				FROM
				    [dbo].[aspnet_Roles] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[RoleId] = PageIndex.[RoleId]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[aspnet_Roles]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
GO
/****** Object:  Table [dbo].[aspnet_Membership]    Script Date: 10/11/2011 11:07:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[aspnet_Membership](
	[ApplicationId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Password] [nvarchar](128) NOT NULL,
	[PasswordFormat] [int] NOT NULL,
	[PasswordSalt] [nvarchar](128) NOT NULL,
	[MobilePIN] [nvarchar](16) NULL,
	[Email] [nvarchar](256) NULL,
	[LoweredEmail] [nvarchar](256) NULL,
	[PasswordQuestion] [nvarchar](256) NULL,
	[PasswordAnswer] [nvarchar](128) NULL,
	[IsApproved] [bit] NOT NULL,
	[IsLockedOut] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[LastLoginDate] [datetime] NOT NULL,
	[LastPasswordChangedDate] [datetime] NOT NULL,
	[LastLockoutDate] [datetime] NOT NULL,
	[FailedPasswordAttemptCount] [int] NOT NULL,
	[FailedPasswordAttemptWindowStart] [datetime] NOT NULL,
	[FailedPasswordAnswerAttemptCount] [int] NOT NULL,
	[FailedPasswordAnswerAttemptWindowStart] [datetime] NOT NULL,
	[Comment] [ntext] NULL,
PRIMARY KEY NONCLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Paths_Update]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Updates a record in the aspnet_Paths table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_Paths_Update]
(

	@ApplicationId uniqueidentifier   ,

	@PathId uniqueidentifier   ,

	@OriginalPathId uniqueidentifier   ,

	@Path nvarchar (256)  ,

	@LoweredPath nvarchar (256)  
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[aspnet_Paths]
				SET
					[ApplicationId] = @ApplicationId
					,[PathId] = @PathId
					,[Path] = @Path
					,[LoweredPath] = @LoweredPath
				WHERE
[PathId] = @OriginalPathId
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Paths_Insert]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Inserts a record into the aspnet_Paths table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_Paths_Insert]
(

	@ApplicationId uniqueidentifier   ,

	@PathId uniqueidentifier    OUTPUT,

	@Path nvarchar (256)  ,

	@LoweredPath nvarchar (256)  
)
AS


				
				INSERT INTO [dbo].[aspnet_Paths]
					(
					[ApplicationId]
					,[PathId]
					,[Path]
					,[LoweredPath]
					)
				VALUES
					(
					@ApplicationId
					,@PathId
					,@Path
					,@LoweredPath
					)
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Paths_GetPaged]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets records from the aspnet_Paths table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_Paths_GetPaged]
(

	@WhereClause varchar (2000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [PathId] uniqueidentifier 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([PathId])'
				SET @SQL = @SQL + ' SELECT'
				IF @PageSize > 0
				BEGIN
					SET @SQL = @SQL + ' TOP ' + CONVERT(nvarchar, @PageUpperBound)
				END
				SET @SQL = @SQL + ' [PathId]'
				SET @SQL = @SQL + ' FROM [dbo].[aspnet_Paths]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Return paged results
				SELECT O.[ApplicationId], O.[PathId], O.[Path], O.[LoweredPath]
				FROM
				    [dbo].[aspnet_Paths] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[PathId] = PageIndex.[PathId]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[aspnet_Paths]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Paths_GetByPathId]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Select records from the aspnet_Paths table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_Paths_GetByPathId]
(

	@PathId uniqueidentifier   
)
AS


				SELECT
					[ApplicationId],
					[PathId],
					[Path],
					[LoweredPath]
				FROM
					[dbo].[aspnet_Paths]
				WHERE
					[PathId] = @PathId
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Paths_GetByApplicationIdLoweredPath]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Select records from the aspnet_Paths table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_Paths_GetByApplicationIdLoweredPath]
(

	@ApplicationId uniqueidentifier   ,

	@LoweredPath nvarchar (256)  
)
AS


				SELECT
					[ApplicationId],
					[PathId],
					[Path],
					[LoweredPath]
				FROM
					[dbo].[aspnet_Paths]
				WHERE
					[ApplicationId] = @ApplicationId
					AND [LoweredPath] = @LoweredPath
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Paths_GetByApplicationId]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Select records from the aspnet_Paths table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_Paths_GetByApplicationId]
(

	@ApplicationId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[ApplicationId],
					[PathId],
					[Path],
					[LoweredPath]
				FROM
					[dbo].[aspnet_Paths]
				WHERE
					[ApplicationId] = @ApplicationId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Paths_Get_List]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets all records from the aspnet_Paths table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_Paths_Get_List]

AS


				
				SELECT
					[ApplicationId],
					[PathId],
					[Path],
					[LoweredPath]
				FROM
					[dbo].[aspnet_Paths]
					
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Paths_Find]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Finds records in the aspnet_Paths table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_Paths_Find]
(

	@SearchUsingOR bit   = null ,

	@ApplicationId uniqueidentifier   = null ,

	@PathId uniqueidentifier   = null ,

	@Path nvarchar (256)  = null ,

	@LoweredPath nvarchar (256)  = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [ApplicationId]
	, [PathId]
	, [Path]
	, [LoweredPath]
    FROM
	[dbo].[aspnet_Paths]
    WHERE 
	 ([ApplicationId] = @ApplicationId OR @ApplicationId IS NULL)
	AND ([PathId] = @PathId OR @PathId IS NULL)
	AND ([Path] = @Path OR @Path IS NULL)
	AND ([LoweredPath] = @LoweredPath OR @LoweredPath IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [ApplicationId]
	, [PathId]
	, [Path]
	, [LoweredPath]
    FROM
	[dbo].[aspnet_Paths]
    WHERE 
	 ([ApplicationId] = @ApplicationId AND @ApplicationId is not null)
	OR ([PathId] = @PathId AND @PathId is not null)
	OR ([Path] = @Path AND @Path is not null)
	OR ([LoweredPath] = @LoweredPath AND @LoweredPath is not null)
	SELECT @@ROWCOUNT			
  END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Paths_Delete]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Deletes a record in the aspnet_Paths table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_Paths_Delete]
(

	@PathId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[aspnet_Paths] WITH (ROWLOCK) 
				WHERE
					[PathId] = @PathId
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Paths_CreatePath]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Paths_CreatePath]
    @ApplicationId UNIQUEIDENTIFIER,
    @Path           NVARCHAR(256),
    @PathId         UNIQUEIDENTIFIER OUTPUT
AS
BEGIN
    BEGIN TRANSACTION
    IF (NOT EXISTS(SELECT * FROM dbo.aspnet_Paths WHERE LoweredPath = LOWER(@Path) AND ApplicationId = @ApplicationId))
    BEGIN
        INSERT dbo.aspnet_Paths (ApplicationId, Path, LoweredPath) VALUES (@ApplicationId, @Path, LOWER(@Path))
    END
    COMMIT TRANSACTION
    SELECT @PathId = PathId FROM dbo.aspnet_Paths WHERE LOWER(@Path) = LoweredPath AND ApplicationId = @ApplicationId
END
GO
/****** Object:  Table [dbo].[aspnet_PersonalizationAllUsers]    Script Date: 10/11/2011 11:07:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[aspnet_PersonalizationAllUsers](
	[PathId] [uniqueidentifier] NOT NULL,
	[PageSettings] [image] NOT NULL,
	[LastUpdatedDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PathId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Roles_Delete]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Deletes a record in the aspnet_Roles table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_Roles_Delete]
(

	@RoleId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[aspnet_Roles] WITH (ROWLOCK) 
				WHERE
					[RoleId] = @RoleId
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Roles_CreateRole]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Roles_CreateRole]
    @ApplicationName  nvarchar(256),
    @RoleName         nvarchar(256)
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL

    DECLARE @ErrorCode     int
    SET @ErrorCode = 0

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
        BEGIN TRANSACTION
        SET @TranStarted = 1
    END
    ELSE
        SET @TranStarted = 0

    EXEC dbo.aspnet_Applications_CreateApplication @ApplicationName, @ApplicationId OUTPUT

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF (EXISTS(SELECT RoleId FROM dbo.aspnet_Roles WHERE LoweredRoleName = LOWER(@RoleName) AND ApplicationId = @ApplicationId))
    BEGIN
        SET @ErrorCode = 1
        GOTO Cleanup
    END

    INSERT INTO dbo.aspnet_Roles
                (ApplicationId, RoleName, LoweredRoleName)
         VALUES (@ApplicationId, @RoleName, LOWER(@RoleName))

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
        COMMIT TRANSACTION
    END

    RETURN(0)

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
        ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode

END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Roles_GetByRoleId]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Select records from the aspnet_Roles table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_Roles_GetByRoleId]
(

	@RoleId uniqueidentifier   
)
AS


				SELECT
					[ApplicationId],
					[RoleId],
					[RoleName],
					[LoweredRoleName],
					[Description]
				FROM
					[dbo].[aspnet_Roles]
				WHERE
					[RoleId] = @RoleId
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Roles_GetByApplicationIdLoweredRoleName]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Select records from the aspnet_Roles table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_Roles_GetByApplicationIdLoweredRoleName]
(

	@ApplicationId uniqueidentifier   ,

	@LoweredRoleName nvarchar (256)  
)
AS


				SELECT
					[ApplicationId],
					[RoleId],
					[RoleName],
					[LoweredRoleName],
					[Description]
				FROM
					[dbo].[aspnet_Roles]
				WHERE
					[ApplicationId] = @ApplicationId
					AND [LoweredRoleName] = @LoweredRoleName
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Roles_GetByApplicationId]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Select records from the aspnet_Roles table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_Roles_GetByApplicationId]
(

	@ApplicationId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[ApplicationId],
					[RoleId],
					[RoleName],
					[LoweredRoleName],
					[Description]
				FROM
					[dbo].[aspnet_Roles]
				WHERE
					[ApplicationId] = @ApplicationId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Roles_GetAllRoles]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Roles_GetAllRoles] (
    @ApplicationName           nvarchar(256))
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN
    SELECT RoleName
    FROM   dbo.aspnet_Roles WHERE ApplicationId = @ApplicationId
    ORDER BY RoleName
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Roles_Get_List]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets all records from the aspnet_Roles table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_Roles_Get_List]

AS


				
				SELECT
					[ApplicationId],
					[RoleId],
					[RoleName],
					[LoweredRoleName],
					[Description]
				FROM
					[dbo].[aspnet_Roles]
					
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Roles_Find]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Finds records in the aspnet_Roles table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_Roles_Find]
(

	@SearchUsingOR bit   = null ,

	@ApplicationId uniqueidentifier   = null ,

	@RoleId uniqueidentifier   = null ,

	@RoleName nvarchar (256)  = null ,

	@LoweredRoleName nvarchar (256)  = null ,

	@Description nvarchar (256)  = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [ApplicationId]
	, [RoleId]
	, [RoleName]
	, [LoweredRoleName]
	, [Description]
    FROM
	[dbo].[aspnet_Roles]
    WHERE 
	 ([ApplicationId] = @ApplicationId OR @ApplicationId IS NULL)
	AND ([RoleId] = @RoleId OR @RoleId IS NULL)
	AND ([RoleName] = @RoleName OR @RoleName IS NULL)
	AND ([LoweredRoleName] = @LoweredRoleName OR @LoweredRoleName IS NULL)
	AND ([Description] = @Description OR @Description IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [ApplicationId]
	, [RoleId]
	, [RoleName]
	, [LoweredRoleName]
	, [Description]
    FROM
	[dbo].[aspnet_Roles]
    WHERE 
	 ([ApplicationId] = @ApplicationId AND @ApplicationId is not null)
	OR ([RoleId] = @RoleId AND @RoleId is not null)
	OR ([RoleName] = @RoleName AND @RoleName is not null)
	OR ([LoweredRoleName] = @LoweredRoleName AND @LoweredRoleName is not null)
	OR ([Description] = @Description AND @Description is not null)
	SELECT @@ROWCOUNT			
  END
GO
/****** Object:  Table [dbo].[aspnet_UsersInRoles]    Script Date: 10/11/2011 11:07:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[aspnet_UsersInRoles](
	[UserId] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Users_Update]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Updates a record in the aspnet_Users table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_Users_Update]
(

	@ApplicationId uniqueidentifier   ,

	@UserId uniqueidentifier   ,

	@OriginalUserId uniqueidentifier   ,

	@UserName nvarchar (256)  ,

	@LoweredUserName nvarchar (256)  ,

	@MobileAlias nvarchar (16)  ,

	@IsAnonymous bit   ,

	@LastActivityDate datetime   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[aspnet_Users]
				SET
					[ApplicationId] = @ApplicationId
					,[UserId] = @UserId
					,[UserName] = @UserName
					,[LoweredUserName] = @LoweredUserName
					,[MobileAlias] = @MobileAlias
					,[IsAnonymous] = @IsAnonymous
					,[LastActivityDate] = @LastActivityDate
				WHERE
[UserId] = @OriginalUserId
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Users_Insert]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Inserts a record into the aspnet_Users table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_Users_Insert]
(

	@ApplicationId uniqueidentifier   ,

	@UserId uniqueidentifier    OUTPUT,

	@UserName nvarchar (256)  ,

	@LoweredUserName nvarchar (256)  ,

	@MobileAlias nvarchar (16)  ,

	@IsAnonymous bit   ,

	@LastActivityDate datetime   
)
AS


				
				INSERT INTO [dbo].[aspnet_Users]
					(
					[ApplicationId]
					,[UserId]
					,[UserName]
					,[LoweredUserName]
					,[MobileAlias]
					,[IsAnonymous]
					,[LastActivityDate]
					)
				VALUES
					(
					@ApplicationId
					,@UserId
					,@UserName
					,@LoweredUserName
					,@MobileAlias
					,@IsAnonymous
					,@LastActivityDate
					)
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Users_GetPaged]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets records from the aspnet_Users table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_Users_GetPaged]
(

	@WhereClause varchar (2000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [UserId] uniqueidentifier 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([UserId])'
				SET @SQL = @SQL + ' SELECT'
				IF @PageSize > 0
				BEGIN
					SET @SQL = @SQL + ' TOP ' + CONVERT(nvarchar, @PageUpperBound)
				END
				SET @SQL = @SQL + ' [UserId]'
				SET @SQL = @SQL + ' FROM [dbo].[aspnet_Users]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Return paged results
				SELECT O.[ApplicationId], O.[UserId], O.[UserName], O.[LoweredUserName], O.[MobileAlias], O.[IsAnonymous], O.[LastActivityDate]
				FROM
				    [dbo].[aspnet_Users] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[UserId] = PageIndex.[UserId]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[aspnet_Users]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Users_GetByUserId]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Select records from the aspnet_Users table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_Users_GetByUserId]
(

	@UserId uniqueidentifier   
)
AS


				SELECT
					[ApplicationId],
					[UserId],
					[UserName],
					[LoweredUserName],
					[MobileAlias],
					[IsAnonymous],
					[LastActivityDate]
				FROM
					[dbo].[aspnet_Users]
				WHERE
					[UserId] = @UserId
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Users_Delete]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Deletes a record in the aspnet_Users table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_Users_Delete]
(

	@UserId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[aspnet_Users] WITH (ROWLOCK) 
				WHERE
					[UserId] = @UserId
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Users_CreateUser]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Users_CreateUser]
    @ApplicationId    uniqueidentifier,
    @UserName         nvarchar(256),
    @IsUserAnonymous  bit,
    @LastActivityDate DATETIME,
    @UserId           uniqueidentifier OUTPUT
AS
BEGIN
    IF( @UserId IS NULL )
        SELECT @UserId = NEWID()
    ELSE
    BEGIN
        IF( EXISTS( SELECT UserId FROM dbo.aspnet_Users
                    WHERE @UserId = UserId ) )
            RETURN -1
    END

    INSERT dbo.aspnet_Users (ApplicationId, UserId, UserName, LoweredUserName, IsAnonymous, LastActivityDate)
    VALUES (@ApplicationId, @UserId, @UserName, LOWER(@UserName), @IsUserAnonymous, @LastActivityDate)

    RETURN 0
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Users_GetByApplicationIdLoweredUserName]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Select records from the aspnet_Users table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_Users_GetByApplicationIdLoweredUserName]
(

	@ApplicationId uniqueidentifier   ,

	@LoweredUserName nvarchar (256)  
)
AS


				SELECT
					[ApplicationId],
					[UserId],
					[UserName],
					[LoweredUserName],
					[MobileAlias],
					[IsAnonymous],
					[LastActivityDate]
				FROM
					[dbo].[aspnet_Users]
				WHERE
					[ApplicationId] = @ApplicationId
					AND [LoweredUserName] = @LoweredUserName
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Users_GetByApplicationIdLastActivityDate]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Select records from the aspnet_Users table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_Users_GetByApplicationIdLastActivityDate]
(

	@ApplicationId uniqueidentifier   ,

	@LastActivityDate datetime   
)
AS


				SELECT
					[ApplicationId],
					[UserId],
					[UserName],
					[LoweredUserName],
					[MobileAlias],
					[IsAnonymous],
					[LastActivityDate]
				FROM
					[dbo].[aspnet_Users]
				WHERE
					[ApplicationId] = @ApplicationId
					AND [LastActivityDate] = @LastActivityDate
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Users_GetByApplicationId]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Select records from the aspnet_Users table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_Users_GetByApplicationId]
(

	@ApplicationId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[ApplicationId],
					[UserId],
					[UserName],
					[LoweredUserName],
					[MobileAlias],
					[IsAnonymous],
					[LastActivityDate]
				FROM
					[dbo].[aspnet_Users]
				WHERE
					[ApplicationId] = @ApplicationId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Users_Get_List]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets all records from the aspnet_Users table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_Users_Get_List]

AS


				
				SELECT
					[ApplicationId],
					[UserId],
					[UserName],
					[LoweredUserName],
					[MobileAlias],
					[IsAnonymous],
					[LastActivityDate]
				FROM
					[dbo].[aspnet_Users]
					
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Users_Find]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Finds records in the aspnet_Users table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_Users_Find]
(

	@SearchUsingOR bit   = null ,

	@ApplicationId uniqueidentifier   = null ,

	@UserId uniqueidentifier   = null ,

	@UserName nvarchar (256)  = null ,

	@LoweredUserName nvarchar (256)  = null ,

	@MobileAlias nvarchar (16)  = null ,

	@IsAnonymous bit   = null ,

	@LastActivityDate datetime   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [ApplicationId]
	, [UserId]
	, [UserName]
	, [LoweredUserName]
	, [MobileAlias]
	, [IsAnonymous]
	, [LastActivityDate]
    FROM
	[dbo].[aspnet_Users]
    WHERE 
	 ([ApplicationId] = @ApplicationId OR @ApplicationId IS NULL)
	AND ([UserId] = @UserId OR @UserId IS NULL)
	AND ([UserName] = @UserName OR @UserName IS NULL)
	AND ([LoweredUserName] = @LoweredUserName OR @LoweredUserName IS NULL)
	AND ([MobileAlias] = @MobileAlias OR @MobileAlias IS NULL)
	AND ([IsAnonymous] = @IsAnonymous OR @IsAnonymous IS NULL)
	AND ([LastActivityDate] = @LastActivityDate OR @LastActivityDate IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [ApplicationId]
	, [UserId]
	, [UserName]
	, [LoweredUserName]
	, [MobileAlias]
	, [IsAnonymous]
	, [LastActivityDate]
    FROM
	[dbo].[aspnet_Users]
    WHERE 
	 ([ApplicationId] = @ApplicationId AND @ApplicationId is not null)
	OR ([UserId] = @UserId AND @UserId is not null)
	OR ([UserName] = @UserName AND @UserName is not null)
	OR ([LoweredUserName] = @LoweredUserName AND @LoweredUserName is not null)
	OR ([MobileAlias] = @MobileAlias AND @MobileAlias is not null)
	OR ([IsAnonymous] = @IsAnonymous AND @IsAnonymous is not null)
	OR ([LastActivityDate] = @LastActivityDate AND @LastActivityDate is not null)
	SELECT @@ROWCOUNT			
  END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Users_DeleteUser]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Users_DeleteUser]
    @ApplicationName  nvarchar(256),
    @UserName         nvarchar(256),
    @TablesToDeleteFrom int,
    @NumTablesDeletedFrom int OUTPUT
AS
BEGIN
    DECLARE @UserId               uniqueidentifier
    SELECT  @UserId               = NULL
    SELECT  @NumTablesDeletedFrom = 0

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
	    BEGIN TRANSACTION
	    SET @TranStarted = 1
    END
    ELSE
	SET @TranStarted = 0

    DECLARE @ErrorCode   int
    DECLARE @RowCount    int

    SET @ErrorCode = 0
    SET @RowCount  = 0

    SELECT  @UserId = u.UserId
    FROM    dbo.aspnet_Users u, dbo.aspnet_Applications a
    WHERE   u.LoweredUserName       = LOWER(@UserName)
        AND u.ApplicationId         = a.ApplicationId
        AND LOWER(@ApplicationName) = a.LoweredApplicationName

    IF (@UserId IS NULL)
    BEGIN
        GOTO Cleanup
    END

    -- Delete from Membership table if (@TablesToDeleteFrom & 1) is set
    IF ((@TablesToDeleteFrom & 1) <> 0 AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N'vw_aspnet_MembershipUsers') AND (type = 'V'))))
    BEGIN
        DELETE FROM dbo.aspnet_Membership WHERE @UserId = UserId

        SELECT @ErrorCode = @@ERROR,
               @RowCount = @@ROWCOUNT

        IF( @ErrorCode <> 0 )
            GOTO Cleanup

        IF (@RowCount <> 0)
            SELECT  @NumTablesDeletedFrom = @NumTablesDeletedFrom + 1
    END

    -- Delete from aspnet_UsersInRoles table if (@TablesToDeleteFrom & 2) is set
    IF ((@TablesToDeleteFrom & 2) <> 0  AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N'vw_aspnet_UsersInRoles') AND (type = 'V'))) )
    BEGIN
        DELETE FROM dbo.aspnet_UsersInRoles WHERE @UserId = UserId

        SELECT @ErrorCode = @@ERROR,
                @RowCount = @@ROWCOUNT

        IF( @ErrorCode <> 0 )
            GOTO Cleanup

        IF (@RowCount <> 0)
            SELECT  @NumTablesDeletedFrom = @NumTablesDeletedFrom + 1
    END

    -- Delete from aspnet_Profile table if (@TablesToDeleteFrom & 4) is set
    IF ((@TablesToDeleteFrom & 4) <> 0  AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N'vw_aspnet_Profiles') AND (type = 'V'))) )
    BEGIN
        DELETE FROM dbo.aspnet_Profile WHERE @UserId = UserId

        SELECT @ErrorCode = @@ERROR,
                @RowCount = @@ROWCOUNT

        IF( @ErrorCode <> 0 )
            GOTO Cleanup

        IF (@RowCount <> 0)
            SELECT  @NumTablesDeletedFrom = @NumTablesDeletedFrom + 1
    END

    -- Delete from aspnet_PersonalizationPerUser table if (@TablesToDeleteFrom & 8) is set
    IF ((@TablesToDeleteFrom & 8) <> 0  AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N'vw_aspnet_WebPartState_User') AND (type = 'V'))) )
    BEGIN
        DELETE FROM dbo.aspnet_PersonalizationPerUser WHERE @UserId = UserId

        SELECT @ErrorCode = @@ERROR,
                @RowCount = @@ROWCOUNT

        IF( @ErrorCode <> 0 )
            GOTO Cleanup

        IF (@RowCount <> 0)
            SELECT  @NumTablesDeletedFrom = @NumTablesDeletedFrom + 1
    END

    -- Delete from aspnet_Users table if (@TablesToDeleteFrom & 1,2,4 & 8) are all set
    IF ((@TablesToDeleteFrom & 1) <> 0 AND
        (@TablesToDeleteFrom & 2) <> 0 AND
        (@TablesToDeleteFrom & 4) <> 0 AND
        (@TablesToDeleteFrom & 8) <> 0 AND
        (EXISTS (SELECT UserId FROM dbo.aspnet_Users WHERE @UserId = UserId)))
    BEGIN
        DELETE FROM dbo.aspnet_Users WHERE @UserId = UserId

        SELECT @ErrorCode = @@ERROR,
                @RowCount = @@ROWCOUNT

        IF( @ErrorCode <> 0 )
            GOTO Cleanup

        IF (@RowCount <> 0)
            SELECT  @NumTablesDeletedFrom = @NumTablesDeletedFrom + 1
    END

    IF( @TranStarted = 1 )
    BEGIN
	    SET @TranStarted = 0
	    COMMIT TRANSACTION
    END

    RETURN 0

Cleanup:
    SET @NumTablesDeletedFrom = 0

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
	    ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode

END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_UsersInRoles_Update]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Updates a record in the aspnet_UsersInRoles table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_UsersInRoles_Update]
(

	@UserId uniqueidentifier   ,

	@OriginalUserId uniqueidentifier   ,

	@RoleId uniqueidentifier   ,

	@OriginalRoleId uniqueidentifier   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[aspnet_UsersInRoles]
				SET
					[UserId] = @UserId
					,[RoleId] = @RoleId
				WHERE
[UserId] = @OriginalUserId 
AND [RoleId] = @OriginalRoleId
GO
/****** Object:  StoredProcedure [dbo].[aspnet_UsersInRoles_RemoveUsersFromRoles]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_UsersInRoles_RemoveUsersFromRoles]
	@ApplicationName  nvarchar(256),
	@UserNames		  nvarchar(4000),
	@RoleNames		  nvarchar(4000)
AS
BEGIN
	DECLARE @AppId uniqueidentifier
	SELECT  @AppId = NULL
	SELECT  @AppId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
	IF (@AppId IS NULL)
		RETURN(2)


	DECLARE @TranStarted   bit
	SET @TranStarted = 0

	IF( @@TRANCOUNT = 0 )
	BEGIN
		BEGIN TRANSACTION
		SET @TranStarted = 1
	END

	DECLARE @tbNames  table(Name nvarchar(256) NOT NULL PRIMARY KEY)
	DECLARE @tbRoles  table(RoleId uniqueidentifier NOT NULL PRIMARY KEY)
	DECLARE @tbUsers  table(UserId uniqueidentifier NOT NULL PRIMARY KEY)
	DECLARE @Num	  int
	DECLARE @Pos	  int
	DECLARE @NextPos  int
	DECLARE @Name	  nvarchar(256)
	DECLARE @CountAll int
	DECLARE @CountU	  int
	DECLARE @CountR	  int


	SET @Num = 0
	SET @Pos = 1
	WHILE(@Pos <= LEN(@RoleNames))
	BEGIN
		SELECT @NextPos = CHARINDEX(N',', @RoleNames,  @Pos)
		IF (@NextPos = 0 OR @NextPos IS NULL)
			SELECT @NextPos = LEN(@RoleNames) + 1
		SELECT @Name = RTRIM(LTRIM(SUBSTRING(@RoleNames, @Pos, @NextPos - @Pos)))
		SELECT @Pos = @NextPos+1

		INSERT INTO @tbNames VALUES (@Name)
		SET @Num = @Num + 1
	END

	INSERT INTO @tbRoles
	  SELECT RoleId
	  FROM   dbo.aspnet_Roles ar, @tbNames t
	  WHERE  LOWER(t.Name) = ar.LoweredRoleName AND ar.ApplicationId = @AppId
	SELECT @CountR = @@ROWCOUNT

	IF (@CountR <> @Num)
	BEGIN
		SELECT TOP 1 N'', Name
		FROM   @tbNames
		WHERE  LOWER(Name) NOT IN (SELECT ar.LoweredRoleName FROM dbo.aspnet_Roles ar,  @tbRoles r WHERE r.RoleId = ar.RoleId)
		IF( @TranStarted = 1 )
			ROLLBACK TRANSACTION
		RETURN(2)
	END


	DELETE FROM @tbNames WHERE 1=1
	SET @Num = 0
	SET @Pos = 1


	WHILE(@Pos <= LEN(@UserNames))
	BEGIN
		SELECT @NextPos = CHARINDEX(N',', @UserNames,  @Pos)
		IF (@NextPos = 0 OR @NextPos IS NULL)
			SELECT @NextPos = LEN(@UserNames) + 1
		SELECT @Name = RTRIM(LTRIM(SUBSTRING(@UserNames, @Pos, @NextPos - @Pos)))
		SELECT @Pos = @NextPos+1

		INSERT INTO @tbNames VALUES (@Name)
		SET @Num = @Num + 1
	END

	INSERT INTO @tbUsers
	  SELECT UserId
	  FROM   dbo.aspnet_Users ar, @tbNames t
	  WHERE  LOWER(t.Name) = ar.LoweredUserName AND ar.ApplicationId = @AppId

	SELECT @CountU = @@ROWCOUNT
	IF (@CountU <> @Num)
	BEGIN
		SELECT TOP 1 Name, N''
		FROM   @tbNames
		WHERE  LOWER(Name) NOT IN (SELECT au.LoweredUserName FROM dbo.aspnet_Users au,  @tbUsers u WHERE u.UserId = au.UserId)

		IF( @TranStarted = 1 )
			ROLLBACK TRANSACTION
		RETURN(1)
	END

	SELECT  @CountAll = COUNT(*)
	FROM	dbo.aspnet_UsersInRoles ur, @tbUsers u, @tbRoles r
	WHERE   ur.UserId = u.UserId AND ur.RoleId = r.RoleId

	IF (@CountAll <> @CountU * @CountR)
	BEGIN
		SELECT TOP 1 UserName, RoleName
		FROM		 @tbUsers tu, @tbRoles tr, dbo.aspnet_Users u, dbo.aspnet_Roles r
		WHERE		 u.UserId = tu.UserId AND r.RoleId = tr.RoleId AND
					 tu.UserId NOT IN (SELECT ur.UserId FROM dbo.aspnet_UsersInRoles ur WHERE ur.RoleId = tr.RoleId) AND
					 tr.RoleId NOT IN (SELECT ur.RoleId FROM dbo.aspnet_UsersInRoles ur WHERE ur.UserId = tu.UserId)
		IF( @TranStarted = 1 )
			ROLLBACK TRANSACTION
		RETURN(3)
	END

	DELETE FROM dbo.aspnet_UsersInRoles
	WHERE UserId IN (SELECT UserId FROM @tbUsers)
	  AND RoleId IN (SELECT RoleId FROM @tbRoles)
	IF( @TranStarted = 1 )
		COMMIT TRANSACTION
	RETURN(0)
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_UsersInRoles_IsUserInRole]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_UsersInRoles_IsUserInRole]
    @ApplicationName  nvarchar(256),
    @UserName         nvarchar(256),
    @RoleName         nvarchar(256)
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN(2)
    DECLARE @UserId uniqueidentifier
    SELECT  @UserId = NULL
    DECLARE @RoleId uniqueidentifier
    SELECT  @RoleId = NULL

    SELECT  @UserId = UserId
    FROM    dbo.aspnet_Users
    WHERE   LoweredUserName = LOWER(@UserName) AND ApplicationId = @ApplicationId

    IF (@UserId IS NULL)
        RETURN(2)

    SELECT  @RoleId = RoleId
    FROM    dbo.aspnet_Roles
    WHERE   LoweredRoleName = LOWER(@RoleName) AND ApplicationId = @ApplicationId

    IF (@RoleId IS NULL)
        RETURN(3)

    IF (EXISTS( SELECT * FROM dbo.aspnet_UsersInRoles WHERE  UserId = @UserId AND RoleId = @RoleId))
        RETURN(1)
    ELSE
        RETURN(0)
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_UsersInRoles_Insert]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Inserts a record into the aspnet_UsersInRoles table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_UsersInRoles_Insert]
(

	@UserId uniqueidentifier   ,

	@RoleId uniqueidentifier   
)
AS


				
				INSERT INTO [dbo].[aspnet_UsersInRoles]
					(
					[UserId]
					,[RoleId]
					)
				VALUES
					(
					@UserId
					,@RoleId
					)
GO
/****** Object:  StoredProcedure [dbo].[aspnet_UsersInRoles_GetUsersInRoles]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_UsersInRoles_GetUsersInRoles]
    @ApplicationName  nvarchar(256),
    @RoleName         nvarchar(256)
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN(1)
     DECLARE @RoleId uniqueidentifier
     SELECT  @RoleId = NULL

     SELECT  @RoleId = RoleId
     FROM    dbo.aspnet_Roles
     WHERE   LOWER(@RoleName) = LoweredRoleName AND ApplicationId = @ApplicationId

     IF (@RoleId IS NULL)
         RETURN(1)

    SELECT u.UserName
    FROM   dbo.aspnet_Users u, dbo.aspnet_UsersInRoles ur
    WHERE  u.UserId = ur.UserId AND @RoleId = ur.RoleId AND u.ApplicationId = @ApplicationId
    ORDER BY u.UserName
    RETURN(0)
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_UsersInRoles_GetRolesForUser]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_UsersInRoles_GetRolesForUser]
    @ApplicationName  nvarchar(256),
    @UserName         nvarchar(256)
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN(1)
    DECLARE @UserId uniqueidentifier
    SELECT  @UserId = NULL

    SELECT  @UserId = UserId
    FROM    dbo.aspnet_Users
    WHERE   LoweredUserName = LOWER(@UserName) AND ApplicationId = @ApplicationId

    IF (@UserId IS NULL)
        RETURN(1)

    SELECT r.RoleName
    FROM   dbo.aspnet_Roles r, dbo.aspnet_UsersInRoles ur
    WHERE  r.RoleId = ur.RoleId AND r.ApplicationId = @ApplicationId AND ur.UserId = @UserId
    ORDER BY r.RoleName
    RETURN (0)
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_UsersInRoles_GetPaged]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets records from the aspnet_UsersInRoles table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_UsersInRoles_GetPaged]
(

	@WhereClause varchar (2000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [UserId] uniqueidentifier, [RoleId] uniqueidentifier 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([UserId], [RoleId])'
				SET @SQL = @SQL + ' SELECT'
				IF @PageSize > 0
				BEGIN
					SET @SQL = @SQL + ' TOP ' + CONVERT(nvarchar, @PageUpperBound)
				END
				SET @SQL = @SQL + ' [UserId], [RoleId]'
				SET @SQL = @SQL + ' FROM [dbo].[aspnet_UsersInRoles]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Return paged results
				SELECT O.[UserId], O.[RoleId]
				FROM
				    [dbo].[aspnet_UsersInRoles] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[UserId] = PageIndex.[UserId]
					AND O.[RoleId] = PageIndex.[RoleId]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[aspnet_UsersInRoles]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_UsersInRoles_GetByUserIdRoleId]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Select records from the aspnet_UsersInRoles table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_UsersInRoles_GetByUserIdRoleId]
(

	@UserId uniqueidentifier   ,

	@RoleId uniqueidentifier   
)
AS


				SELECT
					[UserId],
					[RoleId]
				FROM
					[dbo].[aspnet_UsersInRoles]
				WHERE
					[UserId] = @UserId
					AND [RoleId] = @RoleId
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[aspnet_UsersInRoles_GetByUserId]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Select records from the aspnet_UsersInRoles table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_UsersInRoles_GetByUserId]
(

	@UserId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[UserId],
					[RoleId]
				FROM
					[dbo].[aspnet_UsersInRoles]
				WHERE
					[UserId] = @UserId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
GO
/****** Object:  StoredProcedure [dbo].[aspnet_UsersInRoles_GetByRoleId]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Select records from the aspnet_UsersInRoles table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_UsersInRoles_GetByRoleId]
(

	@RoleId uniqueidentifier   
)
AS


				SELECT
					[UserId],
					[RoleId]
				FROM
					[dbo].[aspnet_UsersInRoles]
				WHERE
					[RoleId] = @RoleId
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[aspnet_UsersInRoles_Get_List]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets all records from the aspnet_UsersInRoles table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_UsersInRoles_Get_List]

AS


				
				SELECT
					[UserId],
					[RoleId]
				FROM
					[dbo].[aspnet_UsersInRoles]
					
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[aspnet_UsersInRoles_FindUsersInRole]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_UsersInRoles_FindUsersInRole]
    @ApplicationName  nvarchar(256),
    @RoleName         nvarchar(256),
    @UserNameToMatch  nvarchar(256)
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN(1)
     DECLARE @RoleId uniqueidentifier
     SELECT  @RoleId = NULL

     SELECT  @RoleId = RoleId
     FROM    dbo.aspnet_Roles
     WHERE   LOWER(@RoleName) = LoweredRoleName AND ApplicationId = @ApplicationId

     IF (@RoleId IS NULL)
         RETURN(1)

    SELECT u.UserName
    FROM   dbo.aspnet_Users u, dbo.aspnet_UsersInRoles ur
    WHERE  u.UserId = ur.UserId AND @RoleId = ur.RoleId AND u.ApplicationId = @ApplicationId AND LoweredUserName LIKE LOWER(@UserNameToMatch)
    ORDER BY u.UserName
    RETURN(0)
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_UsersInRoles_Find]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Finds records in the aspnet_UsersInRoles table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_UsersInRoles_Find]
(

	@SearchUsingOR bit   = null ,

	@UserId uniqueidentifier   = null ,

	@RoleId uniqueidentifier   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [UserId]
	, [RoleId]
    FROM
	[dbo].[aspnet_UsersInRoles]
    WHERE 
	 ([UserId] = @UserId OR @UserId IS NULL)
	AND ([RoleId] = @RoleId OR @RoleId IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [UserId]
	, [RoleId]
    FROM
	[dbo].[aspnet_UsersInRoles]
    WHERE 
	 ([UserId] = @UserId AND @UserId is not null)
	OR ([RoleId] = @RoleId AND @RoleId is not null)
	SELECT @@ROWCOUNT			
  END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_UsersInRoles_Delete]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Deletes a record in the aspnet_UsersInRoles table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_UsersInRoles_Delete]
(

	@UserId uniqueidentifier   ,

	@RoleId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[aspnet_UsersInRoles] WITH (ROWLOCK) 
				WHERE
					[UserId] = @UserId
					AND [RoleId] = @RoleId
GO
/****** Object:  StoredProcedure [dbo].[aspnet_UsersInRoles_AddUsersToRoles]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_UsersInRoles_AddUsersToRoles]
	@ApplicationName  nvarchar(256),
	@UserNames		  nvarchar(4000),
	@RoleNames		  nvarchar(4000),
	@CurrentTimeUtc   datetime
AS
BEGIN
	DECLARE @AppId uniqueidentifier
	SELECT  @AppId = NULL
	SELECT  @AppId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
	IF (@AppId IS NULL)
		RETURN(2)
	DECLARE @TranStarted   bit
	SET @TranStarted = 0

	IF( @@TRANCOUNT = 0 )
	BEGIN
		BEGIN TRANSACTION
		SET @TranStarted = 1
	END

	DECLARE @tbNames	table(Name nvarchar(256) NOT NULL PRIMARY KEY)
	DECLARE @tbRoles	table(RoleId uniqueidentifier NOT NULL PRIMARY KEY)
	DECLARE @tbUsers	table(UserId uniqueidentifier NOT NULL PRIMARY KEY)
	DECLARE @Num		int
	DECLARE @Pos		int
	DECLARE @NextPos	int
	DECLARE @Name		nvarchar(256)

	SET @Num = 0
	SET @Pos = 1
	WHILE(@Pos <= LEN(@RoleNames))
	BEGIN
		SELECT @NextPos = CHARINDEX(N',', @RoleNames,  @Pos)
		IF (@NextPos = 0 OR @NextPos IS NULL)
			SELECT @NextPos = LEN(@RoleNames) + 1
		SELECT @Name = RTRIM(LTRIM(SUBSTRING(@RoleNames, @Pos, @NextPos - @Pos)))
		SELECT @Pos = @NextPos+1

		INSERT INTO @tbNames VALUES (@Name)
		SET @Num = @Num + 1
	END

	INSERT INTO @tbRoles
	  SELECT RoleId
	  FROM   dbo.aspnet_Roles ar, @tbNames t
	  WHERE  LOWER(t.Name) = ar.LoweredRoleName AND ar.ApplicationId = @AppId

	IF (@@ROWCOUNT <> @Num)
	BEGIN
		SELECT TOP 1 Name
		FROM   @tbNames
		WHERE  LOWER(Name) NOT IN (SELECT ar.LoweredRoleName FROM dbo.aspnet_Roles ar,  @tbRoles r WHERE r.RoleId = ar.RoleId)
		IF( @TranStarted = 1 )
			ROLLBACK TRANSACTION
		RETURN(2)
	END

	DELETE FROM @tbNames WHERE 1=1
	SET @Num = 0
	SET @Pos = 1

	WHILE(@Pos <= LEN(@UserNames))
	BEGIN
		SELECT @NextPos = CHARINDEX(N',', @UserNames,  @Pos)
		IF (@NextPos = 0 OR @NextPos IS NULL)
			SELECT @NextPos = LEN(@UserNames) + 1
		SELECT @Name = RTRIM(LTRIM(SUBSTRING(@UserNames, @Pos, @NextPos - @Pos)))
		SELECT @Pos = @NextPos+1

		INSERT INTO @tbNames VALUES (@Name)
		SET @Num = @Num + 1
	END

	INSERT INTO @tbUsers
	  SELECT UserId
	  FROM   dbo.aspnet_Users ar, @tbNames t
	  WHERE  LOWER(t.Name) = ar.LoweredUserName AND ar.ApplicationId = @AppId

	IF (@@ROWCOUNT <> @Num)
	BEGIN
		DELETE FROM @tbNames
		WHERE LOWER(Name) IN (SELECT LoweredUserName FROM dbo.aspnet_Users au,  @tbUsers u WHERE au.UserId = u.UserId)

		INSERT dbo.aspnet_Users (ApplicationId, UserId, UserName, LoweredUserName, IsAnonymous, LastActivityDate)
		  SELECT @AppId, NEWID(), Name, LOWER(Name), 0, @CurrentTimeUtc
		  FROM   @tbNames

		INSERT INTO @tbUsers
		  SELECT  UserId
		  FROM	dbo.aspnet_Users au, @tbNames t
		  WHERE   LOWER(t.Name) = au.LoweredUserName AND au.ApplicationId = @AppId
	END

	IF (EXISTS (SELECT * FROM dbo.aspnet_UsersInRoles ur, @tbUsers tu, @tbRoles tr WHERE tu.UserId = ur.UserId AND tr.RoleId = ur.RoleId))
	BEGIN
		SELECT TOP 1 UserName, RoleName
		FROM		 dbo.aspnet_UsersInRoles ur, @tbUsers tu, @tbRoles tr, aspnet_Users u, aspnet_Roles r
		WHERE		u.UserId = tu.UserId AND r.RoleId = tr.RoleId AND tu.UserId = ur.UserId AND tr.RoleId = ur.RoleId

		IF( @TranStarted = 1 )
			ROLLBACK TRANSACTION
		RETURN(3)
	END

	INSERT INTO dbo.aspnet_UsersInRoles (UserId, RoleId)
	SELECT UserId, RoleId
	FROM @tbUsers, @tbRoles

	IF( @TranStarted = 1 )
		COMMIT TRANSACTION
	RETURN(0)
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Users_GetByRoleIdFromAspnetUsersInRoles]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets records through a junction table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_Users_GetByRoleIdFromAspnetUsersInRoles]
(

	@RoleId uniqueidentifier   
)
AS


SELECT dbo.[aspnet_Users].[ApplicationId]
       ,dbo.[aspnet_Users].[UserId]
       ,dbo.[aspnet_Users].[UserName]
       ,dbo.[aspnet_Users].[LoweredUserName]
       ,dbo.[aspnet_Users].[MobileAlias]
       ,dbo.[aspnet_Users].[IsAnonymous]
       ,dbo.[aspnet_Users].[LastActivityDate]
  FROM dbo.[aspnet_Users]
 WHERE EXISTS (SELECT 1
                 FROM dbo.[aspnet_UsersInRoles] 
                WHERE dbo.[aspnet_UsersInRoles].[RoleId] = @RoleId
                  AND dbo.[aspnet_UsersInRoles].[UserId] = dbo.[aspnet_Users].[UserId]
                  )
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Roles_DeleteRole]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Roles_DeleteRole]
    @ApplicationName            nvarchar(256),
    @RoleName                   nvarchar(256),
    @DeleteOnlyIfRoleIsEmpty    bit
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN(1)

    DECLARE @ErrorCode     int
    SET @ErrorCode = 0

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
        BEGIN TRANSACTION
        SET @TranStarted = 1
    END
    ELSE
        SET @TranStarted = 0

    DECLARE @RoleId   uniqueidentifier
    SELECT  @RoleId = NULL
    SELECT  @RoleId = RoleId FROM dbo.aspnet_Roles WHERE LoweredRoleName = LOWER(@RoleName) AND ApplicationId = @ApplicationId

    IF (@RoleId IS NULL)
    BEGIN
        SELECT @ErrorCode = 1
        GOTO Cleanup
    END
    IF (@DeleteOnlyIfRoleIsEmpty <> 0)
    BEGIN
        IF (EXISTS (SELECT RoleId FROM dbo.aspnet_UsersInRoles  WHERE @RoleId = RoleId))
        BEGIN
            SELECT @ErrorCode = 2
            GOTO Cleanup
        END
    END


    DELETE FROM dbo.aspnet_UsersInRoles  WHERE @RoleId = RoleId

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    DELETE FROM dbo.aspnet_Roles WHERE @RoleId = RoleId  AND ApplicationId = @ApplicationId

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
        COMMIT TRANSACTION
    END

    RETURN(0)

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
        ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Profile_DeleteInactiveProfiles]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Profile_DeleteInactiveProfiles]
    @ApplicationName        nvarchar(256),
    @ProfileAuthOptions     int,
    @InactiveSinceDate      datetime
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
    BEGIN
        SELECT  0
        RETURN
    END

    DELETE
    FROM    dbo.aspnet_Profile
    WHERE   UserId IN
            (   SELECT  UserId
                FROM    dbo.aspnet_Users u
                WHERE   ApplicationId = @ApplicationId
                        AND (LastActivityDate <= @InactiveSinceDate)
                        AND (
                                (@ProfileAuthOptions = 2)
                             OR (@ProfileAuthOptions = 0 AND IsAnonymous = 1)
                             OR (@ProfileAuthOptions = 1 AND IsAnonymous = 0)
                            )
            )

    SELECT  @@ROWCOUNT
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Profile_Delete]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Deletes a record in the aspnet_Profile table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_Profile_Delete]
(

	@UserId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[aspnet_Profile] WITH (ROWLOCK) 
				WHERE
					[UserId] = @UserId
GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationAdministration_ResetUserState]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_PersonalizationAdministration_ResetUserState] (
    @Count                  int                 OUT,
    @ApplicationName        NVARCHAR(256),
    @InactiveSinceDate      DATETIME            = NULL,
    @UserName               NVARCHAR(256)       = NULL,
    @Path                   NVARCHAR(256)       = NULL)
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    EXEC dbo.aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
        SELECT @Count = 0
    ELSE
    BEGIN
        DELETE FROM dbo.aspnet_PersonalizationPerUser
        WHERE Id IN (SELECT PerUser.Id
                     FROM dbo.aspnet_PersonalizationPerUser PerUser, dbo.aspnet_Users Users, dbo.aspnet_Paths Paths
                     WHERE Paths.ApplicationId = @ApplicationId
                           AND PerUser.UserId = Users.UserId
                           AND PerUser.PathId = Paths.PathId
                           AND (@InactiveSinceDate IS NULL OR Users.LastActivityDate <= @InactiveSinceDate)
                           AND (@UserName IS NULL OR Users.LoweredUserName = LOWER(@UserName))
                           AND (@Path IS NULL OR Paths.LoweredPath = LOWER(@Path)))

        SELECT @Count = @@ROWCOUNT
    END
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationAdministration_ResetSharedState]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_PersonalizationAdministration_ResetSharedState] (
    @Count int OUT,
    @ApplicationName NVARCHAR(256),
    @Path NVARCHAR(256))
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    EXEC dbo.aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
        SELECT @Count = 0
    ELSE
    BEGIN
        DELETE FROM dbo.aspnet_PersonalizationAllUsers
        WHERE PathId IN
            (SELECT AllUsers.PathId
             FROM dbo.aspnet_PersonalizationAllUsers AllUsers, dbo.aspnet_Paths Paths
             WHERE Paths.ApplicationId = @ApplicationId
                   AND AllUsers.PathId = Paths.PathId
                   AND Paths.LoweredPath = LOWER(@Path))

        SELECT @Count = @@ROWCOUNT
    END
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationAdministration_GetCountOfState]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_PersonalizationAdministration_GetCountOfState] (
    @Count int OUT,
    @AllUsersScope bit,
    @ApplicationName NVARCHAR(256),
    @Path NVARCHAR(256) = NULL,
    @UserName NVARCHAR(256) = NULL,
    @InactiveSinceDate DATETIME = NULL)
AS
BEGIN

    DECLARE @ApplicationId UNIQUEIDENTIFIER
    EXEC dbo.aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
        SELECT @Count = 0
    ELSE
        IF (@AllUsersScope = 1)
            SELECT @Count = COUNT(*)
            FROM dbo.aspnet_PersonalizationAllUsers AllUsers, dbo.aspnet_Paths Paths
            WHERE Paths.ApplicationId = @ApplicationId
                  AND AllUsers.PathId = Paths.PathId
                  AND (@Path IS NULL OR Paths.LoweredPath LIKE LOWER(@Path))
        ELSE
            SELECT @Count = COUNT(*)
            FROM dbo.aspnet_PersonalizationPerUser PerUser, dbo.aspnet_Users Users, dbo.aspnet_Paths Paths
            WHERE Paths.ApplicationId = @ApplicationId
                  AND PerUser.UserId = Users.UserId
                  AND PerUser.PathId = Paths.PathId
                  AND (@Path IS NULL OR Paths.LoweredPath LIKE LOWER(@Path))
                  AND (@UserName IS NULL OR Users.LoweredUserName LIKE LOWER(@UserName))
                  AND (@InactiveSinceDate IS NULL OR Users.LastActivityDate <= @InactiveSinceDate)
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationAdministration_FindState]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_PersonalizationAdministration_FindState] (
    @AllUsersScope bit,
    @ApplicationName NVARCHAR(256),
    @PageIndex              INT,
    @PageSize               INT,
    @Path NVARCHAR(256) = NULL,
    @UserName NVARCHAR(256) = NULL,
    @InactiveSinceDate DATETIME = NULL)
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    EXEC dbo.aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
        RETURN

    -- Set the page bounds
    DECLARE @PageLowerBound INT
    DECLARE @PageUpperBound INT
    DECLARE @TotalRecords   INT
    SET @PageLowerBound = @PageSize * @PageIndex
    SET @PageUpperBound = @PageSize - 1 + @PageLowerBound

    -- Create a temp table to store the selected results
    CREATE TABLE #PageIndex (
        IndexId int IDENTITY (0, 1) NOT NULL,
        ItemId UNIQUEIDENTIFIER
    )

    IF (@AllUsersScope = 1)
    BEGIN
        -- Insert into our temp table
        INSERT INTO #PageIndex (ItemId)
        SELECT Paths.PathId
        FROM dbo.aspnet_Paths Paths,
             ((SELECT Paths.PathId
               FROM dbo.aspnet_PersonalizationAllUsers AllUsers, dbo.aspnet_Paths Paths
               WHERE Paths.ApplicationId = @ApplicationId
                      AND AllUsers.PathId = Paths.PathId
                      AND (@Path IS NULL OR Paths.LoweredPath LIKE LOWER(@Path))
              ) AS SharedDataPerPath
              FULL OUTER JOIN
              (SELECT DISTINCT Paths.PathId
               FROM dbo.aspnet_PersonalizationPerUser PerUser, dbo.aspnet_Paths Paths
               WHERE Paths.ApplicationId = @ApplicationId
                      AND PerUser.PathId = Paths.PathId
                      AND (@Path IS NULL OR Paths.LoweredPath LIKE LOWER(@Path))
              ) AS UserDataPerPath
              ON SharedDataPerPath.PathId = UserDataPerPath.PathId
             )
        WHERE Paths.PathId = SharedDataPerPath.PathId OR Paths.PathId = UserDataPerPath.PathId
        ORDER BY Paths.Path ASC

        SELECT @TotalRecords = @@ROWCOUNT

        SELECT Paths.Path,
               SharedDataPerPath.LastUpdatedDate,
               SharedDataPerPath.SharedDataLength,
               UserDataPerPath.UserDataLength,
               UserDataPerPath.UserCount
        FROM dbo.aspnet_Paths Paths,
             ((SELECT PageIndex.ItemId AS PathId,
                      AllUsers.LastUpdatedDate AS LastUpdatedDate,
                      DATALENGTH(AllUsers.PageSettings) AS SharedDataLength
               FROM dbo.aspnet_PersonalizationAllUsers AllUsers, #PageIndex PageIndex
               WHERE AllUsers.PathId = PageIndex.ItemId
                     AND PageIndex.IndexId >= @PageLowerBound AND PageIndex.IndexId <= @PageUpperBound
              ) AS SharedDataPerPath
              FULL OUTER JOIN
              (SELECT PageIndex.ItemId AS PathId,
                      SUM(DATALENGTH(PerUser.PageSettings)) AS UserDataLength,
                      COUNT(*) AS UserCount
               FROM aspnet_PersonalizationPerUser PerUser, #PageIndex PageIndex
               WHERE PerUser.PathId = PageIndex.ItemId
                     AND PageIndex.IndexId >= @PageLowerBound AND PageIndex.IndexId <= @PageUpperBound
               GROUP BY PageIndex.ItemId
              ) AS UserDataPerPath
              ON SharedDataPerPath.PathId = UserDataPerPath.PathId
             )
        WHERE Paths.PathId = SharedDataPerPath.PathId OR Paths.PathId = UserDataPerPath.PathId
        ORDER BY Paths.Path ASC
    END
    ELSE
    BEGIN
        -- Insert into our temp table
        INSERT INTO #PageIndex (ItemId)
        SELECT PerUser.Id
        FROM dbo.aspnet_PersonalizationPerUser PerUser, dbo.aspnet_Users Users, dbo.aspnet_Paths Paths
        WHERE Paths.ApplicationId = @ApplicationId
              AND PerUser.UserId = Users.UserId
              AND PerUser.PathId = Paths.PathId
              AND (@Path IS NULL OR Paths.LoweredPath LIKE LOWER(@Path))
              AND (@UserName IS NULL OR Users.LoweredUserName LIKE LOWER(@UserName))
              AND (@InactiveSinceDate IS NULL OR Users.LastActivityDate <= @InactiveSinceDate)
        ORDER BY Paths.Path ASC, Users.UserName ASC

        SELECT @TotalRecords = @@ROWCOUNT

        SELECT Paths.Path, PerUser.LastUpdatedDate, DATALENGTH(PerUser.PageSettings), Users.UserName, Users.LastActivityDate
        FROM dbo.aspnet_PersonalizationPerUser PerUser, dbo.aspnet_Users Users, dbo.aspnet_Paths Paths, #PageIndex PageIndex
        WHERE PerUser.Id = PageIndex.ItemId
              AND PerUser.UserId = Users.UserId
              AND PerUser.PathId = Paths.PathId
              AND PageIndex.IndexId >= @PageLowerBound AND PageIndex.IndexId <= @PageUpperBound
        ORDER BY Paths.Path ASC, Users.UserName ASC
    END

    RETURN @TotalRecords
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationAdministration_DeleteAllState]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_PersonalizationAdministration_DeleteAllState] (
    @AllUsersScope bit,
    @ApplicationName NVARCHAR(256),
    @Count int OUT)
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    EXEC dbo.aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
        SELECT @Count = 0
    ELSE
    BEGIN
        IF (@AllUsersScope = 1)
            DELETE FROM aspnet_PersonalizationAllUsers
            WHERE PathId IN
               (SELECT Paths.PathId
                FROM dbo.aspnet_Paths Paths
                WHERE Paths.ApplicationId = @ApplicationId)
        ELSE
            DELETE FROM aspnet_PersonalizationPerUser
            WHERE PathId IN
               (SELECT Paths.PathId
                FROM dbo.aspnet_Paths Paths
                WHERE Paths.ApplicationId = @ApplicationId)

        SELECT @Count = @@ROWCOUNT
    END
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_UpdateUserInfo]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_UpdateUserInfo]
    @ApplicationName                nvarchar(256),
    @UserName                       nvarchar(256),
    @IsPasswordCorrect              bit,
    @UpdateLastLoginActivityDate    bit,
    @MaxInvalidPasswordAttempts     int,
    @PasswordAttemptWindow          int,
    @CurrentTimeUtc                 datetime,
    @LastLoginDate                  datetime,
    @LastActivityDate               datetime
AS
BEGIN
    DECLARE @UserId                                 uniqueidentifier
    DECLARE @IsApproved                             bit
    DECLARE @IsLockedOut                            bit
    DECLARE @LastLockoutDate                        datetime
    DECLARE @FailedPasswordAttemptCount             int
    DECLARE @FailedPasswordAttemptWindowStart       datetime
    DECLARE @FailedPasswordAnswerAttemptCount       int
    DECLARE @FailedPasswordAnswerAttemptWindowStart datetime

    DECLARE @ErrorCode     int
    SET @ErrorCode = 0

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
	    BEGIN TRANSACTION
	    SET @TranStarted = 1
    END
    ELSE
    	SET @TranStarted = 0

    SELECT  @UserId = u.UserId,
            @IsApproved = m.IsApproved,
            @IsLockedOut = m.IsLockedOut,
            @LastLockoutDate = m.LastLockoutDate,
            @FailedPasswordAttemptCount = m.FailedPasswordAttemptCount,
            @FailedPasswordAttemptWindowStart = m.FailedPasswordAttemptWindowStart,
            @FailedPasswordAnswerAttemptCount = m.FailedPasswordAnswerAttemptCount,
            @FailedPasswordAnswerAttemptWindowStart = m.FailedPasswordAnswerAttemptWindowStart
    FROM    dbo.aspnet_Applications a, dbo.aspnet_Users u, dbo.aspnet_Membership m WITH ( UPDLOCK )
    WHERE   LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.ApplicationId = a.ApplicationId    AND
            u.UserId = m.UserId AND
            LOWER(@UserName) = u.LoweredUserName

    IF ( @@rowcount = 0 )
    BEGIN
        SET @ErrorCode = 1
        GOTO Cleanup
    END

    IF( @IsLockedOut = 1 )
    BEGIN
        GOTO Cleanup
    END

    IF( @IsPasswordCorrect = 0 )
    BEGIN
        IF( @CurrentTimeUtc > DATEADD( minute, @PasswordAttemptWindow, @FailedPasswordAttemptWindowStart ) )
        BEGIN
            SET @FailedPasswordAttemptWindowStart = @CurrentTimeUtc
            SET @FailedPasswordAttemptCount = 1
        END
        ELSE
        BEGIN
            SET @FailedPasswordAttemptWindowStart = @CurrentTimeUtc
            SET @FailedPasswordAttemptCount = @FailedPasswordAttemptCount + 1
        END

        BEGIN
            IF( @FailedPasswordAttemptCount >= @MaxInvalidPasswordAttempts )
            BEGIN
                SET @IsLockedOut = 1
                SET @LastLockoutDate = @CurrentTimeUtc
            END
        END
    END
    ELSE
    BEGIN
        IF( @FailedPasswordAttemptCount > 0 OR @FailedPasswordAnswerAttemptCount > 0 )
        BEGIN
            SET @FailedPasswordAttemptCount = 0
            SET @FailedPasswordAttemptWindowStart = CONVERT( datetime, '17540101', 112 )
            SET @FailedPasswordAnswerAttemptCount = 0
            SET @FailedPasswordAnswerAttemptWindowStart = CONVERT( datetime, '17540101', 112 )
            SET @LastLockoutDate = CONVERT( datetime, '17540101', 112 )
        END
    END

    IF( @UpdateLastLoginActivityDate = 1 )
    BEGIN
        UPDATE  dbo.aspnet_Users
        SET     LastActivityDate = @LastActivityDate
        WHERE   @UserId = UserId

        IF( @@ERROR <> 0 )
        BEGIN
            SET @ErrorCode = -1
            GOTO Cleanup
        END

        UPDATE  dbo.aspnet_Membership
        SET     LastLoginDate = @LastLoginDate
        WHERE   UserId = @UserId

        IF( @@ERROR <> 0 )
        BEGIN
            SET @ErrorCode = -1
            GOTO Cleanup
        END
    END


    UPDATE dbo.aspnet_Membership
    SET IsLockedOut = @IsLockedOut, LastLockoutDate = @LastLockoutDate,
        FailedPasswordAttemptCount = @FailedPasswordAttemptCount,
        FailedPasswordAttemptWindowStart = @FailedPasswordAttemptWindowStart,
        FailedPasswordAnswerAttemptCount = @FailedPasswordAnswerAttemptCount,
        FailedPasswordAnswerAttemptWindowStart = @FailedPasswordAnswerAttemptWindowStart
    WHERE @UserId = UserId

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF( @TranStarted = 1 )
    BEGIN
	SET @TranStarted = 0
	COMMIT TRANSACTION
    END

    RETURN @ErrorCode

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
    	ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode

END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_UpdateUser]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_UpdateUser]
    @ApplicationName      nvarchar(256),
    @UserName             nvarchar(256),
    @Email                nvarchar(256),
    @Comment              ntext,
    @IsApproved           bit,
    @LastLoginDate        datetime,
    @LastActivityDate     datetime,
    @UniqueEmail          int,
    @CurrentTimeUtc       datetime
AS
BEGIN
    DECLARE @UserId uniqueidentifier
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @UserId = NULL
    SELECT  @UserId = u.UserId, @ApplicationId = a.ApplicationId
    FROM    dbo.aspnet_Users u, dbo.aspnet_Applications a, dbo.aspnet_Membership m
    WHERE   LoweredUserName = LOWER(@UserName) AND
            u.ApplicationId = a.ApplicationId  AND
            LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.UserId = m.UserId

    IF (@UserId IS NULL)
        RETURN(1)

    IF (@UniqueEmail = 1)
    BEGIN
        IF (EXISTS (SELECT *
                    FROM  dbo.aspnet_Membership WITH (UPDLOCK, HOLDLOCK)
                    WHERE ApplicationId = @ApplicationId  AND @UserId <> UserId AND LoweredEmail = LOWER(@Email)))
        BEGIN
            RETURN(7)
        END
    END

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
	    BEGIN TRANSACTION
	    SET @TranStarted = 1
    END
    ELSE
	SET @TranStarted = 0

    UPDATE dbo.aspnet_Users WITH (ROWLOCK)
    SET
         LastActivityDate = @LastActivityDate
    WHERE
       @UserId = UserId

    IF( @@ERROR <> 0 )
        GOTO Cleanup

    UPDATE dbo.aspnet_Membership WITH (ROWLOCK)
    SET
         Email            = @Email,
         LoweredEmail     = LOWER(@Email),
         Comment          = @Comment,
         IsApproved       = @IsApproved,
         LastLoginDate    = @LastLoginDate
    WHERE
       @UserId = UserId

    IF( @@ERROR <> 0 )
        GOTO Cleanup

    IF( @TranStarted = 1 )
    BEGIN
	SET @TranStarted = 0
	COMMIT TRANSACTION
    END

    RETURN 0

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
    	ROLLBACK TRANSACTION
    END

    RETURN -1
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_Update]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Updates a record in the aspnet_Membership table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_Membership_Update]
(

	@ApplicationId uniqueidentifier   ,

	@UserId uniqueidentifier   ,

	@OriginalUserId uniqueidentifier   ,

	@Password nvarchar (128)  ,

	@PasswordFormat int   ,

	@PasswordSalt nvarchar (128)  ,

	@MobilePin nvarchar (16)  ,

	@Email nvarchar (256)  ,

	@LoweredEmail nvarchar (256)  ,

	@PasswordQuestion nvarchar (256)  ,

	@PasswordAnswer nvarchar (128)  ,

	@IsApproved bit   ,

	@IsLockedOut bit   ,

	@CreateDate datetime   ,

	@LastLoginDate datetime   ,

	@LastPasswordChangedDate datetime   ,

	@LastLockoutDate datetime   ,

	@FailedPasswordAttemptCount int   ,

	@FailedPasswordAttemptWindowStart datetime   ,

	@FailedPasswordAnswerAttemptCount int   ,

	@FailedPasswordAnswerAttemptWindowStart datetime   ,

	@Comment ntext   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[aspnet_Membership]
				SET
					[ApplicationId] = @ApplicationId
					,[UserId] = @UserId
					,[Password] = @Password
					,[PasswordFormat] = @PasswordFormat
					,[PasswordSalt] = @PasswordSalt
					,[MobilePIN] = @MobilePin
					,[Email] = @Email
					,[LoweredEmail] = @LoweredEmail
					,[PasswordQuestion] = @PasswordQuestion
					,[PasswordAnswer] = @PasswordAnswer
					,[IsApproved] = @IsApproved
					,[IsLockedOut] = @IsLockedOut
					,[CreateDate] = @CreateDate
					,[LastLoginDate] = @LastLoginDate
					,[LastPasswordChangedDate] = @LastPasswordChangedDate
					,[LastLockoutDate] = @LastLockoutDate
					,[FailedPasswordAttemptCount] = @FailedPasswordAttemptCount
					,[FailedPasswordAttemptWindowStart] = @FailedPasswordAttemptWindowStart
					,[FailedPasswordAnswerAttemptCount] = @FailedPasswordAnswerAttemptCount
					,[FailedPasswordAnswerAttemptWindowStart] = @FailedPasswordAnswerAttemptWindowStart
					,[Comment] = @Comment
				WHERE
[UserId] = @OriginalUserId
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_UnlockUser]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_UnlockUser]
    @ApplicationName                         nvarchar(256),
    @UserName                                nvarchar(256)
AS
BEGIN
    DECLARE @UserId uniqueidentifier
    SELECT  @UserId = NULL
    SELECT  @UserId = u.UserId
    FROM    dbo.aspnet_Users u, dbo.aspnet_Applications a, dbo.aspnet_Membership m
    WHERE   LoweredUserName = LOWER(@UserName) AND
            u.ApplicationId = a.ApplicationId  AND
            LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.UserId = m.UserId

    IF ( @UserId IS NULL )
        RETURN 1

    UPDATE dbo.aspnet_Membership
    SET IsLockedOut = 0,
        FailedPasswordAttemptCount = 0,
        FailedPasswordAttemptWindowStart = CONVERT( datetime, '17540101', 112 ),
        FailedPasswordAnswerAttemptCount = 0,
        FailedPasswordAnswerAttemptWindowStart = CONVERT( datetime, '17540101', 112 ),
        LastLockoutDate = CONVERT( datetime, '17540101', 112 )
    WHERE @UserId = UserId

    RETURN 0
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_SetPassword]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_SetPassword]
    @ApplicationName  nvarchar(256),
    @UserName         nvarchar(256),
    @NewPassword      nvarchar(128),
    @PasswordSalt     nvarchar(128),
    @CurrentTimeUtc   datetime,
    @PasswordFormat   int = 0
AS
BEGIN
    DECLARE @UserId uniqueidentifier
    SELECT  @UserId = NULL
    SELECT  @UserId = u.UserId
    FROM    dbo.aspnet_Users u, dbo.aspnet_Applications a, dbo.aspnet_Membership m
    WHERE   LoweredUserName = LOWER(@UserName) AND
            u.ApplicationId = a.ApplicationId  AND
            LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.UserId = m.UserId

    IF (@UserId IS NULL)
        RETURN(1)

    UPDATE dbo.aspnet_Membership
    SET Password = @NewPassword, PasswordFormat = @PasswordFormat, PasswordSalt = @PasswordSalt,
        LastPasswordChangedDate = @CurrentTimeUtc
    WHERE @UserId = UserId
    RETURN(0)
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_ResetPassword]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_ResetPassword]
    @ApplicationName             nvarchar(256),
    @UserName                    nvarchar(256),
    @NewPassword                 nvarchar(128),
    @MaxInvalidPasswordAttempts  int,
    @PasswordAttemptWindow       int,
    @PasswordSalt                nvarchar(128),
    @CurrentTimeUtc              datetime,
    @PasswordFormat              int = 0,
    @PasswordAnswer              nvarchar(128) = NULL
AS
BEGIN
    DECLARE @IsLockedOut                            bit
    DECLARE @LastLockoutDate                        datetime
    DECLARE @FailedPasswordAttemptCount             int
    DECLARE @FailedPasswordAttemptWindowStart       datetime
    DECLARE @FailedPasswordAnswerAttemptCount       int
    DECLARE @FailedPasswordAnswerAttemptWindowStart datetime

    DECLARE @UserId                                 uniqueidentifier
    SET     @UserId = NULL

    DECLARE @ErrorCode     int
    SET @ErrorCode = 0

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
	    BEGIN TRANSACTION
	    SET @TranStarted = 1
    END
    ELSE
    	SET @TranStarted = 0

    SELECT  @UserId = u.UserId
    FROM    dbo.aspnet_Users u, dbo.aspnet_Applications a, dbo.aspnet_Membership m
    WHERE   LoweredUserName = LOWER(@UserName) AND
            u.ApplicationId = a.ApplicationId  AND
            LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.UserId = m.UserId

    IF ( @UserId IS NULL )
    BEGIN
        SET @ErrorCode = 1
        GOTO Cleanup
    END

    SELECT @IsLockedOut = IsLockedOut,
           @LastLockoutDate = LastLockoutDate,
           @FailedPasswordAttemptCount = FailedPasswordAttemptCount,
           @FailedPasswordAttemptWindowStart = FailedPasswordAttemptWindowStart,
           @FailedPasswordAnswerAttemptCount = FailedPasswordAnswerAttemptCount,
           @FailedPasswordAnswerAttemptWindowStart = FailedPasswordAnswerAttemptWindowStart
    FROM dbo.aspnet_Membership WITH ( UPDLOCK )
    WHERE @UserId = UserId

    IF( @IsLockedOut = 1 )
    BEGIN
        SET @ErrorCode = 99
        GOTO Cleanup
    END

    UPDATE dbo.aspnet_Membership
    SET    Password = @NewPassword,
           LastPasswordChangedDate = @CurrentTimeUtc,
           PasswordFormat = @PasswordFormat,
           PasswordSalt = @PasswordSalt
    WHERE  @UserId = UserId AND
           ( ( @PasswordAnswer IS NULL ) OR ( LOWER( PasswordAnswer ) = LOWER( @PasswordAnswer ) ) )

    IF ( @@ROWCOUNT = 0 )
        BEGIN
            IF( @CurrentTimeUtc > DATEADD( minute, @PasswordAttemptWindow, @FailedPasswordAnswerAttemptWindowStart ) )
            BEGIN
                SET @FailedPasswordAnswerAttemptWindowStart = @CurrentTimeUtc
                SET @FailedPasswordAnswerAttemptCount = 1
            END
            ELSE
            BEGIN
                SET @FailedPasswordAnswerAttemptWindowStart = @CurrentTimeUtc
                SET @FailedPasswordAnswerAttemptCount = @FailedPasswordAnswerAttemptCount + 1
            END

            BEGIN
                IF( @FailedPasswordAnswerAttemptCount >= @MaxInvalidPasswordAttempts )
                BEGIN
                    SET @IsLockedOut = 1
                    SET @LastLockoutDate = @CurrentTimeUtc
                END
            END

            SET @ErrorCode = 3
        END
    ELSE
        BEGIN
            IF( @FailedPasswordAnswerAttemptCount > 0 )
            BEGIN
                SET @FailedPasswordAnswerAttemptCount = 0
                SET @FailedPasswordAnswerAttemptWindowStart = CONVERT( datetime, '17540101', 112 )
            END
        END

    IF( NOT ( @PasswordAnswer IS NULL ) )
    BEGIN
        UPDATE dbo.aspnet_Membership
        SET IsLockedOut = @IsLockedOut, LastLockoutDate = @LastLockoutDate,
            FailedPasswordAttemptCount = @FailedPasswordAttemptCount,
            FailedPasswordAttemptWindowStart = @FailedPasswordAttemptWindowStart,
            FailedPasswordAnswerAttemptCount = @FailedPasswordAnswerAttemptCount,
            FailedPasswordAnswerAttemptWindowStart = @FailedPasswordAnswerAttemptWindowStart
        WHERE @UserId = UserId

        IF( @@ERROR <> 0 )
        BEGIN
            SET @ErrorCode = -1
            GOTO Cleanup
        END
    END

    IF( @TranStarted = 1 )
    BEGIN
	SET @TranStarted = 0
	COMMIT TRANSACTION
    END

    RETURN @ErrorCode

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
    	ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode

END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_Insert]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Inserts a record into the aspnet_Membership table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_Membership_Insert]
(

	@ApplicationId uniqueidentifier   ,

	@UserId uniqueidentifier   ,

	@Password nvarchar (128)  ,

	@PasswordFormat int   ,

	@PasswordSalt nvarchar (128)  ,

	@MobilePin nvarchar (16)  ,

	@Email nvarchar (256)  ,

	@LoweredEmail nvarchar (256)  ,

	@PasswordQuestion nvarchar (256)  ,

	@PasswordAnswer nvarchar (128)  ,

	@IsApproved bit   ,

	@IsLockedOut bit   ,

	@CreateDate datetime   ,

	@LastLoginDate datetime   ,

	@LastPasswordChangedDate datetime   ,

	@LastLockoutDate datetime   ,

	@FailedPasswordAttemptCount int   ,

	@FailedPasswordAttemptWindowStart datetime   ,

	@FailedPasswordAnswerAttemptCount int   ,

	@FailedPasswordAnswerAttemptWindowStart datetime   ,

	@Comment ntext   
)
AS


				
				INSERT INTO [dbo].[aspnet_Membership]
					(
					[ApplicationId]
					,[UserId]
					,[Password]
					,[PasswordFormat]
					,[PasswordSalt]
					,[MobilePIN]
					,[Email]
					,[LoweredEmail]
					,[PasswordQuestion]
					,[PasswordAnswer]
					,[IsApproved]
					,[IsLockedOut]
					,[CreateDate]
					,[LastLoginDate]
					,[LastPasswordChangedDate]
					,[LastLockoutDate]
					,[FailedPasswordAttemptCount]
					,[FailedPasswordAttemptWindowStart]
					,[FailedPasswordAnswerAttemptCount]
					,[FailedPasswordAnswerAttemptWindowStart]
					,[Comment]
					)
				VALUES
					(
					@ApplicationId
					,@UserId
					,@Password
					,@PasswordFormat
					,@PasswordSalt
					,@MobilePin
					,@Email
					,@LoweredEmail
					,@PasswordQuestion
					,@PasswordAnswer
					,@IsApproved
					,@IsLockedOut
					,@CreateDate
					,@LastLoginDate
					,@LastPasswordChangedDate
					,@LastLockoutDate
					,@FailedPasswordAttemptCount
					,@FailedPasswordAttemptWindowStart
					,@FailedPasswordAnswerAttemptCount
					,@FailedPasswordAnswerAttemptWindowStart
					,@Comment
					)
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_GetUserByUserId]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_GetUserByUserId]
    @UserId               uniqueidentifier,
    @CurrentTimeUtc       datetime,
    @UpdateLastActivity   bit = 0
AS
BEGIN
    IF ( @UpdateLastActivity = 1 )
    BEGIN
        UPDATE   dbo.aspnet_Users
        SET      LastActivityDate = @CurrentTimeUtc
        FROM     dbo.aspnet_Users
        WHERE    @UserId = UserId

        IF ( @@ROWCOUNT = 0 ) -- User ID not found
            RETURN -1
    END

    SELECT  m.Email, m.PasswordQuestion, m.Comment, m.IsApproved,
            m.CreateDate, m.LastLoginDate, u.LastActivityDate,
            m.LastPasswordChangedDate, u.UserName, m.IsLockedOut,
            m.LastLockoutDate
    FROM    dbo.aspnet_Users u, dbo.aspnet_Membership m
    WHERE   @UserId = u.UserId AND u.UserId = m.UserId

    IF ( @@ROWCOUNT = 0 ) -- User ID not found
       RETURN -1

    RETURN 0
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_GetUserByName]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_GetUserByName]
    @ApplicationName      nvarchar(256),
    @UserName             nvarchar(256),
    @CurrentTimeUtc       datetime,
    @UpdateLastActivity   bit = 0
AS
BEGIN
    DECLARE @UserId uniqueidentifier

    IF (@UpdateLastActivity = 1)
    BEGIN
        -- select user ID from aspnet_users table
        SELECT TOP 1 @UserId = u.UserId
        FROM    dbo.aspnet_Applications a, dbo.aspnet_Users u, dbo.aspnet_Membership m
        WHERE    LOWER(@ApplicationName) = a.LoweredApplicationName AND
                u.ApplicationId = a.ApplicationId    AND
                LOWER(@UserName) = u.LoweredUserName AND u.UserId = m.UserId

        IF (@@ROWCOUNT = 0) -- Username not found
            RETURN -1

        UPDATE   dbo.aspnet_Users
        SET      LastActivityDate = @CurrentTimeUtc
        WHERE    @UserId = UserId

        SELECT m.Email, m.PasswordQuestion, m.Comment, m.IsApproved,
                m.CreateDate, m.LastLoginDate, u.LastActivityDate, m.LastPasswordChangedDate,
                u.UserId, m.IsLockedOut, m.LastLockoutDate
        FROM    dbo.aspnet_Applications a, dbo.aspnet_Users u, dbo.aspnet_Membership m
        WHERE  @UserId = u.UserId AND u.UserId = m.UserId 
    END
    ELSE
    BEGIN
        SELECT TOP 1 m.Email, m.PasswordQuestion, m.Comment, m.IsApproved,
                m.CreateDate, m.LastLoginDate, u.LastActivityDate, m.LastPasswordChangedDate,
                u.UserId, m.IsLockedOut,m.LastLockoutDate
        FROM    dbo.aspnet_Applications a, dbo.aspnet_Users u, dbo.aspnet_Membership m
        WHERE    LOWER(@ApplicationName) = a.LoweredApplicationName AND
                u.ApplicationId = a.ApplicationId    AND
                LOWER(@UserName) = u.LoweredUserName AND u.UserId = m.UserId

        IF (@@ROWCOUNT = 0) -- Username not found
            RETURN -1
    END

    RETURN 0
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_GetUserByEmail]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_GetUserByEmail]
    @ApplicationName  nvarchar(256),
    @Email            nvarchar(256)
AS
BEGIN
    IF( @Email IS NULL )
        SELECT  u.UserName
        FROM    dbo.aspnet_Applications a, dbo.aspnet_Users u, dbo.aspnet_Membership m
        WHERE   LOWER(@ApplicationName) = a.LoweredApplicationName AND
                u.ApplicationId = a.ApplicationId    AND
                u.UserId = m.UserId AND
                m.LoweredEmail IS NULL
    ELSE
        SELECT  u.UserName
        FROM    dbo.aspnet_Applications a, dbo.aspnet_Users u, dbo.aspnet_Membership m
        WHERE   LOWER(@ApplicationName) = a.LoweredApplicationName AND
                u.ApplicationId = a.ApplicationId    AND
                u.UserId = m.UserId AND
                LOWER(@Email) = m.LoweredEmail

    IF (@@rowcount = 0)
        RETURN(1)
    RETURN(0)
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_GetPasswordWithFormat]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_GetPasswordWithFormat]
    @ApplicationName                nvarchar(256),
    @UserName                       nvarchar(256),
    @UpdateLastLoginActivityDate    bit,
    @CurrentTimeUtc                 datetime
AS
BEGIN
    DECLARE @IsLockedOut                        bit
    DECLARE @UserId                             uniqueidentifier
    DECLARE @Password                           nvarchar(128)
    DECLARE @PasswordSalt                       nvarchar(128)
    DECLARE @PasswordFormat                     int
    DECLARE @FailedPasswordAttemptCount         int
    DECLARE @FailedPasswordAnswerAttemptCount   int
    DECLARE @IsApproved                         bit
    DECLARE @LastActivityDate                   datetime
    DECLARE @LastLoginDate                      datetime

    SELECT  @UserId          = NULL

    SELECT  @UserId = u.UserId, @IsLockedOut = m.IsLockedOut, @Password=Password, @PasswordFormat=PasswordFormat,
            @PasswordSalt=PasswordSalt, @FailedPasswordAttemptCount=FailedPasswordAttemptCount,
		    @FailedPasswordAnswerAttemptCount=FailedPasswordAnswerAttemptCount, @IsApproved=IsApproved,
            @LastActivityDate = LastActivityDate, @LastLoginDate = LastLoginDate
    FROM    dbo.aspnet_Applications a, dbo.aspnet_Users u, dbo.aspnet_Membership m
    WHERE   LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.ApplicationId = a.ApplicationId    AND
            u.UserId = m.UserId AND
            LOWER(@UserName) = u.LoweredUserName

    IF (@UserId IS NULL)
        RETURN 1

    IF (@IsLockedOut = 1)
        RETURN 99

    SELECT   @Password, @PasswordFormat, @PasswordSalt, @FailedPasswordAttemptCount,
             @FailedPasswordAnswerAttemptCount, @IsApproved, @LastLoginDate, @LastActivityDate

    IF (@UpdateLastLoginActivityDate = 1 AND @IsApproved = 1)
    BEGIN
        UPDATE  dbo.aspnet_Membership
        SET     LastLoginDate = @CurrentTimeUtc
        WHERE   UserId = @UserId

        UPDATE  dbo.aspnet_Users
        SET     LastActivityDate = @CurrentTimeUtc
        WHERE   @UserId = UserId
    END


    RETURN 0
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_GetPassword]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_GetPassword]
    @ApplicationName                nvarchar(256),
    @UserName                       nvarchar(256),
    @MaxInvalidPasswordAttempts     int,
    @PasswordAttemptWindow          int,
    @CurrentTimeUtc                 datetime,
    @PasswordAnswer                 nvarchar(128) = NULL
AS
BEGIN
    DECLARE @UserId                                 uniqueidentifier
    DECLARE @PasswordFormat                         int
    DECLARE @Password                               nvarchar(128)
    DECLARE @passAns                                nvarchar(128)
    DECLARE @IsLockedOut                            bit
    DECLARE @LastLockoutDate                        datetime
    DECLARE @FailedPasswordAttemptCount             int
    DECLARE @FailedPasswordAttemptWindowStart       datetime
    DECLARE @FailedPasswordAnswerAttemptCount       int
    DECLARE @FailedPasswordAnswerAttemptWindowStart datetime

    DECLARE @ErrorCode     int
    SET @ErrorCode = 0

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
	    BEGIN TRANSACTION
	    SET @TranStarted = 1
    END
    ELSE
    	SET @TranStarted = 0

    SELECT  @UserId = u.UserId,
            @Password = m.Password,
            @passAns = m.PasswordAnswer,
            @PasswordFormat = m.PasswordFormat,
            @IsLockedOut = m.IsLockedOut,
            @LastLockoutDate = m.LastLockoutDate,
            @FailedPasswordAttemptCount = m.FailedPasswordAttemptCount,
            @FailedPasswordAttemptWindowStart = m.FailedPasswordAttemptWindowStart,
            @FailedPasswordAnswerAttemptCount = m.FailedPasswordAnswerAttemptCount,
            @FailedPasswordAnswerAttemptWindowStart = m.FailedPasswordAnswerAttemptWindowStart
    FROM    dbo.aspnet_Applications a, dbo.aspnet_Users u, dbo.aspnet_Membership m WITH ( UPDLOCK )
    WHERE   LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.ApplicationId = a.ApplicationId    AND
            u.UserId = m.UserId AND
            LOWER(@UserName) = u.LoweredUserName

    IF ( @@rowcount = 0 )
    BEGIN
        SET @ErrorCode = 1
        GOTO Cleanup
    END

    IF( @IsLockedOut = 1 )
    BEGIN
        SET @ErrorCode = 99
        GOTO Cleanup
    END

    IF ( NOT( @PasswordAnswer IS NULL ) )
    BEGIN
        IF( ( @passAns IS NULL ) OR ( LOWER( @passAns ) <> LOWER( @PasswordAnswer ) ) )
        BEGIN
            IF( @CurrentTimeUtc > DATEADD( minute, @PasswordAttemptWindow, @FailedPasswordAnswerAttemptWindowStart ) )
            BEGIN
                SET @FailedPasswordAnswerAttemptWindowStart = @CurrentTimeUtc
                SET @FailedPasswordAnswerAttemptCount = 1
            END
            ELSE
            BEGIN
                SET @FailedPasswordAnswerAttemptCount = @FailedPasswordAnswerAttemptCount + 1
                SET @FailedPasswordAnswerAttemptWindowStart = @CurrentTimeUtc
            END

            BEGIN
                IF( @FailedPasswordAnswerAttemptCount >= @MaxInvalidPasswordAttempts )
                BEGIN
                    SET @IsLockedOut = 1
                    SET @LastLockoutDate = @CurrentTimeUtc
                END
            END

            SET @ErrorCode = 3
        END
        ELSE
        BEGIN
            IF( @FailedPasswordAnswerAttemptCount > 0 )
            BEGIN
                SET @FailedPasswordAnswerAttemptCount = 0
                SET @FailedPasswordAnswerAttemptWindowStart = CONVERT( datetime, '17540101', 112 )
            END
        END

        UPDATE dbo.aspnet_Membership
        SET IsLockedOut = @IsLockedOut, LastLockoutDate = @LastLockoutDate,
            FailedPasswordAttemptCount = @FailedPasswordAttemptCount,
            FailedPasswordAttemptWindowStart = @FailedPasswordAttemptWindowStart,
            FailedPasswordAnswerAttemptCount = @FailedPasswordAnswerAttemptCount,
            FailedPasswordAnswerAttemptWindowStart = @FailedPasswordAnswerAttemptWindowStart
        WHERE @UserId = UserId

        IF( @@ERROR <> 0 )
        BEGIN
            SET @ErrorCode = -1
            GOTO Cleanup
        END
    END

    IF( @TranStarted = 1 )
    BEGIN
	SET @TranStarted = 0
	COMMIT TRANSACTION
    END

    IF( @ErrorCode = 0 )
        SELECT @Password, @PasswordFormat

    RETURN @ErrorCode

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
    	ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode

END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_GetPaged]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets records from the aspnet_Membership table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_Membership_GetPaged]
(

	@WhereClause varchar (2000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [UserId] uniqueidentifier 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([UserId])'
				SET @SQL = @SQL + ' SELECT'
				IF @PageSize > 0
				BEGIN
					SET @SQL = @SQL + ' TOP ' + CONVERT(nvarchar, @PageUpperBound)
				END
				SET @SQL = @SQL + ' [UserId]'
				SET @SQL = @SQL + ' FROM [dbo].[aspnet_Membership]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Return paged results
				SELECT O.[ApplicationId], O.[UserId], O.[Password], O.[PasswordFormat], O.[PasswordSalt], O.[MobilePIN], O.[Email], O.[LoweredEmail], O.[PasswordQuestion], O.[PasswordAnswer], O.[IsApproved], O.[IsLockedOut], O.[CreateDate], O.[LastLoginDate], O.[LastPasswordChangedDate], O.[LastLockoutDate], O.[FailedPasswordAttemptCount], O.[FailedPasswordAttemptWindowStart], O.[FailedPasswordAnswerAttemptCount], O.[FailedPasswordAnswerAttemptWindowStart], O.[Comment]
				FROM
				    [dbo].[aspnet_Membership] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[UserId] = PageIndex.[UserId]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[aspnet_Membership]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_GetNumberOfUsersOnline]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_GetNumberOfUsersOnline]
    @ApplicationName            nvarchar(256),
    @MinutesSinceLastInActive   int,
    @CurrentTimeUtc             datetime
AS
BEGIN
    DECLARE @DateActive datetime
    SELECT  @DateActive = DATEADD(minute,  -(@MinutesSinceLastInActive), @CurrentTimeUtc)

    DECLARE @NumOnline int
    SELECT  @NumOnline = COUNT(*)
    FROM    dbo.aspnet_Users u(NOLOCK),
            dbo.aspnet_Applications a(NOLOCK),
            dbo.aspnet_Membership m(NOLOCK)
    WHERE   u.ApplicationId = a.ApplicationId                  AND
            LastActivityDate > @DateActive                     AND
            a.LoweredApplicationName = LOWER(@ApplicationName) AND
            u.UserId = m.UserId
    RETURN(@NumOnline)
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_GetByUserId]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Select records from the aspnet_Membership table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_Membership_GetByUserId]
(

	@UserId uniqueidentifier   
)
AS


				SELECT
					[ApplicationId],
					[UserId],
					[Password],
					[PasswordFormat],
					[PasswordSalt],
					[MobilePIN],
					[Email],
					[LoweredEmail],
					[PasswordQuestion],
					[PasswordAnswer],
					[IsApproved],
					[IsLockedOut],
					[CreateDate],
					[LastLoginDate],
					[LastPasswordChangedDate],
					[LastLockoutDate],
					[FailedPasswordAttemptCount],
					[FailedPasswordAttemptWindowStart],
					[FailedPasswordAnswerAttemptCount],
					[FailedPasswordAnswerAttemptWindowStart],
					[Comment]
				FROM
					[dbo].[aspnet_Membership]
				WHERE
					[UserId] = @UserId
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_GetByApplicationIdLoweredEmail]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Select records from the aspnet_Membership table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_Membership_GetByApplicationIdLoweredEmail]
(

	@ApplicationId uniqueidentifier   ,

	@LoweredEmail nvarchar (256)  
)
AS


				SELECT
					[ApplicationId],
					[UserId],
					[Password],
					[PasswordFormat],
					[PasswordSalt],
					[MobilePIN],
					[Email],
					[LoweredEmail],
					[PasswordQuestion],
					[PasswordAnswer],
					[IsApproved],
					[IsLockedOut],
					[CreateDate],
					[LastLoginDate],
					[LastPasswordChangedDate],
					[LastLockoutDate],
					[FailedPasswordAttemptCount],
					[FailedPasswordAttemptWindowStart],
					[FailedPasswordAnswerAttemptCount],
					[FailedPasswordAnswerAttemptWindowStart],
					[Comment]
				FROM
					[dbo].[aspnet_Membership]
				WHERE
					[ApplicationId] = @ApplicationId
					AND [LoweredEmail] = @LoweredEmail
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_GetByApplicationId]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Select records from the aspnet_Membership table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_Membership_GetByApplicationId]
(

	@ApplicationId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[ApplicationId],
					[UserId],
					[Password],
					[PasswordFormat],
					[PasswordSalt],
					[MobilePIN],
					[Email],
					[LoweredEmail],
					[PasswordQuestion],
					[PasswordAnswer],
					[IsApproved],
					[IsLockedOut],
					[CreateDate],
					[LastLoginDate],
					[LastPasswordChangedDate],
					[LastLockoutDate],
					[FailedPasswordAttemptCount],
					[FailedPasswordAttemptWindowStart],
					[FailedPasswordAnswerAttemptCount],
					[FailedPasswordAnswerAttemptWindowStart],
					[Comment]
				FROM
					[dbo].[aspnet_Membership]
				WHERE
					[ApplicationId] = @ApplicationId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_GetAllUsers]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_GetAllUsers]
    @ApplicationName       nvarchar(256),
    @PageIndex             int,
    @PageSize              int
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM dbo.aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN 0


    -- Set the page bounds
    DECLARE @PageLowerBound int
    DECLARE @PageUpperBound int
    DECLARE @TotalRecords   int
    SET @PageLowerBound = @PageSize * @PageIndex
    SET @PageUpperBound = @PageSize - 1 + @PageLowerBound

    -- Create a temp table TO store the select results
    CREATE TABLE #PageIndexForUsers
    (
        IndexId int IDENTITY (0, 1) NOT NULL,
        UserId uniqueidentifier
    )

    -- Insert into our temp table
    INSERT INTO #PageIndexForUsers (UserId)
    SELECT u.UserId
    FROM   dbo.aspnet_Membership m, dbo.aspnet_Users u
    WHERE  u.ApplicationId = @ApplicationId AND u.UserId = m.UserId
    ORDER BY u.UserName

    SELECT @TotalRecords = @@ROWCOUNT

    SELECT u.UserName, m.Email, m.PasswordQuestion, m.Comment, m.IsApproved,
            m.CreateDate,
            m.LastLoginDate,
            u.LastActivityDate,
            m.LastPasswordChangedDate,
            u.UserId, m.IsLockedOut,
            m.LastLockoutDate
    FROM   dbo.aspnet_Membership m, dbo.aspnet_Users u, #PageIndexForUsers p
    WHERE  u.UserId = p.UserId AND u.UserId = m.UserId AND
           p.IndexId >= @PageLowerBound AND p.IndexId <= @PageUpperBound
    ORDER BY u.UserName
    RETURN @TotalRecords
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_Get_List]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets all records from the aspnet_Membership table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_Membership_Get_List]

AS


				
				SELECT
					[ApplicationId],
					[UserId],
					[Password],
					[PasswordFormat],
					[PasswordSalt],
					[MobilePIN],
					[Email],
					[LoweredEmail],
					[PasswordQuestion],
					[PasswordAnswer],
					[IsApproved],
					[IsLockedOut],
					[CreateDate],
					[LastLoginDate],
					[LastPasswordChangedDate],
					[LastLockoutDate],
					[FailedPasswordAttemptCount],
					[FailedPasswordAttemptWindowStart],
					[FailedPasswordAnswerAttemptCount],
					[FailedPasswordAnswerAttemptWindowStart],
					[Comment]
				FROM
					[dbo].[aspnet_Membership]
					
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_FindUsersByName]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_FindUsersByName]
    @ApplicationName       nvarchar(256),
    @UserNameToMatch       nvarchar(256),
    @PageIndex             int,
    @PageSize              int
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM dbo.aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN 0

    -- Set the page bounds
    DECLARE @PageLowerBound int
    DECLARE @PageUpperBound int
    DECLARE @TotalRecords   int
    SET @PageLowerBound = @PageSize * @PageIndex
    SET @PageUpperBound = @PageSize - 1 + @PageLowerBound

    -- Create a temp table TO store the select results
    CREATE TABLE #PageIndexForUsers
    (
        IndexId int IDENTITY (0, 1) NOT NULL,
        UserId uniqueidentifier
    )

    -- Insert into our temp table
    INSERT INTO #PageIndexForUsers (UserId)
        SELECT u.UserId
        FROM   dbo.aspnet_Users u, dbo.aspnet_Membership m
        WHERE  u.ApplicationId = @ApplicationId AND m.UserId = u.UserId AND u.LoweredUserName LIKE LOWER(@UserNameToMatch)
        ORDER BY u.UserName


    SELECT  u.UserName, m.Email, m.PasswordQuestion, m.Comment, m.IsApproved,
            m.CreateDate,
            m.LastLoginDate,
            u.LastActivityDate,
            m.LastPasswordChangedDate,
            u.UserId, m.IsLockedOut,
            m.LastLockoutDate
    FROM   dbo.aspnet_Membership m, dbo.aspnet_Users u, #PageIndexForUsers p
    WHERE  u.UserId = p.UserId AND u.UserId = m.UserId AND
           p.IndexId >= @PageLowerBound AND p.IndexId <= @PageUpperBound
    ORDER BY u.UserName

    SELECT  @TotalRecords = COUNT(*)
    FROM    #PageIndexForUsers
    RETURN @TotalRecords
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_FindUsersByEmail]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_FindUsersByEmail]
    @ApplicationName       nvarchar(256),
    @EmailToMatch          nvarchar(256),
    @PageIndex             int,
    @PageSize              int
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM dbo.aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN 0

    -- Set the page bounds
    DECLARE @PageLowerBound int
    DECLARE @PageUpperBound int
    DECLARE @TotalRecords   int
    SET @PageLowerBound = @PageSize * @PageIndex
    SET @PageUpperBound = @PageSize - 1 + @PageLowerBound

    -- Create a temp table TO store the select results
    CREATE TABLE #PageIndexForUsers
    (
        IndexId int IDENTITY (0, 1) NOT NULL,
        UserId uniqueidentifier
    )

    -- Insert into our temp table
    IF( @EmailToMatch IS NULL )
        INSERT INTO #PageIndexForUsers (UserId)
            SELECT u.UserId
            FROM   dbo.aspnet_Users u, dbo.aspnet_Membership m
            WHERE  u.ApplicationId = @ApplicationId AND m.UserId = u.UserId AND m.Email IS NULL
            ORDER BY m.LoweredEmail
    ELSE
        INSERT INTO #PageIndexForUsers (UserId)
            SELECT u.UserId
            FROM   dbo.aspnet_Users u, dbo.aspnet_Membership m
            WHERE  u.ApplicationId = @ApplicationId AND m.UserId = u.UserId AND m.LoweredEmail LIKE LOWER(@EmailToMatch)
            ORDER BY m.LoweredEmail

    SELECT  u.UserName, m.Email, m.PasswordQuestion, m.Comment, m.IsApproved,
            m.CreateDate,
            m.LastLoginDate,
            u.LastActivityDate,
            m.LastPasswordChangedDate,
            u.UserId, m.IsLockedOut,
            m.LastLockoutDate
    FROM   dbo.aspnet_Membership m, dbo.aspnet_Users u, #PageIndexForUsers p
    WHERE  u.UserId = p.UserId AND u.UserId = m.UserId AND
           p.IndexId >= @PageLowerBound AND p.IndexId <= @PageUpperBound
    ORDER BY m.LoweredEmail

    SELECT  @TotalRecords = COUNT(*)
    FROM    #PageIndexForUsers
    RETURN @TotalRecords
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_Find]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Finds records in the aspnet_Membership table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_Membership_Find]
(

	@SearchUsingOR bit   = null ,

	@ApplicationId uniqueidentifier   = null ,

	@UserId uniqueidentifier   = null ,

	@Password nvarchar (128)  = null ,

	@PasswordFormat int   = null ,

	@PasswordSalt nvarchar (128)  = null ,

	@MobilePin nvarchar (16)  = null ,

	@Email nvarchar (256)  = null ,

	@LoweredEmail nvarchar (256)  = null ,

	@PasswordQuestion nvarchar (256)  = null ,

	@PasswordAnswer nvarchar (128)  = null ,

	@IsApproved bit   = null ,

	@IsLockedOut bit   = null ,

	@CreateDate datetime   = null ,

	@LastLoginDate datetime   = null ,

	@LastPasswordChangedDate datetime   = null ,

	@LastLockoutDate datetime   = null ,

	@FailedPasswordAttemptCount int   = null ,

	@FailedPasswordAttemptWindowStart datetime   = null ,

	@FailedPasswordAnswerAttemptCount int   = null ,

	@FailedPasswordAnswerAttemptWindowStart datetime   = null ,

	@Comment ntext   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [ApplicationId]
	, [UserId]
	, [Password]
	, [PasswordFormat]
	, [PasswordSalt]
	, [MobilePIN]
	, [Email]
	, [LoweredEmail]
	, [PasswordQuestion]
	, [PasswordAnswer]
	, [IsApproved]
	, [IsLockedOut]
	, [CreateDate]
	, [LastLoginDate]
	, [LastPasswordChangedDate]
	, [LastLockoutDate]
	, [FailedPasswordAttemptCount]
	, [FailedPasswordAttemptWindowStart]
	, [FailedPasswordAnswerAttemptCount]
	, [FailedPasswordAnswerAttemptWindowStart]
	, [Comment]
    FROM
	[dbo].[aspnet_Membership]
    WHERE 
	 ([ApplicationId] = @ApplicationId OR @ApplicationId IS NULL)
	AND ([UserId] = @UserId OR @UserId IS NULL)
	AND ([Password] = @Password OR @Password IS NULL)
	AND ([PasswordFormat] = @PasswordFormat OR @PasswordFormat IS NULL)
	AND ([PasswordSalt] = @PasswordSalt OR @PasswordSalt IS NULL)
	AND ([MobilePIN] = @MobilePin OR @MobilePin IS NULL)
	AND ([Email] = @Email OR @Email IS NULL)
	AND ([LoweredEmail] = @LoweredEmail OR @LoweredEmail IS NULL)
	AND ([PasswordQuestion] = @PasswordQuestion OR @PasswordQuestion IS NULL)
	AND ([PasswordAnswer] = @PasswordAnswer OR @PasswordAnswer IS NULL)
	AND ([IsApproved] = @IsApproved OR @IsApproved IS NULL)
	AND ([IsLockedOut] = @IsLockedOut OR @IsLockedOut IS NULL)
	AND ([CreateDate] = @CreateDate OR @CreateDate IS NULL)
	AND ([LastLoginDate] = @LastLoginDate OR @LastLoginDate IS NULL)
	AND ([LastPasswordChangedDate] = @LastPasswordChangedDate OR @LastPasswordChangedDate IS NULL)
	AND ([LastLockoutDate] = @LastLockoutDate OR @LastLockoutDate IS NULL)
	AND ([FailedPasswordAttemptCount] = @FailedPasswordAttemptCount OR @FailedPasswordAttemptCount IS NULL)
	AND ([FailedPasswordAttemptWindowStart] = @FailedPasswordAttemptWindowStart OR @FailedPasswordAttemptWindowStart IS NULL)
	AND ([FailedPasswordAnswerAttemptCount] = @FailedPasswordAnswerAttemptCount OR @FailedPasswordAnswerAttemptCount IS NULL)
	AND ([FailedPasswordAnswerAttemptWindowStart] = @FailedPasswordAnswerAttemptWindowStart OR @FailedPasswordAnswerAttemptWindowStart IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [ApplicationId]
	, [UserId]
	, [Password]
	, [PasswordFormat]
	, [PasswordSalt]
	, [MobilePIN]
	, [Email]
	, [LoweredEmail]
	, [PasswordQuestion]
	, [PasswordAnswer]
	, [IsApproved]
	, [IsLockedOut]
	, [CreateDate]
	, [LastLoginDate]
	, [LastPasswordChangedDate]
	, [LastLockoutDate]
	, [FailedPasswordAttemptCount]
	, [FailedPasswordAttemptWindowStart]
	, [FailedPasswordAnswerAttemptCount]
	, [FailedPasswordAnswerAttemptWindowStart]
	, [Comment]
    FROM
	[dbo].[aspnet_Membership]
    WHERE 
	 ([ApplicationId] = @ApplicationId AND @ApplicationId is not null)
	OR ([UserId] = @UserId AND @UserId is not null)
	OR ([Password] = @Password AND @Password is not null)
	OR ([PasswordFormat] = @PasswordFormat AND @PasswordFormat is not null)
	OR ([PasswordSalt] = @PasswordSalt AND @PasswordSalt is not null)
	OR ([MobilePIN] = @MobilePin AND @MobilePin is not null)
	OR ([Email] = @Email AND @Email is not null)
	OR ([LoweredEmail] = @LoweredEmail AND @LoweredEmail is not null)
	OR ([PasswordQuestion] = @PasswordQuestion AND @PasswordQuestion is not null)
	OR ([PasswordAnswer] = @PasswordAnswer AND @PasswordAnswer is not null)
	OR ([IsApproved] = @IsApproved AND @IsApproved is not null)
	OR ([IsLockedOut] = @IsLockedOut AND @IsLockedOut is not null)
	OR ([CreateDate] = @CreateDate AND @CreateDate is not null)
	OR ([LastLoginDate] = @LastLoginDate AND @LastLoginDate is not null)
	OR ([LastPasswordChangedDate] = @LastPasswordChangedDate AND @LastPasswordChangedDate is not null)
	OR ([LastLockoutDate] = @LastLockoutDate AND @LastLockoutDate is not null)
	OR ([FailedPasswordAttemptCount] = @FailedPasswordAttemptCount AND @FailedPasswordAttemptCount is not null)
	OR ([FailedPasswordAttemptWindowStart] = @FailedPasswordAttemptWindowStart AND @FailedPasswordAttemptWindowStart is not null)
	OR ([FailedPasswordAnswerAttemptCount] = @FailedPasswordAnswerAttemptCount AND @FailedPasswordAnswerAttemptCount is not null)
	OR ([FailedPasswordAnswerAttemptWindowStart] = @FailedPasswordAnswerAttemptWindowStart AND @FailedPasswordAnswerAttemptWindowStart is not null)
	SELECT @@ROWCOUNT			
  END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_Delete]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Deletes a record in the aspnet_Membership table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_Membership_Delete]
(

	@UserId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[aspnet_Membership] WITH (ROWLOCK) 
				WHERE
					[UserId] = @UserId
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_CreateUser]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_CreateUser]
    @ApplicationName                        nvarchar(256),
    @UserName                               nvarchar(256),
    @Password                               nvarchar(128),
    @PasswordSalt                           nvarchar(128),
    @Email                                  nvarchar(256),
    @PasswordQuestion                       nvarchar(256),
    @PasswordAnswer                         nvarchar(128),
    @IsApproved                             bit,
    @CurrentTimeUtc                         datetime,
    @CreateDate                             datetime = NULL,
    @UniqueEmail                            int      = 0,
    @PasswordFormat                         int      = 0,
    @UserId                                 uniqueidentifier OUTPUT
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL

    DECLARE @NewUserId uniqueidentifier
    SELECT @NewUserId = NULL

    DECLARE @IsLockedOut bit
    SET @IsLockedOut = 0

    DECLARE @LastLockoutDate  datetime
    SET @LastLockoutDate = CONVERT( datetime, '17540101', 112 )

    DECLARE @FailedPasswordAttemptCount int
    SET @FailedPasswordAttemptCount = 0

    DECLARE @FailedPasswordAttemptWindowStart  datetime
    SET @FailedPasswordAttemptWindowStart = CONVERT( datetime, '17540101', 112 )

    DECLARE @FailedPasswordAnswerAttemptCount int
    SET @FailedPasswordAnswerAttemptCount = 0

    DECLARE @FailedPasswordAnswerAttemptWindowStart  datetime
    SET @FailedPasswordAnswerAttemptWindowStart = CONVERT( datetime, '17540101', 112 )

    DECLARE @NewUserCreated bit
    DECLARE @ReturnValue   int
    SET @ReturnValue = 0

    DECLARE @ErrorCode     int
    SET @ErrorCode = 0

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
	    BEGIN TRANSACTION
	    SET @TranStarted = 1
    END
    ELSE
    	SET @TranStarted = 0

    EXEC dbo.aspnet_Applications_CreateApplication @ApplicationName, @ApplicationId OUTPUT

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    SET @CreateDate = @CurrentTimeUtc

    SELECT  @NewUserId = UserId FROM dbo.aspnet_Users WHERE LOWER(@UserName) = LoweredUserName AND @ApplicationId = ApplicationId
    IF ( @NewUserId IS NULL )
    BEGIN
        SET @NewUserId = @UserId
        EXEC @ReturnValue = dbo.aspnet_Users_CreateUser @ApplicationId, @UserName, 0, @CreateDate, @NewUserId OUTPUT
        SET @NewUserCreated = 1
    END
    ELSE
    BEGIN
        SET @NewUserCreated = 0
        IF( @NewUserId <> @UserId AND @UserId IS NOT NULL )
        BEGIN
            SET @ErrorCode = 6
            GOTO Cleanup
        END
    END

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF( @ReturnValue = -1 )
    BEGIN
        SET @ErrorCode = 10
        GOTO Cleanup
    END

    IF ( EXISTS ( SELECT UserId
                  FROM   dbo.aspnet_Membership
                  WHERE  @NewUserId = UserId ) )
    BEGIN
        SET @ErrorCode = 6
        GOTO Cleanup
    END

    SET @UserId = @NewUserId

    IF (@UniqueEmail = 1)
    BEGIN
        IF (EXISTS (SELECT *
                    FROM  dbo.aspnet_Membership m WITH ( UPDLOCK, HOLDLOCK )
                    WHERE ApplicationId = @ApplicationId AND LoweredEmail = LOWER(@Email)))
        BEGIN
            SET @ErrorCode = 7
            GOTO Cleanup
        END
    END

    IF (@NewUserCreated = 0)
    BEGIN
        UPDATE dbo.aspnet_Users
        SET    LastActivityDate = @CreateDate
        WHERE  @UserId = UserId
        IF( @@ERROR <> 0 )
        BEGIN
            SET @ErrorCode = -1
            GOTO Cleanup
        END
    END

    INSERT INTO dbo.aspnet_Membership
                ( ApplicationId,
                  UserId,
                  Password,
                  PasswordSalt,
                  Email,
                  LoweredEmail,
                  PasswordQuestion,
                  PasswordAnswer,
                  PasswordFormat,
                  IsApproved,
                  IsLockedOut,
                  CreateDate,
                  LastLoginDate,
                  LastPasswordChangedDate,
                  LastLockoutDate,
                  FailedPasswordAttemptCount,
                  FailedPasswordAttemptWindowStart,
                  FailedPasswordAnswerAttemptCount,
                  FailedPasswordAnswerAttemptWindowStart )
         VALUES ( @ApplicationId,
                  @UserId,
                  @Password,
                  @PasswordSalt,
                  @Email,
                  LOWER(@Email),
                  @PasswordQuestion,
                  @PasswordAnswer,
                  @PasswordFormat,
                  @IsApproved,
                  @IsLockedOut,
                  @CreateDate,
                  @CreateDate,
                  @CreateDate,
                  @LastLockoutDate,
                  @FailedPasswordAttemptCount,
                  @FailedPasswordAttemptWindowStart,
                  @FailedPasswordAnswerAttemptCount,
                  @FailedPasswordAnswerAttemptWindowStart )

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF( @TranStarted = 1 )
    BEGIN
	    SET @TranStarted = 0
	    COMMIT TRANSACTION
    END

    RETURN 0

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
    	ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode

END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_ChangePasswordQuestionAndAnswer]    Script Date: 10/11/2011 11:07:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_ChangePasswordQuestionAndAnswer]
    @ApplicationName       nvarchar(256),
    @UserName              nvarchar(256),
    @NewPasswordQuestion   nvarchar(256),
    @NewPasswordAnswer     nvarchar(128)
AS
BEGIN
    DECLARE @UserId uniqueidentifier
    SELECT  @UserId = NULL
    SELECT  @UserId = u.UserId
    FROM    dbo.aspnet_Membership m, dbo.aspnet_Users u, dbo.aspnet_Applications a
    WHERE   LoweredUserName = LOWER(@UserName) AND
            u.ApplicationId = a.ApplicationId  AND
            LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.UserId = m.UserId
    IF (@UserId IS NULL)
    BEGIN
        RETURN(1)
    END

    UPDATE dbo.aspnet_Membership
    SET    PasswordQuestion = @NewPasswordQuestion, PasswordAnswer = @NewPasswordAnswer
    WHERE  UserId=@UserId
    RETURN(0)
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_AnyDataInTables]    Script Date: 10/11/2011 11:07:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_AnyDataInTables]
    @TablesToCheck int
AS
BEGIN
    -- Check Membership table if (@TablesToCheck & 1) is set
    IF ((@TablesToCheck & 1) <> 0 AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N'vw_aspnet_MembershipUsers') AND (type = 'V'))))
    BEGIN
        IF (EXISTS(SELECT TOP 1 UserId FROM dbo.aspnet_Membership))
        BEGIN
            SELECT N'aspnet_Membership'
            RETURN
        END
    END

    -- Check aspnet_Roles table if (@TablesToCheck & 2) is set
    IF ((@TablesToCheck & 2) <> 0  AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N'vw_aspnet_Roles') AND (type = 'V'))) )
    BEGIN
        IF (EXISTS(SELECT TOP 1 RoleId FROM dbo.aspnet_Roles))
        BEGIN
            SELECT N'aspnet_Roles'
            RETURN
        END
    END

    -- Check aspnet_Profile table if (@TablesToCheck & 4) is set
    IF ((@TablesToCheck & 4) <> 0  AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N'vw_aspnet_Profiles') AND (type = 'V'))) )
    BEGIN
        IF (EXISTS(SELECT TOP 1 UserId FROM dbo.aspnet_Profile))
        BEGIN
            SELECT N'aspnet_Profile'
            RETURN
        END
    END

    -- Check aspnet_PersonalizationPerUser table if (@TablesToCheck & 8) is set
    IF ((@TablesToCheck & 8) <> 0  AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N'vw_aspnet_WebPartState_User') AND (type = 'V'))) )
    BEGIN
        IF (EXISTS(SELECT TOP 1 UserId FROM dbo.aspnet_PersonalizationPerUser))
        BEGIN
            SELECT N'aspnet_PersonalizationPerUser'
            RETURN
        END
    END

    -- Check aspnet_PersonalizationPerUser table if (@TablesToCheck & 16) is set
    IF ((@TablesToCheck & 16) <> 0  AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N'aspnet_WebEvent_LogEvent') AND (type = 'P'))) )
    BEGIN
        IF (EXISTS(SELECT TOP 1 * FROM dbo.aspnet_WebEvent_Events))
        BEGIN
            SELECT N'aspnet_WebEvent_Events'
            RETURN
        END
    END

    -- Check aspnet_Users table if (@TablesToCheck & 1,2,4 & 8) are all set
    IF ((@TablesToCheck & 1) <> 0 AND
        (@TablesToCheck & 2) <> 0 AND
        (@TablesToCheck & 4) <> 0 AND
        (@TablesToCheck & 8) <> 0 AND
        (@TablesToCheck & 32) <> 0 AND
        (@TablesToCheck & 128) <> 0 AND
        (@TablesToCheck & 256) <> 0 AND
        (@TablesToCheck & 512) <> 0 AND
        (@TablesToCheck & 1024) <> 0)
    BEGIN
        IF (EXISTS(SELECT TOP 1 UserId FROM dbo.aspnet_Users))
        BEGIN
            SELECT N'aspnet_Users'
            RETURN
        END
        IF (EXISTS(SELECT TOP 1 ApplicationId FROM dbo.aspnet_Applications))
        BEGIN
            SELECT N'aspnet_Applications'
            RETURN
        END
    END
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Roles_GetByUserIdFromAspnetUsersInRoles]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets records through a junction table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_Roles_GetByUserIdFromAspnetUsersInRoles]
(

	@UserId uniqueidentifier   
)
AS


SELECT dbo.[aspnet_Roles].[ApplicationId]
       ,dbo.[aspnet_Roles].[RoleId]
       ,dbo.[aspnet_Roles].[RoleName]
       ,dbo.[aspnet_Roles].[LoweredRoleName]
       ,dbo.[aspnet_Roles].[Description]
  FROM dbo.[aspnet_Roles]
 WHERE EXISTS (SELECT 1
                 FROM dbo.[aspnet_UsersInRoles] 
                WHERE dbo.[aspnet_UsersInRoles].[UserId] = @UserId
                  AND dbo.[aspnet_UsersInRoles].[RoleId] = dbo.[aspnet_Roles].[RoleId]
                  )
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationPerUser_Update]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Updates a record in the aspnet_PersonalizationPerUser table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_PersonalizationPerUser_Update]
(

	@Id uniqueidentifier   ,

	@OriginalId uniqueidentifier   ,

	@PathId uniqueidentifier   ,

	@UserId uniqueidentifier   ,

	@PageSettings image   ,

	@LastUpdatedDate datetime   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[aspnet_PersonalizationPerUser]
				SET
					[Id] = @Id
					,[PathId] = @PathId
					,[UserId] = @UserId
					,[PageSettings] = @PageSettings
					,[LastUpdatedDate] = @LastUpdatedDate
				WHERE
[Id] = @OriginalId
GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationPerUser_SetPageSettings]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_PersonalizationPerUser_SetPageSettings] (
    @ApplicationName  NVARCHAR(256),
    @UserName         NVARCHAR(256),
    @Path             NVARCHAR(256),
    @PageSettings     IMAGE,
    @CurrentTimeUtc   DATETIME)
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    DECLARE @PathId UNIQUEIDENTIFIER
    DECLARE @UserId UNIQUEIDENTIFIER

    SELECT @ApplicationId = NULL
    SELECT @PathId = NULL
    SELECT @UserId = NULL

    EXEC dbo.aspnet_Applications_CreateApplication @ApplicationName, @ApplicationId OUTPUT

    SELECT @PathId = u.PathId FROM dbo.aspnet_Paths u WHERE u.ApplicationId = @ApplicationId AND u.LoweredPath = LOWER(@Path)
    IF (@PathId IS NULL)
    BEGIN
        EXEC dbo.aspnet_Paths_CreatePath @ApplicationId, @Path, @PathId OUTPUT
    END

    SELECT @UserId = u.UserId FROM dbo.aspnet_Users u WHERE u.ApplicationId = @ApplicationId AND u.LoweredUserName = LOWER(@UserName)
    IF (@UserId IS NULL)
    BEGIN
        EXEC dbo.aspnet_Users_CreateUser @ApplicationId, @UserName, 0, @CurrentTimeUtc, @UserId OUTPUT
    END

    UPDATE   dbo.aspnet_Users WITH (ROWLOCK)
    SET      LastActivityDate = @CurrentTimeUtc
    WHERE    UserId = @UserId
    IF (@@ROWCOUNT = 0) -- Username not found
        RETURN

    IF (EXISTS(SELECT PathId FROM dbo.aspnet_PersonalizationPerUser WHERE UserId = @UserId AND PathId = @PathId))
        UPDATE dbo.aspnet_PersonalizationPerUser SET PageSettings = @PageSettings, LastUpdatedDate = @CurrentTimeUtc WHERE UserId = @UserId AND PathId = @PathId
    ELSE
        INSERT INTO dbo.aspnet_PersonalizationPerUser(UserId, PathId, PageSettings, LastUpdatedDate) VALUES (@UserId, @PathId, @PageSettings, @CurrentTimeUtc)
    RETURN 0
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationPerUser_ResetPageSettings]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_PersonalizationPerUser_ResetPageSettings] (
    @ApplicationName  NVARCHAR(256),
    @UserName         NVARCHAR(256),
    @Path             NVARCHAR(256),
    @CurrentTimeUtc   DATETIME)
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    DECLARE @PathId UNIQUEIDENTIFIER
    DECLARE @UserId UNIQUEIDENTIFIER

    SELECT @ApplicationId = NULL
    SELECT @PathId = NULL
    SELECT @UserId = NULL

    EXEC dbo.aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
    BEGIN
        RETURN
    END

    SELECT @PathId = u.PathId FROM dbo.aspnet_Paths u WHERE u.ApplicationId = @ApplicationId AND u.LoweredPath = LOWER(@Path)
    IF (@PathId IS NULL)
    BEGIN
        RETURN
    END

    SELECT @UserId = u.UserId FROM dbo.aspnet_Users u WHERE u.ApplicationId = @ApplicationId AND u.LoweredUserName = LOWER(@UserName)
    IF (@UserId IS NULL)
    BEGIN
        RETURN
    END

    UPDATE   dbo.aspnet_Users WITH (ROWLOCK)
    SET      LastActivityDate = @CurrentTimeUtc
    WHERE    UserId = @UserId
    IF (@@ROWCOUNT = 0) -- Username not found
        RETURN

    DELETE FROM dbo.aspnet_PersonalizationPerUser WHERE PathId = @PathId AND UserId = @UserId
    RETURN 0
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationPerUser_Insert]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Inserts a record into the aspnet_PersonalizationPerUser table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_PersonalizationPerUser_Insert]
(

	@Id uniqueidentifier    OUTPUT,

	@PathId uniqueidentifier   ,

	@UserId uniqueidentifier   ,

	@PageSettings image   ,

	@LastUpdatedDate datetime   
)
AS


				
				INSERT INTO [dbo].[aspnet_PersonalizationPerUser]
					(
					[Id]
					,[PathId]
					,[UserId]
					,[PageSettings]
					,[LastUpdatedDate]
					)
				VALUES
					(
					@Id
					,@PathId
					,@UserId
					,@PageSettings
					,@LastUpdatedDate
					)
GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationPerUser_GetPageSettings]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_PersonalizationPerUser_GetPageSettings] (
    @ApplicationName  NVARCHAR(256),
    @UserName         NVARCHAR(256),
    @Path             NVARCHAR(256),
    @CurrentTimeUtc   DATETIME)
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    DECLARE @PathId UNIQUEIDENTIFIER
    DECLARE @UserId UNIQUEIDENTIFIER

    SELECT @ApplicationId = NULL
    SELECT @PathId = NULL
    SELECT @UserId = NULL

    EXEC dbo.aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
    BEGIN
        RETURN
    END

    SELECT @PathId = u.PathId FROM dbo.aspnet_Paths u WHERE u.ApplicationId = @ApplicationId AND u.LoweredPath = LOWER(@Path)
    IF (@PathId IS NULL)
    BEGIN
        RETURN
    END

    SELECT @UserId = u.UserId FROM dbo.aspnet_Users u WHERE u.ApplicationId = @ApplicationId AND u.LoweredUserName = LOWER(@UserName)
    IF (@UserId IS NULL)
    BEGIN
        RETURN
    END

    UPDATE   dbo.aspnet_Users WITH (ROWLOCK)
    SET      LastActivityDate = @CurrentTimeUtc
    WHERE    UserId = @UserId
    IF (@@ROWCOUNT = 0) -- Username not found
        RETURN

    SELECT p.PageSettings FROM dbo.aspnet_PersonalizationPerUser p WHERE p.PathId = @PathId AND p.UserId = @UserId
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationPerUser_GetPaged]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets records from the aspnet_PersonalizationPerUser table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_PersonalizationPerUser_GetPaged]
(

	@WhereClause varchar (2000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [Id] uniqueidentifier 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([Id])'
				SET @SQL = @SQL + ' SELECT'
				IF @PageSize > 0
				BEGIN
					SET @SQL = @SQL + ' TOP ' + CONVERT(nvarchar, @PageUpperBound)
				END
				SET @SQL = @SQL + ' [Id]'
				SET @SQL = @SQL + ' FROM [dbo].[aspnet_PersonalizationPerUser]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Return paged results
				SELECT O.[Id], O.[PathId], O.[UserId], O.[PageSettings], O.[LastUpdatedDate]
				FROM
				    [dbo].[aspnet_PersonalizationPerUser] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[Id] = PageIndex.[Id]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[aspnet_PersonalizationPerUser]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationPerUser_GetByUserIdPathId]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Select records from the aspnet_PersonalizationPerUser table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_PersonalizationPerUser_GetByUserIdPathId]
(

	@UserId uniqueidentifier   ,

	@PathId uniqueidentifier   
)
AS


				SELECT
					[Id],
					[PathId],
					[UserId],
					[PageSettings],
					[LastUpdatedDate]
				FROM
					[dbo].[aspnet_PersonalizationPerUser]
				WHERE
					[UserId] = @UserId
					AND [PathId] = @PathId
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationPerUser_GetByUserId]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Select records from the aspnet_PersonalizationPerUser table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_PersonalizationPerUser_GetByUserId]
(

	@UserId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[Id],
					[PathId],
					[UserId],
					[PageSettings],
					[LastUpdatedDate]
				FROM
					[dbo].[aspnet_PersonalizationPerUser]
				WHERE
					[UserId] = @UserId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationPerUser_GetByPathIdUserId]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Select records from the aspnet_PersonalizationPerUser table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_PersonalizationPerUser_GetByPathIdUserId]
(

	@PathId uniqueidentifier   ,

	@UserId uniqueidentifier   
)
AS


				SELECT
					[Id],
					[PathId],
					[UserId],
					[PageSettings],
					[LastUpdatedDate]
				FROM
					[dbo].[aspnet_PersonalizationPerUser]
				WHERE
					[PathId] = @PathId
					AND [UserId] = @UserId
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationPerUser_GetByPathId]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Select records from the aspnet_PersonalizationPerUser table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_PersonalizationPerUser_GetByPathId]
(

	@PathId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[Id],
					[PathId],
					[UserId],
					[PageSettings],
					[LastUpdatedDate]
				FROM
					[dbo].[aspnet_PersonalizationPerUser]
				WHERE
					[PathId] = @PathId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationPerUser_GetById]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Select records from the aspnet_PersonalizationPerUser table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_PersonalizationPerUser_GetById]
(

	@Id uniqueidentifier   
)
AS


				SELECT
					[Id],
					[PathId],
					[UserId],
					[PageSettings],
					[LastUpdatedDate]
				FROM
					[dbo].[aspnet_PersonalizationPerUser]
				WHERE
					[Id] = @Id
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationPerUser_Get_List]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets all records from the aspnet_PersonalizationPerUser table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_PersonalizationPerUser_Get_List]

AS


				
				SELECT
					[Id],
					[PathId],
					[UserId],
					[PageSettings],
					[LastUpdatedDate]
				FROM
					[dbo].[aspnet_PersonalizationPerUser]
					
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationPerUser_Find]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Finds records in the aspnet_PersonalizationPerUser table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_PersonalizationPerUser_Find]
(

	@SearchUsingOR bit   = null ,

	@Id uniqueidentifier   = null ,

	@PathId uniqueidentifier   = null ,

	@UserId uniqueidentifier   = null ,

	@PageSettings image   = null ,

	@LastUpdatedDate datetime   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [Id]
	, [PathId]
	, [UserId]
	, [PageSettings]
	, [LastUpdatedDate]
    FROM
	[dbo].[aspnet_PersonalizationPerUser]
    WHERE 
	 ([Id] = @Id OR @Id IS NULL)
	AND ([PathId] = @PathId OR @PathId IS NULL)
	AND ([UserId] = @UserId OR @UserId IS NULL)
	AND ([LastUpdatedDate] = @LastUpdatedDate OR @LastUpdatedDate IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [Id]
	, [PathId]
	, [UserId]
	, [PageSettings]
	, [LastUpdatedDate]
    FROM
	[dbo].[aspnet_PersonalizationPerUser]
    WHERE 
	 ([Id] = @Id AND @Id is not null)
	OR ([PathId] = @PathId AND @PathId is not null)
	OR ([UserId] = @UserId AND @UserId is not null)
	OR ([LastUpdatedDate] = @LastUpdatedDate AND @LastUpdatedDate is not null)
	SELECT @@ROWCOUNT			
  END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationPerUser_Delete]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Deletes a record in the aspnet_PersonalizationPerUser table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_PersonalizationPerUser_Delete]
(

	@Id uniqueidentifier   
)
AS


				DELETE FROM [dbo].[aspnet_PersonalizationPerUser] WITH (ROWLOCK) 
				WHERE
					[Id] = @Id
GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationAllUsers_Update]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Updates a record in the aspnet_PersonalizationAllUsers table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_PersonalizationAllUsers_Update]
(

	@PathId uniqueidentifier   ,

	@OriginalPathId uniqueidentifier   ,

	@PageSettings image   ,

	@LastUpdatedDate datetime   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[aspnet_PersonalizationAllUsers]
				SET
					[PathId] = @PathId
					,[PageSettings] = @PageSettings
					,[LastUpdatedDate] = @LastUpdatedDate
				WHERE
[PathId] = @OriginalPathId
GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationAllUsers_SetPageSettings]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_PersonalizationAllUsers_SetPageSettings] (
    @ApplicationName  NVARCHAR(256),
    @Path             NVARCHAR(256),
    @PageSettings     IMAGE,
    @CurrentTimeUtc   DATETIME)
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    DECLARE @PathId UNIQUEIDENTIFIER

    SELECT @ApplicationId = NULL
    SELECT @PathId = NULL

    EXEC dbo.aspnet_Applications_CreateApplication @ApplicationName, @ApplicationId OUTPUT

    SELECT @PathId = u.PathId FROM dbo.aspnet_Paths u WHERE u.ApplicationId = @ApplicationId AND u.LoweredPath = LOWER(@Path)
    IF (@PathId IS NULL)
    BEGIN
        EXEC dbo.aspnet_Paths_CreatePath @ApplicationId, @Path, @PathId OUTPUT
    END

    IF (EXISTS(SELECT PathId FROM dbo.aspnet_PersonalizationAllUsers WHERE PathId = @PathId))
        UPDATE dbo.aspnet_PersonalizationAllUsers SET PageSettings = @PageSettings, LastUpdatedDate = @CurrentTimeUtc WHERE PathId = @PathId
    ELSE
        INSERT INTO dbo.aspnet_PersonalizationAllUsers(PathId, PageSettings, LastUpdatedDate) VALUES (@PathId, @PageSettings, @CurrentTimeUtc)
    RETURN 0
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationAllUsers_ResetPageSettings]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_PersonalizationAllUsers_ResetPageSettings] (
    @ApplicationName  NVARCHAR(256),
    @Path              NVARCHAR(256))
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    DECLARE @PathId UNIQUEIDENTIFIER

    SELECT @ApplicationId = NULL
    SELECT @PathId = NULL

    EXEC dbo.aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
    BEGIN
        RETURN
    END

    SELECT @PathId = u.PathId FROM dbo.aspnet_Paths u WHERE u.ApplicationId = @ApplicationId AND u.LoweredPath = LOWER(@Path)
    IF (@PathId IS NULL)
    BEGIN
        RETURN
    END

    DELETE FROM dbo.aspnet_PersonalizationAllUsers WHERE PathId = @PathId
    RETURN 0
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationAllUsers_Insert]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Inserts a record into the aspnet_PersonalizationAllUsers table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_PersonalizationAllUsers_Insert]
(

	@PathId uniqueidentifier   ,

	@PageSettings image   ,

	@LastUpdatedDate datetime   
)
AS


				
				INSERT INTO [dbo].[aspnet_PersonalizationAllUsers]
					(
					[PathId]
					,[PageSettings]
					,[LastUpdatedDate]
					)
				VALUES
					(
					@PathId
					,@PageSettings
					,@LastUpdatedDate
					)
GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationAllUsers_GetPageSettings]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_PersonalizationAllUsers_GetPageSettings] (
    @ApplicationName  NVARCHAR(256),
    @Path              NVARCHAR(256))
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    DECLARE @PathId UNIQUEIDENTIFIER

    SELECT @ApplicationId = NULL
    SELECT @PathId = NULL

    EXEC dbo.aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
    BEGIN
        RETURN
    END

    SELECT @PathId = u.PathId FROM dbo.aspnet_Paths u WHERE u.ApplicationId = @ApplicationId AND u.LoweredPath = LOWER(@Path)
    IF (@PathId IS NULL)
    BEGIN
        RETURN
    END

    SELECT p.PageSettings FROM dbo.aspnet_PersonalizationAllUsers p WHERE p.PathId = @PathId
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationAllUsers_GetPaged]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets records from the aspnet_PersonalizationAllUsers table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_PersonalizationAllUsers_GetPaged]
(

	@WhereClause varchar (2000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [PathId] uniqueidentifier 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([PathId])'
				SET @SQL = @SQL + ' SELECT'
				IF @PageSize > 0
				BEGIN
					SET @SQL = @SQL + ' TOP ' + CONVERT(nvarchar, @PageUpperBound)
				END
				SET @SQL = @SQL + ' [PathId]'
				SET @SQL = @SQL + ' FROM [dbo].[aspnet_PersonalizationAllUsers]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Return paged results
				SELECT O.[PathId], O.[PageSettings], O.[LastUpdatedDate]
				FROM
				    [dbo].[aspnet_PersonalizationAllUsers] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[PathId] = PageIndex.[PathId]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[aspnet_PersonalizationAllUsers]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationAllUsers_GetByPathId]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Select records from the aspnet_PersonalizationAllUsers table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_PersonalizationAllUsers_GetByPathId]
(

	@PathId uniqueidentifier   
)
AS


				SELECT
					[PathId],
					[PageSettings],
					[LastUpdatedDate]
				FROM
					[dbo].[aspnet_PersonalizationAllUsers]
				WHERE
					[PathId] = @PathId
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationAllUsers_Get_List]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets all records from the aspnet_PersonalizationAllUsers table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_PersonalizationAllUsers_Get_List]

AS


				
				SELECT
					[PathId],
					[PageSettings],
					[LastUpdatedDate]
				FROM
					[dbo].[aspnet_PersonalizationAllUsers]
					
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationAllUsers_Find]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Finds records in the aspnet_PersonalizationAllUsers table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_PersonalizationAllUsers_Find]
(

	@SearchUsingOR bit   = null ,

	@PathId uniqueidentifier   = null ,

	@PageSettings image   = null ,

	@LastUpdatedDate datetime   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [PathId]
	, [PageSettings]
	, [LastUpdatedDate]
    FROM
	[dbo].[aspnet_PersonalizationAllUsers]
    WHERE 
	 ([PathId] = @PathId OR @PathId IS NULL)
	AND ([LastUpdatedDate] = @LastUpdatedDate OR @LastUpdatedDate IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [PathId]
	, [PageSettings]
	, [LastUpdatedDate]
    FROM
	[dbo].[aspnet_PersonalizationAllUsers]
    WHERE 
	 ([PathId] = @PathId AND @PathId is not null)
	OR ([LastUpdatedDate] = @LastUpdatedDate AND @LastUpdatedDate is not null)
	SELECT @@ROWCOUNT			
  END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationAllUsers_Delete]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Deletes a record in the aspnet_PersonalizationAllUsers table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_PersonalizationAllUsers_Delete]
(

	@PathId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[aspnet_PersonalizationAllUsers] WITH (ROWLOCK) 
				WHERE
					[PathId] = @PathId
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Profile_Update]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Updates a record in the aspnet_Profile table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_Profile_Update]
(

	@UserId uniqueidentifier   ,

	@OriginalUserId uniqueidentifier   ,

	@PropertyNames ntext   ,

	@PropertyValuesString ntext   ,

	@PropertyValuesBinary image   ,

	@LastUpdatedDate datetime   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[aspnet_Profile]
				SET
					[UserId] = @UserId
					,[PropertyNames] = @PropertyNames
					,[PropertyValuesString] = @PropertyValuesString
					,[PropertyValuesBinary] = @PropertyValuesBinary
					,[LastUpdatedDate] = @LastUpdatedDate
				WHERE
[UserId] = @OriginalUserId
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Profile_SetProperties]    Script Date: 10/11/2011 11:07:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Profile_SetProperties]
    @ApplicationName        nvarchar(256),
    @PropertyNames          ntext,
    @PropertyValuesString   ntext,
    @PropertyValuesBinary   image,
    @UserName               nvarchar(256),
    @IsUserAnonymous        bit,
    @CurrentTimeUtc         datetime
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL

    DECLARE @ErrorCode     int
    SET @ErrorCode = 0

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
       BEGIN TRANSACTION
       SET @TranStarted = 1
    END
    ELSE
    	SET @TranStarted = 0

    EXEC dbo.aspnet_Applications_CreateApplication @ApplicationName, @ApplicationId OUTPUT

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    DECLARE @UserId uniqueidentifier
    DECLARE @LastActivityDate datetime
    SELECT  @UserId = NULL
    SELECT  @LastActivityDate = @CurrentTimeUtc

    SELECT @UserId = UserId
    FROM   dbo.aspnet_Users
    WHERE  ApplicationId = @ApplicationId AND LoweredUserName = LOWER(@UserName)
    IF (@UserId IS NULL)
        EXEC dbo.aspnet_Users_CreateUser @ApplicationId, @UserName, @IsUserAnonymous, @LastActivityDate, @UserId OUTPUT

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    UPDATE dbo.aspnet_Users
    SET    LastActivityDate=@CurrentTimeUtc
    WHERE  UserId = @UserId

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF (EXISTS( SELECT *
               FROM   dbo.aspnet_Profile
               WHERE  UserId = @UserId))
        UPDATE dbo.aspnet_Profile
        SET    PropertyNames=@PropertyNames, PropertyValuesString = @PropertyValuesString,
               PropertyValuesBinary = @PropertyValuesBinary, LastUpdatedDate=@CurrentTimeUtc
        WHERE  UserId = @UserId
    ELSE
        INSERT INTO dbo.aspnet_Profile(UserId, PropertyNames, PropertyValuesString, PropertyValuesBinary, LastUpdatedDate)
             VALUES (@UserId, @PropertyNames, @PropertyValuesString, @PropertyValuesBinary, @CurrentTimeUtc)

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF( @TranStarted = 1 )
    BEGIN
    	SET @TranStarted = 0
    	COMMIT TRANSACTION
    END

    RETURN 0

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
    	ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode

END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Profile_Insert]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Inserts a record into the aspnet_Profile table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_Profile_Insert]
(

	@UserId uniqueidentifier   ,

	@PropertyNames ntext   ,

	@PropertyValuesString ntext   ,

	@PropertyValuesBinary image   ,

	@LastUpdatedDate datetime   
)
AS


				
				INSERT INTO [dbo].[aspnet_Profile]
					(
					[UserId]
					,[PropertyNames]
					,[PropertyValuesString]
					,[PropertyValuesBinary]
					,[LastUpdatedDate]
					)
				VALUES
					(
					@UserId
					,@PropertyNames
					,@PropertyValuesString
					,@PropertyValuesBinary
					,@LastUpdatedDate
					)
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Profile_GetProperties]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Profile_GetProperties]
    @ApplicationName      nvarchar(256),
    @UserName             nvarchar(256),
    @CurrentTimeUtc       datetime
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM dbo.aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN

    DECLARE @UserId uniqueidentifier
    SELECT  @UserId = NULL

    SELECT @UserId = UserId
    FROM   dbo.aspnet_Users
    WHERE  ApplicationId = @ApplicationId AND LoweredUserName = LOWER(@UserName)

    IF (@UserId IS NULL)
        RETURN
    SELECT TOP 1 PropertyNames, PropertyValuesString, PropertyValuesBinary
    FROM         dbo.aspnet_Profile
    WHERE        UserId = @UserId

    IF (@@ROWCOUNT > 0)
    BEGIN
        UPDATE dbo.aspnet_Users
        SET    LastActivityDate=@CurrentTimeUtc
        WHERE  UserId = @UserId
    END
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Profile_GetProfiles]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Profile_GetProfiles]
    @ApplicationName        nvarchar(256),
    @ProfileAuthOptions     int,
    @PageIndex              int,
    @PageSize               int,
    @UserNameToMatch        nvarchar(256) = NULL,
    @InactiveSinceDate      datetime      = NULL
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN

    -- Set the page bounds
    DECLARE @PageLowerBound int
    DECLARE @PageUpperBound int
    DECLARE @TotalRecords   int
    SET @PageLowerBound = @PageSize * @PageIndex
    SET @PageUpperBound = @PageSize - 1 + @PageLowerBound

    -- Create a temp table TO store the select results
    CREATE TABLE #PageIndexForUsers
    (
        IndexId int IDENTITY (0, 1) NOT NULL,
        UserId uniqueidentifier
    )

    -- Insert into our temp table
    INSERT INTO #PageIndexForUsers (UserId)
        SELECT  u.UserId
        FROM    dbo.aspnet_Users u, dbo.aspnet_Profile p
        WHERE   ApplicationId = @ApplicationId
            AND u.UserId = p.UserId
            AND (@InactiveSinceDate IS NULL OR LastActivityDate <= @InactiveSinceDate)
            AND (     (@ProfileAuthOptions = 2)
                   OR (@ProfileAuthOptions = 0 AND IsAnonymous = 1)
                   OR (@ProfileAuthOptions = 1 AND IsAnonymous = 0)
                 )
            AND (@UserNameToMatch IS NULL OR LoweredUserName LIKE LOWER(@UserNameToMatch))
        ORDER BY UserName

    SELECT  u.UserName, u.IsAnonymous, u.LastActivityDate, p.LastUpdatedDate,
            DATALENGTH(p.PropertyNames) + DATALENGTH(p.PropertyValuesString) + DATALENGTH(p.PropertyValuesBinary)
    FROM    dbo.aspnet_Users u, dbo.aspnet_Profile p, #PageIndexForUsers i
    WHERE   u.UserId = p.UserId AND p.UserId = i.UserId AND i.IndexId >= @PageLowerBound AND i.IndexId <= @PageUpperBound

    SELECT COUNT(*)
    FROM   #PageIndexForUsers

    DROP TABLE #PageIndexForUsers
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Profile_GetPaged]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets records from the aspnet_Profile table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_Profile_GetPaged]
(

	@WhereClause varchar (2000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [UserId] uniqueidentifier 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([UserId])'
				SET @SQL = @SQL + ' SELECT'
				IF @PageSize > 0
				BEGIN
					SET @SQL = @SQL + ' TOP ' + CONVERT(nvarchar, @PageUpperBound)
				END
				SET @SQL = @SQL + ' [UserId]'
				SET @SQL = @SQL + ' FROM [dbo].[aspnet_Profile]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Return paged results
				SELECT O.[UserId], O.[PropertyNames], O.[PropertyValuesString], O.[PropertyValuesBinary], O.[LastUpdatedDate]
				FROM
				    [dbo].[aspnet_Profile] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[UserId] = PageIndex.[UserId]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[aspnet_Profile]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Profile_GetNumberOfInactiveProfiles]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Profile_GetNumberOfInactiveProfiles]
    @ApplicationName        nvarchar(256),
    @ProfileAuthOptions     int,
    @InactiveSinceDate      datetime
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
    BEGIN
        SELECT 0
        RETURN
    END

    SELECT  COUNT(*)
    FROM    dbo.aspnet_Users u, dbo.aspnet_Profile p
    WHERE   ApplicationId = @ApplicationId
        AND u.UserId = p.UserId
        AND (LastActivityDate <= @InactiveSinceDate)
        AND (
                (@ProfileAuthOptions = 2)
                OR (@ProfileAuthOptions = 0 AND IsAnonymous = 1)
                OR (@ProfileAuthOptions = 1 AND IsAnonymous = 0)
            )
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Profile_GetByUserId]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Select records from the aspnet_Profile table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_Profile_GetByUserId]
(

	@UserId uniqueidentifier   
)
AS


				SELECT
					[UserId],
					[PropertyNames],
					[PropertyValuesString],
					[PropertyValuesBinary],
					[LastUpdatedDate]
				FROM
					[dbo].[aspnet_Profile]
				WHERE
					[UserId] = @UserId
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Profile_Get_List]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets all records from the aspnet_Profile table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_Profile_Get_List]

AS


				
				SELECT
					[UserId],
					[PropertyNames],
					[PropertyValuesString],
					[PropertyValuesBinary],
					[LastUpdatedDate]
				FROM
					[dbo].[aspnet_Profile]
					
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Profile_Find]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Finds records in the aspnet_Profile table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[aspnet_Profile_Find]
(

	@SearchUsingOR bit   = null ,

	@UserId uniqueidentifier   = null ,

	@PropertyNames ntext   = null ,

	@PropertyValuesString ntext   = null ,

	@PropertyValuesBinary image   = null ,

	@LastUpdatedDate datetime   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [UserId]
	, [PropertyNames]
	, [PropertyValuesString]
	, [PropertyValuesBinary]
	, [LastUpdatedDate]
    FROM
	[dbo].[aspnet_Profile]
    WHERE 
	 ([UserId] = @UserId OR @UserId IS NULL)
	AND ([LastUpdatedDate] = @LastUpdatedDate OR @LastUpdatedDate IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [UserId]
	, [PropertyNames]
	, [PropertyValuesString]
	, [PropertyValuesBinary]
	, [LastUpdatedDate]
    FROM
	[dbo].[aspnet_Profile]
    WHERE 
	 ([UserId] = @UserId AND @UserId is not null)
	OR ([LastUpdatedDate] = @LastUpdatedDate AND @LastUpdatedDate is not null)
	SELECT @@ROWCOUNT			
  END
GO
/****** Object:  View [dbo].[vw_aspnet_Profiles]    Script Date: 10/11/2011 11:07:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE VIEW [dbo].[vw_aspnet_Profiles]
  AS SELECT [dbo].[aspnet_Profile].[UserId], [dbo].[aspnet_Profile].[LastUpdatedDate],
      [DataSize]=  DATALENGTH([dbo].[aspnet_Profile].[PropertyNames])
                 + DATALENGTH([dbo].[aspnet_Profile].[PropertyValuesString])
                 + DATALENGTH([dbo].[aspnet_Profile].[PropertyValuesBinary])
  FROM [dbo].[aspnet_Profile]
GO
/****** Object:  View [dbo].[vw_aspnet_UsersInRoles]    Script Date: 10/11/2011 11:07:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE VIEW [dbo].[vw_aspnet_UsersInRoles]
  AS SELECT [dbo].[aspnet_UsersInRoles].[UserId], [dbo].[aspnet_UsersInRoles].[RoleId]
  FROM [dbo].[aspnet_UsersInRoles]
GO
/****** Object:  StoredProcedure [dbo].[vw_aspnet_Users_Get_List]    Script Date: 10/11/2011 11:07:42 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets all records from the vw_aspnet_Users view
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[vw_aspnet_Users_Get_List]

AS


				
				SELECT
					[ApplicationId],
					[UserId],
					[UserName],
					[LoweredUserName],
					[MobileAlias],
					[IsAnonymous],
					[LastActivityDate]
				FROM
					[dbo].[vw_aspnet_Users]
					
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[vw_aspnet_Roles_Get_List]    Script Date: 10/11/2011 11:07:42 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets all records from the vw_aspnet_Roles view
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[vw_aspnet_Roles_Get_List]

AS


				
				SELECT
					[ApplicationId],
					[RoleId],
					[RoleName],
					[LoweredRoleName],
					[Description]
				FROM
					[dbo].[vw_aspnet_Roles]
					
				SELECT @@ROWCOUNT
GO
/****** Object:  View [dbo].[vw_aspnet_WebPartState_User]    Script Date: 10/11/2011 11:07:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE VIEW [dbo].[vw_aspnet_WebPartState_User]
  AS SELECT [dbo].[aspnet_PersonalizationPerUser].[PathId], [dbo].[aspnet_PersonalizationPerUser].[UserId], [DataSize]=DATALENGTH([dbo].[aspnet_PersonalizationPerUser].[PageSettings]), [dbo].[aspnet_PersonalizationPerUser].[LastUpdatedDate]
  FROM [dbo].[aspnet_PersonalizationPerUser]
GO
/****** Object:  View [dbo].[vw_aspnet_WebPartState_Shared]    Script Date: 10/11/2011 11:07:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE VIEW [dbo].[vw_aspnet_WebPartState_Shared]
  AS SELECT [dbo].[aspnet_PersonalizationAllUsers].[PathId], [DataSize]=DATALENGTH([dbo].[aspnet_PersonalizationAllUsers].[PageSettings]), [dbo].[aspnet_PersonalizationAllUsers].[LastUpdatedDate]
  FROM [dbo].[aspnet_PersonalizationAllUsers]
GO
/****** Object:  View [dbo].[vw_aspnet_MembershipUsers]    Script Date: 10/11/2011 11:07:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE VIEW [dbo].[vw_aspnet_MembershipUsers]
  AS SELECT [dbo].[aspnet_Membership].[UserId],
            [dbo].[aspnet_Membership].[PasswordFormat],
            [dbo].[aspnet_Membership].[MobilePIN],
            [dbo].[aspnet_Membership].[Email],
            [dbo].[aspnet_Membership].[LoweredEmail],
            [dbo].[aspnet_Membership].[PasswordQuestion],
            [dbo].[aspnet_Membership].[PasswordAnswer],
            [dbo].[aspnet_Membership].[IsApproved],
            [dbo].[aspnet_Membership].[IsLockedOut],
            [dbo].[aspnet_Membership].[CreateDate],
            [dbo].[aspnet_Membership].[LastLoginDate],
            [dbo].[aspnet_Membership].[LastPasswordChangedDate],
            [dbo].[aspnet_Membership].[LastLockoutDate],
            [dbo].[aspnet_Membership].[FailedPasswordAttemptCount],
            [dbo].[aspnet_Membership].[FailedPasswordAttemptWindowStart],
            [dbo].[aspnet_Membership].[FailedPasswordAnswerAttemptCount],
            [dbo].[aspnet_Membership].[FailedPasswordAnswerAttemptWindowStart],
            [dbo].[aspnet_Membership].[Comment],
            [dbo].[aspnet_Users].[ApplicationId],
            [dbo].[aspnet_Users].[UserName],
            [dbo].[aspnet_Users].[MobileAlias],
            [dbo].[aspnet_Users].[IsAnonymous],
            [dbo].[aspnet_Users].[LastActivityDate]
  FROM [dbo].[aspnet_Membership] INNER JOIN [dbo].[aspnet_Users]
      ON [dbo].[aspnet_Membership].[UserId] = [dbo].[aspnet_Users].[UserId]
GO
/****** Object:  StoredProcedure [dbo].[vw_aspnet_UsersInRoles_Get_List]    Script Date: 10/11/2011 11:07:42 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets all records from the vw_aspnet_UsersInRoles view
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[vw_aspnet_UsersInRoles_Get_List]

AS


				
				SELECT
					[UserId],
					[RoleId]
				FROM
					[dbo].[vw_aspnet_UsersInRoles]
					
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[vw_aspnet_Profiles_Get_List]    Script Date: 10/11/2011 11:07:42 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets all records from the vw_aspnet_Profiles view
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[vw_aspnet_Profiles_Get_List]

AS


				
				SELECT
					[UserId],
					[LastUpdatedDate],
					[DataSize]
				FROM
					[dbo].[vw_aspnet_Profiles]
					
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[vw_aspnet_MembershipUsers_Get_List]    Script Date: 10/11/2011 11:07:42 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
----------------------------------------------------------------------------------------------------

-- Created By: Managed Information Solutions, LLC (www.meanstream.net)
-- Purpose: Gets all records from the vw_aspnet_MembershipUsers view
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[vw_aspnet_MembershipUsers_Get_List]

AS


				
				SELECT
					[UserId],
					[PasswordFormat],
					[MobilePIN],
					[Email],
					[LoweredEmail],
					[PasswordQuestion],
					[PasswordAnswer],
					[IsApproved],
					[IsLockedOut],
					[CreateDate],
					[LastLoginDate],
					[LastPasswordChangedDate],
					[LastLockoutDate],
					[FailedPasswordAttemptCount],
					[FailedPasswordAttemptWindowStart],
					[FailedPasswordAnswerAttemptCount],
					[FailedPasswordAnswerAttemptWindowStart],
					[Comment],
					[ApplicationId],
					[UserName],
					[MobileAlias],
					[IsAnonymous],
					[LastActivityDate]
				FROM
					[dbo].[vw_aspnet_MembershipUsers]
					
				SELECT @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Profile_DeleteProfiles]    Script Date: 10/11/2011 11:07:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Profile_DeleteProfiles]
    @ApplicationName        nvarchar(256),
    @UserNames              nvarchar(4000)
AS
BEGIN
    DECLARE @UserName     nvarchar(256)
    DECLARE @CurrentPos   int
    DECLARE @NextPos      int
    DECLARE @NumDeleted   int
    DECLARE @DeletedUser  int
    DECLARE @TranStarted  bit
    DECLARE @ErrorCode    int

    SET @ErrorCode = 0
    SET @CurrentPos = 1
    SET @NumDeleted = 0
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
        BEGIN TRANSACTION
        SET @TranStarted = 1
    END
    ELSE
    	SET @TranStarted = 0

    WHILE (@CurrentPos <= LEN(@UserNames))
    BEGIN
        SELECT @NextPos = CHARINDEX(N',', @UserNames,  @CurrentPos)
        IF (@NextPos = 0 OR @NextPos IS NULL)
            SELECT @NextPos = LEN(@UserNames) + 1

        SELECT @UserName = SUBSTRING(@UserNames, @CurrentPos, @NextPos - @CurrentPos)
        SELECT @CurrentPos = @NextPos+1

        IF (LEN(@UserName) > 0)
        BEGIN
            SELECT @DeletedUser = 0
            EXEC dbo.aspnet_Users_DeleteUser @ApplicationName, @UserName, 4, @DeletedUser OUTPUT
            IF( @@ERROR <> 0 )
            BEGIN
                SET @ErrorCode = -1
                GOTO Cleanup
            END
            IF (@DeletedUser <> 0)
                SELECT @NumDeleted = @NumDeleted + 1
        END
    END
    SELECT @NumDeleted
    IF (@TranStarted = 1)
    BEGIN
    	SET @TranStarted = 0
    	COMMIT TRANSACTION
    END
    SET @TranStarted = 0

    RETURN 0

Cleanup:
    IF (@TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
    	ROLLBACK TRANSACTION
    END
    RETURN @ErrorCode
END
GO
/****** Object:  Default [DF__aspnet_Ap__Appli__4FD1D5C8]    Script Date: 10/11/2011 11:07:35 ******/
ALTER TABLE [dbo].[aspnet_Applications] ADD  DEFAULT (newid()) FOR [ApplicationId]
GO
/****** Object:  Default [DF__aspnet_Me__Passw__567ED357]    Script Date: 10/11/2011 11:07:35 ******/
ALTER TABLE [dbo].[aspnet_Membership] ADD  DEFAULT ((0)) FOR [PasswordFormat]
GO
/****** Object:  Default [DF__aspnet_Pa__PathI__50C5FA01]    Script Date: 10/11/2011 11:07:35 ******/
ALTER TABLE [dbo].[aspnet_Paths] ADD  DEFAULT (newid()) FOR [PathId]
GO
/****** Object:  Default [DF__aspnet_Perso__Id__558AAF1E]    Script Date: 10/11/2011 11:07:35 ******/
ALTER TABLE [dbo].[aspnet_PersonalizationPerUser] ADD  DEFAULT (newid()) FOR [Id]
GO
/****** Object:  Default [DF__aspnet_Ro__RoleI__54968AE5]    Script Date: 10/11/2011 11:07:35 ******/
ALTER TABLE [dbo].[aspnet_Roles] ADD  DEFAULT (newid()) FOR [RoleId]
GO
/****** Object:  Default [DF__aspnet_Us__UserI__51BA1E3A]    Script Date: 10/11/2011 11:07:35 ******/
ALTER TABLE [dbo].[aspnet_Users] ADD  DEFAULT (newid()) FOR [UserId]
GO
/****** Object:  Default [DF__aspnet_Us__Mobil__52AE4273]    Script Date: 10/11/2011 11:07:35 ******/
ALTER TABLE [dbo].[aspnet_Users] ADD  DEFAULT (NULL) FOR [MobileAlias]
GO
/****** Object:  Default [DF__aspnet_Us__IsAno__53A266AC]    Script Date: 10/11/2011 11:07:35 ******/
ALTER TABLE [dbo].[aspnet_Users] ADD  DEFAULT ((0)) FOR [IsAnonymous]
GO
/****** Object:  ForeignKey [FK__aspnet_Me__Appli__5D2BD0E6]    Script Date: 10/11/2011 11:07:35 ******/
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Object:  ForeignKey [FK__aspnet_Me__UserI__5E1FF51F]    Script Date: 10/11/2011 11:07:35 ******/
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[aspnet_Users] ([UserId])
GO
/****** Object:  ForeignKey [FK__aspnet_Pa__Appli__5772F790]    Script Date: 10/11/2011 11:07:35 ******/
ALTER TABLE [dbo].[aspnet_Paths]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Object:  ForeignKey [FK__aspnet_Pe__PathI__5F141958]    Script Date: 10/11/2011 11:07:35 ******/
ALTER TABLE [dbo].[aspnet_PersonalizationAllUsers]  WITH CHECK ADD FOREIGN KEY([PathId])
REFERENCES [dbo].[aspnet_Paths] ([PathId])
GO
/****** Object:  ForeignKey [FK__aspnet_Pe__PathI__5A4F643B]    Script Date: 10/11/2011 11:07:35 ******/
ALTER TABLE [dbo].[aspnet_PersonalizationPerUser]  WITH CHECK ADD FOREIGN KEY([PathId])
REFERENCES [dbo].[aspnet_Paths] ([PathId])
GO
/****** Object:  ForeignKey [FK__aspnet_Pe__UserI__5B438874]    Script Date: 10/11/2011 11:07:35 ******/
ALTER TABLE [dbo].[aspnet_PersonalizationPerUser]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[aspnet_Users] ([UserId])
GO
/****** Object:  ForeignKey [FK__aspnet_Pr__UserI__5C37ACAD]    Script Date: 10/11/2011 11:07:35 ******/
ALTER TABLE [dbo].[aspnet_Profile]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[aspnet_Users] ([UserId])
GO
/****** Object:  ForeignKey [FK__aspnet_Ro__Appli__595B4002]    Script Date: 10/11/2011 11:07:35 ******/
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Object:  ForeignKey [FK__aspnet_Us__Appli__58671BC9]    Script Date: 10/11/2011 11:07:35 ******/
ALTER TABLE [dbo].[aspnet_Users]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Object:  ForeignKey [FK__aspnet_Us__RoleI__60083D91]    Script Date: 10/11/2011 11:07:35 ******/
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Object:  ForeignKey [FK__aspnet_Us__UserI__60FC61CA]    Script Date: 10/11/2011 11:07:35 ******/
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[aspnet_Users] ([UserId])
GO
