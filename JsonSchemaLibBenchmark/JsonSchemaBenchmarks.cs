using System.Text.Json;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace JsonSchemaLibBenchmark
{
    [MemoryDiagnoser]
    public class JsonSchemaBenchmarks
    {
        [Benchmark]
        public void JsonSchemaDotNetBenchmark()
        {
            var jsonElement = JsonDocument.Parse(CustomData);
            var schema = Json.Schema.JsonSchema.FromText(CustomDataSchema);
            var validationResults = schema.Validate(jsonElement.RootElement);
        }

        [Benchmark]
        public void NewtonsoftJsonSchemaBenchmark()
        {
            var jsonData = JObject.Parse(CustomData);
            JSchema schema = JSchema.Parse(CustomDataSchema);
            var validationResults = jsonData.IsValid(schema);
        }

        [Benchmark]
        public async Task NJsonSchemaBenchmarkAsync()
        {
            var schema = await NJsonSchema.JsonSchema.FromJsonAsync(CustomDataSchema);
            var validationResults = schema.Validate(CustomData);
        }

        private const string CustomData =
            @"{
                ""string_data"": ""The unique identifier for a product"",
                ""integer_data"": 2,
                ""boolean_data"": true,
                ""array_data"": [ ""hello"", ""hallo"", ""hola"", ""bonjour"" ],
                ""object_data"": {
                  ""nested_string_data"": ""nested_string_data"",
                  ""nested_number_data"": 100,
                  ""nested_integer_data"": 50
                }
              }";

        private const string CustomDataSchema =
            @"{
                ""$schema"": ""https://json-schema.org/draft/2020-12/schema"",
                ""$id"": ""https://checkout.com/custom_data.schema.json"",
                ""title"": ""Custom Data"",
                ""description"": ""Custom Data for additional payment information"",
                ""type"": ""object"",
                ""required"": [],
                ""additionalProperties"": false,
                ""properties"": {
                    ""string_data"": {
                      ""description"": ""The unique identifier for a product"",
                      ""type"": ""string""
                    },
                    ""integer_data"": {
                        ""description"": ""The unique identifier for a product"",
                        ""type"": ""integer""
                    },
                    ""boolean_data"": {
                        ""description"": ""The unique identifier for a product"",
                        ""type"": ""boolean""
                    },
                    ""array_data"": {
                        ""description"": ""The unique identifier for a product"",
                        ""type"": ""array"",
                        ""items"": {
                            ""type"": ""string""
                          },
                        ""uniqueItems"": true
                    },
                    ""object_data"": {
                        ""description"": ""The unique identifier for a product"",
                        ""type"": ""object"",
                        ""additionalProperties"": false,
                        ""properties"": {
                            ""nested_string_data"": {
                              ""type"": ""string""
                            },
                            ""nested_number_data"": {
                              ""type"": ""number""
                            },
                            ""nested_integer_data"": {
                              ""type"": ""integer""
                            }
                        }
                    }
                  }
              }";
    }
}
