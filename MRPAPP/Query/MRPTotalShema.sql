USE [MRP]
GO
/****** Object:  Table [dbo].[Process]    Script Date: 2021-07-01 오후 2:01:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Process](
	[PrcIdx] [int] IDENTITY(1,1) NOT NULL,
	[Schidx] [int] NOT NULL,
	[PrcCD] [char](14) NOT NULL,
	[PrcDate] [date] NOT NULL,
	[PrcLoadTime] [int] NULL,
	[PrcStartTime] [time](7) NULL,
	[PrcEndTime] [time](7) NULL,
	[PrcFacilityID] [char](8) NULL,
	[PrcResult] [bit] NOT NULL,
	[RegData] [datetime] NULL,
	[RegID] [varchar](20) NULL,
	[ModDate] [datetime] NULL,
	[ModID] [varchar](20) NULL,
 CONSTRAINT [PK_Process] PRIMARY KEY CLUSTERED 
(
	[PrcIdx] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Schedules]    Script Date: 2021-07-01 오후 2:01:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Schedules](
	[SchIdx] [int] IDENTITY(1,1) NOT NULL,
	[PlantCode] [char](8) NULL,
	[SchDate] [date] NOT NULL,
	[SchLoadTime] [int] NOT NULL,
	[SchStartTime] [time](7) NULL,
	[SchEndTime] [time](7) NULL,
	[SchFacilityID] [char](8) NULL,
	[SchAmount] [int] NULL,
	[RegData] [datetime] NULL,
	[RegID] [varchar](20) NULL,
	[ModDate] [datetime] NULL,
	[ModID] [varchar](80) NULL,
 CONSTRAINT [PK_Schedules] PRIMARY KEY CLUSTERED 
(
	[SchIdx] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Settings]    Script Date: 2021-07-01 오후 2:01:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Settings](
	[BasicCode] [char](8) NOT NULL,
	[CodeName] [nvarchar](100) NOT NULL,
	[CodeDesc] [nvarchar](max) NULL,
	[RegData] [datetime] NULL,
	[RegID] [varchar](20) NULL,
	[ModDate] [datetime] NULL,
	[ModID] [varchar](20) NULL,
 CONSTRAINT [PK_Settings] PRIMARY KEY CLUSTERED 
(
	[BasicCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Process] ON 

INSERT [dbo].[Process] ([PrcIdx], [Schidx], [PrcCD], [PrcDate], [PrcLoadTime], [PrcStartTime], [PrcEndTime], [PrcFacilityID], [PrcResult], [RegData], [RegID], [ModDate], [ModID]) VALUES (1, 3, N'PRC20210629001', CAST(N'2021-06-29' AS Date), 5, CAST(N'03:00:00' AS Time), CAST(N'06:00:00' AS Time), N'FAC10002', 1, CAST(N'2021-06-29T10:24:57.607' AS DateTime), N'MRP', NULL, NULL)
INSERT [dbo].[Process] ([PrcIdx], [Schidx], [PrcCD], [PrcDate], [PrcLoadTime], [PrcStartTime], [PrcEndTime], [PrcFacilityID], [PrcResult], [RegData], [RegID], [ModDate], [ModID]) VALUES (2, 3, N'PRC20210629011', CAST(N'2021-06-29' AS Date), 5, CAST(N'03:00:00' AS Time), CAST(N'06:00:00' AS Time), N'FAC10002', 1, CAST(N'2021-06-29T10:25:58.837' AS DateTime), N'MRP', NULL, NULL)
INSERT [dbo].[Process] ([PrcIdx], [Schidx], [PrcCD], [PrcDate], [PrcLoadTime], [PrcStartTime], [PrcEndTime], [PrcFacilityID], [PrcResult], [RegData], [RegID], [ModDate], [ModID]) VALUES (3, 3, N'PRC20210629111', CAST(N'2021-06-29' AS Date), 5, CAST(N'03:00:00' AS Time), CAST(N'06:00:00' AS Time), N'FAC10002', 1, CAST(N'2021-06-29T10:26:40.730' AS DateTime), N'MRP', NULL, NULL)
INSERT [dbo].[Process] ([PrcIdx], [Schidx], [PrcCD], [PrcDate], [PrcLoadTime], [PrcStartTime], [PrcEndTime], [PrcFacilityID], [PrcResult], [RegData], [RegID], [ModDate], [ModID]) VALUES (4, 3, N'PRC20210629112', CAST(N'2021-06-29' AS Date), 5, CAST(N'03:00:00' AS Time), CAST(N'06:00:00' AS Time), N'FAC10002', 1, CAST(N'2021-06-29T10:32:21.557' AS DateTime), N'MRP', NULL, NULL)
INSERT [dbo].[Process] ([PrcIdx], [Schidx], [PrcCD], [PrcDate], [PrcLoadTime], [PrcStartTime], [PrcEndTime], [PrcFacilityID], [PrcResult], [RegData], [RegID], [ModDate], [ModID]) VALUES (5, 3, N'PRC20210629113', CAST(N'2021-06-29' AS Date), 5, CAST(N'03:00:00' AS Time), CAST(N'06:00:00' AS Time), N'FAC10002', 1, CAST(N'2021-06-29T10:32:32.213' AS DateTime), N'MRP', NULL, NULL)
INSERT [dbo].[Process] ([PrcIdx], [Schidx], [PrcCD], [PrcDate], [PrcLoadTime], [PrcStartTime], [PrcEndTime], [PrcFacilityID], [PrcResult], [RegData], [RegID], [ModDate], [ModID]) VALUES (6, 3, N'PRC20210629114', CAST(N'2021-06-29' AS Date), 5, CAST(N'03:00:00' AS Time), CAST(N'06:00:00' AS Time), N'FAC10002', 1, CAST(N'2021-06-29T10:33:02.597' AS DateTime), N'MRP', CAST(N'2021-06-29T11:16:05.000' AS DateTime), N'SYS')
INSERT [dbo].[Process] ([PrcIdx], [Schidx], [PrcCD], [PrcDate], [PrcLoadTime], [PrcStartTime], [PrcEndTime], [PrcFacilityID], [PrcResult], [RegData], [RegID], [ModDate], [ModID]) VALUES (7, 3, N'PRC20210629115', CAST(N'2021-06-29' AS Date), 5, CAST(N'03:00:00' AS Time), CAST(N'06:00:00' AS Time), N'FAC10002', 1, CAST(N'2021-06-29T12:21:27.937' AS DateTime), N'MRP', CAST(N'2021-06-29T12:28:15.000' AS DateTime), N'SYS')
INSERT [dbo].[Process] ([PrcIdx], [Schidx], [PrcCD], [PrcDate], [PrcLoadTime], [PrcStartTime], [PrcEndTime], [PrcFacilityID], [PrcResult], [RegData], [RegID], [ModDate], [ModID]) VALUES (8, 3, N'PRC20210629116', CAST(N'2021-06-29' AS Date), 5, CAST(N'03:00:00' AS Time), CAST(N'06:00:00' AS Time), N'FAC10002', 1, CAST(N'2021-06-29T12:28:27.647' AS DateTime), N'MRP', CAST(N'2021-06-30T09:30:27.000' AS DateTime), N'SYS')
INSERT [dbo].[Process] ([PrcIdx], [Schidx], [PrcCD], [PrcDate], [PrcLoadTime], [PrcStartTime], [PrcEndTime], [PrcFacilityID], [PrcResult], [RegData], [RegID], [ModDate], [ModID]) VALUES (9, 6, N'PRC20210630001', CAST(N'2021-06-30' AS Date), 20, CAST(N'01:00:00' AS Time), CAST(N'06:00:00' AS Time), N'FAC10002', 1, CAST(N'2021-06-30T09:30:31.877' AS DateTime), N'MRP', CAST(N'2021-06-30T09:31:13.000' AS DateTime), N'SYS')
INSERT [dbo].[Process] ([PrcIdx], [Schidx], [PrcCD], [PrcDate], [PrcLoadTime], [PrcStartTime], [PrcEndTime], [PrcFacilityID], [PrcResult], [RegData], [RegID], [ModDate], [ModID]) VALUES (10, 6, N'PRC20210630002', CAST(N'2021-06-30' AS Date), 20, CAST(N'01:00:00' AS Time), CAST(N'06:00:00' AS Time), N'FAC10002', 1, CAST(N'2021-06-30T09:31:35.560' AS DateTime), N'MRP', CAST(N'2021-06-30T09:32:00.000' AS DateTime), N'SYS')
INSERT [dbo].[Process] ([PrcIdx], [Schidx], [PrcCD], [PrcDate], [PrcLoadTime], [PrcStartTime], [PrcEndTime], [PrcFacilityID], [PrcResult], [RegData], [RegID], [ModDate], [ModID]) VALUES (11, 6, N'PRC20210630003', CAST(N'2021-06-30' AS Date), 20, CAST(N'01:00:00' AS Time), CAST(N'06:00:00' AS Time), N'FAC10002', 1, CAST(N'2021-06-30T09:43:21.813' AS DateTime), N'MRP', CAST(N'2021-06-30T09:43:47.000' AS DateTime), N'SYS')
INSERT [dbo].[Process] ([PrcIdx], [Schidx], [PrcCD], [PrcDate], [PrcLoadTime], [PrcStartTime], [PrcEndTime], [PrcFacilityID], [PrcResult], [RegData], [RegID], [ModDate], [ModID]) VALUES (12, 6, N'PRC20210630004', CAST(N'2021-06-30' AS Date), 20, CAST(N'01:00:00' AS Time), CAST(N'06:00:00' AS Time), N'FAC10002', 0, CAST(N'2021-06-30T09:48:49.030' AS DateTime), N'MRP', CAST(N'2021-06-30T09:49:26.000' AS DateTime), N'SYS')
INSERT [dbo].[Process] ([PrcIdx], [Schidx], [PrcCD], [PrcDate], [PrcLoadTime], [PrcStartTime], [PrcEndTime], [PrcFacilityID], [PrcResult], [RegData], [RegID], [ModDate], [ModID]) VALUES (13, 6, N'PRC20210630005', CAST(N'2021-06-30' AS Date), 5, CAST(N'01:00:00' AS Time), CAST(N'06:00:00' AS Time), N'FAC10002', 0, CAST(N'2021-06-30T10:05:26.150' AS DateTime), N'MRP', CAST(N'2021-06-30T10:07:02.000' AS DateTime), N'SYS')
INSERT [dbo].[Process] ([PrcIdx], [Schidx], [PrcCD], [PrcDate], [PrcLoadTime], [PrcStartTime], [PrcEndTime], [PrcFacilityID], [PrcResult], [RegData], [RegID], [ModDate], [ModID]) VALUES (14, 6, N'PRC20210630006', CAST(N'2021-06-30' AS Date), 5, CAST(N'01:00:00' AS Time), CAST(N'06:00:00' AS Time), N'FAC10002', 0, CAST(N'2021-06-30T10:07:43.390' AS DateTime), N'MRP', CAST(N'2021-06-30T10:08:02.000' AS DateTime), N'SYS')
INSERT [dbo].[Process] ([PrcIdx], [Schidx], [PrcCD], [PrcDate], [PrcLoadTime], [PrcStartTime], [PrcEndTime], [PrcFacilityID], [PrcResult], [RegData], [RegID], [ModDate], [ModID]) VALUES (15, 6, N'PRC20210630007', CAST(N'2021-06-30' AS Date), 5, CAST(N'01:00:00' AS Time), CAST(N'06:00:00' AS Time), N'FAC10002', 1, CAST(N'2021-06-30T10:35:44.947' AS DateTime), N'MRP', CAST(N'2021-06-30T10:36:09.000' AS DateTime), N'SYS')
INSERT [dbo].[Process] ([PrcIdx], [Schidx], [PrcCD], [PrcDate], [PrcLoadTime], [PrcStartTime], [PrcEndTime], [PrcFacilityID], [PrcResult], [RegData], [RegID], [ModDate], [ModID]) VALUES (16, 6, N'PRC20210630008', CAST(N'2021-06-30' AS Date), 5, CAST(N'01:00:00' AS Time), CAST(N'06:00:00' AS Time), N'FAC10002', 0, CAST(N'2021-06-30T10:36:13.907' AS DateTime), N'MRP', CAST(N'2021-06-30T10:36:25.000' AS DateTime), N'SYS')
INSERT [dbo].[Process] ([PrcIdx], [Schidx], [PrcCD], [PrcDate], [PrcLoadTime], [PrcStartTime], [PrcEndTime], [PrcFacilityID], [PrcResult], [RegData], [RegID], [ModDate], [ModID]) VALUES (17, 6, N'PRC20210630009', CAST(N'2021-06-30' AS Date), 5, CAST(N'01:00:00' AS Time), CAST(N'06:00:00' AS Time), N'FAC10002', 0, CAST(N'2021-06-30T10:46:30.020' AS DateTime), N'MRP', CAST(N'2021-06-30T10:46:55.000' AS DateTime), N'SYS')
INSERT [dbo].[Process] ([PrcIdx], [Schidx], [PrcCD], [PrcDate], [PrcLoadTime], [PrcStartTime], [PrcEndTime], [PrcFacilityID], [PrcResult], [RegData], [RegID], [ModDate], [ModID]) VALUES (18, 6, N'PRC20210630010', CAST(N'2021-06-30' AS Date), 5, CAST(N'01:00:00' AS Time), CAST(N'06:00:00' AS Time), N'FAC10002', 1, CAST(N'2021-06-30T10:50:55.420' AS DateTime), N'MRP', CAST(N'2021-06-30T10:51:07.000' AS DateTime), N'SYS')
INSERT [dbo].[Process] ([PrcIdx], [Schidx], [PrcCD], [PrcDate], [PrcLoadTime], [PrcStartTime], [PrcEndTime], [PrcFacilityID], [PrcResult], [RegData], [RegID], [ModDate], [ModID]) VALUES (19, 6, N'PRC20210630011', CAST(N'2021-06-30' AS Date), 5, CAST(N'01:00:00' AS Time), CAST(N'06:00:00' AS Time), N'FAC10002', 1, CAST(N'2021-06-30T10:55:16.283' AS DateTime), N'MRP', CAST(N'2021-06-30T10:55:38.000' AS DateTime), N'SYS')
INSERT [dbo].[Process] ([PrcIdx], [Schidx], [PrcCD], [PrcDate], [PrcLoadTime], [PrcStartTime], [PrcEndTime], [PrcFacilityID], [PrcResult], [RegData], [RegID], [ModDate], [ModID]) VALUES (20, 6, N'PRC20210630012', CAST(N'2021-06-30' AS Date), 5, CAST(N'01:00:00' AS Time), CAST(N'06:00:00' AS Time), N'FAC10002', 1, CAST(N'2021-06-30T10:57:26.867' AS DateTime), N'MRP', CAST(N'2021-06-30T10:58:06.000' AS DateTime), N'SYS')
INSERT [dbo].[Process] ([PrcIdx], [Schidx], [PrcCD], [PrcDate], [PrcLoadTime], [PrcStartTime], [PrcEndTime], [PrcFacilityID], [PrcResult], [RegData], [RegID], [ModDate], [ModID]) VALUES (21, 6, N'PRC20210630013', CAST(N'2021-06-30' AS Date), 5, CAST(N'01:00:00' AS Time), CAST(N'06:00:00' AS Time), N'FAC10002', 0, CAST(N'2021-06-30T11:04:50.443' AS DateTime), N'MRP', CAST(N'2021-06-30T11:05:11.000' AS DateTime), N'SYS')
INSERT [dbo].[Process] ([PrcIdx], [Schidx], [PrcCD], [PrcDate], [PrcLoadTime], [PrcStartTime], [PrcEndTime], [PrcFacilityID], [PrcResult], [RegData], [RegID], [ModDate], [ModID]) VALUES (22, 6, N'PRC20210630014', CAST(N'2021-06-30' AS Date), 5, CAST(N'01:00:00' AS Time), CAST(N'06:00:00' AS Time), N'FAC10002', 0, CAST(N'2021-06-30T17:12:20.223' AS DateTime), N'MRP', CAST(N'2021-06-30T17:13:11.000' AS DateTime), N'SYS')
INSERT [dbo].[Process] ([PrcIdx], [Schidx], [PrcCD], [PrcDate], [PrcLoadTime], [PrcStartTime], [PrcEndTime], [PrcFacilityID], [PrcResult], [RegData], [RegID], [ModDate], [ModID]) VALUES (23, 6, N'PRC20210630015', CAST(N'2021-06-30' AS Date), 5, CAST(N'01:00:00' AS Time), CAST(N'06:00:00' AS Time), N'FAC10002', 0, CAST(N'2021-06-30T17:19:45.063' AS DateTime), N'MRP', CAST(N'2021-06-30T17:20:20.000' AS DateTime), N'SYS')
INSERT [dbo].[Process] ([PrcIdx], [Schidx], [PrcCD], [PrcDate], [PrcLoadTime], [PrcStartTime], [PrcEndTime], [PrcFacilityID], [PrcResult], [RegData], [RegID], [ModDate], [ModID]) VALUES (24, 6, N'PRC20210630016', CAST(N'2021-06-30' AS Date), 10, CAST(N'01:00:00' AS Time), CAST(N'06:00:00' AS Time), N'FAC10002', 0, CAST(N'2021-06-30T17:25:03.700' AS DateTime), N'MRP', CAST(N'2021-06-30T17:25:53.000' AS DateTime), N'SYS')
SET IDENTITY_INSERT [dbo].[Process] OFF
GO
SET IDENTITY_INSERT [dbo].[Schedules] ON 

INSERT [dbo].[Schedules] ([SchIdx], [PlantCode], [SchDate], [SchLoadTime], [SchStartTime], [SchEndTime], [SchFacilityID], [SchAmount], [RegData], [RegID], [ModDate], [ModID]) VALUES (1, N'PC010001', CAST(N'2021-06-24' AS Date), 15, CAST(N'10:00:00' AS Time), CAST(N'18:00:00' AS Time), N'FAC10001', 35, CAST(N'2021-06-24T18:00:00.000' AS DateTime), N'SYS', CAST(N'2021-06-25T11:47:17.377' AS DateTime), N'MRP')
INSERT [dbo].[Schedules] ([SchIdx], [PlantCode], [SchDate], [SchLoadTime], [SchStartTime], [SchEndTime], [SchFacilityID], [SchAmount], [RegData], [RegID], [ModDate], [ModID]) VALUES (2, N'PC010002', CAST(N'2021-06-25' AS Date), 15, CAST(N'07:00:00' AS Time), CAST(N'20:00:00' AS Time), N'FAC10001', 10, CAST(N'2021-06-25T12:39:10.980' AS DateTime), N'MRP', CAST(N'2021-06-25T14:11:00.477' AS DateTime), N'MRP')
INSERT [dbo].[Schedules] ([SchIdx], [PlantCode], [SchDate], [SchLoadTime], [SchStartTime], [SchEndTime], [SchFacilityID], [SchAmount], [RegData], [RegID], [ModDate], [ModID]) VALUES (3, N'PC010002', CAST(N'2021-06-29' AS Date), 5, CAST(N'03:00:00' AS Time), CAST(N'06:00:00' AS Time), N'FAC10001', 30, CAST(N'2021-06-25T14:13:36.287' AS DateTime), N'MRP', CAST(N'2021-06-29T10:32:59.427' AS DateTime), N'MRP')
INSERT [dbo].[Schedules] ([SchIdx], [PlantCode], [SchDate], [SchLoadTime], [SchStartTime], [SchEndTime], [SchFacilityID], [SchAmount], [RegData], [RegID], [ModDate], [ModID]) VALUES (4, N'PC010002', CAST(N'2021-06-28' AS Date), 10, NULL, NULL, N'FAC10001', 40, CAST(N'2021-06-28T09:20:57.500' AS DateTime), N'MRP', CAST(N'2021-06-28T11:51:55.910' AS DateTime), N'MRP')
INSERT [dbo].[Schedules] ([SchIdx], [PlantCode], [SchDate], [SchLoadTime], [SchStartTime], [SchEndTime], [SchFacilityID], [SchAmount], [RegData], [RegID], [ModDate], [ModID]) VALUES (5, N'PC010001', CAST(N'2021-06-27' AS Date), 15, CAST(N'05:00:00' AS Time), CAST(N'09:00:00' AS Time), N'FAC10001', 45, CAST(N'2021-06-28T15:32:20.197' AS DateTime), N'MRP', NULL, NULL)
INSERT [dbo].[Schedules] ([SchIdx], [PlantCode], [SchDate], [SchLoadTime], [SchStartTime], [SchEndTime], [SchFacilityID], [SchAmount], [RegData], [RegID], [ModDate], [ModID]) VALUES (6, N'PC010002', CAST(N'2021-06-30' AS Date), 10, CAST(N'01:00:00' AS Time), CAST(N'06:00:00' AS Time), N'FAC10002', 40, CAST(N'2021-06-28T15:34:02.747' AS DateTime), N'MRP', CAST(N'2021-06-30T17:24:54.697' AS DateTime), N'MRP')
SET IDENTITY_INSERT [dbo].[Schedules] OFF
GO
INSERT [dbo].[Settings] ([BasicCode], [CodeName], [CodeDesc], [RegData], [RegID], [ModDate], [ModID]) VALUES (N'FAC10001', N'설비1', N'생산설비1', CAST(N'2021-06-24T14:07:01.413' AS DateTime), N'MRP', CAST(N'2021-06-24T14:08:53.063' AS DateTime), N'MRP')
INSERT [dbo].[Settings] ([BasicCode], [CodeName], [CodeDesc], [RegData], [RegID], [ModDate], [ModID]) VALUES (N'FAC10002', N'설비2', N'생산설비2', CAST(N'2021-06-24T14:08:43.037' AS DateTime), N'MRP', NULL, NULL)
INSERT [dbo].[Settings] ([BasicCode], [CodeName], [CodeDesc], [RegData], [RegID], [ModDate], [ModID]) VALUES (N'PC010001', N'수원공장', N'수원공장(코드) 1', CAST(N'2021-06-24T11:22:24.000' AS DateTime), N'SYS', CAST(N'2021-06-28T10:44:57.483' AS DateTime), N'MRP')
INSERT [dbo].[Settings] ([BasicCode], [CodeName], [CodeDesc], [RegData], [RegID], [ModDate], [ModID]) VALUES (N'PC010002', N'부산공장', N'부산공장', CAST(N'2021-06-24T13:58:03.133' AS DateTime), N'MRP', CAST(N'2021-06-28T10:44:53.640' AS DateTime), N'MRP')
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UK_Process_PrcCD]    Script Date: 2021-07-01 오후 2:02:00 ******/
ALTER TABLE [dbo].[Process] ADD  CONSTRAINT [UK_Process_PrcCD] UNIQUE NONCLUSTERED 
(
	[PrcCD] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Process]  WITH NOCHECK ADD  CONSTRAINT [FK_Process_Schedules] FOREIGN KEY([Schidx])
REFERENCES [dbo].[Schedules] ([SchIdx])
GO
ALTER TABLE [dbo].[Process] NOCHECK CONSTRAINT [FK_Process_Schedules]
GO
ALTER TABLE [dbo].[Process]  WITH NOCHECK ADD  CONSTRAINT [FK_Process_Settings] FOREIGN KEY([PrcFacilityID])
REFERENCES [dbo].[Settings] ([BasicCode])
GO
ALTER TABLE [dbo].[Process] NOCHECK CONSTRAINT [FK_Process_Settings]
GO
ALTER TABLE [dbo].[Schedules]  WITH NOCHECK ADD  CONSTRAINT [FK_Schedules_Settings] FOREIGN KEY([PlantCode])
REFERENCES [dbo].[Settings] ([BasicCode])
GO
ALTER TABLE [dbo].[Schedules] NOCHECK CONSTRAINT [FK_Schedules_Settings]
GO
ALTER TABLE [dbo].[Schedules]  WITH NOCHECK ADD  CONSTRAINT [FK_Schedules_Settings1] FOREIGN KEY([SchFacilityID])
REFERENCES [dbo].[Settings] ([BasicCode])
GO
ALTER TABLE [dbo].[Schedules] NOCHECK CONSTRAINT [FK_Schedules_Settings1]
GO
