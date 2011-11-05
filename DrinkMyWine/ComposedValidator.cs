using System.Collections.Generic;

namespace DrinkMyWine
{
    public class ComposedValidator : IValidator
    {
        private List<IValidator> validators = new List<IValidator>();

        public void AddValidator(IValidator validator)
        {
            validators.Add(validator);
        }

        public bool ValidateWithResult()
        {
            return validators.TrueForAll(v => v.ValidateWithResult());
        }
    }
}