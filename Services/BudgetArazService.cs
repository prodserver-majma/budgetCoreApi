using Abp;
using Abp.Collections.Extensions;
using Abp.Extensions;
using Amazon;
using AutoMapper.Internal.Mappers;
using Humanizer;
using mahadalzahrawebapi.Controllers;
using mahadalzahrawebapi.GenericModels;
using mahadalzahrawebapi.Mappings;
using mahadalzahrawebapi.Migrations;
using mahadalzahrawebapi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using NuGet.Packaging;
using Razorpay.Api;
using System;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using static Castle.MicroKernel.ModelBuilder.Descriptors.InterceptorDescriptor;
using static mahadalzahrawebapi.Controllers.ItemController;

namespace mahadalzahrawebapi.Services
{
    public class BudgetArazService
    {
        private readonly mzdbContext _context;        

        public globalConstants _globalConstants;

        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(
            DateTime.UtcNow,
            TimeZoneInfo.FindSystemTimeZoneById("Asia/Kolkata")
        );

        public BudgetArazService(mzdbContext context)
        {
            _context = context;
            _globalConstants = new globalConstants();
        }

        


        public struct arazItemIssue
        {
            public string remark;
            public string createdBy;
            public bool isConcerning;
        }

        public string consumeBudgetResourse(
            AuthUser authUser,
            BillManagementModel bill,
            int financialYear,
            bool isConsumed = true
        )
        {
            mz_expense_budget_araz arazItem = _context
                .mz_expense_budget_araz.Where(x =>
                    x.deptVenueId == bill.deptVenueId
                    && x.psetId == bill.psetId
                    && x.baseItemId == bill.baseItemId
                    && x.itemId == bill.itemId
                    && x.financialYear == financialYear
                ).Include(x => x.mz_expense_budget_araz_monthly)
                .Include(x => x.deptVenue)
                .ThenInclude(x => x.dept_venue_baseitem)
                .FirstOrDefault();

            int monthToUpdate = 0;
            int mnth = 0;
            if (bill.billDate.HasValue)
            {
                mnth = bill.billDate.Value.Month;
                monthToUpdate = switchMonth(mnth);                
            }

            var monthCheck = mnth % 3;

            var start = 0;
            var end = 0;

            if (monthCheck == 0)
            {
                start = mnth - 2;
                end = mnth;
            }
            else if (monthCheck == 2)
            {
                start = mnth - 1;
                end = mnth + 1;
            }
            else
            {
                start = mnth;
                end = mnth + 2;
            }


            start = switchMonth(start);
            end = switchMonth(end);

            if (arazItem == null && isConsumed == false)
            {
                arazItem = new mz_expense_budget_araz
                {
                    deptVenueId = bill.deptVenueId ?? 0,
                    baseItemId = bill.baseItemId ?? 0,
                    itemId = bill.itemId ?? 0,
                    amountPerUom = 0,
                    quantity = 0,
                    consumedAmount = 0,
                    consumedQty = 0,
                    transferedAmount = 0,
                    stage = "Sanctioned",
                    financialYear = financialYear,
                    mz_expense_budget_araz_monthly = new List<mz_expense_budget_araz_monthly>()
                };
                _context.mz_expense_budget_araz.Add(arazItem);


                foreach (var i in arazItem.mz_expense_budget_araz_monthly)
                {
                    if(int.Parse(i.month_num.Replace("Month ", "")) == monthToUpdate)
                    {
                        i.consumedAmount = 0;
                        i.consumedQuantity = 0;
                        i.transferredAmount = 0;
                        i.modified_on = DateTime.Now;
                    }
                }
                
            }
            int monthId = 0;

            if (arazItem == null)
            {
                throw new Exception("Unsanctioned Item");
            }

            float amountWithoutGst = (float)(
                bill.totalAmount - bill.gstAmount - bill.conveyanceAmount
            );
            float gstItemAmount =
                ((bill.gstAmount ?? 0) / amountWithoutGst) * (bill.quantity * bill.amountPerUom)
                ?? 0.0f;
            float conveyanceItemAmount =
                ((bill.conveyanceAmount ?? 0) / amountWithoutGst)
                    * bill.quantity
                    * bill.amountPerUom
                ?? 0.0f;

            float totalDedactable = (float)
                Math.Round(
                    (bill.quantity * bill.amountPerUom) + gstItemAmount + conveyanceItemAmount
                        ?? 0.0f
                );

            if (isConsumed)
            {
                bool toCheck = arazItem
                    .deptVenue.dept_venue_baseitem.FirstOrDefault(x =>
                        x.baseItemId == arazItem.baseItemId
                    )
                    .hasItemBlock;

                //int remBudget = arazItem.mz_expense_budget_araz_monthly.Where(x => int.Parse(x.month_num) >= start && int.Parse(x.month_num) <= end).Sum(i => (int)(i.amount * i.quantity)
                //            + (i.transferredAmount ?? 0)
                //         - ((int)i.consumedAmount * (int)i.consumedQuantity) );

                int remBudget = 0;
                var msg = new List<object>();
                    
                remBudget = arazItem.mz_expense_budget_araz_monthly.Where(i => int.Parse(i.month_num.Replace("Month ", "")) >= 1 && int.Parse(i.month_num.Replace("Month ", "")) <= end)
                    .Sum(i => (int)(i.amount * i.quantity)
                            + (i.transferredAmount)
                         - ((int?)(i.consumedAmount) ?? 0));

                 if ((remBudget < totalDedactable) && toCheck)
                {

                    throw new Exception("Item Sanctioned Amount Exhuasted for " + bill.itemName);
                }

                if ((remBudget < totalDedactable) && toCheck)
                {
                    throw new Exception("Item Sanctioned Amount Exhuasted for " + bill.itemName);
                }

                foreach (var i in arazItem.mz_expense_budget_araz_monthly)
                {

                    //if (int.Parse(i.month_num.Replace("Month ", "")) >= start && int.Parse(i.month_num.Replace("Month ", "")) <= end)
                    //{
                    //    remBudget +=
                    //    (
                    //        (int)(i.amount * i.quantity)
                    //        + (i.transferredAmount ?? 0)
                    //    ) - ((int)i.consumedAmount);
                        
                    //}

                    if (int.Parse(i.month_num.Replace("Month ", "")) == monthToUpdate)
                    {
                        i.modified_on = DateTime.Now;
                        i.consumedQuantity += bill.quantity ?? 0;
                        i.consumedAmount += totalDedactable;
                        monthId = i.id;
                    }
                }

                arazItem.updatedBy = authUser.Name;
                arazItem.updatedOn = DateTime.Now;
                arazItem.consumedAmount += totalDedactable;
                arazItem.consumedQty += bill.quantity;
            }
            else
            {
                foreach (var i in arazItem.mz_expense_budget_araz_monthly)
                {

                    if (int.Parse(i.month_num.Replace("Month ", "")) == monthToUpdate)
                    {
                        i.modified_on = DateTime.Now;
                        i.consumedQuantity -= bill.quantity ?? 0;
                        i.consumedAmount -= totalDedactable;

                    }
                }

                arazItem.updatedBy = authUser.Name;
                arazItem.updatedOn = DateTime.Now;
                arazItem.consumedAmount -= totalDedactable;
                arazItem.consumedQty -= bill.quantity;
            }
            _context.SaveChanges();

            return System.Text.Json.JsonSerializer.Serialize(new {Message = "Success", Amount = totalDedactable, Quantity = bill.quantity, Id = monthId });
            //return "Success";
        }

        public int switchMonth(int month)
        {
            int monthNum = 0;

            switch (month)
            {
                case 04:
                    monthNum = 1;
                    break;
                case 05:
                    monthNum = 2;
                    break;
                case 06:
                    monthNum = 3;
                    break;
                case 07:
                    monthNum = 4;
                    break;
                case 08:
                    monthNum = 5;
                    break;
                case 09:
                    monthNum = 6;
                    break;
                case 10:
                    monthNum = 7;
                    break;
                case 11:
                    monthNum = 8;
                    break;
                case 12:
                    monthNum = 9;
                    break;
                case 01:
                    monthNum = 10;
                    break;
                case 02:
                    monthNum = 11;
                    break;
                case 03:
                    monthNum = 12;
                    break;
            }
            return monthNum;
        }

        public struct budgetconsumedunits
        {
            public int itemId;
            public float amount;
            public float quantity;
            public int month;
        }

        public List<budgetconsumedunits> getBudgetConsumedAmount(
            int deptVenueId,
            int psetId,
            int baseItemId,
            int financialYear
        )
        {
            List<budgetconsumedunits> listItemConsumed = new List<budgetconsumedunits>();
            List<mz_expense_budget_araz> arazList = _context
                .mz_expense_budget_araz.Include(x => x.mz_expense_budget_araz_monthly).Where(x =>
                    x.deptVenueId == deptVenueId
                    && x.psetId == psetId
                    && x.baseItemId == baseItemId
                    && x.financialYear == financialYear
                )
                .ToList();
            foreach (mz_expense_budget_araz item in arazList)
            {
                foreach (var item1 in item.mz_expense_budget_araz_monthly)
                {
                    listItemConsumed.Add(
                    new budgetconsumedunits
                    {
                        amount = item1.consumedAmount,
                        itemId = item1.itemId,
                        quantity = item1.consumedQuantity,
                        month = int.Parse(item1.month_num.Replace("Month ", ""))
                    }
                );
                }
                
            }
            return listItemConsumed;
        }

        public List<BudgetArazItem> getDeptVenueBaseItemTrueRightForBudgetAraz_for_Admin(
            int itsId,
            int deptVenueId,
            int FinancialYear
        )
        {
            int financialYear = FinancialYear;

            try
            {
                List<BudgetArazItem> rights = new List<BudgetArazItem>();
                List<user_deptvenue> udv = _context
                    .user_deptvenue.Where(x => x.itsId == itsId)
                    .ToList();

                List<dept_venue_baseitem> dept_Venue_Baseitems = _context.dept_venue_baseitem.ToList();
                List<mz_expense_procurement_baseitem> baseitems = _context.mz_expense_procurement_baseitem.ToList();
                List<mz_expense_procurement_item> items = _context.mz_expense_procurement_item.ToList();
                List<dept_venue> deptvenues = _context.dept_venue.ToList();
                List<user_dept_venue_baseitem> userDeptVenueBaseitems = _context.user_dept_venue_baseitem.ToList();
                List<mz_expense_budget_araz> budgetAraz = _context.mz_expense_budget_araz.Include(x => x.mz_expense_budget_issue_logs).ToList();
                List<greg_months> gregMonths = _context.greg_months.ToList();
                List<registrationform_dropdown_set> rd = _context.registrationform_dropdown_set.ToList();
                List<registrationform_subprograms> rfs = _context.registrationform_subprograms.ToList();
                List<mz_expense_budget_araz_monthly> monthlyExpense = _context.mz_expense_budget_araz_monthly.ToList();

                //List<mz_expense_budget_araz_monthly> budArazMonthly = _context.mz_expense_budget_araz_monthly.ToList();
                if (deptVenueId == 500)
                {
                    foreach (var ba in budgetAraz)
                    {
                        var udv1 = udv.FirstOrDefault(x => x.deptVenueId == ba.deptVenueId && x.psetId == ba.psetId);
                        if (udv1 != null)
                        {
                            var udvbi1 = userDeptVenueBaseitems.Where(x => x.baseItemId == ba.baseItemId && x.dept_venueId == udv1.deptVenueId);
                            if (udvbi1 != null)
                            {
                                float budget = ba.amountPerUom;
                                int quntity = ba.quantity;

                                var bi = baseitems.FirstOrDefault(x => x.id == ba.baseItemId);
                                var item = items.FirstOrDefault(x => x.id == ba.itemId);

                                dept_venue dv = deptvenues.Where(x => x.id == ba.deptVenueId).FirstOrDefault();
                                var deptName = dv.deptName + "_" + dv.venueName;
                                if (ba.psetId != null)
                                {
                                    var rds = rd?.FirstOrDefault(sr => sr.id == ba.psetId);
                                    var className = rfs?.FirstOrDefault(rs => rs.id == rds.subprogramId);
                                    deptName = dv.deptName + "_" + dv.venueName + "_" + className.name;
                                }
                                BudgetArazItem res = new BudgetArazItem
                                {
                                    total = budget * quntity,
                                    uom = item.uom,
                                    id = ba.id,
                                    baseItemName = bi.name,
                                    perUnitAmt = budget,
                                    description = ba.justification,
                                    quantity = quntity,
                                    itemId = item.id,
                                    name = item.name,
                                    deptVenueName = deptName,
                                    isConcerning = false,
                                    verified = true,
                                    stage = ba.stage,
                                    remark = ba.remarks_admin,
                                    hasIssues = false,
                                    isExpense = true,
                                    createdBy = ba.createdBy,
                                    createdOn = ba.createdOn?.ToString("dd/MM/yyyy hh:mm tt"),
                                };

                                res.months = new List<mz_expense_budget_araz_monthly>();

                                List<mz_expense_budget_araz_monthly>? month = monthlyExpense.Where(x => x.budget_araz_id == ba.id).ToList();
                                if (month != null)
                                {
                                    foreach (var monthitem in month)
                                    {
                                        var greg_month = gregMonths.Where(x => x.slug == monthitem.month_num).FirstOrDefault();

                                        if (greg_month != null)
                                        {
                                            monthitem.month_num = greg_month.month_name;
                                        }
                                        if (ba.id == monthitem.budget_araz_id)
                                        {
                                            res.months.Add(monthitem);
                                        }
                                    }
                                }

                                //System.Diagnostics.Debug.WriteLine($"This is res.months: {System.Text.Json.JsonSerializer.Serialize(res.months)}");

                                var remark = ba.mz_expense_budget_issue_logs.FirstOrDefault();
                                if (ba.mz_expense_budget_issue_logs.Count > 0)
                                {
                                    res.hasIssues = true;
                                    res.remark = remark.remark;
                                }

                                if (ba.stage.Contains("Initia"))
                                {
                                    res.verified = false;
                                    if (ba.stage.Contains("concern"))
                                    {
                                        res.isConcerning = true;
                                    }
                                }
                                ;

                                rights.Add(res);
                            }

                        }

                    }
                }
                else if (deptVenueId != 500)
                {
                    dept_venue dv = deptvenues.Where(x => x.id == deptVenueId)
                        .FirstOrDefault();
                    List<user_dept_venue_baseitem> udvbi = userDeptVenueBaseitems.Where(x =>
                            x.dept_venueId == deptVenueId && x.itsId == itsId
                        )
                        .ToList();
                    foreach (var j in udvbi)
                    {
                        List<mz_expense_budget_araz> bb = budgetAraz.Where(x =>
                                x.financialYear == financialYear
                                && x.deptVenueId == j.dept_venueId
                                && x.baseItemId == j.baseItemId
                            )
                            .ToList();
                        bb = bb.OrderByDescending(x => x.createdOn).ToList();
                        foreach (var b in bb)
                        {
                            float budget = b.amountPerUom;
                            int quntity = b.quantity;
                            string remarks = b.justification;
                            dept_venue_baseitem dvbi = dept_Venue_Baseitems.Where(x =>
                                    x.deptVenueId == j.dept_venueId && x.psetId == j.psetId && x.baseItemId == j.baseItemId
                                )
                                .FirstOrDefault();
                            mz_expense_procurement_baseitem bi = baseitems.Where(x => x.id == j.baseItemId)
                                .FirstOrDefault();
                            mz_expense_procurement_item item = items.Where(x => x.id == b.itemId)
                                .FirstOrDefault();

                            BudgetArazItem res = new BudgetArazItem
                            {
                                total = budget * quntity,
                                uom = item.uom,
                                id = b.id,
                                baseItemName = bi.name,
                                perUnitAmt = budget,
                                description = remarks,
                                quantity = quntity,
                                itemId = item.id,
                                name = item.name,
                                deptVenueName = dv.deptName + "_" + dv.venueName,
                                isConcerning = false,
                                verified = true,
                                stage = b.stage,
                                remark = b.remarks_admin,
                                hasIssues = false,
                                isExpense = true,
                                createdBy = b.createdBy,
                                createdOn = b.createdOn?.ToString("dd/MM/yyyy hh:mm tt"),
                            };

                            if (b.mz_expense_budget_issue_logs.Count > 0)
                            {
                                res.hasIssues = true;
                            }

                            if (b.stage.Contains("Initia"))
                            {
                                res.verified = false;
                                if (b.stage.Contains("concern"))
                                {
                                    res.isConcerning = true;
                                }
                            }

                            rights.Add(res);
                        }
                    }
                }
                rights = rights
                    .OrderByDescending(x => x.isConcerning)
                    .ThenBy(x => x.deptVenueName)
                    .ThenBy(x => x.baseItemName)
                    .ThenBy(x => x.name)
                    .ThenByDescending(x => x.total)
                    .ToList();

                return rights;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<BudgetArazItem> getDeptVenueBaseItemTrueRightForBudgetAraz_for_User(
            int itsId,
            int deptVenueId,
            int FinancialYear
        )
        {
            int financialYear = FinancialYear;

            try
            {
                List<BudgetArazItem> rights = new List<BudgetArazItem>();
                List<user_deptvenue> udv = _context
                    .user_deptvenue.Where(x => x.itsId == itsId)
                    .ToList();

                List<dept_venue_baseitem> dept_Venue_Baseitems = _context.dept_venue_baseitem.ToList();
                List<mz_expense_procurement_baseitem> baseitems = _context.mz_expense_procurement_baseitem.ToList();
                List<mz_expense_procurement_item> items = _context.mz_expense_procurement_item.ToList();
                List<dept_venue> deptvenues = _context.dept_venue.ToList();
                List<user_dept_venue_baseitem> userDeptVenueBaseitems = _context.user_dept_venue_baseitem.ToList();
                List<mz_expense_budget_araz> budgetAraz = _context.mz_expense_budget_araz.Where(x => x.financialYear == financialYear).Include(x => x.mz_expense_budget_issue_logs).ToList();
                List<greg_months> gregMonths = _context.greg_months.ToList();
                List<registrationform_dropdown_set> rd = _context.registrationform_dropdown_set.ToList();
                List<registrationform_subprograms> rfs = _context.registrationform_subprograms.ToList();
                List<mz_expense_budget_araz_monthly> monthlyExpense = _context.mz_expense_budget_araz_monthly.ToList();

                //List<mz_expense_budget_araz_monthly> budArazMonthly = _context.mz_expense_budget_araz_monthly.ToList();
                if (deptVenueId == 500)
                {
                    foreach (var ba in budgetAraz)
                    {
                        var udv1 = udv.FirstOrDefault(x => x.deptVenueId == ba.deptVenueId && x.psetId == ba.psetId);
                        if (udv1 != null)
                        {
                            var udvbi1 = userDeptVenueBaseitems.Where(x => x.baseItemId == ba.baseItemId && x.dept_venueId == udv1.deptVenueId);
                            if (udvbi1 != null)
                            {
                                float budget = ba.amountPerUom;
                                int quntity = ba.quantity;

                                var bi = baseitems.FirstOrDefault(x => x.id == ba.baseItemId);
                                var item = items.FirstOrDefault(x => x.id == ba.itemId);

                                dept_venue dv = deptvenues.Where(x => x.id == ba.deptVenueId).FirstOrDefault();
                                var deptName = dv.deptName + "_" + dv.venueName;
                                if (ba.psetId != null)
                                {
                                    var rds = rd?.FirstOrDefault(sr => sr.id == ba.psetId);
                                    var className = rfs?.FirstOrDefault(rs => rs.id == rds.subprogramId);
                                    deptName = dv.deptName + "_" + dv.venueName + "_" + className.name;
                                }
                                BudgetArazItem res = new BudgetArazItem
                                {
                                    total = budget * quntity,
                                    uom = item.uom,
                                    id = ba.id,
                                    baseItemName = bi.name,
                                    perUnitAmt = budget,
                                    description = ba.justification,
                                    quantity = quntity,
                                    itemId = item.id,
                                    name = item.name,
                                    deptVenueName = deptName,
                                    isConcerning = false,
                                    verified = true,
                                    stage = ba.stage,
                                    remark = ba.remarks_admin,
                                    hasIssues = false,
                                    isExpense = true,
                                    createdBy = ba.createdBy,
                                    createdOn = ba.createdOn?.ToString("dd/MM/yyyy hh:mm tt"),
                                };

                                res.months = new List<mz_expense_budget_araz_monthly>();

                                List<mz_expense_budget_araz_monthly>? month = monthlyExpense.Where(x => x.budget_araz_id == ba.id).ToList();
                                if (month != null)
                                {
                                    foreach (var monthitem in month)
                                    {
                                        var greg_month = gregMonths.Where(x => x.slug == monthitem.month_num).FirstOrDefault();

                                        if (greg_month != null)
                                        {
                                            monthitem.month_num = greg_month.month_name;
                                        }
                                        if (ba.id == monthitem.budget_araz_id)
                                        {
                                            res.months.Add(monthitem);
                                            //res.months = month.Select(m => new
                                            //mz_expense_budget_araz_monthly
                                            //{
                                            //    id = monthitem.id,
                                            //    month_num = monthitem.month_num,
                                            //    amount = monthitem.amount,
                                            //    quantity = monthitem.quantity,
                                            //    deptVenueId = monthitem.deptVenueId,
                                            //    itemId = monthitem.itemId,
                                            //    consumedAmount = monthitem.consumedQuantity,
                                            //    consumedQuantity = monthitem.consumedQuantity,
                                            //    transferredAmount = monthitem.transferredAmount,
                                            //    status = monthitem.status
                                            //}).ToList();
                                        }
                                    }
                                }

                                //System.Diagnostics.Debug.WriteLine($"This is res.months: {System.Text.Json.JsonSerializer.Serialize(res.months)}");

                                var remark = ba.mz_expense_budget_issue_logs.FirstOrDefault();
                                if (ba.mz_expense_budget_issue_logs.Count > 0)
                                {
                                    res.hasIssues = true;
                                    res.remark = remark.remark;
                                }

                                if (ba.stage.Contains("Initia"))
                                {
                                    res.verified = false;
                                    if (ba.stage.Contains("concern"))
                                    {
                                        res.isConcerning = true;
                                    }
                                }
                                ;

                                rights.Add(res);
                            }

                        }

                    }
                    //foreach (var i in udv)
                    //{
                    //    dept_venue dv = deptvenues.Where(x => x.id == i.deptVenueId)
                    //        .FirstOrDefault();
                    //    List<user_dept_venue_baseitem> udvbi = userDeptVenueBaseitems.Where(x =>
                    //            x.dept_venueId == i.deptVenueId && x.itsId == itsId && x.psetId == i.psetId
                    //        )
                    //        .ToList();
                    //    foreach (var j in udvbi)
                    //    {
                    //        List<mz_expense_budget_araz> bb = _context
                    //            .mz_expense_budget_araz.Where(x =>
                    //                x.financialYear == financialYear
                    //                && x.deptVenueId == j.dept_venueId
                    //                && x.baseItemId == j.baseItemId
                    //                && x.psetId == j.psetId
                    //            )
                    //            .ToList();
                    //        bb = bb.OrderByDescending(x => x.createdOn).ToList();
                    //        System.Diagnostics.Debug.WriteLine($"This is bb Data: {financialYear}, {j.dept_venueId}, {j.baseItemId}, {j.psetId} ");

                    //        //if(bb != null)
                    //        //{
                    //        //    
                    //        //}

                    //        foreach (var b in bb)
                    //        {
                    //            int budget = b.amountPerUom;
                    //            int quntity = b.quantity;
                    //            string remarks = b.justification;
                    //            dept_venue_baseitem dvbi = dept_Venue_Baseitems.Where(x =>
                    //                    x.deptVenueId == i.deptVenueId
                    //                    && x.baseItemId == j.baseItemId
                    //                )
                    //                .FirstOrDefault();
                    //            mz_expense_procurement_baseitem bi = baseitems.Where(x =>
                    //                    x.id == j.baseItemId
                    //                )
                    //                .FirstOrDefault();
                    //            mz_expense_procurement_item item = items.Where(x => x.id == b.itemId)
                    //                .FirstOrDefault();

                    //            var deptName = dv.deptName + "_" + dv.venueName;
                    //            if (b.psetId != null)
                    //            {
                    //                var rds = _context.registrationform_dropdown_set?.FirstOrDefault(sr => sr.id == b.psetId);                                
                    //                var className = _context.registrationform_subprograms.FirstOrDefault(rs => rs.id == rds.subprogramId);
                    //                deptName = dv.deptName + "_" + dv.venueName + "_" + className.name;
                    //            }

                    //            BudgetArazItem res = new BudgetArazItem
                    //            {
                    //                total = budget * quntity,
                    //                uom = item.uom,
                    //                id = b.id,
                    //                baseItemName = bi.name,
                    //                perUnitAmt = budget,
                    //                description = remarks,
                    //                quantity = quntity,
                    //                itemId = item.id,
                    //                name = item.name,
                    //                deptVenueName = deptName,
                    //                isConcerning = false,
                    //                verified = true,
                    //                stage = b.stage,
                    //                remark = b.remarks_admin,
                    //                hasIssues = false,
                    //                isExpense = true,
                    //                createdBy = b.createdBy,
                    //                createdOn = b.createdOn?.ToString("dd/MM/yyyy hh:mm tt"),
                    //            };

                    //            res.months = new List<mz_expense_budget_araz_monthly>();

                    //            List<mz_expense_budget_araz_monthly> month = _context.mz_expense_budget_araz_monthly.Where(x => x.budget_araz_id == b.id).ToList();

                    //            foreach (var monthitem in month)
                    //            {
                    //                if (b.id == monthitem.budget_araz_id)
                    //                {
                    //                    res.months.Add(monthitem);
                    //                }
                    //            }
                    //            System.Diagnostics.Debug.WriteLine($"This is res.months: {System.Text.Json.JsonSerializer.Serialize(res.months)}");


                    //            if (b.mz_expense_budget_issue_logs.Count > 0)
                    //            {
                    //                res.hasIssues = true;
                    //            }

                    //            if (b.stage.Contains("Initia"))
                    //            {
                    //                res.verified = false;
                    //                if (b.stage.Contains("concern"))
                    //                {
                    //                    res.isConcerning = true;
                    //                }
                    //            };

                    //            rights.Add(res);
                    //        }
                    //    }
                    //}
                }
                else if (deptVenueId != 500)
                {
                    dept_venue dv = deptvenues.Where(x => x.id == deptVenueId)
                        .FirstOrDefault();
                    List<user_dept_venue_baseitem> udvbi = userDeptVenueBaseitems.Where(x =>
                            x.dept_venueId == deptVenueId && x.itsId == itsId
                        )
                        .ToList();
                    foreach (var j in udvbi)
                    {
                        List<mz_expense_budget_araz> bb = budgetAraz.Where(x =>
                                x.financialYear == financialYear
                                && x.deptVenueId == j.dept_venueId
                                && x.baseItemId == j.baseItemId
                            )
                            .ToList();
                        bb = bb.OrderByDescending(x => x.createdOn).ToList();
                        foreach (var b in bb)
                        {
                            float budget = b.amountPerUom;
                            int quntity = b.quantity;
                            string remarks = b.justification;
                            dept_venue_baseitem dvbi = dept_Venue_Baseitems.Where(x =>
                                    x.deptVenueId == j.dept_venueId && x.psetId == j.psetId && x.baseItemId == j.baseItemId
                                )
                                .FirstOrDefault();
                            mz_expense_procurement_baseitem bi = baseitems.Where(x => x.id == j.baseItemId)
                                .FirstOrDefault();
                            mz_expense_procurement_item item = items.Where(x => x.id == b.itemId)
                                .FirstOrDefault();

                            BudgetArazItem res = new BudgetArazItem
                            {
                                total = budget * quntity,
                                uom = item.uom,
                                id = b.id,
                                baseItemName = bi.name,
                                perUnitAmt = budget,
                                description = remarks,
                                quantity = quntity,
                                itemId = item.id,
                                name = item.name,
                                deptVenueName = dv.deptName + "_" + dv.venueName,
                                isConcerning = false,
                                verified = true,
                                stage = b.stage,
                                remark = b.remarks_admin,
                                hasIssues = false,
                                isExpense = true,
                                createdBy = b.createdBy,
                                createdOn = b.createdOn?.ToString("dd/MM/yyyy hh:mm tt"),
                            };

                            if (b.mz_expense_budget_issue_logs.Count > 0)
                            {
                                res.hasIssues = true;
                            }

                            if (b.stage.Contains("Initia"))
                            {
                                res.verified = false;
                                if (b.stage.Contains("concern"))
                                {
                                    res.isConcerning = true;
                                }
                            }

                            rights.Add(res);
                        }
                    }
                }
                rights = rights
                    .OrderByDescending(x => x.isConcerning)
                    .ThenBy(x => x.deptVenueName)
                    .ThenBy(x => x.baseItemName)
                    .ThenBy(x => x.name)
                    .ThenByDescending(x => x.total)
                    .ToList();

                return rights;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private string getColheaderColor(Dictionary<string, string> test, string myKey)
        {

            if (test.ContainsKey(myKey))
                return test[myKey];
            else
                return "#fb7185";
        }

        //public BudgetArazSummaryModels getBudgetArazSummary(AuthUser authuser, bool audited)
        //{
        //    List<dept_venue> deptvenues = _context
        //        .dept_venue.Where(x => x.status == "active")
        //        .Include(x => x.venue)
        //        .Include(x => x.registrationform_dropdown_set)
        //        .ThenInclude(x => x.program)
        //        .Include(x => x.registrationform_dropdown_set)
        //        .ThenInclude(x => x.subprogram)
        //        .Include(x => x.registrationform_dropdown_set)
        //        .ThenInclude(x => x.venue)
        //        .ToList();

        //    List<mz_expense_procurement_baseitem> baseitems = _context
        //        .mz_expense_procurement_baseitem.Where(x => x.status == true)
        //        .ToList();

        //    List<mz_student_feecategory> feecategories = _context.mz_student_feecategory.ToList();

        //    Dictionary<string, string> deptColors = new Dictionary<string, string>();

        //    deptColors.Add("NISAB", "#fed7aa");
        //    deptColors.Add("ELEARNING", "#fde68a");
        //    deptColors.Add("MUKHAYYAM", "#bfdbfe");
        //    deptColors.Add("SHOUN AMMAH", "#99f6e4");
        //    deptColors.Add("MAUZE TAHFEEZ", "#d9f99d");
        //    deptColors.Add("ADMINISTRATION", "#BD8B9C");

        //    try
        //    {
        //        int financialYear = Int32.Parse(
        //            _context
        //                .global_constant.Where(x => x.key == "budgetFinancialYear")
        //                .FirstOrDefault()
        //                .value
        //        );
        //        BudgetArazSummaryModels result = new BudgetArazSummaryModels();
        //        result.colHeaderGroup = new List<BudgetSummaryColHeaderGroup>();
        //        result.colHeader = new List<BudgetSummaryColHeader>();
        //        result.rowHeader = new List<BudgetSummaryRowHeader>();

        //        List<BudgetArazItem> adminCommisionList = new List<BudgetArazItem>();

        //        result.rowHeader.Add(
        //            new BudgetSummaryRowHeader
        //            {
        //                id = -1,
        //                name = "Students Hub Raqm",
        //                isIncluded = true,
        //                Total = 0,
        //                isCapital = false,
        //                isExpense = false,
        //                show = true,
        //            }
        //        );
        //        List<user_dept_venue_baseitem> udv = _context
        //            .user_dept_venue_baseitem.Where(x => x.itsId == authuser.ItsId)
        //            .ToList();
        //        for (int i = 0; i < udv.Count(); i++)
        //        {
        //            dept_venue dv = deptvenues
        //                .Where(x => x.id == udv[i].dept_venueId)
        //                .FirstOrDefault();

        //            if (dv != null && !result.colHeader.Any(x => x.id == dv.id))
        //            {
        //                if (!result.colHeader.Any(x => x.id == dv.id))
        //                {
        //                    BudgetSummaryColHeader colHeader = new BudgetSummaryColHeader
        //                    {
        //                        id = dv.id,
        //                        name = dv.venue.displayName,
        //                        deptVenue = dv,
        //                        groupId = dv.deptId ?? 0,
        //                        isIncluded = false,
        //                        deptVenueName = dv.deptName + "_" + dv.venue.displayName,
        //                        show = true
        //                    };
        //                    colHeader.color = getColheaderColor(deptColors, dv.deptName);

        //                    result.colHeader.Add(colHeader);
        //                }
        //                if (!result.colHeaderGroup.Any(x => x.id == dv.deptId))
        //                {
        //                    BudgetSummaryColHeaderGroup colHeaderGroup =
        //                        new BudgetSummaryColHeaderGroup
        //                        {
        //                            id = dv.deptId ?? 0,
        //                            name = dv.deptName,
        //                            isIncluded = false,
        //                            show = true,
        //                        };
        //                    colHeaderGroup.color = getColheaderColor(deptColors, dv.deptName);
        //                    result.colHeaderGroup.Add(colHeaderGroup);
        //                }
        //            }
        //            result.colHeader = result.colHeader.OrderBy(x => x.groupId).ToList();
        //            result.colHeaderGroup = result.colHeaderGroup.OrderBy(x => x.id).ToList();

        //            mz_expense_procurement_baseitem baseItem = baseitems
        //                .Where(x => x.id == udv[i].baseItemId)
        //                .FirstOrDefault();
        //            if (baseItem != null && !result.rowHeader.Any(x => x.id == baseItem.id))
        //            {
        //                result.rowHeader.Add(
        //                    new BudgetSummaryRowHeader
        //                    {
        //                        id = baseItem.id,
        //                        name = baseItem.name,
        //                        expensehead = baseItem,
        //                        isIncluded = true,
        //                        Total = 0,
        //                        isCapital = baseItem.isCapital,
        //                        isExpense = !baseItem.isIncome,
        //                        show = true,
        //                    }
        //                );
        //            }
        //        }
        //        result.rowHeader = result
        //            .rowHeader.OrderBy(x => x.isExpense)
        //            .ThenBy(x => x.isCapital)
        //            .ToList();
        //        result.summary = new List<BudgetArazDept>();
        //        List<mz_expense_budget_araz> mainBudget = _context
        //            .mz_expense_budget_araz.Where(x => x.financialYear == financialYear)
        //            .Include(x => x.mz_expense_budget_issue_logs)
        //            .Include(x => x.deptVenue)
        //            .Include(x => x.baseItem)
        //            .Include(x => x.item)
        //            .ToList();
        //        if (audited)
        //        {
        //            mainBudget = mainBudget.Where(x => !x.stage.Contains("Initia")).ToList();
        //        }
        //        List<mz_expense_estimate_student> incomeEstimate = _context
        //            .mz_expense_estimate_student.Where(x => x.financialYear == financialYear)
        //            .Include(x => x.mz_expense_student_budget_issue_logs)
        //            .ToList();

        //        for (int i = 0; i < result.colHeaderGroup.Count; i++)
        //        {
        //            BudgetArazDept dept = new BudgetArazDept()
        //            {
        //                name = result.colHeaderGroup[i].name,
        //                id = result.colHeaderGroup[i].id,
        //            };
        //            List<mz_expense_budget_araz> budget = new List<mz_expense_budget_araz>();
        //            mainBudget.ForEach(x =>
        //            {
        //                if (x.deptVenue.deptId == dept.id)
        //                {
        //                    budget.Add(x);
        //                }
        //                ;
        //            });
        //            dept.deptVenues = new List<BudgetArazDeptVenue>();
        //            for (int j = 0; j < result.colHeader.Count; j++)
        //            {
        //                dept_venue d = result.colHeader[j].deptVenue;
        //                if (d.deptId != dept.id)
        //                {
        //                    continue;
        //                }
        //                if (d.status != "active")
        //                {
        //                    continue;
        //                }
        //                BudgetArazDeptVenue deptVenue = new BudgetArazDeptVenue()
        //                {
        //                    id = d.id,
        //                    deptId = d.deptId ?? 0,
        //                    deptName = d.deptName,
        //                    name = d.deptName + "_" + d.venueName,
        //                    venueName = result.colHeader[j].name,
        //                    masterDeptName = d.masterDeptName
        //                };
        //                deptVenue.expenseHeads = new List<BudgetArazExpenseHead>();

        //                List<mz_expense_budget_araz> bbi = budget
        //                    .Where(x => x.deptVenueId == d.id)
        //                    .GroupBy(x => x.baseItemId)
        //                    .Select(x => x.FirstOrDefault())
        //                    .ToList();
        //                for (int k = 0; k < bbi.Count; k++)
        //                {
        //                    BudgetSummaryRowHeader expHead = result
        //                        .rowHeader.Where(x => x.id == bbi[k].baseItemId)
        //                        .FirstOrDefault();
        //                    if (expHead == null)
        //                    {
        //                        continue;
        //                    }

        //                    mz_expense_procurement_baseitem obi = expHead.expensehead;
        //                    List<mz_expense_budget_araz> bitems = budget
        //                        .Where(x => x.baseItemId == obi.id && x.deptVenueId == d.id)
        //                        .ToList();

        //                    if (bitems.Count > 0)
        //                    {
        //                        BudgetArazExpenseHead expenseHead = new BudgetArazExpenseHead
        //                        {
        //                            id = obi.id,
        //                            name = obi.name,
        //                            status = obi.status,
        //                            isCapital = obi.isCapital,
        //                            isConcerning = false,
        //                            verified = true,
        //                            isExpense = !obi.isIncome,
        //                        };
        //                        expenseHead.items = new List<BudgetArazItem>();
        //                        int c = 1;
        //                        foreach (mz_expense_budget_araz x in bitems)
        //                        {
        //                            BudgetArazItem item = new BudgetArazItem
        //                            {
        //                                id = x.id,
        //                                itemId = x.item.id,
        //                                name = x.item.name,
        //                                type = x.item.type,
        //                                description = x.justification,
        //                                perUnitAmt = x.amountPerUom,
        //                                quantity = x.quantity,
        //                                uom = x.uom,
        //                                srno = c,
        //                                remark = x.remarks_admin,
        //                                isConcerning = false,
        //                                verified = true,
        //                                hasIssues = false,
        //                                isExpense = !obi.isIncome,
        //                                stage = x.stage,
        //                            };
        //                            if (x.mz_expense_budget_issue_logs.Count > 0)
        //                            {
        //                                item.hasIssues = true;
        //                            }
        //                            if (audited)
        //                            {
        //                                if (x.stage != "Audited")
        //                                {
        //                                    expenseHead.verified = false;
        //                                    item.verified = false;
        //                                }
        //                            }
        //                            if (x.stage.Contains("Initia"))
        //                            {
        //                                if (audited)
        //                                {
        //                                    continue;
        //                                }
        //                                expenseHead.verified = false;
        //                                item.verified = false;
        //                                if (x.stage.Contains("concern"))
        //                                {
        //                                    expenseHead.isConcerning = true;
        //                                    item.isConcerning = true;
        //                                }
        //                            }
        //                            ;

        //                            item.total = item.perUnitAmt * item.quantity;
        //                            expenseHead.items.Add(item);
        //                            c++;
        //                        }
        //                        ;
        //                        if (audited)
        //                        {
        //                            if (expenseHead.items.Any(x => x.stage.Contains("Initia")))
        //                            {
        //                                continue;
        //                            }
        //                        }
        //                        expenseHead.total = expenseHead.items.Sum(x => x.total);
        //                        deptVenue.expenseHeads.Add(expenseHead);
        //                        expHead.isIncluded = true;
        //                        expHead.Total += expenseHead.total;
        //                    }
        //                }

        //                List<mz_expense_estimate_student> deptStudentEstimate = incomeEstimate
        //                    .Where(x => d.registrationform_dropdown_set.Any(y => y.id == x.psetId))
        //                    .ToList();
        //                if (deptStudentEstimate.Count > 0)
        //                {
        //                    BudgetArazExpenseHead incomeHead = new BudgetArazExpenseHead
        //                    {
        //                        id = -1,
        //                        name = "Students Hub Raqm",
        //                        total = 0,
        //                        isExpense = false,
        //                    };
        //                    incomeHead.items = new List<BudgetArazItem>();
        //                    int sr = 1;

        //                    for (int k = 0; k < deptStudentEstimate.Count; k++)
        //                    {
        //                        BudgetArazItem item = new BudgetArazItem
        //                        {
        //                            id = deptStudentEstimate[k].id,
        //                            type = deptStudentEstimate[k].fcId.ToString(),
        //                            description = deptStudentEstimate[k].remarks,
        //                            perUnitAmt = deptStudentEstimate[k].feesAmount ?? 0,
        //                            quantity = deptStudentEstimate[k].studentCountPerMonth ?? 0,
        //                            uom = deptStudentEstimate[k].duration.ToString(),
        //                            srno = sr,
        //                            remark = deptStudentEstimate[k].remarks_admin,
        //                            isConcerning = false,
        //                            verified = true,
        //                            hasIssues = false,
        //                            isExpense = false,
        //                            stage = deptStudentEstimate[k].stage
        //                        };
        //                        string feecategoryName =
        //                            feecategories
        //                                .Where(x => x.id == deptStudentEstimate[k].fcId)
        //                                .FirstOrDefault()
        //                                ?.categoryName ?? "";
        //                        registrationform_dropdown_set pset = d
        //                            .registrationform_dropdown_set.Where(x =>
        //                                x.id == deptStudentEstimate[k].psetId
        //                            )
        //                            .FirstOrDefault();
        //                        item.name =
        //                            pset.program.name
        //                            + "-"
        //                            + pset.program.name
        //                            + "-"
        //                            + feecategoryName;
        //                        item.total =
        //                            item.perUnitAmt
        //                            * item.quantity
        //                            * deptStudentEstimate[k].duration;
        //                        if (
        //                            deptStudentEstimate[k]
        //                                .mz_expense_student_budget_issue_logs
        //                                .Count > 0
        //                        )
        //                        {
        //                            item.hasIssues = true;
        //                        }
        //                        if (audited)
        //                        {
        //                            if (deptStudentEstimate[k].stage != "Audited")
        //                            {
        //                                incomeHead.verified = false;
        //                                item.verified = false;
        //                            }
        //                        }
        //                        if (deptStudentEstimate[k].stage.Contains("Initia"))
        //                        {
        //                            if (audited)
        //                            {
        //                                continue;
        //                            }
        //                            incomeHead.verified = false;
        //                            item.verified = false;
        //                            if (deptStudentEstimate[k].stage.Contains("concern"))
        //                            {
        //                                incomeHead.isConcerning = true;
        //                                item.isConcerning = true;
        //                            }
        //                        }
        //                        ;

        //                        incomeHead.total += item.total;
        //                        incomeHead.items.Add(item);
        //                        sr++;
        //                    }
        //                    int adminCommision = (int)(((double)incomeHead.total) * -0.025);
        //                    BudgetArazItem adminIncomeCommission = new BudgetArazItem();
        //                    if (d.deptId == 3 || d.deptId == 2)
        //                    {
        //                        adminCommision = (int)(((double)incomeHead.total) * -0.03);
        //                        adminIncomeCommission = new BudgetArazItem
        //                        {
        //                            id = -1,
        //                            type = "3% Admin Expense",
        //                            description = "3% Admin Expense",
        //                            perUnitAmt = adminCommision,
        //                            quantity = 1,
        //                            uom = "Percentage",
        //                            srno = sr,
        //                            remark = "Administration Expense of 3%",
        //                            isConcerning = false,
        //                            verified = true,
        //                            hasIssues = false,
        //                            isExpense = false,
        //                            stage = "",
        //                            total = adminCommision,
        //                            name = "Admin Expense"
        //                        };
        //                    }
        //                    else
        //                    {
        //                        adminIncomeCommission = new BudgetArazItem
        //                        {
        //                            id = -1,
        //                            type = "2.5% Admin Expense",
        //                            description = "2.5% Admin Expense",
        //                            perUnitAmt = adminCommision,
        //                            quantity = 1,
        //                            uom = "Percentage",
        //                            srno = sr,
        //                            remark = "Administration Expense of 2.5%",
        //                            isConcerning = false,
        //                            verified = true,
        //                            hasIssues = false,
        //                            isExpense = false,
        //                            stage = "",
        //                            total = adminCommision,
        //                            name = "Admin Expense"
        //                        };
        //                    }
        //                    incomeHead.total += adminIncomeCommission.total;
        //                    incomeHead.items.Add(adminIncomeCommission);

        //                    adminCommisionList.Add(
        //                        new BudgetArazItem
        //                        {
        //                            id = 0 - d.id,
        //                            type = "",
        //                            description =
        //                                "Administration Income from "
        //                                + d.deptName
        //                                + "_"
        //                                + d.venueName,
        //                            perUnitAmt = adminCommision * -1,
        //                            quantity = 1,
        //                            uom = "Percentage",
        //                            srno = sr,
        //                            remark =
        //                                "Administration Income from "
        //                                + d.deptName
        //                                + "_"
        //                                + d.venueName,
        //                            isConcerning = false,
        //                            verified = true,
        //                            hasIssues = false,
        //                            isExpense = false,
        //                            stage = "",
        //                            total = adminCommision * -1,
        //                            name = "Admin Income"
        //                        }
        //                    );

        //                    if (audited)
        //                    {
        //                        if (incomeHead.items.Any(x => x.stage.Contains("Initia")))
        //                        {
        //                            continue;
        //                        }
        //                    }
        //                    deptVenue.expenseHeads.Add(incomeHead);
        //                    BudgetSummaryRowHeader rh = result
        //                        .rowHeader.Where(x => x.id == -1)
        //                        .FirstOrDefault();
        //                    rh.isIncluded = true;
        //                    rh.Total += incomeHead.total;
        //                }

        //                deptVenue.total =
        //                    deptVenue.expenseHeads.Where(x => x.isExpense).Sum(x => x.total)
        //                    - deptVenue.expenseHeads.Where(x => !x.isExpense).Sum(x => x.total);
        //                result.colHeader[j].isIncluded = true;
        //                dept.deptVenues.Add(deptVenue);
        //            }
        //            if (dept.deptVenues.Count > 0)
        //            {
        //                BudgetArazDeptVenue deptTotal = new BudgetArazDeptVenue
        //                {
        //                    id = 0 - dept.id,
        //                    name = "Total",
        //                    deptId = dept.id,
        //                    deptName = dept.name,
        //                    total = dept.deptVenues.Sum(x => x.total)
        //                };
        //                deptTotal.expenseHeads = new List<BudgetArazExpenseHead>();
        //                string errMsg = "";
        //                try
        //                {
        //                    foreach (BudgetArazDeptVenue x in dept.deptVenues)
        //                    {
        //                        foreach (BudgetArazExpenseHead y in x.expenseHeads)
        //                        {
        //                            if (!deptTotal.expenseHeads.Any(z => z.id == y.id))
        //                            {
        //                                deptTotal.expenseHeads.Add(
        //                                    new BudgetArazExpenseHead
        //                                    {
        //                                        id = y.id,
        //                                        name = y.name,
        //                                        status = y.status,
        //                                        total = y.total,
        //                                        isCapital = y.isCapital,
        //                                        isExpense = y.isExpense,
        //                                    }
        //                                );
        //                            }
        //                            else
        //                            {
        //                                deptTotal
        //                                    .expenseHeads.Where(z => z.id == y.id)
        //                                    .FirstOrDefault()
        //                                    .total += y.total;
        //                            }
        //                        }
        //                    }
        //                }
        //                catch (Exception e)
        //                {
        //                    //errMsg += e.Message + ", ";
        //                    throw new Exception(e.ToString());
        //                }
        //                dept.deptVenues.Add(deptTotal);
        //                dept.total = deptTotal.total;
        //                result.colHeaderGroup[i].isIncluded = true;
        //                result.summary.Add(dept);
        //            }
        //        }

        //        result.colHeader = result.colHeader.Where(x => x.isIncluded).ToList();
        //        result.colHeaderGroup = result.colHeaderGroup.Where(x => x.isIncluded).ToList();
        //        result.rowHeader = result.rowHeader.Where(x => x.isIncluded).ToList();

        //        BudgetArazDept adminDept = result.summary.Where(x => x.id == 11).FirstOrDefault();
        //        BudgetArazDeptVenue adminDeptVenue = adminDept
        //            ?.deptVenues.Where(y => y.id == 15)
        //            .FirstOrDefault();

        //        if (adminDeptVenue != null)
        //        {
        //            int totalAdminIncome = adminCommisionList.Sum(x => x.total);
        //            result.rowHeader.Where(x => x.id == -1).FirstOrDefault().Total +=
        //                totalAdminIncome;
        //            adminDeptVenue.expenseHeads.Add(
        //                new BudgetArazExpenseHead
        //                {
        //                    id = -1,
        //                    isConcerning = false,
        //                    isCapital = false,
        //                    total = totalAdminIncome,
        //                    items = adminCommisionList,
        //                    name = "Admin Income",
        //                    verified = true,
        //                    isExpense = false,
        //                }
        //            );
        //            adminDept
        //                .deptVenues.Where(y => y.id == -11)
        //                .FirstOrDefault()
        //                ?.expenseHeads.Add(
        //                    new BudgetArazExpenseHead
        //                    {
        //                        id = -1,
        //                        isConcerning = false,
        //                        isCapital = false,
        //                        total = adminCommisionList.Sum(x => x.total),
        //                        name = "Admin Income",
        //                        verified = true,
        //                    }
        //                );
        //            adminDept.total -= totalAdminIncome;
        //        }

        //        BudgetArazDeptVenue grandTotal = new BudgetArazDeptVenue
        //        {
        //            id = 0,
        //            name = "Grand Total",
        //            deptId = 0,
        //            deptName = "Total",
        //        };
        //        grandTotal.expenseHeads = new List<BudgetArazExpenseHead>();
        //        foreach (BudgetSummaryRowHeader y in result.rowHeader)
        //        {
        //            grandTotal.expenseHeads.Add(
        //                new BudgetArazExpenseHead
        //                {
        //                    id = y.id,
        //                    name = y.name,
        //                    total = y.Total,
        //                    isCapital = y.isCapital,
        //                    isExpense = y.isExpense
        //                }
        //            );
        //        }
        //        grandTotal.total = grandTotal.expenseHeads.Sum(x => x.total);
        //        result.summary.Add(
        //            new BudgetArazDept
        //            {
        //                id = 0,
        //                name = "Total",
        //                total = grandTotal.total,
        //                deptVenues = new List<BudgetArazDeptVenue> { grandTotal }
        //            }
        //        );

        //        result.colHeader.ForEach(x =>
        //        {
        //            x.deptVenue = null;
        //        });
        //        foreach (BudgetSummaryColHeaderGroup x in result.colHeaderGroup)
        //        {
        //            List<BudgetSummaryColHeader> t = result
        //                .colHeader.Where(y => y.groupId == x.id)
        //                .ToList();
        //            if (t.Count == 0)
        //            {
        //                continue;
        //            }
        //            x.count = result.colHeader.Where(y => y.groupId == x.id).Count() + 1;
        //            int index = result.colHeader.IndexOf(t.Last());
        //            result.colHeader.Insert(
        //                index + 1,
        //                new BudgetSummaryColHeader
        //                {
        //                    id = x.id * -1,
        //                    groupId = x.id,
        //                    name = "Total",
        //                    show = true,
        //                    deptVenueName = "Total: " + x.name,
        //                    color = t[0].color,
        //                }
        //            );
        //        }
        //        ;

        //        result.colHeaderGroup.Add(
        //            new BudgetSummaryColHeaderGroup
        //            {
        //                id = 0,
        //                name = "Total",
        //                show = true,
        //                count = 1,
        //                color = "#f3f4f6",
        //            }
        //        );
        //        result.colHeader.Add(
        //            new BudgetSummaryColHeader
        //            {
        //                id = 0,
        //                name = "Grand Total",
        //                deptVenueName = "Grand Total",
        //                show = true,
        //                groupId = 0,
        //                color = "#f3f4f6",
        //            }
        //        );
        //        result.rowHeader.ForEach(x => x.expensehead = null);
        //        Console.WriteLine(result.colHeaderGroup.Count());
        //        return result;
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.ToString());
        //    }
        //}

        public BudgetArazSummaryModels getBudgetArazSummary(AuthUser authuser, bool audited)
        {
            //List<dept_venue> deptvenues = _context
            //     .dept_venue.Where(x => x.status == "active")
            //     .Include(x => x.venue)
            //     .Include(x => x.registrationform_dropdown_set)
            //        .ThenInclude(x => x.program)
            //     .Include(x => x.registrationform_dropdown_set)
            //        .ThenInclude(x => x.subprogram)
            //     .Include(x => x.registrationform_dropdown_set)
            //        .ThenInclude(x => x.venue)
            //     .ToList();

            //Console.WriteLine("This is the deptvenues query");

            List<mz_expense_procurement_baseitem> baseitems = _context
                .mz_expense_procurement_baseitem.Where(x => x.status == true)
                .ToList();

            List<mz_expense_budget_araz_monthly> monthlyEntries = _context.mz_expense_budget_araz_monthly.ToList();

            List<mz_expense_estimate_student_monthly> incomeMonthlyEntries = _context.mz_expense_estimate_student_monthly.ToList();

            List<mz_student_feecategory> feecategories = _context.mz_student_feecategory.ToList();

            Dictionary<string, string> deptColors = new Dictionary<string, string>();

            deptColors.Add("NISAB", "#fed7aa");
            deptColors.Add("ELEARNING", "#fde68a");
            deptColors.Add("MUKHAYYAM", "#bfdbfe");
            deptColors.Add("SHOUN AMMAH", "#99f6e4");
            deptColors.Add("MAUZE TAHFEEZ", "#d9f99d");
            deptColors.Add("ADMINISTRATION", "#BD8B9C");

            try
            {
                int financialYear = Int32.Parse(
                    _context
                        .global_constant.Where(x => x.key == "budgetFinancialYear")
                        .FirstOrDefault()
                        .value
                );
                BudgetArazSummaryModels result = new BudgetArazSummaryModels();
                result.colHeaderGroup = new List<BudgetSummaryColHeaderGroup>();
                result.colHeader = new List<BudgetSummaryColHeader>();
                result.rowHeader = new List<BudgetSummaryRowHeader>();
                result.sectionHeader = new List<BudgetSummarySection>();

                List<BudgetArazItem> adminCommisionList = new List<BudgetArazItem>();

                result.rowHeader.Add(
                    new BudgetSummaryRowHeader
                    {
                        id = -1,
                        name = "Students Hub Raqm",
                        isIncluded = true,
                        Total = 0,
                        isCapital = false,
                        isExpense = false,
                        show = true,
                    }
                );
                //List<user_dept_venue_baseitem> udv = _context
                //    .user_dept_venue_baseitem.Where(x => x.itsId == authuser.ItsId)
                //    .ToList();

                var udv1 = _context.user_dept_venue_baseitem
                            .Where(x => x.itsId == authuser.ItsId)
                            .Join(
                                _context.dept_venue.Where(x => x.status == "Active"),
                                udv => udv.dept_venueId,
                                dv => dv.id,
                                (udv, dv) => new {
                                    udvs = udv,
                                    deptVenue = dv,
                                    venue = dv.venue,
                                    regSet = dv.registrationform_dropdown_set
                                }
                            )
                            .ToList();

                var subprogram = _context.registrationform_subprograms.ToList();

                var budget_araz = _context.mz_expense_budget_araz.Include(x => x.deptVenue).ToList();

                List<mz_expense_budget_araz> mainBudget = _context
                    .mz_expense_budget_araz.Where(x => x.financialYear == financialYear)
                    .Include(x => x.mz_expense_budget_issue_logs)
                    .Include(x => x.deptVenue)
                    .Include(x => x.baseItem)
                    .Include(x => x.item)
                    .AsNoTracking()
                    .ToList();

                System.Diagnostics.Debug.WriteLine($"mainBudget: {System.Text.Json.JsonSerializer.Serialize(mainBudget.Count)}");

                List<mz_expense_estimate_student> incomeEstimate = _context
                    .mz_expense_estimate_student.Where(x => x.financialYear == financialYear)
                    .Include(x => x.mz_expense_student_budget_issue_logs)                    
                    .ToList();

                List<mz_student_feecategory> incomeEntries = _context.mz_student_feecategory.ToList();
                //int i = 0;
                foreach (var use_dept in udv1)
                {
                    foreach (var dept in use_dept.regSet)
                    {
                        var classId = subprogram.FirstOrDefault(x => x.id == dept.subprogramId);
                        //if (result.colHeaderGroup == null)
                        //{
                        if (!result.colHeaderGroup.Any(x => x.id == use_dept.deptVenue.venue.Id))
                        {
                            result.colHeaderGroup.Add(new BudgetSummaryColHeaderGroup
                            {
                                id = use_dept.deptVenue.venue.Id,
                                name = use_dept.deptVenue.venueName,
                                deptVenueName = use_dept.deptVenue.deptName,
                                count = 0,
                                show = true,
                                isIncluded = true,
                                color = getColheaderColor(deptColors, use_dept.deptVenue.deptName),
                                school = use_dept.venue.CampVenue
                            });
                        }
                        if (!result.colHeader.Any(x => x.id == use_dept.deptVenue.id && x.psetId == dept.id))
                        {
                            result.colHeader.Add(new BudgetSummaryColHeader
                            {
                                id = use_dept.deptVenue.id,
                                name = use_dept.deptVenue.deptName,
                                school = use_dept.deptVenue.venueName,
                                count = 0,
                                className = classId.name,
                                deptVenue = use_dept.deptVenue,
                                groupId = use_dept.venue.Id,
                                isIncluded = false,
                                deptVenueName = use_dept.deptVenue.deptName + "_" + use_dept.deptVenue.venue.CampVenue,
                                show = true,
                                color = getColheaderColor(deptColors, use_dept.deptVenue.deptName),
                                psetId = dept.id
                            });
                        }
                        result.colHeader = result.colHeader.OrderBy(x => x.groupId).ToList();
                        result.colHeaderGroup = result.colHeaderGroup.OrderBy(x => x.id).ToList();
                        //i++;
                    }

                    mz_expense_procurement_baseitem baseItem = baseitems.Where(x => x.id == use_dept.udvs.baseItemId).FirstOrDefault();

                    if (baseItem != null && !result.rowHeader.Any(x => x.id == baseItem.id))
                    {
                        result.rowHeader.Add(
                            new BudgetSummaryRowHeader
                            {
                                id = baseItem.id,
                                name = baseItem.name,
                                expensehead = baseItem,
                                isIncluded = true,
                                //Total = item.amountPerUom,
                                isCapital = baseItem.isCapital,
                                isExpense = !baseItem.isIncome,
                                isNonOpIncome = baseItem.isNonOpIncome,
                                isDawatReceipt = baseItem.isDawatReceipt,
                                isOtherReceipt = baseItem.isOtherReceipt,
                                isOtherPayment = baseItem.isOtherPayment,
                                show = true,
                                //section = [item.deptVenue.deptName]
                            }
                        );
                    }
                }                

                var res = result.rowHeader.ToList();

                foreach (var item in budget_araz)
                {
                    var matchfound = result.rowHeader.FirstOrDefault(x => x.id == item.baseItemId);

                    if (matchfound != null)
                    {
                        //matchfound.Total += item.amountPerUom;
                        var schoolFound = matchfound.schools.FirstOrDefault(x => x.school_name == item.deptVenue.venueName);
                        if (schoolFound == null)
                        {
                            schoolFound = new School
                            {
                                school_name = item.deptVenue.venueName
                            };
                            matchfound.schools.Add(schoolFound);
                        }
                        schoolFound.total_section.Add(new total_section
                        {
                            section = item.deptVenue.deptName + "_" + item.deptVenue.venueName,
                            total = item.amountPerUom
                        });

                        schoolFound.total_section.OrderBy(x => x.section);
                    }
                }

                //result.)

                result.rowHeader = result
                    .rowHeader.OrderBy(x => x.isExpense)
                    .ThenBy(x => x.isCapital)
                    .ToList();

                // budgetSummary code here...

                result.summary = new List<BudgetArazDept>();

                for (int i = 0; i < result.colHeaderGroup.Count; i++)
                {
                    BudgetArazDept dept = new BudgetArazDept()
                    {
                        name = result.colHeaderGroup[i].name,
                        id = result.colHeaderGroup[i].id,
                    };
                    List<mz_expense_budget_araz> budget = new List<mz_expense_budget_araz>();

                    mainBudget.ForEach(x =>
                    {
                        //System.Diagnostics.Debug.WriteLine($"dept.id: {System.Text.Json.JsonSerializer.Serialize(dept.id)}");

                        if (x.deptVenue.venueId == dept.id)
                        {
                            budget.Add(x);
                        }                        
                    });
                    //System.Diagnostics.Debug.WriteLine($"mainBudget.id: {System.Text.Json.JsonSerializer.Serialize(budget)}");

                    dept.deptVenues = new List<BudgetArazDeptVenue>();
                    for (int j = 0; j < result.colHeader.Count; j++)
                    {                        
                        dept_venue d = result.colHeader[j].deptVenue;
                        //Console.WriteLine(d.registrationform_dropdown_set.Count());
                        if (d.venueId != dept.id)
                        {
                            continue;
                        }
                        if (d.status != "active")
                        {
                            continue;
                        }
                        BudgetArazDeptVenue deptVenue = new BudgetArazDeptVenue()
                        {
                            id = d.id,
                            deptId = d.venueId ?? 0, // d.deptId
                            deptName = d.deptName,
                            name = d.deptName + "_" + d.venueName,
                            venueName = result.colHeader[j].name,
                            masterDeptName = d.masterDeptName,
                            className = result.colHeader[j].className,
                            pset = result.colHeader[j].psetId
                        };

                        deptVenue.expenseHeads = new List<BudgetArazExpenseHead>();

                        List<mz_expense_budget_araz> bbi = budget
                            .Where(x => x.deptVenueId == d.id && x.psetId == result.colHeader[j].psetId)
                            .GroupBy(x => x.baseItemId)
                            .Select(x => x.FirstOrDefault())
                            .ToList();
                        //Console.WriteLine(bbi);
                        for (int k = 0; k < bbi.Count; k++)
                        {
                            BudgetSummaryRowHeader expHead = result
                                .rowHeader.Where(x => x.id == bbi[k].baseItemId)
                                .FirstOrDefault();
                            if (expHead == null)
                            {
                                continue;
                            }

                            mz_expense_procurement_baseitem obi = expHead.expensehead;
                            List<mz_expense_budget_araz> bitems = budget
                                .Where(x => x.baseItemId == obi.id && x.deptVenueId == d.id && x.psetId == result.colHeader[j].psetId)
                                .ToList();

                            if (bitems.Count > 0)
                            {
                                BudgetArazExpenseHead expenseHead = new BudgetArazExpenseHead
                                {
                                    id = obi.id,
                                    name = obi.name,
                                    status = obi.status,
                                    isCapital = obi.isCapital,
                                    isConcerning = false,
                                    verified = true,
                                    isExpense = !obi.isIncome,
                                    isNonOpIncome = obi.isNonOpIncome,
                                    isDawatReceipt = obi.isDawatReceipt,
                                    isOtherReceipt = obi.isOtherReceipt,
                                    isOtherPayment = obi.isOtherPayment,
                                };
                                expenseHead.items = new List<BudgetArazItem>();
                                int c = 1;
                                foreach (mz_expense_budget_araz x in bitems)
                                {
                                    //Console.WriteLine($"This is Items: {x.id}");
                                    BudgetArazItem item = new BudgetArazItem
                                    {
                                        id = x.id,
                                        itemId = x.item.id,
                                        name = x.item.name,
                                        type = x.item.type,
                                        description = x.justification,
                                        perUnitAmt = x.amountPerUom,
                                        quantity = x.quantity,
                                        uom = x.uom,
                                        srno = c,
                                        remark = x.remarks_admin,
                                        isConcerning = false,
                                        verified = true,
                                        hasIssues = false,
                                        isExpense = !obi.isIncome,
                                        isNonOpIncome = obi.isNonOpIncome,
                                        isDawatReceipt = obi.isDawatReceipt,
                                        isOtherReceipt = obi.isOtherReceipt,
                                        isOtherPayment = obi.isOtherPayment,
                                        stage = x.stage,
                                    };

                                    item.months = new List<mz_expense_budget_araz_monthly>();

                                    List<mz_expense_budget_araz_monthly> month = monthlyEntries.Where(y => y.budget_araz_id == x.id).ToList();

                                    foreach (var monthitem in month)
                                    {
                                        item.months.Add(monthitem);
                                    }

                                    if (x.mz_expense_budget_issue_logs.Count > 0)
                                    {
                                        item.hasIssues = true;
                                    }
                                    if (audited)
                                    {
                                        if (x.stage != "Audited")
                                        {
                                            expenseHead.verified = false;
                                            item.verified = false;
                                        }
                                    }
                                    if (x.stage.Contains("Initia"))
                                    {
                                        if (audited)
                                        {
                                            continue;
                                        }
                                        expenseHead.verified = false;
                                        item.verified = false;
                                        if (x.stage.Contains("concern"))
                                        {
                                            expenseHead.isConcerning = true;
                                            item.isConcerning = true;
                                        }
                                    }


                                    item.total = item.perUnitAmt * item.quantity ?? 0;
                                    expenseHead.items.Add(item);
                                    c++;
                                }
                                ;
                                if (audited)
                                {
                                    if (expenseHead.items.Any(x => x.stage.Contains("Initia")))
                                    {
                                        continue;
                                    }
                                }
                                expenseHead.total = expenseHead.items.Sum(x => x.total);
                                deptVenue.expenseHeads.Add(expenseHead);
                                expHead.isIncluded = true;
                                expHead.Total += expenseHead.total;
                            }
                        }



                        List<mz_expense_estimate_student> deptStudentEstimate = incomeEstimate
                            .Where(x => x.psetId == result.colHeader[j].psetId)
                            .ToList();
                        //List<mz_expense_estimate_student> deptStudentEstimate = incomeEstimate.ToList();

                        //System.Diagnostics.Debug.WriteLine($"This is deptestimatecount {System.Text.Json.JsonSerializer.Serialize(deptStudentEstimate)}");

                        if (deptStudentEstimate.Count > 0)
                        {
                            BudgetArazExpenseHead incomeHead = new BudgetArazExpenseHead
                            {
                                id = -1,
                                name = "Revenue",
                                total = 0,
                                isExpense = false,
                                verified = true
                            };
                            incomeHead.items = new List<BudgetArazItem>();
                            int sr = 1;

                            for (int k = 0; k < deptStudentEstimate.Count; k++)
                            {
                                var fName = incomeEntries.Where(x => x.id == deptStudentEstimate[k].fcId).FirstOrDefault();
                                BudgetArazItem item = new BudgetArazItem
                                {
                                    name = fName.categoryName,
                                    id = deptStudentEstimate[k].id,
                                    type = deptStudentEstimate[k].fcId.ToString(),
                                    description = deptStudentEstimate[k].remarks,
                                    perUnitAmt = deptStudentEstimate[k].feesAmount ?? 0,
                                    quantity = deptStudentEstimate[k].studentCountPerMonth ?? 0,
                                    uom = deptStudentEstimate[k].duration.ToString(),
                                    srno = sr,
                                    remark = deptStudentEstimate[k].remarks_admin,
                                    isConcerning = false,
                                    verified = true,
                                    hasIssues = false,
                                    isExpense = false,
                                    stage = deptStudentEstimate[k].stage
                                };

                                string feecategoryName =
                                    feecategories
                                        .Where(x => x.id == deptStudentEstimate[k].fcId)
                                        .FirstOrDefault()
                                        ?.categoryName ?? "";
                                registrationform_dropdown_set pset = d.registrationform_dropdown_set.Where(x => x.id == deptStudentEstimate[k].psetId).FirstOrDefault();
                                //item.name =
                                //    pset.program.name
                                //    + "-"
                                //    + pset.program.name
                                //    + "-"
                                //    + feecategoryName;
                                item.total =
                                    item.perUnitAmt
                                    * item.quantity ?? 0
                                    * deptStudentEstimate[k].duration;

                                if (
                                    deptStudentEstimate[k]
                                        .mz_expense_student_budget_issue_logs
                                        .Count > 0
                                )
                                {
                                    item.hasIssues = true;
                                }
                                if (audited)
                                {
                                    if (deptStudentEstimate[k].stage != "Audited")
                                    {
                                        incomeHead.verified = false;
                                        item.verified = false;
                                    }                                    
                                }
                                
                                if (deptStudentEstimate[k].stage.Contains("Initia"))
                                {
                                    if (audited)
                                    {
                                        continue;
                                    }
                                    incomeHead.verified = false;
                                    item.verified = false;
                                    if (deptStudentEstimate[k].stage.Contains("concern"))
                                    {
                                        incomeHead.isConcerning = true;
                                        item.isConcerning = true;
                                    }
                                }
                                //List<studentFeesMonthly> monthss = _context.mz_expense_estimate_student_monthly.Where().ToList();
                                var incomeMonth = incomeMonthlyEntries.Where(ees => ees.estimate_student_id == deptStudentEstimate[k].sfcp_id).ToList();

                                foreach (var iMonth in incomeMonth)
                                {
                                    var monthss = new studentFeesMonthly
                                    {
                                        fees_per_student = iMonth.fees_per_student,
                                        student_count = iMonth.student_count,
                                        month = iMonth.month
                                    };

                                    item.incomeMonths.Add(monthss);
                                }

                                incomeHead.total += item.total;
                                incomeHead.items.Add(item);
                                sr++;
                            }
                            //System.Diagnostics.Debug.WriteLine($"This is incomeHead: {System.Text.Json.JsonSerializer.Serialize(incomeHead.items)}");
                            //Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(incomeHead.items));

                            //foreach (var item in incomeHead.items)
                            //{
                            //    Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(item));
                            //}
                            //int adminCommision = (int)(((double)incomeHead.total) * -0.025);
                            int adminCommision = (int)(((double)incomeHead.total) * -0.00);

                            BudgetArazItem adminIncomeCommission = new BudgetArazItem();
                            //if (d.deptId == 3 || d.deptId == 2)
                            //{
                            //    adminCommision = (int)(((double)incomeHead.total) * -0.03);
                            //    adminIncomeCommission = new BudgetArazItem
                            //    {
                            //        id = -1,
                            //        type = "3% Admin Expense",
                            //        description = "3% Admin Expense",
                            //        perUnitAmt = adminCommision,
                            //        quantity = 1,
                            //        uom = "Percentage",
                            //        srno = sr,
                            //        remark = "Administration Expense of 3%",
                            //        isConcerning = false,
                            //        verified = true,
                            //        hasIssues = false,
                            //        isExpense = false,
                            //        stage = "",
                            //        total = adminCommision,
                            //        name = "Admin Expense"
                            //    };
                            //}
                            //else
                            //{
                            //    adminIncomeCommission = new BudgetArazItem
                            //    {
                            //        id = -1,
                            //        type = "2.5% Admin Expense",
                            //        description = "2.5% Admin Expense",
                            //        perUnitAmt = adminCommision,
                            //        quantity = 1,
                            //        uom = "Percentage",
                            //        srno = sr,
                            //        remark = "Administration Expense of 2.5%",
                            //        isConcerning = false,
                            //        verified = true,
                            //        hasIssues = false,
                            //        isExpense = false,
                            //        stage = "",
                            //        total = adminCommision,
                            //        name = "Admin Expense"
                            //    };
                            //}

                            adminIncomeCommission = new BudgetArazItem
                            {
                                id = -1,
                                type = "3% Admin Expense",
                                description = "3% Admin Expense",
                                perUnitAmt = adminCommision,
                                quantity = 1,
                                uom = "Percentage",
                                srno = sr,
                                remark = "Administration Expense of 3%",
                                isConcerning = false,
                                verified = true,
                                hasIssues = false,
                                isExpense = false,
                                stage = "",
                                total = adminCommision,
                                name = "Admin Expense"
                            };
                            incomeHead.total += adminIncomeCommission.total;
                            incomeHead.items.Add(adminIncomeCommission);

                            adminCommisionList.Add(
                                new BudgetArazItem
                                {
                                    id = 0 - d.id,
                                    type = "",
                                    description =
                                        "Administration Income from "
                                        + d.deptName
                                        + "_"
                                        + d.venueName,
                                    perUnitAmt = adminCommision * -1,
                                    quantity = 1,
                                    uom = "Percentage",
                                    srno = sr,
                                    remark =
                                        "Administration Income from "
                                        + d.deptName
                                        + "_"
                                        + d.venueName,
                                    isConcerning = false,
                                    verified = true,
                                    hasIssues = false,
                                    isExpense = false,
                                    stage = "",
                                    total = adminCommision * -1,
                                    name = "Admin Income"
                                }
                            );

                            if (audited)
                            {
                                if (incomeHead.items.Any(x => x.stage.Contains("Initia")))
                                {
                                    continue;
                                }
                            }

                            //Console.WriteLine(incomeHead.name);

                            deptVenue.expenseHeads.Add(incomeHead);

                            BudgetSummaryRowHeader rh = result
                                .rowHeader.Where(x => x.id == -1)
                                .FirstOrDefault();
                            rh.isIncluded = true;
                            rh.Total += incomeHead.total;
                        }

                        deptVenue.total = deptVenue.expenseHeads.Where(x => x.isExpense).Sum(x => x.total) - deptVenue.expenseHeads.Where(x => !x.isExpense).Sum(x => x.total);
                        result.colHeader[j].isIncluded = true;
                        dept.deptVenues.Add(deptVenue);
                    }
                    if (dept.deptVenues.Count > 0)
                    {
                        BudgetArazDeptVenue deptTotal = new BudgetArazDeptVenue
                        {
                            id = 0 - dept.id,
                            pset = 0 - dept.id,
                            name = "Total",
                            deptId = dept.id,
                            deptName = dept.name,
                            total = dept.deptVenues.Sum(x => x.total)
                        };

                        deptTotal.expenseHeads = new List<BudgetArazExpenseHead>();
                        string errMsg = "";
                        try
                        {
                            foreach (BudgetArazDeptVenue x in dept.deptVenues)
                            {
                                foreach (BudgetArazExpenseHead y in x.expenseHeads)
                                {
                                    if (!deptTotal.expenseHeads.Any(z => z.id == y.id))
                                    {
                                        deptTotal.expenseHeads.Add(
                                            new BudgetArazExpenseHead
                                            {
                                                id = y.id,
                                                name = y.name,
                                                status = y.status,
                                                total = y.total,
                                                isCapital = y.isCapital,
                                                isExpense = y.isExpense,
                                                isNonOpIncome = y.isNonOpIncome,
                                                isDawatReceipt = y.isDawatReceipt,
                                                isOtherReceipt = y.isOtherReceipt,
                                                isOtherPayment = y.isOtherPayment,
                                            }
                                        );
                                    }
                                    else
                                    {
                                        deptTotal
                                            .expenseHeads.Where(z => z.id == y.id)
                                            .FirstOrDefault()
                                            .total += y.total;
                                    }
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            //errMsg += e.Message + ", ";
                            throw new Exception(e.ToString());
                        }
                        dept.deptVenues.Add(deptTotal);
                        dept.total = deptTotal.total;
                        result.colHeaderGroup[i].isIncluded = true;
                        result.summary.Add(dept);
                    }
                }

                

                foreach (BudgetSummaryColHeaderGroup x in result.colHeaderGroup)
                {
                    List<BudgetSummaryColHeader> t = result
                        .colHeader.Where(y => y.groupId == x.id)
                        .ToList();
                    if (t.Count == 0)
                    {
                        continue;
                    }
                    x.count = result.colHeader.Where(y => y.groupId == x.id).Count() + 1;
                    int index = result.colHeader.IndexOf(t.Last());
                    result.colHeader.Insert(
                        index + 1,
                        new BudgetSummaryColHeader
                        {
                            id = x.id * -1,
                            groupId = x.id,
                            name = "Total",
                            className = "Total",
                            psetId = x.id * -1,
                            show = true,
                            deptVenueName = "Total: " + x.school,
                            color = t[0].color,
                            school = x.school
                        }
                    );
                }
                result.colHeader.ForEach(x => x.deptVenue = null);
                result.rowHeader.ForEach(x => x.expensehead = null);
                return result;
            }
            catch (Exception e)
            {
                //errMsg += e.Message + ", ";
                throw new Exception(e.ToString());
            }
        }

        public BudgetArazSummaryModels getBudgetArazSummary_(AuthUser authuser, bool audited)
        {
            List<dept_venue> deptvenues = _context
                 .dept_venue.Where(x => x.status == "active")
                 .Include(x => x.venue)
                 .Include(x => x.registrationform_dropdown_set)
                    .ThenInclude(x => x.program)
                 .Include(x => x.registrationform_dropdown_set)
                    .ThenInclude(x => x.subprogram)
                 .Include(x => x.registrationform_dropdown_set)
                    .ThenInclude(x => x.venue)
                 .ToList();

            Console.WriteLine("This is the deptvenues query");

            List<mz_expense_procurement_baseitem> baseitems = _context
                .mz_expense_procurement_baseitem.Where(x => x.status == true)
                .ToList();

            List<mz_student_feecategory> feecategories = _context.mz_student_feecategory.ToList();

            Dictionary<string, string> deptColors = new Dictionary<string, string>();

            deptColors.Add("NISAB", "#fed7aa");
            deptColors.Add("ELEARNING", "#fde68a");
            deptColors.Add("MUKHAYYAM", "#bfdbfe");
            deptColors.Add("SHOUN AMMAH", "#99f6e4");
            deptColors.Add("MAUZE TAHFEEZ", "#d9f99d");
            deptColors.Add("ADMINISTRATION", "#BD8B9C");

            try
            {
                int financialYear = Int32.Parse(
                    _context
                        .global_constant.Where(x => x.key == "budgetFinancialYear")
                        .FirstOrDefault()
                        .value
                );
                BudgetArazSummaryModels result = new BudgetArazSummaryModels();
                result.colHeaderGroup = new List<BudgetSummaryColHeaderGroup>();
                result.colHeader = new List<BudgetSummaryColHeader>();
                result.rowHeader = new List<BudgetSummaryRowHeader>();
                result.sectionHeader = new List<BudgetSummarySection>();

                List<BudgetArazItem> adminCommisionList = new List<BudgetArazItem>();

                result.rowHeader.Add(
                    new BudgetSummaryRowHeader
                    {
                        id = -1,
                        name = "Students Hub Raqm",
                        isIncluded = true,
                        Total = 0,
                        isCapital = false,
                        isExpense = false,
                        show = true,
                    }
                );
                List<user_dept_venue_baseitem> udv = _context
                    .user_dept_venue_baseitem.Where(x => x.itsId == authuser.ItsId)
                    .ToList();
                for (int i = 0; i < udv.Count(); i++)
                {
                    dept_venue dv = deptvenues.Where(x => x.id == udv[i].dept_venueId).FirstOrDefault();

                    foreach (var item in dv.registrationform_dropdown_set)
                    {                        
                        if (dv != null )
                        {
                            if (!result.colHeader.Any(x => x.id == dv.id && x.className == item.subprogram.name))
                            {
                                BudgetSummaryColHeader colHeader = new BudgetSummaryColHeader
                                {
                                    id = dv.id,
                                    name = dv.deptName,
                                    school = dv.venueName,
                                    className = item.subprogram.name,
                                    deptVenue = dv,
                                    groupId = dv.venue.Id,
                                    isIncluded = false,
                                    deptVenueName = dv.deptName + "_" + dv.venue.CampVenue + "_" + item.subprogram.name,
                                    show = true,
                                    psetId = item.id
                                };
                                colHeader.color = getColheaderColor(deptColors, dv.deptName);

                                result.colHeader.Add(colHeader);
                            }
                            if (!result.colHeaderGroup.Any(x => x.id == dv.venue.Id))
                            {
                                BudgetSummaryColHeaderGroup colHeaderGroup =
                                    new BudgetSummaryColHeaderGroup
                                    {
                                        id = dv.venue.Id,
                                        name = dv.venue.displayName,
                                        isIncluded = false,
                                        show = true,
                                        school = dv.venue.CampVenue
                                    };
                                colHeaderGroup.color = getColheaderColor(deptColors, dv.deptName);
                                result.colHeaderGroup.Add(colHeaderGroup);

                            }
                        }

                        result.colHeader = result.colHeader.OrderBy(x => x.groupId).ToList();
                        result.colHeaderGroup = result.colHeaderGroup.OrderBy(x => x.id).ToList();
                    }
                    Console.WriteLine();
                    

                    mz_expense_procurement_baseitem baseItem = baseitems.Where(x => x.id == udv[i].baseItemId).FirstOrDefault();

                    if (baseItem != null && !result.rowHeader.Any(x => x.id == baseItem.id))
                    {
                        result.rowHeader.Add(
                            new BudgetSummaryRowHeader
                            {
                                id = baseItem.id,
                                name = baseItem.name,
                                expensehead = baseItem,
                                isIncluded = true,
                                //Total = item.amountPerUom,
                                isCapital = baseItem.isCapital,
                                isExpense = !baseItem.isIncome,
                                show = true,
                                //section = [item.deptVenue.deptName]
                            }
                        );
                    }
                }

                var budget_araz = _context.mz_expense_budget_araz.Include(x => x.deptVenue).ToList();
                var res = result.rowHeader.ToList();

                foreach (var item in budget_araz)
                {
                    var matchfound = result.rowHeader.FirstOrDefault(x => x.id == item.baseItemId);

                    if (matchfound != null)
                    {
                        //matchfound.Total += item.amountPerUom;
                        var schoolFound = matchfound.schools.FirstOrDefault(x => x.school_name == item.deptVenue.venueName);
                        if (schoolFound == null)
                        {
                            schoolFound = new School
                            {
                                school_name = item.deptVenue.venueName
                            };
                            matchfound.schools.Add(schoolFound);
                        }
                        schoolFound.total_section.Add(new total_section
                        {
                            section = item.deptVenue.deptName + "_" + item.deptVenue.venueName,
                            total = item.amountPerUom
                        });

                        schoolFound.total_section.OrderBy(x => x.section);
                    }
                }

                //result.)

                result.rowHeader = result
                    .rowHeader.OrderBy(x => x.isExpense)
                    .ThenBy(x => x.isCapital)
                    .ToList();

                result.summary = new List<BudgetArazDept>();
                List<mz_expense_budget_araz> mainBudget = _context
                    .mz_expense_budget_araz.Where(x => x.financialYear == financialYear)
                    .Include(x => x.mz_expense_budget_issue_logs)
                    .Include(x => x.deptVenue)
                    .Include(x => x.baseItem)
                    .Include(x => x.item)
                    .ToList();
                Console.WriteLine("Above is mainbudget query.");
                Console.WriteLine(mainBudget.Count());
                                
                Console.WriteLine("IncomeHead Query");
                //Console.WriteLine(result.rowHeader[1].Total);
                for (int i = 0; i < result.colHeaderGroup.Count; i++)
                {
                    BudgetArazDept dept = new BudgetArazDept()
                    {
                        name = result.colHeaderGroup[i].name,
                        id = result.colHeaderGroup[i].id,
                    };
                    List<mz_expense_budget_araz> budget = new List<mz_expense_budget_araz>();
                    mainBudget.ForEach(x =>
                    {
                        if (x.deptVenue.venueId == dept.id)
                        {
                            budget.Add(x);
                        }
                        ;
                    });
                    dept.deptVenues = new List<BudgetArazDeptVenue>();
                    for (int j = 0; j < result.colHeader.Count; j++)
                    {
                        List<mz_expense_estimate_student> incomeEstimate = _context
                    .mz_expense_estimate_student.Where(x => x.financialYear == financialYear)
                    .Include(x => x.mz_expense_student_budget_issue_logs)
                    .Where(y => y.psetId == result.colHeader[j].psetId)
                    .ToList();
                        dept_venue d = result.colHeader[j].deptVenue;
                        //Console.WriteLine(d.registrationform_dropdown_set.Count());
                        if (d.venueId != dept.id)
                        {
                            continue;
                        }
                        if (d.status != "active")
                        {
                            continue;
                        }
                        BudgetArazDeptVenue deptVenue = new BudgetArazDeptVenue()
                        {
                            id = d.id,
                            deptId = d.venueId ?? 0, // d.deptId
                            deptName = d.deptName,
                            name = d.deptName + "_" + d.venueName,
                            venueName = result.colHeader[j].name,
                            masterDeptName = d.masterDeptName,
                            className = result.colHeader[j].className,
                            pset = result.colHeader[j].psetId
                        };

                        deptVenue.expenseHeads = new List<BudgetArazExpenseHead>();

                        List<mz_expense_budget_araz> bbi = budget
                            .Where(x => x.deptVenueId == d.id && x.psetId == result.colHeader[j].psetId)
                            .GroupBy(x => x.baseItemId)
                            .Select(x => x.FirstOrDefault())
                            .ToList();
                        Console.WriteLine(bbi);
                        for (int k = 0; k < bbi.Count; k++)
                        {
                            BudgetSummaryRowHeader expHead = result
                                .rowHeader.Where(x => x.id == bbi[k].baseItemId)
                                .FirstOrDefault();
                            if (expHead == null)
                            {
                                continue;
                            }

                            mz_expense_procurement_baseitem obi = expHead.expensehead;
                            List<mz_expense_budget_araz> bitems = budget
                                .Where(x => x.baseItemId == obi.id && x.deptVenueId == d.id && x.psetId == result.colHeader[j].psetId)
                                .ToList();

                            if (bitems.Count > 0)
                            {
                                BudgetArazExpenseHead expenseHead = new BudgetArazExpenseHead
                                {
                                    id = obi.id,
                                    name = obi.name,
                                    status = obi.status,
                                    isCapital = obi.isCapital,
                                    isConcerning = false,
                                    verified = true,
                                    isExpense = !obi.isIncome,
                                };
                                expenseHead.items = new List<BudgetArazItem>();
                                int c = 1;
                                foreach (mz_expense_budget_araz x in bitems)
                                {
                                    Console.WriteLine($"This is Items: {x.id}");
                                    BudgetArazItem item = new BudgetArazItem
                                    {
                                        id = x.id,
                                        itemId = x.item.id,
                                        name = x.item.name,
                                        type = x.item.type,
                                        description = x.justification,
                                        perUnitAmt = x.amountPerUom,
                                        quantity = x.quantity,
                                        uom = x.uom,
                                        srno = c,
                                        remark = x.remarks_admin,
                                        isConcerning = false,
                                        verified = true,
                                        hasIssues = false,
                                        isExpense = !obi.isIncome,
                                        stage = x.stage,
                                    };

                                    item.months = new List<mz_expense_budget_araz_monthly>();

                                    List<mz_expense_budget_araz_monthly> month = _context.mz_expense_budget_araz_monthly.Where(y => y.budget_araz_id == x.id).ToList();

                                    foreach (var monthitem in month)
                                    {
                                        item.months.Add(monthitem);
                                    }
                                    System.Console.WriteLine($"This is monthItem: {System.Text.Json.JsonSerializer.Serialize(item.months)}");

                                    if (x.mz_expense_budget_issue_logs.Count > 0)
                                    {
                                        item.hasIssues = true;
                                    }
                                    if (audited)
                                    {
                                        if (x.stage != "Audited")
                                        {
                                            expenseHead.verified = false;
                                            item.verified = false;
                                        }
                                    }
                                    if (x.stage.Contains("Initia"))
                                    {
                                        if (audited)
                                        {
                                            continue;
                                        }
                                        expenseHead.verified = false;
                                        item.verified = false;
                                        if (x.stage.Contains("concern"))
                                        {
                                            expenseHead.isConcerning = true;
                                            item.isConcerning = true;
                                        }
                                    }


                                    item.total = item.perUnitAmt * item.quantity ?? 0;
                                    expenseHead.items.Add(item);
                                    c++;
                                }
                                ;
                                if (audited)
                                {
                                    if (expenseHead.items.Any(x => x.stage.Contains("Initia")))
                                    {
                                        continue;
                                    }
                                }
                                expenseHead.total = expenseHead.items.Sum(x => x.total);
                                deptVenue.expenseHeads.Add(expenseHead);
                                expHead.isIncluded = true;
                                expHead.Total += expenseHead.total;
                            }
                        }
                        //Console.WriteLine(result.rowHeader[1].Total);
                        //Console.WriteLine($"DeptStudentEstimate Count: {d.registrationform_dropdown_set.Count}");


                        List<mz_expense_estimate_student> deptStudentEstimate = incomeEstimate
                            .Where(x => d.registrationform_dropdown_set.Any(y => y.id == x.psetId))
                            .ToList();

                        //List<mz_expense_estimate_student> deptStudentEstimate = incomeEstimate.ToList();


                        if (deptStudentEstimate.Count > 0)
                        {
                            BudgetArazExpenseHead incomeHead = new BudgetArazExpenseHead
                            {
                                id = -1,
                                name = "Revenue",
                                total = 0,
                                isExpense = false,
                            };
                            incomeHead.items = new List<BudgetArazItem>();
                            int sr = 1;

                            for (int k = 0; k < deptStudentEstimate.Count; k++)
                            {
                                Console.WriteLine($"IncomeHeads: {deptStudentEstimate[k].id}");
                                BudgetArazItem item = new BudgetArazItem
                                {
                                    id = deptStudentEstimate[k].id,
                                    type = deptStudentEstimate[k].fcId.ToString(),
                                    description = deptStudentEstimate[k].remarks,
                                    perUnitAmt = deptStudentEstimate[k].feesAmount ?? 0,
                                    quantity = deptStudentEstimate[k].studentCountPerMonth ?? 0,
                                    uom = deptStudentEstimate[k].duration.ToString(),
                                    srno = sr,
                                    remark = deptStudentEstimate[k].remarks_admin,
                                    isConcerning = false,
                                    verified = true,
                                    hasIssues = false,
                                    isExpense = false,
                                    stage = deptStudentEstimate[k].stage
                                };

                                string feecategoryName =
                                    feecategories
                                        .Where(x => x.id == deptStudentEstimate[k].fcId)
                                        .FirstOrDefault()
                                        ?.categoryName ?? "";
                                registrationform_dropdown_set pset = d.registrationform_dropdown_set.Where(x => x.id == deptStudentEstimate[k].psetId).FirstOrDefault();
                                item.name =
                                    pset.program.name
                                    + "-"
                                    + pset.program.name
                                    + "-"
                                    + feecategoryName;
                                item.total =
                                    item.perUnitAmt
                                    * item.quantity ?? 0
                                    * deptStudentEstimate[k].duration;

                                if (
                                    deptStudentEstimate[k]
                                        .mz_expense_student_budget_issue_logs
                                        .Count > 0
                                )
                                {
                                    item.hasIssues = true;
                                }
                                if (audited)
                                {
                                    if (deptStudentEstimate[k].stage != "Audited")
                                    {
                                        incomeHead.verified = false;
                                        item.verified = false;
                                    }
                                }
                                if (deptStudentEstimate[k].stage.Contains("Initia"))
                                {
                                    if (audited)
                                    {
                                        continue;
                                    }
                                    incomeHead.verified = false;
                                    item.verified = false;
                                    if (deptStudentEstimate[k].stage.Contains("concern"))
                                    {
                                        incomeHead.isConcerning = true;
                                        item.isConcerning = true;
                                    }
                                }
                                //List<studentFeesMonthly> monthss = _context.mz_expense_estimate_student_monthly.Where().ToList();
                                var incomeMonth = _context.mz_expense_estimate_student_monthly.Where(ees => ees.estimate_student_id == deptStudentEstimate[k].sfcp_id).ToList();

                                foreach(var iMonth in incomeMonth)
                                {
                                    var monthss = new studentFeesMonthly
                                    {
                                        fees_per_student = iMonth.fees_per_student,
                                        student_count = iMonth.student_count,
                                        month = iMonth.month
                                    };

                                    item.incomeMonths.Add(monthss);
                                }

                                incomeHead.total += item.total;
                                incomeHead.items.Add(item);
                                sr++;
                            }
                            System.Diagnostics.Debug.WriteLine($"This is incomeHead: {System.Text.Json.JsonSerializer.Serialize(incomeHead.items)}");
                            Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(incomeHead.items));

                            foreach (var item in incomeHead.items)
                            {
                                Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(item));
                            }
                            //int adminCommision = (int)(((double)incomeHead.total) * -0.025);
                            int adminCommision = (int)(((double)incomeHead.total) * -0.00);

                            BudgetArazItem adminIncomeCommission = new BudgetArazItem();
                            //if (d.deptId == 3 || d.deptId == 2)
                            //{
                            //    adminCommision = (int)(((double)incomeHead.total) * -0.03);
                            //    adminIncomeCommission = new BudgetArazItem
                            //    {
                            //        id = -1,
                            //        type = "3% Admin Expense",
                            //        description = "3% Admin Expense",
                            //        perUnitAmt = adminCommision,
                            //        quantity = 1,
                            //        uom = "Percentage",
                            //        srno = sr,
                            //        remark = "Administration Expense of 3%",
                            //        isConcerning = false,
                            //        verified = true,
                            //        hasIssues = false,
                            //        isExpense = false,
                            //        stage = "",
                            //        total = adminCommision,
                            //        name = "Admin Expense"
                            //    };
                            //}
                            //else
                            //{
                            //    adminIncomeCommission = new BudgetArazItem
                            //    {
                            //        id = -1,
                            //        type = "2.5% Admin Expense",
                            //        description = "2.5% Admin Expense",
                            //        perUnitAmt = adminCommision,
                            //        quantity = 1,
                            //        uom = "Percentage",
                            //        srno = sr,
                            //        remark = "Administration Expense of 2.5%",
                            //        isConcerning = false,
                            //        verified = true,
                            //        hasIssues = false,
                            //        isExpense = false,
                            //        stage = "",
                            //        total = adminCommision,
                            //        name = "Admin Expense"
                            //    };
                            //}

                            adminIncomeCommission = new BudgetArazItem
                            {
                                id = -1,
                                type = "3% Admin Expense",
                                description = "3% Admin Expense",
                                perUnitAmt = adminCommision,
                                quantity = 1,
                                uom = "Percentage",
                                srno = sr,
                                remark = "Administration Expense of 3%",
                                isConcerning = false,
                                verified = true,
                                hasIssues = false,
                                isExpense = false,
                                stage = "",
                                total = adminCommision,
                                name = "Admin Expense"
                            };
                            incomeHead.total += adminIncomeCommission.total;
                            incomeHead.items.Add(adminIncomeCommission);

                            adminCommisionList.Add(
                                new BudgetArazItem
                                {
                                    id = 0 - d.id,
                                    type = "",
                                    description =
                                        "Administration Income from "
                                        + d.deptName
                                        + "_"
                                        + d.venueName,
                                    perUnitAmt = adminCommision * -1,
                                    quantity = 1,
                                    uom = "Percentage",
                                    srno = sr,
                                    remark =
                                        "Administration Income from "
                                        + d.deptName
                                        + "_"
                                        + d.venueName,
                                    isConcerning = false,
                                    verified = true,
                                    hasIssues = false,
                                    isExpense = false,
                                    stage = "",
                                    total = adminCommision * -1,
                                    name = "Admin Income"
                                }
                            );

                            if (audited)
                            {
                                if (incomeHead.items.Any(x => x.stage.Contains("Initia")))
                                {
                                    continue;
                                }
                            }

                            Console.WriteLine(incomeHead.name);

                            deptVenue.expenseHeads.Add(incomeHead);
                            
                            BudgetSummaryRowHeader rh = result
                                .rowHeader.Where(x => x.id == -1)
                                .FirstOrDefault();
                            rh.isIncluded = true;
                            rh.Total += incomeHead.total;
                        }

                        deptVenue.total = deptVenue.expenseHeads.Where(x => x.isExpense).Sum(x => x.total) - deptVenue.expenseHeads.Where(x => !x.isExpense).Sum(x => x.total);
                        result.colHeader[j].isIncluded = true;
                        dept.deptVenues.Add(deptVenue);
                    }
                    if (dept.deptVenues.Count > 0)
                    {
                        BudgetArazDeptVenue deptTotal = new BudgetArazDeptVenue
                        {
                            id = 0 - dept.id,
                            pset = 0 - dept.id,
                            name = "Total",
                            deptId = dept.id,
                            deptName = dept.name,
                            total = dept.deptVenues.Sum(x => x.total)
                        };

                        deptTotal.expenseHeads = new List<BudgetArazExpenseHead>();
                        string errMsg = "";
                        try
                        {
                            foreach (BudgetArazDeptVenue x in dept.deptVenues)
                            {
                                foreach (BudgetArazExpenseHead y in x.expenseHeads)
                                {
                                    if (!deptTotal.expenseHeads.Any(z => z.id == y.id))
                                    {
                                        deptTotal.expenseHeads.Add(
                                            new BudgetArazExpenseHead
                                            {
                                                id = y.id,
                                                name = y.name,
                                                status = y.status,
                                                total = y.total,
                                                isCapital = y.isCapital,
                                                isExpense = y.isExpense,
                                            }
                                        );
                                    }
                                    else
                                    {
                                        deptTotal
                                            .expenseHeads.Where(z => z.id == y.id)
                                            .FirstOrDefault()
                                            .total += y.total;
                                    }
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            //errMsg += e.Message + ", ";
                            throw new Exception(e.ToString());
                        }
                        dept.deptVenues.Add(deptTotal);
                        dept.total = deptTotal.total;
                        result.colHeaderGroup[i].isIncluded = true;
                        result.summary.Add(dept);
                    }
                }



                //result.colHeader = result.colHeader.Where(x => x.isIncluded).ToList();
                //result.colHeaderGroup = result.colHeaderGroup.Where(x => x.isIncluded).ToList();
                result.colHeader = result.colHeader.ToList();
                result.colHeaderGroup = result.colHeaderGroup.ToList();
                //result.rowHeader = result.rowHeader.Where(x => x.isIncluded).ToList();
                Console.WriteLine(result.colHeaderGroup.Count());
                BudgetArazDept adminDept = result.summary.Where(x => x.id == 11).FirstOrDefault();
                BudgetArazDeptVenue adminDeptVenue = adminDept?.deptVenues.Where(y => y.id == 15).FirstOrDefault();
                // Console.WriteLine(result.rowHeader[1].Total);
                if (adminDeptVenue != null)
                {
                    float totalAdminIncome = adminCommisionList.Sum(x => x.total);
                    result.rowHeader.Where(x => x.id == -1).FirstOrDefault().Total +=
                        totalAdminIncome;
                    adminDeptVenue.expenseHeads.Add(
                        new BudgetArazExpenseHead
                        {
                            id = -1,
                            isConcerning = false,
                            isCapital = false,
                            total = totalAdminIncome,
                            items = adminCommisionList,
                            name = "Admin Income",
                            verified = true,
                            isExpense = false,
                        }
                    );
                    adminDept
                        .deptVenues.Where(y => y.id == -11)
                        .FirstOrDefault()
                        ?.expenseHeads.Add(
                            new BudgetArazExpenseHead
                            {
                                id = -1,
                                isConcerning = false,
                                isCapital = false,
                                total = adminCommisionList.Sum(x => x.total),
                                name = "Admin Income",
                                verified = true,
                            }
                        );
                    adminDept.total -= totalAdminIncome;
                }

                BudgetArazDeptVenue grandTotal = new BudgetArazDeptVenue
                {
                    id = 0,
                    name = "Grand Total",
                    pset = 0,
                    deptId = 0,
                    deptName = "Total",
                };
                grandTotal.expenseHeads = new List<BudgetArazExpenseHead>();
                foreach (BudgetSummaryRowHeader y in result.rowHeader)
                {
                    grandTotal.expenseHeads.Add(
                        new BudgetArazExpenseHead
                        {
                            id = y.id,
                            name = y.name,
                            total = y.Total,
                            isCapital = y.isCapital,
                            isExpense = y.isExpense,
                            //total_section = y.schools.
                        }
                    );

                    grandTotal.expenseHeads.Add(new BudgetArazExpenseHead
                    {
                        items = new List<BudgetArazItem>()
                    });
                    for (int i = 0; i < result.summary.Count(); i++)
                    {

                    }


                }

                
                grandTotal.total = grandTotal.expenseHeads.Sum(x => x.total);
                result.summary.Add(
                    new BudgetArazDept
                    {
                        id = 0,
                        pset = 0,
                        name = "Total",
                        total = grandTotal.total,
                        deptVenues = new List<BudgetArazDeptVenue> { grandTotal }
                    }
                );


                //foreach (var item in result.colHeader)
                //{
                //    foreach (var item1 in item.deptVenue.registrationform_dropdown_set)
                //    {
                //        Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(item1, options));
                //    }
                //}

                result.colHeader.ForEach(x =>
                {
                    x.deptVenue = null;
                });
                foreach (BudgetSummaryColHeaderGroup x in result.colHeaderGroup)
                {
                    List<BudgetSummaryColHeader> t = result
                        .colHeader.Where(y => y.groupId == x.id)
                        .ToList();
                    if (t.Count == 0)
                    {
                        continue;
                    }
                    x.count = result.colHeader.Where(y => y.groupId == x.id).Count() + 1;
                    int index = result.colHeader.IndexOf(t.Last());
                    result.colHeader.Insert(
                        index + 1,
                        new BudgetSummaryColHeader
                        {
                            id = x.id * -1,
                            groupId = x.id,
                            name = "Total",
                            className = "Total",
                            psetId = x.id * -1,
                            show = true,
                            deptVenueName = "Total: " + x.school,
                            color = t[0].color,
                            school = x.school
                        }
                    );
                }
                ;

                result.colHeader.Add(
                    new BudgetSummaryColHeader
                    {
                        id = 0,
                        name = "Total",
                        show = true,
                        groupId = 0,
                        count = 1,
                        color = "#f3f4f6",
                        psetId = 0
                    }
                );
                result.colHeaderGroup.Add(
                    new BudgetSummaryColHeaderGroup
                    {
                        id = 0,
                        name = "Grand Total",
                        deptVenueName = "Grand Total",
                        show = true,
                        //groupId = 0,
                        color = "#f3f4f6",
                    }
                );
                result.rowHeader.ForEach(x => x.expensehead = null);

                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public List<List<string>> getBudgetArazSummaryExport(AuthUser authuser, bool audited)
        {
            List<dept_venue> deptvenues = _context
                .dept_venue.Where(x => x.status == "active")
                .ToList();
            List<mz_expense_procurement_baseitem> baseitems = _context
                .mz_expense_procurement_baseitem.Where(x => x.status == true)
                .ToList();
            try
            {
                BudgetArazSummaryModels budget = getBudgetArazSummary(authuser, audited);
                List<List<string>> table = new List<List<string>>();
                List<string> headers = new List<string> { "Expense Head" };
                table.Add(headers);

                foreach (BudgetSummaryColHeader colH in budget.colHeader)
                {
                    BudgetArazDeptVenue deptVen = budget
                        .summary.Where(x => x.id == colH.groupId)
                        .FirstOrDefault()
                        .deptVenues.Where(x => x.id == colH.id)
                        .FirstOrDefault();
                    List<string> col = new List<string>();
                    if (colH.name == "Total")
                    {
                        col.Add(deptVen.deptName + "-" + " Total");
                    }
                    else
                    {
                        col.Add(deptVen.name);
                    }
                    foreach (BudgetSummaryRowHeader rowH in budget.rowHeader)
                    {
                        BudgetArazExpenseHead expenseHead = deptVen
                            .expenseHeads.Where(x => x.id == rowH.id)
                            .FirstOrDefault();
                        if (budget.colHeader.IndexOf(colH) == 1)
                        {
                            table[0].Add(rowH.name);
                        }
                        if (expenseHead == null)
                        {
                            col.Add("0");
                        }
                        else
                        {
                            col.Add(expenseHead.total.ToString());
                        }
                    }
                    table.Add(col);
                }
                return table;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public string verifyBudgetArazItem(AuthUser authUser, List<BudgetArazItem> arazIds)
        {
            Console.WriteLine(arazIds);
            string res = "";

            foreach (BudgetArazItem arazItem in arazIds)
            {
                if (arazItem.isExpense == false)
                {
                    mz_expense_estimate_student araz = _context
                        .mz_expense_estimate_student.Where(x => x.id == arazItem.id)
                        .FirstOrDefault();
                    mz_expense_student_budget_issue_logs latestArazIssue = new mz_expense_student_budget_issue_logs();

                    if (araz != null)
                    {
                        latestArazIssue = araz
                       .mz_expense_student_budget_issue_logs.OrderByDescending(x => x.createdOn)
                       .FirstOrDefault();

                        if (latestArazIssue != null && (latestArazIssue.isConcerning ?? false))
                        {
                            res +=
                                "araz Id - "
                                + arazItem.id
                                + " was not verified as there is an issue raised, ";
                            continue;
                        }

                        if (araz.stage.Contains("Initia"))
                        {
                            araz.remarks_admin = arazItem.remark;
                            araz.stage = "In Audit";
                            res += "araz Id - " + arazItem.id + " was verified, ";
                        }
                        else if (araz.stage.Contains("Audit"))
                        {
                            araz.stage = "Audited";
                            res += "Audit completed for araz Id - " + arazItem.id + " , ";
                        }
                    }                    
                }
                else
                {
                    mz_expense_budget_araz araz = _context
                        .mz_expense_budget_araz.Where(x => x.id == arazItem.id)
                        .FirstOrDefault();
                    mz_expense_budget_issue_logs latestArazIssue = araz
                        .mz_expense_budget_issue_logs.OrderByDescending(x => x.createdOn)
                        .FirstOrDefault();

                    if (latestArazIssue != null && (latestArazIssue.isConcerning ?? false))
                    {
                        res +=
                            "araz Id - "
                            + arazItem.id
                            + " was not verified as there is an issue raised, ";
                        continue;
                    }

                    araz.updatedBy = authUser.Name;
                    araz.updatedOn = DateTime.Now;

                    if (araz.stage.Contains("Initia"))
                    {
                        araz.remarks_admin = arazItem.remark;
                        araz.stage = "In Audit";
                        res += "araz Id - " + arazItem.id + " was varified, ";
                    }
                    else if (araz.stage.Contains("Audit"))
                    {
                        araz.stage = "Audited";
                        res += "Audit completed for araz Id - " + arazItem.id + " , ";
                    }
                }
                Console.WriteLine(res);

                _context.SaveChanges();
            }

            return res;
        }

        public string verifyBudgetSmartGoal(AuthUser authUser, List<BudgetSmartGoal> arazIds)
        {
            string res = "";

            foreach (BudgetSmartGoal arazItem in arazIds)
            {
                mz_expense_budget_smart_goals araz = _context
                    .mz_expense_budget_smart_goals.Where(x => x.id == arazItem.id)
                    .FirstOrDefault();
                mz_expense_budget_smart_issue_logs latestArazIssue = araz
                    .mz_expense_budget_smart_issue_logs.OrderByDescending(x => x.createdOn)
                    .FirstOrDefault();

                if (latestArazIssue != null && (latestArazIssue.isConcerning ?? false))
                {
                    res +=
                        "araz Id - "
                        + arazItem.id
                        + " was not varified as there is an issue raised, ";
                    continue;
                }

                araz.updatedBy = authUser.Name;
                araz.updatedOn = DateTime.Now;

                if (araz.stage.Contains("Initia"))
                {
                    araz.remarks_admin = arazItem.remark;
                    araz.stage = "Varified 1";
                    res += "goal Id - " + arazItem.id + " was varified, ";
                }
                else if (araz.stage.Contains("Varified 1"))
                {
                    araz.stage = "Varified 2";
                    res += "Varification completed for goal Id - " + arazItem.id + " , ";
                }

                _context.SaveChanges();
            }

            return res;
        }

        public string budgetSmartGoalIssue(
            AuthUser authUser,
            BudgetSmartGoal budgetVarification,
            bool isAdmin
        )
        {
            string res = "";

            mz_expense_budget_smart_goals araz = _context
                .mz_expense_budget_smart_goals.Where(x => x.id == budgetVarification.id)
                .FirstOrDefault();
            if (araz == null)
            {
                throw new Exception("Smart Goal not found");
            }
            mz_expense_budget_smart_issue_logs newIssue = new mz_expense_budget_smart_issue_logs
            {
                isConcerning = budgetVarification.isConcerning,
                smartGoalId = araz.id,
                createdBy = authUser.Name,
                remark = budgetVarification.remark,
                createdOn = DateTime.Now,
            };

            araz.category = budgetVarification.category;
            araz.specific = budgetVarification.specific;
            araz.measearable = budgetVarification.measurable;
            araz.attainable = budgetVarification.attainable;
            araz.relevant = budgetVarification.relevant;
            if (budgetVarification.time != null)
            {
                araz.timeStart = budgetVarification.time?.FromDate;
                araz.timeEnd = budgetVarification.time?.ToDate;
            }
            araz.updatedOn = DateTime.Now;
            araz.updatedBy = authUser.Name;

            if (!(araz.stage.Contains("Initiated") || araz.stage.Contains("Initial resolved")))
            {
                _context.mz_expense_budget_smart_issue_logs.Add(newIssue);
            }

            if (budgetVarification.isConcerning)
            {
                if (!isAdmin)
                {
                    araz.stage = "Initial concern";
                }
                res += " issue raised Successfully";
            }
            else
            {
                if (!araz.stage.Contains("Audited") && !isAdmin)
                {
                    araz.stage = "Initial resolved";
                }
                res += " issue resolved Successfully";
            }
            ;

            _context.SaveChanges();

            return res;
        }

        public string budgetArazItemIssue(
            AuthUser authUser,
            BudgetArazItem budgetVarification,
            bool isAdmin
        )
        {
            string res = "";

            CultureInfo indianCulture = new CultureInfo("hi-IN");
            NumberFormatInfo numberFormat = indianCulture.NumberFormat;

            mz_expense_budget_araz araz = _context
                .mz_expense_budget_araz.Where(x => x.id == budgetVarification.id)
                .FirstOrDefault();

            mz_expense_budget_issue_logs newIssue = new mz_expense_budget_issue_logs
            {
                isConcerning = budgetVarification.isConcerning,
                budgetArazId = araz.id,
                createdBy = authUser.Name,
                remark = budgetVarification.remark,
                createdOn = DateTime.Now,
            };

            Dictionary<string, string> oldData = new Dictionary<string, string>();
            oldData.Add("unitAmount", (budgetVarification.perUnitAmt ?? 0).ToString("N0", numberFormat));
            oldData.Add("justification", budgetVarification.description);
            oldData.Add("quantity", budgetVarification.quantity.ToString());

            newIssue.arazState = JsonConvert.SerializeObject(oldData);
            araz.quantity = budgetVarification.quantity ?? 0;
            araz.justification = budgetVarification.description;
            araz.amountPerUom = budgetVarification.perUnitAmt ?? 0;

            if (budgetVarification.isConcerning)
            {
                if (!isAdmin)
                {
                    araz.stage = "Initial concern";
                }
                res += " issue raised Successfully";
            }
            else
            {
                if (!araz.stage.Contains("Audited") && !isAdmin)
                {
                    araz.stage = "Initial resolved";
                }
                res += " issue resolved Successfully";
            }

            _context.mz_expense_budget_issue_logs.Add(newIssue);

            var budgetAraz = _context.mz_expense_budget_araz.Where(x => x.id == budgetVarification.id).FirstOrDefault();
            var budgetArazMonth = _context.mz_expense_budget_araz_monthly.Where(x => x.budget_araz_id == budgetVarification.id).ToList();

            if (budgetAraz != null)
            {
                budgetAraz.quantity = budgetVarification.quantity ?? 0;
                budgetAraz.amountPerUom = budgetVarification.total;

                if(budgetArazMonth != null)
                {
                    int i = 0;
                    foreach (var item in budgetArazMonth)
                    {
                        item.amount = budgetVarification.months[i].amount;
                        item.quantity = budgetVarification.months[i].quantity;
                        item.month_num = budgetVarification.months[i].month_num;
                        i++;
                    }
                }

            }
            _context.SaveChanges();

            return res;
        }

        public string budgetArazIncomeItemIssue(
            AuthUser authUser,
            BudgetArazItem budgetVarification,
            bool isAdmin
        )
        {
            string res = "";

            CultureInfo indianCulture = new CultureInfo("hi-IN");
            NumberFormatInfo numberFormat = indianCulture.NumberFormat;
            mz_expense_estimate_student araz = _context
                .mz_expense_estimate_student.Where(x => x.id == budgetVarification.id)
                .FirstOrDefault();

            mz_expense_student_budget_issue_logs newIssue = new mz_expense_student_budget_issue_logs
            {
                isConcerning = budgetVarification.isConcerning,
                estimateStudentId = araz.id,
                createdBy = authUser.Name,
                remark = budgetVarification.remark,
                createdOn = DateTime.Now,
            };

            Dictionary<string, string> oldData = new Dictionary<string, string>();
            oldData.Add("unitAmount", (budgetVarification.perUnitAmt ?? 0).ToString("N0", numberFormat));
            oldData.Add("justification", budgetVarification.description);
            oldData.Add("quantity", budgetVarification.quantity.ToString());

            //araz.studentCountPerMonth = budgetVarification.quantity;
            //araz.remarks_admin = budgetVarification.description;
            //araz.duration = budgetVarification.perUnitAmt;

            newIssue.arazState = JsonConvert.SerializeObject(oldData);
            if (budgetVarification.isConcerning)
            {
                if (!isAdmin)
                {
                    araz.stage = "Initial concern";
                }
                res += " issue raised Successfully";
            }
            else
            {
                if (!araz.stage.Contains("Audited") && !isAdmin)
                {
                    araz.stage = "Initial resolved";
                }
                res += " issue resolved Successfully";
            }
            ;

            _context.mz_expense_student_budget_issue_logs.Add(newIssue);
            _context.SaveChanges();

            return res;
        }

        public string updateExpenseBudgetAraz(AuthUser authUser, BudgetArazItem budgetVarification)
        {

            // System.Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(budgetVarification));
            string res = "";

            mz_expense_budget_araz araz = _context
                .mz_expense_budget_araz.Where(x => x.id == budgetVarification.id)
                .FirstOrDefault();

            araz.quantity = budgetVarification.quantity ?? 0;
            araz.justification = budgetVarification.description;
            araz.amountPerUom = budgetVarification.perUnitAmt ?? 0;
            araz.updatedBy = authUser.Name;
            araz.updatedOn = DateTime.Now;
            _context.SaveChanges();

            var monthly_araz = _context.mz_expense_budget_araz_monthly.Where(x => x.budget_araz_id == araz.id).ToList();

            if (monthly_araz.Count() == budgetVarification.months.Count())
            {
                for (int i = 0; i < monthly_araz.Count; i++)
                {
                    //System.Console.WriteLine($"Amount in DB: {monthly_araz[i].amount}, Amount to Update: {budgetVarification.months[i].amount}");
                    monthly_araz[i].quantity = budgetVarification.months[i].quantity;
                    monthly_araz[i].amount = budgetVarification.months[i].amount;
                }
            }
            else
            {
                throw new InvalidOperationException("The collections have different lengths.");

            }

            _context.SaveChanges();

            res += "Araz updated successfully";

            return res;
        }
    }
}
