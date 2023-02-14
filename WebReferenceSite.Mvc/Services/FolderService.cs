using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using WebReferenceSite.Mvc.Models.RepositoryModels;
using WebReferenceSite.Mvc.Models.ViewModels;
using WebReferenceSite.Mvc.Repositories;
using WebReferenceSite.Mvc.Utilities;

namespace WebReferenceSite.Mvc.Services
{
    public interface IFolderService
    {
        //FolderContentsViewModel GetFolderViewModelItem(string folderId, string sortColumnId = "", bool sortAscending = true);
        FolderCreateRenameViewModel FolderCreate(string folderName, string parentFolderId, string parentFolderName);
        //FolderCreateRenameViewModel GetCreateEditFolderViewModel(string folderId, string parentFolderId);
        int FolderNameCount(string folderName);
    }

    public class FolderService : IFolderService
    {
        ILoggerFactory _loggerFactory;
        ILogger<FolderService> _logger;
        string _connectionString = string.Empty;
        IFolderRepository _folderRepository;

        public FolderService(ILoggerFactory loggerFactory, string connectionString)
        {
            _loggerFactory = loggerFactory;
            _logger = loggerFactory.CreateLogger<FolderService>();
            //var folderRepositoryLogger = loggerFactory.CreateLogger<FolderRepository>();
            //_folderRepository = new FolderRepository(folderRepositoryLogger, connectionString);
        }

        //public FolderContentsViewModel GetFolderViewModelItem(string folderId,
        //                                                      string sortColumnId="1",
        //                                                      bool sortAscending=true)
        //{
        //    FolderContentsViewModel folderContentsViewModel = new FolderContentsViewModel();
        //    //folderContentsViewModel.RootFolderId = "1";
            
        //    Folder folder = _folderRepository.GetFolderByFolderId(folderId);
        //    folderContentsViewModel.LoadFolder(folder);
        //    folderContentsViewModel.SelectedFolderPath = string.Join("/\n", GetFoldersFromIdToRoot(folderId));

        //    folderContentsViewModel.GridRows = GetFolderGridRows(folder.FolderId.ToString(), sortColumnId, sortAscending);

        //    return folderContentsViewModel;
        //}

        public FolderCreateRenameViewModel GetCreateEditFolderViewModel(int folderId, int parentFolderId)
        {
            FolderCreateRenameViewModel createRenameViewModel = new FolderCreateRenameViewModel();
            Folder currentFolder = new Folder();
                //folderId = folderId != "0" ? folderId : "1";

            //if(folderId != 0) currentFolder = _folderRepository.GetFolderByFolderId(folderId);

            //Folder parentFolder = _folderRepository.GetFolderByFolderId(parentFolderId);
            //string parentFolderPath = string.Join("/\n", GetFoldersFromIdToRoot(parentFolderId));
            
            //createRenameViewModel.LoadViewModel(currentFolder, parentFolder, parentFolderPath);
            
            return createRenameViewModel;
        }

        //create method in file service for getting count of child items for parent id
        //private List<FolderGridItemsViewModel> GetFolderGridRows(string parentFolderId,
        //                                                        string sortColumnId, 
        //                                                        bool sortAscending)
        //{
        //    List<FolderGridItemsViewModel> gridItemRows = new List<FolderGridItemsViewModel>();
        //    List<Folder> folderList = new List<Folder>();

        //    if (string.IsNullOrEmpty(parentFolderId) || parentFolderId=="0") parentFolderId = "1";

        //    folderList = _folderRepository.GetFolderChildFolders(parentFolderId);

        //    //check here if folders only or file only 

        //    foreach (Folder folderItem in folderList)
        //    {
        //        FolderGridItemsViewModel folderGridItemsViewModel = new FolderGridItemsViewModel();
        //        folderGridItemsViewModel.IsFolder = true;
        //        folderGridItemsViewModel.CurrentId = folderItem.FolderId.ToString();
        //        folderGridItemsViewModel.FolderName = folderItem.FolderName;
        //        folderGridItemsViewModel.CreatedTimeStamp = folderItem.CreatedOn;
        //        folderGridItemsViewModel.UpdatedTimeStamp = folderItem.UpdatedOn;

        //        folderGridItemsViewModel.CountOfFoldersContained = folderList.Where(t => t.ParentFolderId == folderItem.FolderId)
        //                                .ToList().Count();

        //        //get child files from file service

        //        gridItemRows.Add(folderGridItemsViewModel);
        //    }

        //    //load file items into grid here based on parentId

        //    //SortGrid(columnId, sortId == ValueConstants.Sort_Direction_Ascending, gridItemRows);

        //    return gridItemRows;
        //}

        private void SortGrid(string sortColumnId, bool sortAscending, List<FolderGridItemsViewModel> gridRows)
        {
            if (sortColumnId == GridViewConstants.FolderGrid_Sort_Column_CreatedOnDate)
            {
                gridRows = sortAscending ?
                            gridRows.OrderBy(t => t.CreatedTimeStamp).ToList() :
                            gridRows.OrderByDescending(t => t.CreatedTimeStamp).ToList();

            }
            else if (sortColumnId == GridViewConstants.FolderGrid_Sort_Column_UpdatedOnDate)
            {
                gridRows = sortAscending ?
                            gridRows.OrderBy(t => t.UpdatedTimeStamp).ToList() :
                            gridRows.OrderByDescending(t => t.UpdatedTimeStamp).ToList();
            }
            else if (sortColumnId == GridViewConstants.FolderGrid_Sort_Column_FoldersContainedCount)
            {
                gridRows = sortAscending ?
                            gridRows.OrderBy(t => t.CountOfFoldersContained).ToList() :
                            gridRows.OrderByDescending(t => t.CountOfFoldersContained).ToList();
            }
            else if (sortColumnId == GridViewConstants.FolderGrid_Sort_Column_FilesContainedCount)
            {
                gridRows = sortAscending ?
                            gridRows.OrderBy(t => t.CountOfFilesContained).ToList() :
                            gridRows.OrderByDescending(t => t.CountOfFilesContained).ToList();
            }
            else
            {                
                gridRows = sortAscending ?
                            gridRows.OrderBy(t => t.FolderName).ToList() :
                            gridRows.OrderByDescending(t => t.FolderName).ToList();
            }

            //return gridRows;
        }

        private List<string> GetFoldersFromIdToRoot(string folderId)
        {
            List<string> folderNames = new List<string>();
            
            folderNames = _folderRepository.GetFoldersFromIdToRoot(folderId).Select(t=>t.FolderName).ToList();
            
            return folderNames;
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
                newFolder.CreatedBy = "Jim";
                newFolder.UpdatedBy = "Jim";

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

        public FolderCreateRenameViewModel FolderRename(string folderId, string folderName, string parentFolderId)
        {
            FolderCreateRenameViewModel folderCreateRenameViewModel = new FolderCreateRenameViewModel();

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
