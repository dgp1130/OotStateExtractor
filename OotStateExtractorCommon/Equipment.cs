using Newtonsoft.Json;

namespace DevelWoutACause.OotStateExtractor.Common {
    /**
     * Owned equipment. "Equipment" in this context refers to swords, shields,
     * tunics, and boots. Notably does **not** include items like sticks, bombs,
     * slingshot, etc.
     */
    public sealed class Equipment {
        [JsonProperty("has_kokiri_sword")]
        public bool HasKokiriSword { get; init; }

        [JsonProperty("has_deku_shield")]
        public bool HasDekuShield { get; init; }
    }
}
