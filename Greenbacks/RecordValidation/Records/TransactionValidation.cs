using Greenbacks.Common.Models;

namespace Greenbacks.RecordValidation
{
    public class TransactionValidation : RecordValidationBase, IValidateRecord
    {
        private TransactionModel transaction;

        public TransactionValidation(TransactionModel transaction)
        {
            this.transaction = transaction;
        }

        public void Validate()
        {
            if (transaction.TransactionDate == null)
            {
                AddError("A transaction date is required.");
            }

            if (transaction.Debit > 0 & transaction.Credit > 0)
            {
                AddError("Both deposit and withdrawal can not have a value greater than $0.00.");
            }

            if (transaction.Debit < 0)
            {
                AddError("A deposit can not be less than $0.00.");
            }

            if (transaction.Credit < 0)
            {
                AddError("A withdrawal can not be less than $0.00.");
            }

            if (transaction.PayeeId == 0) 
            {
                AddError("A Payee is required."); 
            }
            if (transaction.CategoryId == 0)
            {  
                AddError("A Category is required.");
            }
        }
    }
}
