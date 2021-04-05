import { Equipment, SerializedEquipment } from './equipment';
import { SerializedUpgrades, Upgrades } from './upgrades';

/** Overall save context for the game containing all relevant game state. */
export class SaveContext {
    public readonly equipment: Equipment;
    public readonly upgrades: Upgrades;

    private constructor({ equipment, upgrades }: {
        equipment: Equipment,
        upgrades: Upgrades,
    }) {
        this.equipment = equipment;
        this.upgrades = upgrades;
    }

    /** Returns a {@link SaveContext} object with the given state. */
    public static of({ equipment, upgrades }: {
        equipment: Equipment,
        upgrades: Upgrades,
    }) {
        return new SaveContext({ equipment, upgrades });
    }

    /** Returns a {@link SaveContext} object equivalent to game start. */
    public static empty(): SaveContext {
        return SaveContext.of({
            equipment: Equipment.empty(),
            upgrades: Upgrades.empty(),
        });
    }

    /**
     * Parses and returns a {@link SaveContext} object from the given serialized
     * format.
     */
    public static deserialize(data: SerializedSaveContext): SaveContext {
        return SaveContext.of({
            equipment: Equipment.deserialize(data.equipment),
            upgrades: Upgrades.deserialize(data.upgrades),
        });
    }
}

/** The serialized format of a {@link SaveContext} object. */
export interface SerializedSaveContext {
    equipment: SerializedEquipment;
    upgrades: SerializedUpgrades;
}
