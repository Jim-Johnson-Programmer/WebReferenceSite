using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using WebReferenceSite.Mvc.Models.RepositoryModels;
using WebReferenceSite.Mvc.Repositories;
using Xunit;

namespace WebReferenceSite.Mvc.Tests
{
    public class FolderRepositoryTests
    {
        [Fact]
        public void SelectAll_LIVE()
        {
            //string connectionString = "Data Source=localhost;Initial Catalog=WebReferenceSiteDb;Integrated Security=True";
            ////Mock<ILoggerFactory> mockLogger = new Mock<ILoggerFactory>();
            //Mock<ILogger<FolderRepository>> mockLogger = new Mock<ILogger<FolderRepository>>();
            ////FolderRepository folderRepository = new FolderRepository(mockLogger.Object, connectionString);
            ////List<Folder> allFolders = folderRepository.GetAllRows();
            ////Assert.Equal(2, allFolders.Count);
        }

        [Fact]
        public void SelectPath_LIVE()
        {
            //string connectionString = "Data Source=localhost;Initial Catalog=WebReferenceSiteDb;Integrated Security=True";
            ////Mock<ILoggerFactory> mockLogger = new Mock<ILoggerFactory>();
            //Mock<ILogger<FolderRepository>> mockLogger = new Mock<ILogger<FolderRepository>>();
            //FolderRepository folderRepository = new FolderRepository(mockLogger.Object, connectionString);
            //List<Folder> allFolders = folderRepository.GetFoldersFromIdToRoot("7");
        }

        [Fact]
        public void GetFolderByFolderId_LIVE()
        {
            //string connectionString = "Data Source=localhost;Initial Catalog=WebReferenceSiteDb;Integrated Security=True";
            //Mock<ILogger<FolderRepository>> mockLoggerFactory = new Mock<ILogger<FolderRepository>>();
            //FolderRepository folderRepository = new FolderRepository(mockLoggerFactory.Object, connectionString);
            //Folder allFolders = folderRepository.GetFolderByFolderId("1");
        }

        [Fact]
        public void GetFoldersByFolderNamePortion_LIVE()
        {
            //string connectionString = "Data Source=localhost;Initial Catalog=WebReferenceSiteDb;Integrated Security=True";
            //Mock<ILogger<FolderRepository>> mockLoggerFactory = new Mock<ILogger<FolderRepository>>();
            //FolderRepository folderRepository = new FolderRepository(mockLoggerFactory.Object, connectionString);
            //List<Folder> folders = folderRepository.GetFoldersByFolderNamePortion("Folder1");
        }

        [Fact]
        public void InsertFolder_LIVE()
        {
            //string connectionString = "Data Source=localhost;Initial Catalog=WebReferenceSiteDb;Integrated Security=True";
            //FolderRepository folderRepository = new FolderRepository(connectionString);
            //Folder folder = new Folder()
            //{
            //     CreatedBy = "Jim",
            //     CreatedOn = DateTime.Now,
            //     FolderName = "ChildFolderAA",
            //     ParentFolderId = 1,
            //     ParentFolderName = "ROOT",
            //     UpdatedBy = "Jim",
            //     UpdatedOn = DateTime.Now
            //};
            //var status = folderRepository.AddRow(folder);
        }
    }
}