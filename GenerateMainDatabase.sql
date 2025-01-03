USE [master]
GO
/****** Object:  Database [PhoenixDB]    Script Date: 12/1/2024 8:06:44 PM ******/
CREATE DATABASE [PhoenixDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PhoenixDB', FILENAME = N'/var/opt/mssql/data/PhoenixDB.mdf' , SIZE = 102400KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'PhoenixDB_log', FILENAME = N'/var/opt/mssql/data/PhoenixDB_log.ldf' , SIZE = 181184KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [PhoenixDB] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PhoenixDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PhoenixDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PhoenixDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PhoenixDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PhoenixDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PhoenixDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [PhoenixDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PhoenixDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PhoenixDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PhoenixDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PhoenixDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PhoenixDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PhoenixDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PhoenixDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PhoenixDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PhoenixDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PhoenixDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PhoenixDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PhoenixDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PhoenixDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PhoenixDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PhoenixDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PhoenixDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PhoenixDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [PhoenixDB] SET  MULTI_USER 
GO
ALTER DATABASE [PhoenixDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PhoenixDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PhoenixDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PhoenixDB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [PhoenixDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PhoenixDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'PhoenixDB', N'ON'
GO
ALTER DATABASE [PhoenixDB] SET QUERY_STORE = OFF
GO
USE [PhoenixDB]
GO
/****** Object:  Schema [cfg]    Script Date: 12/1/2024 8:06:45 PM ******/
CREATE SCHEMA [cfg]
GO
/****** Object:  Schema [elc]    Script Date: 12/1/2024 8:06:45 PM ******/
CREATE SCHEMA [elc]
GO
/****** Object:  Schema [hub]    Script Date: 12/1/2024 8:06:45 PM ******/
CREATE SCHEMA [hub]
GO
/****** Object:  Schema [oper]    Script Date: 12/1/2024 8:06:45 PM ******/
CREATE SCHEMA [oper]
GO
/****** Object:  Schema [sd]    Script Date: 12/1/2024 8:06:45 PM ******/
CREATE SCHEMA [sd]
GO
/****** Object:  Schema [ter]    Script Date: 12/1/2024 8:06:45 PM ******/
CREATE SCHEMA [ter]
GO
/****** Object:  UserDefinedTableType [dbo].[TVP_Person]    Script Date: 12/1/2024 8:06:45 PM ******/
CREATE TYPE [dbo].[TVP_Person] AS TABLE(
	[ID] [uniqueidentifier] NULL,
	[LNAME] [nvarchar](200) NOT NULL,
	[FNAME] [nvarchar](100) NOT NULL,
	[MNAME] [nvarchar](200) NULL,
	[GENDER] [bit] NULL,
	[BDATE] [date] NULL,
	[VOTE] [bit] NOT NULL,
	[ADDR_REG] [uniqueidentifier] NULL,
	[ADDR_REG_ROOM] [nvarchar](50) NULL,
	[ADDR_HOME] [uniqueidentifier] NULL,
	[ADDR_HOME_ROOM] [nvarchar](50) NULL,
	[PHONE] [nvarchar](30) NULL,
	[HAS_TELEGRAM] [bit] NOT NULL,
	[HAS_VIBER] [bit] NOT NULL,
	[HAS_WHATSAPP] [bit] NOT NULL,
	[EMAIL] [nvarchar](256) NULL,
	[HAS_FACEBOOK] [bit] NOT NULL,
	[HAS_INSTAGRAM] [bit] NOT NULL,
	[IS_EMPLOYEE] [bit] NOT NULL,
	[IS_DEPUTY] [bit] NOT NULL,
	[IS_PARTY_MEMBER] [bit] NOT NULL,
	[IS_DELETED] [bit] NOT NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[TVP_PersonFilter]    Script Date: 12/1/2024 8:06:45 PM ******/
CREATE TYPE [dbo].[TVP_PersonFilter] AS TABLE(
	[AREA_ID] [uniqueidentifier] NULL,
	[REG_ID] [uniqueidentifier] NULL,
	[CMN_ID] [uniqueidentifier] NULL,
	[HAS_TELEGRAM] [bit] NULL,
	[HAS_VIBER] [bit] NULL,
	[HAS_WHATSAPP] [bit] NULL,
	[HAS_FACEBOOK] [bit] NULL,
	[HAS_INSTAGRAM] [bit] NULL,
	[IS_EMPLOYEE] [bit] NULL,
	[POS_ID] [uniqueidentifier] NULL,
	[IS_DEPUTY] [bit] NULL,
	[IS_PARTYMEMBER] [bit] NULL,
	[IS_DELETED] [bit] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[TVP_PersonInfo]    Script Date: 12/1/2024 8:06:45 PM ******/
CREATE TYPE [dbo].[TVP_PersonInfo] AS TABLE(
	[PSN_ID] [uniqueidentifier] NOT NULL,
	[PASS_SERIES] [nvarchar](4) NULL,
	[PASS_NUMBER] [nvarchar](10) NULL,
	[PASS_ISSUE] [nvarchar](100) NULL,
	[TAX_NUMBER] [nvarchar](20) NULL
)
GO
/****** Object:  UserDefinedTableType [elc].[TVP_ElectionPrecinct]    Script Date: 12/1/2024 8:06:45 PM ******/
CREATE TYPE [elc].[TVP_ElectionPrecinct] AS TABLE(
	[ELC_PRCT_ID] [uniqueidentifier] NULL,
	[PRCT_OPENED] [bit] NULL,
	[PRCT_NOTOPENED_CAUSE] [nvarchar](300) NULL,
	[PRCT_VOUTERS] [int] NULL
)
GO
/****** Object:  UserDefinedTableType [elc].[TVP_ElectionPrecinctInfo]    Script Date: 12/1/2024 8:06:45 PM ******/
CREATE TYPE [elc].[TVP_ElectionPrecinctInfo] AS TABLE(
	[ELC_PRCT_INFO_ID] [uniqueidentifier] NULL,
	[BLT_RECV] [int] NULL
)
GO
/****** Object:  UserDefinedTableType [elc].[TVP_ProtocolItemValue]    Script Date: 12/1/2024 8:06:45 PM ******/
CREATE TYPE [elc].[TVP_ProtocolItemValue] AS TABLE(
	[ID] [uniqueidentifier] NOT NULL,
	[VOTES] [bigint] NULL
)
GO
/****** Object:  UserDefinedTableType [elc].[TVP_Turnout]    Script Date: 12/1/2024 8:06:45 PM ******/
CREATE TYPE [elc].[TVP_Turnout] AS TABLE(
	[TURNOUT_ID] [uniqueidentifier] NOT NULL,
	[TURNOUT_VOTERS] [int] NULL
)
GO
/****** Object:  UserDefinedFunction [dbo].[GetUserDisplayName]    Script Date: 12/1/2024 8:06:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		ghost1908
-- Create date: 22.06.2021
-- Description:	Получить имя пользователя
-- =============================================
CREATE FUNCTION [dbo].[GetUserDisplayName]
(
	@userId uniqueidentifier
)
RETURNS nvarchar(MAX)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @result nvarchar(MAX)

	SELECT top 1
		@result = CASE WHEN claims.ClaimType = N'DisplayName' THEN claims.ClaimValue ELSE users.UserName END
	FROM PhoenixIdentity.dbo.NetCoreUsers users
	LEFT JOIN PhoenixIdentity.dbo.NetCoreClaims claims
		ON claims.UserId = users.Id
	WHERE users.Id = @userId

	RETURN @result

END
GO
/****** Object:  Table [cfg].[ObjectAccess]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [cfg].[ObjectAccess](
	[Id] [uniqueidentifier] NOT NULL,
	[ObjectName] [nvarchar](max) NOT NULL,
	[ObjectDisplayName] [nvarchar](max) NULL,
	[ObjectType] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ObjectAccess] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [cfg].[UserAccess]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [cfg].[UserAccess](
	[UserId] [uniqueidentifier] NOT NULL,
	[ObjectId] [uniqueidentifier] NOT NULL,
	[ObjectValue] [sql_variant] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Address]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Address](
	[ID] [uniqueidentifier] NOT NULL,
	[SIC_ID] [uniqueidentifier] NOT NULL,
	[BLD_ID] [uniqueidentifier] NOT NULL,
	[BLD_ISSUE] [nvarchar](100) NULL,
 CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AddressInPrecinct]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AddressInPrecinct](
	[ADDR_ID] [uniqueidentifier] NOT NULL,
	[PRCT_ID] [uniqueidentifier] NOT NULL,
	[DCLOSE] [date] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Area]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Area](
	[ID] [uniqueidentifier] NOT NULL,
	[NAME] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Area] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Building]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Building](
	[ID] [uniqueidentifier] NOT NULL,
	[NUMBER] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_Building] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[City]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[City](
	[ID] [uniqueidentifier] NOT NULL,
	[CITYT_ID] [uniqueidentifier] NOT NULL,
	[NAME] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_City] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CityInRegion]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CityInRegion](
	[ID] [uniqueidentifier] NOT NULL,
	[REG_ID] [uniqueidentifier] NOT NULL,
	[CITY_ID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_CityInRegion] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CityType]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CityType](
	[ID] [uniqueidentifier] NOT NULL,
	[NAME] [nvarchar](5) NOT NULL,
 CONSTRAINT [PK_CityType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomData]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomData](
	[ID] [uniqueidentifier] NOT NULL,
	[ObjectType] [nvarchar](max) NOT NULL,
	[ObjectID] [uniqueidentifier] NOT NULL,
	[DataType] [nvarchar](max) NULL,
	[DataValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_CustomData] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[District]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[District](
	[ID] [uniqueidentifier] NOT NULL,
	[DSTRT_ID] [uniqueidentifier] NOT NULL,
	[NUMBER] [nvarchar](6) NOT NULL,
 CONSTRAINT [PK_District] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DistrictType]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DistrictType](
	[ID] [uniqueidentifier] NOT NULL,
	[NAME] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_DistrictType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Organization]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Organization](
	[ID] [uniqueidentifier] NOT NULL,
	[LEVEL] [hierarchyid] NOT NULL,
	[NAME] [nvarchar](256) NOT NULL,
	[ORG_TYPE_ID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Organization] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrganizationStructure]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrganizationStructure](
	[ID] [uniqueidentifier] NOT NULL,
	[ORG_ID] [uniqueidentifier] NOT NULL,
	[PARENT_ID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_OrganizationStructure] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrganizationType]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrganizationType](
	[ID] [uniqueidentifier] NOT NULL,
	[NAME] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_OrganizationType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Person]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Person](
	[ID] [uniqueidentifier] NOT NULL,
	[LNAME] [nvarchar](200) NOT NULL,
	[FNAME] [nvarchar](100) NOT NULL,
	[MNAME] [nvarchar](200) NULL,
	[GENDER] [bit] NULL,
	[BDATE] [date] NULL,
	[VOTE] [bit] NOT NULL,
	[ADDR_REG] [uniqueidentifier] NULL,
	[ADDR_REG_ROOM] [nvarchar](50) NULL,
	[ADDR_HOME] [uniqueidentifier] NULL,
	[ADDR_HOME_ROOM] [nvarchar](50) NULL,
	[PHONE] [nvarchar](30) NULL,
	[HAS_TELEGRAM] [bit] NOT NULL,
	[HAS_VIBER] [bit] NOT NULL,
	[HAS_WHATSAPP] [bit] NOT NULL,
	[EMAIL] [nvarchar](256) NULL,
	[HAS_FACEBOOK] [bit] NOT NULL,
	[HAS_INSTAGRAM] [bit] NOT NULL,
	[IS_EMPLOYEE] [bit] NOT NULL,
	[IS_DEPUTY] [bit] NOT NULL,
	[IS_PARTY_MEMBER] [bit] NOT NULL,
	[IS_DELETED] [bit] NOT NULL,
	[USER_ID] [uniqueidentifier] NULL,
	[DCREATE] [datetime2](3) NULL,
 CONSTRAINT [PK_People] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PersonEvent]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PersonEvent](
	[ID] [uniqueidentifier] NOT NULL,
	[PRE_ID] [uniqueidentifier] NOT NULL,
	[PSN_ID] [uniqueidentifier] NOT NULL,
	[REG_DATE] [datetime2](3) NOT NULL,
	[PSN_STATUS] [tinyint] NOT NULL,
	[EVT_COMMENT] [nvarchar](100) NULL,
	[USER_ID] [uniqueidentifier] NULL,
	[EVT_CREATE] [datetime2](3) NULL,
 CONSTRAINT [PK_PersonEvent] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PersonFormStatus]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PersonFormStatus](
	[ID] [uniqueidentifier] NOT NULL,
	[PSN_ID] [uniqueidentifier] NOT NULL,
	[STATUS] [tinyint] NOT NULL,
	[USER_ID] [uniqueidentifier] NOT NULL,
	[CREATE_DATE] [datetime2](3) NOT NULL,
	[COMMENT] [nvarchar](200) NULL,
 CONSTRAINT [PK_PersonFormStatus] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PersonInfo]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PersonInfo](
	[PSN_ID] [uniqueidentifier] NOT NULL,
	[PASS_SERIES] [nvarchar](4) NULL,
	[PASS_NUMBER] [nvarchar](10) NULL,
	[PASS_ISSUE] [nvarchar](100) NULL,
	[TAX_NUMBER] [nvarchar](20) NULL,
 CONSTRAINT [PK_PersonInfo] PRIMARY KEY CLUSTERED 
(
	[PSN_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PersonParty]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PersonParty](
	[ID] [uniqueidentifier] NOT NULL,
	[PSN_ID] [uniqueidentifier] NOT NULL,
	[DATE_ENTRY] [date] NOT NULL,
	[DATE_ADOPTION] [date] NULL,
	[DATE_DISPOSAL] [date] NULL,
	[TICKET_NUMBER] [nvarchar](10) NULL,
 CONSTRAINT [PK_PersonParty] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Position]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Position](
	[ID] [uniqueidentifier] NOT NULL,
	[POS_NAME] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Position] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PositionInOrganization]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PositionInOrganization](
	[ID] [uniqueidentifier] NOT NULL,
	[ORG_ID] [uniqueidentifier] NOT NULL,
	[PSN_ID] [uniqueidentifier] NOT NULL,
	[POS_ID] [uniqueidentifier] NOT NULL,
	[APPOINT_DATE] [date] NULL,
	[DISMISS_DATE] [date] NULL,
 CONSTRAINT [PK_PositionInOrganization] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Precinct]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Precinct](
	[ID] [uniqueidentifier] NOT NULL,
	[NUMBER] [nvarchar](6) NOT NULL,
	[DCLOSE] [datetime] NULL,
 CONSTRAINT [PK_Precinct] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PrecinctInDistrict]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PrecinctInDistrict](
	[DSTR_ID] [uniqueidentifier] NOT NULL,
	[PRCT_ID] [uniqueidentifier] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Project]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Project](
	[ID] [uniqueidentifier] NOT NULL,
	[PRJT_NAME] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_Project] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectEvent]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectEvent](
	[ID] [uniqueidentifier] NOT NULL,
	[EVT_NAME] [nvarchar](256) NOT NULL,
	[EVT_TYPE] [bit] NOT NULL,
	[EVT_ACCESS] [tinyint] NOT NULL,
	[PRJT_ID] [uniqueidentifier] NOT NULL,
	[EVT_START] [datetime2](3) NOT NULL,
	[EVT_END] [datetime2](3) NOT NULL,
	[ORG_ID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_ProjectEvent] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Region]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Region](
	[ID] [uniqueidentifier] NOT NULL,
	[AREA_ID] [uniqueidentifier] NOT NULL,
	[NAME] [nvarchar](200) NOT NULL,
	[IS_CITY_REGION] [bit] NOT NULL,
 CONSTRAINT [PK_Region] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Street]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Street](
	[ID] [uniqueidentifier] NOT NULL,
	[STYPE_ID] [uniqueidentifier] NOT NULL,
	[NAME] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_Street] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StreetInCity]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StreetInCity](
	[ID] [uniqueidentifier] NOT NULL,
	[CIR_ID] [uniqueidentifier] NOT NULL,
	[STR_ID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_StreetInCity] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StreetType]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StreetType](
	[ID] [uniqueidentifier] NOT NULL,
	[NAME] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_StreetType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TerritoryDescription]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TerritoryDescription](
	[SIC_ID] [uniqueidentifier] NOT NULL,
	[PRCT_ID] [uniqueidentifier] NOT NULL,
	[BLD_START] [nvarchar](10) NULL,
	[BLD_END] [nvarchar](10) NULL,
	[DCLOSE] [date] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [elc].[Candidate]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [elc].[Candidate](
	[ID] [uniqueidentifier] NOT NULL,
	[CND_FULLNAME] [nvarchar](500) NOT NULL,
	[CND_SHORTNAME] [nvarchar](250) NULL,
	[CND_TYPE] [bit] NOT NULL,
	[PARENT_CND_ID] [uniqueidentifier] NULL,
	[LIST_ORDER] [tinyint] NULL,
 CONSTRAINT [PK_Candidate] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [elc].[CandidateInfo]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [elc].[CandidateInfo](
	[ID] [uniqueidentifier] NOT NULL,
	[CND_ID] [uniqueidentifier] NOT NULL,
	[CNCL_ID] [uniqueidentifier] NULL,
	[DSTR_CNCL_ID] [uniqueidentifier] NULL,
	[IS_ELIMINATED] [bit] NOT NULL,
	[CND_ORDER] [tinyint] NULL,
	[WATCH_ORDER] [tinyint] NULL,
 CONSTRAINT [PK_CandidateInfo] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [elc].[CommunityInCouncil]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [elc].[CommunityInCouncil](
	[ID] [uniqueidentifier] NOT NULL,
	[ELC_ID] [uniqueidentifier] NOT NULL,
	[CNCL_ID] [uniqueidentifier] NOT NULL,
	[CMN_REG_ID] [uniqueidentifier] NOT NULL,
	[IS_PARALLEL_COUNTING] [bit] NOT NULL,
 CONSTRAINT [PK_CommunityInCouncil] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [elc].[Council]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [elc].[Council](
	[ID] [uniqueidentifier] NOT NULL,
	[CNCLT_ID] [uniqueidentifier] NOT NULL,
	[NAME] [nvarchar](200) NOT NULL,
	[PRTL_TYPE_ID] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Council] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [elc].[CouncilType]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [elc].[CouncilType](
	[ID] [uniqueidentifier] NOT NULL,
	[NAME] [nvarchar](100) NOT NULL,
	[WATCH_ORDER] [tinyint] NULL,
 CONSTRAINT [PK_CouncilType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [elc].[DistrictInCouncil]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [elc].[DistrictInCouncil](
	[ID] [uniqueidentifier] NOT NULL,
	[CNCL_ID] [uniqueidentifier] NOT NULL,
	[DSTR_ID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_DistrictInCouncil] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [elc].[ElectionInfo]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [elc].[ElectionInfo](
	[ID] [uniqueidentifier] NOT NULL,
	[ET_ID] [uniqueidentifier] NOT NULL,
	[ELC_NAME] [nvarchar](200) NOT NULL,
	[ELC_DATE] [date] NOT NULL,
 CONSTRAINT [PK_ElectionInfo] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [elc].[ElectionPrecinct]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [elc].[ElectionPrecinct](
	[ID] [uniqueidentifier] NOT NULL,
	[ELC_ID] [uniqueidentifier] NOT NULL,
	[PRCT_CMN_ID] [uniqueidentifier] NOT NULL,
	[PRCT_OPENED] [bit] NULL,
	[PRCT_NOTOPEN_CAUSE] [nvarchar](300) NULL,
	[PRCT_VOTERS] [int] NULL,
 CONSTRAINT [PK_ElectionPrecinct] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [elc].[ElectionPrecinctInDistrict]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [elc].[ElectionPrecinctInDistrict](
	[ID] [uniqueidentifier] NOT NULL,
	[ELC_PRCT_ID] [uniqueidentifier] NOT NULL,
	[DSTR_CNCL_ID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_ElectionPrecinctInDistrict] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [elc].[ElectionPrecinctInfo]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [elc].[ElectionPrecinctInfo](
	[ID] [uniqueidentifier] NOT NULL,
	[ELC_PRCT_ID] [uniqueidentifier] NOT NULL,
	[CNCL_ID] [uniqueidentifier] NOT NULL,
	[BULLETIN_RECV] [int] NULL,
 CONSTRAINT [PK_ElectionPrecinctInfo] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [elc].[ElectionType]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [elc].[ElectionType](
	[ID] [uniqueidentifier] NOT NULL,
	[ET_NAME] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_ElectionType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [elc].[Protocol]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [elc].[Protocol](
	[ID] [uniqueidentifier] NOT NULL,
	[ELC_PRCT_ID] [uniqueidentifier] NOT NULL,
	[CNCL_ID] [uniqueidentifier] NULL,
	[PRTL_TYPE_ID] [uniqueidentifier] NOT NULL,
	[PRTL_STATUS_ID] [tinyint] NOT NULL,
	[PRTL_ISSUE] [nvarchar](1000) NULL,
 CONSTRAINT [PK_Protocol] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [elc].[ProtocolDetail]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [elc].[ProtocolDetail](
	[ID] [uniqueidentifier] NOT NULL,
	[PRTL_ID] [uniqueidentifier] NOT NULL,
	[PRTL_TMPL_ID] [uniqueidentifier] NOT NULL,
	[CND_INFO_ID] [uniqueidentifier] NULL,
	[PRTL_ITEM_VALUE] [bigint] NULL,
 CONSTRAINT [PK_ProtocolDetail] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [elc].[ProtocolDetail_Chng]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [elc].[ProtocolDetail_Chng](
	[ID] [uniqueidentifier] NOT NULL,
	[DATE_CHANGE] [datetime2](3) NOT NULL,
	[USER_ID] [uniqueidentifier] NOT NULL,
	[PRTL_ID] [uniqueidentifier] NOT NULL,
	[PRTL_STATUS_OLD] [nchar](10) NULL,
	[PRTL_STATUS_NEW] [nchar](10) NULL,
	[PRTL_TMPL_ID] [uniqueidentifier] NULL,
	[PRTL_VALUE_OLD] [bigint] NULL,
	[PRTL_VALUE_NEW] [bigint] NULL,
 CONSTRAINT [PK_ProtocolDetail_Chng] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [elc].[ProtocolItem]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [elc].[ProtocolItem](
	[ID] [uniqueidentifier] NOT NULL,
	[PRTL_ITEM_NAME] [nvarchar](300) NOT NULL,
 CONSTRAINT [PK_ProtocolItem] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [elc].[ProtocolTemplate]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [elc].[ProtocolTemplate](
	[ID] [uniqueidentifier] NOT NULL,
	[PRTL_TYPE_ID] [uniqueidentifier] NOT NULL,
	[PRTL_ITEM_ID] [uniqueidentifier] NOT NULL,
	[ITEM_ORDER] [tinyint] NOT NULL,
	[CND_TYPE] [bit] NULL,
	[IS_MULTIPLE_VALUE] [bit] NOT NULL,
	[DATE_CLOSE] [date] NULL,
	[GROUP_BY_PARENT] [bit] NOT NULL,
 CONSTRAINT [PK_ProtocolTemplate] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [elc].[ProtocolType]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [elc].[ProtocolType](
	[ID] [uniqueidentifier] NOT NULL,
	[PRTL_NAME] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_ProtocolType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [elc].[Turnout]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [elc].[Turnout](
	[ID] [uniqueidentifier] NOT NULL,
	[ELC_PRCT_ID] [uniqueidentifier] NOT NULL,
	[TT_ID] [uniqueidentifier] NOT NULL,
	[TO_VOTERS] [int] NULL,
 CONSTRAINT [PK_Turnout] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [elc].[TurnoutTime]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [elc].[TurnoutTime](
	[ID] [uniqueidentifier] NOT NULL,
	[TT_VALUE] [time](7) NOT NULL,
 CONSTRAINT [PK_TurnoutTime] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [hub].[Person]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [hub].[Person](
	[CONN_ID] [nvarchar](max) NOT NULL,
	[PSN_ID] [uniqueidentifier] NULL,
	[USR_DSPL_NAME] [nvarchar](max) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [oper].[OpenElectionPrecinct]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [oper].[OpenElectionPrecinct](
	[ID] [uniqueidentifier] NOT NULL,
	[ELC_PRCT_ID] [uniqueidentifier] NOT NULL,
	[REQUEST_DATE] [datetime] NOT NULL,
	[USER_ID] [uniqueidentifier] NOT NULL,
	[ACCEPTED] [bit] NOT NULL,
 CONSTRAINT [PK_OpenElectionPrecinct] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [sd].[Comment]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [sd].[Comment](
	[ID] [uniqueidentifier] NOT NULL,
	[TASK_ID] [uniqueidentifier] NOT NULL,
	[USER_ID] [uniqueidentifier] NOT NULL,
	[CREATE_DATE] [datetime2](3) NOT NULL,
	[TEXT] [nvarchar](200) NOT NULL,
	[COMMENT_ID] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Comment] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [sd].[Task]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [sd].[Task](
	[ID] [uniqueidentifier] NOT NULL,
	[NUMBER] [bigint] IDENTITY(1,1) NOT NULL,
	[OWNER_ID] [uniqueidentifier] NOT NULL,
	[PERFORMER_ID] [uniqueidentifier] NULL,
	[REQUEST] [nvarchar](500) NOT NULL,
	[CREATE_DATE] [datetime2](3) NOT NULL,
	[SEND_DATE] [datetime2](3) NULL,
	[ACCEPT_DATE] [datetime2](3) NULL,
	[DONE_DATE] [datetime2](3) NULL,
	[CONFIRM_DATE] [datetime2](3) NULL,
	[RESPONSE] [nvarchar](500) NULL,
 CONSTRAINT [PK_Task] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [ter].[Address]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ter].[Address](
	[ID] [uniqueidentifier] NOT NULL,
	[SIC_ID] [uniqueidentifier] NOT NULL,
	[BLD_ID] [uniqueidentifier] NOT NULL,
	[BLD_ISSUE] [nvarchar](100) NULL,
 CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [ter].[AddressInPrecinct]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ter].[AddressInPrecinct](
	[ID] [uniqueidentifier] NOT NULL,
	[ADDR_ID] [uniqueidentifier] NOT NULL,
	[PRCT_ID] [uniqueidentifier] NOT NULL,
	[DCLOSE] [date] NULL,
 CONSTRAINT [PK_AddressInPrecinct] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [ter].[Area]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ter].[Area](
	[ID] [uniqueidentifier] NOT NULL,
	[NAME] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Area] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [ter].[Building]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ter].[Building](
	[ID] [uniqueidentifier] NOT NULL,
	[NUMBER] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_Building] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [ter].[City]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ter].[City](
	[ID] [uniqueidentifier] NOT NULL,
	[CITYT_ID] [uniqueidentifier] NOT NULL,
	[NAME] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_City] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [ter].[CityInCommunity]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ter].[CityInCommunity](
	[ID] [uniqueidentifier] NOT NULL,
	[CIR_ID] [uniqueidentifier] NOT NULL,
	[CITY_ID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_CityInRegion] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [ter].[CityType]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ter].[CityType](
	[ID] [uniqueidentifier] NOT NULL,
	[NAME] [nvarchar](5) NOT NULL,
 CONSTRAINT [PK_CityType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [ter].[Community]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ter].[Community](
	[ID] [uniqueidentifier] NOT NULL,
	[NAME] [nvarchar](200) NOT NULL,
	[PARENT_ID] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Community] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [ter].[CommunityChild]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ter].[CommunityChild](
	[ID] [uniqueidentifier] NOT NULL,
	[ELC_PRCT_ID] [uniqueidentifier] NOT NULL,
	[CNCL_ID] [uniqueidentifier] NOT NULL,
	[CMN_ID] [uniqueidentifier] NOT NULL,
	[NAME] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_CommunityChild] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [ter].[CommunityInRegion]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ter].[CommunityInRegion](
	[ID] [uniqueidentifier] NOT NULL,
	[CMN_ID] [uniqueidentifier] NOT NULL,
	[RIA_ID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_CommunityInRegion] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [ter].[District]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ter].[District](
	[ID] [uniqueidentifier] NOT NULL,
	[DSTRT_ID] [uniqueidentifier] NOT NULL,
	[NUMBER] [nvarchar](6) NULL,
 CONSTRAINT [PK_District] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [ter].[DistrictType]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ter].[DistrictType](
	[ID] [uniqueidentifier] NOT NULL,
	[NAME] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_DistrictType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [ter].[Precinct]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ter].[Precinct](
	[ID] [uniqueidentifier] NOT NULL,
	[NUMBER] [nvarchar](6) NOT NULL,
	[TYPE] [bit] NOT NULL,
	[SIZE] [tinyint] NOT NULL,
	[DCLOSE] [datetime] NULL,
 CONSTRAINT [PK_Precinct_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [ter].[PrecinctInCommunity]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ter].[PrecinctInCommunity](
	[ID] [uniqueidentifier] NOT NULL,
	[CMIR_ID] [uniqueidentifier] NOT NULL,
	[PRCT_ID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_PrecinctInCommunity] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [ter].[PrecinctInfo]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ter].[PrecinctInfo](
	[ID] [uniqueidentifier] NOT NULL,
	[PRCT_ID] [uniqueidentifier] NOT NULL,
	[IDATE] [date] NOT NULL,
	[VOTERS] [int] NULL,
 CONSTRAINT [PK_PrecinctInfo] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [ter].[Region]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ter].[Region](
	[ID] [uniqueidentifier] NOT NULL,
	[NAME] [nvarchar](200) NOT NULL,
	[IS_CITY_REGION] [bit] NOT NULL,
 CONSTRAINT [PK_Region] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [ter].[RegionInArea]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ter].[RegionInArea](
	[ID] [uniqueidentifier] NOT NULL,
	[AREA_ID] [uniqueidentifier] NOT NULL,
	[REG_ID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_RegionInArea] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [ter].[Street]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ter].[Street](
	[ID] [uniqueidentifier] NOT NULL,
	[STRT_ID] [uniqueidentifier] NOT NULL,
	[NAME] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_Street] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [ter].[StreetInCity]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ter].[StreetInCity](
	[ID] [uniqueidentifier] NOT NULL,
	[CIC_ID] [uniqueidentifier] NOT NULL,
	[STR_ID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_StreetInCity] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [ter].[StreetType]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ter].[StreetType](
	[ID] [uniqueidentifier] NOT NULL,
	[NAME] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_StreetType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [ter].[TerritoryDescription]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ter].[TerritoryDescription](
	[SIC_ID] [uniqueidentifier] NOT NULL,
	[PRCT_ID] [uniqueidentifier] NOT NULL,
	[BLD_START] [nvarchar](20) NULL,
	[BLD_START_1] [int] NULL,
	[BLD_START_M] [nvarchar](5) NULL,
	[BLD_START_2] [nvarchar](20) NULL,
	[BLD_END] [nvarchar](20) NULL,
	[BLD_END_1] [int] NULL,
	[BLD_END_M] [nvarchar](5) NULL,
	[BLD_END_2] [nvarchar](20) NULL,
	[DCLOSE] [date] NULL
) ON [PRIMARY]
GO
/****** Object:  Index [IX_AddressInPrecinct]    Script Date: 12/1/2024 8:06:46 PM ******/
CREATE NONCLUSTERED INDEX [IX_AddressInPrecinct] ON [dbo].[AddressInPrecinct]
(
	[ADDR_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Person] ADD  CONSTRAINT [DF_Person_HAS_TELEGRAM]  DEFAULT ((0)) FOR [HAS_TELEGRAM]
GO
ALTER TABLE [dbo].[Person] ADD  CONSTRAINT [DF_Person_HAS_VIBER]  DEFAULT ((0)) FOR [HAS_VIBER]
GO
ALTER TABLE [dbo].[Person] ADD  CONSTRAINT [DF_Person_HAS_WHATSAPP]  DEFAULT ((0)) FOR [HAS_WHATSAPP]
GO
ALTER TABLE [dbo].[Person] ADD  CONSTRAINT [DF_Person_HAS_FACEBOOK]  DEFAULT ((0)) FOR [HAS_FACEBOOK]
GO
ALTER TABLE [dbo].[Person] ADD  CONSTRAINT [DF_Person_HAS_INSTAGRAM]  DEFAULT ((0)) FOR [HAS_INSTAGRAM]
GO
ALTER TABLE [dbo].[Person] ADD  CONSTRAINT [DF_Person_IS_EMPLOYEE]  DEFAULT ((0)) FOR [IS_EMPLOYEE]
GO
ALTER TABLE [dbo].[Person] ADD  CONSTRAINT [DF_Person_IS_DEPUTY]  DEFAULT ((0)) FOR [IS_DEPUTY]
GO
ALTER TABLE [dbo].[Person] ADD  CONSTRAINT [DF_Person_IS_PARTY_MEMBER]  DEFAULT ((0)) FOR [IS_PARTY_MEMBER]
GO
ALTER TABLE [dbo].[Person] ADD  CONSTRAINT [DF_Person_IS_DELETED]  DEFAULT ((0)) FOR [IS_DELETED]
GO
ALTER TABLE [cfg].[UserAccess]  WITH CHECK ADD  CONSTRAINT [FK_UserAccess_ObjectAccess] FOREIGN KEY([ObjectId])
REFERENCES [cfg].[ObjectAccess] ([Id])
GO
ALTER TABLE [cfg].[UserAccess] CHECK CONSTRAINT [FK_UserAccess_ObjectAccess]
GO
ALTER TABLE [dbo].[Address]  WITH CHECK ADD  CONSTRAINT [FK_Address_Address] FOREIGN KEY([SIC_ID])
REFERENCES [dbo].[StreetInCity] ([ID])
GO
ALTER TABLE [dbo].[Address] CHECK CONSTRAINT [FK_Address_Address]
GO
ALTER TABLE [dbo].[Address]  WITH CHECK ADD  CONSTRAINT [FK_Address_Building] FOREIGN KEY([BLD_ID])
REFERENCES [dbo].[Building] ([ID])
GO
ALTER TABLE [dbo].[Address] CHECK CONSTRAINT [FK_Address_Building]
GO
ALTER TABLE [dbo].[AddressInPrecinct]  WITH CHECK ADD  CONSTRAINT [FK_AddressInPrecinct_Address] FOREIGN KEY([ADDR_ID])
REFERENCES [dbo].[Address] ([ID])
GO
ALTER TABLE [dbo].[AddressInPrecinct] CHECK CONSTRAINT [FK_AddressInPrecinct_Address]
GO
ALTER TABLE [dbo].[AddressInPrecinct]  WITH CHECK ADD  CONSTRAINT [FK_AddressInPrecinct_Precinct] FOREIGN KEY([PRCT_ID])
REFERENCES [dbo].[Precinct] ([ID])
GO
ALTER TABLE [dbo].[AddressInPrecinct] CHECK CONSTRAINT [FK_AddressInPrecinct_Precinct]
GO
ALTER TABLE [dbo].[City]  WITH CHECK ADD  CONSTRAINT [FK_City_CityType] FOREIGN KEY([CITYT_ID])
REFERENCES [dbo].[CityType] ([ID])
GO
ALTER TABLE [dbo].[City] CHECK CONSTRAINT [FK_City_CityType]
GO
ALTER TABLE [dbo].[CityInRegion]  WITH CHECK ADD  CONSTRAINT [FK_CityInRegion_City] FOREIGN KEY([CITY_ID])
REFERENCES [dbo].[City] ([ID])
GO
ALTER TABLE [dbo].[CityInRegion] CHECK CONSTRAINT [FK_CityInRegion_City]
GO
ALTER TABLE [dbo].[CityInRegion]  WITH CHECK ADD  CONSTRAINT [FK_CityInRegion_Region] FOREIGN KEY([REG_ID])
REFERENCES [dbo].[Region] ([ID])
GO
ALTER TABLE [dbo].[CityInRegion] CHECK CONSTRAINT [FK_CityInRegion_Region]
GO
ALTER TABLE [dbo].[District]  WITH CHECK ADD  CONSTRAINT [FK_District_DistrictType] FOREIGN KEY([DSTRT_ID])
REFERENCES [dbo].[DistrictType] ([ID])
GO
ALTER TABLE [dbo].[District] CHECK CONSTRAINT [FK_District_DistrictType]
GO
ALTER TABLE [dbo].[OrganizationStructure]  WITH CHECK ADD  CONSTRAINT [FK_OrganizationStructure_OrganizationChild] FOREIGN KEY([ORG_ID])
REFERENCES [dbo].[Organization] ([ID])
GO
ALTER TABLE [dbo].[OrganizationStructure] CHECK CONSTRAINT [FK_OrganizationStructure_OrganizationChild]
GO
ALTER TABLE [dbo].[OrganizationStructure]  WITH CHECK ADD  CONSTRAINT [FK_OrganizationStructure_OrganizationParent] FOREIGN KEY([PARENT_ID])
REFERENCES [dbo].[Organization] ([ID])
GO
ALTER TABLE [dbo].[OrganizationStructure] CHECK CONSTRAINT [FK_OrganizationStructure_OrganizationParent]
GO
ALTER TABLE [dbo].[Person]  WITH CHECK ADD  CONSTRAINT [FK_Person_AddressHome] FOREIGN KEY([ADDR_HOME])
REFERENCES [ter].[Address] ([ID])
GO
ALTER TABLE [dbo].[Person] CHECK CONSTRAINT [FK_Person_AddressHome]
GO
ALTER TABLE [dbo].[Person]  WITH CHECK ADD  CONSTRAINT [FK_Person_AddressReg] FOREIGN KEY([ADDR_REG])
REFERENCES [ter].[Address] ([ID])
GO
ALTER TABLE [dbo].[Person] CHECK CONSTRAINT [FK_Person_AddressReg]
GO
ALTER TABLE [dbo].[PersonEvent]  WITH CHECK ADD  CONSTRAINT [FK_PersonEvent_Person] FOREIGN KEY([PSN_ID])
REFERENCES [dbo].[Person] ([ID])
GO
ALTER TABLE [dbo].[PersonEvent] CHECK CONSTRAINT [FK_PersonEvent_Person]
GO
ALTER TABLE [dbo].[PersonEvent]  WITH CHECK ADD  CONSTRAINT [FK_PersonEvent_ProjectEvent] FOREIGN KEY([PRE_ID])
REFERENCES [dbo].[ProjectEvent] ([ID])
GO
ALTER TABLE [dbo].[PersonEvent] CHECK CONSTRAINT [FK_PersonEvent_ProjectEvent]
GO
ALTER TABLE [dbo].[PersonFormStatus]  WITH CHECK ADD  CONSTRAINT [FK_PersonFormStatus_Person] FOREIGN KEY([PSN_ID])
REFERENCES [dbo].[Person] ([ID])
GO
ALTER TABLE [dbo].[PersonFormStatus] CHECK CONSTRAINT [FK_PersonFormStatus_Person]
GO
ALTER TABLE [dbo].[PersonInfo]  WITH CHECK ADD  CONSTRAINT [FK_PersonInfo_Person] FOREIGN KEY([PSN_ID])
REFERENCES [dbo].[Person] ([ID])
GO
ALTER TABLE [dbo].[PersonInfo] CHECK CONSTRAINT [FK_PersonInfo_Person]
GO
ALTER TABLE [dbo].[PersonParty]  WITH CHECK ADD  CONSTRAINT [FK_PersonParty_Person] FOREIGN KEY([PSN_ID])
REFERENCES [dbo].[Person] ([ID])
GO
ALTER TABLE [dbo].[PersonParty] CHECK CONSTRAINT [FK_PersonParty_Person]
GO
ALTER TABLE [dbo].[PositionInOrganization]  WITH CHECK ADD  CONSTRAINT [FK_PositionInOrganization_Organization] FOREIGN KEY([ORG_ID])
REFERENCES [dbo].[Organization] ([ID])
GO
ALTER TABLE [dbo].[PositionInOrganization] CHECK CONSTRAINT [FK_PositionInOrganization_Organization]
GO
ALTER TABLE [dbo].[PositionInOrganization]  WITH CHECK ADD  CONSTRAINT [FK_PositionInOrganization_Person] FOREIGN KEY([PSN_ID])
REFERENCES [dbo].[Person] ([ID])
GO
ALTER TABLE [dbo].[PositionInOrganization] CHECK CONSTRAINT [FK_PositionInOrganization_Person]
GO
ALTER TABLE [dbo].[PositionInOrganization]  WITH CHECK ADD  CONSTRAINT [FK_PositionInOrganization_Position] FOREIGN KEY([POS_ID])
REFERENCES [dbo].[Position] ([ID])
GO
ALTER TABLE [dbo].[PositionInOrganization] CHECK CONSTRAINT [FK_PositionInOrganization_Position]
GO
ALTER TABLE [dbo].[PrecinctInDistrict]  WITH CHECK ADD  CONSTRAINT [FK_PrecinctInDistrict_District] FOREIGN KEY([DSTR_ID])
REFERENCES [dbo].[District] ([ID])
GO
ALTER TABLE [dbo].[PrecinctInDistrict] CHECK CONSTRAINT [FK_PrecinctInDistrict_District]
GO
ALTER TABLE [dbo].[PrecinctInDistrict]  WITH CHECK ADD  CONSTRAINT [FK_PrecinctInDistrict_Precinct] FOREIGN KEY([PRCT_ID])
REFERENCES [dbo].[Precinct] ([ID])
GO
ALTER TABLE [dbo].[PrecinctInDistrict] CHECK CONSTRAINT [FK_PrecinctInDistrict_Precinct]
GO
ALTER TABLE [dbo].[ProjectEvent]  WITH CHECK ADD  CONSTRAINT [FK_ProjectEvent_Organization] FOREIGN KEY([ORG_ID])
REFERENCES [dbo].[Organization] ([ID])
GO
ALTER TABLE [dbo].[ProjectEvent] CHECK CONSTRAINT [FK_ProjectEvent_Organization]
GO
ALTER TABLE [dbo].[ProjectEvent]  WITH CHECK ADD  CONSTRAINT [FK_ProjectEvent_Project] FOREIGN KEY([PRJT_ID])
REFERENCES [dbo].[Project] ([ID])
GO
ALTER TABLE [dbo].[ProjectEvent] CHECK CONSTRAINT [FK_ProjectEvent_Project]
GO
ALTER TABLE [dbo].[Region]  WITH CHECK ADD  CONSTRAINT [FK_Region_Area] FOREIGN KEY([AREA_ID])
REFERENCES [dbo].[Area] ([ID])
GO
ALTER TABLE [dbo].[Region] CHECK CONSTRAINT [FK_Region_Area]
GO
ALTER TABLE [dbo].[Street]  WITH CHECK ADD  CONSTRAINT [FK_Street_StreetType] FOREIGN KEY([STYPE_ID])
REFERENCES [dbo].[StreetType] ([ID])
GO
ALTER TABLE [dbo].[Street] CHECK CONSTRAINT [FK_Street_StreetType]
GO
ALTER TABLE [dbo].[StreetInCity]  WITH CHECK ADD  CONSTRAINT [FK_StreetInCity_CityInRegion] FOREIGN KEY([CIR_ID])
REFERENCES [dbo].[CityInRegion] ([ID])
GO
ALTER TABLE [dbo].[StreetInCity] CHECK CONSTRAINT [FK_StreetInCity_CityInRegion]
GO
ALTER TABLE [dbo].[StreetInCity]  WITH CHECK ADD  CONSTRAINT [FK_StreetInCity_StreetInCity] FOREIGN KEY([STR_ID])
REFERENCES [dbo].[Street] ([ID])
GO
ALTER TABLE [dbo].[StreetInCity] CHECK CONSTRAINT [FK_StreetInCity_StreetInCity]
GO
ALTER TABLE [dbo].[TerritoryDescription]  WITH CHECK ADD  CONSTRAINT [FK_TerritoryDescription_Precinct] FOREIGN KEY([PRCT_ID])
REFERENCES [dbo].[Precinct] ([ID])
GO
ALTER TABLE [dbo].[TerritoryDescription] CHECK CONSTRAINT [FK_TerritoryDescription_Precinct]
GO
ALTER TABLE [dbo].[TerritoryDescription]  WITH CHECK ADD  CONSTRAINT [FK_TerritoryDescription_StreetInCity] FOREIGN KEY([SIC_ID])
REFERENCES [dbo].[StreetInCity] ([ID])
GO
ALTER TABLE [dbo].[TerritoryDescription] CHECK CONSTRAINT [FK_TerritoryDescription_StreetInCity]
GO
ALTER TABLE [elc].[CandidateInfo]  WITH CHECK ADD  CONSTRAINT [FK_CandidateInfo_Candidate] FOREIGN KEY([CND_ID])
REFERENCES [elc].[Candidate] ([ID])
GO
ALTER TABLE [elc].[CandidateInfo] CHECK CONSTRAINT [FK_CandidateInfo_Candidate]
GO
ALTER TABLE [elc].[CandidateInfo]  WITH CHECK ADD  CONSTRAINT [FK_CandidateInfo_Council] FOREIGN KEY([CNCL_ID])
REFERENCES [elc].[Council] ([ID])
GO
ALTER TABLE [elc].[CandidateInfo] CHECK CONSTRAINT [FK_CandidateInfo_Council]
GO
ALTER TABLE [elc].[CandidateInfo]  WITH CHECK ADD  CONSTRAINT [FK_CandidateInfo_DistrictInCouncil] FOREIGN KEY([DSTR_CNCL_ID])
REFERENCES [elc].[DistrictInCouncil] ([ID])
GO
ALTER TABLE [elc].[CandidateInfo] CHECK CONSTRAINT [FK_CandidateInfo_DistrictInCouncil]
GO
ALTER TABLE [elc].[CommunityInCouncil]  WITH CHECK ADD  CONSTRAINT [FK_CommunityInCouncil_CommunityInRegion] FOREIGN KEY([CMN_REG_ID])
REFERENCES [ter].[CommunityInRegion] ([ID])
GO
ALTER TABLE [elc].[CommunityInCouncil] CHECK CONSTRAINT [FK_CommunityInCouncil_CommunityInRegion]
GO
ALTER TABLE [elc].[CommunityInCouncil]  WITH CHECK ADD  CONSTRAINT [FK_CommunityInCouncil_Council] FOREIGN KEY([CNCL_ID])
REFERENCES [elc].[Council] ([ID])
GO
ALTER TABLE [elc].[CommunityInCouncil] CHECK CONSTRAINT [FK_CommunityInCouncil_Council]
GO
ALTER TABLE [elc].[CommunityInCouncil]  WITH CHECK ADD  CONSTRAINT [FK_CommunityInCouncil_ElectionInfo] FOREIGN KEY([ELC_ID])
REFERENCES [elc].[ElectionInfo] ([ID])
GO
ALTER TABLE [elc].[CommunityInCouncil] CHECK CONSTRAINT [FK_CommunityInCouncil_ElectionInfo]
GO
ALTER TABLE [elc].[Council]  WITH CHECK ADD  CONSTRAINT [FK_Council_CouncilType] FOREIGN KEY([CNCLT_ID])
REFERENCES [elc].[CouncilType] ([ID])
GO
ALTER TABLE [elc].[Council] CHECK CONSTRAINT [FK_Council_CouncilType]
GO
ALTER TABLE [elc].[Council]  WITH CHECK ADD  CONSTRAINT [FK_Council_ProtocolType] FOREIGN KEY([PRTL_TYPE_ID])
REFERENCES [elc].[ProtocolType] ([ID])
GO
ALTER TABLE [elc].[Council] CHECK CONSTRAINT [FK_Council_ProtocolType]
GO
ALTER TABLE [elc].[DistrictInCouncil]  WITH CHECK ADD  CONSTRAINT [FK_DistrictInCouncil_Council] FOREIGN KEY([CNCL_ID])
REFERENCES [elc].[Council] ([ID])
GO
ALTER TABLE [elc].[DistrictInCouncil] CHECK CONSTRAINT [FK_DistrictInCouncil_Council]
GO
ALTER TABLE [elc].[DistrictInCouncil]  WITH CHECK ADD  CONSTRAINT [FK_DistrictInCouncil_District] FOREIGN KEY([DSTR_ID])
REFERENCES [ter].[District] ([ID])
GO
ALTER TABLE [elc].[DistrictInCouncil] CHECK CONSTRAINT [FK_DistrictInCouncil_District]
GO
ALTER TABLE [elc].[ElectionInfo]  WITH CHECK ADD  CONSTRAINT [FK_ElectionInfo_ElectionType] FOREIGN KEY([ET_ID])
REFERENCES [elc].[ElectionType] ([ID])
GO
ALTER TABLE [elc].[ElectionInfo] CHECK CONSTRAINT [FK_ElectionInfo_ElectionType]
GO
ALTER TABLE [elc].[ElectionPrecinct]  WITH CHECK ADD  CONSTRAINT [FK_ElectionPrecinct_ElectionInfo] FOREIGN KEY([ELC_ID])
REFERENCES [elc].[ElectionInfo] ([ID])
GO
ALTER TABLE [elc].[ElectionPrecinct] CHECK CONSTRAINT [FK_ElectionPrecinct_ElectionInfo]
GO
ALTER TABLE [elc].[ElectionPrecinct]  WITH CHECK ADD  CONSTRAINT [FK_ElectionPrecinct_PrecinctInCommunity] FOREIGN KEY([PRCT_CMN_ID])
REFERENCES [ter].[PrecinctInCommunity] ([ID])
GO
ALTER TABLE [elc].[ElectionPrecinct] CHECK CONSTRAINT [FK_ElectionPrecinct_PrecinctInCommunity]
GO
ALTER TABLE [elc].[ElectionPrecinctInDistrict]  WITH CHECK ADD  CONSTRAINT [FK_ElectionPrecinctInDistrict_DistrictInCouncil] FOREIGN KEY([DSTR_CNCL_ID])
REFERENCES [elc].[DistrictInCouncil] ([ID])
GO
ALTER TABLE [elc].[ElectionPrecinctInDistrict] CHECK CONSTRAINT [FK_ElectionPrecinctInDistrict_DistrictInCouncil]
GO
ALTER TABLE [elc].[ElectionPrecinctInDistrict]  WITH CHECK ADD  CONSTRAINT [FK_ElectionPrecinctInDistrict_ElectionPrecinct] FOREIGN KEY([ELC_PRCT_ID])
REFERENCES [elc].[ElectionPrecinct] ([ID])
GO
ALTER TABLE [elc].[ElectionPrecinctInDistrict] CHECK CONSTRAINT [FK_ElectionPrecinctInDistrict_ElectionPrecinct]
GO
ALTER TABLE [elc].[ElectionPrecinctInfo]  WITH CHECK ADD  CONSTRAINT [FK_ElectionPrecinctInfo_Council] FOREIGN KEY([CNCL_ID])
REFERENCES [elc].[Council] ([ID])
GO
ALTER TABLE [elc].[ElectionPrecinctInfo] CHECK CONSTRAINT [FK_ElectionPrecinctInfo_Council]
GO
ALTER TABLE [elc].[ElectionPrecinctInfo]  WITH CHECK ADD  CONSTRAINT [FK_ElectionPrecinctInfo_ElectionPrecinct] FOREIGN KEY([ELC_PRCT_ID])
REFERENCES [elc].[ElectionPrecinct] ([ID])
GO
ALTER TABLE [elc].[ElectionPrecinctInfo] CHECK CONSTRAINT [FK_ElectionPrecinctInfo_ElectionPrecinct]
GO
ALTER TABLE [elc].[Protocol]  WITH CHECK ADD  CONSTRAINT [FK_Protocol_Council] FOREIGN KEY([CNCL_ID])
REFERENCES [elc].[Council] ([ID])
GO
ALTER TABLE [elc].[Protocol] CHECK CONSTRAINT [FK_Protocol_Council]
GO
ALTER TABLE [elc].[Protocol]  WITH CHECK ADD  CONSTRAINT [FK_Protocol_ElectionPrecinct] FOREIGN KEY([ELC_PRCT_ID])
REFERENCES [elc].[ElectionPrecinct] ([ID])
GO
ALTER TABLE [elc].[Protocol] CHECK CONSTRAINT [FK_Protocol_ElectionPrecinct]
GO
ALTER TABLE [elc].[Protocol]  WITH CHECK ADD  CONSTRAINT [FK_Protocol_ProtocolType] FOREIGN KEY([PRTL_TYPE_ID])
REFERENCES [elc].[ProtocolType] ([ID])
GO
ALTER TABLE [elc].[Protocol] CHECK CONSTRAINT [FK_Protocol_ProtocolType]
GO
ALTER TABLE [elc].[ProtocolDetail]  WITH CHECK ADD  CONSTRAINT [FK_ProtocolDetail_CandidateInfo] FOREIGN KEY([CND_INFO_ID])
REFERENCES [elc].[CandidateInfo] ([ID])
GO
ALTER TABLE [elc].[ProtocolDetail] CHECK CONSTRAINT [FK_ProtocolDetail_CandidateInfo]
GO
ALTER TABLE [elc].[ProtocolDetail]  WITH CHECK ADD  CONSTRAINT [FK_ProtocolDetail_Protocol] FOREIGN KEY([PRTL_ID])
REFERENCES [elc].[Protocol] ([ID])
GO
ALTER TABLE [elc].[ProtocolDetail] CHECK CONSTRAINT [FK_ProtocolDetail_Protocol]
GO
ALTER TABLE [elc].[ProtocolDetail]  WITH CHECK ADD  CONSTRAINT [FK_ProtocolDetail_ProtocolTemplate] FOREIGN KEY([PRTL_TMPL_ID])
REFERENCES [elc].[ProtocolTemplate] ([ID])
GO
ALTER TABLE [elc].[ProtocolDetail] CHECK CONSTRAINT [FK_ProtocolDetail_ProtocolTemplate]
GO
ALTER TABLE [elc].[ProtocolTemplate]  WITH CHECK ADD  CONSTRAINT [FK_ProtocolTemplate_ProtocolItem] FOREIGN KEY([PRTL_ITEM_ID])
REFERENCES [elc].[ProtocolItem] ([ID])
GO
ALTER TABLE [elc].[ProtocolTemplate] CHECK CONSTRAINT [FK_ProtocolTemplate_ProtocolItem]
GO
ALTER TABLE [elc].[ProtocolTemplate]  WITH CHECK ADD  CONSTRAINT [FK_ProtocolTemplate_ProtocolType] FOREIGN KEY([PRTL_TYPE_ID])
REFERENCES [elc].[ProtocolType] ([ID])
GO
ALTER TABLE [elc].[ProtocolTemplate] CHECK CONSTRAINT [FK_ProtocolTemplate_ProtocolType]
GO
ALTER TABLE [elc].[Turnout]  WITH CHECK ADD  CONSTRAINT [FK_Turnout_ElectionPrecinct] FOREIGN KEY([ELC_PRCT_ID])
REFERENCES [elc].[ElectionPrecinct] ([ID])
GO
ALTER TABLE [elc].[Turnout] CHECK CONSTRAINT [FK_Turnout_ElectionPrecinct]
GO
ALTER TABLE [elc].[Turnout]  WITH CHECK ADD  CONSTRAINT [FK_Turnout_TurnoutTime] FOREIGN KEY([TT_ID])
REFERENCES [elc].[TurnoutTime] ([ID])
GO
ALTER TABLE [elc].[Turnout] CHECK CONSTRAINT [FK_Turnout_TurnoutTime]
GO
ALTER TABLE [sd].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_Comment] FOREIGN KEY([COMMENT_ID])
REFERENCES [sd].[Comment] ([ID])
GO
ALTER TABLE [sd].[Comment] CHECK CONSTRAINT [FK_Comment_Comment]
GO
ALTER TABLE [sd].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_Task] FOREIGN KEY([TASK_ID])
REFERENCES [sd].[Task] ([ID])
GO
ALTER TABLE [sd].[Comment] CHECK CONSTRAINT [FK_Comment_Task]
GO
ALTER TABLE [ter].[Address]  WITH CHECK ADD  CONSTRAINT [FK_Address_Address] FOREIGN KEY([SIC_ID])
REFERENCES [ter].[StreetInCity] ([ID])
GO
ALTER TABLE [ter].[Address] CHECK CONSTRAINT [FK_Address_Address]
GO
ALTER TABLE [ter].[Address]  WITH CHECK ADD  CONSTRAINT [FK_Address_Building] FOREIGN KEY([BLD_ID])
REFERENCES [ter].[Building] ([ID])
GO
ALTER TABLE [ter].[Address] CHECK CONSTRAINT [FK_Address_Building]
GO
ALTER TABLE [ter].[AddressInPrecinct]  WITH CHECK ADD  CONSTRAINT [FK_AddressInPrecinct_Address] FOREIGN KEY([ADDR_ID])
REFERENCES [ter].[Address] ([ID])
GO
ALTER TABLE [ter].[AddressInPrecinct] CHECK CONSTRAINT [FK_AddressInPrecinct_Address]
GO
ALTER TABLE [ter].[AddressInPrecinct]  WITH CHECK ADD  CONSTRAINT [FK_AddressInPrecinct_Precinct] FOREIGN KEY([PRCT_ID])
REFERENCES [ter].[Precinct] ([ID])
GO
ALTER TABLE [ter].[AddressInPrecinct] CHECK CONSTRAINT [FK_AddressInPrecinct_Precinct]
GO
ALTER TABLE [ter].[City]  WITH CHECK ADD  CONSTRAINT [FK_City_CityType] FOREIGN KEY([CITYT_ID])
REFERENCES [ter].[CityType] ([ID])
GO
ALTER TABLE [ter].[City] CHECK CONSTRAINT [FK_City_CityType]
GO
ALTER TABLE [ter].[CityInCommunity]  WITH CHECK ADD  CONSTRAINT [FK_CityInRegion_City] FOREIGN KEY([CITY_ID])
REFERENCES [ter].[City] ([ID])
GO
ALTER TABLE [ter].[CityInCommunity] CHECK CONSTRAINT [FK_CityInRegion_City]
GO
ALTER TABLE [ter].[CityInCommunity]  WITH CHECK ADD  CONSTRAINT [FK_CityInRegion_CommunityInRegion] FOREIGN KEY([CIR_ID])
REFERENCES [ter].[CommunityInRegion] ([ID])
GO
ALTER TABLE [ter].[CityInCommunity] CHECK CONSTRAINT [FK_CityInRegion_CommunityInRegion]
GO
ALTER TABLE [ter].[CommunityInRegion]  WITH CHECK ADD  CONSTRAINT [FK_CommunityInRegion_Community] FOREIGN KEY([CMN_ID])
REFERENCES [ter].[Community] ([ID])
GO
ALTER TABLE [ter].[CommunityInRegion] CHECK CONSTRAINT [FK_CommunityInRegion_Community]
GO
ALTER TABLE [ter].[CommunityInRegion]  WITH CHECK ADD  CONSTRAINT [FK_CommunityInRegion_RegionInArea] FOREIGN KEY([RIA_ID])
REFERENCES [ter].[RegionInArea] ([ID])
GO
ALTER TABLE [ter].[CommunityInRegion] CHECK CONSTRAINT [FK_CommunityInRegion_RegionInArea]
GO
ALTER TABLE [ter].[District]  WITH CHECK ADD  CONSTRAINT [FK_District_DistrictType] FOREIGN KEY([DSTRT_ID])
REFERENCES [ter].[DistrictType] ([ID])
GO
ALTER TABLE [ter].[District] CHECK CONSTRAINT [FK_District_DistrictType]
GO
ALTER TABLE [ter].[PrecinctInCommunity]  WITH CHECK ADD  CONSTRAINT [FK_PrecinctInCommunity_CommunityInRegion] FOREIGN KEY([CMIR_ID])
REFERENCES [ter].[CommunityInRegion] ([ID])
GO
ALTER TABLE [ter].[PrecinctInCommunity] CHECK CONSTRAINT [FK_PrecinctInCommunity_CommunityInRegion]
GO
ALTER TABLE [ter].[PrecinctInCommunity]  WITH CHECK ADD  CONSTRAINT [FK_PrecinctInCommunity_Precinct] FOREIGN KEY([PRCT_ID])
REFERENCES [ter].[Precinct] ([ID])
GO
ALTER TABLE [ter].[PrecinctInCommunity] CHECK CONSTRAINT [FK_PrecinctInCommunity_Precinct]
GO
ALTER TABLE [ter].[PrecinctInfo]  WITH CHECK ADD  CONSTRAINT [FK_PrecinctInfo_Precinct] FOREIGN KEY([PRCT_ID])
REFERENCES [ter].[Precinct] ([ID])
GO
ALTER TABLE [ter].[PrecinctInfo] CHECK CONSTRAINT [FK_PrecinctInfo_Precinct]
GO
ALTER TABLE [ter].[RegionInArea]  WITH CHECK ADD  CONSTRAINT [FK_RegionInArea_Area] FOREIGN KEY([AREA_ID])
REFERENCES [ter].[Area] ([ID])
GO
ALTER TABLE [ter].[RegionInArea] CHECK CONSTRAINT [FK_RegionInArea_Area]
GO
ALTER TABLE [ter].[RegionInArea]  WITH CHECK ADD  CONSTRAINT [FK_RegionInArea_Region] FOREIGN KEY([REG_ID])
REFERENCES [ter].[Region] ([ID])
GO
ALTER TABLE [ter].[RegionInArea] CHECK CONSTRAINT [FK_RegionInArea_Region]
GO
ALTER TABLE [ter].[Street]  WITH CHECK ADD  CONSTRAINT [FK_Street_StreetType] FOREIGN KEY([STRT_ID])
REFERENCES [ter].[StreetType] ([ID])
GO
ALTER TABLE [ter].[Street] CHECK CONSTRAINT [FK_Street_StreetType]
GO
ALTER TABLE [ter].[StreetInCity]  WITH CHECK ADD  CONSTRAINT [FK_StreetInCity_CityInCommunity] FOREIGN KEY([CIC_ID])
REFERENCES [ter].[CityInCommunity] ([ID])
GO
ALTER TABLE [ter].[StreetInCity] CHECK CONSTRAINT [FK_StreetInCity_CityInCommunity]
GO
ALTER TABLE [ter].[StreetInCity]  WITH CHECK ADD  CONSTRAINT [FK_StreetInCity_StreetInCity] FOREIGN KEY([STR_ID])
REFERENCES [ter].[Street] ([ID])
GO
ALTER TABLE [ter].[StreetInCity] CHECK CONSTRAINT [FK_StreetInCity_StreetInCity]
GO
ALTER TABLE [ter].[TerritoryDescription]  WITH CHECK ADD  CONSTRAINT [FK_TerritoryDescription_Precinct] FOREIGN KEY([PRCT_ID])
REFERENCES [ter].[Precinct] ([ID])
GO
ALTER TABLE [ter].[TerritoryDescription] CHECK CONSTRAINT [FK_TerritoryDescription_Precinct]
GO
ALTER TABLE [ter].[TerritoryDescription]  WITH CHECK ADD  CONSTRAINT [FK_TerritoryDescription_StreetInCity] FOREIGN KEY([SIC_ID])
REFERENCES [ter].[StreetInCity] ([ID])
GO
ALTER TABLE [ter].[TerritoryDescription] CHECK CONSTRAINT [FK_TerritoryDescription_StreetInCity]
GO
/****** Object:  StoredProcedure [cfg].[sp_getaccessbyuser]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		ghost1908
-- Create date: 20.05.2020
-- Description:	Получить список ID указанной таблицы для указанного пользователя
-- =============================================
-- Modify date: 22.05.2020
-- Description: Изменен результирующий запрос
-- =============================================
-- Modify date: 19.08.2020
-- Description: Перенесено в другую схему БД
-- =============================================
-- Modify date: 22.07.2021
-- Description: Добавлены города и улицы
-- =============================================
CREATE PROCEDURE [cfg].[sp_getaccessbyuser] 
	@UserID uniqueidentifier,
	@TableOutput nvarchar(500)
AS
BEGIN
	SET NOCOUNT ON;

    DECLARE @sqlquery nvarchar(MAX)
	DECLARE @WhereAccess nvarchar(MAX)

	SELECT @WhereAccess =
	STUFF(
	(SELECT IIF(ua.ObjectValue IS NULL,
				 N'',
				 N' OR PhoenixDB.' + oa.ObjectName + N'.ID = ''' + CAST(ua.ObjectValue AS nvarchar(256)) + N'''')
	FROM PhoenixDB.cfg.UserAccess ua
	JOIN PhoenixDB.cfg.ObjectAccess oa
		ON oa.Id = ua.ObjectId
	WHERE
		ua.UserId = @UserID
	FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 2, 2, '')
	
	SET @sqlquery = N'SELECT DISTINCT PhoenixDB.' + @TableOutput + N'.ID '+
	'FROM PhoenixDB.ter.Area
		JOIN PhoenixDB.ter.RegionInArea
			ON PhoenixDB.ter.RegionInArea.AREA_ID = PhoenixDB.ter.Area.ID
		JOIN PhoenixDB.ter.Region
			ON PhoenixDB.ter.Region.ID = PhoenixDB.ter.RegionInArea.REG_ID
		JOIN PhoenixDB.ter.CommunityInRegion
			ON PhoenixDB.ter.CommunityInRegion.RIA_ID = PhoenixDB.ter.RegionInArea.ID
		JOIN PhoenixDB.ter.Community
			ON PhoenixDB.ter.Community.ID = PhoenixDB.ter.CommunityInRegion.CMN_ID
		JOIN PhoenixDB.ter.PrecinctInCommunity
			ON PhoenixDB.ter.PrecinctInCommunity.CMIR_ID = PhoenixDB.ter.CommunityInRegion.ID
		JOIN PhoenixDB.ter.Precinct
			ON PhoenixDB.ter.Precinct.ID = PhoenixDB.ter.PrecinctInCommunity.PRCT_ID
		JOIN PhoenixDB.ter.CityInCommunity
			ON PhoenixDB.ter.CityInCommunity.CIR_ID = PhoenixDB.ter.CommunityInRegion.ID
		JOIN PhoenixDB.ter.City
			ON PhoenixDB.ter.City.ID = PhoenixDB.ter.CityInCommunity.CITY_ID
		JOIN PhoenixDB.ter.StreetInCity
			ON PhoenixDB.ter.StreetInCity.CIC_ID = PhoenixDB.ter.CityInCommunity.ID
		JOIN PhoenixDB.ter.Street
			ON PhoenixDB.ter.Street.ID = PhoenixDB.ter.StreetInCity.STR_ID'
	IF @WhereAccess <> ''
		SET @sqlquery = @sqlquery + N' WHERE ' + @WhereAccess
	
	EXEC sp_executesql @sqlquery
END

GO
/****** Object:  StoredProcedure [cfg].[sp_getobjectaccessnames]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		ghost1908
-- Create date: 22.03.2021
-- Description:	Получить наименование доступа по пользователю
-- =============================================
CREATE PROCEDURE [cfg].[sp_getobjectaccessnames]
	@UserID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

    DECLARE @sqlquery nvarchar(MAX)

	SELECT @sqlquery =
	STUFF((SELECT N'UNION SELECT PhoenixDB.' + oa.ObjectName + '.NAME FROM PhoenixDB.' + oa.ObjectName + IIF(ua.ObjectValue IS NULL,
				 N' ',
				 N' WHERE PhoenixDB.' + oa.ObjectName + N'.ID = ''' + CAST(ua.ObjectValue AS nvarchar(256)) + N'''')
				
	FROM PhoenixDB.cfg.UserAccess ua
	JOIN PhoenixDB.cfg.ObjectAccess oa
		ON oa.Id = ua.ObjectId
	WHERE
		ua.UserId = @UserID
		FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 6, '')
	
	EXEC sp_executesql @sqlquery
END
GO
/****** Object:  StoredProcedure [cfg].[sp_gettabledatabyobjectname]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		ghost1908
-- Create date: 22.03.2021
-- Description:	Получить значения из таблицы БД по имени таблицы
-- =============================================
CREATE PROCEDURE [cfg].[sp_gettabledatabyobjectname]
	@table nvarchar(256)
AS
BEGIN
	SET NOCOUNT ON;

    DECLARE @sqlQuery nvarchar(512)

	SET @sqlQuery = N'SELECT * FROM ' + @table + N' ORDER BY 2'

	EXEC sp_executesql @sqlquery
END
GO
/****** Object:  StoredProcedure [dbo].[GetUserRights]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		ghost1908
-- Create date: 05.03.2020
-- Description:	Get User's rights
-- =============================================
CREATE PROCEDURE [dbo].[GetUserRights]
	@userId uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
		ua.UserId
		,ua.ObjectId
		,oa.ObjectName
		,ua.ObjectValue
	FROM cfg.UserAccess ua
	INNER JOIN cfg.ObjectAccess oa
		ON oa.Id = ua.ObjectId
	WHERE
		ua.UserId = @userId
END
GO
/****** Object:  StoredProcedure [dbo].[sp_checkuseraccess]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		ghost1908
-- Create date: 26.05.2020
-- Description:	Check user access
-- =============================================
CREATE PROCEDURE [dbo].[sp_checkuseraccess]
	@UserID nvarchar(256),
	@AccessType nvarchar(50),
	@ObjectAccess nvarchar(256),
	@ObjectID nvarchar(256)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @result bit
    DECLARE @accessCount int

	DECLARE @WhereAccess nvarchar(MAX)
	DECLARE @sqlquery nvarchar(MAX)

	DECLARE @tempValue nvarchar(256)

	SET @result = 0

	IF @AccessType = 'Person'
	BEGIN
		SET @AccessType = N'Address'
		SET @tempValue = @ObjectID
		SELECT @ObjectID = psn.ADDR_REG FROM Person psn WHERE psn.ID = @tempValue
	END

	SELECT @WhereAccess = STUFF(
		(SELECT IIF(ua.ObjectValue IS NULL,
					 N' OR',
					 N' OR PhoenixDB.dbo.' + oa.ObjectName + N'.ID = ''' + CAST(ua.ObjectValue AS nvarchar(256)) + N'''')
		FROM PhoenixDB.cfg.UserAccess ua
		JOIN PhoenixDB.cfg.ObjectAccess oa
			ON oa.Id = ua.ObjectId
		WHERE
			ua.UserId = @UserID
			AND oa.ObjectType = @AccessType
		FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 2, 2, '')

	IF @AccessType = 'Address'
	BEGIN
		SET @sqlquery = N'SELECT @accessCountOUT = COUNT(*) FROM ('
			+ N'SELECT
				PhoenixDB.dbo.District.ID AS DistrictID
				,PhoenixDB.dbo.Precinct.ID AS PrecinctID
				,PhoenixDB.dbo.Area.ID AS AreaID
				,PhoenixDB.dbo.Region.ID AS RegionID
				,PhoenixDB.dbo.CityInRegion.ID AS CityInRegionID
				,PhoenixDB.dbo.StreetInCity.ID AS StreetInCityID
			FROM PhoenixDB.dbo.Area
			JOIN PhoenixDB.dbo.Region
				ON PhoenixDB.dbo.Region.AREA_ID = PhoenixDB.dbo.Area.ID
			JOIN PhoenixDB.dbo.CityInRegion
				ON PhoenixDB.dbo.CityInRegion.REG_ID = PhoenixDB.dbo.Region.ID
			JOIN PhoenixDB.dbo.StreetInCity
				ON PhoenixDB.dbo.StreetInCity.CIR_ID = PhoenixDB.dbo.CityInRegion.ID
			JOIN PhoenixDB.dbo.TerritoryDescription
				ON PhoenixDB.dbo.TerritoryDescription.SIC_ID = PhoenixDB.dbo.StreetInCity.ID
			JOIN PhoenixDB.dbo.Precinct
				ON PhoenixDB.dbo.Precinct.ID = PhoenixDB.dbo.TerritoryDescription.PRCT_ID
			JOIN PhoenixDB.dbo.PrecinctInDistrict
				ON PhoenixDB.dbo.PrecinctInDistrict.PRCT_ID = PhoenixDB.dbo.Precinct.ID
			JOIN PhoenixDB.dbo.District
				ON PhoenixDB.dbo.District.ID = PhoenixDB.dbo.PrecinctInDistrict.DSTR_ID'

		IF @ObjectAccess = 'Address'
			SET @sqlquery = @sqlquery + N'
			JOIN PhoenixDB.dbo.Address
				ON PhoenixDB.dbo.Address.SIC_ID = PhoenixDB.dbo.StreetInCity.ID'

		IF @WhereAccess <> ''
			SET @sqlquery = @sqlquery + N'
			WHERE (' + @WhereAccess + N') AND PhoenixDB.dbo.' + @ObjectAccess + N'.ID = ''' + @ObjectID
		ELSE
			SET @sqlquery = @sqlquery + N'
			WHERE PhoenixDB.dbo.' + @ObjectAccess + N'.ID = ''' + @ObjectID
	
		SET @sqlquery = @sqlquery + N''') AS t'

		EXEC sp_executesql @sqlquery, N'@accessCountOUT int OUTPUT', @accessCountOUT = @accessCount OUTPUT

		IF @accessCount > 0
			SET @result = 1
	END

	SELECT @result AS IsAllow
END
GO
/****** Object:  StoredProcedure [dbo].[sp_createperson]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		ghost1908
-- Create date: 22.02.2021
-- Description:	Создание анкеты
-- =============================================
-- Modify date: 23.02.2021
-- Description:	Добавлено создание пустого PersonInfo
-- =============================================
-- Modify date: 02.04.2021
-- Description:	Добавлены поля USER_ID, DCREATE
-- =============================================
CREATE PROCEDURE [dbo].[sp_createperson] 
	@person dbo.TVP_Person READONLY,
	@userid uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @id uniqueidentifier

	SET @id = NEWID()

	INSERT INTO dbo.Person
	SELECT @id, LNAME, FNAME, MNAME, GENDER, 
		BDATE, VOTE, ADDR_REG, ADDR_REG_ROOM, ADDR_HOME, ADDR_HOME_ROOM,
		PHONE, HAS_TELEGRAM, HAS_VIBER, HAS_WHATSAPP,
		EMAIL, HAS_FACEBOOK, HAS_INSTAGRAM,
		IS_EMPLOYEE, IS_DEPUTY, IS_PARTY_MEMBER, IS_DELETED, @userid, GETDATE() FROM @person

	INSERT INTO dbo.PersonInfo VALUES ( @id, NULL, NULL, NULL, NULL )

	SELECT @id
END
GO
/****** Object:  StoredProcedure [dbo].[sp_finddublicates]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		ghost1908
-- Create date: 10.08.2021
-- Description:	Поиск дубликатов по Фамилии и Имени
-- =============================================
CREATE PROCEDURE [dbo].[sp_finddublicates] 
	@lastName nvarchar(200),
	@firstName nvarchar(100)
AS
BEGIN
	SET NOCOUNT ON;

    SELECT
		*
	FROM Person
	WHERE
		LOWER(CONCAT(LNAME,'$',FNAME)) LIKE LOWER(CONCAT(@lastName,'%$',@firstName,'%'))
	order by
		LNAME, FNAME, MNAME
END
GO
/****** Object:  StoredProcedure [dbo].[sp_getaddressaccessbyuser]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		ghost1908
-- Create date: 20.05.2020
-- Description:	Получить список ID указанной таблицы для указанного пользователя
-- =============================================
-- Modify date: 22.05.2020
-- Description: Изменен результирующий запрос
-- =============================================
CREATE PROCEDURE [dbo].[sp_getaddressaccessbyuser] 
	@UserID uniqueidentifier,
	@TableOutput nvarchar(500)
AS
BEGIN
	SET NOCOUNT ON;

    DECLARE @sqlquery nvarchar(MAX)
	DECLARE @WhereAccess nvarchar(MAX)

	SELECT @WhereAccess =
	STUFF(
	(SELECT IIF(ua.ObjectValue IS NULL,
				 N' OR',
				 N' OR PhoenixDB.' + oa.ObjectName + N'.ID = ''' + CAST(ua.ObjectValue AS nvarchar(256)) + N'''')
	FROM PhoenixDB.cfg.UserAccess ua
	JOIN PhoenixDB.cfg.ObjectAccess oa
		ON oa.Id = ua.ObjectId
	WHERE
		ua.UserId = @UserID
	FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 2, 2, '')
	
	SET @sqlquery = N'SELECT ' + @TableOutput + N'.ID AS ID FROM ' + @TableOutput
	IF @WhereAccess <> ''
		SET @sqlquery = @sqlquery + N' WHERE ' + @WhereAccess

	/*
	SET @sqlquery = N'SELECT t.' + @TableOutput + N'ID AS ID FROM ('
	+ N'SELECT
		PhoenixDB.dbo.District.ID AS DistrictID
		,PhoenixDB.dbo.Precinct.ID AS PrecinctID
		,PhoenixDB.dbo.Area.ID AS AreaID
		,PhoenixDB.dbo.Region.ID AS RegionID
		,PhoenixDB.dbo.City.ID AS CityID
		,PhoenixDB.dbo.Street.ID AS StreetID
	FROM PhoenixDB.dbo.Area
	JOIN PhoenixDB.dbo.Region
		ON PhoenixDB.dbo.Region.AREA_ID = PhoenixDB.dbo.Area.ID
	JOIN PhoenixDB.dbo.CityInRegion
		ON PhoenixDB.dbo.CityInRegion.REG_ID = PhoenixDB.dbo.Region.ID
	JOIN PhoenixDB.dbo.City
		ON PhoenixDB.dbo.City.ID = PhoenixDB.dbo.CityInRegion.CITY_ID
	JOIN PhoenixDB.dbo.StreetInCity
		ON PhoenixDB.dbo.StreetInCity.CIR_ID = PhoenixDB.dbo.CityInRegion.ID
	JOIN PhoenixDB.dbo.Street
		ON PhoenixDB.dbo.Street.ID = PhoenixDB.dbo.StreetInCity.STR_ID
	JOIN PhoenixDB.dbo.TerritoryDescription
		ON PhoenixDB.dbo.TerritoryDescription.SIC_ID = PhoenixDB.dbo.StreetInCity.ID
	JOIN PhoenixDB.dbo.Precinct
		ON PhoenixDB.dbo.Precinct.ID = PhoenixDB.dbo.TerritoryDescription.PRCT_ID
	JOIN PhoenixDB.dbo.PrecinctInDistrict
		ON PhoenixDB.dbo.PrecinctInDistrict.PRCT_ID = PhoenixDB.dbo.Precinct.ID
	JOIN PhoenixDB.dbo.District
		ON PhoenixDB.dbo.District.ID = PhoenixDB.dbo.PrecinctInDistrict.DSTR_ID'

	IF @WhereAccess <> ''
		SET @sqlquery = @sqlquery + N' WHERE ' + @WhereAccess

	SET @sqlquery = @sqlquery + N') as t GROUP BY t.' + @TableOutput + N'ID'
	
	*/

	EXEC sp_executesql @sqlquery
END
GO
/****** Object:  StoredProcedure [dbo].[sp_getarealist]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		ghost1908
-- Create date: 20.05.2020
-- Description:	Получить список Областей
-- =============================================
CREATE PROCEDURE [dbo].[sp_getarealist] 
	@UserID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

    CREATE TABLE #tmpAccess (ID uniqueidentifier)

	INSERT INTO #tmpAccess
	EXEC cfg.sp_getaccessbyuser @UserID, N'ter.Area'

	SELECT 
		area.*
	FROM ter.Area area
	WHERE area.ID IN (SELECT ID FROM #tmpAccess)
	ORDER BY
		area.NAME

	DROP TABLE #tmpAccess
END
GO
/****** Object:  StoredProcedure [dbo].[sp_getcityinregion]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		ghost1908
-- Create date: 28.05.2020
-- Description:	Получить список Городов в Районе для пользователя
-- =============================================
CREATE PROCEDURE [dbo].[sp_getcityinregion] 
	@UserID uniqueidentifier,
	@RegionID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

    --CREATE TABLE #tmpAccess (ID uniqueidentifier)

	--INSERT INTO #tmpAccess
	--EXEC dbo.sp_getaddressaccessbyuser @UserID, N'CityInRegion'

	SELECT 
		cir.*
		,city.NAME AS CITY_NAME
		,ct.ID AS CITYT_ID
		,ct.NAME AS CITYT_NAME
	FROM CityInRegion cir
	JOIN City city
		ON city.ID = cir.CITY_ID
	JOIN CityType ct
		ON ct.ID = city.CITYT_ID
	WHERE
		cir.REG_ID = @RegionID

	--DROP TABLE #tmpAccess
END
GO
/****** Object:  StoredProcedure [dbo].[sp_getcitylist]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		ghost1908
-- Create date: 21.05.2020
-- Description:	Получить список Городов для пользователя
-- =============================================
CREATE PROCEDURE [dbo].[sp_getcitylist] 
	@UserID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

    CREATE TABLE #tmpAccess (ID uniqueidentifier)

	INSERT INTO #tmpAccess
	EXEC dbo.sp_getaddressaccessbyuser @UserID, N'City'

	SELECT 
		city.*
	FROM City city
	JOIN #tmpAccess acl
		ON acl.ID = city.ID

	DROP TABLE #tmpAccess
END
GO
/****** Object:  StoredProcedure [dbo].[sp_getdistrictlist]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		ghost1908
-- Create date: 21.05.2020
-- Description:	Получить список Округов для пользователя
-- =============================================
CREATE PROCEDURE [dbo].[sp_getdistrictlist] 
	@UserID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

    CREATE TABLE #tmpAccess (ID uniqueidentifier)

	INSERT INTO #tmpAccess
	EXEC dbo.sp_getaddressaccessbyuser @UserID, N'District'

	SELECT 
		dstr.*
	FROM District dstr
	JOIN #tmpAccess acl
		ON acl.ID = dstr.ID

	DROP TABLE #tmpAccess
END
GO
/****** Object:  StoredProcedure [dbo].[sp_getfulladdress]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		ghost1908
-- Create date: 14.05.2020
-- Description:	Get full address by id
-- =============================================
-- Modify date: 25.05.2020
-- Description: Добавлен контроль доступа
-- =============================================
CREATE PROCEDURE [dbo].[sp_getfulladdress]
	@ID uniqueidentifier,
	@UserID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

	--CREATE TABLE #tmpAccess (ID uniqueidentifier)

	--INSERT INTO #tmpAccess
	--EXEC dbo.sp_getaddressaccessbyuser @UserID, N'Street'

	SELECT
		addr.ID
		,area.ID AS AREA_ID
		,area.NAME AS AREA_NAME
		,reg.ID AS REG_ID
		,reg.NAME AS REG_NAME
		,reg.IS_CITY_REGION
		,cir.ID AS CITY_IN_REG_ID
		,ctype.ID AS CITY_TYPE_ID
		,ctype.NAME AS CITY_TYPE_NAME
		,city.ID AS CITY_ID
		,city.NAME AS CITY_NAME
		,sic.ID AS STREET_IN_CITY_ID
		,stype.ID AS STREET_TYPE_ID
		,stype.NAME AS STREET_TYPE_NAME
		,strt.ID AS STREET_ID
		,strt.NAME AS STREET_NAME
		,bld.ID AS BLD_ID
		,bld.NUMBER AS BLD_NUMBER
		,addr.BLD_ISSUE
	FROM dbo.[Address] addr
	JOIN dbo.Building bld
		ON bld.ID = addr.BLD_ID
	JOIN dbo.StreetInCity sic
		ON sic.ID = addr.SIC_ID
	JOIN dbo.Street strt
		ON strt.ID = sic.STR_ID
	JOIN dbo.StreetType stype
		ON stype.ID = strt.STYPE_ID
	JOIN dbo.CityInRegion cir
		ON cir.ID = sic.CIR_ID
	JOIN dbo.City city
		ON city.ID = cir.CITY_ID
	JOIN dbo.CityType ctype
		ON ctype.ID = city.CITYT_ID
	JOIN dbo.Region reg
		ON reg.ID = cir.REG_ID
	JOIN dbo.Area area
		ON area.ID = reg.AREA_ID
	WHERE
		addr.ID = @ID
		--AND strt.ID IN (SELECT #tmpAccess.ID FROM #tmpAccess)

	--DROP TABLE #tmpAccess
END
GO
/****** Object:  StoredProcedure [dbo].[sp_getperson]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		ghost1908
-- Create date: 27.05.2020
-- Description:	Получить анкету
-- =============================================
CREATE PROCEDURE [dbo].[sp_getperson]
	@ID uniqueidentifier, 
	@UserID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

    --CREATE TABLE #tmpAccess (ID uniqueidentifier)

	--INSERT INTO #tmpAccess
	--EXEC dbo.sp_getaddressaccessbyuser @UserID, N'Street'

	SELECT 
		psn.*
	FROM Person psn
	WHERE
		psn.ID = @ID

	--DROP TABLE #tmpAccess
END
GO
/****** Object:  StoredProcedure [dbo].[sp_getpersonlist]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		ghost1908
-- Create date: 22.05.2020
-- Description:	Получить список партийцев для пользователя
-- =============================================
-- Modify date: 03.02.2021
-- Description: Добавлен контроль доступа по Громаде
-- TODO: надо контроль доступа по участку?
-- =============================================
-- Modify date: 12.03.2021
-- Description: Добавлен страничный фильтр
-- =============================================
-- Modify date: 12.10.2021
-- Description: Добавлен дополнительный фильтр
-- =============================================
CREATE PROCEDURE [dbo].[sp_getpersonlist] 
	@Filter dbo.TVP_PersonFilter READONLY,
	@Skip int,
	@Take int,
	@UserID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @TotalRows int

    CREATE TABLE #tmpAccess (ID uniqueidentifier)
	CREATE TABLE #cteResult ([ROW] bigint, [ID] uniqueidentifier, [MAX_ROW] bigint)

	INSERT INTO #tmpAccess
	EXEC cfg.sp_getaccessbyuser @UserID, N'ter.Community'

	--IF @Skip = 0 AND @Take = 0
	--BEGIN
	--	SELECT 
	--		psn.*
	--	FROM Person psn
	--	JOIN ter.[Address] addr ON addr.ID = psn.ADDR_REG
	--	JOIN ter.StreetInCity sic ON sic.ID = addr.SIC_ID
	--	JOIN ter.CityInCommunity cic ON cic.ID = sic.CIC_ID
	--	JOIN ter.CommunityInRegion cir ON cir.ID = cic.CIR_ID
	--	JOIN ter.RegionInArea ria ON ria.ID = cir.RIA_ID
	--	JOIN @Filter flt ON IIF(flt.AREA_ID IS NULL, ria.AREA_ID, flt.AREA_ID) = ria.AREA_ID
	--		AND IIF(flt.REG_ID IS NULL, ria.REG_ID, flt.REG_ID) = ria.REG_ID
	--		AND IIF(flt.CMN_ID IS NULL, cir.CMN_ID, flt.CMN_ID) = cir.CMN_ID
	--		AND IIF(flt.HAS_TELEGRAM IS NULL, psn.HAS_TELEGRAM, flt.HAS_TELEGRAM) = psn.HAS_TELEGRAM
	--		AND IIF(flt.HAS_VIBER IS NULL, psn.IS_DEPUTY, flt.HAS_VIBER) = psn.HAS_VIBER
	--		AND IIF(flt.HAS_WHATSAPP IS NULL, psn.HAS_WHATSAPP, flt.HAS_WHATSAPP) = psn.HAS_WHATSAPP
	--		AND IIF(flt.HAS_FACEBOOK IS NULL, psn.HAS_FACEBOOK, flt.HAS_FACEBOOK) = psn.HAS_FACEBOOK
	--		AND IIF(flt.HAS_INSTAGRAM IS NULL, psn.HAS_INSTAGRAM, flt.HAS_INSTAGRAM) = psn.HAS_INSTAGRAM
	--		AND IIF(flt.IS_EMPLOYEE IS NULL, psn.IS_EMPLOYEE, flt.IS_EMPLOYEE) = psn.IS_EMPLOYEE
	--		AND IIF(flt.IS_DEPUTY IS NULL, psn.IS_DEPUTY, flt.IS_DEPUTY) = psn.IS_DEPUTY
	--		AND IIF(flt.IS_PARTYMEMBER IS NULL, psn.IS_PARTY_MEMBER, flt.IS_PARTYMEMBER) = psn.IS_PARTY_MEMBER
	--		AND IIF(flt.IS_DELETED IS NULL, psn.IS_DELETED, flt.IS_DELETED) = psn.IS_DELETED
	--	WHERE
	--		cir.CMN_ID IN (SELECT ID FROM #tmpAccess)
	--	ORDER BY
	--		psn.LNAME, psn.FNAME, psn.MNAME
	--END
	--ELSE
	--BEGIN
	
		;WITH PersonEntities ([ROW], [ID], [MAX_ROW]) AS
		(
			SELECT ROW_NUMBER() OVER (ORDER BY psn.LNAME, psn.FNAME, psn.MNAME) AS [ROW],
				psn.ID,
				COUNT_BIG(*) OVER() AS [MAX_ROW]
			FROM dbo.Person psn
			JOIN ter.[Address] addr ON addr.ID = psn.ADDR_REG
			JOIN ter.StreetInCity sic ON sic.ID = addr.SIC_ID
			JOIN ter.CityInCommunity cic ON cic.ID = sic.CIC_ID
			JOIN ter.CommunityInRegion cir ON cir.ID = cic.CIR_ID
			JOIN ter.RegionInArea ria ON ria.ID = cir.RIA_ID
			JOIN @Filter flt ON IIF(flt.AREA_ID IS NULL, ria.AREA_ID, flt.AREA_ID) = ria.AREA_ID
				AND IIF(flt.REG_ID IS NULL, ria.ID, flt.REG_ID) = ria.ID
				AND IIF(flt.CMN_ID IS NULL, cir.ID, flt.CMN_ID) = cir.ID
				AND IIF(flt.HAS_TELEGRAM IS NULL, psn.HAS_TELEGRAM, flt.HAS_TELEGRAM) = psn.HAS_TELEGRAM
				AND IIF(flt.HAS_VIBER IS NULL, psn.HAS_VIBER, flt.HAS_VIBER) = psn.HAS_VIBER
				AND IIF(flt.HAS_WHATSAPP IS NULL, psn.HAS_WHATSAPP, flt.HAS_WHATSAPP) = psn.HAS_WHATSAPP
				AND IIF(flt.HAS_FACEBOOK IS NULL, psn.HAS_FACEBOOK, flt.HAS_FACEBOOK) = psn.HAS_FACEBOOK
				AND IIF(flt.HAS_INSTAGRAM IS NULL, psn.HAS_INSTAGRAM, flt.HAS_INSTAGRAM) = psn.HAS_INSTAGRAM
				AND IIF(flt.IS_EMPLOYEE IS NULL, psn.IS_EMPLOYEE, flt.IS_EMPLOYEE) = psn.IS_EMPLOYEE
				AND IIF(flt.IS_DEPUTY IS NULL, psn.IS_DEPUTY, flt.IS_DEPUTY) = psn.IS_DEPUTY
				AND IIF(flt.IS_PARTYMEMBER IS NULL, psn.IS_PARTY_MEMBER, flt.IS_PARTYMEMBER) = psn.IS_PARTY_MEMBER
				AND IIF(flt.IS_DELETED IS NULL, psn.IS_DELETED, flt.IS_DELETED) = psn.IS_DELETED
			WHERE
				cir.CMN_ID IN (SELECT ID FROM #tmpAccess)
		)
		INSERT INTO #cteResult
		SELECT [ROW], [ID], [MAX_ROW] FROM PersonEntities

		SELECT TOP 1 [MAX_ROW] FROM #cteResult

		SELECT 
			p.*
		FROM #cteResult pe
		INNER JOIN dbo.Person p ON p.ID = pe.ID
		WHERE pe.[ROW] > @Skip AND pe.[ROW] <= @Skip + IIF(@Take = 0, pe.MAX_ROW, @Take)
		ORDER BY
			p.LNAME, p.FNAME, p.MNAME
	--END
	
	DROP TABLE #tmpAccess
	DROP TABLE #cteResult
END
GO
/****** Object:  StoredProcedure [dbo].[sp_getpersonlistcount]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		ghost1908
-- Create date: 02.04.2021
-- Description:	Получить количество партийцев для пользователя
-- =============================================
CREATE PROCEDURE [dbo].[sp_getpersonlistcount] 
	@UserID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

    CREATE TABLE #tmpAccess (ID uniqueidentifier)

	INSERT INTO #tmpAccess
	EXEC cfg.sp_getaccessbyuser @UserID, N'ter.Community'
		
	SELECT 
		COUNT(psn.ID)
	FROM Person psn
	JOIN ter.[Address] addr ON addr.ID = psn.ADDR_REG
	JOIN ter.StreetInCity sic ON sic.ID = addr.SIC_ID
	JOIN ter.CityInCommunity cic ON cic.ID = sic.CIC_ID
	JOIN ter.CommunityInRegion cir ON cir.ID = cic.CIR_ID
	WHERE
		cir.CMN_ID IN (SELECT ID FROM #tmpAccess)
		
	DROP TABLE #tmpAccess
END
GO
/****** Object:  StoredProcedure [dbo].[sp_getprecinctlist]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		ghost1908
-- Create date: 21.05.2020
-- Description:	Получить список Избирательных участков для пользователя
-- =============================================
CREATE PROCEDURE [dbo].[sp_getprecinctlist] 
	@UserID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

    CREATE TABLE #tmpAccess (ID uniqueidentifier)

	INSERT INTO #tmpAccess
	EXEC dbo.sp_getaddressaccessbyuser @UserID, N'Precinct'

	SELECT 
		prct.*
	FROM Precinct prct
	JOIN #tmpAccess acl
		ON acl.ID = prct.ID

	DROP TABLE #tmpAccess
END
GO
/****** Object:  StoredProcedure [dbo].[sp_getregionlist]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		ghost1908
-- Create date: 21.05.2020
-- Description:	Получить список районов для пользователя
-- =============================================
CREATE PROCEDURE [dbo].[sp_getregionlist] 
	@UserID uniqueidentifier,
	@AreaID uniqueidentifier = NULL
AS
BEGIN
	SET NOCOUNT ON;

    CREATE TABLE #tmpAccess (ID uniqueidentifier)

	INSERT INTO #tmpAccess
	EXEC dbo.sp_getaddressaccessbyuser @UserID, N'Region'

	SELECT 
		reg.*
	FROM Region reg
	JOIN #tmpAccess acl
		ON acl.ID = reg.ID
	WHERE
		reg.AREA_ID = CASE WHEN @AreaID IS NULL THEN reg.AREA_ID ELSE @AreaID END
	ORDER BY
		reg.IS_CITY_REGION DESC, reg.NAME

	DROP TABLE #tmpAccess
END
GO
/****** Object:  StoredProcedure [dbo].[sp_getstreetincity]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		ghost1908
-- Create date: 29.05.2020
-- Description:	Получить список Улиц в Городе для пользователя
-- =============================================
CREATE PROCEDURE [dbo].[sp_getstreetincity] 
	@UserID uniqueidentifier,
	@CityID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

    --CREATE TABLE #tmpAccess (ID uniqueidentifier)

	--INSERT INTO #tmpAccess
	--EXEC dbo.sp_getaddressaccessbyuser @UserID, N'CityInRegion'

	SELECT 
		sic.*
		,strt.NAME AS STR_NAME
		,st.ID AS STRT_ID
		,st.NAME AS STRT_NAME
	FROM StreetInCity sic
	JOIN Street strt
		ON strt.ID = sic.STR_ID
	JOIN StreetType st
		ON st.ID = strt.STYPE_ID
	WHERE
		sic.CIR_ID = @CityID

	--DROP TABLE #tmpAccess
END
GO
/****** Object:  StoredProcedure [dbo].[sp_getstreetlist]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		ghost1908
-- Create date: 21.05.2020
-- Description:	Получить список Улиц для пользователя
-- =============================================
CREATE PROCEDURE [dbo].[sp_getstreetlist]
	@UserID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

    CREATE TABLE #tmpAccess (ID uniqueidentifier)

	INSERT INTO #tmpAccess
	EXEC dbo.sp_getaddressaccessbyuser @UserID, N'Street'

	SELECT 
		strt.*
	FROM Street strt
	JOIN #tmpAccess acl
		ON acl.ID = strt.ID

	DROP TABLE #tmpAccess
END
GO
/****** Object:  StoredProcedure [dbo].[sp_report_communityplans]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		ghost1908
-- Create date: 12.07.2021
-- Description:	Выполнение плана по ОТГ и пользователям
-- =============================================
-- Modify date: 10.08.2021
-- Description: Добавлены параметры периода
-- =============================================
CREATE PROCEDURE [dbo].[sp_report_communityplans]
	@startDate datetime2,
	@endDate datetime2
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @start_date datetime2
	DECLARE @end_date datetime2

    CREATE TABLE #plan
	(
		REG_ID uniqueidentifier,
		REG_NAME nvarchar(MAX),
		CMN_ID uniqueidentifier,
		CMN_NAME nvarchar(MAX),
		USER_NAME nvarchar(MAX),
		PSN_WEEK int,
		PSN_COUNT int,
		PSN_PLAN int
	)

	INSERT INTO #plan
	EXECUTE 
	('
		SELECT
			reg.ID AS REG_ID
			,reg.NAME AS REG_NAME
			,cmn.ID AS CMN_ID
			,cmn.NAME AS CMN_NAME
			,dbo.GetUserDisplayName(psn.USER_ID) AS USER_NAME
			,0 AS PSN_WEEK
			,COUNT(psn.ID) AS PSN_COUNT
			,0 AS PSN_PLAN
		FROM dbo.Person psn
		JOIN ter.Address addr ON addr.ID = psn.ADDR_REG
		JOIN ter.StreetInCity sic ON sic.ID = addr.SIC_ID
		JOIN ter.CityInCommunity cic ON cic.ID = sic.CIC_ID
		JOIN ter.CommunityInRegion cir ON cir.ID = cic.CIR_ID
		JOIN ter.Community cmn ON cmn.ID = cir.CMN_ID
		JOIN ter.RegionInArea ria ON ria.ID = cir.RIA_ID
		JOIN ter.Region reg ON reg.ID = ria.REG_ID
		WHERE (psn.DCREATE >= ''1970-01-01 00:00:00'' AND psn.DCREATE < ''' + @endDate + ''')
		GROUP BY 
			reg.ID
			,reg.NAME
			,cmn.ID
			,cmn.NAME
			,psn.USER_ID
		ORDER BY 2,4,5
	')

	INSERT INTO #plan
	EXECUTE 
	('
		SELECT
			reg.ID AS REG_ID
			,reg.NAME AS REG_NAME
			,cmn.ID AS CMN_ID
			,cmn.NAME AS CMN_NAME
			,dbo.GetUserDisplayName(psn.USER_ID) AS USER_NAME
			,COUNT(psn.ID) AS PSN_WEEK
			,0 AS PSN_COUNT
			,0 AS PSN_PLAN
		FROM dbo.Person psn
		JOIN ter.Address addr ON addr.ID = psn.ADDR_REG
		JOIN ter.StreetInCity sic ON sic.ID = addr.SIC_ID
		JOIN ter.CityInCommunity cic ON cic.ID = sic.CIC_ID
		JOIN ter.CommunityInRegion cir ON cir.ID = cic.CIR_ID
		JOIN ter.Community cmn ON cmn.ID = cir.CMN_ID
		JOIN ter.RegionInArea ria ON ria.ID = cir.RIA_ID
		JOIN ter.Region reg ON reg.ID = ria.REG_ID
		WHERE (psn.DCREATE >= ''' + @startDate + ''' AND psn.DCREATE < ''' + @endDate + ''')
		GROUP BY 
			reg.ID
			,reg.NAME
			,cmn.ID
			,cmn.NAME
			,psn.USER_ID
		ORDER BY 2,4,5
	')

	SELECT
		reg.NAME AS REG_NAME
		,cmn.NAME AS CMN_NAME
		,#plan.USER_NAME
		,SUM(ISNULL(#plan.PSN_WEEK, 0)) AS PSN_WEEK
		,SUM(ISNULL(#plan.PSN_COUNT, 0)) AS PSN_COUNT
		,CAST(cd.DataValue AS int) AS PSN_PLAN
	FROM #plan 
	RIGHT JOIN dbo.CustomData cd ON cd.ObjectID = #plan.CMN_ID
	RIGHT JOIN ter.Community cmn ON cmn.ID = cd.ObjectID
	RIGHT JOIN ter.CommunityInRegion cir ON cir.CMN_ID = cd.ObjectID
	RIGHT JOIN ter.RegionInArea ria ON ria.ID = cir.RIA_ID
	RIGHT JOIN ter.Region reg ON reg.ID = ria.REG_ID
	GROUP BY reg.NAME, cmn.NAME, #plan.USER_NAME, cd.DataValue

	DROP TABLE #plan
END
GO
/****** Object:  StoredProcedure [dbo].[sp_report_createdpersons]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		ghost1908
-- Create date: 22.06.2021
-- Description:	Отчет по внесенным анкетам
-- =============================================
CREATE PROCEDURE [dbo].[sp_report_createdpersons]
	@start_date datetime2,
	@end_date datetime2,
	@userId uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

	CREATE TABLE #tmpAccess (ID uniqueidentifier)

	INSERT INTO #tmpAccess
	EXEC cfg.sp_getaccessbyuser @UserID, N'ter.Community'

    SELECT
		reg.NAME AS REG_NAME
		,cmn.NAME AS CMN_NAME
		,dbo.GetUserDisplayName(psn.USER_ID) AS USER_NAME
		,COUNT(psn.ID) AS PSN_COUNT
		--,COUNT(psn.IS_EMPLOYEE)
		--,COUNT(psn.IS_PARTY_MEMBER)
		--,COUNT(psn.IS_DEPUTY)
	
		--,psn.DCREATE
	FROM dbo.Person psn
	JOIN ter.Address addr ON addr.ID = psn.ADDR_REG
	JOIN ter.StreetInCity sic ON sic.ID = addr.SIC_ID
	JOIN ter.CityInCommunity cic ON cic.ID = sic.CIC_ID
	JOIN ter.CommunityInRegion cir ON cir.ID = cic.CIR_ID
	JOIN ter.Community cmn ON cmn.ID = cir.CMN_ID
	JOIN ter.RegionInArea ria ON ria.ID = cir.RIA_ID
	JOIN ter.Region reg ON reg.ID = ria.REG_ID
	WHERE (psn.DCREATE >= @start_date AND psn.DCREATE < @end_date)
			AND cir.CMN_ID IN (SELECT ID FROM #tmpAccess)
	GROUP BY 
		reg.NAME
		,cmn.NAME
		,psn.USER_ID
	ORDER BY 1,2,3

	DROP TABLE #tmpAccess
END
GO
/****** Object:  StoredProcedure [dbo].[sp_report_personstate]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		ghost1908
-- Create date: 05.08.2021
-- Description:	Сводный отчет по статусам анкет
-- =============================================
CREATE PROCEDURE [dbo].[sp_report_personstate]
	@areaId uniqueidentifier,
	@regionId uniqueidentifier,
	@communityId uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

    CREATE TABLE #tmpResult
	(
		[Level] hierarchyid NOT NULL,
		[Name] nvarchar(100),
		[Count] bigint NOT NULL,
		[Style] nvarchar(100)
	)

	INSERT #tmpResult
	SELECT '/1/', N'Всього анкет', COUNT(p.ID), N'font-weight:bold;' FROM dbo.Person p
	INNER JOIN ter.[Address] addr ON addr.ID = p.ADDR_REG
	INNER JOIN ter.StreetInCity sic ON sic.ID = addr.SIC_ID
	INNER JOIN ter.CityInCommunity cic ON cic.ID = sic.CIC_ID
	INNER JOIN ter.CommunityInRegion cir ON cir.ID = cic.CIR_ID
	INNER JOIN ter.RegionInArea ria ON ria.ID = cir.RIA_ID
	WHERE
		cir.ID = CASE WHEN @communityId IS NULL THEN cir.ID ELSE @communityId END
		AND
		ria.ID = CASE WHEN @regionId IS NULL THEN ria.ID ELSE @regionId END
		AND
		ria.AREA_ID = CASE WHEN @areaId IS NULL THEN ria.AREA_ID ELSE @areaId END

	INSERT #tmpResult
	SELECT '/2/', N'Всього прихильників', COUNT(p.ID), N'font-weight:bold;' FROM dbo.Person p
	INNER JOIN ter.[Address] addr ON addr.ID = p.ADDR_REG
	INNER JOIN ter.StreetInCity sic ON sic.ID = addr.SIC_ID
	INNER JOIN ter.CityInCommunity cic ON cic.ID = sic.CIC_ID
	INNER JOIN ter.CommunityInRegion cir ON cir.ID = cic.CIR_ID
	INNER JOIN ter.RegionInArea ria ON ria.ID = cir.RIA_ID
	WHERE
		cir.ID = CASE WHEN @communityId IS NULL THEN cir.ID ELSE @communityId END
		AND
		ria.ID = CASE WHEN @regionId IS NULL THEN ria.ID ELSE @regionId END
		AND
		ria.AREA_ID = CASE WHEN @areaId IS NULL THEN ria.AREA_ID ELSE @areaId END
		AND ( p.IS_EMPLOYEE = 0 AND p.IS_PARTY_MEMBER = 0 AND p.IS_DEPUTY = 0 )

	INSERT #tmpResult
	SELECT '/3/', N'Всього співробітників', COUNT(p.ID), N'font-weight:bold;' FROM dbo.Person p 
	INNER JOIN ter.[Address] addr ON addr.ID = p.ADDR_REG
	INNER JOIN ter.StreetInCity sic ON sic.ID = addr.SIC_ID
	INNER JOIN ter.CityInCommunity cic ON cic.ID = sic.CIC_ID
	INNER JOIN ter.CommunityInRegion cir ON cir.ID = cic.CIR_ID
	INNER JOIN ter.RegionInArea ria ON ria.ID = cir.RIA_ID
	WHERE
		cir.ID = CASE WHEN @communityId IS NULL THEN cir.ID ELSE @communityId END
		AND
		ria.ID = CASE WHEN @regionId IS NULL THEN ria.ID ELSE @regionId END
		AND
		ria.AREA_ID = CASE WHEN @areaId IS NULL THEN ria.AREA_ID ELSE @areaId END
		AND ( p.IS_EMPLOYEE = 1 )

	INSERT #tmpResult
	SELECT '/3/1/', N'в т.ч. "член партії - ні" та "депутат - ні"', COUNT(p.ID), N'padding-left:2rem;' FROM dbo.Person p 
	INNER JOIN ter.[Address] addr ON addr.ID = p.ADDR_REG
	INNER JOIN ter.StreetInCity sic ON sic.ID = addr.SIC_ID
	INNER JOIN ter.CityInCommunity cic ON cic.ID = sic.CIC_ID
	INNER JOIN ter.CommunityInRegion cir ON cir.ID = cic.CIR_ID
	INNER JOIN ter.RegionInArea ria ON ria.ID = cir.RIA_ID
	WHERE
		cir.ID = CASE WHEN @communityId IS NULL THEN cir.ID ELSE @communityId END
		AND
		ria.ID = CASE WHEN @regionId IS NULL THEN ria.ID ELSE @regionId END
		AND
		ria.AREA_ID = CASE WHEN @areaId IS NULL THEN ria.AREA_ID ELSE @areaId END
		AND ( p.IS_EMPLOYEE = 1 AND p.IS_PARTY_MEMBER = 0 AND p.IS_DEPUTY = 0 )

	INSERT #tmpResult
	SELECT '/3/2/', N'в т.ч. "член партії - так" та "депутат - ні"', COUNT(p.ID), N'padding-left:2rem;' FROM dbo.Person p 
	INNER JOIN ter.[Address] addr ON addr.ID = p.ADDR_REG
	INNER JOIN ter.StreetInCity sic ON sic.ID = addr.SIC_ID
	INNER JOIN ter.CityInCommunity cic ON cic.ID = sic.CIC_ID
	INNER JOIN ter.CommunityInRegion cir ON cir.ID = cic.CIR_ID
	INNER JOIN ter.RegionInArea ria ON ria.ID = cir.RIA_ID
	WHERE
		cir.ID = CASE WHEN @communityId IS NULL THEN cir.ID ELSE @communityId END
		AND
		ria.ID = CASE WHEN @regionId IS NULL THEN ria.ID ELSE @regionId END
		AND
		ria.AREA_ID = CASE WHEN @areaId IS NULL THEN ria.AREA_ID ELSE @areaId END
		AND ( p.IS_EMPLOYEE = 1 AND p.IS_PARTY_MEMBER = 1 AND p.IS_DEPUTY = 0 )

	INSERT #tmpResult
	SELECT '/3/3/', N'в т.ч. "член партії - ні" та "депутат - так"', COUNT(p.ID), N'padding-left:2rem;' FROM dbo.Person p 
	INNER JOIN ter.[Address] addr ON addr.ID = p.ADDR_REG
	INNER JOIN ter.StreetInCity sic ON sic.ID = addr.SIC_ID
	INNER JOIN ter.CityInCommunity cic ON cic.ID = sic.CIC_ID
	INNER JOIN ter.CommunityInRegion cir ON cir.ID = cic.CIR_ID
	INNER JOIN ter.RegionInArea ria ON ria.ID = cir.RIA_ID
	WHERE
		cir.ID = CASE WHEN @communityId IS NULL THEN cir.ID ELSE @communityId END
		AND
		ria.ID = CASE WHEN @regionId IS NULL THEN ria.ID ELSE @regionId END
		AND
		ria.AREA_ID = CASE WHEN @areaId IS NULL THEN ria.AREA_ID ELSE @areaId END
		AND ( p.IS_EMPLOYEE = 1 AND p.IS_PARTY_MEMBER = 0 AND p.IS_DEPUTY = 1 )

	INSERT #tmpResult
	SELECT '/3/4/', N'в т.ч. "член партії - так" та "депутат - так"', COUNT(p.ID), N'padding-left:2rem;' FROM dbo.Person p 
	INNER JOIN ter.[Address] addr ON addr.ID = p.ADDR_REG
	INNER JOIN ter.StreetInCity sic ON sic.ID = addr.SIC_ID
	INNER JOIN ter.CityInCommunity cic ON cic.ID = sic.CIC_ID
	INNER JOIN ter.CommunityInRegion cir ON cir.ID = cic.CIR_ID
	INNER JOIN ter.RegionInArea ria ON ria.ID = cir.RIA_ID
	WHERE
		cir.ID = CASE WHEN @communityId IS NULL THEN cir.ID ELSE @communityId END
		AND
		ria.ID = CASE WHEN @regionId IS NULL THEN ria.ID ELSE @regionId END
		AND
		ria.AREA_ID = CASE WHEN @areaId IS NULL THEN ria.AREA_ID ELSE @areaId END
		AND ( p.IS_EMPLOYEE = 1 AND p.IS_PARTY_MEMBER = 1 AND p.IS_DEPUTY = 1 )

	INSERT #tmpResult
	SELECT '/4/', N'Всього членів партії', COUNT(p.ID), N'font-weight:bold;' FROM dbo.Person p 
	INNER JOIN ter.[Address] addr ON addr.ID = p.ADDR_REG
	INNER JOIN ter.StreetInCity sic ON sic.ID = addr.SIC_ID
	INNER JOIN ter.CityInCommunity cic ON cic.ID = sic.CIC_ID
	INNER JOIN ter.CommunityInRegion cir ON cir.ID = cic.CIR_ID
	INNER JOIN ter.RegionInArea ria ON ria.ID = cir.RIA_ID
	WHERE
		cir.ID = CASE WHEN @communityId IS NULL THEN cir.ID ELSE @communityId END
		AND
		ria.ID = CASE WHEN @regionId IS NULL THEN ria.ID ELSE @regionId END
		AND
		ria.AREA_ID = CASE WHEN @areaId IS NULL THEN ria.AREA_ID ELSE @areaId END
		AND ( p.IS_PARTY_MEMBER = 1 )

	INSERT #tmpResult
	SELECT '/4/1/', N'в т.ч. "співробітник - ні" та "депутат - ні"', COUNT(p.ID), N'padding-left:2rem;' FROM dbo.Person p 
	INNER JOIN ter.[Address] addr ON addr.ID = p.ADDR_REG
	INNER JOIN ter.StreetInCity sic ON sic.ID = addr.SIC_ID
	INNER JOIN ter.CityInCommunity cic ON cic.ID = sic.CIC_ID
	INNER JOIN ter.CommunityInRegion cir ON cir.ID = cic.CIR_ID
	INNER JOIN ter.RegionInArea ria ON ria.ID = cir.RIA_ID
	WHERE
		cir.ID = CASE WHEN @communityId IS NULL THEN cir.ID ELSE @communityId END
		AND
		ria.ID = CASE WHEN @regionId IS NULL THEN ria.ID ELSE @regionId END
		AND
		ria.AREA_ID = CASE WHEN @areaId IS NULL THEN ria.AREA_ID ELSE @areaId END
		AND ( p.IS_EMPLOYEE = 0 AND p.IS_PARTY_MEMBER = 1 AND p.IS_DEPUTY = 0 )

	INSERT #tmpResult
	SELECT '/4/2/', N'в т.ч. "співробітник - так" та "депутат - ні"', COUNT(p.ID), N'padding-left:2rem;' FROM dbo.Person p 
	INNER JOIN ter.[Address] addr ON addr.ID = p.ADDR_REG
	INNER JOIN ter.StreetInCity sic ON sic.ID = addr.SIC_ID
	INNER JOIN ter.CityInCommunity cic ON cic.ID = sic.CIC_ID
	INNER JOIN ter.CommunityInRegion cir ON cir.ID = cic.CIR_ID
	INNER JOIN ter.RegionInArea ria ON ria.ID = cir.RIA_ID
	WHERE
		cir.ID = CASE WHEN @communityId IS NULL THEN cir.ID ELSE @communityId END
		AND
		ria.ID = CASE WHEN @regionId IS NULL THEN ria.ID ELSE @regionId END
		AND
		ria.AREA_ID = CASE WHEN @areaId IS NULL THEN ria.AREA_ID ELSE @areaId END
		AND ( p.IS_EMPLOYEE = 1 AND p.IS_PARTY_MEMBER = 1 AND p.IS_DEPUTY = 0 )

	INSERT #tmpResult
	SELECT '/4/3/', N'в т.ч. "співробітник - ні" та "депутат - так"', COUNT(p.ID), N'padding-left:2rem;' FROM dbo.Person p 
	INNER JOIN ter.[Address] addr ON addr.ID = p.ADDR_REG
	INNER JOIN ter.StreetInCity sic ON sic.ID = addr.SIC_ID
	INNER JOIN ter.CityInCommunity cic ON cic.ID = sic.CIC_ID
	INNER JOIN ter.CommunityInRegion cir ON cir.ID = cic.CIR_ID
	INNER JOIN ter.RegionInArea ria ON ria.ID = cir.RIA_ID
	WHERE
		cir.ID = CASE WHEN @communityId IS NULL THEN cir.ID ELSE @communityId END
		AND
		ria.ID = CASE WHEN @regionId IS NULL THEN ria.ID ELSE @regionId END
		AND
		ria.AREA_ID = CASE WHEN @areaId IS NULL THEN ria.AREA_ID ELSE @areaId END
		AND ( p.IS_EMPLOYEE = 0 AND p.IS_PARTY_MEMBER = 1 AND p.IS_DEPUTY = 1 )

	INSERT #tmpResult
	SELECT '/4/4/', N'в т.ч. "співробітник - так" та "депутат - так"', COUNT(p.ID), N'padding-left:2rem;' FROM dbo.Person p 
	INNER JOIN ter.[Address] addr ON addr.ID = p.ADDR_REG
	INNER JOIN ter.StreetInCity sic ON sic.ID = addr.SIC_ID
	INNER JOIN ter.CityInCommunity cic ON cic.ID = sic.CIC_ID
	INNER JOIN ter.CommunityInRegion cir ON cir.ID = cic.CIR_ID
	INNER JOIN ter.RegionInArea ria ON ria.ID = cir.RIA_ID
	WHERE
		cir.ID = CASE WHEN @communityId IS NULL THEN cir.ID ELSE @communityId END
		AND
		ria.ID = CASE WHEN @regionId IS NULL THEN ria.ID ELSE @regionId END
		AND
		ria.AREA_ID = CASE WHEN @areaId IS NULL THEN ria.AREA_ID ELSE @areaId END
		AND ( p.IS_EMPLOYEE = 1 AND p.IS_PARTY_MEMBER = 1 AND p.IS_DEPUTY = 1 )

	INSERT #tmpResult
	SELECT '/5/', N'Всього депутатів', COUNT(p.ID), N'font-weight:bold;' FROM dbo.Person p 
	INNER JOIN ter.[Address] addr ON addr.ID = p.ADDR_REG
	INNER JOIN ter.StreetInCity sic ON sic.ID = addr.SIC_ID
	INNER JOIN ter.CityInCommunity cic ON cic.ID = sic.CIC_ID
	INNER JOIN ter.CommunityInRegion cir ON cir.ID = cic.CIR_ID
	INNER JOIN ter.RegionInArea ria ON ria.ID = cir.RIA_ID
	WHERE
		cir.ID = CASE WHEN @communityId IS NULL THEN cir.ID ELSE @communityId END
		AND
		ria.ID = CASE WHEN @regionId IS NULL THEN ria.ID ELSE @regionId END
		AND
		ria.AREA_ID = CASE WHEN @areaId IS NULL THEN ria.AREA_ID ELSE @areaId END
		AND ( p.IS_DEPUTY = 1 )

	INSERT #tmpResult
	SELECT '/5/1/', N'в т.ч. "співробітник - ні" та "член партії - ні"', COUNT(p.ID), N'padding-left:2rem;' FROM dbo.Person p 
	INNER JOIN ter.[Address] addr ON addr.ID = p.ADDR_REG
	INNER JOIN ter.StreetInCity sic ON sic.ID = addr.SIC_ID
	INNER JOIN ter.CityInCommunity cic ON cic.ID = sic.CIC_ID
	INNER JOIN ter.CommunityInRegion cir ON cir.ID = cic.CIR_ID
	INNER JOIN ter.RegionInArea ria ON ria.ID = cir.RIA_ID
	WHERE
		cir.ID = CASE WHEN @communityId IS NULL THEN cir.ID ELSE @communityId END
		AND
		ria.ID = CASE WHEN @regionId IS NULL THEN ria.ID ELSE @regionId END
		AND
		ria.AREA_ID = CASE WHEN @areaId IS NULL THEN ria.AREA_ID ELSE @areaId END
		AND ( p.IS_EMPLOYEE = 0 AND p.IS_PARTY_MEMBER = 0 AND p.IS_DEPUTY = 1 )

	INSERT #tmpResult
	SELECT '/5/2/', N'в т.ч. "співробітник - так" та "член партії - ні"', COUNT(p.ID), N'padding-left:2rem;' FROM dbo.Person p 
	INNER JOIN ter.[Address] addr ON addr.ID = p.ADDR_REG
	INNER JOIN ter.StreetInCity sic ON sic.ID = addr.SIC_ID
	INNER JOIN ter.CityInCommunity cic ON cic.ID = sic.CIC_ID
	INNER JOIN ter.CommunityInRegion cir ON cir.ID = cic.CIR_ID
	INNER JOIN ter.RegionInArea ria ON ria.ID = cir.RIA_ID
	WHERE
		cir.ID = CASE WHEN @communityId IS NULL THEN cir.ID ELSE @communityId END
		AND
		ria.ID = CASE WHEN @regionId IS NULL THEN ria.ID ELSE @regionId END
		AND
		ria.AREA_ID = CASE WHEN @areaId IS NULL THEN ria.AREA_ID ELSE @areaId END
		AND ( p.IS_EMPLOYEE = 1 AND p.IS_PARTY_MEMBER = 0 AND p.IS_DEPUTY = 1 )

	INSERT #tmpResult
	SELECT '/5/3/', N'в т.ч. "співробітник - ні" та "член партії - так"', COUNT(p.ID), N'padding-left:2rem;' FROM dbo.Person p 
	INNER JOIN ter.[Address] addr ON addr.ID = p.ADDR_REG
	INNER JOIN ter.StreetInCity sic ON sic.ID = addr.SIC_ID
	INNER JOIN ter.CityInCommunity cic ON cic.ID = sic.CIC_ID
	INNER JOIN ter.CommunityInRegion cir ON cir.ID = cic.CIR_ID
	INNER JOIN ter.RegionInArea ria ON ria.ID = cir.RIA_ID
	WHERE
		cir.ID = CASE WHEN @communityId IS NULL THEN cir.ID ELSE @communityId END
		AND
		ria.ID = CASE WHEN @regionId IS NULL THEN ria.ID ELSE @regionId END
		AND
		ria.AREA_ID = CASE WHEN @areaId IS NULL THEN ria.AREA_ID ELSE @areaId END
		AND ( p.IS_EMPLOYEE = 0 AND p.IS_PARTY_MEMBER = 1 AND p.IS_DEPUTY = 1 )

	INSERT #tmpResult
	SELECT '/5/4/', N'в т.ч. "співробітник - так" та "член партії - так"', COUNT(p.ID), N'padding-left:2rem;' FROM dbo.Person p 
	INNER JOIN ter.[Address] addr ON addr.ID = p.ADDR_REG
	INNER JOIN ter.StreetInCity sic ON sic.ID = addr.SIC_ID
	INNER JOIN ter.CityInCommunity cic ON cic.ID = sic.CIC_ID
	INNER JOIN ter.CommunityInRegion cir ON cir.ID = cic.CIR_ID
	INNER JOIN ter.RegionInArea ria ON ria.ID = cir.RIA_ID
	WHERE
		cir.ID = CASE WHEN @communityId IS NULL THEN cir.ID ELSE @communityId END
		AND
		ria.ID = CASE WHEN @regionId IS NULL THEN ria.ID ELSE @regionId END
		AND
		ria.AREA_ID = CASE WHEN @areaId IS NULL THEN ria.AREA_ID ELSE @areaId END
		AND ( p.IS_EMPLOYEE = 1 AND p.IS_PARTY_MEMBER = 1 AND p.IS_DEPUTY = 1 )

	SELECT * FROM #tmpResult

	DROP TABLE #tmpResult
END
GO
/****** Object:  StoredProcedure [dbo].[sp_updateperson]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		ghost1908
-- Create date: 23.02.2021
-- Description:	Обновление данных анкеты
-- =============================================
CREATE PROCEDURE [dbo].[sp_updateperson] 
	@person dbo.TVP_Person READONLY
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @result int

	SET @result = 1

	IF EXISTS(SELECT * FROM dbo.Person p JOIN @person d ON d.ID = p.ID)
	BEGIN
		UPDATE psn
		SET
			psn.LNAME = p.LNAME
			,psn.FNAME = p.FNAME
			,psn.MNAME = p.MNAME
			,psn.GENDER = p.GENDER
			,psn.BDATE = p.BDATE
			,psn.VOTE = p.VOTE
			,psn.ADDR_REG = p.ADDR_REG
			,psn.ADDR_REG_ROOM = p.ADDR_REG_ROOM
			,psn.ADDR_HOME = p.ADDR_HOME
			,psn.ADDR_HOME_ROOM = p.ADDR_HOME_ROOM
			,psn.PHONE = p.PHONE
			,psn.HAS_TELEGRAM = p.HAS_TELEGRAM
			,psn.HAS_VIBER = p.HAS_VIBER
			,psn.HAS_WHATSAPP = p.HAS_WHATSAPP
			,psn.EMAIL = p.EMAIL
			,psn.HAS_FACEBOOK = p.HAS_FACEBOOK
			,psn.HAS_INSTAGRAM = p.HAS_INSTAGRAM
			,psn.IS_EMPLOYEE = p.IS_EMPLOYEE
			,psn.IS_DEPUTY = p.IS_DEPUTY
			,psn.IS_PARTY_MEMBER = p.IS_PARTY_MEMBER
			,psn.IS_DELETED = p.IS_DELETED
		FROM dbo.Person psn
		JOIN @person p ON p.ID = psn.ID

		SET @result = 0
	END
	
	SELECT @result
END
GO
/****** Object:  StoredProcedure [dbo].[sp_updatepersoninfo]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		ghost1908
-- Create date: 23.02.2021
-- Description:	Обновление доп. данных анкеты
-- =============================================
CREATE PROCEDURE [dbo].[sp_updatepersoninfo] 
	@personInfo dbo.TVP_PersonInfo READONLY
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @result int

	SET @result = 1

	IF EXISTS(SELECT * FROM dbo.PersonInfo p JOIN @personInfo d ON d.PSN_ID = p.PSN_ID)
	BEGIN
		UPDATE psn
		SET
			psn.PASS_SERIES = p.PASS_SERIES
			,psn.PASS_NUMBER = p.PASS_NUMBER
			,psn.PASS_ISSUE = p.PASS_ISSUE
			,psn.TAX_NUMBER = p.TAX_NUMBER
		FROM dbo.PersonInfo psn
		JOIN @personInfo p ON p.PSN_ID = psn.PSN_ID

		SET @result = 0
	END
	
	SELECT @result
END
GO
/****** Object:  StoredProcedure [dbo].[spAddCity]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		ghost1908
-- Create date: 06.03.2020
-- Description:	Добавление Города с возвратом ID
-- =============================================
CREATE PROCEDURE [dbo].[spAddCity] 
	@city_name nvarchar(200),
	@type_name nvarchar(5),
	@city_id uniqueidentifier OUTPUT,
	@type_id uniqueidentifier OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

    IF EXISTS(SELECT * FROM CityType WHERE NAME = @type_name)
		SET @type_id = (SELECT ID FROM CityType WHERE NAME = @type_name)
	ELSE
	BEGIN
		SET @type_id = NEWID()
		INSERT INTO CityType VALUES (@type_id, @type_name)
	END

	IF EXISTS(SELECT * FROM City WHERE NAME = @city_name AND CITYT_ID = @type_id)
		SET @city_id = (SELECT ID FROM City WHERE NAME = @city_name AND CITYT_ID = @type_id)
	ELSE
	BEGIN
		SET @city_id = NEWID()
		INSERT INTO City VALUES (@city_id, @type_id, @city_name)
	END
END
GO
/****** Object:  StoredProcedure [dbo].[spAddCityInRegion]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		ghost1908
-- Create date: 11.03.2020
-- Description:	Добавление Города в Район с возвратом ID
-- =============================================
CREATE PROCEDURE [dbo].[spAddCityInRegion] 
	@city_id uniqueidentifier,
	@region_id uniqueidentifier,
	@id uniqueidentifier OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

    IF EXISTS(SELECT * FROM CityInRegion WHERE CITY_ID = @city_id AND REG_ID = @region_id)
		SET @id = (SELECT ID FROM CityInRegion WHERE CITY_ID = @city_id AND REG_ID = @region_id)
	ELSE
	BEGIN
		SET @id = NEWID()
		INSERT INTO CityInRegion VALUES (@id, @region_id, @city_id)
	END	
END

GO
/****** Object:  StoredProcedure [dbo].[spAddStreet]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		ghost1908
-- Create date: 12.03.2020
-- Description:	Добавление Улицы с возвратом ID
-- =============================================
CREATE PROCEDURE [dbo].[spAddStreet] 
	@street_name nvarchar(200),
	@type_name nvarchar(10),
	@id uniqueidentifier OUTPUT,
	@type_id uniqueidentifier OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

    IF EXISTS(SELECT * FROM StreetType WHERE NAME = @type_name)
		SET @type_id = (SELECT ID FROM StreetType WHERE NAME = @type_name)
	ELSE
	BEGIN
		SET @type_id = NEWID()
		INSERT INTO StreetType VALUES (@type_id, @type_name)
	END

	IF EXISTS(SELECT * FROM Street WHERE NAME = @street_name AND STYPE_ID = @type_id)
		SET @id = (SELECT ID FROM Street WHERE NAME = @street_name AND STYPE_ID = @type_id)
	ELSE
	BEGIN
		SET @id = NEWID()
		INSERT INTO Street VALUES (@id, @type_id, @street_name)
	END
END

GO
/****** Object:  StoredProcedure [dbo].[spAddStreetInCity]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		ghost1908
-- Create date: 12.03.2020
-- Description:	Добавление Улицы в Город с возвратом ID
-- =============================================
CREATE PROCEDURE [dbo].[spAddStreetInCity] 
	@street_id uniqueidentifier,
	@cir_id uniqueidentifier,
	@id uniqueidentifier OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

    IF EXISTS(SELECT * FROM StreetInCity WHERE STR_ID = @street_id AND CIR_ID = @cir_id)
		SET @id = (SELECT ID FROM StreetInCity WHERE STR_ID = @street_id AND CIR_ID = @cir_id)
	ELSE
	BEGIN
		SET @id = NEWID()
		INSERT INTO StreetInCity VALUES (@id, @cir_id, @street_id)
	END	
END


GO
/****** Object:  StoredProcedure [elc].[sp_cleardata]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		ghost1908
-- Create date: 22.10.2020
-- Description:	Очистка данных открытия, явки, протоколов
-- =============================================
CREATE PROCEDURE [elc].[sp_cleardata] 
	@dataType nvarchar(10)
AS
BEGIN
	SET NOCOUNT ON;

	IF @dataType = N'all'
	BEGIN
		UPDATE elc.ElectionPrecinctInfo
		SET BULLETIN_RECV = NULL

		UPDATE elc.ElectionPrecinct
		SET PRCT_OPENED = NULL, PRCT_VOTERS = NULL, PRCT_NOTOPEN_CAUSE = NULL

		UPDATE elc.Turnout
		SET TO_VOTERS = 0

		UPDATE elc.ProtocolDetail
		SET PRTL_ITEM_VALUE = NULL

		UPDATE elc.Protocol
		SET PRTL_STATUS_ID = 0
	END

	IF @dataType = N'open'
	BEGIN
		UPDATE elc.ElectionPrecinctInfo
		SET BULLETIN_RECV = NULL

		UPDATE elc.ElectionPrecinct
		SET PRCT_OPENED = NULL, PRCT_VOTERS = NULL, PRCT_NOTOPEN_CAUSE = NULL
	END

	IF @dataType = N'turnout'
	BEGIN
		UPDATE elc.Turnout
		SET TO_VOTERS = 0
	END

	IF @dataType = N'protocol'
	BEGIN
		UPDATE elc.ProtocolDetail
		SET PRTL_ITEM_VALUE = NULL

		UPDATE elc.Protocol
		SET PRTL_STATUS_ID = 0
	END
END
GO
/****** Object:  StoredProcedure [elc].[sp_district_by_council]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [elc].[sp_district_by_council]
	@elcID uniqueidentifier,
	@councilID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
		dic.ID AS DIC_ID,
		dic.CNCL_ID AS CNCL_ID,
		dstr.ID AS DSTR_ID,
		dt.NAME + N' № ' + dstr.NUMBER AS DSTR_NAME
	FROM elc.DistrictInCouncil dic
	JOIN ter.District dstr ON dstr.ID = dic.DSTR_ID
	JOIN ter.DistrictType dt ON dt.ID = dstr.DSTRT_ID
	WHERE dic.CNCL_ID = @councilID
	ORDER BY dstr.NUMBER
END
GO
/****** Object:  StoredProcedure [elc].[sp_get_results_community]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [elc].[sp_get_results_community]
	@elc_id uniqueidentifier,
	@cmn_reg_id uniqueidentifier,
	@prct_id uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

    create table #candidates
	(
		CND_INFO_ID uniqueidentifier,
		CNCL_ID uniqueidentifier,
		ITEM_VALUE bigint NULL
	)

	insert into #candidates
	select
		pd.CND_INFO_ID,
		p.CNCL_ID,
		sum(isnull(pd.PRTL_ITEM_VALUE,0))
	from elc.ProtocolDetail pd
	join elc.ProtocolTemplate pt on pt.ID = pd.PRTL_TMPL_ID
	join elc.Protocol p on p.ID = pd.PRTL_ID
	join elc.ElectionPrecinct ep on ep.ID = p.ELC_PRCT_ID
	join ter.PrecinctInCommunity pic on pic.ID = ep.PRCT_CMN_ID
	where pt.ITEM_ORDER = 12
		and pic.CMIR_ID = case when @cmn_reg_id is null then pic.CMIR_ID else @cmn_reg_id END
		and pic.PRCT_ID = case when @prct_id is null then pic.PRCT_ID else @prct_id end
	group by pd.CND_INFO_ID,
		p.CNCL_ID
	order by 2,1

	select
		ci.CND_ORDER,
		cnd.CND_FULLNAME,
		--tmp.CNCL_ID,
		ct.WATCH_ORDER,
		cncl.NAME + ' ' + LOWER(ct.NAME) AS CNCL_NAME,
		tmp.ITEM_VALUE
	from elc.Candidate cnd
	join elc.CandidateInfo ci on ci.CND_ID = cnd.ID
	join #candidates tmp on tmp.CND_INFO_ID = ci.ID
	join elc.Council cncl on cncl.ID = tmp.CNCL_ID
	join elc.CouncilType ct on ct.ID = cncl.CNCLT_ID
	order by ct.WATCH_ORDER, ci.CND_ORDER

	drop table #candidates
END
GO
/****** Object:  StoredProcedure [elc].[sp_getareadistrictresult]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [elc].[sp_getareadistrictresult]
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    declare @councilID uniqueidentifier

set @councilID = 'A49932CE-97E6-4C3B-B7A5-402937E9EEDE'

SELECT
	grp.PRTL_ITEM_ORDER
	,grp.CND_NAME
	,SUM(grp.DSTR1) AS DST_1
	,SUM(grp.DSTR2) AS DST_2
	,SUM(grp.DSTR3) AS DST_3
	,SUM(grp.DSTR4) AS DST_4
	,SUM(grp.DSTR5) AS DST_5
	,SUM(grp.DSTR6) AS DST_6
	,SUM(grp.DSTR7) AS DST_7
	,SUM(grp.DSTR8) AS DST_8
FROM (
SELECT
	obl.PRTL_ITEM_ORDER,
	obl.CND_NAME,
	CASE WHEN obl.NUMBER = 1 THEN obl.PRTL_ITEM_VALUE ELSE 0 END AS DSTR1,
	CASE WHEN obl.NUMBER = 2 THEN obl.PRTL_ITEM_VALUE ELSE 0 END AS DSTR2,
	CASE WHEN obl.NUMBER = 3 THEN obl.PRTL_ITEM_VALUE ELSE 0 END AS DSTR3,
	CASE WHEN obl.NUMBER = 4 THEN obl.PRTL_ITEM_VALUE ELSE 0 END AS DSTR4,
	CASE WHEN obl.NUMBER = 5 THEN obl.PRTL_ITEM_VALUE ELSE 0 END AS DSTR5,
	CASE WHEN obl.NUMBER = 6 THEN obl.PRTL_ITEM_VALUE ELSE 0 END AS DSTR6,
	CASE WHEN obl.NUMBER = 7 THEN obl.PRTL_ITEM_VALUE ELSE 0 END AS DSTR7,
	CASE WHEN obl.NUMBER = 8 THEN obl.PRTL_ITEM_VALUE ELSE 0 END AS DSTR8
FROM (
	SELECT
		dstr.NUMBER
		,tmpl.ITEM_ORDER AS PRTL_ITEM_ORDER
		,cnd.CND_FULLNAME AS CND_NAME
		,SUM(ISNULL(pd.PRTL_ITEM_VALUE,0)) AS PRTL_ITEM_VALUE
	FROM elc.ProtocolDetail pd
	JOIN elc.ProtocolTemplate tmpl
		ON tmpl.ID = pd.PRTL_TMPL_ID
	JOIN elc.ProtocolItem item
		ON item.ID = tmpl.PRTL_ITEM_ID
	JOIN elc.Protocol prtl
		ON prtl.ID = pd.PRTL_ID
	JOIN elc.ElectionPrecinctInDistrict epd
		ON epd.ELC_PRCT_ID = prtl.ELC_PRCT_ID
	JOIN elc.DistrictInCouncil dic
		ON dic.ID = epd.DSTR_CNCL_ID
	JOIN ter.District dstr
		ON dstr.ID = dic.DSTR_ID
	LEFT JOIN elc.CandidateInfo ci
		ON ci.ID = pd.CND_INFO_ID
	LEFT JOIN elc.Candidate cnd
		ON cnd.ID = ci.CND_ID
	WHERE
		prtl.CNCL_ID = @councilID
		AND dic.CNCL_ID = @councilID
		AND tmpl.ITEM_ORDER IN ( 3, 9, 12 )
	GROUP BY
		dstr.NUMBER
		,tmpl.ITEM_ORDER
		,cnd.CND_FULLNAME
) obl ) grp
GROUP BY
	grp.PRTL_ITEM_ORDER
	,grp.CND_NAME
ORDER BY
	grp.PRTL_ITEM_ORDER
END
GO
/****** Object:  StoredProcedure [elc].[sp_getelectionprecinctlistforedit]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		ghost1908
-- Create date: 20.08.2020
-- Description:	Получить участок для редактирования открытия
-- =============================================
-- Modify date: 
-- Description: 
-- =============================================
CREATE PROCEDURE [elc].[sp_getelectionprecinctlistforedit]
	@eprctID uniqueidentifier,
	@UserID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

	CREATE TABLE #tmpAccess (ID uniqueidentifier)

	INSERT INTO #tmpAccess
	EXEC cfg.sp_getaccessbyuser @UserID, N'ter.Precinct'

    SELECT
		eprct.ID AS ELC_PRCT_ID
		,prct.NUMBER AS PRCT_NUMBER
		,reg.NAME AS REG_NAME
		,cmn.NAME AS CMN_NAME
		,eprct.PRCT_OPENED
		,eprct.PRCT_NOTOPEN_CAUSE
		,eprct.PRCT_VOTERS
	FROM elc.ElectionPrecinct eprct
	JOIN ter.PrecinctInCommunity prctcmn
		ON prctcmn.ID = eprct.PRCT_CMN_ID
	JOIN ter.Precinct prct
		ON prct.ID = prctcmn.PRCT_ID
	JOIN ter.CommunityInRegion cmir
		ON cmir.ID = prctcmn.CMIR_ID
	JOIN ter.Community cmn
		ON cmn.ID = cmir.CMN_ID
	JOIN ter.RegionInArea ria
		ON ria.ID = cmir.RIA_ID
	JOIN ter.Region reg
		ON reg.ID = ria.REG_ID
	WHERE
		prct.ID IN (SELECT ID FROM #tmpAccess)
		AND eprct.ID = @eprctID

	SELECT
		eprctinfo.ID AS ELC_PRCT_INFO_ID
		,cnclt.NAME AS CNCL_NAME
		,eprctinfo.BULLETIN_RECV AS BLT_RECV
	FROM elc.ElectionPrecinctInfo eprctinfo
	JOIN elc.Council cncl
		ON cncl.ID = eprctinfo.CNCL_ID
	JOIN elc.CouncilType cnclt
		ON cnclt.ID = cncl.CNCLT_ID
	JOIN elc.ElectionPrecinct eprct
		ON eprct.ID = eprctinfo.ELC_PRCT_ID
	JOIN ter.PrecinctInCommunity prctcmn
		ON prctcmn.ID = eprct.PRCT_CMN_ID
	WHERE
		prctcmn.PRCT_ID IN (SELECT ID FROM #tmpAccess)
		AND eprctinfo.ELC_PRCT_ID = @eprctID
	ORDER BY
		cnclt.WATCH_ORDER ASC

	DROP TABLE #tmpAccess
END
GO
/****** Object:  StoredProcedure [elc].[sp_getelectionprecinctlistforopen]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		ghost1908
-- Create date: 19.08.2020
-- Description:	Список избирательных участков для открытия
-- =============================================
-- Modify date: 16.10.2020
-- Description: Добавлен вывод колонки громады
-- =============================================
CREATE PROCEDURE [elc].[sp_getelectionprecinctlistforopen]
	@elcID uniqueidentifier,
	@UserID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

	CREATE TABLE #tmpAccess (ID uniqueidentifier)

	INSERT INTO #tmpAccess
	EXEC cfg.sp_getaccessbyuser @UserID, N'ter.Precinct'

    SELECT
		eprct.ID AS ELC_PRCT_ID
		,prct.NUMBER AS PRCT_NUMBER
		,reg.NAME AS REG_NAME
		,cmn.NAME AS CMN_NAME
		,eprct.PRCT_OPENED
		,eprct.PRCT_NOTOPEN_CAUSE
		,eprct.PRCT_VOTERS
	FROM elc.ElectionPrecinct eprct
	JOIN ter.PrecinctInCommunity prctcmn
		ON prctcmn.ID = eprct.PRCT_CMN_ID
	JOIN ter.Precinct prct
		ON prct.ID = prctcmn.PRCT_ID
	JOIN ter.CommunityInRegion cmir
		ON cmir.ID = prctcmn.CMIR_ID
	JOIN ter.Community cmn
		ON cmn.ID = cmir.CMN_ID
	JOIN ter.RegionInArea ria
		ON ria.ID = cmir.RIA_ID
	JOIN ter.Region reg
		ON reg.ID = ria.REG_ID
	WHERE
		prct.ID IN (SELECT ID FROM #tmpAccess)
	ORDER BY
		reg.NAME
		--,cmn.NAME
		,prct.NUMBER

	DROP TABLE #tmpAccess
END
GO
/****** Object:  StoredProcedure [elc].[sp_getprecinct_by_district]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [elc].[sp_getprecinct_by_district]
	@elcID uniqueidentifier,
	@districtID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
		ep.ID AS ELC_PRCT_ID,
		prct.NUMBER AS PRCT_NUMBER
	FROM ter.Precinct prct
	JOIN ter.PrecinctInCommunity pic ON pic.PRCT_ID = prct.ID
	JOIN elc.ElectionPrecinct ep ON ep.PRCT_CMN_ID = pic.ID
	JOIN elc.ElectionPrecinctInDistrict epd ON epd.ELC_PRCT_ID = ep.ID
	WHERE epd.DSTR_CNCL_ID = @districtID
	ORDER BY prct.NUMBER
END
GO
/****** Object:  StoredProcedure [elc].[sp_getprotocolforedit]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		ghost1908
-- Create date: 08.10.2020
-- Description:	Получить протокол для редактирования
-- =============================================
-- Modify date: 09.10.2020
-- Description: Исправлено - @ElectionPrecinctID на @ProtocolID, соответственно в WHERE тоже
-- =============================================
CREATE PROCEDURE [elc].[sp_getprotocolforedit]
	@ProtocolID uniqueidentifier,
	@UserID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

	CREATE TABLE #tmpAccess (ID uniqueidentifier)

	INSERT INTO #tmpAccess
	EXEC cfg.sp_getaccessbyuser @UserID, N'ter.Precinct'

	SELECT 
		prtl.ID AS PRTL_ID
		,pt.PRTL_NAME AS PRTL_NAME
		,prct.NUMBER AS PRCT_NUMBER
		,cncl.NAME AS CNCL_NAME
		,ct.NAME AS CNCL_TYPE_NAME
		,reg.NAME AS REG_NAME
		,area.NAME AS AREA_NAME
		,dstr.NUMBER AS DSTR_NUMBER
		,prtl.PRTL_ISSUE AS PRTL_ISSUE
	FROM elc.Protocol prtl
	JOIN elc.ProtocolType pt
		ON pt.ID = prtl.PRTL_TYPE_ID
	JOIN elc.ElectionPrecinct ep
		ON ep.ID = prtl.ELC_PRCT_ID
	JOIN ter.PrecinctInCommunity pic
		ON pic.ID = ep.PRCT_CMN_ID
	JOIN ter.Precinct prct
		ON prct.ID = pic.PRCT_ID
	JOIN ter.CommunityInRegion cir
		ON cir.ID = pic.CMIR_ID
	JOIN ter.RegionInArea ria
		ON ria.ID = cir.RIA_ID
	JOIN ter.Region reg
		ON reg.ID = ria.REG_ID
	JOIN ter.Area area
		ON area.ID = ria.AREA_ID
	JOIN elc.ElectionPrecinctInDistrict epd
		ON epd.ELC_PRCT_ID = ep.ID
	JOIN elc.DistrictInCouncil dic
		ON dic.ID = epd.DSTR_CNCL_ID
	JOIN ter.District dstr
		ON dstr.ID = dic.DSTR_ID
	JOIN elc.Council cncl
		ON cncl.ID = dic.CNCL_ID AND cncl.ID = prtl.CNCL_ID
	JOIN elc.CouncilType ct
		ON ct.ID = cncl.CNCLT_ID
	WHERE
		prtl.ID = @ProtocolID
		AND prct.ID IN (SELECT ID FROM #tmpAccess)

	SELECT
		pd.ID AS PRTL_DTL_ID
		,tmpl.ITEM_ORDER AS PRTL_ITEM_ORDER
		,item.PRTL_ITEM_NAME AS PRTL_ITEM_NAME
		,pd.PRTL_ITEM_VALUE AS PRTL_ITEM_VALUE
		,tmpl.IS_MULTIPLE_VALUE AS IS_MULTIPLE_VALUE
		,cnd.CND_FULLNAME AS CND_FULLNAME
		,cnd.CND_SHORTNAME AS CND_SHORTNAME
		,cnd.CND_TYPE AS CND_TYPE
		,ci.CND_ORDER AS CND_ORDER
		,ci.WATCH_ORDER AS CND_WATCH_ORDER
		,cnd.LIST_ORDER AS CND_LIST_ORDER
		,tmpl.GROUP_BY_PARENT AS PARENT_GROUPED
		,parent_cnd.CND_FULLNAME AS PARENT_FULLNAME
		,parent_order.CND_ORDER AS PARENT_ORDER
	FROM elc.ProtocolDetail pd
	JOIN elc.ProtocolTemplate tmpl
		ON tmpl.ID = pd.PRTL_TMPL_ID
	JOIN elc.ProtocolItem item
		ON item.ID = tmpl.PRTL_ITEM_ID
	JOIN elc.Protocol prtl
		ON prtl.ID = pd.PRTL_ID
	LEFT JOIN elc.CandidateInfo ci
		ON ci.ID = pd.CND_INFO_ID
	LEFT JOIN elc.Candidate cnd
		ON cnd.ID = ci.CND_ID
	LEFT JOIN elc.Candidate parent_cnd
		ON parent_cnd.ID = cnd.PARENT_CND_ID
	LEFT JOIN elc.CandidateInfo parent_order
		ON parent_order.CND_ID = parent_cnd.ID AND parent_order.CNCL_ID = prtl.CNCL_ID
	WHERE
		pd.PRTL_ID = @ProtocolID
	ORDER BY
		tmpl.ITEM_ORDER, ci.CND_ORDER, cnd.LIST_ORDER

	DROP TABLE #tmpAccess
END
GO
/****** Object:  StoredProcedure [elc].[sp_getprotocollist]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		ghost1908
-- Create date: 08.10.2020
-- Description:	Список протоколов
-- =============================================
CREATE PROCEDURE [elc].[sp_getprotocollist]
	@elcID uniqueidentifier,
	@UserID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

	CREATE TABLE #tmpAccess (ID uniqueidentifier)

	INSERT INTO #tmpAccess
	EXEC cfg.sp_getaccessbyuser @UserID, N'ter.Precinct'

	SELECT 
		ep.ID AS ELC_PRCT_ID
		,prtl.PRTL_STATUS_ID AS PRTL_STATUS_ID
		,prtl.ID AS PRTL_ID
		,prct.NUMBER AS PRCT_NUMBER
		,cncl.NAME AS CNCL_NAME
		,ct.WATCH_ORDER AS CNCL_TYPE_ORDER
		,ct.NAME AS CNCL_TYPE_NAME
		,cmn.NAME AS CMN_NAME
		,reg.NAME AS REG_NAME
		,area.NAME AS AREA_NAME
	FROM elc.ElectionPrecinct ep
	JOIN elc.ElectionPrecinctInfo epi
		ON epi.ELC_PRCT_ID = ep.ID
	JOIN ter.PrecinctInCommunity pic
		ON pic.ID = ep.PRCT_CMN_ID
	JOIN ter.Precinct prct
		ON prct.ID = pic.PRCT_ID
	JOIN ter.CommunityInRegion cir
		ON cir.ID = pic.CMIR_ID
	JOIN ter.Community cmn
		ON cmn.ID = cir.CMN_ID
	JOIN ter.RegionInArea ria
		ON ria.ID = cir.RIA_ID
	JOIN ter.Region reg
		ON reg.ID = ria.REG_ID
	JOIN ter.Area area
		ON area.ID = ria.AREA_ID
	JOIN elc.Council cncl
		ON cncl.ID = epi.CNCL_ID
	JOIN elc.CouncilType ct
		ON ct.ID = cncl.CNCLT_ID
	LEFT JOIN elc.Protocol prtl
		ON prtl.CNCL_ID = cncl.ID and prtl.ELC_PRCT_ID = ep.ID
	WHERE
		prct.ID IN (SELECT ID FROM #tmpAccess)
		AND ep.PRCT_OPENED = 1
	ORDER BY
		ct.WATCH_ORDER
		,reg.NAME
		,cmn.NAME
		,prct.NUMBER

	DROP TABLE #tmpAccess
END
GO
/****** Object:  StoredProcedure [elc].[sp_getprotocollist_nk]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		ghost1908
-- Create date: 08.10.2020
-- Description:	Список протоколов
-- =============================================
CREATE PROCEDURE [elc].[sp_getprotocollist_nk]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
		--eprct.ID AS ELC_PRCT_ID
		dstr.NUMBER as DSTR_NUMBER
		,reg.NAME AS REG_NAME
		,cmn.NAME AS CMN_NAME
		,prct.NUMBER AS PRCT_NUMBER
		,pt.ITEM_ORDER
		,cnd.CND_FULLNAME
		,pd.PRTL_ITEM_VALUE
	FROM elc.ElectionPrecinct eprct
	JOIN ter.PrecinctInCommunity prctcmn
		ON prctcmn.ID = eprct.PRCT_CMN_ID
	JOIN ter.Precinct prct
		ON prct.ID = prctcmn.PRCT_ID
	JOIN ter.CommunityInRegion cmir
		ON cmir.ID = prctcmn.CMIR_ID
	JOIN elc.ElectionPrecinctInfo epi
		on epi.ELC_PRCT_ID = eprct.ID
	JOIN ter.Community cmn
		ON cmn.ID = cmir.CMN_ID
	JOIN ter.RegionInArea ria
		ON ria.ID = cmir.RIA_ID
	JOIN ter.Region reg
		ON reg.ID = ria.REG_ID
	JOIN elc.DistrictInCouncil dic
		ON dic.CNCL_ID = epi.CNCL_ID
	JOIN ter.District dstr
		ON dstr.ID = dic.DSTR_ID
	JOIN elc.ElectionPrecinctInDistrict epd
		ON epd.ELC_PRCT_ID = eprct.ID AND epd.DSTR_CNCL_ID = dic.ID
	JOIN elc.Protocol prtl on prtl.ELC_PRCT_ID = eprct.ID
	JOIN elc.ProtocolDetail pd on pd.PRTL_ID = prtl.ID
	JOIN elc.ProtocolTemplate pt on pt.ID = pd.PRTL_TMPL_ID
	left join elc.CandidateInfo ci on ci.ID = pd.CND_INFO_ID
	left join elc.Candidate cnd on cnd.ID = ci.CND_ID
	WHERE
		eprct.PRCT_OPENED = 1
		and pt.ITEM_ORDER in ( 11, 12)
		AND epi.CNCL_ID = 'A49932CE-97E6-4C3B-B7A5-402937E9EEDE'
		and prtl.CNCL_ID='A49932CE-97E6-4C3B-B7A5-402937E9EEDE'
		AND prct.NUMBER NOT IN (
			'230834','230835','230910','230911',
'230912','230913','230915','231128',
'230860','230861','230968','230969',
'230971','231007','231008','231009',
'231010','231011','231012','231013',
'231014','231060','231121','231122',
'231123','231124','230375','230688',
'230689','230690','230523','230079',
'230713','230331','230775','230776',
'230777','230778','230779','230780',
'230635','230187','230188','230189',
'230571','230413','230450','230808'

		)
	ORDER BY
		DSTR_NUMBER
		,REG_NAME
		,CMN_NAME
		,PRCT_NUMBER
END
GO
/****** Object:  StoredProcedure [elc].[sp_getturnoutlist]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		ghost1908
-- Create date: 28.09.2020
-- Description:	Список избирательных участков для явки
-- =============================================
CREATE PROCEDURE [elc].[sp_getturnoutlist]
	@elcID uniqueidentifier,
	@UserID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

	CREATE TABLE #tmpAccess (ID uniqueidentifier)

	INSERT INTO #tmpAccess
	EXEC cfg.sp_getaccessbyuser @UserID, N'ter.Precinct'

	SELECT
		eprct.ID AS ELC_PRCT_ID
		,prct.NUMBER AS PRCT_NUMBER
		,cmn.NAME AS CMN_NAME
		,reg.NAME AS REG_NAME
		,eprct.PRCT_VOTERS
	FROM elc.ElectionPrecinct eprct
	JOIN ter.PrecinctInCommunity prctcmn
		ON prctcmn.ID = eprct.PRCT_CMN_ID
	JOIN ter.Precinct prct
		ON prct.ID = prctcmn.PRCT_ID
	JOIN ter.CommunityInRegion cmir
		ON cmir.ID = prctcmn.CMIR_ID
	JOIN ter.Community cmn
		ON cmn.ID = cmir.CMN_ID
	JOIN ter.RegionInArea ria
		ON ria.ID = cmir.RIA_ID
	JOIN ter.Region reg
		ON reg.ID = ria.REG_ID
	WHERE
		eprct.PRCT_OPENED = 1
		AND prct.ID IN (SELECT ID FROM #tmpAccess)
	ORDER BY
		reg.NAME
		,cmn.NAME
		,prct.NUMBER

    SELECT
		ep.ID AS ELC_PRCT_ID
		,t.ID AS ELC_TURNOUT_ID
		,tt.TT_VALUE AS TURNOUT_TIME
		,t.TO_VOTERS AS TURNOUT_VOTERS
	FROM elc.Turnout t
	JOIN elc.TurnoutTime tt
		ON tt.ID = t.TT_ID
	JOIN elc.ElectionPrecinct ep
		ON ep.ID = t.ELC_PRCT_ID
	JOIN ter.PrecinctInCommunity prctcmn
		ON prctcmn.ID = ep.PRCT_CMN_ID
	WHERE
		ep.PRCT_OPENED = 1
		AND prctcmn.PRCT_ID IN (SELECT ID FROM #tmpAccess)
	ORDER BY
		tt.TT_VALUE

	DROP TABLE #tmpAccess
END
GO
/****** Object:  StoredProcedure [elc].[sp_getturnoutlist_nk]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		ghost1908
-- Create date: 25.10.2020
-- Description:	
-- =============================================
CREATE PROCEDURE [elc].[sp_getturnoutlist_nk]
	@elcID uniqueidentifier,
	@UserID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

	CREATE TABLE #tmpAccess (ID uniqueidentifier)

	INSERT INTO #tmpAccess
	EXEC cfg.sp_getaccessbyuser @UserID, N'ter.Precinct'

	SELECT
		eprct.ID AS ELC_PRCT_ID
		,dstr.NUMBER as DSTR_NUMBER
		,reg.NAME AS REG_NAME
		,cmn.NAME AS CMN_NAME
		,prct.NUMBER AS PRCT_NUMBER
		,eprct.PRCT_VOTERS
	FROM elc.ElectionPrecinct eprct
	JOIN ter.PrecinctInCommunity prctcmn
		ON prctcmn.ID = eprct.PRCT_CMN_ID
	JOIN ter.Precinct prct
		ON prct.ID = prctcmn.PRCT_ID
	JOIN ter.CommunityInRegion cmir
		ON cmir.ID = prctcmn.CMIR_ID
	JOIN elc.ElectionPrecinctInfo epi
		on epi.ELC_PRCT_ID = eprct.ID
	JOIN ter.Community cmn
		ON cmn.ID = cmir.CMN_ID
	JOIN ter.RegionInArea ria
		ON ria.ID = cmir.RIA_ID
	JOIN ter.Region reg
		ON reg.ID = ria.REG_ID
	JOIN elc.DistrictInCouncil dic
		ON dic.CNCL_ID = epi.CNCL_ID
	JOIN ter.District dstr
		ON dstr.ID = dic.DSTR_ID
	JOIN elc.ElectionPrecinctInDistrict epd
		ON epd.ELC_PRCT_ID = eprct.ID AND epd.DSTR_CNCL_ID = dic.ID
	WHERE
		eprct.PRCT_OPENED = 1
		AND epi.CNCL_ID = 'A49932CE-97E6-4C3B-B7A5-402937E9EEDE'
		AND prct.NUMBER NOT IN (
			'230834',
'230835',
'230910',
'230911',
'230912',
'230913',
'230915',
'231128',
'230860',
'230861',
'230968',
'230969',
'230971',
'231007',
'231008',
'231009',
'231010',
'231011',
'231012',
'231013',
'231014',
'231060',
'231121',
'231122',
'231123',
'231124',
'230375',
'230688',
'230689',
'230690',
'230523',
'230079',
'230713',
'230331',
'230775',
'230776',
'230777',
'230778',
'230779',
'230780',
'230635',
'230187',
'230188',
'230189',
'230571',
'230413',
'230450',
'230808'

		)
	ORDER BY
		DSTR_NUMBER
		,REG_NAME
		,CMN_NAME
		,PRCT_NUMBER
	
    SELECT
		ep.ID AS ELC_PRCT_ID
		,t.ID AS ELC_TURNOUT_ID
		,tt.TT_VALUE AS TURNOUT_TIME
		,t.TO_VOTERS AS TURNOUT_VOTERS
	FROM elc.Turnout t
	JOIN elc.TurnoutTime tt
		ON tt.ID = t.TT_ID
	JOIN elc.ElectionPrecinct ep
		ON ep.ID = t.ELC_PRCT_ID
	JOIN ter.PrecinctInCommunity prctcmn
		ON prctcmn.ID = ep.PRCT_CMN_ID
	WHERE
		ep.PRCT_OPENED = 1
		AND prctcmn.PRCT_ID IN (SELECT ID FROM #tmpAccess)
	ORDER BY
		tt.TT_VALUE

	DROP TABLE #tmpAccess
END
GO
/****** Object:  StoredProcedure [elc].[sp_getturnoutprecinctforedit]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		ghost1908
-- Create date: 28.09.2020
-- Description:	Получить участок для редактирования явки
-- =============================================
-- Modify date: 
-- Description: 
-- =============================================
CREATE PROCEDURE [elc].[sp_getturnoutprecinctforedit]
	@turnoutID uniqueidentifier,
	@UserID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

	CREATE TABLE #tmpAccess (ID uniqueidentifier)

	INSERT INTO #tmpAccess
	EXEC cfg.sp_getaccessbyuser @UserID, N'ter.Precinct'

    SELECT
		t.ID AS ELC_TURNOUT_ID
		,tt.TT_VALUE AS TURNOUT_TIME
		,t.TO_VOTERS AS TURNOUT_VOTERS
	FROM elc.Turnout t
	JOIN elc.TurnoutTime tt
		ON tt.ID = t.TT_ID
	JOIN elc.ElectionPrecinct ep
		ON ep.ID = t.ELC_PRCT_ID
	JOIN ter.PrecinctInCommunity prctcmn
		ON prctcmn.ID = ep.PRCT_CMN_ID
	WHERE
		ep.PRCT_OPENED = 1
		AND t.ID = @turnoutID
		AND prctcmn.PRCT_ID IN (SELECT ID FROM #tmpAccess)
	ORDER BY
		tt.TT_VALUE

	DROP TABLE #tmpAccess
END
GO
/****** Object:  StoredProcedure [elc].[sp_onlineopenprecincts]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		ghost1908
-- Create date: 13.10.2020
-- Description:	Онлайн, открытие участков
-- =============================================
CREATE PROCEDURE [elc].[sp_onlineopenprecincts] 
	@elcID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

    SELECT
		area.NAME AS AREA_NAME
		,reg.NAME AS REG_NAME
		,cmn.NAME AS CMN_NAME
		,ep.PRCT_OPENED
		,prct.NUMBER AS PRCT_NUMBER
		,ep.PRCT_VOTERS
		,ct.WATCH_ORDER AS CNCL_TYPE_ORDER
		,ct.NAME AS CNCL_TYPE_NAME
		,epi.BULLETIN_RECV
	FROM elc.ElectionPrecinct ep
	JOIN elc.ElectionPrecinctInfo epi
		ON epi.ELC_PRCT_ID = ep.ID
	JOIN elc.Council cncl
		ON cncl.ID = epi.CNCL_ID
	JOIN elc.CouncilType ct
		ON ct.ID = cncl.CNCLT_ID
	JOIN ter.PrecinctInCommunity pic
		ON pic.ID = ep.PRCT_CMN_ID
	JOIN ter.Precinct prct
		ON prct.ID = pic.PRCT_ID
	JOIN ter.CommunityInRegion cir
		ON cir.ID = pic.CMIR_ID
	JOIN ter.Community cmn
		ON cmn.ID = cir.CMN_ID
	JOIN ter.RegionInArea ria
		ON ria.ID = cir.RIA_ID
	JOIN ter.Region reg
		ON reg.ID = ria.REG_ID
	JOIN ter.Area area
		ON area.ID = ria.AREA_ID
	ORDER BY
		reg.NAME, cmn.NAME, prct.NUMBER, ct.WATCH_ORDER
END
GO
/****** Object:  StoredProcedure [elc].[sp_onlineprotocol_by_council]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		ghost1908
-- Create date: 18.10.2020
-- Description:	Онлайн протокол (для одного совета)
-- =============================================
-- Modify date: 19.10.2020
-- Description: Добавлены статусы протоколов
-- =============================================
CREATE PROCEDURE [elc].[sp_onlineprotocol_by_council] 
	@elcID uniqueidentifier,
	@councilID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

	SELECT c.NAME + ' ' + LOWER(ct.NAME) AS COUNCIL_NAME 
	FROM elc.Council c JOIN elc.CouncilType ct ON ct.ID = c.CNCLT_ID WHERE c.ID = @councilID

	SELECT
		prtl.PRTL_STATUS_ID AS PRTL_STATUS_ID
		,COUNT(*) AS PRTL_STATUS_COUNT
	FROM elc.Protocol prtl
	JOIN elc.ElectionPrecinct ep ON ep.ID = prtl.ELC_PRCT_ID
	WHERE prtl.CNCL_ID = @councilID AND ep.PRCT_OPENED = 1
	GROUP BY prtl.PRTL_STATUS_ID

    SELECT
		tmpl.ITEM_ORDER AS PRTL_ITEM_ORDER
		,cnd.CND_TYPE AS CND_TYPE
		,cnd.CND_FULLNAME AS CND_NAME
		,parent.CND_FULLNAME AS PARENT_NAME
		,SUM(ISNULL(pd.PRTL_ITEM_VALUE,0)) AS PRTL_ITEM_VALUE
	FROM elc.ProtocolDetail pd
	JOIN elc.ProtocolTemplate tmpl
		ON tmpl.ID = pd.PRTL_TMPL_ID
	JOIN elc.ProtocolItem item
		ON item.ID = tmpl.PRTL_ITEM_ID
	JOIN elc.Protocol prtl
		ON prtl.ID = pd.PRTL_ID
	JOIN elc.ElectionPrecinct ep ON ep.ID = prtl.ELC_PRCT_ID
	LEFT JOIN elc.CandidateInfo ci
		ON ci.ID = pd.CND_INFO_ID
	LEFT JOIN elc.Candidate cnd
		ON cnd.ID = ci.CND_ID
	LEFT JOIN elc.Candidate parent
		ON parent.ID = cnd.PARENT_CND_ID
	WHERE
		prtl.CNCL_ID = @councilID AND ep.PRCT_OPENED = 1 AND prtl.PRTL_STATUS_ID >= 2
	GROUP BY
		tmpl.ITEM_ORDER
		,cnd.CND_TYPE
		,cnd.CND_FULLNAME
		,parent.CND_FULLNAME
	ORDER BY
		tmpl.ITEM_ORDER
END
GO
/****** Object:  StoredProcedure [elc].[sp_onlineprotocol_by_council_or_district]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		ghost1908
-- Create date: 24.10.2020
-- Description:	Онлайн протокол (для одного совета или округа)
-- =============================================
-- Modify date: 
-- Description: 
-- =============================================
CREATE PROCEDURE [elc].[sp_onlineprotocol_by_council_or_district] 
	@elcID uniqueidentifier,
	@councilID uniqueidentifier,
	@districtID uniqueidentifier = null,
	@precinctID uniqueidentifier = null
AS
BEGIN
	SET NOCOUNT ON;

    SELECT
		dt.NAME AS DSTR_NAME,
		dstr.NUMBER AS DSTR_NUMBER,
		prct.NUMBER AS PRCT_NUMBER,
		tmpl.ITEM_ORDER AS PRTL_ITEM_ORDER
		,item.PRTL_ITEM_NAME AS PRTL_ITEM_NAME
		,cnd.CND_FULLNAME AS CND_NAME
		,tmpl.IS_MULTIPLE_VALUE AS IS_MULTIPLE_VALUE
		,cnd.CND_TYPE AS CND_TYPE
		,ci.CND_ORDER AS CND_ORDER
		,ci.WATCH_ORDER AS CND_WATCH_ORDER
		,cnd.LIST_ORDER AS CND_LIST_ORDER
		,tmpl.GROUP_BY_PARENT AS PARENT_GROUPED
		,parent_cnd.CND_FULLNAME AS PARENT_FULLNAME
		,parent_order.CND_ORDER AS PARENT_ORDER
		,ISNULL(pd.PRTL_ITEM_VALUE,0) AS PRTL_ITEM_VALUE
	FROM elc.ProtocolDetail pd
	JOIN elc.ProtocolTemplate tmpl ON tmpl.ID = pd.PRTL_TMPL_ID
	JOIN elc.ProtocolItem item ON item.ID = tmpl.PRTL_ITEM_ID
	JOIN elc.Protocol prtl ON prtl.ID = pd.PRTL_ID
	JOIN elc.ElectionPrecinct ep ON ep.ID = prtl.ELC_PRCT_ID
	JOIN elc.ElectionPrecinctInDistrict epd ON epd.ELC_PRCT_ID = ep.ID
	JOIN elc.DistrictInCouncil dic ON dic.ID = epd.DSTR_CNCL_ID AND dic.CNCL_ID = prtl.CNCL_ID
	JOIN ter.District dstr ON dstr.ID = dic.DSTR_ID
	JOIN ter.DistrictType dt ON dt.ID = dstr.DSTRT_ID
	JOIN ter.PrecinctInCommunity pic ON pic.ID = ep.PRCT_CMN_ID
	JOIN ter.Precinct prct ON prct.ID = pic.PRCT_ID
	LEFT JOIN elc.CandidateInfo ci ON ci.ID = pd.CND_INFO_ID
	LEFT JOIN elc.Candidate cnd ON cnd.ID = ci.CND_ID
	LEFT JOIN elc.Candidate parent_cnd ON parent_cnd.ID = cnd.PARENT_CND_ID
	LEFT JOIN elc.CandidateInfo parent_order ON parent_order.CND_ID = parent_cnd.ID AND parent_order.CNCL_ID = prtl.CNCL_ID
	WHERE
		prtl.CNCL_ID = @councilID
		AND ep.PRCT_OPENED = 1
		AND dic.ID = CASE WHEN @districtID IS NULL THEN dic.ID ELSE @districtID END
	--	AND epd.ELC_PRCT_ID = CASE WHEN @precinctID IS NULL THEN epd.ELC_PRCT_ID ELSE @precinctID END
	ORDER BY
		dstr.NUMBER, tmpl.ITEM_ORDER, prct.NUMBER
END
GO
/****** Object:  StoredProcedure [elc].[sp_onlineprotocol_status]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		ghost1908
-- Create date: 21.10.2020
-- Description:	Онлайн протокол по статусам
-- =============================================
-- =============================================
CREATE PROCEDURE [elc].[sp_onlineprotocol_status] 
	@elcID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
		ct.WATCH_ORDER AS CNCL_ORDER
		,cncl.NAME + ' ' + LOWER(ct.NAME) AS CNCL_NAME
		,area.NAME AS AREA_NAME
		,reg.NAME AS REG_NAME
		,cmn.NAME AS CMN_NAME
		,cc.NAME AS CC_NAME
		,prct.NUMBER AS PRCT_NUMBER
		,prtl.PRTL_STATUS_ID
	FROM elc.Protocol prtl
	JOIN elc.Council cncl
		ON cncl.ID = prtl.CNCL_ID
	JOIN elc.CouncilType ct
		ON ct.ID = cncl.CNCLT_ID
	JOIN elc.ElectionPrecinct ep
		ON ep.ID = prtl.ELC_PRCT_ID
	JOIN ter.PrecinctInCommunity pic
		ON pic.ID = ep.PRCT_CMN_ID
	JOIN ter.Precinct prct
		ON prct.ID = pic.PRCT_ID
	JOIN ter.CommunityInRegion cir
		ON cir.ID = pic.CMIR_ID
	JOIN ter.Community cmn
		ON cmn.ID = cir.CMN_ID
	JOIN ter.RegionInArea ria
		ON ria.ID = cir.RIA_ID
	JOIN ter.Region reg
		ON reg.ID = ria.REG_ID
	JOIN ter.Area area
		ON area.ID = ria.AREA_ID
	LEFT JOIN ter.CommunityChild cc ON cc.ELC_PRCT_ID = ep.ID
	WHERE
		ep.PRCT_OPENED = 1
	ORDER BY
		1,2,4,5,6
END
GO
/****** Object:  StoredProcedure [elc].[sp_onlineturnoutprecincts]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		ghost1908
-- Create date: 16.10.2020
-- Description:	Онлайн явка
-- =============================================
CREATE PROCEDURE [elc].[sp_onlineturnoutprecincts] 
	@elcID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

    SELECT
		area.NAME AS AREA_NAME
		,reg.NAME AS REG_NAME
		,cmn.NAME AS CMN_NAME
		,cc.NAME as CC_NAME
		,prct.NUMBER AS PRCT_NUMBER
		,ep.PRCT_VOTERS AS PRCT_VOTERS
		,tt.TT_VALUE AS TURNOUT_TIME
		,t.TO_VOTERS AS TURNOUT_VALUE
	FROM elc.Turnout t
	JOIN elc.TurnoutTime tt
		ON tt.ID = t.TT_ID
	JOIN elc.ElectionPrecinct ep
		ON ep.ID = t.ELC_PRCT_ID
	JOIN ter.PrecinctInCommunity pic
		ON pic.ID = ep.PRCT_CMN_ID
	JOIN ter.Precinct prct
		ON prct.ID = pic.PRCT_ID
	JOIN ter.CommunityInRegion cir
		ON cir.ID = pic.CMIR_ID
	JOIN ter.Community cmn
		ON cmn.ID = cir.CMN_ID
	JOIN ter.RegionInArea ria
		ON ria.ID = cir.RIA_ID
	JOIN ter.Region reg
		ON reg.ID = ria.REG_ID
	JOIN ter.Area area
		ON area.ID = ria.AREA_ID
	LEFT JOIN ter.CommunityChild cc ON cc.ELC_PRCT_ID = ep.ID
	WHERE
		ep.PRCT_OPENED = 1
	ORDER BY
		reg.NAME, cmn.NAME, prct.NUMBER, tt.TT_VALUE
END
GO
/****** Object:  StoredProcedure [elc].[sp_updateelectionprecinctopen]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		ghost1908
-- Create date: 28.09.2020
-- Description:	Отрытие участка и внесение информации о бюллетенях по советам
-- =============================================
CREATE PROCEDURE [elc].[sp_updateelectionprecinctopen]
	@ELC_PRCT elc.TVP_ElectionPrecinct READONLY,
	@ELC_PRCT_INFO elc.TVP_ElectionPrecinctInfo READONLY
AS
BEGIN
	SET NOCOUNT ON;

    UPDATE eprct
	SET eprct.PRCT_OPENED = ep.PRCT_OPENED,
		eprct.PRCT_NOTOPEN_CAUSE = ep.PRCT_NOTOPENED_CAUSE,
		eprct.PRCT_VOTERS = ep.PRCT_VOUTERS
	FROM elc.ElectionPrecinct eprct
	JOIN @ELC_PRCT ep
		ON ep.ELC_PRCT_ID = eprct.ID

	UPDATE epinfo
	SET epinfo.BULLETIN_RECV = epi.BLT_RECV
	FROM elc.ElectionPrecinctInfo epinfo
	JOIN @ELC_PRCT_INFO epi
		ON epi.ELC_PRCT_INFO_ID = epinfo.ID

	SELECT N'OK'
END
GO
/****** Object:  StoredProcedure [elc].[sp_updateprotocol]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		ghost1908
-- Create date: 13.10.2020
-- Description:	Обновление протокола
-- =============================================
-- Modify date: 16.10.2020
-- Description: Добавлено обновление статуса протокола
-- =============================================
-- =============================================
-- Modify date: 30.10.2020
-- Description: Добавлено сохранение комментария к протоколу и пользователь
-- =============================================
CREATE PROCEDURE [elc].[sp_updateprotocol]
	@PROTOCOL_ID uniqueidentifier,
	@PROTOCOL_ISSUE nvarchar(1000),
	@PROTOCOL_DATA elc.TVP_ProtocolItemValue READONLY,
	@UserId uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @result int

	IF NOT EXISTS(SELECT * FROM elc.Protocol WHERE ID = @PROTOCOL_ID)
	BEGIN
		SET @result = -1
		GOTO Branch_End
	END

	IF (SELECT COUNT(*) FROM @PROTOCOL_DATA) - 
		(SELECT COUNT(*) FROM elc.ProtocolDetail pd
			JOIN @PROTOCOL_DATA pData ON pData.ID = pd.ID
			WHERE pd.PRTL_ID = @PROTOCOL_ID) <> 0
	BEGIN
		SET @result = (SELECT COUNT(*) FROM elc.ProtocolDetail pd
			JOIN @PROTOCOL_DATA pData ON pData.ID = pd.ID
			WHERE pd.PRTL_ID = @PROTOCOL_ID)
		GOTO Branch_End
	END

	INSERT INTO elc.ProtocolDetail_Chng
	SELECT NEWID(), GETDATE(), @UserId, @PROTOCOL_ID, NULL, NULL, pd.PRTL_TMPL_ID, pd.PRTL_ITEM_VALUE, pData.VOTES
	FROM @PROTOCOL_DATA AS pData
	JOIN elc.ProtocolDetail AS pd ON pd.ID = pData.ID
	WHERE pd.PRTL_ID = @PROTOCOL_ID
		AND (pd.PRTL_ITEM_VALUE <> pData.VOTES 
			OR (pd.PRTL_ITEM_VALUE IS NULL AND pData.VOTES IS NOT NULL)
			OR (pd.PRTL_ITEM_VALUE IS NOT NULL AND pData.VOTES IS NULL))

	UPDATE pd
	SET pd.PRTL_ITEM_VALUE = pData.VOTES
	FROM @PROTOCOL_DATA AS pData
	JOIN elc.ProtocolDetail AS pd ON pd.ID = pData.ID
	WHERE pd.PRTL_ID = @PROTOCOL_ID

	UPDATE elc.Protocol
	SET PRTL_ISSUE = @PROTOCOL_ISSUE
	WHERE ID = @PROTOCOL_ID

	SET @result = 0

	Branch_End:
		EXEC elc.sp_updateprotocolstatus @PROTOCOL_ID, @UserId
		SELECT @result
END
GO
/****** Object:  StoredProcedure [elc].[sp_updateprotocolstatus]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		ghost1908
-- Create date: 16.10.2020
-- Description:	Обновление статуса протокола
-- =============================================
CREATE PROCEDURE [elc].[sp_updateprotocolstatus]
	@prtlID uniqueidentifier,
	@user_id uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

    DECLARE @status tinyint, @status_old tinyint
	DECLARE @skip bit
	DECLARE @p1 bigint, @p2 bigint, @p3 bigint, @p7 bigint
	DECLARE @p9 bigint, @p10 bigint, @p11 bigint

	CREATE TABLE #protocol
	(
		CND_ID uniqueidentifier null,
		WATCH_ORDER tinyint null,
		ITEM_ORDER tinyint,
		IS_MULTIPLE_VALUE bit,
		PRTL_ITEM_VALUE bigint null,
		GROUP_BY_PARENT bit null,
		PARENT_CND_ID uniqueidentifier null
	)

	CREATE TABLE #candidates
	(
		CND_ID uniqueidentifier,
		ITEM_12 bigint,
		ITEM_13 bigint,
		ITEM_14 bigint
	)

	INSERT INTO #protocol
	SELECT
		cnd.ID
		,ci.WATCH_ORDER
		,pt.ITEM_ORDER
		,pt.IS_MULTIPLE_VALUE
		,pd.PRTL_ITEM_VALUE
		,pt.GROUP_BY_PARENT
		,cnd.PARENT_CND_ID
	FROM elc.ProtocolDetail pd
	JOIN elc.ProtocolTemplate pt
		ON pt.ID = pd.PRTL_TMPL_ID
	LEFT JOIN elc.CandidateInfo ci
		ON ci.ID = pd.CND_INFO_ID
	LEFT JOIN elc.Candidate cnd
		ON cnd.ID = ci.CND_ID
	WHERE pd.PRTL_ID = @prtlID

	IF EXISTS(SELECT * FROM #protocol WHERE ITEM_ORDER IN (12, 13, 14))
		SET @skip = 0
	ELSE
		SET @skip = 1

	IF (SELECT SUM(1) - SUM(IIF(PRTL_ITEM_VALUE IS NULL, 1, 0)) FROM #protocol) = 0
	BEGIN
		SET @status = 0		-- нет данных
		GOTO BranchEnd
	END

	SET @p1 = (SELECT PRTL_ITEM_VALUE FROM #protocol WHERE ITEM_ORDER = 1)
	SET @p2 = (SELECT PRTL_ITEM_VALUE FROM #protocol WHERE ITEM_ORDER = 2)
	SET @p3 = (SELECT PRTL_ITEM_VALUE FROM #protocol WHERE ITEM_ORDER = 3)
	SET @p7 = (SELECT PRTL_ITEM_VALUE FROM #protocol WHERE ITEM_ORDER = 7)
	SET @p9 = (SELECT PRTL_ITEM_VALUE FROM #protocol WHERE ITEM_ORDER = 9)
	SET @p10 = (SELECT PRTL_ITEM_VALUE FROM #protocol WHERE ITEM_ORDER = 10)
	IF @skip = 0
	BEGIN
		SET @p11 = (SELECT PRTL_ITEM_VALUE FROM #protocol WHERE ITEM_ORDER = 11)

		INSERT INTO #candidates
		SELECT p.CND_ID, p.PRTL_ITEM_VALUE, 0, 0 FROM #protocol p WHERE p.ITEM_ORDER = 12

		INSERT INTO #candidates
		SELECT p.CND_ID, 0, p.PRTL_ITEM_VALUE, 0 FROM #protocol p WHERE p.ITEM_ORDER = 13

		INSERT INTO #candidates
		SELECT p.PARENT_CND_ID, SUM(0), SUM(0), SUM(ISNULL(p.PRTL_ITEM_VALUE, 0)) FROM #protocol p WHERE p.ITEM_ORDER = 14 GROUP BY p.PARENT_CND_ID

		IF EXISTS(SELECT * FROM #protocol WHERE ITEM_ORDER IN ( 1, 3, 7, 9, 11, 12) AND PRTL_ITEM_VALUE IS NULL)
		BEGIN
			SET @status = 1		--неполные оперативные
			GOTO BranchEnd
		END

		IF NOT EXISTS(SELECT * FROM #protocol WHERE ITEM_ORDER IN ( 1, 3, 7, 9, 11, 12) AND PRTL_ITEM_VALUE IS NULL)
			AND (SELECT COUNT(*) FROM #protocol WHERE ITEM_ORDER IN ( 2, 4, 5, 6, 8, 10, 13, 14) AND PRTL_ITEM_VALUE IS NOT NULL) = 0
		BEGIN
			SET @status = 2		--оперативные данные
			GOTO BranchEnd
		END

		IF NOT EXISTS(SELECT * FROM #protocol WHERE ITEM_ORDER IN ( 1, 3, 7, 9, 11, 12) AND PRTL_ITEM_VALUE IS NULL)
			AND ((SELECT COUNT(*) FROM #protocol WHERE ITEM_ORDER IN ( 1, 3, 7, 9, 11, 12) AND PRTL_ITEM_VALUE IS NOT NULL)
				+ (SELECT COUNT(*) FROM #protocol WHERE ITEM_ORDER IN ( 2, 4, 5, 6, 8, 10, 13, 14) AND PRTL_ITEM_VALUE IS NOT NULL)) 
				< (SELECT COUNT(*) FROM #protocol)
		BEGIN
			SET @status = 3		-- частичный протокол
			GOTO BranchEnd
		END

		IF EXISTS(SELECT * FROM #protocol WHERE PRTL_ITEM_VALUE IS NULL)
			OR (@p1 <> @p2 + @p7) OR (@p9 <> @p10 + @p11)
			OR EXISTS(SELECT * FROM
					(SELECT CND_ID, (SUM(ISNULL(ITEM_12,0)) - SUM(ISNULL(ITEM_13,0)) - SUM(ISNULL(ITEM_14,0))) AS CND_RESULT FROM #candidates GROUP BY CND_ID) r
					WHERE r.CND_RESULT <> 0)
		BEGIN
			SET @status = 4		-- неверный протокол
			GOTO BranchEnd
		END

		IF NOT EXISTS(SELECT * FROM #protocol WHERE PRTL_ITEM_VALUE IS NULL)
			AND (@p1 = @p2 + @p7)
			AND (@p9 = @p10 + @p11)
			AND NOT EXISTS(SELECT * FROM
					(SELECT CND_ID, (SUM(ISNULL(ITEM_12,0)) - SUM(ISNULL(ITEM_13,0)) - SUM(ISNULL(ITEM_14,0))) AS CND_RESULT FROM #candidates GROUP BY CND_ID) r
					WHERE r.CND_RESULT <> 0)
			SET @status = 5		-- верный протокол
	END
	ELSE
	BEGIN
		INSERT INTO #candidates
		SELECT p.CND_ID, p.PRTL_ITEM_VALUE, 0, 0 FROM #protocol p WHERE p.ITEM_ORDER = 11

		IF EXISTS(SELECT * FROM #protocol WHERE ITEM_ORDER IN ( 1, 3, 7, 9, 11 ) AND PRTL_ITEM_VALUE IS NULL)
		BEGIN
			SET @status = 1		--неполные оперативные
			GOTO BranchEnd
		END

		IF NOT EXISTS(SELECT * FROM #protocol WHERE ITEM_ORDER IN ( 1, 3, 7, 9, 11) AND PRTL_ITEM_VALUE IS NULL)
			AND (SELECT COUNT(*) FROM #protocol WHERE ITEM_ORDER IN ( 2, 4, 5, 6, 8, 10 ) AND PRTL_ITEM_VALUE IS NOT NULL) = 0
		BEGIN
			SET @status = 2		--оперативные данные
			GOTO BranchEnd
		END

		IF NOT EXISTS(SELECT * FROM #protocol WHERE ITEM_ORDER IN ( 1, 3, 7, 9, 11 ) AND PRTL_ITEM_VALUE IS NULL)
			AND ((SELECT COUNT(*) FROM #protocol WHERE ITEM_ORDER IN ( 1, 3, 7, 9, 11 ) AND PRTL_ITEM_VALUE IS NOT NULL)
				+ (SELECT COUNT(*) FROM #protocol WHERE ITEM_ORDER IN ( 2, 4, 5, 6, 8, 10 ) AND PRTL_ITEM_VALUE IS NOT NULL)) 
				< (SELECT COUNT(*) FROM #protocol)
		BEGIN
			SET @status = 3		-- частичный протокол
			GOTO BranchEnd
		END

		IF EXISTS(SELECT * FROM #protocol WHERE PRTL_ITEM_VALUE IS NULL)
			OR (@p1 <> @p2 + @p7) OR (@p9 <> @p10 + (SELECT SUM(ISNULL(ITEM_12,0)) FROM #candidates))
		BEGIN
			SET @status = 4		-- неверный протокол
			GOTO BranchEnd
		END

		IF NOT EXISTS(SELECT * FROM #protocol WHERE PRTL_ITEM_VALUE IS NULL)
			AND (@p1 = @p2 + @p7)
			AND (@p9 = @p10 + (SELECT SUM(ISNULL(ITEM_12,0)) FROM #candidates))
			SET @status = 5		-- верный протокол
	END
	BranchEnd:
		
		SET @status_old = (SELECT PRTL_STATUS_ID FROM elc.Protocol WHERE ID = @prtlID)
		IF @status_old <> @status
			INSERT INTO elc.ProtocolDetail_Chng
			SELECT NEWID(), GETDATE(), @user_id, @prtlID, @status_old, @status, NULL, NULL, NULL
			FROM elc.Protocol
			WHERE ID = @prtlID

		UPDATE elc.Protocol
		SET PRTL_STATUS_ID = @status
		WHERE ID = @prtlID
	
		DROP TABLE #candidates
		DROP TABLE #protocol
END
GO
/****** Object:  StoredProcedure [elc].[sp_updateturnoutprecinct]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		ghost1908
-- Create date: 29.09.2020
-- Description:	Ввод явки по участкам
-- =============================================
CREATE PROCEDURE [elc].[sp_updateturnoutprecinct]
	@TURNOUT elc.TVP_Turnout READONLY
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE et
	SET et.TO_VOTERS = t.TURNOUT_VOTERS
	FROM elc.Turnout et
	JOIN @TURNOUT t
		ON t.TURNOUT_ID = et.ID

	SELECT N'OK'
END
GO
/****** Object:  StoredProcedure [ter].[sp_createcommunity]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		ghost1908
-- Create date: 10.08.2020
-- Description:	Создание ОТГ и добавление в район
-- =============================================
CREATE PROCEDURE [ter].[sp_createcommunity]
	@ria_id uniqueidentifier,
	@community_name nvarchar(200)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @cmn_id uniqueidentifier

	IF NOT EXISTS(SELECT * FROM ter.RegionInArea WHERE ID = @ria_id)
	BEGIN
		SELECT N'Нет района'
		RETURN
	END

	IF EXISTS(SELECT * FROM ter.Community WHERE NAME = @community_name)
	BEGIN
		SELECT N'ОТГ с таким именем существует'
		RETURN
	END

	SET @cmn_id = NEWID()

	INSERT INTO ter.Community VALUES ( @cmn_id, @community_name )

	INSERT INTO ter.CommunityInRegion VALUES ( NEWID(), @cmn_id, @ria_id )

	SELECT N'OK'
END
GO
/****** Object:  StoredProcedure [ter].[sp_getarealist]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		ghost1908
-- Create date: 10.08.2020
-- Description:	Получить список Областей
-- =============================================
-- Modify date: 25.01.2021
-- Description: Изменена схема доступа
-- =============================================
CREATE PROCEDURE [ter].[sp_getarealist] 
	@UserID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

    CREATE TABLE #tmpAccess (ID uniqueidentifier)

	INSERT INTO #tmpAccess
	--EXEC dbo.sp_getaddressaccessbyuser @UserID, N'ter.Area'
	EXEC cfg.sp_getaccessbyuser @UserID, N'ter.Area'

	--SELECT 
	--	area.*
	--FROM ter.Area area
	--JOIN #tmpAccess acl
	--	ON acl.ID = area.ID
	--ORDER BY
	--	area.NAME
	SELECT 
		area.*
	FROM ter.Area area
	WHERE area.ID IN (SELECT ID FROM #tmpAccess)
	ORDER BY
		area.NAME

	DROP TABLE #tmpAccess
END
GO
/****** Object:  StoredProcedure [ter].[sp_getcitylist]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		ghost1908
-- Create date: 02.02.2021
-- Description:	Получить список Городов в Громаде для пользователя
-- =============================================
CREATE PROCEDURE [ter].[sp_getcitylist]
	@areaId uniqueidentifier = null,
	@riaId uniqueidentifier = null,
	@cirId uniqueidentifier = null,
	@UserID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

    CREATE TABLE #tmpAccess (ID uniqueidentifier)

	INSERT INTO #tmpAccess
	EXEC cfg.sp_getaccessbyuser @UserID, N'ter.City'

	--SELECT 
	--	cic.*
	--	,city.NAME AS CITY_NAME
	--	,ct.ID AS CITYT_ID
	--	,ct.NAME AS CITYT_NAME
	--FROM ter.CityInCommunity cic
	--JOIN ter.City city
	--	ON city.ID = cic.CITY_ID
	--JOIN ter.CityType ct
	--	ON ct.ID = city.CITYT_ID
	--WHERE
	--	cic.CIR_ID = @CommunityID
	SELECT
		cic.ID
		,city.ID AS CityID
		,city.NAME AS CityName
		,ct.ID AS CityTypeID
		,ct.NAME AS CityTypeName
		,cmn.ID AS CommunityID
		,cmn.NAME AS CommunityName
		,cir.ID AS CommunityInRegionID
		,reg.ID AS RegionID
		,reg.NAME AS RegionName
		,ria.ID AS RegionInAreaID
		,area.ID AS AreaID
		,area.NAME AS AreaName
	FROM ter.City city
	JOIN ter.CityType ct
		ON ct.ID = city.CITYT_ID
	JOIN ter.CityInCommunity cic
		ON cic.CITY_ID = city.ID
	JOIN ter.CommunityInRegion cir
		ON cir.ID = cic.CIR_ID
	JOIN ter.Community cmn
		ON cmn.ID = cir.CMN_ID
	JOIN ter.RegionInArea ria
		ON ria.ID = cir.RIA_ID
	JOIN ter.Region reg
		ON reg.ID = ria.REG_ID
	JOIN ter.Area area
		ON area.ID = ria.AREA_ID
	WHERE
		area.ID = CASE WHEN @areaId IS NULL THEN area.ID ELSE @areaId END
		AND
		ria.ID = CASE WHEN @riaId IS NULL THEN ria.ID ELSE @riaId END
		AND
		cir.ID = CASE WHEN @cirId IS NULL THEN cir.ID ELSE @cirId END
		AND
		city.ID IN (SELECT ID FROM #tmpAccess)
	ORDER BY
		area.NAME
		,reg.NAME
		,cmn.NAME
		,city.NAME

	DROP TABLE #tmpAccess
END
GO
/****** Object:  StoredProcedure [ter].[sp_getcommunitylist]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		ghost1908
-- Create date: 10.08.2020
-- Description:	Весь список ОТГ и/или по району и/или по области
-- =============================================
CREATE PROCEDURE [ter].[sp_getcommunitylist]
	@areaId uniqueidentifier = null,
	@regId uniqueidentifier = null,
	@riaId uniqueidentifier = null,
	@UserID uniqueidentifier = null
AS
BEGIN
	SET NOCOUNT ON;

	CREATE TABLE #tmpAccess (ID uniqueidentifier)

	INSERT INTO #tmpAccess
	EXEC cfg.sp_getaccessbyuser @UserID, N'ter.Community'

    SELECT
		cmn.ID
		,cmn.NAME AS CommunityName
		,cmir.ID AS CommunityInRegionID
		,reg.ID AS RegionID
		,reg.NAME AS RegionName
		,ria.ID AS RegionInAreaID
		,area.ID AS AreaID
		,area.NAME AS AreaName
	FROM ter.Community cmn
	JOIN ter.CommunityInRegion cmir
		ON cmir.CMN_ID = cmn.ID
	JOIN ter.RegionInArea ria
		ON ria.ID = cmir.RIA_ID
	JOIN ter.Region reg
		ON reg.ID = ria.REG_ID
	JOIN ter.Area area
		ON area.ID = ria.AREA_ID
	WHERE
		area.ID = CASE WHEN @areaId IS NULL THEN area.ID ELSE @areaId END
		AND
		reg.ID = CASE WHEN @regId IS NULL THEN reg.ID ELSE @regId END
		AND
		ria.ID = CASE WHEN @riaId IS NULL THEN ria.ID ELSE @riaId END
		AND
		cmn.ID IN (SELECT ID FROM #tmpAccess)
	ORDER BY
		area.NAME
		,reg.NAME
		,cmn.NAME

	DROP TABLE #tmpAccess
END
GO
/****** Object:  StoredProcedure [ter].[sp_getfulladdress]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		ghost1908
-- Create date: 01.02.2021
-- Description:	Get full address by id
-- =============================================
-- Modify date: 
-- Description: 
-- =============================================
CREATE PROCEDURE [ter].[sp_getfulladdress]
	@ID uniqueidentifier,
	@UserID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

	--CREATE TABLE #tmpAccess (ID uniqueidentifier)

	--INSERT INTO #tmpAccess
	--EXEC dbo.sp_getaddressaccessbyuser @UserID, N'Street'

	SELECT
		addr.ID
		,area.ID AS AREA_ID
		,area.NAME AS AREA_NAME
		,ria.ID AS REG_ARIA_ID
		,reg.ID AS REG_ID
		,reg.NAME AS REG_NAME
		--,reg.IS_CITY_REGION
		,cir.ID AS CMN_RIA_ID
		,cmn.ID AS CMN_ID
		,cmn.NAME AS CMN_NAME
		,cic.ID AS CITY_IN_CMN_ID
		,ctype.ID AS CITY_TYPE_ID
		,ctype.NAME AS CITY_TYPE_NAME
		,city.ID AS CITY_ID
		,city.NAME AS CITY_NAME
		,sic.ID AS STREET_IN_CITY_ID
		,stype.ID AS STREET_TYPE_ID
		,stype.NAME AS STREET_TYPE_NAME
		,strt.ID AS STREET_ID
		,strt.NAME AS STREET_NAME
		,bld.ID AS BLD_ID
		,bld.NUMBER AS BLD_NUMBER
		,addr.BLD_ISSUE
	FROM ter.[Address] addr
	JOIN ter.Building bld		ON bld.ID = addr.BLD_ID
	JOIN ter.StreetInCity sic	ON sic.ID = addr.SIC_ID
	JOIN ter.Street strt		ON strt.ID = sic.STR_ID
	JOIN ter.StreetType stype	ON stype.ID = strt.STRT_ID
	JOIN ter.CityInCommunity cic	ON cic.ID = sic.CIC_ID
	JOIN ter.City city			ON city.ID = cic.CITY_ID
	JOIN ter.CityType ctype		ON ctype.ID = city.CITYT_ID
	JOIN ter.CommunityInRegion cir	ON cir.ID = cic.CIR_ID
	JOIN ter.Community cmn		ON cmn.ID = cir.CMN_ID
	JOIN ter.RegionInArea ria	ON ria.ID=cir.RIA_ID
	JOIN ter.Region reg			ON reg.ID = ria.REG_ID
	JOIN ter.Area area			ON area.ID = ria.AREA_ID
	WHERE
		addr.ID = @ID
		--AND strt.ID IN (SELECT #tmpAccess.ID FROM #tmpAccess)

	--DROP TABLE #tmpAccess
END

GO
/****** Object:  StoredProcedure [ter].[sp_getprecinctlist]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		ghost1908
-- Create date: 19.10.2020
-- Description:	Весь список Участков и/или по громаде и/или по району и/или по области
-- =============================================
CREATE PROCEDURE [ter].[sp_getprecinctlist]
	@areaId uniqueidentifier = null,
	@regId uniqueidentifier = null,
	@cmnId uniqueidentifier = null
AS
BEGIN
	SET NOCOUNT ON;

    SELECT
		prct.ID
		,prct.NUMBER AS PRCT_NUMBER
		,pic.ID AS PRCT_CMN_ID
		,cmn.ID AS CMN_ID
		,cmn.NAME AS CMN_NAME
		,cmir.ID AS CMN_REG_ID
		,reg.ID AS REG_ID
		,reg.NAME AS REG_NAME
		,ria.ID AS REG_AREA_ID
		,area.ID AS AREA_ID
		,area.NAME AS AREA_NAME
	FROM ter.Precinct prct
	JOIN ter.PrecinctInCommunity pic
		ON pic.PRCT_ID = prct.ID
	JOIN ter.CommunityInRegion cmir
		ON cmir.ID = pic.CMIR_ID
	JOIN ter.Community cmn
		ON cmn.ID = cmir.CMN_ID
	JOIN ter.RegionInArea ria
		ON ria.ID = cmir.RIA_ID
	JOIN ter.Region reg
		ON reg.ID = ria.REG_ID
	JOIN ter.Area area
		ON area.ID = ria.AREA_ID
	WHERE
		area.ID = CASE WHEN @areaId IS NULL THEN area.ID ELSE @areaId END
		AND
		reg.ID = CASE WHEN @regId IS NULL THEN reg.ID ELSE @regId END
		AND
		cmn.ID = CASE WHEN @cmnId IS NULL THEN cmn.ID ELSE @cmnId END
	ORDER BY
		area.NAME
		,reg.NAME
		,cmn.NAME
END
GO
/****** Object:  StoredProcedure [ter].[sp_getregionlist]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		ghost1908
-- Create date: 10.08.2020
-- Description:	Получить список районов для пользователя
-- =============================================
-- Modify date: 11.08.2020
-- Description: Убран контроль доступа (пока)
-- =============================================
-- Modify date: 25.01.2021
-- Description: Изменени контроль доступа
-- =============================================
CREATE PROCEDURE [ter].[sp_getregionlist] 
	@UserID uniqueidentifier,
	@AreaID uniqueidentifier = NULL
AS
BEGIN
	SET NOCOUNT ON;

    CREATE TABLE #tmpAccess (ID uniqueidentifier)

	INSERT INTO #tmpAccess
	--EXEC dbo.sp_getaddressaccessbyuser @UserID, N'Region'
	EXEC cfg.sp_getaccessbyuser @UserID, N'ter.Region'

	SELECT
		ria.ID
		,ria.AREA_ID 
		,reg.ID AS REG_ID
		,reg.NAME
		,reg.IS_CITY_REGION
	FROM ter.Region reg
	JOIN #tmpAccess acl
		ON acl.ID = reg.ID
	JOIN ter.RegionInArea ria
		ON ria.REG_ID = reg.ID
	WHERE
		ria.AREA_ID = CASE WHEN @AreaID IS NULL THEN ria.AREA_ID ELSE @AreaID END
	ORDER BY
		reg.IS_CITY_REGION DESC, reg.NAME

	DROP TABLE #tmpAccess
END
GO
/****** Object:  StoredProcedure [ter].[sp_getsetaddressbystreet]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		ghost1908
-- Create date: 19.02.2021
-- Description:	Получить или создать id адреса по улице и номеру дома
-- =============================================
-- Modify date: 29.03.2021
-- Description: Добавлен Output
-- =============================================

CREATE PROCEDURE [ter].[sp_getsetaddressbystreet] 
	@sicId uniqueidentifier,
	@buildNumber nvarchar(10),
	@addrId uniqueidentifier OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @result uniqueidentifier
	DECLARE @buildId uniqueidentifier

	IF NOT EXISTS(SELECT * FROM ter.StreetInCity WHERE ID = @sicId)
		GOTO ProcedureEnd

    SET @buildId = (SELECT ID FROM ter.Building WHERE NUMBER = @buildNumber)

	IF @buildId IS NULL
	BEGIN
		SET @buildId = NEWID()
		INSERT INTO ter.Building VALUES ( @buildId, @buildNumber )
		SET @result = NEWID()
		INSERT INTO ter.Address VALUES ( @result, @sicId, @buildId, NULL)
		GOTO ProcedureEnd
	END

	SET @result = (SELECT ID FROM ter.Address WHERE SIC_ID = @sicId AND BLD_ID = @buildId)

	IF @result IS NULL
	BEGIN
		SET @result = NEWID()
		INSERT INTO ter.Address VALUES ( @result, @sicId, @buildId, NULL)
	END

	ProcedureEnd:

	IF @result IS NULL
		SELECT CAST(0x0 AS uniqueidentifier)
	ELSE
		SELECT @addrId = @result
END
GO
/****** Object:  StoredProcedure [ter].[sp_getstreetincity]    Script Date: 12/1/2024 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		ghost1908
-- Create date: 02.02.2021
-- Description:	Получить список Улиц в Городе для пользователя
-- =============================================
CREATE PROCEDURE [ter].[sp_getstreetincity] 
	@UserID uniqueidentifier,
	@CityID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

    CREATE TABLE #tmpAccess (ID uniqueidentifier)

	INSERT INTO #tmpAccess
	EXEC cfg.sp_getaccessbyuser @UserID, N'ter.StreetInCity'

	SELECT 
		sic.*
		,strt.NAME AS STR_NAME
		,st.ID AS STRT_ID
		,st.NAME AS STRT_NAME
	FROM ter.StreetInCity sic
	JOIN ter.Street strt
		ON strt.ID = sic.STR_ID
	JOIN ter.StreetType st
		ON st.ID = strt.STRT_ID
	WHERE
		sic.CIC_ID = @CityID
		AND sic.ID IN (SELECT ID FROM #tmpAccess)

	DROP TABLE #tmpAccess
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Должность' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Position'
GO
USE [master]
GO
ALTER DATABASE [PhoenixDB] SET  READ_WRITE 
GO
