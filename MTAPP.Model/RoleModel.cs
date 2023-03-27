using System.ComponentModel.DataAnnotations;

namespace MTAPP.Model
{
    /// <summary>
    /// AppRole table
    /// </summary>
    public class RoleModel
    {
        public Int16 roleid { get; set; }
        public string rolename { get; set; }
        public DateTime createddate { get; set; }
    }
}
