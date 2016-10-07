using System;
using Newtonsoft.Json;

namespace grundlage.jsonnet
{
    public abstract class BasePrimitiveJsonConverter<TValueType, TPrimitive> : JsonConverter
        where TValueType : class, ValueType<TPrimitive>
        where TPrimitive : IComparable<TPrimitive>, IEquatable<TPrimitive>, IComparable
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var vo = (TValueType) value;
            if (vo == null)
            {
                writer.WriteNull();
            }
            else
            {
                writer.WriteValue(GetValue(vo));
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            var s = reader.Value;
            if (s == null)
            {
                return default(TValueType);
            }

            var v = Convert.ChangeType(s, typeof(TPrimitive));

            return Activator.CreateInstance(typeof(TValueType), v);
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(TValueType) == objectType;
        }

        protected virtual TPrimitive GetValue(TValueType microType)
        {
            return microType.Unwrap();
        }
    }
}
