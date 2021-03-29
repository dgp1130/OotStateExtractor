namespace DevelWoutACause.OotStateExtractor {
    /** Overall save context for the game containing all relevant game state. */
    internal sealed record SaveContext(Upgrades Upgrades) {
        public static SaveContext empty() {
            return new SaveContext(Upgrades: new Upgrades());
        }
    }
}
