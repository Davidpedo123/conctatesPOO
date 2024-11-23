CREATE TABLE Contacts (
    Id SERIAL PRIMARY KEY,       
    Name VARCHAR(50) NOT NULL,   
    LastName VARCHAR(50),        
    Address VARCHAR(255),        
    Telephone VARCHAR(15) NOT NULL UNIQUE, 
    Email VARCHAR(100) UNIQUE,   
    Age INT,                     
    IsBestFriend BOOLEAN DEFAULT FALSE 
);
