using WebReferenceSite.Mvc.Models.ViewModels;

namespace WebReferenceSite.Mvc.Services
{
    public interface IFolderService
    {
        FolderCreateRenameViewModel FolderCreate(string folderName, string parentFolderId, string parentFolderName);
        int FolderNameCount(string folderName);
    }
}
