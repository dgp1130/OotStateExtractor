using Newtonsoft.Json;

namespace DevelWoutACause.OotStateExtractor.Common {
    /*
     * Player upgrades. "Upgrades" in this context refers to the game's
     * definition of the term, mostly referring to carrying capacities for
     * arrows, bombs, sticks, etc. Notably does **not** include equipment like
     * swords, shields, tunics, or boots.
     */
    public sealed record Upgrades {
        [JsonProperty("has_sticks")]
        public bool HasSticks { get; init; }

        [JsonProperty("has_nuts")]
        public bool HasNuts { get; init; }
    }
}
