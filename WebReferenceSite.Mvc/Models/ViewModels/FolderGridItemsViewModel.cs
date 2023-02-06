using System;
using System.Collections.Generic;

namespace WebReferenceSite.Mvc.Models.ViewModels
{
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
}
