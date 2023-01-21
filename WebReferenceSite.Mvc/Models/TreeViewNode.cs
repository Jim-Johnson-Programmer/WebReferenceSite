using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WebReferenceSite.Mvc.Models
{
    public class TreeNodeViewModel
    {
        public string title { get; set; } = string.Empty;
        public bool folder { get; set; }
        public string href { get; set; } = string.Empty;
        public string icon { get; set; } = "fancytree-icon";
        public string key { get; set; } = Guid.NewGuid().ToString();
        public bool lazy { get; set; } = true;
        public string target { get; set; } = string.Empty;
        public string tooltip { get; set; } = string.Empty;
        [JsonIgnore]
        public string path { get; set; } = string.Empty;
        /// <summary>
        /// Must be null for lazy load to work
        /// </summary>
        public List<TreeNodeViewModel> children { get; set; }// = new List<TreeNodeViewModel>();
    }
}
