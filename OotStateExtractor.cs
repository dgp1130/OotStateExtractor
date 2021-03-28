using System;
using System.Windows.Forms;
using BizHawk.Client.Common;
using BizHawk.Client.EmuHawk;
using BizHawk.Emulation.Common;

using DisplayType = BizHawk.Client.Common.DisplayType;

namespace DevelWoutACause.OotStateExtractor {
    [ExternalTool("OoT State Extractor")]
    public sealed class Extractor : Form, IExternalToolForm {
        [RequiredService]
        public IMemoryDomains? memoryDomains { get; set; }

        private Watcher? watcher;

        public bool AskSaveChanges() => true;

        public void Restart() {
            if (memoryDomains == null) {
                throw new Exception("Memory domains is not available.");
            }

            watcher = Watcher.Of(Watch.GenerateWatch(
                memoryDomains.MainMemory,
                0x11A670 /* address */,
                WatchSize.DWord,
                DisplayType.Hex,
                true /* big endian */
            ));

            watcher.Changed += (value) => {
                Console.WriteLine($"Upgrades: {value}");
            };
        }

        public void UpdateValues(ToolFormUpdateType type) {
            // Only execute after a frame.
            if (type != ToolFormUpdateType.PostFrame) return;

            if (watcher == null) {
                throw new Exception("Watcher not initialized.");
            }

            watcher.Update(GlobalWin.Config.RamWatchDefinePrevious);
        }
    }
}
