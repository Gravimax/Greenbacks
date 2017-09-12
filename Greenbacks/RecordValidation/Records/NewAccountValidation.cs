using Greenbacks.Models;

namespace Greenbacks.RecordValidation
{
    public class NewAccountValidation : RecordValidationBase, IValidateRecord
    {
        private NewAccountModel account;

        public NewAccountValidation(NewAccountModel account)
        {
            this.account = account;
        }

        public void Validate()
        {
            if (string.IsNullOrEmpty(account.AccountName))
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
