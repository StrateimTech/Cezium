using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Cezium.Rust;

namespace Cezium.Utils;

public class JsonGunConverter : JsonConverter<(RustSettings.Guns, RustSettings.BulletCount, RustSettings.FireRate)>
{
    public override (RustSettings.Guns, RustSettings.BulletCount, RustSettings.FireRate) Read(
        ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // TODO: Fix 
        while (reader.Read())
        {
            return JsonSerializer
                .Deserialize<(RustSettings.Guns, RustSettings.BulletCount, RustSettings.FireRate)>(
                    ref reader, options)!;
        }

        return (RustSettings.Guns.ASSAULT_RIFLE, RustSettings.BulletCount.ASSAULT_RIFLE,
            RustSettings.FireRate.ASSAULT_RIFLE);
    }

    public override void Write(Utf8JsonWriter writer,
        (RustSettings.Guns, RustSettings.BulletCount, RustSettings.FireRate) value,
        JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("Gun");
        writer.WriteStringValue(Enum.GetName(typeof(RustSettings.Guns), value.Item1));
        writer.WritePropertyName("BulletCount");
        writer.WriteStringValue(Enum.GetName(typeof(RustSettings.BulletCount), value.Item2));
        writer.WritePropertyName("FireRate");
        writer.WriteStringValue(Enum.GetName(typeof(RustSettings.FireRate), value.Item3));
        writer.WriteEndObject();
    }
}