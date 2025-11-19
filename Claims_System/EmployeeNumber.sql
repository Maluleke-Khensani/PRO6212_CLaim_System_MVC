-- Drop the table if it exists
DROP TABLE IF EXISTS LecturerProfile;

-- Recreate table with EmployeeNumber as IDENTITY
CREATE TABLE LecturerProfile (
    EmployeeNumber INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(200) NOT NULL,
    Email NVARCHAR(256) NOT NULL,
    HourlyRate DECIMAL(18,2) NOT NULL
);
