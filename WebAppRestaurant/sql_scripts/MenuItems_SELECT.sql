/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) MenuItems.Id
      ,MenuItems.Name as MenuItems_Name
      ,[Description]
      ,[Price]
      ,[Category_Id]
	  , Categories.Name as CategoryName
-- UPDATE MenuItems SET Category_Id = 3
  FROM [OopRestaurantDb].[dbo].[MenuItems]
	LEFT OUTER JOIN Categories ON MenuItems.Category_Id = Categories.id
  --WHERE id  in (6)
