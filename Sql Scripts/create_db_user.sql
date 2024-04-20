CREATE LOGIN ItMarathon2024   
    WITH PASSWORD = 'RegularEverydayNormalM@#$er';  
GO  

CREATE USER ItMarathon2024 FOR LOGIN ItMarathon2024;  
GO

GRANT CONTROL ON schema::dbo TO ItMarathon2024;
GO

-- dotnet tool install --global dotnet-ef
-- dotnet ef database update --project ItMarathon.Data --startup-project .