namespace DevelWoutACause.OotStateExtractor {
    /*
     * Player upgrades. "Upgrades" in this context refers to the game's
     * definition of the term, mostly referring to carrying capacities for
     * arrows, bombs, sticks, etc. Notably does **not** include equipment like
     * swords, shields, tunics, or boots.
     */
    internal sealed record Upgrades {
        public bool HasSticks { get; init; }
        public bool HasNuts { get; init; }
    }
}
