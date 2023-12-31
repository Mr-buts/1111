USE [master]
GO
/****** Object:  Database [HorseSectionModel]    Script Date: 30.01.2023 15:00:51 ******/
CREATE DATABASE [HorseSectionModel]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'HorseSectionModel', FILENAME = N'E:\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\HorseSectionModel.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'HorseSectionModel_log', FILENAME = N'E:\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\HorseSectionModel_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [HorseSectionModel] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [HorseSectionModel].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [HorseSectionModel] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [HorseSectionModel] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [HorseSectionModel] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [HorseSectionModel] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [HorseSectionModel] SET ARITHABORT OFF 
GO
ALTER DATABASE [HorseSectionModel] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [HorseSectionModel] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [HorseSectionModel] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [HorseSectionModel] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [HorseSectionModel] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [HorseSectionModel] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [HorseSectionModel] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [HorseSectionModel] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [HorseSectionModel] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [HorseSectionModel] SET  DISABLE_BROKER 
GO
ALTER DATABASE [HorseSectionModel] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [HorseSectionModel] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [HorseSectionModel] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [HorseSectionModel] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [HorseSectionModel] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [HorseSectionModel] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [HorseSectionModel] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [HorseSectionModel] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [HorseSectionModel] SET  MULTI_USER 
GO
ALTER DATABASE [HorseSectionModel] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [HorseSectionModel] SET DB_CHAINING OFF 
GO
ALTER DATABASE [HorseSectionModel] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [HorseSectionModel] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [HorseSectionModel] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [HorseSectionModel] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [HorseSectionModel] SET QUERY_STORE = OFF
GO
USE [HorseSectionModel]
GO
/****** Object:  Table [dbo].[record]    Script Date: 30.01.2023 15:00:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[record](
	[recordid] [int] IDENTITY(7,1) NOT NULL,
	[workdayid] [int] NOT NULL,
	[userid] [int] NOT NULL,
	[numperson] [int] NOT NULL,
 CONSTRAINT [PK_record] PRIMARY KEY CLUSTERED 
(
	[recordid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[user]    Script Date: 30.01.2023 15:00:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user](
	[userid] [int] IDENTITY(4,1) NOT NULL,
	[surname] [nvarchar](50) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[patronymic] [nvarchar](50) NULL,
	[phone] [nchar](11) NOT NULL,
	[password] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_user] PRIMARY KEY CLUSTERED 
(
	[userid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[workday]    Script Date: 30.01.2023 15:00:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[workday](
	[workdayid] [int] NOT NULL,
	[workdaydate] [date] NULL,
 CONSTRAINT [PK_workday] PRIMARY KEY CLUSTERED 
(
	[workdayid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[record] ON 

INSERT [dbo].[record] ([recordid], [workdayid], [userid], [numperson]) VALUES (1, 1, 2, 2)
INSERT [dbo].[record] ([recordid], [workdayid], [userid], [numperson]) VALUES (2, 1, 3, 1)
INSERT [dbo].[record] ([recordid], [workdayid], [userid], [numperson]) VALUES (3, 2, 1, 1)
INSERT [dbo].[record] ([recordid], [workdayid], [userid], [numperson]) VALUES (4, 2, 2, 2)
INSERT [dbo].[record] ([recordid], [workdayid], [userid], [numperson]) VALUES (5, 2, 3, 1)
INSERT [dbo].[record] ([recordid], [workdayid], [userid], [numperson]) VALUES (6, 3, 1, 1)
INSERT [dbo].[record] ([recordid], [workdayid], [userid], [numperson]) VALUES (7, 3, 2, 1)
INSERT [dbo].[record] ([recordid], [workdayid], [userid], [numperson]) VALUES (8, 3, 5, 2)
INSERT [dbo].[record] ([recordid], [workdayid], [userid], [numperson]) VALUES (10, 4, 1, 3)
INSERT [dbo].[record] ([recordid], [workdayid], [userid], [numperson]) VALUES (11, 4, 2, 2)
SET IDENTITY_INSERT [dbo].[record] OFF
GO
SET IDENTITY_INSERT [dbo].[user] ON 

INSERT [dbo].[user] ([userid], [surname], [name], [patronymic], [phone], [password]) VALUES (1, N'Осипов', N'Сергей', NULL, N'79775162297', N'12kasta12_')
INSERT [dbo].[user] ([userid], [surname], [name], [patronymic], [phone], [password]) VALUES (2, N'Петров', N'Андрей', N'Георгиевич', N'79775739821', N'wad:ila97')
INSERT [dbo].[user] ([userid], [surname], [name], [patronymic], [phone], [password]) VALUES (3, N'Савина', N'Дарья', N'Викторовна', N'79777233523', N'io%2ud3hfq')
INSERT [dbo].[user] ([userid], [surname], [name], [patronymic], [phone], [password]) VALUES (5, N'Овчагин', N'Данил', N'Ахметов', N'79772539058', N'Zetralen_35')
SET IDENTITY_INSERT [dbo].[user] OFF
GO
INSERT [dbo].[workday] ([workdayid], [workdaydate]) VALUES (1, CAST(N'2023-01-27' AS Date))
INSERT [dbo].[workday] ([workdayid], [workdaydate]) VALUES (2, CAST(N'2023-02-01' AS Date))
INSERT [dbo].[workday] ([workdayid], [workdaydate]) VALUES (3, CAST(N'2023-02-02' AS Date))
INSERT [dbo].[workday] ([workdayid], [workdaydate]) VALUES (4, CAST(N'2023-02-03' AS Date))
GO
ALTER TABLE [dbo].[record]  WITH CHECK ADD  CONSTRAINT [FK_record_user] FOREIGN KEY([userid])
REFERENCES [dbo].[user] ([userid])
GO
ALTER TABLE [dbo].[record] CHECK CONSTRAINT [FK_record_user]
GO
ALTER TABLE [dbo].[record]  WITH CHECK ADD  CONSTRAINT [FK_record_workday] FOREIGN KEY([workdayid])
REFERENCES [dbo].[workday] ([workdayid])
GO
ALTER TABLE [dbo].[record] CHECK CONSTRAINT [FK_record_workday]
GO
USE [master]
GO
ALTER DATABASE [HorseSectionModel] SET  READ_WRITE 
GO
