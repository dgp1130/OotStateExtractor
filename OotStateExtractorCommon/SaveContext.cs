namespace DevelWoutACause.OotStateExtractor.Common {
    /** Overall save context for the game containing all relevant game state. */
    public sealed record SaveContext(Upgrades Upgrades) {
        public static SaveContext empty() {
            return new SaveContext(Upgrades: new Upgrades());
        }
    }
}
