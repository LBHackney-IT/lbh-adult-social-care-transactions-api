using System.Collections.Generic;
using System.Linq;
using LBH.AdultSocialCare.Transactions.Api.V1.Exceptions.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Extensions
{
    public static class ModelStateExtensions
    {
        public static IEnumerable<ModelStateError> AllErrors(this ModelStateDictionary modelState)
        {
            if (modelState == null || !modelState.Any(m => m.Value.Errors.Count > 0))
            {
                return null;
            }

            var result = new List<ModelStateError>();
            var erroneousFields = modelState.Where(ms => ms.Value.Errors.Any())
                .Select(x => new { x.Key, x.Value.Errors });

            foreach (var erroneousField in erroneousFields)
            {
                var fieldKey = erroneousField.Key;
                var fieldErrors = erroneousField.Errors
                    .Select(error => new ModelStateError(fieldKey, error.ErrorMessage));
                result.AddRange(fieldErrors);
            }

            return result;
        }

        public static IEnumerable<ModelStateError> AllModelStateErrors(this ModelStateDictionary modelState)
        {
            if (modelState == null || !modelState.Any(m => m.Value.Errors.Count > 0))
            {
                return null;
            }

            var result = from ms in modelState
                         where ms.Value.Errors.Any()
                         let fieldKey = ms.Key
                         let errors = ms.Value.Errors
                         from error in errors
                         select new ModelStateError(fieldKey, error.ErrorMessage);

            return result;
        }
    }
}
