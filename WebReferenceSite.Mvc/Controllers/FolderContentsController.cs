﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using WebReferenceSite.Mvc.Models.RepositoryModels;
using WebReferenceSite.Mvc.Models.ViewModels;
using WebReferenceSite.Mvc.Repositories;
using WebReferenceSite.Mvc.Repositories.DapperWrapper;
using WebReferenceSite.Mvc.Services;

namespace WebReferenceSite.Mvc.Controllers
{
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

            _folderRepository = new FolderRepository(_connectionString, dbExecutorFactory, _loggerFactory);
            _folderService = new FolderService(_loggerFactory, _folderRepository);
        }

        public IActionResult GetFolderContents(string folderId="1", string sortColumnId="1", string sortAscending="true")
        {
            FolderContentsViewModel folderContentsViewModel = new FolderContentsViewModel();
            Folder folder = _folderRepository.GetFolderByFolderId(folderId);
            
            folderContentsViewModel.LoadFolder(folder);
            List<string> folderNamesToRoot = _folderRepository.GetFoldersFromIdToRoot(folderId).Select(t => t.FolderName).ToList();
            folderContentsViewModel.SelectedFolderPath = string.Join("/\n", folderNamesToRoot);
            folderContentsViewModel.GridRows = GetFolderGridRows(folder.FolderId.ToString(), sortColumnId, sortAscending=="true");

            return View(folderContentsViewModel);
        }
       

        #region Folder Support Functions

        private List<FolderGridItemsViewModel> GetFolderGridRows(string parentFolderId,
                                                               string sortColumnId,
                                                               bool sortAscending)
        {
            List<FolderGridItemsViewModel> gridItemRows = new List<FolderGridItemsViewModel>();
            List<Folder> folderList = new List<Folder>();

            if (string.IsNullOrEmpty(parentFolderId) || parentFolderId == "0") parentFolderId = "1";

            folderList = _folderRepository.GetFolderChildFolders(parentFolderId);

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

            return gridItemRows;
        }

        #endregion Folder Support Functions
    }
}
