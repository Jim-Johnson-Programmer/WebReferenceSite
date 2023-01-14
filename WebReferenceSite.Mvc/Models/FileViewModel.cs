using System;

namespace WebReferenceSite.Mvc.Models
{
    public class FileContentsViewModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public bool SaveOnly { get; set; }
        public string DocumentText { get; set; } = string.Empty;
    }
}
