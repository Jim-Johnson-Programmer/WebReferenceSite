using System.Collections.Generic;
using WebReferenceSite.Mvc.Models.RepositoryModels;

namespace WebReferenceSite.Mvc.Repositories
{
    public interface IFolderRepository
    {
        int CreateFolder(Folder folder);
        bool UpdateFolder(Folder folder);
        List<Folder> GetAllRows();
        List<Folder> GetFolderChildFolders(string parentFolderId);
        List<Folder> GetFoldersFromIdToRoot(string folderId);
        Folder GetFolderByFolderId(string folderId);
        List<Folder> GetFoldersByFolderNamePortion(string folderName);
    }
}
