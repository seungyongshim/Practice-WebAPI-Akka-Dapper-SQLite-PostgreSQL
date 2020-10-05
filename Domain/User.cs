using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Domain
{
    public class User
    {
        public byte[] BLOB { get; set; }
        public string PASSWORD { get; set; }
        public string USER_GROUP { get; set; }
        public long USER_ID { get; set; }
        public string USER_NAME { get; set; }

        public override string ToString()
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };

            options.Converters.Add(new ByteArrayConverter());

            return JsonSerializer.Serialize(this, options);
        }
    }

    public class ByteArrayConverter : JsonConverter<byte[]>
    {
        public override byte[] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            short[] sByteArray = JsonSerializer.Deserialize<short[]>(ref reader);
            byte[] value = new byte[sByteArray.Length];
            for (int i = 0; i < sByteArray.Length; i++)
            {
                value[i] = (byte)sByteArray[i];
            }

            return value;
        }

        public override void Write(Utf8JsonWriter writer, byte[] values, JsonSerializerOptions options)
        {
            writer.WriteStartArray();

            foreach (var val in values)
            {
                writer.WriteNumberValue(val);
            }

            writer.WriteEndArray();
        }
    }
}
