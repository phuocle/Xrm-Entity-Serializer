﻿using Microsoft.Xrm.Sdk;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace XrmEntitySerializer
{
#if !XRM_7
    public class KeyAttributeCollectionConverter : CollectionTypeConverter<KeyAttributeCollection>
    {
        protected override KeyAttributeCollection ReadCollection(JsonReader reader, Type objectType, KeyAttributeCollection existingKeyAttributes, JsonSerializer serializer, JArray jArray)
        {
            foreach (JToken item in jArray)
            {
                KeyValuePair<string, object> pair = item.ToObject<KeyValuePair<string, object>>(serializer);
                existingKeyAttributes.Add(pair.Key, pair.Value);
            }
            return existingKeyAttributes;
        }

        protected override void WriteCollection(JsonWriter writer, KeyAttributeCollection keyAttributes, JsonSerializer serializer)
        {
            foreach (KeyValuePair<string, object> attribute in keyAttributes)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("Key");
                writer.WriteValue(attribute.Key);
                writer.WritePropertyName("Value");
                serializer.Serialize(writer, attribute.Value);
                writer.WriteEndObject();
            }
        }
    }
#endif
}
