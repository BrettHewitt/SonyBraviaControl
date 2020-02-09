using Newtonsoft.Json;

namespace SonyBraviaControl
{
    public class VolumeInformation
    {
        [JsonProperty("result")]
        public VolumeResult[][] Result { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }
    }

    public class VolumeResult
    {
        [JsonProperty("volume")]
        public long Volume { get; set; }

        [JsonProperty("minVolume")]
        public long MinVolume { get; set; }

        [JsonProperty("mute")]
        public bool Mute { get; set; }

        [JsonProperty("maxVolume")]
        public long MaxVolume { get; set; }

        [JsonProperty("target")]
        public string Target { get; set; }
    }

    public class PowerInformation
    {
        [JsonProperty("result")]
        public PowerResult[] Result { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }
    }

    public class PowerResult
    {
        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public class SystemInformation
    {
        [JsonProperty("result")]
        public SystemInformationResult[] Result { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }
    }

    public class SystemInformationResult
    {
        [JsonProperty("generation")]
        public string Generation { get; set; }

        [JsonProperty("product")]
        public string Product { get; set; }

        [JsonProperty("serial")]
        public string Serial { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("macAddr")]
        public string MacAddr { get; set; }
    }

    public class PlayingContentInformation
    {
        [JsonProperty("result")]
        public PlayingContentInformationResult[] Result { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }
    }

    public class PlayingContentInformationResult
    {
        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }
    }

    public class ContentListInformation
    {
        [JsonProperty("result")]
        public ContentListResult[][] Result { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }
    }

    public class ContentListResult
    {
        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("index")]
        public long Index { get; set; }
    }
}
