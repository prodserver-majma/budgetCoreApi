namespace mahadalzahrawebapi.Mappings
{
    public class Wafd_ul_huffaz_ExportModel
    {
        //personal details
        public string srNo { get; set; }
        public string photo2 { get; set; }
        public string itsId { get; set; }
        public string fullName { get; set; }
        public string fullNameArabic { get; set; }
        public string age { get; set; }
        public string dob { get; set; }
        public string dobArabic { get; set; }
        public string bloodGroup { get; set; }
        public string maritalStatus { get; set; }
        public string its_idaras { get; set; }
        public string its_preferredIdara { get; set; }
        public string nationality { get; set; }
        public string title { get; set; }
        public string jamaat { get; set; }
        public string jamiat { get; set; }
        public string mafsuhiyatYear { get; set; }
        public string haddiyatYear { get; set; }

        //contact details
        public string primaryEmailAddress { get; set; }
        public string officialEmailAddress { get; set; }
        public string emailAddress { get; set; }
        public string mobileNo { get; set; }
        public string whatsappNo { get; set; }

        //housing details
        public string khidmatMauzeHouseStatus { get; set; }
        public string khdimatMauzeHouseType { get; set; }
        public string khidmatMauzeAddress { get; set; }
        public string personalHouseStatus { get; set; }
        public string personalHouseType { get; set; }
        public string personalHouseArea { get; set; }
        public string personalHouseAddress { get; set; }
        public string watan { get; set; }
        public string watanAddress { get; set; }
        public string domacileParents { get; set; }
        public string domacileParentsAddress { get; set; }


        //acedemic details
        public string category { get; set; }

        public string farigDarajah
        { get; set; }
        public string fariqYear
        { get; set; }
        public string trNo
        { get; set; }
        public string alJameaDegree
        { get; set; }
        public string hifzSanadYear
        { get; set; }
        public string currentDarajah
        { get; set; }
        public string batchId
        { get; set; }
        public string latestQualifications
        { get; set; }



        //self asesment details

        public string personalityType
        { get; set; }
        public string aboutYourSelf
        { get; set; }


        //khidmat details                
        public string khidmatYear
        { get; set; }
        public string mahad_khidmatYear
        { get; set; }
        public string totalKhidmatYear
        { get; set; }
        public string tayeenYear
        { get; set; }
        public string tayeenDuration
        { get; set; }
        public string qismTahfeez
        { get; set; }
        public string moze
        { get; set; }
        public string programs
        { get; set; }
        public string mzPastMawaze
        { get; set; }
        public string otherIdaraMawaze
        { get; set; }
        public string mz_idara { get; set; }


        //Bank details       
        public string bankAccountName
        { get; set; }
        public string bankAccountNumber { get; set; }
        public string bankName { get; set; }
        public string bankBranch { get; set; }
        public string ifsc
        { get; set; }
        public string accountType
        { get; set; }
        public string pancardName
        { get; set; }
        public string pancardNumber
        { get; set; }




        //documents details              
        public string passportName
        { get; set; }
        public string passportNumber
        { get; set; }
        public string dobPassport
        { get; set; }
        public string passportBirthPlace
        { get; set; }
        public string dateOfIssue
        { get; set; }
        public string placeOfIssue
        { get; set; }
        public string dateOfExpiry
        { get; set; }
        public string aadharCardName
        { get; set; }
        public string aadharCardNo
        { get; set; }


        // strength & weakness
        public string strength
        { get; set; }
        public string weakness
        { get; set; }

        // questions about yourself
        public string longTermGoal { get; set; }
        public string changeAboutYourself { get; set; }
        public string alternativeCareerPath { get; set; }
        public string roleModel { get; set; }

        public int childCount { get; set; }
    }

    public class R_22
    {
        public int srNo { get; set; }

        public string photo2 { get; set; }

        public int? itsId { get; set; }
        public string fullName { get; set; }
        public string fullNameArabic { get; set; }
        public string age { get; set; }
        public string preferredIdara { get; set; }
        public string mz_idara { get; set; }
        public int batchId { get; set; }
        public string qismTahfeez { get; set; }
        public string moze { get; set; }
        public string category { get; set; }
        public int farigDarajah { get; set; }
        public string farigYear { get; set; }
        public string aljameaDegree { get; set; }
        public int darajah { get; set; }
        public string primaryEmailAddress { get; set; }
        public string mobileNumber { get; set; }
        public string whatsappNumber { get; set; }
        public string hifzSanadyear { get; set; }

        public string hifzStatus { get; set; }
        public string hifzStatusModified { get; set; }


    }

    public class R_22_1
    {
        public int srNo { get; set; }

        public int? itsId { get; set; }
        public string fullName { get; set; }
        public string fullNameArabic { get; set; }
        public string age { get; set; }
        public string preferredIdara { get; set; }
        public string mz_idara { get; set; }
        public int batchId { get; set; }
        public string qismTahfeez { get; set; }
        public string moze { get; set; }
        public string category { get; set; }
        public int farigDarajah { get; set; }
        public string farigYear { get; set; }
        public string aljameaDegree { get; set; }
        public int darajah { get; set; }
        public string primaryEmailAddress { get; set; }
        public string mobileNumber { get; set; }
        public string whatsappNumber { get; set; }
        public string hifzStatus { get; set; }
        public string hifzSanadyear { get; set; }
        public string hifzStatusModified { get; set; }

    }


}
