USE [master]
GO
/****** Object:  Database [APP.DB]    Script Date: 2023-07-24 오전 1:20:39 ******/
CREATE DATABASE [APP.DB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'APP.DB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\APP.DB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'APP.DB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\APP.DB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [APP.DB] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [APP.DB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [APP.DB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [APP.DB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [APP.DB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [APP.DB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [APP.DB] SET ARITHABORT OFF 
GO
ALTER DATABASE [APP.DB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [APP.DB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [APP.DB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [APP.DB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [APP.DB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [APP.DB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [APP.DB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [APP.DB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [APP.DB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [APP.DB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [APP.DB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [APP.DB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [APP.DB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [APP.DB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [APP.DB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [APP.DB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [APP.DB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [APP.DB] SET RECOVERY FULL 
GO
ALTER DATABASE [APP.DB] SET  MULTI_USER 
GO
ALTER DATABASE [APP.DB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [APP.DB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [APP.DB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [APP.DB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [APP.DB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [APP.DB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'APP.DB', N'ON'
GO
ALTER DATABASE [APP.DB] SET QUERY_STORE = ON
GO
ALTER DATABASE [APP.DB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [APP.DB]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 2023-07-24 오전 1:20:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[USERS]    Script Date: 2023-07-24 오전 1:20:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[USERS](
	[USER_CD] [nvarchar](450) NOT NULL,
	[USER_NM] [nvarchar](max) NOT NULL,
	[USER_DEPT] [nvarchar](max) NULL,
	[USER_EMAIL] [nvarchar](max) NOT NULL,
	[USER_CONTACT] [nvarchar](max) NULL,
	[REMARK] [nvarchar](max) NULL,
 CONSTRAINT [PK_USERS] PRIMARY KEY CLUSTERED 
(
	[USER_CD] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [APP.DB] SET  READ_WRITE 
GO
