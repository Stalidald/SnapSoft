using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace SnapSoft.Models
{
    public class BaseModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public string? Body { get; set; }
        public string? Url { get; set; }
        public string? Method { get; set; }
        public Guid RequestId { get; set; }
        public int? Code { get; set; }
    }
}
