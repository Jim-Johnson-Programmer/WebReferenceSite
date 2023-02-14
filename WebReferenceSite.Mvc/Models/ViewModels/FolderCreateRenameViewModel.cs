using WebReferenceSite.Mvc.Models.RepositoryModels;

namespace WebReferenceSite.Mvc.Models.ViewModels
{
    public class FolderCreateRenameViewModel
    {
        public string CurrentFolderId { get; private set; } = string.Empty;
        public string CurrentFolderName { get; private set; } = string.Empty;
        public string CurrentParentId { get; private set; } = string.Empty;
        public string ParentFolderName { get; private set; } = string.Empty;
        public string ParentFolderPath { get; private set; } = string.Empty;
        public bool IsRootFolder { get; private set; }

        public void LoadViewModel(Folder folder, Folder parentFolder, string parentFolderPath)
        {
            CurrentFolderId =   folder.FolderId.ToString();
            CurrentFolderName = folder.FolderName.ToString();

            CurrentParentId =   parentFolder.FolderId.ToString();
            ParentFolderName = parentFolder.FolderName.ToString();
            IsRootFolder = folder.FolderId == 1 && parentFolder.FolderId==1;

            ParentFolderPath = parentFolderPath;
        }
    }
}
