using Greenbacks.Common.Models;

namespace Greenbacks.RecordValidation
{
    public class AccountValidation : RecordValidationBase, IValidateRecord
    {
        private AccountModel account;

        public AccountValidation(AccountModel account)
        {
            this.account = account;
        }

        public void Validate()
        {
            if (string.IsNullOrEmpty(account.Name))
            {
                AddError("An account name is required.");
            }

            if (string.IsNullOrEmpty(account.BankName))
            {
                AddError("An bank name is required.");
            }

            if (account.AccountTypeId == 0)
            {
                AddError("An account type is required");
            }
        }
    }
}
