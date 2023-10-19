CREATE TRIGGER LogInsertsAndUpdates
    ON Log
    AFTER INSERT, UPDATE
                      AS
BEGIN
    DECLARE @id int;
    SELECT @id = i.CustomerID FROM inserted i;
    INSERT INTO Log (createdAt, Loglevel, Message) VALUES (current_timestamp, 1, @id);
END