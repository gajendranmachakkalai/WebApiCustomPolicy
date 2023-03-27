using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MTAPP.DAL.Model
{
    /// <summary>
    /// AppRoute table
    /// </summary>
    public class AppRoute
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int routeid { get; set; }
        public string routepath { get; set; }
        public bool isexclude { get; set; }
        public bool isactive { get; set; }
        public DateTime createddate { get; set; }
        public virtual List<AppRoleRoute> roles { get; set; }

    }
}
