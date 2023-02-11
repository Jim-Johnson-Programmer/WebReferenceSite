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

        public FolderContentsController(ILogger<FolderContentsController> logger, IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");            
            _logger = logger;
            _loggerFactory = loggerFactory;

            if (string.IsNullOrEmpty(_connectionString)) throw new ArgumentNullException("Connection string not found");

            _folderService = new FolderService(_loggerFactory, _connectionString);
        }

        public IActionResult GetFolderContents(string folderId, string sortColumnId)
        {
            //FolderContentsViewModel folderContentsViewModel = LoadFolderContentsViewModel();
            FolderContentsViewModel folderContentsViewModel = new FolderContentsViewModel();

            folderId = folderId != null ? folderId : "1";
            sortColumnId = sortColumnId != null ? sortColumnId : "1";

            folderContentsViewModel = _folderService.GetFolderViewModelItem(folderId, sortColumnId, true);
            //folderContentsViewModel.CurrentFolderId = parentFolderId !=null ? parentFolderId : "1";
            //folderContentsViewModel.SortColumnId = sortColumnId != null ? sortColumnId: "1";
                        
            //folderContentsViewModel.GridRows = _folderService.GetFolderGridRows(parentFolderId, sortColumnId, true);

            return View(folderContentsViewModel);
        }

        //private FolderContentsViewModel LoadFolderContentsViewModel()
        //{
        //    FolderContentsViewModel viewModel = new FolderContentsViewModel();
        //    viewModel.CurrentFolderId = Guid.NewGuid().ToString();
        //    viewModel.SelectedFolderPath = "Root\n/Folder1\n/Folder2";

        //    viewModel.GridRows = new List<FolderGridItemsViewModel>() 
        //    {
        //        new FolderGridItemsViewModel() 
        //        {
        //             CurrentId = Guid.NewGuid().ToString(),
        //             IsFolder = true,
        //             FolderName = "FolderXXX",
        //             Url = "",
        //             CountOfFoldersContained=10,
        //             CountOfFilesContained=5,
        //             CreatedTimeStamp = DateTime.Now.AddDays(-1),
        //             UpdatedTimeStamp = DateTime.Now,
        //        },                
        //        new FolderGridItemsViewModel()
        //        {
        //             CurrentId = Guid.NewGuid().ToString(),
        //             IsFolder = true,
        //             FolderName = "FolderYY",
        //             Url = "",
        //             CountOfFoldersContained=11,
        //             CountOfFilesContained=15,
        //             CreatedTimeStamp = DateTime.Now.AddDays(-1),
        //             UpdatedTimeStamp = DateTime.Now,
        //        },
        //        new FolderGridItemsViewModel()
        //        {
        //             CurrentId = Guid.NewGuid().ToString(),
        //             FileGroups = new List<FileGroup>()
        //             {
        //                new FileGroup(){Id=Guid.NewGuid().ToString(), Name="GroupAA"},
        //                new FileGroup(){Id=Guid.NewGuid().ToString(), Name="GroupBB"}
        //             },
        //             IsFolder = false,
        //             FileHasAccountInfo = true,
        //             FolderName = "FileYY",
        //             Url = "",
        //             CountOfCharactersInFile=9,
        //             CountOfRowsInFile=16,
        //             CreatedTimeStamp = DateTime.Now.AddDays(-1),
        //             UpdatedTimeStamp = DateTime.Now,
        //        },
        //        new FolderGridItemsViewModel()
        //        {
        //             CurrentId = Guid.NewGuid().ToString(),
        //             IsFolder = false,
        //             FileHasAccountInfo = false,
        //             FolderName = "FileDD",
        //             Url = "",
        //             CountOfCharactersInFile=9,
        //             CountOfRowsInFile=16,
        //             CreatedTimeStamp = DateTime.Now.AddDays(-1),
        //             UpdatedTimeStamp = DateTime.Now,
        //        }
        //    };

        //    return viewModel;
        //}

        //public IActionResult GetFancyTreeRoot()
        //{
        //    //TreeNodeViewModel node = new TreeNodeViewModel 
        //    //{ folder = true, title = "Root", lazy=true, //this node is invisible.
        //    //    children=new List<TreeNodeViewModel>() 
        //    //    {
        //    //        new TreeNodeViewModel(){ title="Child1", icon="fancytree-icon", key="da45584173814e7a97804d40e9c7f8de", href="/FileContents/GetFileContents?id=da45584173814e7a97804d40e9c7f8de"},
        //    //        new TreeNodeViewModel(){ title="Child1", icon="fancytree-icon", folder=true,
        //    //            children = new List<TreeNodeViewModel>()
        //    //            {
        //    //                new TreeNodeViewModel(){title = "Sub child", key="da45584173814e7a97804d40e9c7f8ad"}
        //    //            }
        //    //        }
        //    //    }
        //    //};

        //    List<TreeNodeViewModel> nodeList = new List<TreeNodeViewModel>()
        //   { 
        //        new TreeNodeViewModel(){title="Sub Item", lazy=true},
        //        new TreeNodeViewModel(){title="Sub Folder", lazy=true, folder=true}
        //   };

        //    return new OkObjectResult(nodeList);
        //}

        //public IActionResult GetFancyTreeNode(string key)
        //{
        //    /*Load root level items here*/
        //    List<TreeNodeViewModel> node = new List<TreeNodeViewModel>()
        //    {
        //        new TreeNodeViewModel()
        //        {
        //            title = "RootWrapper1",
        //            //icon = "fancytree-icon",
        //            lazy = true,
        //            key = "RootWrapperKey1",
        //            //children = new List<TreeNodeViewModel>()
        //        },
        //        new TreeNodeViewModel()
        //        {
        //            title = "RootWrapper2",
        //            //icon = "fancytree-icon",
        //            lazy = true,
        //            key = "RootWrapperKey2",
        //            //children = new List<TreeNodeViewModel>()
        //        }
        //    };

        //    /*Build child lazy loading for subs of root*/
        //    if (!string.IsNullOrEmpty(key))            
        //    {
        //        node[1].children = new List<TreeNodeViewModel>()
        //        {
        //            new TreeNodeViewModel()
        //            {
        //                title = "Child 1",
        //                lazy = true,
        //                key = "ChildKey",
        //                //children = new List<TreeNodeViewModel>()
        //            }
        //        };
        //    }

        //    return new OkObjectResult(_nodes);
        //}

        //Folder add, rename, delete,  move
        #region Folder Create/Edit/Delete
        public IActionResult FolderCreateRename(string parentId, string id)
        {
            FolderCreateRenameViewModel viewModel = new FolderCreateRenameViewModel();
            viewModel.CurrentParentId = parentId;

            return View(viewModel);
        }

        public IActionResult FolderCreateRename(FolderCreateRenameViewModel inputViewModel)
        {
            if(!ModelState.IsValid)
            {
                return View(inputViewModel);
            }

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
    }
}
