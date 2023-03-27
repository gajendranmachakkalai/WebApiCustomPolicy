using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MTAPP.Model
{
    /// <summary>
    /// AppUser table
    /// </summary>
    public class UserModel
    {
        public long userid { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string cellphone { get; set; }
        public Int16 roleid { get; set; }
        public string rolename { get; set; }
        public string passwordhash { get; set; }
        public DateTime createddate { get; set; }
        public string refreshtoken { get; set; }
    }
}