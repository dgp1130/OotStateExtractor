using System;
using BizHawk.Client.Common;
using BizHawk.Emulation.Common;
using DevelWoutACause.OotStateExtractor.Common;

namespace DevelWoutACause.OotStateExtractor {
    /** A `Watcher` for the entire game save context. */
    internal sealed class SaveContextWatcher
            : Watcher<SaveContext>, IDisposable {
        private SaveContext saveContext;
        private readonly UpgradesWatcher upgradesWatcher;
        private bool disposed;

        private SaveContextWatcher(UpgradesWatcher upgradesWatcher) {
            this.saveContext = SaveContext.Empty();
            this.upgradesWatcher = upgradesWatcher;
            this.disposed = false;

            // HACK: This event handler is never cleaned up and is likely a
            // memory leak.
            this.upgradesWatcher.Updated += this.upgradesUpdated;
        }

        /** Returns a `SaveContextWatcher` for the given memory domain. */
        public static SaveContextWatcher Of(IMemoryDomains memoryDomains) {
            return new SaveContextWatcher(
                upgradesWatcher: UpgradesWatcher.Of(memoryDomains)
            );
        }

        public event EventHandler<SaveContext>? Updated;
        private void upgradesUpdated(object sender, Upgrades upgrades) {
            saveContext = saveContext with { Upgrades = upgrades };
            Updated?.Invoke(this, saveContext);
        }

        /**
         * Updates the save context based on memory changes and emits a new
         * context if applicable.
         */
        public void Update(PreviousType previousType) {
            upgradesWatcher.Update(previousType);
        }

        public void Dispose() {
            if (disposed) return;
            
            upgradesWatcher.Updated -= this.upgradesUpdated;

            disposed = true;
        }
    }
}
