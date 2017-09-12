using System.Collections.Generic;
using System.Text;

namespace Greenbacks.RecordValidation
{
    public class RecordValidationBase
    {
        private List<string> errors = new List<string>();

        private string _baseMessage = "Please correct the following errors before continuing:\r\n";
        /// <summary>
        /// Gets or sets the base message for the error message.
        /// </summary>
        /// <value>
        /// The base message.
        /// </value>
        public string BaseMessage
        {
            get { return _baseMessage; }
            set { _baseMessage = value; }
        }


        /// <summary>
        /// Gets a value indicating whether this <see cref="RecordValidationResult"/> has errors.
        /// </summary>
        /// <value>
        ///   <c>true</c> if successful validation; otherwise, <c>false</c>.
        /// </value>
        public bool HasErrors { get { return errors.Count > 0; } }


        /// <summary>
        /// Gets validation error messages.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message
        {
            get
            {
                StringBuilder message = new StringBuilder();

                message.AppendLine(_baseMessage);
                foreach (var error in errors)
                {
                    message.AppendLine(error);
                }

                return message.ToString();
            }
        }


        /// <summary>
        /// Adds an error message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void AddError(string message)
        {
            errors.Add(message);
        }


        /// <summary>
        /// Clears the errors.
        /// </summary>
        public void ClearErrors()
        {
            errors.Clear();
        }
    }
}
