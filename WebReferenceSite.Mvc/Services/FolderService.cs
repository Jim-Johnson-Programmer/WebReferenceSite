using Microsoft.Extensions.Logging;
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
        FolderContentsViewModel GetFolderViewModelItem(string folderId,
                                                       string sortColumnId,
                                                       bool stortAscending);
        //List<FolderGridItemsViewModel> GetFolderGridRows(string parentFolderId,
        //                                                 string sortColumnId,
        //                                                 bool sortAscending);
        //FolderGridItemsViewModel GetFolderGridItem();
    }

    public class FolderService : IFolderService
    {
        //ILoggerFactory _loggerFactory;
        string _connectionString = string.Empty;
        IFolderRepository _folderRepository;

        public FolderService(ILoggerFactory loggerFactory, string connectionString)
        {
            //_loggerFactory = loggerFactory;
            _folderRepository = new FolderRepository(loggerFactory, connectionString);
        }

        public FolderContentsViewModel GetFolderViewModelItem(string folderId,
                                                              string sortColumnId,
                                                              bool sortAscending)
        {
            FolderContentsViewModel folderContentsViewModel = new FolderContentsViewModel();
            folderContentsViewModel.RootFolderId = "1";
            
            Folder folder = _folderRepository.GetFolder(folderId);
            folderContentsViewModel.LoadFolder(folder);
            folderContentsViewModel.SelectedFolderPath = string.Join("/\n", GetFoldersFromIdToRoot(folderId));
;
            folderContentsViewModel.GridRows = GetFolderGridRows(folder.FolderId.ToString(), sortColumnId, sortAscending);

            return folderContentsViewModel;
        }

        //create method in file service for getting count of child items for parent id
        private List<FolderGridItemsViewModel> GetFolderGridRows(string parentFolderId,
                                                                string sortColumnId, 
                                                                bool sortAscending)
        {
            List<FolderGridItemsViewModel> gridItemRows = new List<FolderGridItemsViewModel>();
            List<Folder> folderList = new List<Folder>();

            if (string.IsNullOrEmpty(parentFolderId) || parentFolderId=="0") parentFolderId = "1";

            folderList = _folderRepository.GetFolderChildFolders(parentFolderId);

            //check here if folders only or file only 

            foreach (Folder folderItem in folderList)
            {
                FolderGridItemsViewModel folderGridItemsViewModel = new FolderGridItemsViewModel();
                folderGridItemsViewModel.IsFolder = true;
                folderGridItemsViewModel.CurrentId = folderItem.FolderId.ToString();
                folderGridItemsViewModel.FolderName = folderItem.FolderName;
                folderGridItemsViewModel.CreatedTimeStamp = folderItem.CreatedOn;
                folderGridItemsViewModel.UpdatedTimeStamp = folderItem.UpdatedOn;

                folderGridItemsViewModel.CountOfFoldersContained = folderList.Where(t => t.ParentFolderId == folderItem.FolderId)
                                        .ToList().Count();

                //get child files from file service

                gridItemRows.Add(folderGridItemsViewModel);
            }

            //load file items into grid here based on parentId

            //SortGrid(columnId, sortId == ValueConstants.Sort_Direction_Ascending, gridItemRows);

            return gridItemRows;
        }

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

        private FolderGridItemsViewModel GetFolderGridItem()
        {
            FolderGridItemsViewModel folderGridItem = new FolderGridItemsViewModel();

            return folderGridItem;
        }

        public List<string> GetFoldersFromIdToRoot(string folderId)
        {
            List<string> folderNames = new List<string>();
            
            folderNames = _folderRepository.GetFolderTreeFromIdToRoot(folderId).Select(t=>t.FolderName).ToList();
            
            return folderNames;
        }

    }
}
