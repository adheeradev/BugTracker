﻿CREATE TABLE [dbo].[WorkFlowStatus] (
    [Id]     INT          NOT NULL IDENTITY,
    [Status] VARCHAR (50) NOT NULL,
    CONSTRAINT PK_WKF_STS PRIMARY KEY CLUSTERED ([Id] ASC)
) ON [PRIMARY]
GO
