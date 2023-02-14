using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebReferenceSite.Mvc.Models.ViewModels;
using Serilog;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using WebReferenceSite.Mvc.Repositories;
using Serilog.Extensions.Logging;
using WebReferenceSite.Mvc.Models.RepositoryModels;
using WebReferenceSite.Mvc.Services;
using WebReferenceSite.Mvc.Repositories.DapperWrapper;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace WebReferenceSite.Mvc.Controllers
{
    //https://qawithexperts.com/article/asp-net/return-jsonresult-in-aspnet-core-mvc/392
    //https://wwwendt.de/tech/fancytree/demo/#sample-iframe.html
    public class FolderContentsController : Controller
    {
        private readonly ILogger<FolderContentsController> _logger;
        private readonly IConfiguration _configuration;
        private readonly string _connectionString = string.Empty;

        private IFolderService _folderService;
        private ILoggerFactory _loggerFactory;
        private IFolderRepository _folderRepository;
        private IDbExecutorFactory _dbExecutorFactory;

        public FolderContentsController(ILogger<FolderContentsController> logger, 
                                        IConfiguration configuration, 
                                        ILoggerFactory loggerFactory,
                                        IDbExecutorFactory dbExecutorFactory)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(_connectionString)) throw new ArgumentNullException("Connection string not found");

            _configuration = configuration;
            _logger = logger;
            _loggerFactory = loggerFactory;
            _dbExecutorFactory = dbExecutorFactory;



            _folderService = new FolderService(_loggerFactory, _connectionString);

            ILogger<FolderRepository> folderRepoLogger = _loggerFactory.CreateLogger<FolderRepository>();
            _folderRepository = new FolderRepository(dbExecutorFactory, folderRepoLogger);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="folderId"></param>
        /// <param name="sortColumnId"></param>
        /// <returns></returns>
        public IActionResult GetFolderContents(string folderId="1", string sortColumnId="1", string sortAscending="true")
        {
            FolderContentsViewModel folderContentsViewModel = new FolderContentsViewModel();

            //folderId = folderId != "0" ? folderId : "1";
            //sortColumnId = sortColumnId != null ? sortColumnId : "1";
            //folderContentsViewModel = _folderService.GetFolderViewModelItem(folderId, sortColumnId, true);

            Folder folder = _folderRepository.GetFolderByFolderId(folderId);
            folderContentsViewModel.LoadFolder(folder);
            List<string> folderNamesToRoot = _folderRepository.GetFoldersFromIdToRoot(folderId).Select(t => t.FolderName).ToList();
            folderContentsViewModel.SelectedFolderPath = string.Join("/\n", folderNamesToRoot);

            folderContentsViewModel.GridRows = GetFolderGridRows(folder.FolderId.ToString(), sortColumnId, sortAscending=="true");

            return View(folderContentsViewModel);
        }
       
        //Folder add, rename, delete,  move
        #region Folder Create/Edit/Delete
        public IActionResult FolderCreateRename(string folderId, string parentId)
        {
            //folderId = folderId != "0" ? folderId : "1";//create or update

            FolderCreateRenameViewModel createRenameViewModel = new FolderCreateRenameViewModel();
            //detect root and disable screen items as needed for root protection

            //TODO: createRenameViewModel = _folderService.GetCreateEditFolderViewModel(folderId, parentId);

            return View(createRenameViewModel);
        }

        [HttpPost]
        public IActionResult FolderCreateRename(FolderCreateRenameViewModel inputViewModel)
        {
            //prevent duplication if name is new, else is update to existing item.
            if(inputViewModel.CurrentFolderId=="0")
            {
                ModelState.AddModelError("CurrentParentId", "Cannot rename ROOT directory");
            }

            //prevent rename of root

            if (!ModelState.IsValid)
            {
                return View(inputViewModel);
            }

            //separate create from update directory


            string routeValues = string.Format("");
            return RedirectToAction("FileContents", "FileContents", routeValues);
        }

        public IActionResult FolderDelete(string id)
        {
            return View();
        }

        public IActionResult FolderMove(string parentId, string id)
        {
            return View();
        }

        public IActionResult FolderSelection(string id)
        {
            return View();
        }
        #endregion Folder Create/Edit/Delete

        #region Folder Action Validations
        public IActionResult FolderAddVerify()
        {
            return View();
        }

        public IActionResult FolderRenameVerify()
        {
            return View();
        }

        public IActionResult FolderDeleteVerify()
        {
            return View();
        }

        public IActionResult FolderMoveVerify()
        {
            return View();
        }
        #endregion Folder Action Validations


        #region Folder Support Functions

        private List<FolderGridItemsViewModel> GetFolderGridRows(string parentFolderId,
                                                               string sortColumnId,
                                                               bool sortAscending)
        {
            List<FolderGridItemsViewModel> gridItemRows = new List<FolderGridItemsViewModel>();
            List<Folder> folderList = new List<Folder>();

            if (string.IsNullOrEmpty(parentFolderId) || parentFolderId == "0") parentFolderId = "1";

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
                folderGridItemsViewModel.CountOfFoldersContained = _folderRepository.GetFolderChildFolders(folderItem.FolderId.ToString()).Count();

                gridItemRows.Add(folderGridItemsViewModel);
            }

            //SortGrid(columnId, sortId == ValueConstants.Sort_Direction_Ascending, gridItemRows);

            return gridItemRows;
        }

        #endregion Folder Support Functions
    }
}
