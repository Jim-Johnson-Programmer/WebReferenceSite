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
        public void SelectAll()
        {
            string connectionString = "Data Source=localhost;Initial Catalog=WebReferenceSiteDb;Integrated Security=True";
            FolderRepository folderRepository = new FolderRepository(connectionString);
            List<Folder> allFolders = folderRepository.GetAllRows();
        }

        [Fact]
        public void SelectRoot()
        {
            string connectionString = "Data Source=localhost;Initial Catalog=WebReferenceSiteDb;Integrated Security=True";
            FolderRepository folderRepository = new FolderRepository(connectionString);
            Folder allFolders = folderRepository.GetFolder(1);
        }

        [Fact]
        public void InsertFolder()
        {
            string connectionString = "Data Source=localhost;Initial Catalog=WebReferenceSiteDb;Integrated Security=True";
            FolderRepository folderRepository = new FolderRepository(connectionString);
            Folder folder = new Folder()
            {
                 CreatedBy = "Jim",
                 CreatedOn = DateTime.Now,
                 FolderName = "ChildFolderAA",
                 ParentFolderId = 1,
                 ParentFolderName = "ROOT",
                 UpdatedBy = "Jim",
                 UpdatedOn = DateTime.Now
            };
            var status = folderRepository.AddRow(folder);
        }
    }
}