IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190403185735_AlausDarykla')
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [role] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190403185735_AlausDarykla')
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] uniqueidentifier NOT NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [Email] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        [Vardas] nvarchar(max) NULL,
        [Pavarde] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190403185735_AlausDarykla')
BEGIN
    CREATE TABLE [Klientas] (
        [KlientoID] numeric(6, 0) NOT NULL IDENTITY,
        [KlientoPavadinimas] varchar(50) NOT NULL,
        CONSTRAINT [PK_Klientas] PRIMARY KEY NONCLUSTERED ([KlientoID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190403185735_AlausDarykla')
BEGIN
    CREATE TABLE [LaisviResursai] (
        [FormosID] numeric(6, 0) NOT NULL IDENTITY,
        [LaisvuResursuKodas] char(1) NOT NULL,
        [ResursoPavadinimas] nvarchar(50) NOT NULL,
        CONSTRAINT [PK_LaisviResursai] PRIMARY KEY NONCLUSTERED ([FormosID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190403185735_AlausDarykla')
BEGIN
    CREATE TABLE [Tiekejas] (
        [TiekejoID] numeric(6, 0) NOT NULL IDENTITY,
        [TiekejoPavadinimas] varchar(50) NOT NULL,
        [TiekejoTtipas] char(1) NOT NULL,
        CONSTRAINT [PK_Tiekejas] PRIMARY KEY NONCLUSTERED ([TiekejoID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190403185735_AlausDarykla')
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] uniqueidentifier NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190403185735_AlausDarykla')
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] uniqueidentifier NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190403185735_AlausDarykla')
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(450) NOT NULL,
        [ProviderKey] nvarchar(450) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190403185735_AlausDarykla')
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] uniqueidentifier NOT NULL,
        [RoleId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190403185735_AlausDarykla')
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] uniqueidentifier NOT NULL,
        [LoginProvider] nvarchar(450) NOT NULL,
        [Name] nvarchar(450) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190403185735_AlausDarykla')
BEGIN
    CREATE TABLE [KlientoUzsakymoAplankas] (
        [KlientoUzsakymoID] numeric(6, 0) NOT NULL IDENTITY,
        [KlientoID] numeric(6, 0) NOT NULL,
        [Terminas] datetime NOT NULL,
        [Busena] char(1) NOT NULL,
        [Archyvavimo_Komentaras] nvarchar(500) NULL,
        CONSTRAINT [PK_KlientoUzsakymoAplankas] PRIMARY KEY NONCLUSTERED ([KlientoUzsakymoID]),
        CONSTRAINT [FK_KlientoUzsakymoAplankas_Klientas_KlientoID] FOREIGN KEY ([KlientoID]) REFERENCES [Klientas] ([KlientoID]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190403185735_AlausDarykla')
BEGIN
    CREATE TABLE [Ingredientas] (
        [IngredientoID] numeric(6, 0) NOT NULL IDENTITY,
        [FormosID] numeric(6, 0) NULL,
        [Ingrediento pavadinimas] varchar(50) NOT NULL,
        [LaisvasKiekis] float NULL,
        CONSTRAINT [PK_Ingredientas] PRIMARY KEY NONCLUSTERED ([IngredientoID]),
        CONSTRAINT [FK_INGREDIE_RELATIONS_LAISVIRE] FOREIGN KEY ([FormosID]) REFERENCES [LaisviResursai] ([FormosID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190403185735_AlausDarykla')
BEGIN
    CREATE TABLE [TechnikosPrietaisas] (
        [TechnikosPrietaisoID] numeric(6, 0) NOT NULL IDENTITY,
        [FormosID] numeric(6, 0) NULL,
        [Prietaiso pavadinimas] varchar(50) NOT NULL,
        [LaisvasKiekis] float NULL,
        CONSTRAINT [PK_TechnikosPrietaisas] PRIMARY KEY NONCLUSTERED ([TechnikosPrietaisoID]),
        CONSTRAINT [FK_TECHNIKO_TURI55_LAISVIRE] FOREIGN KEY ([FormosID]) REFERENCES [LaisviResursai] ([FormosID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190403185735_AlausDarykla')
BEGIN
    CREATE TABLE [GamybosInstrukcija] (
        [KlientoUzsakymoID] numeric(6, 0) NOT NULL,
        [InstrukcijosID] numeric(6, 0) NOT NULL IDENTITY,
        [Busena] char(1) NOT NULL,
        [RecepturaParuosta] char(1) NOT NULL,
        [TechnikosPatarimaiParuosti] char(1) NOT NULL,
        [GamybosTerminas] datetime NOT NULL,
        [Brandinimo laikas] varchar(50) NULL,
        CONSTRAINT [PK_GamybosInstrukcija] PRIMARY KEY NONCLUSTERED ([KlientoUzsakymoID], [InstrukcijosID]),
        CONSTRAINT [FK_GAMYBOSI_TURI_KLIENTOU] FOREIGN KEY ([KlientoUzsakymoID]) REFERENCES [KlientoUzsakymoAplankas] ([KlientoUzsakymoID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190403185735_AlausDarykla')
BEGIN
    CREATE TABLE [LaisviResursaiUzsakymui] (
        [KlientoUzsakymoID] numeric(6, 0) NOT NULL,
        [FormosID] numeric(6, 0) NOT NULL,
        [LaisvuResursuTipas] char(1) NOT NULL,
        CONSTRAINT [PK_LaisviResursaiUzsakymui] PRIMARY KEY NONCLUSTERED ([KlientoUzsakymoID], [FormosID]),
        CONSTRAINT [FK__LaisviRes__Formo__13BCEBC1] FOREIGN KEY ([FormosID]) REFERENCES [LaisviResursai] ([FormosID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_LaisviResursaiUzsakymui_KlientoUzsakymoAplankas_KlientoUzsakymoID] FOREIGN KEY ([KlientoUzsakymoID]) REFERENCES [KlientoUzsakymoAplankas] ([KlientoUzsakymoID]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190403185735_AlausDarykla')
BEGIN
    CREATE TABLE [Sutartis] (
        [SutartiesID] numeric(6, 0) NOT NULL IDENTITY,
        [KlientoUzsakymoID] numeric(6, 0) NULL,
        [TiekejoID] numeric(6, 0) NULL,
        [KlientoID] numeric(6, 0) NULL,
        [SutartiesPasirasymoData] datetime NOT NULL,
        [SutartiesTerminas] datetime NOT NULL,
        CONSTRAINT [PK_Sutartis] PRIMARY KEY ([SutartiesID]),
        CONSTRAINT [FK_SUTARTIS_TURI13_KLIENTAS] FOREIGN KEY ([KlientoID]) REFERENCES [Klientas] ([KlientoID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_SUTARTIS_TURI14_KLIENTOU] FOREIGN KEY ([KlientoUzsakymoID]) REFERENCES [KlientoUzsakymoAplankas] ([KlientoUzsakymoID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_SUTARTIS_TURI12_TIEKEJAS] FOREIGN KEY ([TiekejoID]) REFERENCES [Tiekejas] ([TiekejoID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190403185735_AlausDarykla')
BEGIN
    CREATE TABLE [IngredientoPrasymas] (
        [KlientoUzsakymoID] numeric(6, 0) NOT NULL,
        [IngredientoID] numeric(6, 0) NOT NULL,
        [Ingrediento kiekis] float NOT NULL,
        [Busena] char(1) NOT NULL,
        CONSTRAINT [PK_IngredientoPrasymas] PRIMARY KEY NONCLUSTERED ([KlientoUzsakymoID], [IngredientoID]),
        CONSTRAINT [FK_INGREDIE_TURI9_INGREDIE] FOREIGN KEY ([IngredientoID]) REFERENCES [Ingredientas] ([IngredientoID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_INGREDIE_TURI8_KLIENTOU] FOREIGN KEY ([KlientoUzsakymoID]) REFERENCES [KlientoUzsakymoAplankas] ([KlientoUzsakymoID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190403185735_AlausDarykla')
BEGIN
    CREATE TABLE [Technikos prietaiso prasymas] (
        [KlientoUzsakymoID] numeric(6, 0) NOT NULL,
        [TechnikosPrietaisoID] numeric(6, 0) NOT NULL,
        [Prietaiso kiekis] float NOT NULL,
        [Busena] char(1) NOT NULL,
        CONSTRAINT [PK_Technikos prietaiso prasymas] PRIMARY KEY NONCLUSTERED ([KlientoUzsakymoID], [TechnikosPrietaisoID]),
        CONSTRAINT [FK_TECHNIKO_TURI11_KLIENTOU] FOREIGN KEY ([KlientoUzsakymoID]) REFERENCES [KlientoUzsakymoAplankas] ([KlientoUzsakymoID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_TECHNIKO_TURI10_TECHNIKO] FOREIGN KEY ([TechnikosPrietaisoID]) REFERENCES [TechnikosPrietaisas] ([TechnikosPrietaisoID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190403185735_AlausDarykla')
BEGIN
    CREATE TABLE [IngredientoReceptas] (
        [KlientoUzsakymoID] numeric(6, 0) NOT NULL,
        [InstrukcijosID] numeric(6, 0) NOT NULL,
        [IngredientoID] numeric(6, 0) NOT NULL,
        [Ingrediento kiekis] float NOT NULL,
        [Virimo laikas] nvarchar(50) NULL,
        [Fermentacijos laikas] nvarchar(50) NULL,
        CONSTRAINT [PK_IngredientoReceptas] PRIMARY KEY NONCLUSTERED ([KlientoUzsakymoID], [InstrukcijosID], [IngredientoID]),
        CONSTRAINT [FK_INGREDIE_TURI6_INGREDIE] FOREIGN KEY ([IngredientoID]) REFERENCES [Ingredientas] ([IngredientoID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_INGREDIE_TURI7_GAMYBOSI] FOREIGN KEY ([KlientoUzsakymoID], [InstrukcijosID]) REFERENCES [GamybosInstrukcija] ([KlientoUzsakymoID], [InstrukcijosID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190403185735_AlausDarykla')
BEGIN
    CREATE TABLE [Technikos prietaiso patarimas] (
        [TechnikosPrietaisoID] numeric(6, 0) NOT NULL,
        [KlientoUzsakymoID] numeric(6, 0) NOT NULL,
        [InstrukcijosID] numeric(6, 0) NOT NULL,
        [Prietaiso komentaras] varchar(1000) NULL,
        [PrietaisoKiekis] float NOT NULL,
        CONSTRAINT [PK_Technikos prietaiso patarimas] PRIMARY KEY NONCLUSTERED ([TechnikosPrietaisoID], [KlientoUzsakymoID], [InstrukcijosID]),
        CONSTRAINT [FK_TECHNIKO_TURI5_TECHNIKO] FOREIGN KEY ([TechnikosPrietaisoID]) REFERENCES [TechnikosPrietaisas] ([TechnikosPrietaisoID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_TECHNIKO_TURI4_GAMYBOSI] FOREIGN KEY ([KlientoUzsakymoID], [InstrukcijosID]) REFERENCES [GamybosInstrukcija] ([KlientoUzsakymoID], [InstrukcijosID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190403185735_AlausDarykla')
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190403185735_AlausDarykla')
BEGIN
    CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190403185735_AlausDarykla')
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190403185735_AlausDarykla')
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190403185735_AlausDarykla')
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190403185735_AlausDarykla')
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190403185735_AlausDarykla')
BEGIN
    CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190403185735_AlausDarykla')
BEGIN
    CREATE UNIQUE INDEX [TURI_FK] ON [GamybosInstrukcija] ([KlientoUzsakymoID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190403185735_AlausDarykla')
BEGIN
    CREATE INDEX [RELATIONSHIP_24_FK] ON [Ingredientas] ([FormosID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190403185735_AlausDarykla')
BEGIN
    CREATE INDEX [TURI9_FK] ON [IngredientoPrasymas] ([IngredientoID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190403185735_AlausDarykla')
BEGIN
    CREATE INDEX [TURI8_FK] ON [IngredientoPrasymas] ([KlientoUzsakymoID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190403185735_AlausDarykla')
BEGIN
    CREATE INDEX [TURI6_FK] ON [IngredientoReceptas] ([IngredientoID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190403185735_AlausDarykla')
BEGIN
    CREATE INDEX [TURI7_FK] ON [IngredientoReceptas] ([KlientoUzsakymoID], [InstrukcijosID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190403185735_AlausDarykla')
BEGIN
    CREATE INDEX [TURI15_FK] ON [KlientoUzsakymoAplankas] ([KlientoID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190403185735_AlausDarykla')
BEGIN
    CREATE INDEX [index_FormosTuri] ON [LaisviResursaiUzsakymui] ([FormosID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190403185735_AlausDarykla')
BEGIN
    CREATE INDEX [index_KlientoUzsakymoTuri] ON [LaisviResursaiUzsakymui] ([KlientoUzsakymoID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190403185735_AlausDarykla')
BEGIN
    CREATE INDEX [TURI13_FK] ON [Sutartis] ([KlientoID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190403185735_AlausDarykla')
BEGIN
    CREATE UNIQUE INDEX [TURI14_FK] ON [Sutartis] ([KlientoUzsakymoID]) WHERE [KlientoUzsakymoID] IS NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190403185735_AlausDarykla')
BEGIN
    CREATE INDEX [TURI12_FK] ON [Sutartis] ([TiekejoID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190403185735_AlausDarykla')
BEGIN
    CREATE INDEX [TURI5_FK] ON [Technikos prietaiso patarimas] ([TechnikosPrietaisoID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190403185735_AlausDarykla')
BEGIN
    CREATE INDEX [TURI4_FK] ON [Technikos prietaiso patarimas] ([KlientoUzsakymoID], [InstrukcijosID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190403185735_AlausDarykla')
BEGIN
    CREATE INDEX [TURI11_FK] ON [Technikos prietaiso prasymas] ([KlientoUzsakymoID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190403185735_AlausDarykla')
BEGIN
    CREATE INDEX [TURI10_FK] ON [Technikos prietaiso prasymas] ([TechnikosPrietaisoID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190403185735_AlausDarykla')
BEGIN
    CREATE INDEX [TURI55_FK] ON [TechnikosPrietaisas] ([FormosID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190403185735_AlausDarykla')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190403185735_AlausDarykla', N'2.2.1-servicing-10028');
END;

GO

