IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [People] (
    [Id] uniqueidentifier NOT NULL DEFAULT (newid()),
    [FirstName] nvarchar(50) NOT NULL,
    [LastName] nvarchar(50) NOT NULL,
    [IdDocument] nvarchar(8) NOT NULL,
    [PhoneNumber] nvarchar(15) NULL,
    [Discriminator] nvarchar(max) NOT NULL,
    [Password] nvarchar(4) NULL,
    [Status] int NULL,
    CONSTRAINT [PK_People] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Accounts] (
    [Id] uniqueidentifier NOT NULL DEFAULT (newid()),
    [AccountNumber] nvarchar(50) NOT NULL,
    [AccountType] int NOT NULL,
    [InitialBalance] decimal(18,2) NOT NULL,
    [Balance] decimal(18,2) NOT NULL,
    [Status] int NOT NULL,
    [ClientId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Accounts] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Accounts_People_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [People] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Addresses] (
    [Id] uniqueidentifier NOT NULL DEFAULT (newid()),
    [Street] nvarchar(100) NULL,
    [City] nvarchar(50) NULL,
    [State] nvarchar(50) NULL,
    [PostalCode] nvarchar(10) NULL,
    [Country] nvarchar(50) NULL,
    CONSTRAINT [PK_Addresses] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Addresses_People_Id] FOREIGN KEY ([Id]) REFERENCES [People] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Transactions] (
    [Id] uniqueidentifier NOT NULL DEFAULT (newid()),
    [Date] datetime2 NOT NULL,
    [Amount] decimal(18,2) NOT NULL,
    [TransactionType] int NOT NULL,
    [AccountId] uniqueidentifier NOT NULL,
    [CounterpartyAccountId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Transactions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Transactions_Accounts_AccountId] FOREIGN KEY ([AccountId]) REFERENCES [Accounts] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Transactions_Accounts_CounterpartyAccountId] FOREIGN KEY ([CounterpartyAccountId]) REFERENCES [Accounts] ([Id])
);
GO

CREATE INDEX [IX_Accounts_ClientId] ON [Accounts] ([ClientId]);
GO

CREATE INDEX [IX_Transactions_AccountId] ON [Transactions] ([AccountId]);
GO

CREATE INDEX [IX_Transactions_CounterpartyAccountId] ON [Transactions] ([CounterpartyAccountId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230728145537_InitialCreate', N'7.0.9');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Discriminator', N'FirstName', N'IdDocument', N'LastName', N'Password', N'PhoneNumber', N'Status') AND [object_id] = OBJECT_ID(N'[People]'))
    SET IDENTITY_INSERT [People] ON;
INSERT INTO [People] ([Id], [Discriminator], [FirstName], [IdDocument], [LastName], [Password], [PhoneNumber], [Status])
VALUES ('1cfec38a-5cc2-4aa9-9775-9425494d359c', N'Client', N'Marianela', N'91011121', N'Montalvo', N'5678', N'097548965', 2),
('43215da9-8700-457b-83cd-e2b9ffc63468', N'Client', N'Juan', N'31415161', N'Osorio', N'1245', N'098874587', 2),
('8df0f660-ec84-4295-83ea-4ccede958993', N'Client', N'Jose', N'12345678', N'Lema', N'1234', N'098254785', 2);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Discriminator', N'FirstName', N'IdDocument', N'LastName', N'Password', N'PhoneNumber', N'Status') AND [object_id] = OBJECT_ID(N'[People]'))
    SET IDENTITY_INSERT [People] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccountNumber', N'AccountType', N'Balance', N'ClientId', N'InitialBalance', N'Status') AND [object_id] = OBJECT_ID(N'[Accounts]'))
    SET IDENTITY_INSERT [Accounts] ON;
INSERT INTO [Accounts] ([Id], [AccountNumber], [AccountType], [Balance], [ClientId], [InitialBalance], [Status])
VALUES ('a0bbf2c2-cae5-4108-8fe0-a2364e2579ff', N'496825', 2, 540.0, '1cfec38a-5cc2-4aa9-9775-9425494d359c', 0.0, 2),
('c12aa5d0-6f53-4bf8-8148-4ba498fd7127', N'478758', 1, 2000.0, '8df0f660-ec84-4295-83ea-4ccede958993', 0.0, 2),
('c983e8c3-6487-4b47-9ff6-5ee3aad89de0', N'495878', 1, 0.0, '43215da9-8700-457b-83cd-e2b9ffc63468', 0.0, 2),
('fe27f0f2-22ca-4592-9c65-922f62b1ae19', N'225487', 2, 100.0, '1cfec38a-5cc2-4aa9-9775-9425494d359c', 0.0, 2);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccountNumber', N'AccountType', N'Balance', N'ClientId', N'InitialBalance', N'Status') AND [object_id] = OBJECT_ID(N'[Accounts]'))
    SET IDENTITY_INSERT [Accounts] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'City', N'Country', N'PostalCode', N'State', N'Street') AND [object_id] = OBJECT_ID(N'[Addresses]'))
    SET IDENTITY_INSERT [Addresses] ON;
INSERT INTO [Addresses] ([Id], [City], [Country], [PostalCode], [State], [Street])
VALUES ('1cfec38a-5cc2-4aa9-9775-9425494d359c', N'Santiago de Surco', N'Perú', N'15023', N'Lima', N'Avenida Caminos del Inca'),
('43215da9-8700-457b-83cd-e2b9ffc63468', N'La Molina', N'Perú', N'15026', N'Lima', N'Avenida La Molina'),
('8df0f660-ec84-4295-83ea-4ccede958993', N'Lima', N'Perú', N'15001', N'Lima', N'Jirón de la Unión');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'City', N'Country', N'PostalCode', N'State', N'Street') AND [object_id] = OBJECT_ID(N'[Addresses]'))
    SET IDENTITY_INSERT [Addresses] OFF;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230728150635_SeedData', N'7.0.9');
GO

COMMIT;
GO

