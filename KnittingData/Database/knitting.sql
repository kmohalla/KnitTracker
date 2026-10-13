-- Knitting database: DDL + DML (SQL Server / LocalDB)
-- 5 tables, 33 records

IF DB_ID('KnittingDb') IS NULL CREATE DATABASE KnittingDb;
GO
USE KnittingDb;
GO

DROP TABLE IF EXISTS Projects, Patterns, Yarns, Needles, Brands;
GO

------------------------------------------------------------
-- DDL
------------------------------------------------------------
CREATE TABLE Brands (                -- admin-only table
    Id      INT IDENTITY PRIMARY KEY,
    Name    NVARCHAR(100) NOT NULL,
    Country NVARCHAR(50)  NOT NULL
);

CREATE TABLE Yarns (                 -- searchable table
    Id        INT IDENTITY PRIMARY KEY,
    Name      NVARCHAR(100) NOT NULL,
    Fiber     NVARCHAR(50)  NOT NULL,
    Weight    NVARCHAR(20)  NOT NULL,
    ColorName NVARCHAR(50)  NOT NULL,
    BrandId   INT NULL REFERENCES Brands(Id) ON DELETE SET NULL
);

CREATE TABLE Needles (
    Id       INT IDENTITY PRIMARY KEY,
    SizeMm   DECIMAL(4,2) NOT NULL,
    Type     NVARCHAR(20) NOT NULL,  -- Straight, Circular, DPN
    Material NVARCHAR(30) NOT NULL,
    LengthCm INT          NOT NULL
);

CREATE TABLE Patterns (
    Id         INT IDENTITY PRIMARY KEY,
    Name       NVARCHAR(100) NOT NULL,
    Designer   NVARCHAR(100) NOT NULL,
    Category   NVARCHAR(30)  NOT NULL,
    Difficulty NVARCHAR(20)  NOT NULL
);

CREATE TABLE Projects (
    Id        INT IDENTITY PRIMARY KEY,
    Name      NVARCHAR(100) NOT NULL,
    Status    NVARCHAR(20)  NOT NULL, -- Planned, In Progress, Finished
    StartDate DATE          NOT NULL,
    PatternId INT NULL REFERENCES Patterns(Id) ON DELETE SET NULL,
    YarnId    INT NULL REFERENCES Yarns(Id)    ON DELETE SET NULL,
    NeedleId  INT NULL REFERENCES Needles(Id)  ON DELETE SET NULL
);
GO

------------------------------------------------------------
-- DML
------------------------------------------------------------
INSERT INTO Brands (Name, Country) VALUES
('Woolly Hollow',        'USA'),
('Northern Spindle',     'Canada'),
('Copper Kettle Fibers', 'USA'),
('Fjord Wool',           'Norway'),
('Highland Croft',       'Scotland');

INSERT INTO Yarns (Name, Fiber, Weight, ColorName, BrandId) VALUES
('Meadow Worsted',  'Wool',        'Worsted',   'Sage Green',     1),
('Meadow Worsted',  'Wool',        'Worsted',   'Charcoal',       1),
('Cloud Fingering', 'Merino',      'Fingering', 'Sky Blue',       2),
('Cozy Chunky',     'Acrylic',     'Bulky',     'Cream',          3),
('Fjell DK',        'Wool',        'DK',        'Rust',           4),
('Silk Mist Lace',  'Silk/Mohair', 'Lace',      'Blush Pink',     2),
('Tweed Aran',      'Wool',        'Aran',      'Heather Purple', 5),
('Cotton Breeze',   'Cotton',      'DK',        'Lemon',          3);

INSERT INTO Needles (SizeMm, Type, Material, LengthCm) VALUES
(3.25, 'Circular', 'Metal',   80),
(3.75, 'DPN',      'Bamboo',  20),
(4.00, 'Straight', 'Bamboo',  35),
(5.00, 'Circular', 'Wood',    80),
(4.50, 'Circular', 'Metal',   40),
(5.50, 'Straight', 'Wood',    35),
(8.00, 'Straight', 'Plastic', 35),
(2.50, 'DPN',      'Metal',   15);

INSERT INTO Patterns (Name, Designer, Category, Difficulty) VALUES
('Easy Garter Scarf',    'Ana Ruiz',   'Scarf',   'Beginner'),
('Ribbed Watch Cap',     'Tom Becker', 'Hat',     'Beginner'),
('Lacy Leaf Shawl',      'Mira Holt',  'Shawl',   'Advanced'),
('Basic Top-Down Socks', 'Jen Okafor', 'Socks',   'Intermediate'),
('Cable Knit Pullover',  'Sean Doyle', 'Sweater', 'Advanced'),
('Striped Baby Blanket', 'Ana Ruiz',   'Blanket', 'Beginner');

INSERT INTO Projects (Name, Status, StartDate, PatternId, YarnId, NeedleId) VALUES
('Winter Scarf for Dad',  'Finished',    '2025-11-02', 1, 2, 6),
('Green Beanie',          'Finished',    '2026-01-15', 2, 1, 5),
('Wedding Shawl',         'In Progress', '2026-05-20', 3, 6, 1),
('First Socks',           'In Progress', '2026-08-03', 4, 3, 8),
('Aran Sweater',          'Planned',     '2026-11-01', 5, 7, 4),
('Baby Blanket for Lily', 'Planned',     '2026-12-01', 6, 8, 3);
GO
