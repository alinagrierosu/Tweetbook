using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.PortableExecutable;

namespace Tweetbook3.Domain
{
    public class Tag
    {
        [Key]
        public string TagName { get; set; }
        public string CreatedById { get; set; }

        [ForeignKey(nameof(CreatedById))]
        public IdentityUser CreatedByUser { get; set; }
    }
}
