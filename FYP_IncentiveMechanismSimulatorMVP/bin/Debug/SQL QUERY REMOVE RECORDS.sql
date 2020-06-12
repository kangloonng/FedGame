
DELETE FROM FederationHistory
DELETE FROM FederationParticipantsHistory
DELETE FROM ParticipantHistory
DELETE FROM InTrainings
DELETE FROM Bids
DELETE FROM Federations
DELETE FROM Participants
DELETE FROM GameInstance
DBCC CHECKIDENT ('Federations', RESEED, 0)  
DBCC CHECKIDENT ('Participants', RESEED, 0)  
DBCC CHECKIDENT ('GameInstance', RESEED, 0)  
DBCC CHECKIDENT ('FederationHistory', RESEED, 0)  
DBCC CHECKIDENT ('FederationParticipantsHistory', RESEED, 0)  
DBCC CHECKIDENT ('ParticipantHistory', RESEED, 0)  
DBCC CHECKIDENT ('InTrainings', RESEED, 0)  
DBCC CHECKIDENT ('Bids', RESEED, 0)  

SELECT * FROM GameInstance
SELECT * FROM Federations
SELECT * FROM Participants

