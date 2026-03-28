using mahadalzahrawebapi.Controllers;
using mahadalzahrawebapi.GenericModels;
using mahadalzahrawebapi.Mappings;
using mahadalzahrawebapi.Models;
using Microsoft.EntityFrameworkCore;

namespace mahadalzahrawebapi.Services
{
    public interface ISalaryService
    {
        int getProfessionalTax(int? salaryPlusAllowance);

        int applicableSalary(List<employee_dept_salary> eds);
        int applicableSalary(List<EmployeeDeptSalaryModel> eds);

        Task<int> getSalaryExpenseAmount(int deptVenueId, int financialYear, string empType);

        List<EmployeeDeptSalaryModel> costPerDept(
            List<EmployeeDeptSalaryModel> eds,
            bool onlyFixed,
            float cost = -1
        );
    }

    public class SalaryService : ISalaryService
    {
        private static readonly string WafdAlHuffaz = "WafdAlHuffaz";
        private static readonly string MahadAlZahra_KHDGZ = "MAHADALZAHRA KHDGZ";

        private readonly mzdbContext _context;

        public SalaryService(mzdbContext context)
        {
            _context = context;
        }

        public SalaryService()
        {
            _context = null;
        }

        public int getProfessionalTax(int? salary)
        {
            if (salary == null)
            {
                salary = 0;
            }
            int tax = 0;

            //if (salary < 5999)
            //{
            //    tax = 0;
            //}
            //else if (salary >= 6000 && salary <= 8999)
            //{
            //    tax = 80;
            //}
            //else if (salary >= 9000 && salary <= 11999)
            //{
            //    tax = 150;
            //}
            //else if (salary >= 12000)
            //{
            //    tax = 200;
            //}

            if (salary > 12000)
            {
                tax = 200;
            }

            return tax;
        }

        public List<salary_allocation_gegorian> GenerateSalaries(
            List<khidmat_guzaar> employees,
            List<azwaaj_minentry> attendance,
            PayrollProcessing payrollProcessing,
            AuthUser authUser
        )
        {
            List<salary_allocation_gegorian> salaryAllocations =
                new List<salary_allocation_gegorian>();

            int daysConsidered = (payrollProcessing.to - payrollProcessing.from).Days + 1;

            foreach (khidmat_guzaar employee in employees)
            {
                salary_allocation_gegorian salaryAllocation = CalculateSalary(
                    employee,
                    attendance,
                    payrollProcessing,
                    authUser,
                    daysConsidered
                );
                salaryAllocations.Add(salaryAllocation);
            }

            return salaryAllocations;
        }

        private salary_allocation_gegorian CalculateSalary(
            khidmat_guzaar employee,
            List<azwaaj_minentry> attendance,
            PayrollProcessing payrollProcessing,
            AuthUser authUser,
            int daysConsidered
        )
        {
            // Initialize salary allocation for employee
            salary_allocation_gegorian salaryAllocation = new salary_allocation_gegorian()
            {
                itsId = employee.itsId,
                createdBy = authUser.Name,
                createdOn = DateTime.Now,
                salary_generation_gegorgian = new List<salary_generation_gegorgian>(),
                timeDelta = 0,
                fmbAllowance = employee.employee_salary.fmbAllowance ?? 0,
                mumbaiAllowance = employee.employee_salary.mumbaiAllowance ?? 0,
                rentAllowance = employee.employee_salary.rentAllowance ?? 0,
                marriageAllowance = employee.employee_salary.marriageAllowance ?? 0,
                qardanHasanahNonRefundable = employee.employee_salary.qardanHasanahNonRefundable ?? 0,
                qardanHasanahRefundable = employee.employee_salary.qardanHasanahRefundable ?? 0,
                sabeel = employee.employee_salary.sabeel ?? 0,
                marafiqKhairiyah = employee.employee_salary.marafiqKhairiyah ?? 0,
                convenienceAllowance = employee.employee_salary.conveyanceAllowance ?? 0,
                currency = employee.employee_salary.currency,
                systemRemarks = "",
                salaryFrom = payrollProcessing.from,
                salaryTo = payrollProcessing.to,
                month = payrollProcessing.to.Month,
                year = payrollProcessing.to.Year,
                netEarnings = 0,
                professionTax = 0,
                ctc = 0,
                salary = 0,
                overtime = 0,
                arrears = employee.employee_salary.arrears,
                shortfall = 0,
                withHoldings = employee.employee_salary.withHoldings,
                lessDeduction = 0,
                bqhs = 0,
                dayDelta = 0,
                extraAllowance = 0,
                installmentDeduction_Qardan = 0,
                mohammedi_qardanHasanah = 0,
                fmbDeduction = 0,
                husaini_qardanHasanah = 0,
                qardanHasanah = 0,
                tds = 0
            };

            // Fetch attendance for this employee
            List<azwaaj_minentry> employeeAttendance = attendance
                .Where(x => x.itsid == employee.itsId)
                .ToList();

            // Salary calculation logic based on attendance and salary type
            CalculateSalaryForAttendance(
                employee,
                employeeAttendance,
                payrollProcessing,
                salaryAllocation,
                daysConsidered
            );

            salaryAllocation.salary = CalculateGrossSalary(employee, salaryAllocation, employeeAttendance);
            // Calculate overtime, shortfall, and final net earnings
            CalculateOvertimeAndShortfall(employee, salaryAllocation, employeeAttendance, daysConsidered);

            // Calculate arrears for delta days
            CalculateArrears(employee, salaryAllocation, payrollProcessing);

            salaryAllocation.ctc = (int)(caculateCTC(salaryAllocation));
            salaryAllocation.professionTax = getProfessionalTax(salaryAllocation.ctc);

            salaryAllocation.netEarnings = netSalary(salaryAllocation);

            // Update CTC and net salary
            // UpdateNetSalaryAndCTC(salaryAllocation, employeeAttendance);

            // If CTC changes, redistribute the amount across departments
            RedistributeCTCAcrossDepartments(salaryAllocation, employeeAttendance);

            return salaryAllocation;
        }

        public int netSalary(salary_allocation_gegorian s)
        {
            var result = s.salary;
            result += s.convenienceAllowance ?? 0;
            result += s.marriageAllowance ?? 0;
            result += s.mumbaiAllowance ?? 0;
            result += s.rentAllowance ?? 0;
            result += s.arrears ?? 0;
            result += s.overtime ?? 0;
            result += s.fmbAllowance ?? 0;
            result -= s.tds ?? 0;
            result -= s.incomeTax ?? 0;
            result -= s.localTax ?? 0;
            result -= s.professionTax ?? 0;
            result -= s.sabeel ?? 0;
            result -= s.withHoldings ?? 0;
            result -= s.shortfall ?? 0;
            result -= s.qardanHasanahNonRefundable ?? 0;
            result -= s.qardanHasanahRefundable ?? 0;
            result -= s.marafiqKhairiyah ?? 0;
            return result;
        }

        public int netSalary(salary_allocation_hijri s)
        {
            var result = s.salary;
            result += s.convenienceAllowance ?? 0;
            result += s.marriageAllowance ?? 0;
            result += s.mumbaiAllowance ?? 0;
            result += s.rentAllowance ?? 0;
            result += s.arrears ?? 0;
            result += s.overtime ?? 0;
            result += s.fmbAllowance ?? 0;
            result -= s.tds ?? 0;
            result -= s.incomeTax ?? 0;
            result -= s.localTax ?? 0;
            result -= s.professionTax ?? 0;
            result -= s.sabeel ?? 0;
            result -= s.withHoldings ?? 0;
            result -= s.shortfall ?? 0;
            result -= s.qardanHasanahNonRefundable ?? 0;
            result -= s.qardanHasanahRefundable ?? 0;
            result -= s.marafiqKhairiyah ?? 0;
            return result;
        }

        private void RedistributeCTCAcrossDepartments(salary_allocation_gegorian salaryAllocation, List<azwaaj_minentry> employeeAttendance)
        {
            // Group attendance by department
            var groupedByDepartment = employeeAttendance.GroupBy(x => x.deptVenueId).ToList();

            // Calculate the total working minutes for proportional distribution
            int totalWorkedMinutes = employeeAttendance.Sum(x => x.min ?? 0);

            // Redistribute the CTC across departments
            foreach (var group in groupedByDepartment)
            {
                int departmentWorkedMinutes = group.Sum(x => x.min ?? 0);
                float departmentSharePercentage = (float)departmentWorkedMinutes / totalWorkedMinutes;

                // Create or update the salary_generation_gegorgian for each department
                salary_generation_gegorgian salaryGeneration = salaryAllocation.salary_generation_gegorgian
                    .FirstOrDefault(x => x.deptVenueId == group.Key) ?? new salary_generation_gegorgian();

                // Allocate proportional CTC to this department
                salaryGeneration.netSalary = (int)Math.Round(departmentSharePercentage * salaryAllocation.ctc);

                // Add or update this department's salary generation entry in the list
                if (!salaryAllocation.salary_generation_gegorgian.Contains(salaryGeneration))
                {
                    salaryAllocation.salary_generation_gegorgian.Add(salaryGeneration);
                }
            }
        }

        private void CalculateSalaryForAttendance(khidmat_guzaar employee, List<azwaaj_minentry> attendance, PayrollProcessing payrollProcessing, salary_allocation_gegorian salaryAllocation, int daysConsidered)
        {
            // Grouping attendance by deptVenueId and policyId
            var groupedAttendance = attendance.GroupBy(x => new { x.deptVenueId, x.policyId });

            foreach (var group in groupedAttendance)
            {
                // Initialize salary generation entry for each grouped attendance
                salary_generation_gegorgian salaryGeneration = new salary_generation_gegorgian()
                {
                    itsId = employee.itsId,
                    deptVenueId = group.Key.deptVenueId ?? 0,
                    salaryType = group.Key.policyId ?? 0,
                    createdBy = salaryAllocation.createdBy,
                    createdOn = salaryAllocation.createdOn,
                    year = payrollProcessing.to.Year,
                    month = payrollProcessing.to.Month,
                    quantity = group.Sum(x => x.min ?? 0) // Total working minutes for this dept and policy
                };

                // Handle salary calculation differently depending on the policy
                switch (group.Key.policyId)
                {
                    case 1: // Fixed Salary (Policy ID - 1)
                        // Fixed salary is assigned as the full month's salary
                        salaryGeneration.netSalary = employee.employee_salary.grossSalary;
                        break;

                    case 2: // Hourly/Minute-based salary (Policy ID - 2)
                        // Calculate based on the rate and working minutes
                        float hourlyRate = group.Average(x => x.rate ?? 0);
                        salaryGeneration.netSalary = (int)Math.Round(hourlyRate * salaryGeneration.quantity);
                        break;

                    case 3: // Period-based Salary (Policy ID - 3)
                        // Period-based salary is calculated as a flat amount for the full period (no working minutes consideration)
                        float periodRate = group.Average(x => x.rate ?? 0);
                        salaryGeneration.netSalary = (int)Math.Round(periodRate * salaryGeneration.quantity);
                        break;

                    case 4: // Daily-based salary (Policy ID - 4)
                        // Daily rate, calculate based on the number of days worked
                        float dailyRate = group.Average(x => x.rate ?? 0);
                        int daysWorked = group.Select(x => x.date).Distinct().Count();
                        salaryGeneration.netSalary = (int)Math.Round(dailyRate * daysWorked);
                        break;

                    default:
                        // Handle other policies if required
                        salaryGeneration.netSalary = 0; // No rate means no salary
                        break;
                }

                // Add the calculated salary generation entry to the salary allocation
                salaryAllocation.salary_generation_gegorgian.Add(salaryGeneration);
            }
        }

        private void CalculateOvertimeAndShortfall(khidmat_guzaar employee, salary_allocation_gegorian salaryAllocation, List<azwaaj_minentry> attendance, int daysConsidered)
        {
            // Calculate total worked minutes and allotted minutes from attendance records (azwaaj_minentry)
            int totalWorkedMinutes = attendance.Where(x => x.itsid == employee.itsId)
                                              .Sum(x => x.min ?? 0);

            int totalAllottedMinutes = attendance.Where(x => x.itsid == employee.itsId)
                                                .Sum(x => x.allotedMin ?? 0);

            float averageGrossSalary = attendance.Where(x => x.itsid == employee.itsId && x.policyId == 1)
                                               .Select(x => x.rate)
                                               .Average(x => x ?? 0);

            // Calculate the overall time delta (difference between worked and allotted minutes)
            salaryAllocation.timeDelta = totalWorkedMinutes - totalAllottedMinutes;

            // If worked minutes are less than allotted, calculate shortfall
            if (totalWorkedMinutes < totalAllottedMinutes)
            {
                int shortfallMinutes = totalAllottedMinutes - totalWorkedMinutes;

                // Calculate the actual shortfall amount based on the employee's rate
                float perMinRate = (float)employee.employee_salary.grossSalary / totalAllottedMinutes; // Calculate per-minute rate based on allotted minutes
                float shortfallAmount = shortfallMinutes * perMinRate; // Calculate the actual shortfall amount

                // Save the actual shortfall amount in salaryAllocation.shortfall
                salaryAllocation.shortfall = (int)Math.Round(shortfallAmount);

                // Log the shortfall in system remarks
                salaryAllocation.systemRemarks += $"| Shortfall: {shortfallMinutes} minutes, deducted {salaryAllocation.shortfall} from wazifa";
            }

            // If worked minutes exceed allotted, calculate overtime
            else if (totalWorkedMinutes > totalAllottedMinutes)
            {
                int overtimeMinutes = totalWorkedMinutes - totalAllottedMinutes;
                float overtimePay = 0;

                // Determine if the employee's allotted time per day is less than 8 hours
                if (totalAllottedMinutes / daysConsidered < 480)
                {
                    // Overtime up to 8 hours is paid at 1x rate, beyond 8 hours at 1.5x
                    int thresholdMinutes = 480 * daysConsidered; // 8 hours/day = 480 minutes
                    if (overtimeMinutes <= thresholdMinutes)
                    {
                        // Overtime within normal 8 hours is paid at 1x rate
                        overtimePay = overtimeMinutes * averageGrossSalary;
                    }
                    else
                    {
                        // First 8 hours are paid at 1x, additional overtime is paid at 1.5x rate
                        int regularOvertime = thresholdMinutes;
                        int extraOvertime = overtimeMinutes - regularOvertime;
                        overtimePay = (regularOvertime * averageGrossSalary) +
                                      (extraOvertime * 1.5f * averageGrossSalary);
                    }
                }
                else
                {
                    // If allotted more than 8 hours/day, all overtime is paid at 1.5x rate
                    overtimePay = overtimeMinutes * 1.5f * averageGrossSalary;
                }

                // Assign overtime pay
                salaryAllocation.overtime = (int)Math.Round(overtimePay);

                // Log the overtime details in system remarks
                salaryAllocation.systemRemarks += $"| Overtime: {overtimeMinutes} minutes, paid {salaryAllocation.overtime} amount at overtime rates";
            }
        }

        private int CalculateGrossSalary(khidmat_guzaar employee, salary_allocation_gegorian salaryAllocation, List<azwaaj_minentry> attendance)
        {

            // If the employee is on a fixed salary (Policy ID - 1), set the gross salary
            if (employee.workType == "Fixed")
            {
                return employee.employee_salary.grossSalary;
            }
            else if (employee.workType == "Time-based")
            {
                // If the employee is on a minute/hour-based salary, calculate the gross salary
                float salaryAmount = attendance.Where(x => x.policyId != 1).Sum(x => (float)(x.min ?? 0) * (x.rate ?? 0f));

                return (int)Math.Round(salaryAmount);
            }
            else
            {
                float salaryAmount = attendance.Where(x => x.policyId != 1).Sum(x => (float)(x.min ?? 0) * (x.rate ?? 0f));

                return (int)Math.Round(salaryAmount + employee.employee_salary.grossSalary);
            }

        }
        private void CalculateArrears(khidmat_guzaar employee, salary_allocation_gegorian salaryAllocation, PayrollProcessing payrollProcessing)
        {
            DateTime? lastSalaryDate = employee.employee_salary.lastSalaryDate;
            DateTime? dateOfJoining = employee.dojGregorian;

            // If last salary date is null, calculate arrears from the date of joining
            if (lastSalaryDate == null && dateOfJoining != null)
            {
                // Calculate the number of unpaid delta days (days for which arrears are due)
                int deltaDays = (payrollProcessing.from - dateOfJoining.Value).Days;
                if (deltaDays > 0)
                {
                    // Calculate the daily rate based on the employee's gross salary
                    float dailyRate = (float)employee.employee_salary.grossSalary / 30; // Assuming 30 days in a month for simplicity

                    // Calculate the arrears for the unpaid delta days
                    salaryAllocation.arrears = (int)Math.Round(dailyRate * deltaDays);

                    // Update system remarks to reflect the arrears calculation
                    salaryAllocation.systemRemarks += $"| Arrears: {deltaDays} unpaid days from date of joining.";
                }
            }
            // If last salary date is before the current period, calculate arrears from the last salary date
            else if (lastSalaryDate != null && lastSalaryDate < payrollProcessing.from)
            {
                // Calculate the number of delta days between the last salary date and the start of the current period
                int deltaDays = (payrollProcessing.from - lastSalaryDate.Value).Days;

                // Calculate the daily rate based on the employee's gross salary
                float dailyRate = (float)employee.employee_salary.grossSalary / 30; // Assuming 30 days in a month

                // Calculate the arrears for the delta days
                salaryAllocation.arrears = (int)Math.Round(dailyRate * deltaDays);

                // Update system remarks to reflect the arrears calculation
                salaryAllocation.systemRemarks += $"| Arrears: {deltaDays} unpaid days from last salary date.";
            }
        }
        private int CalculateNetEarnings(salary_allocation_gegorian salaryAllocation)
        {
            // Calculate final net earnings from salary allocation
            int netEarnings = salaryAllocation.salary_generation_gegorgian.Sum(x =>
                x.netSalary ?? 0
            );
            return netEarnings;
        }

        public salary_allocation_hijri CreateSalaryAllocationHijri(
            khidmat_guzaar employee,
            EmployeeSalaryModel salaryModel,
            DateTime fromEng,
            DateTime toEng,
            int numberOfDay,
            AuthUser authUser,
            PayrollProcessing payrollProcessing)
        {
            return new salary_allocation_hijri()
            {
                itsId = salaryModel.salaryDetails.itsId,
                createdBy = authUser.Name,
                salary = salaryModel.salaryDetails.grossSalary,
                bqhs = salaryModel.salaryDetails.bqhs,
                convenienceAllowance = salaryModel.salaryDetails.conveyanceAllowance ?? 0,
                createdOn = DateTime.UtcNow,
                ctc = 0, // CTC will be calculated later
                currency = employee.mauzeNavigation.currency,
                extraAllowance = salaryModel.salaryDetails.extraAllowance ?? 0,
                husaini_qardanHasanah = 0,
                installmentDeduction_Qardan = 0,
                lessDeduction = 0,
                marafiqKhairiyah = salaryModel.salaryDetails.marafiqKhairiyah ?? 0,
                marriageAllowance = salaryModel.salaryDetails.marriageAllowance ?? 0,
                mohammedi_qardanHasanah = 0,
                month = payrollProcessing.hMonth ?? 0,
                year = payrollProcessing.hYear ?? 0,
                mumbaiAllowance = 0,
                netEarnings = 0, // Net earnings will be calculated later
                professionTax = salaryModel.salaryDetails.professionTax ?? 0,
                qardanHasanah = 0,
                rentAllowance = salaryModel.salaryDetails.rentAllowance ?? 0,
                sabeel = salaryModel.salaryDetails.sabeel ?? 0,
                fmbAllowance = salaryModel.salaryDetails.fmbAllowance ?? 0,
                fmbDeduction = 0,
                salaryFrom = fromEng,
                salaryTo = toEng,
                arrears = salaryModel.salaryDetails.arrears ?? 0,
                dayDelta = 0,
                shortfall = 0,
                overtime = 0,
                timeDelta = 0,
                incomeTax = 0,
                systemRemarks = "", // Remarks will be updated during shortfall/arrears calculation
                localTax = 0,
                qardanHasanahNonRefundable = salaryModel.salaryDetails.qardanHasanahNonRefundable ?? 0,
                qardanHasanahRefundable = salaryModel.salaryDetails.qardanHasanahRefundable ?? 0,
                qismRemarks = "",
                tds = 0,
                withHoldings = salaryModel.salaryDetails.withHoldings ?? 0,
            };
        }

        public salary_allocation_hijri HandleShortfallHijri(
            salary_allocation_hijri sah,
            khidmat_guzaar employee,
            DateTime fromEng,
            int numberOfDay,
            PayrollProcessing payrollProcessing)
        {
            if (employee.employee_salary.lastSalaryDate != null && employee.employee_salary.lastSalaryDate > fromEng)
            {
                sah.salaryFrom = employee.employee_salary.lastSalaryDate?.AddDays(1) ?? payrollProcessing.from;

                // Calculate the number of days already paid
                int daysPaid = (int)Math.Round(((TimeSpan)(employee.employee_salary.lastSalaryDate - payrollProcessing.from)).TotalDays);

                // Calculate the shortfall based on the number of days already paid
                sah.shortfall = (sah.salary / numberOfDay) * daysPaid;
                sah.dayDelta = 0 - daysPaid;

                // Update remarks to include shortfall details
                sah.systemRemarks = $"Shortfall: Wazifa for {daysPaid} days has been allotted previously";
            }

            return sah;
        }

        public async Task<salary_allocation_hijri> HandleArrearsHijri(
            salary_allocation_hijri sah,
            khidmat_guzaar employee,
            DateTime fromEng,
            PayrollProcessing payrollProcessing,
            IEnumerable<venue> venues, int hijriDays)
        {
            if (employee.employee_salary.lastSalaryDate != null && employee.employee_salary.lastSalaryDate < fromEng.AddDays(-1))
            {
                // Fetch attendance to calculate unpaid days
                List<azwaaj_minentry> attendance = await _context.azwaaj_minentry.Where(
                    x => x.itsid == employee.itsId &&
                        x.date >= DateOnly.FromDateTime(employee.employee_salary.lastSalaryDate ?? DateTime.UtcNow) &&
                        x.date <= DateOnly.FromDateTime(payrollProcessing.from) &&
                        venues.Any(v => v.Id == x.deptVenue.venueId)
                ).Include(x => x.deptVenue).ToListAsync();

                // Group attendance by day and calculate the total unpaid days
                attendance = attendance.GroupBy(x => x.date).Select(x => x.FirstOrDefault()).ToList();
                int daysUnpaid = attendance.Count;

                // Calculate arrears for the unpaid days
                sah.salaryFrom = employee.employee_salary.lastSalaryDate?.AddDays(1) ?? payrollProcessing.from;
                sah.arrears += (sah.salary / hijriDays) * daysUnpaid;
                sah.dayDelta = daysUnpaid;

                // Update remarks to include arrears details
                sah.systemRemarks = $"Arrears: Wazifa for {daysUnpaid} days is pending since the last allocation date.";
            }

            return sah;
        }

        public async Task GenerateSalariesForDepartments(
            List<EmployeeDeptSalaries> toGenerate,
            salary_allocation_hijri sah,
            int grossSalary,
            AuthUser authUser,
            int ctc
        )
        {
            int netAmount = 0;
            int totalAllowances = sah.ctc - grossSalary;

            foreach (EmployeeDeptSalaries eds in toGenerate)
            {
                if (eds.deptVenueId != 17)
                {
                    // Calculate the percentage of total allowances to be distributed to this department
                    if (totalAllowances != 0 && grossSalary > 0)
                    {
                        float percent = ((eds.salaryAmount ?? 0) * 100) / grossSalary;
                        float partialAllowance = ((percent * totalAllowances) / 100);
                        eds.salaryAmount += partialAllowance;
                    }
                    else if (grossSalary == 0)
                    {
                        // If gross salary is 0, distribute allowances based on department working minutes
                        float totalMinutes = toGenerate.Sum(x => x.workingMin ?? 0);
                        float departmentMinutes = eds.workingMin ?? 0;
                        float allowanceShare = (departmentMinutes / totalMinutes) * totalAllowances;
                        eds.salaryAmount = allowanceShare;
                    }

                    netAmount += (int)Math.Round(eds.salaryAmount ?? 0);
                }

                // Generate salary entry for the department
                salary_generation_hijri sgh = new salary_generation_hijri()
                {
                    createdBy = authUser.Name,
                    itsId = eds.itsId,
                    salaryType = eds.salaryTypeId,
                    allocationId = sah.id,
                    deptVenueId = eds.deptVenueId,
                    month = sah.month,
                    year = sah.year,
                    createdOn = DateTime.Now,
                    netSalary = (int)Math.Round(eds.salaryAmount ?? 0),
                    quantity = eds.workingMin ?? 0,
                };
                sah.salary_generation_hijri.Add(sgh);
            }

            // Adjust for any discrepancies between CTC and the total net amount
            if ((sah.ctc - netAmount) != 0 && sah.ctc != 0 && netAmount != 0)
            {
                sah.salary_generation_hijri.Last().netSalary += (sah.ctc - netAmount);
            }

            await Task.CompletedTask;
        }

        public async Task<int> getSalaryExpenseAmount(
            int deptVenueId,
            int financialYear,
            string empType
        )
        {
            //using (var _context = new mzmanageEntities())
            //{
            DateTime fromD = new DateTime(financialYear, 4, 1);
            DateTime toD = new DateTime(financialYear + 1, 4, 1);

            DateRange dateRange = new DateRange(fromD, toD);

            return await getSalaryExpenseAmount(deptVenueId, dateRange, empType);

            //}
        }

        public async Task<int> getSalaryExpenseAmount(
            int deptVenueId,
            DateRange dateRange,
            string empType
        )
        {
            try
            {
                DateTime fromD = dateRange.FromDate ?? DateTime.Now.AddDays(-30).Date;
                DateTime toD = dateRange.ToDate ?? DateTime.Now;

                // Calculate expense for Hijri salary generation
                int hijriTotalExpense = await _context
                    .salary_generation_hijri.Where(x => x.deptVenueId == deptVenueId)
                    .Include(x => x.allocation)
                    .Include(x => x.its)
                    .Where(x =>
                        x.its != null
                        && x.allocation != null
                        && x.its.employeeType != null
                        && x.allocation.paymentDate != null
                    )
                    .Where(x =>
                        x.its.employeeType == empType
                        && x.allocation.paymentDate > fromD
                        && x.allocation.paymentDate <= toD
                    )
                    .SumAsync(x => x.netSalary ?? 0);

                // Calculate expense for Gregorian salary generation
                int gregorianTotalExpense = await _context
                    .salary_generation_gegorgian.Where(x => x.deptVenueId == deptVenueId)
                    .Include(x => x.allocation)
                    .Include(x => x.its)
                    .Where(x =>
                        x.its != null
                        && x.allocation != null
                        && x.its.employeeType != null
                        && x.allocation.paymentDate != null
                    )
                    .Where(x =>
                        x.its.employeeType == empType
                        && x.allocation.paymentDate > fromD
                        && x.allocation.paymentDate <= toD
                    )
                    .SumAsync(x => x.netSalary ?? 0);

                // Combine expenses from both salary calculations
                int totalExpense = hijriTotalExpense + gregorianTotalExpense;
                return totalExpense;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        // public int getSalaryExpenseAmount(int deptVenueId, DateRange dateRange, string empType)
        // {

        //     //using (var _context = new mzmanageEntities())
        //     //{
        //         DateTime fromD = dateRange.FromDate ?? DateTime.Now.AddDays(-30).Date;
        //         DateTime toD = dateRange.ToDate ?? DateTime.Now;

        //         int totalExpense = 0;

        //         List<salary_generation_hijri> hijriSalaryGeneration = new List<salary_generation_hijri>();

        //         hijriSalaryGeneration = (from sg in _context.salary_generation_hijri
        //                                  where sg.deptVenueId == deptVenueId
        //                                  join sag in _context.salary_allocation_hijri on sg.allocationId equals sag.id
        //                                  where sag.paymentDate > fromD && sag.paymentDate <= toD && sag.packageId != null
        //                                  join kh in _context.khidmat_guzaar on sg.itsId equals kh.itsId
        //                                  where kh.employeeType == empType
        //                                  select sg).ToList();

        //         totalExpense = hijriSalaryGeneration.Sum(x => (x.netSalary ?? 0));

        //         List<salary_generation_gegorgian> engSalaryGeneration = new List<salary_generation_gegorgian>();

        //         engSalaryGeneration = (from sg in _context.salary_generation_gegorgian
        //                                where sg.deptVenueId == deptVenueId
        //                                join sag in _context.salary_allocation_gegorian on sg.allocationId equals sag.id
        //                                where sag.paymentDate > fromD && sag.paymentDate <= toD && sag.packageId != null
        //                                join kh in _context.khidmat_guzaar on sg.itsId equals kh.itsId
        //                                where kh.employeeType == empType
        //                                select sg).ToList();

        //         totalExpense += engSalaryGeneration.Sum(x => (x.netSalary ?? 0));

        //         return totalExpense;

        //     //}

        // }

        public List<EmployeeDeptSalaryModel> costPerDept(
            List<EmployeeDeptSalaryModel> eds,
            bool onlyFixed,
            float cost = -1
        )
        {
            float rateAmount = 0;
            if (eds.Count == 0)
            {
                return eds;
            }

            List<EmployeeDeptSalaryModel> edsl = eds.Where(x => x.hasSalary == true).ToList();
            if (onlyFixed)
            {
                edsl = edsl.Where(x => x.salaryTypeId == 1).ToList();
            }

            if (cost == -1)
            {
                cost = edsl.Last().salaryAmount ?? 0;
            }

            foreach (EmployeeDeptSalaryModel ed in edsl)
            {
                if (ed.workingMin != 0)
                {
                    rateAmount += (ed.workingMin ?? 0);
                }
            }
            ;
            if (rateAmount == 0)
            {
                return eds;
            }
            var perMinCost = cost / rateAmount;
            float tdistributed = 0;

            foreach (EmployeeDeptSalaryModel ed in edsl)
            {
                var tempCal = Math.Round((ed.workingMin ?? 0) * perMinCost);
                tdistributed += (float)tempCal;
                ed.salaryAmount = (float)tempCal;
            }
            ;

            if (tdistributed == cost)
            {
                return eds;
            }

            edsl.OrderByDescending(x => x.salaryAmount).ToList().First().salaryAmount += (
                cost - tdistributed
            );

            return eds;
        }

        public int applicableSalary(List<employee_dept_salary> eds)
        {
            float tSalary = 0;
            eds.ToList().ForEach(x => tSalary += (x.salaryAmount ?? 0));

            return (int)Math.Round(tSalary);
        }

        public int applicableSalary(List<salary_generation_gegorgian> eds)
        {
            float tSalary = 0;
            eds.ToList().ForEach(x => tSalary += (x.netSalary ?? 0));

            return (int)Math.Round(tSalary);
        }

        public int applicableSalary(List<EmployeeDeptSalaryModel> eds)
        {
            float tSalary = 0;
            eds.ToList()
                .ForEach(x =>
                {
                    if (x.deptVenueId != 17)
                    {
                        tSalary = (x.salaryAmount ?? 0) + tSalary;
                    }
                });
            return (int)Math.Round(tSalary);
        }

        public int applicableSalary(List<EmployeeDeptSalaries> eds)
        {
            float tSalary = 0;
            eds.ToList()
                .ForEach(x =>
                {
                    if (x.deptVenueId != 17)
                    {
                        tSalary = (x.salaryAmount ?? 0) + tSalary;
                    }
                });
            return (int)Math.Round(tSalary);
        }

        public String getCurrency(int d)
        {
            //using (var _context = new mzmanageEntities())
            //{
            return _context.venue.Where(x => x.Id == d).Select(x => x.currency).FirstOrDefault()
                ?? "N/A";
            //}
        }

        public int netSalary(employee_salary s)
        {
            var result = s.grossSalary;
            result += s.conveyanceAllowance ?? 0;
            result += s.marriageAllowance ?? 0;
            result += s.rentAllowance ?? 0;
            result += s.arrears ?? 0;
            result += s.fmbAllowance ?? 0;
            result -= s.tds ?? 0;
            result -= s.incomeTax ?? 0;
            result -= s.localTax ?? 0;
            result -= s.professionTax ?? 0;
            result -= s.sabeel ?? 0;
            result -= s.withHoldings ?? 0;
            result -= s.qardanHasanahNonRefundable ?? 0;
            result -= s.qardanHasanahRefundable ?? 0;
            result -= s.marafiqKhairiyah ?? 0;
            return result;
        }

        public int netSalary(EmployeeSalaryDetailsModel s)
        {
            var result = s.grossSalary;
            result += s.conveyanceAllowance ?? 0;
            result += s.marriageAllowance ?? 0;
            result += s.rentAllowance ?? 0;
            result += s.arrears ?? 0;
            result += s.fmbAllowance ?? 0;
            result -= s.tds ?? 0;
            result -= s.incomeTax ?? 0;
            result -= s.localTax ?? 0;
            result -= s.professionTax ?? 0;
            result -= s.sabeel ?? 0;
            result -= s.withHoldings ?? 0;
            result -= s.qardanHasanahNonRefundable ?? 0;
            result -= s.qardanHasanahRefundable ?? 0;
            result -= s.marafiqKhairiyah ?? 0;
            return result;
        }

        public int sumAllowances(EmployeeSalaryDetailsModel s)
        {
            var result = 0;
            result += s.conveyanceAllowance ?? 0;
            result += s.marriageAllowance ?? 0;
            result += s.rentAllowance ?? 0;
            result += s.arrears ?? 0;
            result += s.fmbAllowance ?? 0;
            return result;
        }

        public int sumDeduction(EmployeeSalaryDetailsModel s)
        {
            var result = 0;
            result += s.fmbDeduction ?? 0;
            result += s.lessDeduction ?? 0;
            result += s.tds ?? 0;
            result += s.incomeTax ?? 0;
            result += s.professionTax ?? 0;
            result += s.localTax ?? 0;
            result += s.sabeel ?? 0;
            result += s.qardanHasanahRefundable ?? 0;
            result += s.qardanHasanahNonRefundable ?? 0;
            result += s.marafiqKhairiyah ?? 0;
            result += s.withHoldings ?? 0;

            return result;
        }

        public int sumAllowances(employee_salary s)
        {
            var result = 0;
            result += s.conveyanceAllowance ?? 0;
            result += s.marriageAllowance ?? 0;
            result += s.rentAllowance ?? 0;
            result += s.arrears ?? 0;
            result += s.fmbAllowance ?? 0;
            return result;
        }

        public int caculateCTC(EmployeeSalaryDetailsModel s)
        {
            var result = s.grossSalary;
            result += s.conveyanceAllowance ?? 0;
            result += s.marriageAllowance ?? 0;
            result += s.rentAllowance ?? 0;
            result += s.arrears ?? 0;
            result += s.fmbAllowance ?? 0;
            return result;
        }

        public int caculateCTC(salary_allocation_gegorian s)
        {
            var result = s.salary;
            result += s.convenienceAllowance ?? 0;
            result += s.marriageAllowance ?? 0;
            result += s.rentAllowance ?? 0;
            result += s.arrears ?? 0;
            result += s.fmbAllowance ?? 0;
            result += s.overtime ?? 0;
            result -= s.shortfall ?? 0;
            return result;
        }

        public int caculateCTC(salary_allocation_hijri s)
        {
            var result = s.salary;
            result += s.convenienceAllowance ?? 0;
            result += s.marriageAllowance ?? 0;
            result += s.rentAllowance ?? 0;
            result += s.arrears ?? 0;
            result += s.fmbAllowance ?? 0;
            result += s.overtime ?? 0;
            result -= s.shortfall ?? 0;
            return result;
        }

        public int caculateCTC(employee_salary s)
        {
            var result = s.grossSalary;
            result += s.conveyanceAllowance ?? 0;
            result += s.marriageAllowance ?? 0;
            result += s.rentAllowance ?? 0;
            result += s.arrears ?? 0;
            result += s.fmbAllowance ?? 0;
            return result;
        }
    }
}
