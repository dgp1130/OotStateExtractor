using BizHawk.Client.Common;
using BizHawk.Emulation.Common;
using DevelWoutACause.OotStateExtractor.Common;
using System;

using DisplayType = BizHawk.Client.Common.DisplayType;

namespace DevelWoutACause.OotStateExtractor {
    /** A `Watcher` for the player's equipment. */
    internal sealed class EquipmentWatcher : MemoryWatcher<Equipment> {
        private EquipmentWatcher(Watch watch)
            : base(watch, EquipmentWatcher.deserialize) { }

        public static EquipmentWatcher Of(IMemoryDomains memoryDomains) {
            return new EquipmentWatcher(Watch.GenerateWatch(
                memoryDomains.MainMemory,
                0x11A66C /* address */,
                WatchSize.Word,
                DisplayType.Hex,
                true /* big endian */
            ));
        }

        // Memory value has format:
        // 0bXXXXXXXX_XAAAXBBB
        // Where:
        // A - Swords.
        //     *   Least significant bit is Kokiri Sword.
        //     *   Middle significant bit is Master Sword.
        //     *   Most significant bit is Biggoron Sword.
        // B - Shields.
        //     *   Least significant bit is Deku Shield.
        //     *   Middle significant bit is Hylian Shield.
        //     *   Most significant bit is Mirror Shield.
        // X - Other data not currently used by extractor.
        private static Equipment deserialize(int value) {
            return new Equipment {
                HasKokiriSword = Convert.ToBoolean(value & 0b00000000_00000001),
                HasDekuShield = Convert.ToBoolean(value & 0b00000000_00010000),
            };
        }
    }
}
