using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTAPP.DAL.Model
{
    /// <summary>
    /// AppRoleRoute table
    /// </summary>
    public class AppRoleRoute
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int rolerouteid { get; set; }
        [ForeignKey("route")]
        public int routeid { get; set; }
        [ForeignKey("role")]
        public Int16 roleid { get; set; }
        public bool isactive { get; set; }
        public DateTime createddate { get; set; }
        public virtual AppRoute route { get; set; }
        public virtual AppRole role { get; set; }
    }
}
