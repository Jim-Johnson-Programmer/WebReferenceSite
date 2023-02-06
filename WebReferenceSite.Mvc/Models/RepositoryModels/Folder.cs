namespace WebReferenceSite.Mvc.Models.RepositoryModels
{

    public class Folder : DataModelBase
    {
        public int FolderId { get; set; }
        public string FolderName { get; set; } = string.Empty;
        public int ParentFolderId { get; set; }
        public string ParentFolderName { get; set; } = string.Empty;
    }
}
