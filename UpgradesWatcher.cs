using System;
using BizHawk.Client.Common;
using BizHawk.Emulation.Common;

using DisplayType = BizHawk.Client.Common.DisplayType;

namespace DevelWoutACause.OotStateExtractor {
    /** A `Watcher` for player upgrades. */
    internal sealed class UpgradesWatcher : MemoryWatcher<Upgrades> {
        private UpgradesWatcher(Watch watch)
                : base(watch, UpgradesWatcher.deserialize) { }

        public static UpgradesWatcher Of(IMemoryDomains memoryDomains) {
            return new UpgradesWatcher(Watch.GenerateWatch(
                memoryDomains.MainMemory,
                0x11A670 /* address */,
                WatchSize.DWord,
                DisplayType.Hex,
                true /* big endian */
            ));
        }

        // Memory value has format:
        // 0bAAAAAAAA_AAAABBBX_XXXXXXXX_XXXXXXXX
        // Where:
        // A - Deku Nuts, most significant bits are unused.
        // B - Deku Sticks.
        // X - Other data not currently used by extractor.
        private static Upgrades deserialize(int value) {
            return new Upgrades {
                HasSticks = Convert.ToBoolean(
                    value & 0b00000000_00001110_00000000_00000000
                ),
                HasNuts = Convert.ToBoolean(
                    value & 0b11111111_11110000_00000000_00000000
                ),
            };
        }
    }
}
