using System;
using grundlage.jsonnet;
using Newtonsoft.Json;
using NUnit.Framework;
using Shouldly;

namespace grundlage.tests
{
    public class JsonConversionTests
    {
        [OneTimeSetUp]
        public void SetUpJson()
        {

            JsonSerializerSettings settings = new JsonSerializerSettings();

            // I usually type scan these in
            settings.Converters.Add(new TestDefault<GrundlagePrimitive, string>());

            JsonConvert.DefaultSettings = () => settings;
        }

        [Test]
        public void GrundlageDefault()
        {
            var payload = JsonConvert.SerializeObject(new GrundlagePrimitive("abc"));
            payload.ShouldBe("\"abc\"");
        }

        [Test]
        public void JsonDefault()
        {
            var payload = JsonConvert.SerializeObject(new TraditionalPrimitive("abc"));
            payload.ShouldBe("{}");
        }

        [Test]
        public void JsonDefault2()
        {
            var payload = JsonConvert.SerializeObject(new TraditionalPrimitive2("abc"));
            payload.ShouldBe("{\"Value\":\"abc\"}");
        }


        public class TestDefault<TValueType, TPrimitive> : BasePrimitiveJsonConverter<TValueType, TPrimitive>
            where TValueType : class, ValueType<TPrimitive>
            where TPrimitive : IComparable<TPrimitive>, IEquatable<TPrimitive>, IComparable
        {

        }
        public class GrundlagePrimitive : Primitive<string>
        {
            public GrundlagePrimitive(string primitiveValue) : base(primitiveValue)
            {
            }
        }

        public class TraditionalPrimitive
        {
            string _value;

            public TraditionalPrimitive(string value)
            {
                _value = value;
            }
        }

        public class TraditionalPrimitive2
        {
            public string Value { get; set; }

            public TraditionalPrimitive2(string value)
            {
                Value = value;
            }
        }
    }
}