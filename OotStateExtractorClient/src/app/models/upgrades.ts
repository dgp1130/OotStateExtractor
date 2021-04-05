/*
 * Player upgrades. "Upgrades" in this context refers to the game's definition
 * of the term, mostly referring to carrying capacities for arrows, bombs,
 * sticks, etc. Notably does **not** include equipment like swords, shields,
 * tunics, or boots.
 */
export class Upgrades {
    public readonly hasSticks: boolean;
    public readonly hasNuts: boolean;

    private constructor({ hasSticks, hasNuts }: {
        hasSticks: boolean,
        hasNuts: boolean,
    }) {
        this.hasSticks = hasSticks;
        this.hasNuts = hasNuts;
    }

    /** Returns an {@link Upgrades} object with the given state. */
    public static of({ hasSticks, hasNuts }: {
        hasSticks: boolean,
        hasNuts: boolean,
    }): Upgrades {
        return new Upgrades({ hasSticks, hasNuts });
    }

    /** Returns an {@link Upgrades} object which contains no items. */
    public static empty(): Upgrades {
        return Upgrades.of({ hasSticks: false, hasNuts: false });
    }

    /**
     * Parses and returns an {@link Upgrades} object from the given serialized
     * format.
     */
    public static deserialize(data: SerializedUpgrades): Upgrades {
        return Upgrades.of({
            hasSticks: data.has_sticks,
            hasNuts: data.has_nuts,
        });
    }
}

/** The serialized format of a {@link Upgrades} object. */
export interface SerializedUpgrades {
    has_sticks: boolean;
    has_nuts: boolean;
}
