﻿CREATE TABLE [dbo].[NodePermission]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PERMISSION_OWNER_ID] INT NOT NULL, 
    [PERMISSION_OWNER_TYPE] INT NOT NULL, 
    [NODE_PATH] NVARCHAR(300) NOT NULL, 
    [PERMISSION_LEVEL] INT NOT NULL
)