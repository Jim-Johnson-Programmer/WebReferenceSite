USE [WebReferenceSiteDb_Selenium]
GO
/****** Object:  Table [dbo].[FileGroups]    Script Date: 2/9/2023 9:13:20 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FileGroups]') AND type in (N'U'))
DROP TABLE [dbo].[FileGroups]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FileGroups](
	[FileGroupId] [int] IDENTITY(1,1) NOT NULL,
	[FileGroupName] [varchar](200) NOT NULL,
	[CreatedOn] [smalldatetime] NOT NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[UpdatedOn] [smalldatetime] NOT NULL,
	[UpdatedBy] [varchar](50) NOT NULL,
 CONSTRAINT [PK_FileGroups] PRIMARY KEY CLUSTERED 
(
	[FileGroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FilePages]    Script Date: 2/9/2023 9:13:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FilePages](
	[FilePageId] [int] IDENTITY(1,1) NOT NULL,
	[FileId] [int] NOT NULL,
	[FilePageSortNumber] [int] NOT NULL,
	[PageText] [varchar](max) NOT NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[UpdatedBy] [varchar](50) NOT NULL,
	[CreatedOn] [smalldatetime] NOT NULL,
	[UpdatedOn] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_FilePages] PRIMARY KEY CLUSTERED 
(
	[FilePageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Files]    Script Date: 2/9/2023 9:13:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Files](
	[FileId] [int] IDENTITY(1,1) NOT NULL,
	[FileName] [varchar](200) NOT NULL,
	[FileRowsCount] [int] NULL,
	[FilePagesCount] [int] NULL,
	[CreatedOn] [smalldatetime] NOT NULL,
	[UpdatedOn] [smalldatetime] NOT NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[UpdatedBy] [varchar](50) NOT NULL,
	[ParentFolderId] [int] NOT NULL,
	[ParentFolderName] [varchar](200) NOT NULL,
 CONSTRAINT [PK_Files] PRIMARY KEY CLUSTERED 
(
	[FileId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FilesInFileGroups]    Script Date: 2/9/2023 9:13:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FilesInFileGroups](
	[FilesInFileGroupsId] [int] IDENTITY(1,1) NOT NULL,
	[FileGroupId] [int] NOT NULL,
	[FileId] [int] NOT NULL,
	[CreatedOn] [smalldatetime] NOT NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[UpdatedOn] [smalldatetime] NOT NULL,
	[UpdatedBy] [varchar](50) NOT NULL,
 CONSTRAINT [PK_FilesInFileGroups] PRIMARY KEY CLUSTERED 
(
	[FilesInFileGroupsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


/****** Object:  Table [dbo].[Folders]    Script Date: 2/9/2023 9:13:20 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Folders]') AND type in (N'U'))
DROP TABLE [dbo].[Folders]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Folders](
	[FolderId] [int] IDENTITY(1,1) NOT NULL,
	[FolderName] [varchar](200) NOT NULL,
	[ParentFolderId] [int] NULL,
	[ParentFolderName] [varchar](200) NULL,
	[CreatedOn] [smalldatetime] NOT NULL,
	[UpdatedOn] [smalldatetime] NOT NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[UpdatedBy] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Folders] PRIMARY KEY CLUSTERED 
(
	[FolderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Logs]    Script Date: 2/9/2023 9:13:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Logs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Message] [nvarchar](max) NULL,
	[MessageTemplate] [nvarchar](max) NULL,
	[Level] [nvarchar](max) NULL,
	[TimeStamp] [datetime] NULL,
	[Exception] [nvarchar](max) NULL,
	[Properties] [nvarchar](max) NULL,
 CONSTRAINT [PK_Logs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[FilePages]  WITH CHECK ADD  CONSTRAINT [FK_FilePages_Files] FOREIGN KEY([FileId])
REFERENCES [dbo].[Files] ([FileId])
GO
ALTER TABLE [dbo].[FilePages] CHECK CONSTRAINT [FK_FilePages_Files]
GO
ALTER TABLE [dbo].[Files]  WITH CHECK ADD  CONSTRAINT [FK_Files_Folders] FOREIGN KEY([ParentFolderId])
REFERENCES [dbo].[Folders] ([FolderId])
GO
ALTER TABLE [dbo].[Files] CHECK CONSTRAINT [FK_Files_Folders]
GO
ALTER TABLE [dbo].[FilesInFileGroups]  WITH CHECK ADD  CONSTRAINT [FK_FilesInFileGroups_FileGroups] FOREIGN KEY([FileGroupId])
REFERENCES [dbo].[FileGroups] ([FileGroupId])
GO
ALTER TABLE [dbo].[FilesInFileGroups] CHECK CONSTRAINT [FK_FilesInFileGroups_FileGroups]
GO
ALTER TABLE [dbo].[FilesInFileGroups]  WITH CHECK ADD  CONSTRAINT [FK_FilesInFileGroups_Files] FOREIGN KEY([FileId])
REFERENCES [dbo].[Files] ([FileId])
GO
ALTER TABLE [dbo].[FilesInFileGroups] CHECK CONSTRAINT [FK_FilesInFileGroups_Files]
GO
