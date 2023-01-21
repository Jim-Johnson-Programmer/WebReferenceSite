using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebReferenceSite.Mvc.Models;

namespace WebReferenceSite.Mvc.Controllers
{
    //https://qawithexperts.com/article/asp-net/return-jsonresult-in-aspnet-core-mvc/392
    //https://wwwendt.de/tech/fancytree/demo/#sample-iframe.html
    public class FolderContentsController : Controller
    {
        List<TreeNodeViewModel> _nodes = new List<TreeNodeViewModel>();
        //public IActionResult Index()
        //{
        //    return View();
        //}

        public IActionResult GetFolderContents()
        {
            return View();
        }

        public IActionResult GetAcctFolderContents()
        {
            return View();
        }

        public IActionResult GetFancyTreeRoot()
        {
            //TreeNodeViewModel node = new TreeNodeViewModel 
            //{ folder = true, title = "Root", lazy=true, //this node is invisible.
            //    children=new List<TreeNodeViewModel>() 
            //    {
            //        new TreeNodeViewModel(){ title="Child1", icon="fancytree-icon", key="da45584173814e7a97804d40e9c7f8de", href="/FileContents/GetFileContents?id=da45584173814e7a97804d40e9c7f8de"},
            //        new TreeNodeViewModel(){ title="Child1", icon="fancytree-icon", folder=true,
            //            children = new List<TreeNodeViewModel>()
            //            {
            //                new TreeNodeViewModel(){title = "Sub child", key="da45584173814e7a97804d40e9c7f8ad"}
            //            }
            //        }
            //    }
            //};

            List<TreeNodeViewModel> nodeList = new List<TreeNodeViewModel>()
           { 
                new TreeNodeViewModel(){title="Sub Item", lazy=true},
                new TreeNodeViewModel(){title="Sub Folder", lazy=true, folder=true}
           };

            return new OkObjectResult(nodeList);
        }

        public IActionResult GetFancyTreeNode(string key)
        {
            /*Load root level items here*/
            List<TreeNodeViewModel> node = new List<TreeNodeViewModel>()
            {
                new TreeNodeViewModel()
                {
                    title = "RootWrapper1",
                    //icon = "fancytree-icon",
                    lazy = true,
                    key = "RootWrapperKey1",
                    //children = new List<TreeNodeViewModel>()
                },
                new TreeNodeViewModel()
                {
                    title = "RootWrapper2",
                    //icon = "fancytree-icon",
                    lazy = true,
                    key = "RootWrapperKey2",
                    //children = new List<TreeNodeViewModel>()
                }
            };

            /*Build child lazy loading for subs of root*/
            if (!string.IsNullOrEmpty(key))            
            {
                node[1].children = new List<TreeNodeViewModel>()
                {
                    new TreeNodeViewModel()
                    {
                        title = "Child 1",
                        lazy = true,
                        key = "ChildKey",
                        //children = new List<TreeNodeViewModel>()
                    }
                };
            }

            return new OkObjectResult(_nodes);
        }

        //Folder add, rename, delete,  move
        #region Folder Mangement
        public IActionResult FolderAdd()
        {
            return View();
        }

        public IActionResult FolderRename()
        {
            return View();
        }

        public IActionResult FolderDelete()
        {
            return View();
        }

        public IActionResult FolderMove()
        {
            return View();
        }

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
        #endregion Folder Mangement

        //File Add, Rename, Delete, Move
        #region File Mangement
        public IActionResult FileAdd()
        {
            return View();
        }

        public IActionResult FileRename()
        {
            return View();
        }

        public IActionResult FileDelete()
        {
            return View();
        }

        public IActionResult FileMove()
        {
            return View();
        }

        public IActionResult FileAddVerify()
        {
            return View();
        }

        public IActionResult FileRenameVerify()
        {
            return View();
        }

        public IActionResult FileDeleteVerify()
        {
            return View();
        }

        public IActionResult FileMoveVerify()
        {
            return View();
        }
        #endregion File Mangement
    }
}
