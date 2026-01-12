CREATE TABLE [dbo].[AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY PRIMARY KEY,
    [UserId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers]([Id]) ON DELETE CASCADE
);