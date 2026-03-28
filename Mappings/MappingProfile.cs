
using AutoMapper;
using mahadalzahrawebapi.Mappings.Leave;
using mahadalzahrawebapi.Mappings.Training;
using mahadalzahrawebapi.Models;
using mahadalzahrawebapi.Services;
using mahadalzahrawedapi.Mappings.Finance;

namespace mahadalzahrawebapi.Mappings
{

    public class MappingProfile : Profile
    {
        HelperService helperService = new HelperService();

        public MappingProfile()
        {

            DateTime curr = DateTime.UtcNow;
            // Define your mappings here

            #region Leave
            CreateMap<mzlm_leave_type, mzlm_leave_type_dto>();
            CreateMap<mzlm_leave_category, mzlm_leave_category_dto>();
            CreateMap<mzlm_leave_logs, mzlm_leave_logs_dto>()
                .ForMember(
                    Dest => Dest.createdByName,
                    Opt => Opt.MapFrom(src => src.createdByNavigation.fullName ?? "")
                ).ForMember(
                    Dest => Dest.stageName,
                    Opt => Opt.MapFrom(src => src.stage.name ?? ""));
            CreateMap<mzlm_leave_package_logs, mzlm_leave_package_logs_dto>()
                .ForMember(
                    Dest => Dest.createdByName,
                    Opt => Opt.MapFrom(src => src.createdByNavigation.fullName ?? "")
                ).ForMember(
                    Dest => Dest.stageName,
                    Opt => Opt.MapFrom(src => src.stage.name ?? ""));
            CreateMap<mzlm_leave_stage, mzlm_leave_stage_dto>();
            CreateMap<mzlm_leave_application, mzlm_leave_application_dto>()
                .ForMember(
                    dest => dest.mauze,
                    opt => opt.MapFrom(src => src.its != null && src.its.mauzeNavigation != null ? src.its.mauzeNavigation.displayName : "")
                )
                .ForMember(
                    dest => dest.qismName,
                    opt => opt.MapFrom(src => src.its != null && src.its.mauzeNavigation != null && src.its.mauzeNavigation.qism != null ? src.its.mauzeNavigation.qism.name : "")
                )
                .ForMember(
                    dest => dest.qismId,
                    opt => opt.MapFrom(src => src.its != null && src.its.mauzeNavigation != null ? src.its.mauzeNavigation.qismId : (int?)null)
                )
                .ForMember(
                    dest => dest.name,
                    opt => opt.MapFrom(src => src.its != null ? src.its.fullName : "")
                )
                .ForMember(
                    dest => dest.mzIdara,
                    opt => opt.MapFrom(src => src.its != null ? src.its.mz_idara : "")
                )
                .ForMember(
                    dest => dest.approvalStages,
                    opt => opt.MapFrom(src => src.type != null ? src.type.approvalFlow : "")
                )
                .ForMember(
                    dest => dest.typeName,
                    opt => opt.MapFrom(src => src.type != null ? src.type.name : "")
                )
                .ForMember(
                    dest => dest.categoryName,
                    opt => opt.MapFrom(src => src.category != null ? src.category.name : "")
                )
                .ForMember(
                    dest => dest.stageName,
                    opt => opt.MapFrom(src => src.stage != null ? src.stage.name : "")
                )
                .ForMember(
                    dest => dest.fromDate,
                    opt => opt.MapFrom(src => $"{src.fromDayId}/{src.fromMonthId}/{src.fromYear}")
                )
                .ForMember(
                    dest => dest.toDate,
                    opt => opt.MapFrom(src => $"{src.toDayId}/{src.toMonthId}/{src.toYear}")
                )
                .ForMember(
                    dest => dest.onLeave,
                    opt => opt.MapFrom(src => src.fromEngDate.Date <= curr.Date && src.toEngDate.Date >= curr.Date && (src.stageId == 5 || src.stageId == 6))
                )
                .ForMember(
                    dest => dest.isDeductable,
                    opt => opt.MapFrom(src => src.stage != null ? src.stage.isDeductable : null)
                )
                .ForMember(
                    dest => dest.remainingDays,
                    opt => opt.MapFrom(src => helperService.remaingDays(src.fromEngDate))
                );
            CreateMap<mzlm_leave_application_dto, mzlm_leave_application>();

            CreateMap<mzlm_leave_application, mzlm_leave_application>();
            CreateMap<mzlm_leave_stage_dto, mzlm_leave_stage>();
            CreateMap<mzlm_leave_logs_dto, mzlm_leave_logs>();
            CreateMap<mzlm_leave_package_logs_dto, mzlm_leave_package_logs>();
            CreateMap<mzlm_leave_category_dto, mzlm_leave_category>();
            CreateMap<mzlm_leave_type_dto, mzlm_leave_type>();
            CreateMap<azwaaj_minentry, azwaaj_minentry_dto>()
                .ForMember(dest => dest.deptVenueName, opt => opt.MapFrom(src => src.deptVenue != null ? src.deptVenue.deptName + "_" + src.deptVenue.venueName : ""))
                .ForMember(dest => dest.qismId, opt => opt.MapFrom(src => src.deptVenue != null ? src.deptVenue.qismId ?? 0 : 0))
                .ForMember(dest => dest.policyName, opt => opt.MapFrom(src => src.salaryType != null ? src.salaryType.Name : ""))
                .ForMember(dest => dest.employeeType, opt => opt.MapFrom(src => src.its != null ? src.its.employeeType : ""))
                .ForMember(dest => dest.mz_idara, opt => opt.MapFrom(src => src.its != null ? src.its.mz_idara : ""))
                .ForMember(dest => dest.mauze, opt => opt.MapFrom(src => src.its != null && src.its.mauzeNavigation != null ? src.its.mauzeNavigation.displayName : ""))
                .ForMember(dest => dest.designation, opt => opt.MapFrom(src => src.its != null ? src.its.designation : ""))
                .ForMember(dest => dest.value, opt => opt.MapFrom(src => src.allotedMin))
                .ForMember(dest => dest.mindiff, opt => opt.MapFrom(src => src.allotedMin - src.min))
                .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.its != null ? src.its.fullName : ""));
            //.ForMember(dest => dest.value, opt => opt.MapFrom<WorkingMinResolver>());

            CreateMap<azwaaj_minentry_dto, azwaaj_minentry>();

            CreateMap<mzlm_leave_package, mzlm_leave_package_dto>()
            .ForMember(dest => dest.categoryName,
                       opt => opt.MapFrom(src => src.mzlm_leave_application.First().category.name ?? ""))
            .ForMember(dest => dest.typeName,
                       opt => opt.MapFrom(src => src.mzlm_leave_application.First().type.name ?? ""))
            .ForMember(dest => dest.approvalStages,
                       opt => opt.MapFrom(src => src.mzlm_leave_application.First().type.approvalFlow ?? ""))
            .ForMember(dest => dest.qismIds,
                        opt => opt.MapFrom(src => src.mzlm_leave_application
                                      .Where(x => x.its != null && x.its.mauzeNavigation != null)
                                      .Select(x => x.its.mauzeNavigation.qismId)
                                      .Distinct()
                                      .ToArray()))
            .ForMember(dest => dest.fromDate,
                       opt => opt.MapFrom(src => helperService.GetFromDate(src)))
            .ForMember(dest => dest.toDate,
                       opt => opt.MapFrom(src => helperService.GetToDate(src)))
            .ForMember(dest => dest.stageName,
                       opt => opt.MapFrom(src => src.mzlm_leave_application.First().stage.name))
            .ForMember(dest => dest.totalDays,
                       opt => opt.MapFrom(src => (float)src.mzlm_leave_application.First().shiftCount / 2.0))
            .ForMember(dest => dest.eveningShift,
                       opt => opt.MapFrom(src => src.mzlm_leave_application.First().eveningShift))
            .ForMember(dest => dest.morningShift,
                       opt => opt.MapFrom(src => src.mzlm_leave_application.First().morningShift))
            .ForMember(dest => dest.fromEngDate,
                       opt => opt.MapFrom(src => src.mzlm_leave_application.First().fromEngDate))
            .ForMember(dest => dest.toEngDate,
                       opt => opt.MapFrom(src => src.mzlm_leave_application.First().toEngDate))
            .ForMember(dest => dest.qismName,
                       opt => opt.MapFrom(src => src.mzlm_leave_application.First().its.mauzeNavigation.qism.name ?? ""))
            .ForMember(
                    dest => dest.remainingDays,
                    opt => opt.MapFrom(src => helperService.remaingDays(src.mzlm_leave_application.First().fromEngDate))
                )
            .ForMember(
                    dest => dest.onLeave,
                    opt => opt.MapFrom(src => src.mzlm_leave_application.First().fromEngDate.Date <= curr.Date && src.mzlm_leave_application.First().toEngDate.Date >= curr.Date && (src.stageId == 5 || src.stageId == 6))
                )
            .ForMember(dest => dest.appliedBy,
                       opt => opt.MapFrom(src => src.mzlm_leave_application.First().appliedBy));


            CreateMap<mzlm_leave_application, mzlm_leave_package_dto>()
            .ForMember(dest => dest.categoryName,
                       opt => opt.MapFrom(src => src.category.name ?? ""))
            .ForMember(dest => dest.typeName,
                       opt => opt.MapFrom(src => src.type.name ?? ""))
            .ForMember(dest => dest.approvalStages,
                       opt => opt.MapFrom(src => src.type.approvalFlow ?? ""))
            .ForMember(dest => dest.qismIds,
                        opt => opt.MapFrom(src => src.its.mauzeNavigation.qismId))
            .ForMember(dest => dest.fromDate,
                       opt => opt.MapFrom(src => helperService.GetFromDate(src)))
            .ForMember(dest => dest.toDate,
                       opt => opt.MapFrom(src => helperService.GetToDate(src)))
            .ForMember(dest => dest.stageName,
                       opt => opt.MapFrom(src => src.stage.name))
            .ForMember(dest => dest.totalDays,
                       opt => opt.MapFrom(src => (float)src.shiftCount / 2.0))
            .ForMember(dest => dest.name,
                       opt => opt.MapFrom(src => src.package.name))
            .ForMember(dest => dest.id,
                                  opt => opt.MapFrom(src => src.packageId))
            .ForMember(dest => dest.purpose,
                                  opt => opt.MapFrom(src => src.package.purpose))
            .ForMember(dest => dest.qismName,
                       opt => opt.MapFrom(src => src.its.mauzeNavigation.qism.name ?? ""))
            .ForMember(
                    dest => dest.remainingDays,
                    opt => opt.MapFrom(src => helperService.remaingDays(src.fromEngDate))
                )
            .ForMember(
                    dest => dest.onLeave,
                    opt => opt.MapFrom(src => src.fromEngDate.Date <= curr.Date && src.toEngDate.Date >= curr.Date && (src.stageId == 5 || src.stageId == 6))
                )
            .ForMember(dest => dest.appliedBy,
                       opt => opt.MapFrom(src => src.appliedBy));

            CreateMap<mzlm_leave_package_dto, mzlm_leave_package>();
            #endregion

            #region Employee Details
            CreateMap<khidmat_guzaar, khidmat_guzaarDTO>()
                            .ForMember(
                                Dest => Dest.employee_academic_details,
                                Opt => Opt.MapFrom(src => src.employee_academic_details)
                            )
                            .ForMember(
                                Dest => Dest.employee_bank_detail,
                                Opt => Opt.MapFrom(src => src.employee_bank_details)
                            )
                            .ForMember(
                                Dest => Dest.employee_salary,
                                Opt => Opt.MapFrom(src => src.employee_salary)
                            )
                            .ForMember(
                                Dest => Dest.employee_dept_salaries,
                                Opt => Opt.MapFrom(src => src.employee_dept_salary)
                            );

            CreateMap<khidmat_guzaarDTO, khidmat_guzaar>();
            CreateMap<employee_dept_salary, employee_dept_salary_dto>();
            CreateMap<employee_dept_salary_dto, employee_dept_salary>();
            CreateMap<employee_salary_dto, employee_salary>();
            CreateMap<employee_salary, employee_salary_dto>();
            CreateMap<venue_dto, venue>();
            CreateMap<venue, venue_dto>();
            // Add other mappings as needed
            // map all remaining dto Mapping with dbmodel eqevalet madels

            CreateMap<branch_user, branch_user_dto>();
            CreateMap<employee_academic_details, employee_academic_detail_dto>();
            CreateMap<employee_bank_details, employee_bank_detail_dto>();
            CreateMap<employee_e_attendence, employee_e_attendence_dto>();
            CreateMap<employee_family_details, employee_family_detail_dto>();
            CreateMap<employee_khidmat_details, employee_khidmat_detail_dto>();
            CreateMap<employee_passport_details, employee_passport_detail_dto>();

            CreateMap<employee_passport_details, EmployeePassportDetailsModel>();
            CreateMap<EmployeePassportDetailsModel, employee_passport_details>();
            CreateMap<employee_salary, EmployeeSalaryDetailsModel>();
            CreateMap<EmployeeSalaryDetailsModel, employee_salary>();
            CreateMap<employee_dept_salary, EmployeeDeptSalaryModel>();
            CreateMap<EmployeeDeptSalaryModel, employee_dept_salary>();
            CreateMap<employee_dept_salary, EmployeeDeptSalaries>();
            CreateMap<EmployeeDeptSalaries, employee_dept_salary>();
            CreateMap<employee_academic_details, EmployeeAcademicDetailsModel>();
            CreateMap<EmployeeAcademicDetailsModel, employee_academic_details>();
            CreateMap<employee_bank_details, EmployeeBankDetailsModel>();
            CreateMap<EmployeeBankDetailsModel, employee_bank_details>();
            CreateMap<employee_khidmat_details, EmployeeKidmatDetailsModel>();
            CreateMap<EmployeeKidmatDetailsModel, employee_khidmat_details>();
            CreateMap<employee_family_details, EmployeeFamilyDetailsModel>();
            CreateMap<EmployeeFamilyDetailsModel, employee_family_details>();
            CreateMap<KGIdentitycards, kg_identitycards>();
            CreateMap<kg_identitycards, KGIdentitycards>();

            CreateMap<EmployeeBasicDetailsModel, khidmat_guzaar>();
            CreateMap<khidmat_guzaar, khidmat_guzaar>();
            CreateMap<khidmat_guzaar, EmployeeBasicDetailsModel>()
                .ForMember(
                    dest => dest.mauzeName,
                    opt => opt.MapFrom(src => src.mauzeNavigation.displayName)
                )
                .ForMember(
                    dest => dest.khidmatMauzeHouseType,
                    opt => opt.MapFrom(src => src.employee_khidmat_details.FirstOrDefault() != null ? src.employee_khidmat_details.FirstOrDefault().khdimatMauzeHouseType : "")
                )
                .ForMember(
                    dest => dest.khidmatMauzeHouseSatus,
                    opt => opt.MapFrom(src => src.employee_khidmat_details.FirstOrDefault() != null ? src.employee_khidmat_details.FirstOrDefault().khidmatMauzeHouseStatus : "")
                );

            //create map for employee basic details to khidmat_guzaar

            CreateMap<khidmat_guzaar, EmployeeModel>()
                .ForMember(
                    dest => dest.basicDetails,
                    opt => opt.MapFrom(src => src)
                ).ForMember(
                    dest => dest.passportDetails,
                    opt => opt.MapFrom(src => src.employee_passport_details.FirstOrDefault() != null ? src.employee_passport_details.FirstOrDefault() :
                               new employee_passport_details())
                ).ForMember(
                    destinationMember: dest => dest.khidmatDetails,
                    memberOptions: opt => opt.MapFrom(src => src.employee_khidmat_details.FirstOrDefault() != null ? src.employee_khidmat_details.FirstOrDefault() : new employee_khidmat_details())
                ).ForMember(
                    destinationMember: dest => dest.bankDetails,
                    memberOptions: opt => opt.MapFrom(src => src.employee_bank_details)
                ).ForMember(
                    destinationMember: dest => dest.academicDetails,
                    memberOptions: opt => opt.MapFrom(src => src.employee_academic_details)
                ).ForMember(
                    destinationMember: dest => dest.employeeSalary,
                    memberOptions: opt => opt.MapFrom(src => src.employee_salary)
                ).ForMember(
                    destinationMember: dest => dest.deptSalaries,
                    memberOptions: opt => opt.MapFrom(src => src.employee_dept_salary)
                );

            CreateMap<EmployeeModel, khidmat_guzaar>()
                .ForMember(dest => dest.employee_passport_details,
                            opt => opt.MapFrom(src => src.passportDetails))
                    .ForMember(
                    dest => dest.employee_khidmat_details,
                                        opt => opt.MapFrom(src => src.khidmatDetails)
                                                        ).ForMember(
                    dest => dest.employee_bank_details,
                                        opt => opt.MapFrom(src => src.bankDetails)
                                                        ).ForMember(
                    dest => dest.employee_academic_details,
                                        opt => opt.MapFrom(src => src.academicDetails)
                                                        ).ForMember(
                    dest => dest.employee_salary,
                                        opt => opt.MapFrom(src => src.employeeSalary)
                                                        ).ForMember(
                    dest => dest.employee_dept_salary,
                        opt => opt.MapFrom(src => src.deptSalaries)
                    ).ForMember(dest => dest.itsId, opt => opt.MapFrom(src => src.basicDetails.itsId))
                     .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.basicDetails.id))
                     .ForMember(dest => dest.photo, opt => opt.MapFrom(src => src.basicDetails.photo))
                     .ForMember(dest => dest.itsId, opt => opt.MapFrom(src => src.basicDetails.itsId))
                     .ForMember(dest => dest.fullName, opt => opt.MapFrom(src => src.basicDetails.fullName))
                     .ForMember(dest => dest.fullNameArabic, opt => opt.MapFrom(src => src.basicDetails.fullNameArabic))
                     .ForMember(dest => dest.c_codeMobile, opt => opt.MapFrom(src => src.basicDetails.c_codeMobile))
                     .ForMember(dest => dest.mobileNo, opt => opt.MapFrom(src => src.basicDetails.mobileNo))
                     .ForMember(dest => dest.c_codeWhatsapp, opt => opt.MapFrom(src => src.basicDetails.c_codeWhatsapp))
                     .ForMember(dest => dest.whatsappNo, opt => opt.MapFrom(src => src.basicDetails.whatsappNo))
                     .ForMember(dest => dest.emailAddress, opt => opt.MapFrom(src => src.basicDetails.emailAddress))
                     .ForMember(dest => dest.officialEmailAddress, opt => opt.MapFrom(src => src.basicDetails.officialEmailAddress))
                     .ForMember(dest => dest.watan, opt => opt.MapFrom(src => src.basicDetails.watan))
                     .ForMember(dest => dest.watanArabic, opt => opt.MapFrom(src => src.basicDetails.watanArabic))
                     .ForMember(dest => dest.watanAdress, opt => opt.MapFrom(src => src.basicDetails.watanAdress))
                     .ForMember(dest => dest.muqam, opt => opt.MapFrom(src => src.basicDetails.muqam))
                     .ForMember(dest => dest.muqamArabic, opt => opt.MapFrom(src => src.basicDetails.muqamArabic))
                     .ForMember(dest => dest.dojGregorian, opt => opt.MapFrom(src => src.basicDetails.dojGregorian))
                     .ForMember(dest => dest.dojHijri, opt => opt.MapFrom(src => src.basicDetails.dojHijri))
                     .ForMember(dest => dest.dobGregorian, opt => opt.MapFrom(src => src.basicDetails.dobGregorian))
                     .ForMember(dest => dest.dobHijri, opt => opt.MapFrom(src => src.basicDetails.dobHijri))
                     .ForMember(dest => dest.bloodGroup, opt => opt.MapFrom(src => src.basicDetails.bloodGroup))
                     .ForMember(dest => dest.currentAddress, opt => opt.MapFrom(src => src.basicDetails.currentAddress))
                     .ForMember(dest => dest.maritalStatus, opt => opt.MapFrom(src => src.basicDetails.maritalStatus))
                     .ForMember(dest => dest.mafsuhiyatYear, opt => opt.MapFrom(src => src.basicDetails.mafsuhiyatYear))
                     .ForMember(dest => dest.activeStatus, opt => opt.MapFrom(src => src.basicDetails.activeStatus))
                     .ForMember(dest => dest.nationality, opt => opt.MapFrom(src => src.basicDetails.nationality))
                     .ForMember(dest => dest.its_idaras, opt => opt.MapFrom(src => src.basicDetails.its_idaras))
                     .ForMember(dest => dest.its_preferredIdara, opt => opt.MapFrom(src => src.basicDetails.its_preferredIdara))
                     .ForMember(dest => dest.mz_idara, opt => opt.MapFrom(src => src.basicDetails.mz_idara))
                     .ForMember(dest => dest.dawat_title, opt => opt.MapFrom(src => src.basicDetails.dawat_title))
                     .ForMember(dest => dest.jamaat, opt => opt.MapFrom(src => src.basicDetails.jamaat))
                     .ForMember(dest => dest.jamiat, opt => opt.MapFrom(src => src.basicDetails.jamiat))
                     .ForMember(dest => dest.age, opt => opt.MapFrom(src => src.basicDetails.age))
                     .ForMember(dest => dest.haddiyatYear, opt => opt.MapFrom(src => src.basicDetails.haddiyatYear))
                     .ForMember(dest => dest.domicileParent, opt => opt.MapFrom(src => src.basicDetails.domicileParent))
                     .ForMember(dest => dest.domicileAddressParents, opt => opt.MapFrom(src => src.basicDetails.domicileAddressParents))
                     .ForMember(dest => dest.personalHouseStatus, opt => opt.MapFrom(src => src.basicDetails.personalHouseStatus))
                     .ForMember(dest => dest.personalHouseType, opt => opt.MapFrom(src => src.basicDetails.personalHouseType))
                     .ForMember(dest => dest.personalHouseArea, opt => opt.MapFrom(src => src.basicDetails.personalHouseArea))
                     .ForMember(dest => dest.personalHouseAddress, opt => opt.MapFrom(src => src.basicDetails.personalHouseAddress))
                     .ForMember(dest => dest.photoBase64, opt => opt.MapFrom(src => src.basicDetails.photoBase64))
                     .ForMember(dest => dest.employeeType, opt => opt.MapFrom(src => src.basicDetails.employeeType))
                     .ForMember(dest => dest.RfId, opt => opt.MapFrom(src => src.basicDetails.RfId))
                     .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.basicDetails.CreatedOn))
                     .ForMember(dest => dest.UpdatedOn, opt => opt.MapFrom(src => src.basicDetails.UpdatedOn))
                     .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.basicDetails.CreatedBy))
                     .ForMember(dest => dest.mauze, opt => opt.MapFrom(src => src.basicDetails.mauze));

            CreateMap<EmployeeSelfAssessmentModel, kg_self_assessment>();
            CreateMap<kg_self_assessment, EmployeeSelfAssessmentModel>();

            CreateMap<wafd_languageproficiency, WafdLanguageProficiency>();
            CreateMap<WafdLanguageProficiency, wafd_languageproficiency>();
            #endregion

            #region Finance

            CreateMap<mz_expense_vendor_master, VendorMasterModel>();
            CreateMap<VendorMasterModel, mz_expense_vendor_master>();

            #endregion


            #region Salary
            CreateMap<salary_allocation_gegorian, SalaryAllocation>()
                .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.its.fullName))
                .ForMember(dest => dest.phoroUrl, opt => opt.MapFrom(src => src.its.photo))
                .ForMember(dest => dest.photoBase64, opt => opt.MapFrom(src => src.its.photoBase64))
                .ForMember(dest => dest.employeeType, opt => opt.MapFrom(src => src.its.employeeType))
                .ForMember(dest => dest.mzIdara, opt => opt.MapFrom(src => src.its.mz_idara))
                .ForMember(dest => dest.designation, opt => opt.MapFrom(src => src.its.designation))
                .ForMember(dest => dest.workType, opt => opt.MapFrom(src => src.its.workType))
                .ForMember(dest => dest.age, opt => opt.MapFrom(src => src.its.age))
                .ForMember(dest => dest.category, opt => opt.MapFrom(src => src.its.employee_academic_details.category))
                .ForMember(dest => dest.farigDarajah, opt => opt.MapFrom(src => src.its.employee_academic_details.farigDarajah))
                .ForMember(dest => dest.batchId, opt => opt.MapFrom(src => src.its.employee_academic_details.batchId))
                .ForMember(dest => dest.accountName, opt => opt.MapFrom<BankAccountNameResolver>())
                .ForMember(dest => dest.isHijri, opt => opt.MapFrom(src => false));

            CreateMap<salary_allocation_hijri, SalaryAllocation>()
                .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.its.fullName))
                .ForMember(dest => dest.phoroUrl, opt => opt.MapFrom(src => src.its.photo))
                .ForMember(dest => dest.photoBase64, opt => opt.MapFrom(src => src.its.photoBase64))
                .ForMember(dest => dest.employeeType, opt => opt.MapFrom(src => src.its.employeeType))
                .ForMember(dest => dest.mzIdara, opt => opt.MapFrom(src => src.its.mz_idara))
                .ForMember(dest => dest.designation, opt => opt.MapFrom(src => src.its.designation))
                .ForMember(dest => dest.workType, opt => opt.MapFrom(src => src.its.workType))
                .ForMember(dest => dest.age, opt => opt.MapFrom(src => src.its.age))
                .ForMember(dest => dest.category, opt => opt.MapFrom(src => src.its.employee_academic_details.category))
                .ForMember(dest => dest.farigDarajah, opt => opt.MapFrom(src => src.its.employee_academic_details.farigDarajah))
                .ForMember(dest => dest.batchId, opt => opt.MapFrom(src => src.its.employee_academic_details.batchId))
                .ForMember(dest => dest.accountName, opt => opt.MapFrom<BankAccountNameResolver>())
                .ForMember(dest => dest.isHijri, opt => opt.MapFrom(src => true));

            CreateMap<SalaryAllocation, salary_allocation_gegorian>();
            CreateMap<SalaryAllocation, salary_allocation_hijri>();

            CreateMap<salary_generation_gegorgian, SalaryGeneration>()
                .ForMember(dest => dest.departmentName, opt => opt.MapFrom(src => src.deptVenue.deptName))
                .ForMember(dest => dest.venueName, opt => opt.MapFrom(src => src.deptVenue.venueName))
                .ForMember(dest => dest.salaryType, opt => opt.MapFrom(src => src.salaryTypeNavigation.Name))
                .ForMember(dest => dest.paymentDate, opt => opt.MapFrom(src => src.allocation.paymentDate))
                .ForMember(dest => dest.isHijri, opt => opt.MapFrom(src => false));

            CreateMap<salary_generation_hijri, SalaryGeneration>()
                .ForMember(dest => dest.venueName, opt => opt.MapFrom(src => src.deptVenue.venueName))
                .ForMember(dest => dest.departmentName, opt => opt.MapFrom(src => src.deptVenue.deptName))
                .ForMember(dest => dest.salaryType, opt => opt.MapFrom(src => src.salaryTypeNavigation.Name))
                .ForMember(dest => dest.paymentDate, opt => opt.MapFrom(src => src.allocation.paymentDate))
                .ForMember(dest => dest.isHijri, opt => opt.MapFrom(src => true));

            CreateMap<SalaryGeneration, salary_generation_gegorgian>();
            CreateMap<SalaryGeneration, salary_generation_hijri>();
            #endregion

            CreateMap<mz_kg_wajebaat_araz, WajebaatArazModel>();
            CreateMap<WajebaatArazModel, mz_kg_wajebaat_araz>();

            #region Training
            CreateMap<TrainingClassSubjectTeacherModel, training_class_subject_teacher>();
            CreateMap<training_class_subject_teacher, TrainingClassSubjectTeacherModel>()
            .ForMember(dest => dest.className, opt => opt.MapFrom(src => src._class.className))
            .ForMember(dest => dest.subjectName, opt => opt.MapFrom(src => src.subject.name))
            .ForMember(dest => dest.teacherName, opt => opt.MapFrom(src => src.teacherITSNavigation.fullName));


            CreateMap<TrainingStudentQuestionareModel, training_student_subject_marksheet>();

            CreateMap<training_student_subject_marksheet, TrainingStudentQuestionareModel>()
                .ForMember(dest => dest.studentName, opt => opt.MapFrom(src => src.studentITSNavigation.fullName))
                .ForMember(dest => dest.studentMauze, opt => opt.MapFrom(src => src.studentITSNavigation.mauzeNavigation.displayName ?? ""))
                .ForMember(dest => dest.hijriStart, opt => opt.MapFrom(src => src.startDate))
                .ForMember(dest => dest.hijriEnd, opt => opt.MapFrom(src => src.endDate))
                .ForMember(dest => dest.hijriMonth, opt => opt.MapFrom(src => src.startDate.Month))
                .ForMember(dest => dest.subject, opt => opt.MapFrom(src => new TrainingSubjectModel
                {
                    name = src.cst.subject.name ?? "",
                    id = src.cst.subjectId,
                    deletable = false,
                    wheightage = src.cst.subject.outOf,
                    status = src.cst.subject.status ?? "",
                }))
                .ForMember(dest => dest.teacher, opt => opt.MapFrom(src => new trainingCandidate
                {
                    name = src.cst.teacherITSNavigation.designation == "Idara Office" ? "Idara Office" : src.cst.teacherITSNavigation.fullName ?? "",
                    maqaraatAvg = 0.0,
                    courseCount = 0,
                    courseNames = "",
                    farigDarajah = 0,
                    farigYear = 0,
                    itsId = src.cst.teacherITSNavigation.designation == "Idara Office" ? 0 : src.cst.teacherITSNavigation.itsId,
                    contactNum = src.cst.teacherITSNavigation.designation == "Idara Office" ? "Not Available" : src.cst.teacherITSNavigation.mobileNo,
                    email = src.cst.teacherITSNavigation.designation == "Idara Office" ? "training@mahadalzahra.com" : src.cst.teacherITSNavigation.officialEmailAddress,
                    mauze = src.cst.teacherITSNavigation.mauzeNavigation.displayName
                }))
                .ForMember(dest => dest.qustionare, opt => opt.MapFrom(src => src.cst.subject.qustionare ?? ""));

            CreateMap<training_student_subject_marksheet, trainingCandidate>();
            CreateMap<trainingCandidate, training_student_subject_marksheet>();

            CreateMap<training_class_student, trainingCandidate>()
                .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.studentITSNavigation.fullName))
                  .ForMember(dest => dest.itsId, opt => opt.MapFrom(src => src.studentITSNavigation.itsId))
                  .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.studentITSNavigation.officialEmailAddress))
                  .ForMember(dest => dest.contactNum, opt => opt.MapFrom(src => src.studentITSNavigation.mobileNo))
                  .ForMember(dest => dest.mauze, opt => opt.MapFrom(src => src.studentITSNavigation.mauzeNavigation.displayName))
                  .ForMember(dest => dest.darajah, opt => opt.MapFrom(src => src._class.className))
                  .ForMember(dest => dest.farigDarajah, opt => opt.MapFrom(src => src.studentITSNavigation.employee_academic_details != null ? src.studentITSNavigation.employee_academic_details.farigDarajah : 0))
                  .ForMember(dest => dest.farigYear, opt => opt.MapFrom(src => src.studentITSNavigation.employee_academic_details != null ? src.studentITSNavigation.employee_academic_details.farigYear : 0));

            CreateMap<trainingCandidate, training_class_student>();

            CreateMap<trainingCandidate, khidmat_guzaar>();
            CreateMap<khidmat_guzaar, trainingCandidate>()
                .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.designation == "Idara Office" ? "Idara Office" : src.fullName))
                .ForMember(dest => dest.itsId, opt => opt.MapFrom(src => src.designation == "Idara Office" ? 0 : src.itsId))
                .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.designation == "Idara Office" ? "training@mahadalzahra.com" : src.officialEmailAddress))
                .ForMember(dest => dest.contactNum, opt => opt.MapFrom(src => src.designation == "Idara Office" ? "Not Available" : src.mobileNo))
                .ForMember(dest => dest.mauze, opt => opt.MapFrom(src => src.mauzeNavigation.displayName))
                .ForMember(dest => dest.farigDarajah, opt => opt.MapFrom(src => src.employee_academic_details != null ? src.employee_academic_details.farigDarajah : 0))
                .ForMember(dest => dest.farigYear, opt => opt.MapFrom(src => src.employee_academic_details != null ? src.employee_academic_details.farigYear : 0));
            #endregion

            #region reports
            CreateMap<khidmat_guzaar, Wafd_ul_huffaz_ExportModel>();
            CreateMap<Wafd_ul_huffaz_ExportModel, khidmat_guzaar>();

            CreateMap<payroll_salary_packages, PackagePayrollModel>()
                .ForMember(dest => dest.totalPaymentAmount, opt => opt.MapFrom(src => src.amount));
            CreateMap<PackagePayrollModel, payroll_salary_packages>();

            CreateMap<List<kg_faimalydetails_its>, List<EmployeeFamilyMembersModel>>()
           .ConvertUsing(new FamilyDetailsToListConverter());

            CreateMap<userdeptassociation, UserDeptAssociationModel>();
            CreateMap<UserDeptAssociationModel, userdeptassociation>();
            #endregion
        }
        private string FormatHijriDate(DateOnly startDate)
        {
            var startDateModel = helperService.getHijriDate(startDate);
            return $"{startDateModel.hijDay}-{startDateModel.hijMonth}-{startDateModel.hijYear}";
        }
    }

    public class FamilyDetailsToListConverter : ITypeConverter<List<kg_faimalydetails_its>, List<EmployeeFamilyMembersModel>>
    {
        public List<EmployeeFamilyMembersModel> Convert(List<kg_faimalydetails_its> source, List<EmployeeFamilyMembersModel> destination, ResolutionContext context)
        {
            destination = new List<EmployeeFamilyMembersModel>();

            // First, map and sort all family members except children
            var familyMembers = source
                .Where(member => member.relation != "Son" && member.relation != "Daughter")
                .Select(member => new EmployeeFamilyMembersModel
                {
                    // other properties mappings here
                    relationTypeId = GetRelationTypeId(member.relation),
                    name = member.name,
                    age = int.Parse(member.age),
                    relation = member.relation,
                    bloodGroup = member.bloodGroup,
                    dob = DateOnly.FromDateTime(member.dob ?? DateTime.MinValue),
                    hifzStatus = member.hifzStatus,
                    idara = member.idara,
                    itsId = member.itsId ?? 0,
                    jamaat = member.jamaat,
                    id = member.id,
                    nationality = member.nationality,
                    occupation = member.occupation
                }).ToList();

            // Now handle the children, sort by BirthOrder, and assign incremental relationTypeId starting from 3
            var children = source
                .Where(member => member.relation == "Son" || member.relation == "Daughter")
                .OrderByDescending(member => int.Parse(member.age))
                .Select((member, index) => new EmployeeFamilyMembersModel
                {
                    relationTypeId = index + 3,
                    name = member.name,
                    age = int.Parse(member.age),
                    relation = member.relation,
                    bloodGroup = member.bloodGroup,
                    dob = DateOnly.FromDateTime(member.dob ?? DateTime.MinValue),
                    hifzStatus = member.hifzStatus,
                    idara = member.idara,
                    itsId = member.itsId ?? 0,
                    jamaat = member.jamaat,
                    id = member.id,
                    nationality = member.nationality,
                    occupation = member.occupation
                }).ToList();

            // Combine lists
            destination.AddRange(familyMembers);
            destination.AddRange(children);

            return destination;
        }

        private int GetRelationTypeId(string relation)
        {
            switch (relation)
            {
                case "Self":
                    return 1;
                case "Wife":
                case "Husband":
                    return 2;
                case "Father":
                case "Mother":
                    return 0;
                default:
                    return 0; // Default case, might not be used if all cases are covered
            }
        }
    }

    public class WorkingMinResolver : IValueResolver<azwaaj_minentry, azwaaj_minentry_dto, float?>
    {
        public float? Resolve(azwaaj_minentry source, azwaaj_minentry_dto destination, float? destMember, ResolutionContext context)
        {
            if (source.its != null && source.its.employee_dept_salary != null)
            {
                var employeeDeptSalary = source.its.employee_dept_salary.FirstOrDefault(x => x.deptVenueId == source.deptVenueId && x.salaryTypeId == source.policyId);
                if (employeeDeptSalary != null)
                {
                    return employeeDeptSalary.workingMin ?? 0;
                }
            }
            return 0;
        }
    }

    public class BankAccountNameResolver : IValueResolver<object, SalaryAllocation, string>
    {
        public string Resolve(object source, SalaryAllocation destination, string destMember, ResolutionContext context)
        {
            if (source is salary_allocation_hijri hijri)
            {
                // Handle salary_allocation_hijri
                return hijri.its.employee_bank_details.FirstOrDefault()?.bankAccountName ?? "";
            }
            else if (source is salary_allocation_gegorian gregorian)
            {
                // Handle salary_allocation_gregorian
                return gregorian.its.employee_bank_details.FirstOrDefault()?.bankAccountName ?? "";
            }
            return "";
        }
    }
}
