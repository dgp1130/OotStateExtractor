using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using BizHawk.Client.Common;
using BizHawk.Client.EmuHawk;
using BizHawk.Emulation.Common;
using DevelWoutACause.OotStateExtractor.Common;
using DevelWoutACause.OotStateExtractor.Service;

namespace DevelWoutACause.OotStateExtractor {
    [ExternalTool("OoT State Extractor")]
    public sealed class Extractor : Form, IExternalToolForm {
        [RequiredService]
        public IMemoryDomains? memoryDomains { get; set; }

        private SaveContextWatcher? saveContextWatcher;
        private LatestEmission<SaveContext>? latestSaveContext;
        private bool initialized = false;
        private bool disposed = false;

        public bool AskSaveChanges() => true;

        public void Restart() {
            if (memoryDomains == null) {
                throw new Exception("Memory domains is not available.");
            }

            latestSaveContext?.Dispose();
            saveContextWatcher?.Dispose();

            if (!initialized) {
                saveContextWatcher = SaveContextWatcher.Of(memoryDomains);

                latestSaveContext = LatestEmission<SaveContext>.Of(
                    initial: SaveContext.Empty(),
                    subscribe: (emit) => saveContextWatcher.Updated += emit,
                    unsubscribe: (emit) => saveContextWatcher.Updated -= emit
                );

                Task.Run(() => Server.Start(latestSaveContext));

                initialized = true;
            }
        }

        protected override void Dispose(bool disposing) {
            if (disposed) return;

            latestSaveContext?.Dispose();
            saveContextWatcher?.Dispose();

            disposed = true;

            base.Dispose(disposing);
        }

        public void UpdateValues(ToolFormUpdateType type) {
            // Only execute after a frame.
            if (type != ToolFormUpdateType.PostFrame) return;

            if (saveContextWatcher == null) {
                throw new Exception("saveContextWatcher not initialized.");
            }

            saveContextWatcher.Update(GlobalWin.Config.RamWatchDefinePrevious);
        }
    }
}
