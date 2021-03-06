USE [master]
GO
/****** Object:  Database [ProductPortfolioData]    Script Date: 27-05-2016 13:20:51 ******/
CREATE DATABASE [ProductPortfolioData]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ProductPortfolioData', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\ProductPortfolioData.mdf' , SIZE = 7168KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'ProductPortfolioData_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\ProductPortfolioData_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [ProductPortfolioData] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ProductPortfolioData].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ProductPortfolioData] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ProductPortfolioData] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ProductPortfolioData] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ProductPortfolioData] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ProductPortfolioData] SET ARITHABORT OFF 
GO
ALTER DATABASE [ProductPortfolioData] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ProductPortfolioData] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ProductPortfolioData] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ProductPortfolioData] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ProductPortfolioData] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ProductPortfolioData] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ProductPortfolioData] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ProductPortfolioData] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ProductPortfolioData] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ProductPortfolioData] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ProductPortfolioData] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ProductPortfolioData] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ProductPortfolioData] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ProductPortfolioData] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ProductPortfolioData] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ProductPortfolioData] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ProductPortfolioData] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ProductPortfolioData] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ProductPortfolioData] SET  MULTI_USER 
GO
ALTER DATABASE [ProductPortfolioData] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ProductPortfolioData] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ProductPortfolioData] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ProductPortfolioData] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [ProductPortfolioData] SET DELAYED_DURABILITY = DISABLED 
GO
USE [ProductPortfolioData]
GO
/****** Object:  Table [dbo].[Announcements]    Script Date: 27-05-2016 13:20:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Announcements](
	[Id] [nvarchar](128) NOT NULL,
	[Subject] [nvarchar](max) NULL,
	[ImageUrl] [nvarchar](max) NULL,
	[AnnouncementDate] [datetime] NOT NULL,
	[BlogUrl] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Version] [timestamp] NOT NULL,
	[CreatedAt] [datetimeoffset](7) NOT NULL,
	[UpdatedAt] [datetimeoffset](7) NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Announcements] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Categories]    Script Date: 27-05-2016 13:20:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[Id] [nvarchar](128) NOT NULL,
	[CategoryName] [nvarchar](max) NULL,
	[Version] [timestamp] NOT NULL,
	[CreatedAt] [datetimeoffset](7) NOT NULL,
	[UpdatedAt] [datetimeoffset](7) NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Categories] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FeatureInPreviews]    Script Date: 27-05-2016 13:20:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FeatureInPreviews](
	[Id] [nvarchar](128) NOT NULL,
	[FeatureName] [nvarchar](max) NULL,
	[ProductId] [nvarchar](max) NULL,
	[ProductName] [nvarchar](max) NULL,
	[FeatureDescription] [nvarchar](max) NULL,
	[ImageUrl] [nvarchar](max) NULL,
	[FeatureUpdateDate] [datetime] NULL,
	[FeatureReferenceUrl] [nvarchar](max) NULL,
	[Version] [timestamp] NOT NULL,
	[CreatedAt] [datetimeoffset](7) NOT NULL,
	[UpdatedAt] [datetimeoffset](7) NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.FeatureInPreviews] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Features]    Script Date: 27-05-2016 13:20:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Features](
	[Id] [nvarchar](128) NOT NULL,
	[FeatureName] [nvarchar](max) NULL,
	[ProductId] [nvarchar](max) NULL,
	[ProductName] [nvarchar](max) NULL,
	[FeatureDescription] [nvarchar](max) NULL,
	[ImageUrl] [nvarchar](max) NULL,
	[FeatureUpdateDate] [datetime] NULL,
	[FeatureReferenceUrl] [nvarchar](max) NULL,
	[Version] [timestamp] NOT NULL,
	[CreatedAt] [datetimeoffset](7) NOT NULL,
	[UpdatedAt] [datetimeoffset](7) NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Features] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProductInPreviews]    Script Date: 27-05-2016 13:20:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductInPreviews](
	[Id] [nvarchar](128) NOT NULL,
	[ProductName] [nvarchar](max) NULL,
	[CategoryName] [nvarchar](max) NULL,
	[CurrentVersion] [nvarchar](max) NULL,
	[ProductDescription] [nvarchar](max) NULL,
	[ProductHomePageUrl] [nvarchar](max) NULL,
	[AnnouncementDate] [datetime] NOT NULL,
	[Version] [timestamp] NOT NULL,
	[CreatedAt] [datetimeoffset](7) NOT NULL,
	[UpdatedAt] [datetimeoffset](7) NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.ProductInPreviews] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Products]    Script Date: 27-05-2016 13:20:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [nvarchar](128) NOT NULL,
	[ProductDescription] [nvarchar](max) NULL,
	[ProductName] [nvarchar](max) NULL,
	[CurrentVersion] [nvarchar](max) NULL,
	[CategoryName] [nvarchar](max) NULL,
	[LifecycleRelevant] [bit] NULL,
	[HasPreviewVersion] [bit] NULL,
	[BlogFeedUrl] [nvarchar](max) NULL,
	[ProductHomePageUrl] [nvarchar](max) NULL,
	[MsdnUrl] [nvarchar](max) NULL,
	[TechnetUrl] [nvarchar](max) NULL,
	[Version] [timestamp] NOT NULL,
	[CreatedAt] [datetimeoffset](7) NOT NULL,
	[UpdatedAt] [datetimeoffset](7) NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Products] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[Announcements] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Announcements] ADD  DEFAULT (sysutcdatetime()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Categories] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Categories] ADD  DEFAULT (sysutcdatetime()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[FeatureInPreviews] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[FeatureInPreviews] ADD  DEFAULT (sysutcdatetime()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Features] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Features] ADD  DEFAULT (sysutcdatetime()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[ProductInPreviews] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[ProductInPreviews] ADD  DEFAULT (sysutcdatetime()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Products] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Products] ADD  CONSTRAINT [DF_Products_LifecycleRelevant]  DEFAULT ((0)) FOR [LifecycleRelevant]
GO
ALTER TABLE [dbo].[Products] ADD  CONSTRAINT [DF_Products_HasPreviewVersion]  DEFAULT ((0)) FOR [HasPreviewVersion]
GO
ALTER TABLE [dbo].[Products] ADD  DEFAULT (sysutcdatetime()) FOR [CreatedAt]
GO
USE [master]
GO
ALTER DATABASE [ProductPortfolioData] SET  READ_WRITE 
GO
