import { SerializedUpgrades, Upgrades } from './upgrades';

/** Overall save context for the game containing all relevant game state. */
export class SaveContext {
    public readonly upgrades: Upgrades;

    private constructor({ upgrades }: { upgrades: Upgrades }) {
        this.upgrades = upgrades;
    }

    /** Returns a {@link SaveContext} object with the given state. */
    public static of({ upgrades }: { upgrades: Upgrades }) {
        return new SaveContext({ upgrades });
    }

    /** Returns a {@link SaveContext} object equivalent to game start. */
    public static empty(): SaveContext {
        return SaveContext.of({ upgrades: Upgrades.empty() });
    }

    /**
     * Parses and returns a {@link SaveContext} object from the given serialized
     * format.
     */
    public static deserialize(data: SerializedSaveContext): SaveContext {
        return SaveContext.of({
            upgrades: Upgrades.deserialize(data.upgrades),
        });
    }
}

/** The serialized format of a {@link SaveContext} object. */
export interface SerializedSaveContext {
    upgrades: SerializedUpgrades;
}
