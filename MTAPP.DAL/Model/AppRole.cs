using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MTAPP.DAL.Model
{
    /// <summary>
    /// AppRole table
    /// </summary>
    public class AppRole
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int16 roleid { get; set; }
        public string rolename { get; set; }
        public DateTime createddate { get; set; }
        public virtual List<AppUser> users { get; set; }
        public virtual List<AppRoleRoute> routes { get; set; }

    }
}
