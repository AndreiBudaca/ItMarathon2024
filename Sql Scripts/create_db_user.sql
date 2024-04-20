CREATE LOGIN ItMarathon2024   
    WITH PASSWORD = 'RegularEverydayNormalM@#$er';  
GO  

CREATE USER ItMarathon2024 FOR LOGIN ItMarathon2024;  
GO

GRANT ALL PRIVILEGES TO ItMarathon2024;
GO