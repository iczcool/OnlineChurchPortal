IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190805174548_firstmigration')
BEGIN
    CREATE TABLE [Addresses] (
        [ID] int NOT NULL IDENTITY,
        [StreetName] nvarchar(100) NULL,
        [StreetNumber] nvarchar(max) NULL,
        [Postcode] nvarchar(max) NULL,
        [Town] nvarchar(100) NULL,
        [Country] nvarchar(100) NULL,
        CONSTRAINT [PK_Addresses] PRIMARY KEY ([ID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190805174548_firstmigration')
BEGIN
    CREATE TABLE [ChurchGroups] (
        [ID] int NOT NULL IDENTITY,
        [GroupName] nvarchar(60) NULL,
        [GroupDescription] nvarchar(max) NULL,
        [MeetingDay] nvarchar(max) NULL,
        [StartTime] datetime2 NOT NULL,
        [EndTime] datetime2 NOT NULL,
        [Location] nvarchar(60) NULL,
        [LeaderName] nvarchar(60) NULL,
        [LeaderProfile] nvarchar(100) NULL,
        CONSTRAINT [PK_ChurchGroups] PRIMARY KEY ([ID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190805174548_firstmigration')
BEGIN
    CREATE TABLE [RegUsers] (
        [ID] int NOT NULL IDENTITY,
        [FirstName] nvarchar(60) NOT NULL,
        [LastName] nvarchar(60) NOT NULL,
        [Sex] nvarchar(max) NOT NULL,
        [MaritalStatus] nvarchar(max) NOT NULL,
        [DateOFBirth] datetime2 NOT NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [Email] nvarchar(max) NOT NULL,
        [MemType] nvarchar(max) NULL,
        [DateOfMembership] datetime2 NOT NULL,
        [AddressID] int NULL,
        CONSTRAINT [PK_RegUsers] PRIMARY KEY ([ID]),
        CONSTRAINT [FK_RegUsers_Addresses_AddressID] FOREIGN KEY ([AddressID]) REFERENCES [Addresses] ([ID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190805174548_firstmigration')
BEGIN
    CREATE TABLE [Enquiries] (
        [ID] int NOT NULL IDENTITY,
        [EnquiryDate] datetime2 NOT NULL,
        [EnquiryType] nvarchar(60) NULL,
        [EnquiryBody] nvarchar(max) NULL,
        [RegUserID] int NULL,
        CONSTRAINT [PK_Enquiries] PRIMARY KEY ([ID]),
        CONSTRAINT [FK_Enquiries_RegUsers_RegUserID] FOREIGN KEY ([RegUserID]) REFERENCES [RegUsers] ([ID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190805174548_firstmigration')
BEGIN
    CREATE TABLE [JoinRegUserAndChurchGroups] (
        [RegUserID] int NOT NULL,
        [ChurchGroupID] int NOT NULL,
        CONSTRAINT [PK_JoinRegUserAndChurchGroups] PRIMARY KEY ([RegUserID], [ChurchGroupID]),
        CONSTRAINT [FK_JoinRegUserAndChurchGroups_ChurchGroups_ChurchGroupID] FOREIGN KEY ([ChurchGroupID]) REFERENCES [ChurchGroups] ([ID]) ON DELETE CASCADE,
        CONSTRAINT [FK_JoinRegUserAndChurchGroups_RegUsers_RegUserID] FOREIGN KEY ([RegUserID]) REFERENCES [RegUsers] ([ID]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190805174548_firstmigration')
BEGIN
    CREATE TABLE [Payments] (
        [ID] int NOT NULL IDENTITY,
        [PaymentType] nvarchar(30) NOT NULL,
        [PaymentDate] datetime2 NOT NULL,
        [Amount] float NOT NULL,
        [RegUserID] int NOT NULL,
        CONSTRAINT [PK_Payments] PRIMARY KEY ([ID]),
        CONSTRAINT [FK_Payments_RegUsers_RegUserID] FOREIGN KEY ([RegUserID]) REFERENCES [RegUsers] ([ID]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190805174548_firstmigration')
BEGIN
    CREATE TABLE [UserAccounts] (
        [ID] int NOT NULL IDENTITY,
        [FirstName] nvarchar(60) NOT NULL,
        [LastName] nvarchar(60) NOT NULL,
        [UserName] nvarchar(60) NOT NULL,
        [Password] nvarchar(60) NOT NULL,
        [Roles] nvarchar(max) NULL,
        [RegUserID] int NULL,
        CONSTRAINT [PK_UserAccounts] PRIMARY KEY ([ID]),
        CONSTRAINT [FK_UserAccounts_RegUsers_RegUserID] FOREIGN KEY ([RegUserID]) REFERENCES [RegUsers] ([ID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190805174548_firstmigration')
BEGIN
    CREATE INDEX [IX_Enquiries_RegUserID] ON [Enquiries] ([RegUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190805174548_firstmigration')
BEGIN
    CREATE INDEX [IX_JoinRegUserAndChurchGroups_ChurchGroupID] ON [JoinRegUserAndChurchGroups] ([ChurchGroupID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190805174548_firstmigration')
BEGIN
    CREATE INDEX [IX_Payments_RegUserID] ON [Payments] ([RegUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190805174548_firstmigration')
BEGIN
    CREATE INDEX [IX_RegUsers_AddressID] ON [RegUsers] ([AddressID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190805174548_firstmigration')
BEGIN
    CREATE INDEX [IX_UserAccounts_RegUserID] ON [UserAccounts] ([RegUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190805174548_firstmigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190805174548_firstmigration', N'2.2.4-servicing-10062');
END;

GO

