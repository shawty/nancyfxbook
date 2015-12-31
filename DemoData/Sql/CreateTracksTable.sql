USE [nancybook]
GO

/****** Object:  Table [dbo].[tracks]    Script Date: 12/31/2015 20:06:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[tracks](
	[pkid] [int] IDENTITY(1,1) NOT NULL,
	[filename] [varchar](200) NULL,
	[path] [varchar](200) NULL,
	[bitrate] [int] NULL,
	[channels] [int] NULL,
	[samplerate] [int] NULL,
	[duration] [varchar](20) NULL,
	[title] [varchar](100) NULL,
	[tracknumber] [int] NULL,
	[yearreleased] [int] NULL,
	[albumid] [int] NOT NULL,
	[genreid] [int] NOT NULL,
	[artistid] [int] NOT NULL,
 CONSTRAINT [PK_mp3raw] PRIMARY KEY CLUSTERED 
(
	[pkid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[tracks] ADD  CONSTRAINT [DF_mp3raw_albumid]  DEFAULT ((0)) FOR [albumid]
GO

ALTER TABLE [dbo].[tracks] ADD  CONSTRAINT [DF_tracks_genreid]  DEFAULT ((0)) FOR [genreid]
GO

ALTER TABLE [dbo].[tracks] ADD  CONSTRAINT [DF_tracks_artistid]  DEFAULT ((0)) FOR [artistid]
GO


