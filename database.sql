USE [master]
GO
/****** Object:  Database [auction_house]    Script Date: 2/2/2019 11:03:06 PM ******/
CREATE DATABASE [auction_house]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'auction_house', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\auction_house.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'auction_house_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\auction_house_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [auction_house] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [auction_house].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [auction_house] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [auction_house] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [auction_house] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [auction_house] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [auction_house] SET ARITHABORT OFF 
GO
ALTER DATABASE [auction_house] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [auction_house] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [auction_house] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [auction_house] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [auction_house] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [auction_house] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [auction_house] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [auction_house] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [auction_house] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [auction_house] SET  DISABLE_BROKER 
GO
ALTER DATABASE [auction_house] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [auction_house] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [auction_house] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [auction_house] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [auction_house] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [auction_house] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [auction_house] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [auction_house] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [auction_house] SET  MULTI_USER 
GO
ALTER DATABASE [auction_house] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [auction_house] SET DB_CHAINING OFF 
GO
ALTER DATABASE [auction_house] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [auction_house] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [auction_house] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [auction_house] SET QUERY_STORE = OFF
GO
USE [auction_house]
GO
/****** Object:  Table [dbo].[Auction]    Script Date: 2/2/2019 11:03:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Auction](
	[id] [uniqueidentifier] NOT NULL,
	[name] [varchar](100) NOT NULL,
	[starting_price] [real] NOT NULL,
	[duration] [int] NOT NULL,
	[owner] [varchar](50) NOT NULL,
	[description] [varchar](500) NOT NULL,
	[state] [nvarchar](50) NOT NULL,
	[created] [datetime2](7) NOT NULL,
	[opened] [datetime2](7) NULL,
	[closed] [datetime2](7) NULL,
	[won] [varchar](50) NULL,
 CONSTRAINT [PK_Auction] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bid]    Script Date: 2/2/2019 11:03:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bid](
	[id] [uniqueidentifier] NOT NULL,
	[auction_id] [uniqueidentifier] NOT NULL,
	[bidder] [varchar](50) NOT NULL,
	[created] [datetime2](7) NOT NULL,
	[amount] [real] NOT NULL,
 CONSTRAINT [PK_Bid_1] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemParameters]    Script Date: 2/2/2019 11:03:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemParameters](
	[id] [int] NOT NULL,
	[N] [int] NOT NULL,
	[D] [int] NOT NULL,
	[S] [real] NOT NULL,
	[G] [real] NOT NULL,
	[P] [real] NOT NULL,
	[C] [nchar](3) NOT NULL,
	[T] [real] NOT NULL,
 CONSTRAINT [PK_SystemParameters] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TokenOrder]    Script Date: 2/2/2019 11:03:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TokenOrder](
	[id] [uniqueidentifier] NOT NULL,
	[orderer] [varchar](50) NOT NULL,
	[amount] [real] NOT NULL,
	[price] [real] NOT NULL,
	[state] [nvarchar](9) NOT NULL,
 CONSTRAINT [PK_TokenOrder_1] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 2/2/2019 11:03:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[email] [varchar](50) NOT NULL,
	[password] [varchar](50) NOT NULL,
	[first_name] [varchar](50) NOT NULL,
	[last_name] [varchar](50) NOT NULL,
	[tokens_amount] [real] NOT NULL,
	[is_administrator] [tinyint] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Auction] ([id], [name], [starting_price], [duration], [owner], [description], [state], [created], [opened], [closed], [won]) VALUES (N'03d91632-650c-4d1b-b1b7-1223e77c8d64', N'Pedjin laptop', 200, 86400, N'momaznikolic96@gmail.com', N'Lep laptop zaista', N'READY', CAST(N'2019-02-02T20:52:09.2649897' AS DateTime2), NULL, NULL, NULL)
INSERT [dbo].[Auction] ([id], [name], [starting_price], [duration], [owner], [description], [state], [created], [opened], [closed], [won]) VALUES (N'52fc2289-234e-4d81-8044-8e6a543796de', N'Momin laptop', 200, 600, N'momaznikolic96@gmail.com', N'Jako jako jako jako lep laptop', N'COMPLETED', CAST(N'2019-02-01T02:53:48.8636386' AS DateTime2), CAST(N'2019-02-01T02:53:52.0044703' AS DateTime2), CAST(N'2019-02-01T03:03:52.0044703' AS DateTime2), N'dragan96@gmail.com')
INSERT [dbo].[Auction] ([id], [name], [starting_price], [duration], [owner], [description], [state], [created], [opened], [closed], [won]) VALUES (N'366b5038-665d-4ef6-acd1-94bf8ab14b05', N'Slusalice jako lepe', 30, 86400, N'dragan96@gmail.com', N'Mnogo mnogo jako lepe dobre slusalice vrh vrhova omg', N'COMPLETED', CAST(N'2019-02-01T04:14:05.3276431' AS DateTime2), CAST(N'2019-02-01T04:14:16.0170680' AS DateTime2), CAST(N'2019-02-02T04:14:16.0170680' AS DateTime2), N'momaznikolic96@gmail.com')
INSERT [dbo].[Auction] ([id], [name], [starting_price], [duration], [owner], [description], [state], [created], [opened], [closed], [won]) VALUES (N'b12a9262-f5aa-439e-ac68-b5287e3874d1', N'Momin ranac', 50, 259200, N'momaznikolic96@gmail.com', N'Jejej jeej ej jejejej', N'OPENED', CAST(N'2019-02-02T16:39:02.5668565' AS DateTime2), CAST(N'2019-02-02T16:39:05.0947271' AS DateTime2), NULL, NULL)
INSERT [dbo].[Auction] ([id], [name], [starting_price], [duration], [owner], [description], [state], [created], [opened], [closed], [won]) VALUES (N'd3082650-bf02-415d-9760-d01442b31b87', N'Lukine patike', 50, 18000, N'luka@gmail.com', N'Jako jako jako jako jako mali broj ljudi je organizovan', N'COMPLETED', CAST(N'2019-02-01T01:54:35.4341830' AS DateTime2), CAST(N'2019-02-01T02:06:52.7240093' AS DateTime2), CAST(N'2019-02-01T07:06:52.7240093' AS DateTime2), N'dragan96@gmail.com')
INSERT [dbo].[Auction] ([id], [name], [starting_price], [duration], [owner], [description], [state], [created], [opened], [closed], [won]) VALUES (N'816ff72a-3207-474e-b5a8-e7293726e4d0', N'Momine patike', 50, 14400, N'momaznikolic96@gmail.com', N'Vrh!', N'READY', CAST(N'2019-02-02T22:31:37.3529004' AS DateTime2), NULL, NULL, NULL)
INSERT [dbo].[Bid] ([id], [auction_id], [bidder], [created], [amount]) VALUES (N'815c7f90-8676-41e3-b14f-0b0dc53f1fc6', N'366b5038-665d-4ef6-acd1-94bf8ab14b05', N'luka@gmail.com', CAST(N'2019-02-01T17:35:01.1884685' AS DateTime2), 105)
INSERT [dbo].[Bid] ([id], [auction_id], [bidder], [created], [amount]) VALUES (N'3b9a0884-b382-45f1-a35f-0bb957dad5d1', N'b12a9262-f5aa-439e-ac68-b5287e3874d1', N'luka@gmail.com', CAST(N'2019-02-02T22:45:38.5581208' AS DateTime2), 55)
INSERT [dbo].[Bid] ([id], [auction_id], [bidder], [created], [amount]) VALUES (N'fe871eeb-7130-4055-972b-19c67fcd6202', N'366b5038-665d-4ef6-acd1-94bf8ab14b05', N'luka@gmail.com', CAST(N'2019-02-01T15:39:15.3518009' AS DateTime2), 65)
INSERT [dbo].[Bid] ([id], [auction_id], [bidder], [created], [amount]) VALUES (N'3e55b047-2d4c-4676-8873-1c36b7db129c', N'366b5038-665d-4ef6-acd1-94bf8ab14b05', N'luka@gmail.com', CAST(N'2019-02-02T03:12:19.0619711' AS DateTime2), 146)
INSERT [dbo].[Bid] ([id], [auction_id], [bidder], [created], [amount]) VALUES (N'1784fe34-5aba-46b7-88bc-1e6373c35fc7', N'd3082650-bf02-415d-9760-d01442b31b87', N'momaznikolic96@gmail.com', CAST(N'2019-02-01T02:55:58.8506315' AS DateTime2), 70)
INSERT [dbo].[Bid] ([id], [auction_id], [bidder], [created], [amount]) VALUES (N'a6ad29bf-ffc1-4efb-9683-26ae45ae8778', N'd3082650-bf02-415d-9760-d01442b31b87', N'dragan96@gmail.com', CAST(N'2019-02-01T03:25:48.6121676' AS DateTime2), 85)
INSERT [dbo].[Bid] ([id], [auction_id], [bidder], [created], [amount]) VALUES (N'c190bb04-97d9-454f-b539-2b02e37ee58f', N'366b5038-665d-4ef6-acd1-94bf8ab14b05', N'momaznikolic96@gmail.com', CAST(N'2019-02-01T18:38:07.8737871' AS DateTime2), 120)
INSERT [dbo].[Bid] ([id], [auction_id], [bidder], [created], [amount]) VALUES (N'15286a98-7a52-478f-b352-3126f582a0f7', N'366b5038-665d-4ef6-acd1-94bf8ab14b05', N'momaznikolic96@gmail.com', CAST(N'2019-02-01T04:16:36.1098912' AS DateTime2), 60)
INSERT [dbo].[Bid] ([id], [auction_id], [bidder], [created], [amount]) VALUES (N'bb872106-bd03-4b45-8b5b-33720bb9b0f0', N'366b5038-665d-4ef6-acd1-94bf8ab14b05', N'momaznikolic96@gmail.com', CAST(N'2019-02-01T17:30:27.1459581' AS DateTime2), 100)
INSERT [dbo].[Bid] ([id], [auction_id], [bidder], [created], [amount]) VALUES (N'501d0a00-a650-46cd-adb1-3cdae93c5567', N'366b5038-665d-4ef6-acd1-94bf8ab14b05', N'momaznikolic96@gmail.com', CAST(N'2019-02-01T17:26:24.8110190' AS DateTime2), 90)
INSERT [dbo].[Bid] ([id], [auction_id], [bidder], [created], [amount]) VALUES (N'd1e6b77f-d31e-4de9-ae5a-3da362454415', N'd3082650-bf02-415d-9760-d01442b31b87', N'dragan96@gmail.com', CAST(N'2019-02-01T02:52:32.2527683' AS DateTime2), 65)
INSERT [dbo].[Bid] ([id], [auction_id], [bidder], [created], [amount]) VALUES (N'37c58f9d-788e-4c86-b74c-3dd1d88e15d4', N'366b5038-665d-4ef6-acd1-94bf8ab14b05', N'luka@gmail.com', CAST(N'2019-02-01T17:28:34.6390223' AS DateTime2), 95)
INSERT [dbo].[Bid] ([id], [auction_id], [bidder], [created], [amount]) VALUES (N'9557c5e0-b185-4aee-8ae8-42be7a3527a3', N'd3082650-bf02-415d-9760-d01442b31b87', N'momaznikolic96@gmail.com', CAST(N'2019-02-01T02:23:10.9527066' AS DateTime2), 55)
INSERT [dbo].[Bid] ([id], [auction_id], [bidder], [created], [amount]) VALUES (N'631a9ee5-a43c-4228-909c-4f38d15fa0ed', N'366b5038-665d-4ef6-acd1-94bf8ab14b05', N'momaznikolic96@gmail.com', CAST(N'2019-02-01T17:36:58.5101442' AS DateTime2), 110)
INSERT [dbo].[Bid] ([id], [auction_id], [bidder], [created], [amount]) VALUES (N'721932fc-385f-4cfd-9385-53ef3994c3d8', N'366b5038-665d-4ef6-acd1-94bf8ab14b05', N'momaznikolic96@gmail.com', CAST(N'2019-02-01T16:35:53.9288525' AS DateTime2), 70)
INSERT [dbo].[Bid] ([id], [auction_id], [bidder], [created], [amount]) VALUES (N'3fba7b07-499b-42a2-b5d2-55c3653edea2', N'366b5038-665d-4ef6-acd1-94bf8ab14b05', N'momaznikolic96@gmail.com', CAST(N'2019-02-01T17:17:32.2285202' AS DateTime2), 80)
INSERT [dbo].[Bid] ([id], [auction_id], [bidder], [created], [amount]) VALUES (N'9ce133b9-b930-4607-91a9-585fac2d29cd', N'366b5038-665d-4ef6-acd1-94bf8ab14b05', N'luka@gmail.com', CAST(N'2019-02-01T17:25:45.4661406' AS DateTime2), 85)
INSERT [dbo].[Bid] ([id], [auction_id], [bidder], [created], [amount]) VALUES (N'90671cb8-1050-4a6a-a970-5a71957d1cb7', N'366b5038-665d-4ef6-acd1-94bf8ab14b05', N'momaznikolic96@gmail.com', CAST(N'2019-02-01T18:48:17.6554218' AS DateTime2), 130)
INSERT [dbo].[Bid] ([id], [auction_id], [bidder], [created], [amount]) VALUES (N'2db93be5-0aac-42e1-af0a-6d68ae8e7aa7', N'366b5038-665d-4ef6-acd1-94bf8ab14b05', N'momaznikolic96@gmail.com', CAST(N'2019-02-02T03:11:57.6698964' AS DateTime2), 145)
INSERT [dbo].[Bid] ([id], [auction_id], [bidder], [created], [amount]) VALUES (N'dc2d8872-17b6-4e0b-84b4-6eb0264d5ba4', N'd3082650-bf02-415d-9760-d01442b31b87', N'momaznikolic96@gmail.com', CAST(N'2019-02-01T03:27:04.4460801' AS DateTime2), 100)
INSERT [dbo].[Bid] ([id], [auction_id], [bidder], [created], [amount]) VALUES (N'78f72bab-e9f7-4bba-a637-947e11ec37a0', N'366b5038-665d-4ef6-acd1-94bf8ab14b05', N'momaznikolic96@gmail.com', CAST(N'2019-02-01T19:28:27.7294377' AS DateTime2), 140)
INSERT [dbo].[Bid] ([id], [auction_id], [bidder], [created], [amount]) VALUES (N'6a2289a4-ed71-4cd8-99ae-a3be6a8adcc0', N'366b5038-665d-4ef6-acd1-94bf8ab14b05', N'luka@gmail.com', CAST(N'2019-02-01T18:39:46.6119884' AS DateTime2), 125)
INSERT [dbo].[Bid] ([id], [auction_id], [bidder], [created], [amount]) VALUES (N'f4c74f1e-b862-40b1-b02e-b0ce836f8cc9', N'366b5038-665d-4ef6-acd1-94bf8ab14b05', N'luka@gmail.com', CAST(N'2019-02-01T16:58:19.7798493' AS DateTime2), 75)
INSERT [dbo].[Bid] ([id], [auction_id], [bidder], [created], [amount]) VALUES (N'fb1c54f6-8b5f-454a-bdf2-b4e4587b68dc', N'366b5038-665d-4ef6-acd1-94bf8ab14b05', N'luka@gmail.com', CAST(N'2019-02-01T18:31:11.2243784' AS DateTime2), 115)
INSERT [dbo].[Bid] ([id], [auction_id], [bidder], [created], [amount]) VALUES (N'c0377dcf-c1a9-415b-b73d-b8c0a9663759', N'd3082650-bf02-415d-9760-d01442b31b87', N'dragan96@gmail.com', CAST(N'2019-02-01T04:59:10.3519263' AS DateTime2), 105)
INSERT [dbo].[Bid] ([id], [auction_id], [bidder], [created], [amount]) VALUES (N'75cf7126-72b9-41ee-ac1c-c07a36e4d9cb', N'366b5038-665d-4ef6-acd1-94bf8ab14b05', N'momaznikolic96@gmail.com', CAST(N'2019-02-02T03:36:16.6006584' AS DateTime2), 147)
INSERT [dbo].[Bid] ([id], [auction_id], [bidder], [created], [amount]) VALUES (N'da23d234-2861-47aa-bba6-d33d36027189', N'366b5038-665d-4ef6-acd1-94bf8ab14b05', N'luka@gmail.com', CAST(N'2019-02-01T19:31:43.3048882' AS DateTime2), 141)
INSERT [dbo].[Bid] ([id], [auction_id], [bidder], [created], [amount]) VALUES (N'46348a92-5eb7-456b-a8dd-d69b059e9b08', N'366b5038-665d-4ef6-acd1-94bf8ab14b05', N'luka@gmail.com', CAST(N'2019-02-01T18:49:24.6688357' AS DateTime2), 135)
INSERT [dbo].[Bid] ([id], [auction_id], [bidder], [created], [amount]) VALUES (N'8c2faea4-de0a-436b-b7e5-f7a41b49cee6', N'366b5038-665d-4ef6-acd1-94bf8ab14b05', N'luka@gmail.com', CAST(N'2019-02-02T03:37:36.1153734' AS DateTime2), 148)
INSERT [dbo].[Bid] ([id], [auction_id], [bidder], [created], [amount]) VALUES (N'4f44f153-5e8c-4cf9-89c6-fbef4edd6f33', N'366b5038-665d-4ef6-acd1-94bf8ab14b05', N'momaznikolic96@gmail.com', CAST(N'2019-02-02T03:42:34.5665406' AS DateTime2), 149)
INSERT [dbo].[Bid] ([id], [auction_id], [bidder], [created], [amount]) VALUES (N'0af00a0f-6e5a-4654-a1ac-fc0a281b93e4', N'52fc2289-234e-4d81-8044-8e6a543796de', N'dragan96@gmail.com', CAST(N'2019-02-01T02:55:16.1359375' AS DateTime2), 210)
INSERT [dbo].[SystemParameters] ([id], [N], [D], [S], [G], [P], [C], [T]) VALUES (1, 8, 86400, 30, 50, 100, N'USD', 1)
INSERT [dbo].[TokenOrder] ([id], [orderer], [amount], [price], [state]) VALUES (N'ab777f1a-32e1-4b93-b36c-00e248c084f1', N'momaznikolic96@gmail.com', 100, 100, N'COMPLETED')
INSERT [dbo].[TokenOrder] ([id], [orderer], [amount], [price], [state]) VALUES (N'ed9303aa-3376-4860-a2ec-1e181054014e', N'dragan96@gmail.com', 50, 50, N'COMPLETED')
INSERT [dbo].[TokenOrder] ([id], [orderer], [amount], [price], [state]) VALUES (N'5267524e-c48d-4d96-93df-310185fd300d', N'momaznikolic96@gmail.com', 100, 100, N'COMPLETED')
INSERT [dbo].[TokenOrder] ([id], [orderer], [amount], [price], [state]) VALUES (N'f28a8391-3f8f-4afb-9e91-356c21a2ef2b', N'dragan96@gmail.com', 100, 100, N'COMPLETED')
INSERT [dbo].[TokenOrder] ([id], [orderer], [amount], [price], [state]) VALUES (N'04d34216-d592-4f68-9ed2-37c413c6f812', N'dragan96@gmail.com', 100, 100, N'COMPLETED')
INSERT [dbo].[TokenOrder] ([id], [orderer], [amount], [price], [state]) VALUES (N'd6da7aa4-f845-475e-9b3d-40c1a16e4481', N'momaznikolic96@gmail.com', 100, 100, N'COMPLETED')
INSERT [dbo].[TokenOrder] ([id], [orderer], [amount], [price], [state]) VALUES (N'aafa99f4-77c0-4367-8c58-42bdfd6d94f5', N'dragan96@gmail.com', 100, 100, N'COMPLETED')
INSERT [dbo].[TokenOrder] ([id], [orderer], [amount], [price], [state]) VALUES (N'9121f034-d4ee-4da2-97e1-5bf28a61d5e2', N'momaznikolic96@gmail.com', 100, 100, N'COMPLETED')
INSERT [dbo].[TokenOrder] ([id], [orderer], [amount], [price], [state]) VALUES (N'5a5ebabc-16bb-41fa-b0d9-7f25803580ea', N'luka@gmail.com', 50, 50, N'COMPLETED')
INSERT [dbo].[TokenOrder] ([id], [orderer], [amount], [price], [state]) VALUES (N'a1f942d1-2b35-4ef1-9a12-8aa84ab37127', N'momaznikolic96@gmail.com', 100, 100, N'COMPLETED')
INSERT [dbo].[TokenOrder] ([id], [orderer], [amount], [price], [state]) VALUES (N'6f7d358d-7b1e-4245-95b3-b8c0942aeb27', N'luka@gmail.com', 100, 100, N'COMPLETED')
INSERT [dbo].[TokenOrder] ([id], [orderer], [amount], [price], [state]) VALUES (N'64aa78b8-3ce2-44eb-8643-b92c07b4277f', N'luka@gmail.com', 30, 30, N'COMPLETED')
INSERT [dbo].[TokenOrder] ([id], [orderer], [amount], [price], [state]) VALUES (N'4914ded2-f6b6-4137-958a-c8c5f6c8409e', N'momaznikolic96@gmail.com', 50, 50, N'COMPLETED')
INSERT [dbo].[TokenOrder] ([id], [orderer], [amount], [price], [state]) VALUES (N'7ed3ba19-5da4-4065-ada5-d47ae8906471', N'luka@gmail.com', 100, 100, N'COMPLETED')
INSERT [dbo].[TokenOrder] ([id], [orderer], [amount], [price], [state]) VALUES (N'09fc0a6e-2dfe-4751-9aea-d679df07823a', N'momaznikolic96@gmail.com', 100, 100, N'COMPLETED')
INSERT [dbo].[User] ([email], [password], [first_name], [last_name], [tokens_amount], [is_administrator]) VALUES (N'dragan96@gmail.com', N'F1-DC-73-5E-E3-58-16-93-48-9E-AF-28-60-88-B9-16', N'Dragan', N'Jovanovic', 184, 0)
INSERT [dbo].[User] ([email], [password], [first_name], [last_name], [tokens_amount], [is_administrator]) VALUES (N'luka@gmail.com', N'F1-DC-73-5E-E3-58-16-93-48-9E-AF-28-60-88-B9-16', N'Luka', N'Lukic', 280, 0)
INSERT [dbo].[User] ([email], [password], [first_name], [last_name], [tokens_amount], [is_administrator]) VALUES (N'momaznikolic96@gmail.com', N'F1-DC-73-5E-E3-58-16-93-48-9E-AF-28-60-88-B9-16', N'Momcilo', N'Nikolic', 301, 1)
ALTER TABLE [dbo].[TokenOrder] ADD  CONSTRAINT [DF_TokenOrder_state]  DEFAULT (N'SUBMITTED') FOR [state]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_tokens_amount]  DEFAULT ((0)) FOR [tokens_amount]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_is_administrator]  DEFAULT ((0)) FOR [is_administrator]
GO
ALTER TABLE [dbo].[Auction]  WITH CHECK ADD  CONSTRAINT [FK_Auction_User_Owner] FOREIGN KEY([owner])
REFERENCES [dbo].[User] ([email])
GO
ALTER TABLE [dbo].[Auction] CHECK CONSTRAINT [FK_Auction_User_Owner]
GO
ALTER TABLE [dbo].[Auction]  WITH CHECK ADD  CONSTRAINT [FK_Auction_User_Won] FOREIGN KEY([won])
REFERENCES [dbo].[User] ([email])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Auction] CHECK CONSTRAINT [FK_Auction_User_Won]
GO
ALTER TABLE [dbo].[Bid]  WITH CHECK ADD  CONSTRAINT [FK_Bid_Auction_Id] FOREIGN KEY([auction_id])
REFERENCES [dbo].[Auction] ([id])
GO
ALTER TABLE [dbo].[Bid] CHECK CONSTRAINT [FK_Bid_Auction_Id]
GO
ALTER TABLE [dbo].[Bid]  WITH CHECK ADD  CONSTRAINT [FK_Bid_User_Bidder] FOREIGN KEY([bidder])
REFERENCES [dbo].[User] ([email])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Bid] CHECK CONSTRAINT [FK_Bid_User_Bidder]
GO
ALTER TABLE [dbo].[TokenOrder]  WITH CHECK ADD  CONSTRAINT [FK_TokenOrder_User_Orderer] FOREIGN KEY([orderer])
REFERENCES [dbo].[User] ([email])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TokenOrder] CHECK CONSTRAINT [FK_TokenOrder_User_Orderer]
GO
USE [master]
GO
ALTER DATABASE [auction_house] SET  READ_WRITE 
GO
