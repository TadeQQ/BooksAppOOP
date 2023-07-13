-- Sprawdzenie, czy baza danych już istnieje
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'Books')
BEGIN
    -- Tworzenie bazy danych
    CREATE DATABASE Books;
END
GO

-- Używanie utworzonej bazy danych
USE Books;
GO

-- Sprawdzenie, czy tabela "Autorzy" już istnieje
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Autorzy')
BEGIN
    -- Tworzenie tabeli "Autorzy"
    CREATE TABLE Autorzy (
        AutorId INT PRIMARY KEY,
        Imię NVARCHAR(50),
        Nazwisko NVARCHAR(50)
    );
END
GO

-- Sprawdzenie, czy tabela "Wydawnictwa" już istnieje
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Wydawnictwa')
BEGIN
    -- Tworzenie tabeli "Wydawnictwa"
    CREATE TABLE Wydawnictwa (
        WydawnictwoId INT PRIMARY KEY,
        Nazwa NVARCHAR(100),
        Adres NVARCHAR(100)
    );
END
GO

-- Sprawdzenie, czy tabela "Kategorie" już istnieje
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Kategorie')
BEGIN
    -- Tworzenie tabeli "Kategorie"
    CREATE TABLE Kategorie (
        KategoriaId INT PRIMARY KEY,
        Nazwa NVARCHAR(50)
    );
END
GO

-- Sprawdzenie, czy tabela "Książki" już istnieje
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Książki')
BEGIN
    -- Tworzenie tabeli "Książki" z kluczami obcymi
    CREATE TABLE Książki (
        KsiążkaId INT PRIMARY KEY,
        Tytuł NVARCHAR(100),
        AutorId INT,
        WydawnictwoId INT,
        KategoriaId INT,
        FOREIGN KEY (AutorId) REFERENCES Autorzy(AutorId),
        FOREIGN KEY (WydawnictwoId) REFERENCES Wydawnictwa(WydawnictwoId),
        FOREIGN KEY (KategoriaId) REFERENCES Kategorie(KategoriaId)
    );
END
GO

-- Dodawanie danych do tabeli "Autorzy" (jeśli tabela istnieje, ale jest pusta)
INSERT INTO Autorzy (AutorId, Imię, Nazwisko)
SELECT 1, 'Stephen', 'King'
WHERE NOT EXISTS (SELECT * FROM Autorzy);

INSERT INTO Autorzy (AutorId, Imię, Nazwisko)
SELECT 2, 'J.K.', 'Rowling'
WHERE NOT EXISTS (SELECT * FROM Autorzy WHERE AutorId = 2);
GO

-- Dodawanie danych do tabeli "Wydawnictwa" (jeśli tabela istnieje, ale jest pusta)
INSERT INTO Wydawnictwa (WydawnictwoId, Nazwa, Adres)
SELECT 1, 'Penguin Random House', '123 Main Street'
WHERE NOT EXISTS (SELECT * FROM Wydawnictwa);

INSERT INTO Wydawnictwa (WydawnictwoId, Nazwa, Adres)
SELECT 2, 'Bloomsbury', '456 Elm Street'
WHERE NOT EXISTS (SELECT * FROM Wydawnictwa WHERE WydawnictwoId = 2);
GO

-- Dodawanie danych do tabeli "Kategorie" (jeśli tabela istnieje, ale jest pusta)
INSERT INTO Kategorie (KategoriaId, Nazwa)
SELECT 1, 'Horror'
WHERE NOT EXISTS (SELECT * FROM Kategorie);

INSERT INTO Kategorie (KategoriaId, Nazwa)
SELECT 2, 'Fantasy'
WHERE NOT EXISTS (SELECT * FROM Kategorie WHERE KategoriaId = 2);
GO

-- Dodawanie danych do tabeli "Książki" (jeśli tabela istnieje, ale jest pusta)
INSERT INTO Książki (KsiążkaId, Tytuł, AutorId, WydawnictwoId, KategoriaId)
SELECT 1, 'It', 1, 1, 1
WHERE NOT EXISTS (SELECT * FROM Książki);

INSERT INTO Książki (KsiążkaId, Tytuł, AutorId, WydawnictwoId, KategoriaId)
SELECT 2, 'Harry Potter and the Philosopher''s Stone', 2, 2, 2
WHERE NOT EXISTS (SELECT * FROM Książki WHERE KsiążkaId = 2);
GO
