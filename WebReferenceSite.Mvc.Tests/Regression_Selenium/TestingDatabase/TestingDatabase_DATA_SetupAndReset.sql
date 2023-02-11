use WebReferenceSiteDb_Selenium;

/*First delete all extra data*/
Delete from FileGroups;

Delete from dbo.FilePages;			
Delete from dbo.Files;			
Delete from dbo.FilesInFileGroups;			
Delete from dbo.folders;			
Delete from dbo.logs;

/*User takes responsibility for identity insert...turn it off by using word ON*/
SET IDENTITY_INSERT Folders  ON
GO

INSERT INTO [dbo].[Folders]([FolderId],[FolderName],		[ParentFolderId],[ParentFolderName],[CreatedOn],[UpdatedOn],[CreatedBy],[UpdatedBy])
		VALUES				(1,			'ROOT'				,NULL	         ,NULL           ,GETDATE()  ,GETDATE()  ,'Jim'      ,'Jim')


INSERT INTO [dbo].[Folders]([FolderId],[FolderName],		[ParentFolderId],[ParentFolderName],[CreatedOn],[UpdatedOn],[CreatedBy],[UpdatedBy])
		VALUES				(2,			'Folder1'			,1				,'ROOT'            ,GETDATE()  ,GETDATE()  ,'Jim'      ,'Jim')

INSERT INTO [dbo].[Folders]([FolderId],[FolderName],		[ParentFolderId],[ParentFolderName],[CreatedOn],[UpdatedOn],[CreatedBy],[UpdatedBy])
		VALUES				(3,		  'GrandChild1Folder1'   ,2				,'Folder1'            ,GETDATE()  ,GETDATE()  ,'Jim'      ,'Jim')

INSERT INTO [dbo].[Folders]([FolderId],[FolderName],		[ParentFolderId],[ParentFolderName],[CreatedOn],[UpdatedOn],[CreatedBy],[UpdatedBy])
		VALUES				(4,		  'Folder2'				,1				,'ROOT'            ,GETDATE()  ,GETDATE()  ,'Jim'      ,'Jim')

INSERT INTO [dbo].[Folders]([FolderId],[FolderName],		[ParentFolderId],[ParentFolderName],[CreatedOn],[UpdatedOn],[CreatedBy],[UpdatedBy])
		VALUES				(5,		  'GrandChild1Folder2'	,4				,'Folder2'            ,GETDATE()  ,GETDATE()  ,'Jim'      ,'Jim')

INSERT INTO [dbo].[Folders]([FolderId],[FolderName],		[ParentFolderId],[ParentFolderName],[CreatedOn],[UpdatedOn],[CreatedBy],[UpdatedBy])
		VALUES				(6,		  'GrandChild2Folder2'	,4				,'Folder2'            ,GETDATE()  ,GETDATE()  ,'Jim'      ,'Jim')

/*User takes responsibility for identity insert...turn it off by using word ON*/
SET IDENTITY_INSERT Folders  OFF
GO

/*Show what we have done*/
Select * from dbo.Folders