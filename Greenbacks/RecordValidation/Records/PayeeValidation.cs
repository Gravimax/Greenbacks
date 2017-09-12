using Greenbacks.Common.Models;

namespace Greenbacks.RecordValidation
{
    public class PayeeValidation : RecordValidationBase, IValidateRecord
    {
        private PayeeModel payee;

        public PayeeValidation(PayeeModel payee)
        {
            this.payee = payee;
            Validate();
        }

        public void Validate()
        {
            if (string.IsNullOrEmpty(payee.Name))
            {
                AddError("A payee name is required.");
            }
            else
            {
                if (payee.Name.Length > 50)
                {
                    AddError("Name length exceded. (50 max).");
                }
            }
            

            if (!string.IsNullOrEmpty(payee.Notes) && payee.Notes.Length > 100)
            {
                AddError("Notes length exceded. (100 max).");
            }
        }
    }
}
