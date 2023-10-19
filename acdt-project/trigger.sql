CREATE TRIGGER IncidentsLogInserts
    AFTER INSERT ON Incidents
    FOR EACH ROW
BEGIN
    INSERT INTO Log (createdAt, Loglevel, Message)
    VALUES (CURRENT_TIMESTAMP, 1, CONCAT('Insert operation detected in Incidents with ID: ', NEW.IncidentId));
END;

CREATE TRIGGER UsersLogInserts
    AFTER INSERT ON Users
    FOR EACH ROW
BEGIN
    INSERT INTO Log (createdAt, Loglevel, Message)
    VALUES (CURRENT_TIMESTAMP, 1, CONCAT('Insert operation detected in Users with ID: ', NEW.UserId));
END;

CREATE TRIGGER UsersLogUpdates
    AFTER UPDATE ON Users
    FOR EACH ROW
BEGIN
    INSERT INTO Log (createdAt, Loglevel, Message)
    VALUES (CURRENT_TIMESTAMP, 1, CONCAT('Update operation detected in Users with ID: ', NEW.UserId));
END;

CREATE TRIGGER IncidentsLogUpdates
    AFTER UPDATE ON Incidents
    FOR EACH ROW
BEGIN
    INSERT INTO Log (createdAt, Loglevel, Message)
    VALUES (CURRENT_TIMESTAMP, 1, CONCAT('Update operation detected in Incident with ID: ', NEW.IncidentId));
END;
