using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MTAPP.DAL.Model
{
    /// <summary>
    /// AppUser table
    /// </summary>
    public class AppUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long userid { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string cellphone { get; set; }
        [ForeignKey("role")]
        public Int16 roleid { get; set; }
        public string passwordhash { get; set; }
        public DateTime createddate { get; set; }
        public string refreshtoken { get; set; }
        public virtual AppRole role { get; set; }
    }
}