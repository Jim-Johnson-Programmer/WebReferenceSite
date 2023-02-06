using System.Collections.Generic;

namespace WebReferenceSite.Mvc.Models.ViewModels
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
}
