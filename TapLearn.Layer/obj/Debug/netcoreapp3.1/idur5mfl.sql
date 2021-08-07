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

CREATE TABLE [courseGroups] (
    [GroupId] int NOT NULL IDENTITY,
    [GroupTitel] nvarchar(200) NOT NULL,
    [IsDelete] bit NOT NULL,
    [ParentId] int NULL,
    CONSTRAINT [PK_courseGroups] PRIMARY KEY ([GroupId]),
    CONSTRAINT [FK_courseGroups_courseGroups_ParentId] FOREIGN KEY ([ParentId]) REFERENCES [courseGroups] ([GroupId]) ON DELETE NO ACTION
);
GO

CREATE TABLE [courseLevels] (
    [LevelId] int NOT NULL IDENTITY,
    [LevelTitle] nvarchar(150) NOT NULL,
    CONSTRAINT [PK_courseLevels] PRIMARY KEY ([LevelId])
);
GO

CREATE TABLE [courseStatuses] (
    [StatusId] int NOT NULL IDENTITY,
    [StatusTitel] nvarchar(150) NOT NULL,
    CONSTRAINT [PK_courseStatuses] PRIMARY KEY ([StatusId])
);
GO

CREATE TABLE [disCounts] (
    [DisCountId] int NOT NULL IDENTITY,
    [DiscountCode] nvarchar(max) NOT NULL,
    [DiscountPercent] int NOT NULL,
    [UsableCount] int NULL,
    [StartDate] datetime2 NULL,
    [EndDate] datetime2 NULL,
    CONSTRAINT [PK_disCounts] PRIMARY KEY ([DisCountId])
);
GO

CREATE TABLE [permissions] (
    [PermissionId] int NOT NULL IDENTITY,
    [PermissionTitle] nvarchar(200) NOT NULL,
    [ParentId] int NULL,
    CONSTRAINT [PK_permissions] PRIMARY KEY ([PermissionId]),
    CONSTRAINT [FK_permissions_permissions_ParentId] FOREIGN KEY ([ParentId]) REFERENCES [permissions] ([PermissionId]) ON DELETE NO ACTION
);
GO

CREATE TABLE [roles] (
    [Roleid] int NOT NULL IDENTITY,
    [RoleTitel] nvarchar(200) NOT NULL,
    [IsDelete] bit NOT NULL,
    CONSTRAINT [PK_roles] PRIMARY KEY ([Roleid])
);
GO

CREATE TABLE [users] (
    [Userid] int NOT NULL IDENTITY,
    [UserName] nvarchar(200) NOT NULL,
    [Email] nvarchar(200) NOT NULL,
    [ActiveCode] nvarchar(50) NULL,
    [IsActive] bit NOT NULL,
    [UserAvatar] nvarchar(200) NULL,
    [RegisterDate] datetime2 NOT NULL,
    [Password] nvarchar(200) NOT NULL,
    [IsDelete] bit NOT NULL,
    CONSTRAINT [PK_users] PRIMARY KEY ([Userid])
);
GO

CREATE TABLE [walletTypes] (
    [TypeId] int NOT NULL,
    [TypeTitel] nvarchar(150) NOT NULL,
    CONSTRAINT [PK_walletTypes] PRIMARY KEY ([TypeId])
);
GO

CREATE TABLE [rolePermissions] (
    [RP_ID] int NOT NULL IDENTITY,
    [Roleid] int NOT NULL,
    [PermissionId] int NOT NULL,
    CONSTRAINT [PK_rolePermissions] PRIMARY KEY ([RP_ID]),
    CONSTRAINT [FK_rolePermissions_permissions_PermissionId] FOREIGN KEY ([PermissionId]) REFERENCES [permissions] ([PermissionId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_rolePermissions_roles_Roleid] FOREIGN KEY ([Roleid]) REFERENCES [roles] ([Roleid]) ON DELETE NO ACTION
);
GO

CREATE TABLE [curses] (
    [CourseId] int NOT NULL IDENTITY,
    [GroupId] int NOT NULL,
    [SubGroup] int NULL,
    [TeacherId] int NOT NULL,
    [SId] int NOT NULL,
    [Lid] int NOT NULL,
    [CourseTitel] nvarchar(450) NOT NULL,
    [CorseDescription] nvarchar(max) NOT NULL,
    [CoursePrice] int NOT NULL,
    [Tags] nvarchar(600) NULL,
    [CourseImageName] nvarchar(50) NULL,
    [DemoFileName] nvarchar(100) NULL,
    [CreateDate] datetime2 NOT NULL,
    [UpdateData] datetime2 NULL,
    CONSTRAINT [PK_curses] PRIMARY KEY ([CourseId]),
    CONSTRAINT [FK_curses_courseGroups_GroupId] FOREIGN KEY ([GroupId]) REFERENCES [courseGroups] ([GroupId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_curses_courseGroups_SubGroup] FOREIGN KEY ([SubGroup]) REFERENCES [courseGroups] ([GroupId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_curses_courseLevels_Lid] FOREIGN KEY ([Lid]) REFERENCES [courseLevels] ([LevelId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_curses_courseStatuses_SId] FOREIGN KEY ([SId]) REFERENCES [courseStatuses] ([StatusId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_curses_users_TeacherId] FOREIGN KEY ([TeacherId]) REFERENCES [users] ([Userid]) ON DELETE NO ACTION
);
GO

CREATE TABLE [orders] (
    [OrderId] int NOT NULL IDENTITY,
    [Uid] int NOT NULL,
    [IsFainally] bit NOT NULL,
    [CreateDate] datetime2 NOT NULL,
    [OrderSum] int NOT NULL,
    CONSTRAINT [PK_orders] PRIMARY KEY ([OrderId]),
    CONSTRAINT [FK_orders_users_Uid] FOREIGN KEY ([Uid]) REFERENCES [users] ([Userid]) ON DELETE NO ACTION
);
GO

CREATE TABLE [userDiscountCodes] (
    [US_Id] int NOT NULL IDENTITY,
    [Userid] int NOT NULL,
    [DisCountId] int NOT NULL,
    CONSTRAINT [PK_userDiscountCodes] PRIMARY KEY ([US_Id]),
    CONSTRAINT [FK_userDiscountCodes_disCounts_DisCountId] FOREIGN KEY ([DisCountId]) REFERENCES [disCounts] ([DisCountId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_userDiscountCodes_users_Userid] FOREIGN KEY ([Userid]) REFERENCES [users] ([Userid]) ON DELETE NO ACTION
);
GO

CREATE TABLE [userRoles] (
    [UR_Id] int NOT NULL IDENTITY,
    [Roleid] int NOT NULL,
    [Userid] int NOT NULL,
    CONSTRAINT [PK_userRoles] PRIMARY KEY ([UR_Id]),
    CONSTRAINT [FK_userRoles_roles_Roleid] FOREIGN KEY ([Roleid]) REFERENCES [roles] ([Roleid]) ON DELETE NO ACTION,
    CONSTRAINT [FK_userRoles_users_Userid] FOREIGN KEY ([Userid]) REFERENCES [users] ([Userid]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Wallets] (
    [walletid] int NOT NULL IDENTITY,
    [TypeId] int NOT NULL,
    [Userid] int NOT NULL,
    [Amount] int NOT NULL,
    [Description] nvarchar(500) NOT NULL,
    [IsPay] bit NOT NULL,
    [CreateDate] datetime2 NOT NULL,
    CONSTRAINT [PK_Wallets] PRIMARY KEY ([walletid]),
    CONSTRAINT [FK_Wallets_users_Userid] FOREIGN KEY ([Userid]) REFERENCES [users] ([Userid]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Wallets_walletTypes_TypeId] FOREIGN KEY ([TypeId]) REFERENCES [walletTypes] ([TypeId]) ON DELETE NO ACTION
);
GO

CREATE TABLE [CourseComment] (
    [CommentId] int NOT NULL IDENTITY,
    [Courseid] int NOT NULL,
    [userid] int NOT NULL,
    [Comment] nvarchar(700) NULL,
    [IsDelete] bit NOT NULL,
    [IsAdminRead] bit NOT NULL,
    [CreateDate] datetime2 NOT NULL,
    CONSTRAINT [PK_CourseComment] PRIMARY KEY ([CommentId]),
    CONSTRAINT [FK_CourseComment_curses_Courseid] FOREIGN KEY ([Courseid]) REFERENCES [curses] ([CourseId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_CourseComment_users_userid] FOREIGN KEY ([userid]) REFERENCES [users] ([Userid]) ON DELETE NO ACTION
);
GO

CREATE TABLE [courseEpisods] (
    [EpisodeId] int NOT NULL IDENTITY,
    [CId] int NOT NULL,
    [EpisodeTitle] nvarchar(400) NOT NULL,
    [EpisodeTime] time NOT NULL,
    [EpisodeFileName] nvarchar(max) NULL,
    [IsFree] bit NOT NULL,
    CONSTRAINT [PK_courseEpisods] PRIMARY KEY ([EpisodeId]),
    CONSTRAINT [FK_courseEpisods_curses_CId] FOREIGN KEY ([CId]) REFERENCES [curses] ([CourseId]) ON DELETE NO ACTION
);
GO

CREATE TABLE [useCourses] (
    [UC_id] int NOT NULL IDENTITY,
    [userid] int NOT NULL,
    [courseid] int NOT NULL,
    [curseCourseId] int NULL,
    CONSTRAINT [PK_useCourses] PRIMARY KEY ([UC_id]),
    CONSTRAINT [FK_useCourses_curses_curseCourseId] FOREIGN KEY ([curseCourseId]) REFERENCES [curses] ([CourseId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_useCourses_users_userid] FOREIGN KEY ([userid]) REFERENCES [users] ([Userid]) ON DELETE NO ACTION
);
GO

CREATE TABLE [detail] (
    [DetailsId] int NOT NULL IDENTITY,
    [OId] int NOT NULL,
    [Courseid] int NOT NULL,
    [Price] int NOT NULL,
    [Count] int NOT NULL,
    CONSTRAINT [PK_detail] PRIMARY KEY ([DetailsId]),
    CONSTRAINT [FK_detail_curses_Courseid] FOREIGN KEY ([Courseid]) REFERENCES [curses] ([CourseId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_detail_orders_OId] FOREIGN KEY ([OId]) REFERENCES [orders] ([OrderId]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_CourseComment_Courseid] ON [CourseComment] ([Courseid]);
GO

CREATE INDEX [IX_CourseComment_userid] ON [CourseComment] ([userid]);
GO

CREATE INDEX [IX_courseEpisods_CId] ON [courseEpisods] ([CId]);
GO

CREATE INDEX [IX_courseGroups_ParentId] ON [courseGroups] ([ParentId]);
GO

CREATE INDEX [IX_curses_GroupId] ON [curses] ([GroupId]);
GO

CREATE INDEX [IX_curses_Lid] ON [curses] ([Lid]);
GO

CREATE INDEX [IX_curses_SId] ON [curses] ([SId]);
GO

CREATE INDEX [IX_curses_SubGroup] ON [curses] ([SubGroup]);
GO

CREATE INDEX [IX_curses_TeacherId] ON [curses] ([TeacherId]);
GO

CREATE INDEX [IX_detail_Courseid] ON [detail] ([Courseid]);
GO

CREATE INDEX [IX_detail_OId] ON [detail] ([OId]);
GO

CREATE INDEX [IX_orders_Uid] ON [orders] ([Uid]);
GO

CREATE INDEX [IX_permissions_ParentId] ON [permissions] ([ParentId]);
GO

CREATE INDEX [IX_rolePermissions_PermissionId] ON [rolePermissions] ([PermissionId]);
GO

CREATE INDEX [IX_rolePermissions_Roleid] ON [rolePermissions] ([Roleid]);
GO

CREATE INDEX [IX_useCourses_curseCourseId] ON [useCourses] ([curseCourseId]);
GO

CREATE INDEX [IX_useCourses_userid] ON [useCourses] ([userid]);
GO

CREATE INDEX [IX_userDiscountCodes_DisCountId] ON [userDiscountCodes] ([DisCountId]);
GO

CREATE INDEX [IX_userDiscountCodes_Userid] ON [userDiscountCodes] ([Userid]);
GO

CREATE INDEX [IX_userRoles_Roleid] ON [userRoles] ([Roleid]);
GO

CREATE INDEX [IX_userRoles_Userid] ON [userRoles] ([Userid]);
GO

CREATE INDEX [IX_Wallets_TypeId] ON [Wallets] ([TypeId]);
GO

CREATE INDEX [IX_Wallets_Userid] ON [Wallets] ([Userid]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210703103434_migcomment', N'5.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210703103841_mg-empty', N'5.0.7');
GO

COMMIT;
GO

