using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebReferenceSite.Mvc.Models;

namespace WebReferenceSite.Mvc.Controllers
{
    //https://qawithexperts.com/article/asp-net/return-jsonresult-in-aspnet-core-mvc/392
    //https://wwwendt.de/tech/fancytree/demo/#sample-iframe.html
    public class FolderContentsController : Controller
    {
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
            TreeNodeViewModel node = new TreeNodeViewModel 
            { folder = true, title = "Root", tooltip = "I am root", //this node is invisible.
                children=new List<TreeNodeViewModel>() 
                {
                    new TreeNodeViewModel(){ title="Child1", icon="fancytree-icon", key="da45584173814e7a97804d40e9c7f8de", href="/FileContents/GetFileContents?id=da45584173814e7a97804d40e9c7f8de"},
                    new TreeNodeViewModel(){ title="Child1", icon="fancytree-icon", folder=true,
                        children = new List<TreeNodeViewModel>()
                        {
                            new TreeNodeViewModel(){title = "Sub child", key="da45584173814e7a97804d40e9c7f8ad"}
                        }
                    }
                }
            };

            return new OkObjectResult(node);
        }

        public IActionResult GetFancyTreeNode(string key)
        {
            TreeNodeViewModel node = new TreeNodeViewModel();

            if (string.IsNullOrEmpty(key))
            {
                node = new TreeNodeViewModel
                {
                    folder = true,
                    title = "RootWrapper",
                    tooltip = "Root Wrapper", //this node is invisible.
                    children = new List<TreeNodeViewModel>()
                    {
                        /*Top visible node*/
                        new TreeNodeViewModel()
                        { 
                            title="Root", 
                            tooltip = "Top Node",
                            icon="fancytree-icon", 
                            key="rootKey", 
                            folder=true,
                            children = new List<TreeNodeViewModel>()
                            {
                                /*First children in the root node*/
                                new TreeNodeViewModel()
                                {
                                    title="Child 1",
                                    tooltip = "Sub node",
                                    icon="fancytree-icon",
                                    key="da45584173814e7a97804d40e9c7f8de", 
                                    //href="/FileContents/GetFileContents?id=da45584173814e7a97804d40e9c7f8de",
                                    folder=true,
                                    children = new List<TreeNodeViewModel>()
                                    {
                                        new TreeNodeViewModel(){ title="Sub Child 1",
                                            key="aaaaa"}
                                    }
                                }
                            }
                        }
                    }
                }; 
            }
            else
            {
                node = new TreeNodeViewModel
                {
                    folder = true,
                    title = "RootWrapper",
                    tooltip = "Root Wrapper", //this node is invisible.
                    children = new List<TreeNodeViewModel>()
                    {
                        /*Top visible node -- should have no href*/
                        new TreeNodeViewModel()
                        {
                            title="Root",
                            tooltip = "Top Node",
                            icon="fancytree-icon",
                            key="rootKey",
                            folder=true,
                            children = new List<TreeNodeViewModel>()
                            {
                                /*First children in the root node*/
                                new TreeNodeViewModel()
                                {
                                    title="Root",
                                    tooltip = "Top Node",
                                    icon="fancytree-icon",
                                    key="da45584173814e7a97804d40e9c7f8de", 
                                    href="/FileContents/GetFancyTreeNode?key=da45584173814e7a97804d40e9c7f8de",
                                    folder=true,
                                    children = new List<TreeNodeViewModel>()
                                    {
                                        new TreeNodeViewModel(){
                                            title="asdf", key="aaaaa"}
                                    }
                                }
                            }
                        }
                    }
                };
            }

            return new OkObjectResult(node);
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
