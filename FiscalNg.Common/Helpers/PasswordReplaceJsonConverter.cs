using System;
using System.Linq;
using FiscalNg.Common.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FiscalNg.Common.Helpers
{
    /// <summary>
    /// Replace passwords in JsonConverter. Used so actions logging would not hold sensitive data.
    /// </summary>
    public class PasswordReplaceJsonConverter : JsonConverter {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            //Simply replace with not usable value.
            var token = JToken.FromObject(value);

            if (token.Type != JTokenType.Object) {
                token.WriteTo(writer);
            } else {
                var obj = (JObject)token;
                var propsWithPasswordAttribute = value.GetType().GetProperties()
                    .Where(prop => Attribute.IsDefined(prop, typeof(PasswordAttribute))).Select(x => x.Name);

                foreach (var jsonProp in propsWithPasswordAttribute)
                {
                    obj.Property(jsonProp).Value = "************";
                }

                obj.WriteTo(writer);
            }
        }

        /// <summary>
        /// Read value from json
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            // The read value must be usable
            return reader.Value;
        }

        /// <summary>
        /// Is Value convertible.
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

    }
}
