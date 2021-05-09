using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System;

namespace OtcWebAPi.Providers
{
    public class ReflectionLogicsProvider
    {

        private readonly ILogger<ReflectionLogicsProvider> _logger;

        public ReflectionLogicsProvider(ILogger<ReflectionLogicsProvider>logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Generate where message from not empty fields
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string GenerateWhereMessageFromFields(object model)
        {
            var propertiesDict = model.GetType().GetProperties().ToDictionary(x => x.Name, y => y.GetValue(model));
            var messages = new List<string>();
            foreach (var property in propertiesDict)
            {
                if (!IsEmptyProperty(property.Value))
                {
                    if (property.Value.GetType() == typeof(string))
                    {
                        messages.Add($"{property.Key} = '{property.Value}'");
                        continue;
                    }
                    messages.Add($"{property.Key} = {property.Value}");
                }
            }
            return String.Join(" AND ", messages);
        }

        /// <summary>
        /// Check property for empty
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        private bool IsEmptyProperty(object property)
        {
            if (property == null)
            {
                return true;
            }
            if (property.GetType() == typeof(int) && ((int)property == 0))
            {
                return true;
            }
            if (property.GetType() == typeof(decimal) && ((decimal)property == 0))
            {
                return true;
            }
            if (property.GetType() == typeof(DateTime) && ((DateTime)property == DateTime.MinValue))
            {
                return true;
            }
            return false;
        }

    }
}
