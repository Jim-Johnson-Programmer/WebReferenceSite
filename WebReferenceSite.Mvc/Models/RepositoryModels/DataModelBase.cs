using System;

namespace WebReferenceSite.Mvc.Models.RepositoryModels
{
    public abstract class DataModelBase
    {
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime UpdatedOn { get; set; } = DateTime.Now;
        public string UpdatedBy { get; set; } = string.Empty;
    }
}
