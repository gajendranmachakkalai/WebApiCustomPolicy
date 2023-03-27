using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MTAPP.DAL.Model
{
    /// <summary>
    /// Organization table
    /// </summary>
    public class Organization
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int orgid { get; set; }
        public string orgname { get; set; }
        public string orgcode { get; set; }
        public bool isactive { get; set; }
        public DateTime createddate { get; set; }

    }
}
