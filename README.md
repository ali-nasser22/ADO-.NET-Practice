### Here Is A Sample Database For SQL Server

---

- In [Program.cs](Program.cs) File I am doing all operations on the Users Table

```sql
-- Create Users table
CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) UNIQUE NOT NULL,
    PhoneNumber NVARCHAR(20),
    MembershipDate DATE NOT NULL,
    IsActive BIT DEFAULT 1
);

-- Create Books table
CREATE TABLE Books (
    BookID INT PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(200) NOT NULL,
    Author NVARCHAR(100) NOT NULL,
    ISBN NVARCHAR(20) UNIQUE NOT NULL,
    PublicationYear INT,
    Genre NVARCHAR(50),
    TotalCopies INT DEFAULT 1
);

-- Create BookLoans table (references both Users and Books)
CREATE TABLE BookLoans (
    LoanID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL,
    BookID INT NOT NULL,
    LoanDate DATE NOT NULL,
    DueDate DATE NOT NULL,
    ReturnDate DATE NULL,
    FineAmount DECIMAL(10,2) DEFAULT 0.00,
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (BookID) REFERENCES Books(BookID)
);

-- Insert 10 users
INSERT INTO Users (FirstName, LastName, Email, PhoneNumber, MembershipDate, IsActive) VALUES
('John', 'Smith', 'john.smith@email.com', '555-0101', '2024-01-15', 1),
('Sarah', 'Johnson', 'sarah.j@email.com', '555-0102', '2024-02-20', 1),
('Michael', 'Brown', 'mbrown@email.com', '555-0103', '2024-03-10', 1),
('Emily', 'Davis', 'emily.davis@email.com', '555-0104', '2024-04-05', 1),
('David', 'Wilson', 'dwilson@email.com', '555-0105', '2024-05-12', 1),
('Lisa', 'Martinez', 'lisa.m@email.com', '555-0106', '2024-06-18', 1),
('James', 'Anderson', 'janderson@email.com', '555-0107', '2024-07-22', 1),
('Jennifer', 'Taylor', 'jtaylor@email.com', '555-0108', '2024-08-30', 1),
('Robert', 'Thomas', 'rthomas@email.com', '555-0109', '2024-09-14', 1),
('Maria', 'Garcia', 'mgarcia@email.com', '555-0110', '2024-10-01', 1);

-- Insert 10 books
INSERT INTO Books (Title, Author, ISBN, PublicationYear, Genre, TotalCopies) VALUES
('The Great Gatsby', 'F. Scott Fitzgerald', '978-0743273565', 1925, 'Fiction', 3),
('To Kill a Mockingbird', 'Harper Lee', '978-0061120084', 1960, 'Fiction', 5),
('1984', 'George Orwell', '978-0452284234', 1949, 'Dystopian', 2),
('Pride and Prejudice', 'Jane Austen', '978-0141439518', 1813, 'Romance', 4),
('The Catcher in the Rye', 'J.D. Salinger', '978-0316769488', 1951, 'Fiction', 3),
('Harry Potter and the Sorcerer''s Stone', 'J.K. Rowling', '978-0590353427', 1997, 'Fantasy', 6),
('The Hobbit', 'J.R.R. Tolkien', '978-0547928227', 1937, 'Fantasy', 4),
('Fahrenheit 451', 'Ray Bradbury', '978-1451673319', 1953, 'Science Fiction', 2),
('Jane Eyre', 'Charlotte Brontë', '978-0141441146', 1847, 'Romance', 3),
('The Da Vinci Code', 'Dan Brown', '978-0307474278', 2003, 'Mystery', 5);

-- Insert 10 book loans
INSERT INTO BookLoans (UserID, BookID, LoanDate, DueDate, ReturnDate, FineAmount) VALUES
(1, 1, '2025-11-01', '2025-11-15', '2025-11-14', 0.00),
(2, 3, '2025-11-05', '2025-11-19', NULL, 0.00),
(3, 6, '2025-11-08', '2025-11-22', NULL, 0.00),
(4, 2, '2025-11-10', '2025-11-24', '2025-11-20', 0.00),
(5, 7, '2025-11-12', '2025-11-26', NULL, 0.00),
(6, 4, '2025-11-15', '2025-11-29', NULL, 0.00),
(7, 8, '2025-11-18', '2025-12-02', NULL, 0.00),
(8, 5, '2025-11-20', '2025-12-04', NULL, 0.00),
(9, 10, '2025-11-22', '2025-12-06', NULL, 0.00),
(10, 9, '2025-11-24', '2025-12-08', NULL, 0.00);

-- Verify the data
SELECT * FROM Users;
SELECT * FROM Books;
SELECT * FROM BookLoans;
```