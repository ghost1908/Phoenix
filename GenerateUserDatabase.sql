USE [master]
GO
/****** Object:  Database [PhoenixIdentity]    Script Date: 12/1/2024 8:10:59 PM ******/
CREATE DATABASE [PhoenixIdentity]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PhoenixIdentity', FILENAME = N'/var/opt/mssql/data/PhoenixIdentity.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'PhoenixIdentity_log', FILENAME = N'/var/opt/mssql/data/PhoenixIdentity_log.ldf' , SIZE = 2816KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [PhoenixIdentity] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PhoenixIdentity].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PhoenixIdentity] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PhoenixIdentity] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PhoenixIdentity] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PhoenixIdentity] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PhoenixIdentity] SET ARITHABORT OFF 
GO
ALTER DATABASE [PhoenixIdentity] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PhoenixIdentity] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PhoenixIdentity] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PhoenixIdentity] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PhoenixIdentity] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PhoenixIdentity] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PhoenixIdentity] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PhoenixIdentity] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PhoenixIdentity] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PhoenixIdentity] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PhoenixIdentity] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PhoenixIdentity] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PhoenixIdentity] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PhoenixIdentity] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PhoenixIdentity] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PhoenixIdentity] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PhoenixIdentity] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PhoenixIdentity] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [PhoenixIdentity] SET  MULTI_USER 
GO
ALTER DATABASE [PhoenixIdentity] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PhoenixIdentity] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PhoenixIdentity] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PhoenixIdentity] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [PhoenixIdentity] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PhoenixIdentity] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'PhoenixIdentity', N'ON'
GO
ALTER DATABASE [PhoenixIdentity] SET QUERY_STORE = OFF
GO
USE [PhoenixIdentity]
GO
/****** Object:  Table [dbo].[NetCoreClaims]    Script Date: 12/1/2024 8:11:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NetCoreClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_NetCoreClaim] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NetCoreRoles]    Script Date: 12/1/2024 8:11:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NetCoreRoles](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[NormalizedName] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_NetCoreRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NetCoreUserRoles]    Script Date: 12/1/2024 8:11:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NetCoreUserRoles](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_NetCoreUserRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NetCoreUsers]    Script Date: 12/1/2024 8:11:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NetCoreUsers](
	[Id] [uniqueidentifier] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
	[NormalizedUserName] [nvarchar](max) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_NetCoreUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[NetCoreUsers] ADD  CONSTRAINT [DF_NetCoreUsers_PhoneNumberConfirmed]  DEFAULT ((1)) FOR [PhoneNumberConfirmed]
GO
ALTER TABLE [dbo].[NetCoreUsers] ADD  CONSTRAINT [DF_NetCoreUsers_TwoFactorEnabled]  DEFAULT ((0)) FOR [TwoFactorEnabled]
GO
ALTER TABLE [dbo].[NetCoreUsers] ADD  CONSTRAINT [DF_NetCoreUsers_LockoutEnabled]  DEFAULT ((1)) FOR [LockoutEnabled]
GO
ALTER TABLE [dbo].[NetCoreUsers] ADD  CONSTRAINT [DF_NetCoreUsers_AccessFailedCount]  DEFAULT ((0)) FOR [AccessFailedCount]
GO
ALTER TABLE [dbo].[NetCoreClaims]  WITH CHECK ADD  CONSTRAINT [FK_NetCoreClaims_NetCoreUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[NetCoreUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[NetCoreClaims] CHECK CONSTRAINT [FK_NetCoreClaims_NetCoreUsers]
GO
USE [master]
GO
ALTER DATABASE [PhoenixIdentity] SET  READ_WRITE 
GO
