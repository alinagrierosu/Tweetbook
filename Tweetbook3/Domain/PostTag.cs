using System.ComponentModel.DataAnnotations;

namespace Tweetbook3.Domain
{
    public class PostTag
    {
        public string PostId { get; set; }
        public string TagName { get; set; }
        public Post Post { get; set; }
        public Tag Tag { get; set; }
    }
}
