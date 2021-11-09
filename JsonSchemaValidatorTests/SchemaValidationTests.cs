using System.IO;
using System.Text.Json;
using Json.Schema;
using NUnit.Framework;

namespace JsonSchemaValidatorTests
{
    public class SchemaValidationTests
    {
        private JsonSchema _sut;

        [OneTimeSetUp]
        public void SetUp()
        {
            _sut = JsonSchema.FromFile("custom_data_schema.json");
        }

        [Test]
        public void CustomDataIsValid()
        {
            var jsonElement = JsonDocument.Parse(LoadJsonFromFile("custom_data.json"));
            var validationResults = _sut.Validate(jsonElement.RootElement);

            Assert.IsTrue(validationResults.IsValid);
        }

        [Test]
        public void ArrayFieldHasInvalidData()
        {
            var jsonElement = JsonDocument.Parse(LoadJsonFromFile("custom_data_invalid_array.json"));
            var validationResults = _sut.Validate(jsonElement.RootElement);

            Assert.IsFalse(validationResults.IsValid);
        }

        [Test]
        public void ArrayFieldDataNotExpectedType()
        {
            var jsonElement = JsonDocument.Parse(LoadJsonFromFile("custom_data_invalid_array_elems.json"));
            var validationResults = _sut.Validate(jsonElement.RootElement);

            Assert.IsFalse(validationResults.IsValid);
        }

        [Test]
        public void InvalidBoolean()
        {
            var jsonElement = JsonDocument.Parse(LoadJsonFromFile("custom_data_invalid_boolean.json"));
            var validationResults = _sut.Validate(jsonElement.RootElement);

            Assert.IsFalse(validationResults.IsValid);
        }

        [Test]
        public void InvalidInteger()
        {
            var jsonElement = JsonDocument.Parse(LoadJsonFromFile("custom_data_invalid_integer.json"));
            var validationResults = _sut.Validate(jsonElement.RootElement);

            Assert.IsFalse(validationResults.IsValid);
        }

        [Test]
        public void InvalidObjectValue()
        {
            var jsonElement = JsonDocument.Parse(LoadJsonFromFile("custom_data_invalid_object.json"));
            var validationResults = _sut.Validate(jsonElement.RootElement);

            Assert.IsFalse(validationResults.IsValid);
        }

        [Test]
        public void WrongFieldInObject()
        {
            var jsonElement = JsonDocument.Parse(LoadJsonFromFile("custom_data_invalid_object_wrong_field.json"));
            var validationResults = _sut.Validate(jsonElement.RootElement);

            Assert.IsFalse(validationResults.IsValid);
        }

        [Test]
        public void WrongValueTypeInObject()
        {
            var jsonElement = JsonDocument.Parse(LoadJsonFromFile("custom_data_invalid_object_wrong_type.json"));
            var validationResults = _sut.Validate(jsonElement.RootElement);

            Assert.IsFalse(validationResults.IsValid);
        }

        [Test]
        public void InvalidString()
        {
            var jsonElement = JsonDocument.Parse(LoadJsonFromFile("custom_data_invalid_string.json"));
            var validationResults = _sut.Validate(jsonElement.RootElement);

            Assert.IsFalse(validationResults.IsValid);
        }

        [Test]
        public void UnsupportedFields()
        {
            var jsonElement = JsonDocument.Parse(LoadJsonFromFile("custom_data_unsupported_field.json"));
            var validationResults = _sut.Validate(jsonElement.RootElement);

            Assert.IsFalse(validationResults.IsValid);
        }

        [Test]
        public void YamlTest()
        {
            var jsonElement = JsonDocument.Parse(LoadJsonFromFile("card_source.yaml"));
            var validationResults = _sut.Validate(jsonElement.RootElement);

            Assert.IsFalse(validationResults.IsValid);
        }

        private string LoadJsonFromFile(string fileName)
        {
            var customDataJsonString = File.ReadAllText(fileName);
            return customDataJsonString;
        }
    }
}