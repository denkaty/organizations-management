using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Data.Models.SqlQueries.CommonQueries
{
    public static class CreateTableQueries
    {
        public const string Country =
			@"CREATE TABLE [Country] (
			[Id] NVARCHAR(36),
			[Name] NVARCHAR(255),
            [IsDeleted] BIT NOT NULL DEFAULT 0,
			CONSTRAINT PK_Country_Id PRIMARY KEY ([Id]),
            CONSTRAINT UQ_Country_Name UNIQUE ([Name])
			);";

        public const string Industry =
			@"CREATE TABLE [Industry] (
			[Id] NVARCHAR(36),
			[Name] NVARCHAR(255),
            [IsDeleted] BIT NOT NULL DEFAULT 0,
			CONSTRAINT PK_Industry_Id PRIMARY KEY ([Id]),
            CONSTRAINT UQ_Industry_Name UNIQUE ([Name])
			);";

        public const string Organization =
			@"CREATE TABLE [Organization] (
            [Id] NVARCHAR(36),
            [OrganizationId] NVARCHAR(36),
            [Name] NVARCHAR(255),
            [Website] NVARCHAR(255),
            [Description] NVARCHAR(255),
            [Founded_year] INT,
            [Employees] INT,
            [Country_Id] NVARCHAR(36) NULL,
            [IsDeleted] BIT NOT NULL DEFAULT 0,
            CONSTRAINT PK_Organization_Id PRIMARY KEY ([Id]),
            CONSTRAINT FK_Organization_Country_Id_Country_Id FOREIGN KEY ([Country_Id]) REFERENCES Country([Id]),
            CONSTRAINT UQ_Organization_OrganizationId UNIQUE ([OrganizationId]),
            CONSTRAINT UQ_Organization_Name UNIQUE ([Name])
            );";

        public const string OrganizationIndustry =
            @"CREATE TABLE [OrganizationIndustry] (
            [Organization_Id] NVARCHAR(36),
            [Industry_Id] NVARCHAR(36),
            CONSTRAINT PK_OrganizationIndustry PRIMARY KEY ([Organization_Id], [Industry_Id]),
            CONSTRAINT FK_OrganizationIndustry_Organization_Id FOREIGN KEY ([Organization_Id]) REFERENCES Organization([Id]),
            CONSTRAINT FK_OrganizationIndustry_Industry_id FOREIGN KEY ([Industry_Id]) REFERENCES Industry([Id])
            );";

        public const string User =
			@"CREATE TABLE [User] (
            [Id] NVARCHAR(36),
            [Username] NVARCHAR(255),
            [Email] NVARCHAR(255),            
            [PasswordHash] NVARCHAR(255),
            [FirstName] NVARCHAR(255),
            [LastName] NVARCHAR(255),
            [IsAdmin] BIT NOT NULL DEFAULT 0,
            [Salt] NVARCHAR(255),
            CONSTRAINT PK_User_Id PRIMARY KEY ([Id]),
            CONSTRAINT UQ_User_Username UNIQUE ([Username]),
            CONSTRAINT UQ_User_Email UNIQUE ([Email])
            )";

	}
}
