﻿USE [nancybook]
GO

/****** Object:  Table [dbo].[artists]    Script Date: 12/31/2015 20:06:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[artists](
	[pkid] [int] IDENTITY(1,1) NOT NULL,
	[artistname] [varchar](100) NOT NULL,
 CONSTRAINT [PK_artists] PRIMARY KEY CLUSTERED 
(
	[pkid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


