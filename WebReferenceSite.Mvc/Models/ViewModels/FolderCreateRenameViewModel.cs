namespace WebReferenceSite.Mvc.Models.ViewModels
{
    public class FolderCreateRenameViewModel
    {
        public string CurrentFolderId { get; set; } = string.Empty;
        public string CurrentParentId { get; set; } = string.Empty;
        public string ParentFolderName { get; set; } = string.Empty;
        public string ParentFolderPath { get; set; } = string.Empty;
        public string CurrentFolderName { get; set; } = string.Empty;   

    }
}
