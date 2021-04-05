using Newtonsoft.Json;

namespace DevelWoutACause.OotStateExtractor.Common {
    /** Overall save context for the game containing all relevant game state. */
    public sealed record SaveContext(
        [property:JsonProperty("equipment")] Equipment Equipment,
        [property:JsonProperty("upgrades")] Upgrades Upgrades
    ) {
        public static SaveContext Empty() {
            return new SaveContext(
                Equipment: new Equipment(),
                Upgrades: new Upgrades()
            );
        }
    }
}
