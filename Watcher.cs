using BizHawk.Client.Common;

namespace DevelWoutACause.OotStateExtractor {
    public sealed class Watcher {
        private Watch watch;

        private Watcher(Watch watch) {
            this.watch = watch;
        }

        public static Watcher Of(Watch watch) {
            return new Watcher(watch);
        }

        public void Update(PreviousType previousType) {
            watch.Update(previousType);
            if (watch.Value != watch.Previous) Changed?.Invoke(watch.Value);
        }

        public delegate void ChangedHandler(int value);
        public event ChangedHandler? Changed;
    }
}
