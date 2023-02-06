namespace WebReferenceSite.Mvc.Models.RepositoryModels
{
    public class FileInFileGroup : DataModelBase
    {
        public int FilesInFileGroupsId { get; set; }
        public int FileGroupId { get; set; }
        public int FileId { get; set; }
    }
}
