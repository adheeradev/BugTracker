CREATE TABLE [dbo].[Bug] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [AssignedToUserId] INT            NULL,
    [Title]            NVARCHAR (255) NOT NULL,
    [Description]      NVARCHAR (MAX) NULL,
    [OpenedDate]       DATETIME       NULL,
    [StatusId]         INT            NULL,
    CONSTRAINT [PK_BUG_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_USER_Id] FOREIGN KEY ([AssignedToUserId]) REFERENCES [dbo].[User] ([Id]) ON DELETE SET NULL,
    CONSTRAINT [FK_Status_Id] FOREIGN KEY ([StatusId]) REFERENCES [dbo].[WorkFlowStatus] ([Id]) ON DELETE SET NULL
);

