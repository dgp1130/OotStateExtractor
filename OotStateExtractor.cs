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

        private Watch? watch;

        public bool AskSaveChanges() => true;

        public void Restart() {
            if (memoryDomains == null) {
                throw new Exception("Memory domains is not available.");
            }

            watch = Watch.GenerateWatch(
                memoryDomains.MainMemory,
                0x11A670 /* address */,
                WatchSize.DWord,
                DisplayType.Hex,
                true /* big endian */
            );
        }

        private string lastValue = "";
        public void UpdateValues(ToolFormUpdateType type) {
            // Only execute after a frame.
            if (type != ToolFormUpdateType.PostFrame) return;

            if ((object?) watch == null) {
                throw new Exception("Watch not initialized.");
            }

            watch.Update(GlobalWin.Config.RamWatchDefinePrevious);
            if (lastValue != watch.ValueString) {
                Console.WriteLine(
                    $"Address {watch.AddressString} has value {watch.ValueString}.");
                lastValue = watch.ValueString;
            }
        }
    }
}
