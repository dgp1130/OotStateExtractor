using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using BizHawk.Client.Common;
using BizHawk.Client.EmuHawk;
using BizHawk.Emulation.Common;
using DevelWoutACause.OotStateExtractor.Service;

namespace DevelWoutACause.OotStateExtractor {
    [ExternalTool("OoT State Extractor")]
    public sealed class Extractor : Form, IExternalToolForm {
        [RequiredService]
        public IMemoryDomains? memoryDomains { get; set; }

        private SaveContextWatcher? saveContextWatcher;

        public bool AskSaveChanges() => true;

        private bool serverStarted = false;
        private bool disposed = false;

        public void Restart() {
            if (memoryDomains == null) {
                throw new Exception("Memory domains is not available.");
            }

            if (saveContextWatcher != null) {
                saveContextWatcher.Updated -= this.saveContextChanged;
                saveContextWatcher.Dispose();
            }
            saveContextWatcher = SaveContextWatcher.Of(memoryDomains);
            saveContextWatcher.Updated += this.saveContextChanged;

            if (!serverStarted) {
                Task.Run(() => Server.Start());
                serverStarted = true;
            }
        }

        protected override void Dispose(bool disposing) {
            if (disposed) return;

            if (saveContextWatcher != null) {
                saveContextWatcher.Updated -= this.saveContextChanged;
                saveContextWatcher.Dispose();
            }

            disposed = true;

            base.Dispose(disposing);
        }

        private void saveContextChanged(SaveContext saveContext) {
            Console.WriteLine(saveContext);
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
