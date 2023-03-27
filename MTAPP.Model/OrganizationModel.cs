using System.ComponentModel.DataAnnotations;

namespace MTAPP.Model
{
    /// <summary>
    /// Organization table
    /// </summary>
    public class OrganizationModel
    {
        public int orgid { get; set; }
        public string orgname { get; set; }
        public string orgcode { get; set; }
        public bool isactive { get; set; }
        public DateTime createddate { get; set; }

    }
}
