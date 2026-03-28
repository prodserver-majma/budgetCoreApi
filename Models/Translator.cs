using mahadalzahrawebapi.Mappings;
using mahadalzahrawebapi.Services;

namespace mahadalzahrawebapi.Models
{
    public class Translator
    {

        public static khidmat_guzaar modelToKg(EmployeeModel j)
        {
            EmployeeBasicDetailsModel k = j.basicDetails;
            return new khidmat_guzaar()
            {
                activeStatus = k.activeStatus == 1 ? true : false,
                age = k.age,
                bloodGroup = k.bloodGroup,
                CreatedBy = k.CreatedBy,
                CreatedOn = k.CreatedOn,
                currentAddress = k.currentAddress,
                c_codeMobile = k.c_codeMobile,
                c_codeWhatsapp = k.c_codeWhatsapp,
                dawat_title = k.dawat_title,
                dobGregorian = k.dobGregorian,
                dobHijri = k.dobHijri,
                dojGregorian = k.dojGregorian,
                dojHijri = k.dojHijri,
                domicileAddressParents = k.domicileAddressParents,
                domicileParent = k.domicileParent,
                emailAddress = k.emailAddress,
                employeeType = k.employeeType,
                fullName = k.fullName,
                fullNameArabic = k.fullNameArabic,
                haddiyatYear = k.haddiyatYear,
                id = k.id,
                isMumin = k.isMumin,
                itsId = k.itsId,
                its_idaras = k.its_idaras,
                its_preferredIdara = k.its_preferredIdara,
                jamaat = k.jamaat,
                jamiat = k.jamiat,
                mafsuhiyatYear = k.mafsuhiyatYear,
                maritalStatus = k.maritalStatus,
                mobileNo = k.mobileNo,
                muqam = k.muqam,
                muqamArabic = k.muqamArabic,
                mz_idara = k.mz_idara,
                nationality = k.nationality,
                officialEmailAddress = k.officialEmailAddress,
                personalHouseAddress = k.personalHouseAddress,
                personalHouseArea = k.personalHouseArea,
                personalHouseStatus = k.personalHouseStatus,
                personalHouseType = k.personalHouseType,
                photo = k.photo,
                photoBase64 = k.photoBase64,
                RfId = k.RfId,
                UpdatedOn = k.UpdatedOn,
                watan = k.watan,
                watanAdress = k.watanAdress,
                watanArabic = k.watanArabic,
                whatsappNo = k.whatsappNo,
                mauze = k.mauze,
                gender = k.gender,
                workType = k.workType,
                designation = k.designation,
                salaryCalender = k.salaryCalender
            };
        }

        public static employee_academic_details modelToAd(EmployeeModel j)
        {
            EmployeeAcademicDetailsModel ead = j.academicDetails;
            return new employee_academic_details()
            {
                aljameaDegree = ead.aljameaDegree,
                batchId = ead.batchId,
                category = ead.category,
                farigDarajah = ead.farigDarajah,
                farigYear = ead.farigYear,
                hifzSanadYear = ead.hifzSanadYear,
                hifzStatus = ead.hifzStatus,
                id = ead.id,
                itsId = ead.itsId,
                maqaraatTeacherIts = ead.maqaraatTeacherIts,
                trNo = ead.trNo,
                wafdClassId = ead.wafdClassId,
                wafdTrainingMasoolIts = ead.wafdTrainingMasoolIts,
                wafdTrainingMushrifIts = ead.wafdTrainingMushrifIts,
            };
        }

        public static List<employee_bank_details> modelToBd(EmployeeModel j)
        {
            List<EmployeeBankDetailsModel> ebds = j.bankDetails;
            if (ebds == null || ebds.Count == 0)
            {
                return new List<employee_bank_details>();
            }
            List<employee_bank_details> db = new List<employee_bank_details>();
            ebds.ForEach(x => db.Add(new employee_bank_details
            {
                bankAccountName = x.bankAccountName,
                bankAccountNumber = x.bankAccountNumber,
                bankAccountType = x.bankAccountType,
                bankBranch = x.bankBranch,
                bankName = x.bankName,
                chequeAttachment = x.chequeAttachment,
                id = x.id,
                ifsc = x.ifsc,
                itsId = x.itsId,
                isDefault = x.isDefault,
                country = x.country,
                domesticCode = x.domesticCode,
                internationalCode = x.internationalCode,
                internationalCodeType = x.internationalCodeType,
                panCard = x.panCard,
                panCardAttachment = x.panCardAttachment
            }));
            return db;
        }

        public static employee_salary modelToSd(EmployeeModel j)
        {
            EmployeeSalaryDetailsModel esd = j.employeeSalary;
            return new employee_salary()
            {
                itsId = esd.itsId,
                bqhs = esd.bqhs,
                conveyanceAllowance = esd.conveyanceAllowance,
                extraAllowance = esd.extraAllowance,
                fmbAllowance = esd.fmbAllowance,
                fmbDeduction = esd.fmbDeduction,
                grossSalary = esd.grossSalary,
                husainiQardanHasanah = esd.husainiQardanHasanah,
                installmentDeduction_Qardan = esd.installmentDeduction_Qardan,
                isHijriAllowence = esd.isHijriAllowence,
                isHusainiQardan = esd.isHusainiQardan,
                isMahadSalary = esd.isMahadSalary,
                lessDeduction = esd.lessDeduction,
                marafiqKhairiyah = esd.marafiqKhairiyah,
                marriageAllowance = esd.marriageAllowance,
                mohammediQardanHasanah = esd.mohammediQardanHasanah,
                mumbaiAllowance = esd.mumbaiAllowance,
                professionTax = esd.professionTax,
                qardanHasanah = esd.qardanHasanah,
                rentAllowance = esd.rentAllowance,
                sabeel = esd.sabeel,
                tds = esd.tds,
                currency = esd.currency,
                arrears = esd.arrears,
                incomeTax = esd.incomeTax,
                localTax = esd.localTax,
                withHoldings = esd.withHoldings,
                qardanHasanahNonRefundable = esd.qardanHasanahNonRefundable,
                qardanHasanahRefundable = esd.qardanHasanahRefundable,
                lastSalary = esd.lastSalary,
                lastSalaryDate = esd.lastSalaryDate
            };
        }

        public static kg_self_assessment modelToSa(EmployeeModel j)
        {
            EmployeeSelfAssessmentModel esa = j.selfAssessment;
            return new kg_self_assessment()
            {
                itsId = esa.itsId,
                aboutYourSelf = esa.aboutYourSelf,
                alternativeCareerPath = esa.alternativeCareerPath,
                changeAboutYourself = esa.changeAboutYourself,
                id = esa.id,
                longTermGoal = esa.longTermGoal,
                personalityReport = esa.personalityReport,
                personalitytype = esa.personalitytype,
                roleModel = esa.roleModel,
                strength = esa.strength,
                weakness = esa.weakness,
            };
        }

        public static employee_passport_details modelToPd(EmployeeModel j)
        {
            EmployeePassportDetailsModel epd = j.passportDetails;
            return new employee_passport_details()
            {
                dateOfExpiry = epd.dateOfExpiry,
                dateOfIssue = epd.dateOfIssue,
                dobPassport = epd.dobPassport,
                id = epd.id,
                itsId = epd.itsId,
                passportCopy = epd.passportCopy,
                passportName = epd.passportName,
                passportNo = epd.passportNo,
                passportPlaceOfBirth = epd.passportPlaceOfBirth,
                placeOfIssue = epd.placeOfIssue,
            };
        }

        public static employee_khidmat_details modelToKd(EmployeeModel j)
        {
            EmployeeKidmatDetailsModel ekd = j.khidmatDetails;
            return new employee_khidmat_details()
            {
                id = ekd.id,
                itsId = ekd.itsId,
                khdimatMauzeHouseType = ekd.khdimatMauzeHouseType,
                khidmatMauzeHouseStatus = ekd.khidmatMauzeHouseStatus,
                khidmatMonth = ekd.khidmatMonth,
                khidmatYear = ekd.khidmatYear,
                mahad_khidmatYear = ekd.mahad_khidmatYear,
                tayeenMonth = ekd.tayeenMonth,
                tayeenYear = ekd.tayeenYear,
            };
        }

        public static employee_family_details modelToFd(EmployeeModel j)
        {
            EmployeeFamilyDetailsModel efd = j.familyDetails;
            return new employee_family_details()
            {
                FatherIts = efd.FatherIts,
                FatherName = efd.FatherName,
                id = efd.id,
                itsId = efd.itsId,
                MotherIts = efd.MotherIts,
                MotherName = efd.MotherName,
                SpouseIts = efd.SpouseIts,
                SpouseName = efd.SpouseName,
            };
        }

        public static List<employee_dept_salary> modelToLDS(EmployeeModel j)
        {
            List<EmployeeDeptSalaryModel> eds = j.deptSalaries;
            if (eds == null || eds.Count == 0)
            {
                return new List<employee_dept_salary>();
            }
            List<employee_dept_salary> db = new List<employee_dept_salary>();
            eds.ForEach(x => db.Add(new employee_dept_salary
            {
                isHijriSalary = x.isHijriSalary,
                deptVenueId = x.deptVenueId,
                hasSalary = x.hasSalary,
                itsId = x.itsId,
                salaryAmount = x.salaryAmount,
                salaryTypeId = x.salaryTypeId,
                workingMin = x.workingMin,
                workingDays = x.workingDays
            }));
            return db;
        }

        public static EmployeeModel khtoModel(khidmat_guzaar k, bool containVirtual = true)
        {
            //var context = new mzmanageEntities();

            string step = "";

            EmployeeModel kg = new EmployeeModel();
            kg = null;
            //CacheService cache = new CacheService();
            SalaryService salaryService = new SalaryService();
            HelperService helperService = new HelperService();
            //kg = cache.GetItem<EmployeeModel>("getEmployeeData" + k.itsId);
            try
            {

                if (kg == null)
                {
                    kg = new EmployeeModel();
                    kg.basicDetails = new EmployeeBasicDetailsModel();
                    step = "Step 1";
                    kg.basicDetails.activeStatus = k.activeStatus == true ? 1 : 0;
                    kg.basicDetails.age = k.age;
                    kg.basicDetails.bloodGroup = k.bloodGroup;
                    kg.basicDetails.CreatedBy = k.CreatedBy;
                    kg.basicDetails.CreatedOn = k.CreatedOn;
                    kg.basicDetails.currentAddress = k.currentAddress;
                    kg.basicDetails.c_codeMobile = k.c_codeMobile;
                    kg.basicDetails.c_codeWhatsapp = k.c_codeWhatsapp;
                    kg.basicDetails.dawat_title = k.dawat_title;
                    kg.basicDetails.dobGregorian = k.dobGregorian;
                    kg.basicDetails.dobHijri = k.dobHijri;
                    kg.basicDetails.dojGregorian = k.dojGregorian;
                    kg.basicDetails.dojHijri = k.dojHijri;
                    kg.basicDetails.domicileAddressParents = k.domicileAddressParents;
                    kg.basicDetails.domicileParent = k.domicileParent;
                    kg.basicDetails.emailAddress = k.emailAddress;
                    kg.basicDetails.employeeType = k.employeeType;
                    kg.basicDetails.fullName = k.fullName;
                    kg.basicDetails.fullNameArabic = k.fullNameArabic;
                    kg.basicDetails.haddiyatYear = k.haddiyatYear;
                    kg.basicDetails.id = k.id;
                    kg.basicDetails.isMumin = k.isMumin;
                    kg.basicDetails.itsId = k.itsId;
                    kg.basicDetails.its_idaras = k.its_idaras;
                    kg.basicDetails.its_preferredIdara = k.its_preferredIdara;
                    kg.basicDetails.jamaat = k.jamaat;
                    kg.basicDetails.jamiat = k.jamiat;
                    kg.basicDetails.mafsuhiyatYear = k.mafsuhiyatYear;
                    kg.basicDetails.maritalStatus = k.maritalStatus;
                    kg.basicDetails.mobileNo = k.mobileNo;
                    kg.basicDetails.muqam = k.muqam;
                    kg.basicDetails.muqamArabic = k.muqamArabic;
                    kg.basicDetails.mz_idara = k.mz_idara;
                    kg.basicDetails.nationality = k.nationality;
                    kg.basicDetails.officialEmailAddress = k.officialEmailAddress;
                    kg.basicDetails.personalHouseAddress = k.personalHouseAddress;
                    kg.basicDetails.personalHouseArea = k.personalHouseArea;
                    kg.basicDetails.personalHouseStatus = k.personalHouseStatus;
                    kg.basicDetails.personalHouseType = k.personalHouseType;
                    kg.basicDetails.photo = k.photo;
                    kg.basicDetails.photoBase64 = k.photoBase64;
                    kg.basicDetails.RfId = k.RfId;
                    kg.basicDetails.UpdatedOn = k.UpdatedOn;
                    kg.basicDetails.watan = k.watan;
                    kg.basicDetails.watanAdress = k.watanAdress;
                    kg.basicDetails.watanArabic = k.watanArabic;
                    kg.basicDetails.whatsappNo = k.whatsappNo;
                    kg.basicDetails.workType = k.workType;
                    kg.basicDetails.mauze = k.mauze;
                    kg.basicDetails.gender = k.gender;
                    kg.basicDetails.mauzeName = k.mauzeNavigation?.displayName;
                    kg.basicDetails.designation = k.designation;
                    kg.basicDetails.salaryCalender = k.salaryCalender;

                    step = "Step 2";

                    employee_academic_details ead = k.employee_academic_details;
                    if (ead != null)
                    {
                        kg.academicDetails = new EmployeeAcademicDetailsModel()
                        {
                            aljameaDegree = ead.aljameaDegree,
                            batchId = ead.batchId,
                            category = ead.category,
                            farigDarajah = ead.farigDarajah,
                            farigYear = ead.farigYear,
                            hifzSanadYear = ead.hifzSanadYear,
                            hifzStatus = ead.hifzStatus,
                            id = ead.id,
                            itsId = ead.itsId,
                            maqaraatTeacherIts = ead.maqaraatTeacherIts,
                            trNo = ead.trNo,
                            wafdClassId = ead.wafdClassId,
                            wafdTrainingMasoolIts = ead.wafdTrainingMasoolIts,
                            wafdTrainingMushrifIts = ead.wafdTrainingMushrifIts,
                        };
                    }
                    step = "Step 3";
                    employee_bank_details ebd = k.employee_bank_details.Where(x => x.isDefault).FirstOrDefault();
                    if (ebd == null)
                    {
                        ebd = k.employee_bank_details.FirstOrDefault();
                    }

                    if (ebd != null)
                    {
                        kg.bankDetails = new List<EmployeeBankDetailsModel>();

                        kg.bankDetails.Add(new EmployeeBankDetailsModel()
                        {
                            bankAccountName = ebd.bankAccountName,
                            bankAccountNumber = ebd.bankAccountNumber,
                            bankAccountType = ebd.bankAccountType,
                            bankBranch = ebd.bankBranch,
                            bankName = ebd.bankName,
                            chequeAttachment = ebd.chequeAttachment,
                            id = ebd.id,
                            ifsc = ebd.ifsc,
                            itsId = ebd.itsId,
                            panCardAttachment = ebd.panCardAttachment,
                            panCard = ebd.panCard,
                            internationalCodeType = ebd.internationalCodeType,
                            internationalCode = ebd.internationalCode,
                            domesticCode = ebd.domesticCode,
                            country = ebd.country,
                            isDefault = ebd.isDefault
                        });
                    }
                    step = "Step 4";

                    employee_salary esd = k.employee_salary;
                    if (esd != null)
                    {
                        kg.employeeSalary = new EmployeeSalaryDetailsModel()
                        {
                            itsId = esd.itsId,
                            bqhs = esd.bqhs,
                            conveyanceAllowance = esd.conveyanceAllowance,
                            extraAllowance = esd.extraAllowance,
                            fmbAllowance = esd.fmbAllowance,
                            fmbDeduction = esd.fmbDeduction,
                            grossSalary = esd.grossSalary,
                            husainiQardanHasanah = esd.husainiQardanHasanah,
                            installmentDeduction_Qardan = esd.installmentDeduction_Qardan,
                            isHijriAllowence = esd.isHijriAllowence,
                            isHusainiQardan = esd.isHusainiQardan,
                            isMahadSalary = esd.isMahadSalary,
                            lessDeduction = esd.lessDeduction,
                            marafiqKhairiyah = esd.marafiqKhairiyah,
                            marriageAllowance = esd.marriageAllowance,
                            mohammediQardanHasanah = esd.mohammediQardanHasanah,
                            mumbaiAllowance = esd.mumbaiAllowance,
                            professionTax = esd.professionTax,
                            qardanHasanah = esd.qardanHasanah,
                            rentAllowance = esd.rentAllowance,
                            sabeel = esd.sabeel,
                            tds = esd.tds,
                            currency = esd.currency,
                            qardanHasanahNonRefundable = esd.qardanHasanahNonRefundable,
                            qardanHasanahRefundable = esd.qardanHasanahRefundable,
                            lastSalaryDate = esd.lastSalaryDate,
                            lastSalary = esd.lastSalary,
                            arrears = esd.arrears,
                            incomeTax = esd.incomeTax,
                            localTax = esd.localTax,
                            withHoldings = esd.withHoldings
                        };
                    }

                    employee_passport_details epd = k.employee_passport_details.FirstOrDefault();
                    if (epd != null)
                    {
                        kg.passportDetails = new EmployeePassportDetailsModel()
                        {
                            dateOfExpiry = epd.dateOfExpiry,
                            dateOfIssue = epd.dateOfIssue,
                            dobPassport = epd.dobPassport,
                            id = epd.id,
                            itsId = epd.itsId,
                            passportCopy = epd.passportCopy,
                            passportName = epd.passportName,
                            passportNo = epd.passportNo,
                            passportPlaceOfBirth = epd.passportPlaceOfBirth,
                            placeOfIssue = epd.placeOfIssue,
                        };
                    }
                    step = "Step 8";

                    employee_khidmat_details ekd = k.employee_khidmat_details.FirstOrDefault();
                    if (ekd != null)
                    {
                        kg.khidmatDetails = new EmployeeKidmatDetailsModel()
                        {
                            id = ekd.id,
                            itsId = ekd.itsId,
                            khdimatMauzeHouseType = ekd.khdimatMauzeHouseType,
                            khidmatMauzeHouseStatus = ekd.khidmatMauzeHouseStatus,
                            khidmatMonth = ekd.khidmatMonth,
                            khidmatYear = ekd.khidmatYear,
                            mahad_khidmatYear = ekd.mahad_khidmatYear,
                            tayeenMonth = ekd.tayeenMonth,
                            tayeenYear = ekd.tayeenYear,
                        };
                    }
                    step = "Step 9";

                    venue v = k.mauzeNavigation;

                    float grossWazifa = 0;
                    float timeBasedWazifa = 0;
                    float wazifa = 0;
                    int totalMin = 0;

                    //salary_allocation_gegorian sag = _context.salary_allocation_gegorian.Where(x => x.itsId == kg.bankDetails.itsId).OrderByDescending(x => x.createdOn).FirstOrDefault();
                    //salary_allocation_hijri sah = _context.salary_allocation_hijri.Where(x => x.itsId == kg.bankDetails.itsId).OrderByDescending(x => x.createdOn).FirstOrDefault();

                    //wazifa = (sag?.netEarnings ?? 0) + (sah?.netEarnings ?? 0);

                    string darajah = "NA";
                    step = "Step 10";

                    if (kg.academicDetails?.wafdClassId != null)
                    {
                        //darajah = (_context.nisaab_classes.Where(x => x.id == kg.academicDetails.wafdClassId).FirstOrDefault().std).ToString();

                    }
                    TimeSpan dateSpan = DateTime.Now - (kg.basicDetails.dojGregorian ?? DateTime.Now);

                    //timeSpan to years and month string

                    int years = dateSpan.Days / 365;
                    int months = (dateSpan.Days % 365) / 30;

                    string khidmatDurationString = "years: " + years + " months: " + months;

                    kg.extraDetails = new EmployeeExtraDetailsModel()
                    {
                        //khidmatDuration = new HijriCalenderService().getTodayHijriDate().hijYear - (kg.khidmatDetails.khidmatYear ?? 0),
                        latestQualification = k.qualification.OrderBy(x => int.Parse(x.year)).LastOrDefault()?.degree ?? "Not Available",
                        mauze = v?.displayName,
                        netSalary = (int)wazifa,
                        qismAlTahfeez = v?.qism?.name,
                        trainingDarajah = darajah,
                        estimatedSalary = 0,
                        khidmatDuration = years,
                        salaryCalender = "",
                        salaryType = "",
                        workingHours = 0,
                        idaraColor = "",
                        khidmatDurationString = khidmatDurationString,
                        status = ""
                    };

                    kg.deptSalaries = new List<EmployeeDeptSalaryModel>();

                    List<employee_dept_salary> eds = k.employee_dept_salary.ToList();
                    step = "Step 11";

                    eds.ForEach(x =>
                    {
                        if (eds[0] == x)
                        {
                            kg.extraDetails.salaryType = x.salaryType.Name;
                            kg.extraDetails.salaryCalender = (x.isHijriSalary ?? false) ? "Hijri" : "Gregorian";
                        }

                        if (!kg.extraDetails.salaryCalender.Contains((x.isHijriSalary ?? false) ? "Hijri" : "Gregorian"))
                        {
                            kg.extraDetails.salaryType += ", " + ((x.isHijriSalary ?? false) ? "Hijri" : "Gregorian");
                        }
                        switch (x.salaryTypeId)
                        {
                            case 1:
                                if (!kg.extraDetails.salaryType.Contains("Fixed"))
                                {
                                    kg.extraDetails.salaryType += ", " + "Fixed";
                                }
                                grossWazifa += (x.salaryAmount ?? 0);
                                totalMin += (x.workingMin ?? 0);
                                break;
                            case 2:
                                grossWazifa += ((x.salaryAmount ?? 0) * (x.workingMin ?? 0)) * (x.workingDays ?? 0);
                                timeBasedWazifa += ((x.salaryAmount ?? 0) * (x.workingMin ?? 0)) * (x.workingDays ?? 0);
                                totalMin += (x.workingMin ?? 0);
                                if (!kg.extraDetails.salaryType.Contains("Per Minute"))
                                {
                                    kg.extraDetails.salaryType += ", " + "Per Minute";
                                }
                                break;
                            case 3:
                                if (!kg.extraDetails.salaryType.Contains("Per Period"))
                                {
                                    kg.extraDetails.salaryType += ", " + "Per Period";
                                }
                                grossWazifa += ((x.salaryAmount ?? 0) * (x.workingMin ?? 0));
                                timeBasedWazifa += ((x.salaryAmount ?? 0) * (x.workingMin ?? 0));
                                totalMin += ((x.workingMin ?? 0) * 30) / 25;
                                break;
                        }
                        kg.deptSalaries.Add(new EmployeeDeptSalaryModel
                        {
                            isHijriSalary = x.isHijriSalary,
                            deptVenueId = x.deptVenueId,
                            dept_venue = new dept_venue_dto
                            {
                                deptName = x.deptVenue?.deptName + "_" + x.deptVenue?.venueName,
                                venueId = x.deptVenue?.venueId,
                            },
                            hasSalary = x.hasSalary,
                            itsId = x.itsId,
                            salaryAmount = x.salaryAmount,
                            salaryTypeId = x.salaryTypeId,
                            workingMin = x.workingMin,
                            salary_Type = new salary_type_dto
                            {
                                id = x.salaryType.id,
                                Name = x.salaryType?.Name
                            },
                            workingDays = x.workingDays
                        });
                    });
                    step = "Step 12";

                    kg.extraDetails.workingHours = totalMin / 60;
                    kg.extraDetails.estimatedSalary = (int)grossWazifa;
                    kg.extraDetails.maxTimeBasedWazifa = (int)timeBasedWazifa;

                    //kg.extraDetails.idaraColor = helperService.stringToColorCode(kg?.basicDetails?.mz_idara??"Not Available");

                    if (kg.employeeSalary != null)
                    {

                        EmployeeSalaryDetailsModel tempSalary = kg.employeeSalary;
                        kg.extraDetails.netSalary = salaryService.netSalary(tempSalary);
                    }

                    //cache.AddItem("getEmployeeData" + k.itsId, kg, DateTime.Now.AddDays(30));
                }
                if (!containVirtual)
                {
                    kg.deptSalaries.ForEach(x =>
                    {
                        x.dept_venue = new dept_venue_dto
                        {
                            deptName = x.dept_venue?.deptName + "_" + x.dept_venue?.venueName,
                            venueId = x.dept_venue?.venueId,
                        };
                        x.salary_Type = new salary_type_dto
                        {
                            id = x.salary_Type.id,
                            Name = x.salary_Type?.Name
                        };
                    });
                }

                return kg;
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString() + " ITS:" + k.itsId + " " + step);
            }
        }


    }
}
