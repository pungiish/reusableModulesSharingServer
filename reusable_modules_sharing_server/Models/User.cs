using System.ComponentModel.DataAnnotations;
namespace reusable_modules_sharing_server.Models
{
    public class User
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Familyname { get; set; }
        [Required]
        public string GoogleID { get; set; }

    }
}
