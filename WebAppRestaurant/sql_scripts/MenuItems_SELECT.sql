/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [Id]
      ,[Name]
      ,[Description]
      ,[Price]
      ,[Category_Id]
-- UPDATE MenuItems SET Category_Id = 3
  FROM [OopRestaurantDb].[dbo].[MenuItems]
  --WHERE id  in (6)
