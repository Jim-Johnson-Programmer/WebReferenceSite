namespace WebReferenceSite.Mvc.Models.RepositoryModels
{
    public class File : DataModelBase
    {
        public int FileId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public int FileRowsCount { get; set; }
        public int FilePagesCount { get; set; }
        public int ParentFolderId { get; set; }
        public string ParentFolderName { get; set; } = string.Empty;
    }

    //public class FilePages:DataModelBase
    //{
    //    public int FilePageId { get; set; }
    //    public int FileId { get; set; }
    //    public int FilePageSortNumber { get; set; }
    //    public string PageText { get; set; } = string.Empty;
    //}

    //public class FileGroups:DataModelBase
    //{
    //    public int FileGroupId { get; set; }
    //    public string FileGroupName { get; set; } = string.Empty;
    //}

    //public class FilesInFileGroups:DataModelBase
    //{
    //    public int FilesInFileGroupsId { get; set; }
    //    public int FileGroupId { get; set; }
    //    public int FileId { get; set; }
    //}
}
