using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using WebReferenceSite.Mvc.Models.RepositoryModels;
using WebReferenceSite.Mvc.Models.ViewModels;
using WebReferenceSite.Mvc.Repositories;
using WebReferenceSite.Mvc.Repositories.DapperWrapper;
using WebReferenceSite.Mvc.Utilities;

namespace WebReferenceSite.Mvc.Services
{

    public class FolderService : IFolderService
    {
        ILoggerFactory _loggerFactory;
        ILogger<FolderService> _logger;
        IFolderRepository _folderRepository;

        public FolderService(ILoggerFactory loggerFactory, IFolderRepository folderRepository)
        {
            _loggerFactory = loggerFactory;
            _logger = loggerFactory.CreateLogger<FolderService>();
            _folderRepository = folderRepository;
        }

        public FolderCreateRenameViewModel FolderCreate(string folderName, string parentFolderId, string parentFolderName)
        {
            FolderCreateRenameViewModel folderCreateRenameViewModel = new FolderCreateRenameViewModel();
            
            try
            {
                Folder newFolder = new Folder();
                newFolder.FolderName = folderName;
                newFolder.ParentFolderName = parentFolderName;
                newFolder.ParentFolderId = int.Parse(parentFolderId);
                newFolder.CreatedOn = DateTime.Now;
                newFolder.UpdatedOn = DateTime.Now;
                newFolder.CreatedBy = Environment.UserName;
                newFolder.UpdatedBy = Environment.UserName;

                int newFolderId = _folderRepository.CreateFolder(newFolder);

                List<Folder> foldersToRoot  = _folderRepository.GetFoldersFromIdToRoot(parentFolderId);
                string parentFolderPath = string.Join("/\n", foldersToRoot);
                Folder parentFolder = _folderRepository.GetFolderByFolderId(parentFolderId);
                folderCreateRenameViewModel.LoadViewModel(newFolder, parentFolder, parentFolderPath);
            }
            catch (System.Exception ex)
            {
                _logger.LogError("Error trying to insert into folder {folderName}", folderName, parentFolderId, ex);
            }

            return folderCreateRenameViewModel;
        }

        public int FolderNameCount(string folderName)
        {
            int folderNameCount = 0;

            try
            {
                List<Folder> folders = _folderRepository.GetFoldersByFolderNamePortion(folderName);
                folderNameCount = folders.Count();
                _logger.LogTrace("Folder exists repo call ran successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error checking if folder exists for {folderName}", folderName);
            }

            return folderNameCount;
        }
    }
}
