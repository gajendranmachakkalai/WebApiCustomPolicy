using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTAPP.Model
{
    /// <summary>
    /// AppRoleRoute table
    /// </summary>
    public class RoleRouteModel
    {
        public int rolerouteid { get; set; }
        public int routeid { get; set; }
        public string route { get; set; }
        public Int16 roleid { get; set; }
        public string rolename { get; set; }
        public bool isactive { get; set; }
        public DateTime createddate { get; set; }
    }
}
