-- Create the Incidents table
CREATE TABLE IF NOT EXISTS Incidents (
    IncidentId INT AUTO_INCREMENT PRIMARY KEY
);

-- Create the Users table
CREATE TABLE IF NOT EXISTS Users (
    UserId INT AUTO_INCREMENT PRIMARY KEY
);

-- Create the Log table with only the necessary ID field
CREATE TABLE IF NOT EXISTS Log (
    LogId INT AUTO_INCREMENT PRIMARY KEY
);

-- Create the IncidentsLogInserts trigger
CREATE TRIGGER IncidentsLogInserts
    AFTER INSERT ON Incidents
    FOR EACH ROW
BEGIN
    INSERT INTO Log (createdAt, Loglevel, Message)
    VALUES (CURRENT_TIMESTAMP, 1, CONCAT('Insert operation detected in Incidents with ID: ', NEW.IncidentId));
END;

-- Create the UsersLogInserts trigger
CREATE TRIGGER UsersLogInserts
    AFTER INSERT ON Users
    FOR EACH ROW
BEGIN
    INSERT INTO Log (createdAt, Loglevel, Message)
    VALUES (CURRENT_TIMESTAMP, 1, CONCAT('Insert operation detected in Users with ID: ', NEW.UserId));
END;

-- Create the UsersLogUpdates trigger
CREATE TRIGGER UsersLogUpdates
    AFTER UPDATE ON Users
    FOR EACH ROW
BEGIN
    INSERT INTO Log (createdAt, Loglevel, Message)
    VALUES (CURRENT_TIMESTAMP, 1, CONCAT('Update operation detected in Users with ID: ', NEW.UserId));
END;

-- Create the IncidentsLogUpdates trigger
CREATE TRIGGER IncidentsLogUpdates
    AFTER UPDATE ON Incidents
    FOR EACH ROW
BEGIN
    INSERT INTO Log (createdAt, Loglevel, Message)
    VALUES (CURRENT_TIMESTAMP, 1, CONCAT('Update operation detected in Incident with ID: ', NEW.IncidentId));
END;
