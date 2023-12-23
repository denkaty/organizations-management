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
			CONSTRAINT PK_Country_Id PRIMARY KEY ([Id])
			);";

        public const string Industry =
            @"CREATE TABLE [Industry] (
			[Id] NVARCHAR(36),
			[Name] NVARCHAR(255),
			CONSTRAINT PK_Industry_Id PRIMARY KEY ([Id])
			);";

        public const string Organization =
            @"CREATE TABLE [Organization] (
            [Id] NVARCHAR(36),
            [Index] INT,
            [Name] NVARCHAR(255),
            [Website] NVARCHAR(255),
            [Description] NVARCHAR(255),
            [Founded_year] INT,
            [Number_of_employees] INT,
            [Country_Id] NVARCHAR(36),
            CONSTRAINT PK_Organization_Id PRIMARY KEY ([Id]),
            CONSTRAINT UQ_Organization_Index UNIQUE ([Index]),
            CONSTRAINT FK_Organization_Country_Id_Country_Id FOREIGN KEY ([Country_Id]) REFERENCES Country([Id])
            );";

        public const string OrganizationIndustry =
            @"CREATE TABLE [OrganizationIndustry] (
            [Organization_Id] NVARCHAR(36),
            [Industry_Id] NVARCHAR(36),
            CONSTRAINT PK_OrganizationIndustry PRIMARY KEY ([Organization_Id], [Industry_Id]),
            CONSTRAINT FK_OrganizationIndustry_Organization_Id FOREIGN KEY ([Organization_Id]) REFERENCES Organization([Id]),
            CONSTRAINT FK_OrganizationIndustry_Industry_id FOREIGN KEY ([Industry_Id]) REFERENCES Industry([Id])
            );";
    }
}
