using Greenbacks.Common.Interfaces;
using Greenbacks.Common.Models;
using Greenbacks.DatabaseAccess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Greenbacks
{
    public static class DefaultTables
    {
        public static void SetDefaultAccountTypes()
        {
            try
            {
                List<AccountTypeModel> temp = new List<AccountTypeModel>();

                temp.Add(new AccountTypeModel { Name = "Bank Accounts", Description = "Checking, Savings, etc.", Asset = true });
                temp.Add(new AccountTypeModel { Name = "Cash", Description = "Cash on hand", Asset = true });
                temp.Add(new AccountTypeModel { Name = "Credit Accounts", Description = "Credit Cards, Bank Cards, etc.", Asset = false });
                temp.Add(new AccountTypeModel { Name = "Investment Accounts", Description = "Stocks, Bonds, etc.", Asset = true });
                temp.Add(new AccountTypeModel { Name = "Loan Accounts", Description = "Auto, Home, etc.", Asset = false });
                temp.Add(new AccountTypeModel { Name = "Retirement Accounts", Description = "401k, IRA, etc.", Asset = true });

                IDAL_AccountTypes types = new DAL_AccountTypes();
                types.CreateAccountTypes(temp);
            }
            catch (Exception ex)
            {
                Utilities.ShowErrorMessageBox(ex.Message);
            }
        }

        public static void SetDefaultPayees()
        {
            IDAL_Payees payees = new DAL_Payees();
            payees.CreatePayee(new PayeeModel { Name = "New Account", Notes = "New account balance" });
        }

        /// <summary>
        /// Creates default list of category types if not CategoryType table is present.
        /// </summary>
        /// <returns>List of category types.</returns>
        public static void SetDefaultCategoryTypes()
        {
            List<CategoryTypeModel> temp = new List<CategoryTypeModel>();

            temp.Add(new CategoryTypeModel { Name = "Unassigned" });
            temp.Add(new CategoryTypeModel { Name = "Transfer" });
            temp.Add(new CategoryTypeModel { Name = "Income" });
            temp.Add(new CategoryTypeModel { Name = "Expense" });

            IDAL_CategoryTypes cats = new DAL_CategoryTypes();
            cats.CreateCategoryTypes(temp);
        }

        // Eventually script this is sql.
        /// <summary>
        /// Creates default categories if no Category table is present.
        /// </summary>
        /// <returns>List of default categories.</returns>
        public static void SetDefaultCategories()
        {
            List<CategoryModel> temp = new List<CategoryModel>();

            temp.Add(new CategoryModel { Name = "Category", SubCategoryName = "", CategoryTypeId = 1 });
            temp.Add(new CategoryModel { Name = "Starting Balance", SubCategoryName = "", CategoryTypeId = 1 });
            temp.Add(new CategoryModel { Name = "Funds", SubCategoryName = "", CategoryTypeId = 2 });
            temp.Add(new CategoryModel { Name = "Funds", SubCategoryName = "Checking", CategoryTypeId = 2 });
            temp.Add(new CategoryModel { Name = "Funds", SubCategoryName = "Savings", CategoryTypeId = 2 });
            temp.Add(new CategoryModel { Name = "Funds", SubCategoryName = "Credit Card", CategoryTypeId = 2 });
            temp.Add(new CategoryModel { Name = "Funds", SubCategoryName = "Loan", CategoryTypeId = 2 });
            temp.Add(new CategoryModel { Name = "Funds", SubCategoryName = "Investment", CategoryTypeId = 2 });
            temp.Add(new CategoryModel { Name = "Funds", SubCategoryName = "Retirement", CategoryTypeId = 2 });
            temp.Add(new CategoryModel { Name = "Bonus", SubCategoryName = "", CategoryTypeId = 3 });
            temp.Add(new CategoryModel { Name = "Business", SubCategoryName = "Sales", CategoryTypeId = 3 });
            temp.Add(new CategoryModel { Name = "Capital Gains", SubCategoryName = "", CategoryTypeId = 3 });
            temp.Add(new CategoryModel { Name = "Child Support", SubCategoryName = "", CategoryTypeId = 3 });
            temp.Add(new CategoryModel { Name = "Commission", SubCategoryName = "", CategoryTypeId = 3 });
            temp.Add(new CategoryModel { Name = "Divedends", SubCategoryName = "", CategoryTypeId = 3 });
            temp.Add(new CategoryModel { Name = "Federal Tax Refund", SubCategoryName = "", CategoryTypeId = 3 });
            temp.Add(new CategoryModel { Name = "Gifts Received", SubCategoryName = "", CategoryTypeId = 3 });
            temp.Add(new CategoryModel { Name = "Gross Pay", SubCategoryName = "", CategoryTypeId = 3 });
            temp.Add(new CategoryModel { Name = "Interest", SubCategoryName = "", CategoryTypeId = 3 });
            temp.Add(new CategoryModel { Name = "Load Principal", SubCategoryName = "", CategoryTypeId = 3 });
            temp.Add(new CategoryModel { Name = "Lottery", SubCategoryName = "", CategoryTypeId = 3 });
            temp.Add(new CategoryModel { Name = "Net Pay", SubCategoryName = "", CategoryTypeId = 3 });
            temp.Add(new CategoryModel { Name = "Other", SubCategoryName = "", CategoryTypeId = 3 });
            temp.Add(new CategoryModel { Name = "Overtime", SubCategoryName = "", CategoryTypeId = 3 });
            temp.Add(new CategoryModel { Name = "Retirement", SubCategoryName = "", CategoryTypeId = 3 });
            temp.Add(new CategoryModel { Name = "Retirement", SubCategoryName = "IRA Distributions", CategoryTypeId = 3 });
            temp.Add(new CategoryModel { Name = "Retirement", SubCategoryName = "Pensions and Annuities", CategoryTypeId = 3 });
            temp.Add(new CategoryModel { Name = "Retirement", SubCategoryName = "SSI Benefits", CategoryTypeId = 3 });
            temp.Add(new CategoryModel { Name = "Retirement", SubCategoryName = "VA Benefits", CategoryTypeId = 3 });
            temp.Add(new CategoryModel { Name = "Salary", SubCategoryName = "", CategoryTypeId = 3 });
            temp.Add(new CategoryModel { Name = "State Tax Refund", SubCategoryName = "", CategoryTypeId = 3 });
            temp.Add(new CategoryModel { Name = "Stock Option", SubCategoryName = "", CategoryTypeId = 3 });
            temp.Add(new CategoryModel { Name = "Tax-Exempt Interest", SubCategoryName = "", CategoryTypeId = 3 });
            temp.Add(new CategoryModel { Name = "Unemployment", SubCategoryName = "", CategoryTypeId = 3 });
            temp.Add(new CategoryModel { Name = "Auto", SubCategoryName = "", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Auto", SubCategoryName = "Miscellaneous", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Auto", SubCategoryName = "Car Payment", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Auto", SubCategoryName = "Gas", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Auto", SubCategoryName = "Insurance", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Auto", SubCategoryName = "Maintenance", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Auto", SubCategoryName = "Parking", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Auto", SubCategoryName = "Registration", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Auto", SubCategoryName = "Toll Roads", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Bank", SubCategoryName = "", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Bank", SubCategoryName = "Charge", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Bank", SubCategoryName = "Credit Card Payment", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Bank", SubCategoryName = "ATM Charge", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Bank", SubCategoryName = "ATM Withdrawl", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Bank", SubCategoryName = "Service Charge", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Business", SubCategoryName = "", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Business", SubCategoryName = "Office Supplies", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Child Support", SubCategoryName = "", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Clothing", SubCategoryName = "", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Computer", SubCategoryName = "", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Computer", SubCategoryName = "Hardware", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Computer", SubCategoryName = "Miscellaneous", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Computer", SubCategoryName = "Software", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Donation", SubCategoryName = "", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Education", SubCategoryName = "", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Education", SubCategoryName = "Books", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Education", SubCategoryName = "Fees", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Education", SubCategoryName = "Student Loan Payment", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Education", SubCategoryName = "Tuition", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Electronics", SubCategoryName = "", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Food", SubCategoryName = "", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Food", SubCategoryName = "Dining Out", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Food", SubCategoryName = "Groceries", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Gifts", SubCategoryName = "", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Healthcare", SubCategoryName = "", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Healthcare", SubCategoryName = "Dental", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Healthcare", SubCategoryName = "Eyecare", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Healthcare", SubCategoryName = "Hospital", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Healthcare", SubCategoryName = "Misc", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Healthcare", SubCategoryName = "Physician", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Healthcare", SubCategoryName = "Prescriptions", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Household", SubCategoryName = "", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Household", SubCategoryName = "Furniture", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Household", SubCategoryName = "House Cleaning", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Household", SubCategoryName = "Laundry", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Household", SubCategoryName = "Miscellaneous", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Household", SubCategoryName = "Yard Service", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Housing", SubCategoryName = "", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Housing", SubCategoryName = "Mortgage Payment", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Housing", SubCategoryName = "Rent", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Insurance", SubCategoryName = "", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Insurance", SubCategoryName = "Dental", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Insurance", SubCategoryName = "Health", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Insurance", SubCategoryName = "Homeowners", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Insurance", SubCategoryName = "Life", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Insurance", SubCategoryName = "Long Term Disability", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Insurance", SubCategoryName = "Miscellaneous", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Insurance", SubCategoryName = "Personal Accident", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Insurance", SubCategoryName = "Short Term Disabilities", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Leisure", SubCategoryName = "", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Leisure", SubCategoryName = "Books and Magazines", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Leisure", SubCategoryName = "Cultural Events", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Leisure", SubCategoryName = "Entertainment", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Leisure", SubCategoryName = "Movies", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Leisure", SubCategoryName = "Photography", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Leisure", SubCategoryName = "Sporting Events", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Leisure", SubCategoryName = "Sporting Goods", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Leisure", SubCategoryName = "Music", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Leisure", SubCategoryName = "Toys and Games", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Miscellaneous", SubCategoryName = "", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Personal Care", SubCategoryName = "", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Personal Care", SubCategoryName = "Hair Cut", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Personal Care", SubCategoryName = "Tan", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Personal Care", SubCategoryName = "Gym", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Pet", SubCategoryName = "", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Pet", SubCategoryName = "Food", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Pet", SubCategoryName = "Health Care", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Pet", SubCategoryName = "Miscellaneous", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Taxes", SubCategoryName = "", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Taxes", SubCategoryName = "Federal Income Tax", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Taxes", SubCategoryName = "Local Income Tax", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Taxes", SubCategoryName = "Medicare Tax", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Taxes", SubCategoryName = "Other Taxes", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Taxes", SubCategoryName = "Real Estate Taxes", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Taxes", SubCategoryName = "SSI Tax", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Taxes", SubCategoryName = "State Income Tax", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Utilities", SubCategoryName = "", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Utilities", SubCategoryName = "Cable/Satellite TV", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Utilities", SubCategoryName = "Cell Phone", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Utilities", SubCategoryName = "Electric", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Utilities", SubCategoryName = "Garbage", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Utilities", SubCategoryName = "Homeowners Dues", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Utilities", SubCategoryName = "Natural Gas", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Utilities", SubCategoryName = "Natural Oil", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Utilities", SubCategoryName = "Internet", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Utilities", SubCategoryName = "Telephone", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Utilities", SubCategoryName = "Water and Sewer", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Vacation", SubCategoryName = "", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Vacation", SubCategoryName = "Lodging", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Vacation", SubCategoryName = "Travel", CategoryTypeId = 4 });
            temp.Add(new CategoryModel { Name = "Vacation", SubCategoryName = "Food", CategoryTypeId = 4 });

            IDAL_Categories cats = new DAL_Categories();
            cats.CreateCategories(temp);

            List<CategoryModel> parents = temp.Where(y => y.SubCategoryName == "").ToList();
            List<CategoryModel> sub = temp.Where(y => !string.IsNullOrEmpty(y.SubCategoryName)).ToList();

            foreach (CategoryModel cat in sub)
            {
                cat.ParentCategoryId = temp.FirstOrDefault(y => y.Name == cat.Name && y.SubCategoryName == "") != null ? temp.FirstOrDefault(y => y.Name == cat.Name && y.SubCategoryName == "").Id : 0;
            }

            cats.UpdateCategories(temp);
        }
    }
}
