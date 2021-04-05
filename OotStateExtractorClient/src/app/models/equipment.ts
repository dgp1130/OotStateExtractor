/**
 * Owned equipment. "Equipment" in this context refers to swords, shields,
 * tunics, and boots. Notably does **not** include items like sticks, bombs,
 * slingshot, etc.
 */
export class Equipment {
    public readonly hasKokiriSword: boolean;
    public readonly hasDekuShield: boolean;

    private constructor({ hasKokiriSword, hasDekuShield }: {
        hasKokiriSword: boolean,
        hasDekuShield: boolean,
    }) {
        this.hasKokiriSword = hasKokiriSword;
        this.hasDekuShield = hasDekuShield;
    }

    /** Returns an {@link Equipment} object with the given state. */
    public static of({ hasKokiriSword, hasDekuShield }: {
        hasKokiriSword: boolean,
        hasDekuShield: boolean,
    }): Equipment {
        return new Equipment({ hasKokiriSword, hasDekuShield });
    }

    /** Returns an {@link Equipment} object which contains no items. */
    public static empty(): Equipment {
        return Equipment.of({ hasKokiriSword: false, hasDekuShield: false });
    }

    /**
     * Parses and returns an {@link Equipment} object from the given serialized
     * format.
     */
    public static deserialize(data: SerializedEquipment): Equipment {
        return Equipment.of({
            hasKokiriSword: data.has_kokiri_sword,
            hasDekuShield: data.has_deku_shield,
        });
    }
}

/** The serialized format of an {@link Equipment} object. */
export interface SerializedEquipment {
    has_kokiri_sword: boolean;
    has_deku_shield: boolean;
}
