using Newtonsoft.Json; // This is assuming you're using Json.NET for JSON operations
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace mahadalzahrawebapi.Models
{
    public class notification_email_template
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        [StringLength(255)]
        public string templateName { get; set; }

        [Required]
        [StringLength(255)]
        public string module { get; set; }

        [Required]
        public string content { get; set; } // HTML content

        // Storing dynamic attributes as JSON string, assuming one template can have various attributes
        public string dynamicAttributes { get; set; }

        // Could be an enum or another reference that ties the template to a specific stage or use case
        [StringLength(255)]
        public string reference { get; set; }

        [Required]
        [StringLength(255)]
        public string emailSubject { get; set; }

        // You might want to add fields for creation, modification, and who created or modified the template.
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        [StringLength(255)]
        public string createdBy { get; set; }
        [StringLength(255)]
        public string updatedBy { get; set; }

        // Utility method to deserialize dynamic attributes into a concrete type
        [NotMapped] // This means it will not be mapped to a database column
        public Dictionary<string, string> attributesDictionary
        {
            get => string.IsNullOrEmpty(dynamicAttributes)
                ? new Dictionary<string, string>()
                : JsonConvert.DeserializeObject<Dictionary<string, string>>(dynamicAttributes);
            set => dynamicAttributes = JsonConvert.SerializeObject(value);
        }
    }
}