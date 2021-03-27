using System.Windows.Forms;
using BizHawk.Client.Common;

namespace DevelWoutACause.OotStateExtractor {
    [ExternalTool("OoT State Extractor")]
    public sealed class Extractor : Form, IExternalToolForm {
        [RequiredApi]
        public IMemoryApi? memApi { get; set; }

        public bool AskSaveChanges() => true;

        public void Restart() {}

        public void UpdateValues(ToolFormUpdateType type) {}
    }
}
