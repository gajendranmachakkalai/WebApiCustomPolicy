USE [MTCatalogDB]
GO
ALTER TABLE [dbo].[AppUser] DROP CONSTRAINT [FK_AppUser_roleid_AppRole_roleid]
GO
ALTER TABLE [dbo].[AppRoleRoute] DROP CONSTRAINT [FK_AppRoleRoute_rolerouteid_AppRoute_routeid]
GO
ALTER TABLE [dbo].[AppRoleRoute] DROP CONSTRAINT [FK_AppRoleRoute_roleid_AppRole_roleid]
GO
/****** Object:  Table [dbo].[Organization]    Script Date: 27-03-2023 13:26:39 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Organization]') AND type in (N'U'))
DROP TABLE [dbo].[Organization]
GO
/****** Object:  Table [dbo].[AppUser]    Script Date: 27-03-2023 13:26:39 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AppUser]') AND type in (N'U'))
DROP TABLE [dbo].[AppUser]
GO
/****** Object:  Table [dbo].[AppRoute]    Script Date: 27-03-2023 13:26:39 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AppRoute]') AND type in (N'U'))
DROP TABLE [dbo].[AppRoute]
GO
/****** Object:  Table [dbo].[AppRoleRoute]    Script Date: 27-03-2023 13:26:39 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AppRoleRoute]') AND type in (N'U'))
DROP TABLE [dbo].[AppRoleRoute]
GO
/****** Object:  Table [dbo].[AppRole]    Script Date: 27-03-2023 13:26:39 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AppRole]') AND type in (N'U'))
DROP TABLE [dbo].[AppRole]
GO
/****** Object:  Table [dbo].[AppRole]    Script Date: 27-03-2023 13:26:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppRole](
	[roleid] [smallint] IDENTITY(1,1) NOT NULL,
	[rolename] [varchar](20) NULL,
	[createddate] [datetime] NULL,
 CONSTRAINT [PK_AppRole_roleid] PRIMARY KEY CLUSTERED 
(
	[roleid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AppRoleRoute]    Script Date: 27-03-2023 13:26:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppRoleRoute](
	[rolerouteid] [smallint] IDENTITY(1,1) NOT NULL,
	[routeid] [int] NULL,
	[roleid] [smallint] NULL,
	[isactive] [bit] NULL,
	[createddate] [datetime] NULL,
 CONSTRAINT [PK_AppRoleRoute_rolerouteid] PRIMARY KEY CLUSTERED 
(
	[rolerouteid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AppRoute]    Script Date: 27-03-2023 13:26:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppRoute](
	[routeid] [int] IDENTITY(1,1) NOT NULL,
	[routepath] [varchar](250) NULL,
	[isexclude] [bit] NULL,
	[isactive] [bit] NULL,
	[createddate] [datetime] NULL,
 CONSTRAINT [PK_AppRoute_routeid] PRIMARY KEY CLUSTERED 
(
	[routeid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AppUser]    Script Date: 27-03-2023 13:26:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppUser](
	[userid] [bigint] IDENTITY(1,1) NOT NULL,
	[username] [varchar](100) NULL,
	[email] [varchar](100) NULL,
	[cellphone] [varchar](10) NULL,
	[roleid] [smallint] NULL,
	[passwordhash] [varchar](250) NULL,
	[createddate] [datetime] NULL,
	[refreshtoken] [varchar](50) NULL,
 CONSTRAINT [PK_AppUser_userid] PRIMARY KEY CLUSTERED 
(
	[userid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Organization]    Script Date: 27-03-2023 13:26:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Organization](
	[orgid] [int] IDENTITY(1,1) NOT NULL,
	[orgname] [varchar](50) NULL,
	[orgcode] [varchar](10) NULL,
	[isactive] [bit] NULL,
	[createddate] [datetime] NULL,
 CONSTRAINT [PK_Organization_orgid] PRIMARY KEY CLUSTERED 
(
	[orgid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AppRoleRoute]  WITH CHECK ADD  CONSTRAINT [FK_AppRoleRoute_roleid_AppRole_roleid] FOREIGN KEY([roleid])
REFERENCES [dbo].[AppRole] ([roleid])
GO
ALTER TABLE [dbo].[AppRoleRoute] CHECK CONSTRAINT [FK_AppRoleRoute_roleid_AppRole_roleid]
GO
ALTER TABLE [dbo].[AppRoleRoute]  WITH CHECK ADD  CONSTRAINT [FK_AppRoleRoute_rolerouteid_AppRoute_routeid] FOREIGN KEY([routeid])
REFERENCES [dbo].[AppRoute] ([routeid])
GO
ALTER TABLE [dbo].[AppRoleRoute] CHECK CONSTRAINT [FK_AppRoleRoute_rolerouteid_AppRoute_routeid]
GO
ALTER TABLE [dbo].[AppUser]  WITH CHECK ADD  CONSTRAINT [FK_AppUser_roleid_AppRole_roleid] FOREIGN KEY([roleid])
REFERENCES [dbo].[AppRole] ([roleid])
GO
ALTER TABLE [dbo].[AppUser] CHECK CONSTRAINT [FK_AppUser_roleid_AppRole_roleid]
GO
