using System;
using System.Collections.Generic;

namespace WebReferenceSite.Mvc.Models
{
    public class FolderContentsViewModel
    {
        public string CurrentFolderId { get; set; } = string.Empty;
        public string CurrentFolderName { get; set; } = string.Empty;
        public string SelectedFolderPath { get; set; } = string.Empty;
        public string ParentFolderId { get; set; } = string.Empty;
        public string RootFolderId { get; set; } = string.Empty;
        public bool IsTagView { get; set; }
        public bool IsFolderOnlyView { get; set; }
        public bool IsFileOnlyView { get; set; }
        public string TagNameField { get; set; } = string.Empty;
        public List<FolderGridItemsViewModel> GridRows { get; set; } = new List<FolderGridItemsViewModel>();
    }

    public class FolderGridItemsViewModel
    {
        public List<FileGroup> FileGroups { get; set; } = new List<FileGroup>();
        public string CurrentId { get; set; } = string.Empty;
        public bool IsFolder { get; set; }
        public bool FileHasAccountInfo { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public DateTime UpdatedTimeStamp { get; set; } = DateTime.Now;
        public DateTime CreatedTimeStamp { get; set; } = DateTime.Now;
        public int CountOfFoldersContained { get; set; }
        public int CountOfFilesContained { get; set; }
        public int CountOfCharactersInFile { get; set; }
        public int CountOfRowsInFile { get; set; }
    }

    public class FileGroup
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
}
