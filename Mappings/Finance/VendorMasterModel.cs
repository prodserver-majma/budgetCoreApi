using mahadalzahrawebapi.Models;

namespace mahadalzahrawedapi.Mappings.Finance
{
    public class VendorMasterModel
    {
        public int id { get; set; }

        public string? name { get; set; }

        public string? phoneNo { get; set; }

        public string? mobileNo { get; set; }

        public string? whatsappNo { get; set; }

        public string? address { get; set; }

        public string? state { get; set; }

        public string? city { get; set; }

        public string? ifscCode { get; set; }

        public string? bankName { get; set; }

        public string? accountNo { get; set; }

        public string? accountName { get; set; }

        public string? panCardNo { get; set; }

        public DateTime? createdOn { get; set; }

        public string? createdBy { get; set; }

        public DateTime? updatedOn { get; set; }
        public int? updatedBy { get; set; }

        public bool? status { get; set; }

        public string? type { get; set; }

        public string? email { get; set; }

        public string? gstNumber { get; set; }

        public bool? isPanCardAttachmentEdited { get; set; }
        public string? panCardAttachment { get; set; }
        public string? panCardAttachmentFileName { get; set; }

        public int? deptVenueId { get; set; }
        public List<int>? psetId { get; set; } = new List<int>();
        public List<int>? schoolId { get; set; } = new List<int>();

        public int? userItsId { get; set; }

        public string? schoolClassName { get; set; }

        public int? pset { get; set; }
        public int school { get; set; }

        public bool? fileCheck { get; set; }
    }
}
