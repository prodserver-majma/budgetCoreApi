using mahadalzahrawebapi.Controllers;

namespace mahadalzahrawebapi.Mappings
{
    public class EmployeeBasicDetailsModel
    {
        public int? id { get; set; }
        public string? photo { get; set; }
        public int itsId { get; set; } // in HR profile
        public string? fullName { get; set; } // in HR profile
        public string? fullNameArabic { get; set; } // in HR profile
        public string? c_codeMobile { get; set; } // in HR profile
        public string? mobileNo { get; set; } // in HR profile
        public string? c_codeWhatsapp { get; set; } // in HR profile
        public string? whatsappNo { get; set; } // in HR profile
        public string? emailAddress { get; set; } // in HR profile
        public string? officialEmailAddress { get; set; } // in HR profile
        public string? watan { get; set; } // in HR profile
        public string? watanArabic { get; set; }
        public string? watanAdress { get; set; } // in HR profile
        public string? muqam { get; set; }
        public string? muqamArabic { get; set; }
        public DateTime? dojGregorian { get; set; }
        public string? dojHijri { get; set; }
        public string? dobGregorian { get; set; } // in HR profile
        public string? dobHijri { get; set; } // in HR profile
        public string? bloodGroup { get; set; } // in HR profile
        public string? currentAddress { get; set; } // in HR profile
        public string? maritalStatus { get; set; } // in HR profile
        public int? mafsuhiyatYear { get; set; } // in HR profile
        public int? activeStatus { get; set; }
        public string? nationality { get; set; } // in HR profile
        public string? its_idaras { get; set; }
        public string? its_preferredIdara { get; set; } // in HR profile
        public string? mz_idara { get; set; }
        public string? dawat_title { get; set; } // in HR profile
        public string? jamaat { get; set; } // in HR profile
        public string? jamiat { get; set; } // in HR profile
        public int? age { get; set; } // in HR profile
        public int? haddiyatYear { get; set; } // in HR profile
        public string? domicileParent { get; set; } // in HR profile
        public string? domicileAddressParents { get; set; } // in HR profile
        public string? personalHouseStatus { get; set; } // in HR profile
        public string? personalHouseType { get; set; } // in HR profile
        public string? personalHouseArea { get; set; } // in HR profile
        public string? personalHouseAddress { get; set; } // in HR profile
        public string? designation { get; set; }
        public string? salaryCalender { get; set; }

        public string? photoBase64 { get; set; }
        public string? employeeType { get; set; }
        public string? RfId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public bool? isMumin { get; set; }
        public int? mauze { get; set; }
        public int? batchid { get; set; }
        public int? status { get; set; }

        public string? gender { get; set; }
        public string? workType { get; set; }
        public string? mauzeName { get; set; } // in HR profile
        public List<mawaze>? mawazes { get; set;  }
        public List<int>? mawaze { get; set; }
        public int? roleId { get; set; }

        public string? khidmatMauzeHouseSatus { get; set; } // in HR profile
        public string? khidmatMauzeHouseType { get; set; } // in HR profile
        public string? khidmatMauzeHouseAddress { get; set; } // in HR profile

        public List<int> psetId { get; set; } = new List<int>();
    }

    public class mawaze{
        public int id { get; set; }
        public List<classes>? classes { get; set; }
    }

    public class userDept
    {
        public int itsId { get; set; }
        public List<int>? venueIds { get; set; }
        public List<int>? classId { get; set; }
    }

}
