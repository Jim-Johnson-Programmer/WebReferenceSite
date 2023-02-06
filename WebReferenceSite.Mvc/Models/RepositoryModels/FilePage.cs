namespace WebReferenceSite.Mvc.Models.RepositoryModels
{
    public class FilePage : DataModelBase
    {
        public int FilePageId { get; set; }
        public int FileId { get; set; }
        public int FilePageSortNumber { get; set; }
        public string PageText { get; set; } = string.Empty;
    }
}
