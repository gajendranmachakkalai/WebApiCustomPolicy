using System.ComponentModel.DataAnnotations;

namespace MTAPP.Model
{
    /// <summary>
    /// AppRoute table
    /// </summary>
    public class RouteModel
    {
        public int routeid { get; set; }
        public string routepath { get; set; }
        public bool isexclude { get; set; }
        public bool isactive { get; set; }
        public DateTime createddate { get; set; }
    }
}
