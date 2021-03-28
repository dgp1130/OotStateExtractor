using System;

namespace DevelWoutACause.OotStateExtractor {
    public sealed record Upgrades {
        public bool HasSticks;
        public bool HasNuts;

        // Memory value has format:
        // 0bAAAAAAAA_AAAABBBX_XXXXXXXX_XXXXXXXX
        // Where:
        // A - Deku Nuts, most significant bits are unused.
        // B - Deku Sticks.
        // X - Other data not currently used by extractor.
        public static Upgrades FromMemory(int value) {
            return new Upgrades {
                HasSticks = Convert.ToBoolean(value & 0b00000000_00001110_00000000_00000000),
                HasNuts = Convert.ToBoolean(value & 0b11111111_11110000_00000000_00000000),
            };
        }
    }
}
