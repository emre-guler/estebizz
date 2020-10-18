using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estebizz.Models.Entities
{
    public class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Path { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}
