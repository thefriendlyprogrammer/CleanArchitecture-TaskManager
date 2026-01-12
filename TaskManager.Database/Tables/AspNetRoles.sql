CREATE TABLE [dbo].[AspNetRoles] (
    [Id] nvarchar(450) NOT NULL PRIMARY KEY,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL
);