using Newtonsoft.Json;

namespace _0_Framework.Application.SmsService;

public class SendSmsModel
{
    [JsonProperty("lineNumber")]
    public string? LineNumber { get; set; }

    [JsonProperty("messageText")]
    public string MessageText { get; set; } = null!;

    [JsonProperty("mobiles")]
    public Array? Mobiles { get; set; }
}