using System;
using System.ComponentModel;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Easy.Commerce.Services.Web.Api.Binders
{
    /// <summary>
    /// Model binder.
    /// </summary>
    public class DelimitedArrayModelBinder : IModelBinder
    {
        /// <summary>
        /// Bind model.
        /// </summary>
        /// <param name="bindingContext">See.</param>
        /// <returns>Returns.</returns>
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var modelName = bindingContext.ModelName;
            var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);
            var values = valueProviderResult
                .ToString()
                .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var elementType = bindingContext.ModelType.GetTypeInfo().GenericTypeArguments[0];

            if (values.Length == 0)
            {
                bindingContext.Result = ModelBindingResult.Success(Array.CreateInstance(elementType, 0));
            }
            else
            {
                var converter = TypeDescriptor.GetConverter(elementType);
                var typedArray = Array.CreateInstance(elementType, values.Length);

                try
                {
                    for (int i = 0; i < values.Length; ++i)
                    {
                        var value = values[i];
                        var convertedValue = converter.ConvertFromString(value);
                        typedArray.SetValue(convertedValue, i);
                    }
                }
                catch (Exception exception)
                {
                    bindingContext.ModelState.TryAddModelError(
                        modelName,
                        exception,
                        bindingContext.ModelMetadata);
                }

                bindingContext.Result = ModelBindingResult.Success(typedArray);
            }

            return Task.CompletedTask;
        }
    }
}