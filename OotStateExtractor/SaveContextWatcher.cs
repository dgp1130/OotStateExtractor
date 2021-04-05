using System;
using BizHawk.Client.Common;
using BizHawk.Emulation.Common;
using DevelWoutACause.OotStateExtractor.Common;

namespace DevelWoutACause.OotStateExtractor {
    /** A `Watcher` for the entire game save context. */
    internal sealed class SaveContextWatcher
            : Watcher<SaveContext>, IDisposable {
        private SaveContext saveContext;
        private readonly EquipmentWatcher equipmentWatcher;
        private readonly UpgradesWatcher upgradesWatcher;
        private bool disposed;

        private SaveContextWatcher(
            EquipmentWatcher equipmentWatcher,
            UpgradesWatcher upgradesWatcher
        ) {
            this.saveContext = SaveContext.Empty();
            this.equipmentWatcher = equipmentWatcher;
            this.upgradesWatcher = upgradesWatcher;
            this.disposed = false;

            this.upgradesWatcher.Updated += this.upgradesUpdated;
            this.equipmentWatcher.Updated += this.equipmentUpdated;

        }

        /** Returns a `SaveContextWatcher` for the given memory domain. */
        public static SaveContextWatcher Of(IMemoryDomains memoryDomains) {
            return new SaveContextWatcher(
                equipmentWatcher: EquipmentWatcher.Of(memoryDomains),
                upgradesWatcher: UpgradesWatcher.Of(memoryDomains)
            );
        }

        public event EventHandler<SaveContext>? Updated;

        private void equipmentUpdated(object sender, Equipment equipment) {
            saveContext = saveContext with { Equipment = equipment };
            Updated?.Invoke(this, saveContext);
        }

        private void upgradesUpdated(object sender, Upgrades upgrades) {
            saveContext = saveContext with { Upgrades = upgrades };
            Updated?.Invoke(this, saveContext);
        }

        /**
         * Updates the save context based on memory changes and emits a new
         * context if applicable.
         */
        public void Update(PreviousType previousType) {
            equipmentWatcher.Update(previousType);
            upgradesWatcher.Update(previousType);
        }

        public void Dispose() {
            if (disposed) return;

            equipmentWatcher.Updated -= this.equipmentUpdated;
            upgradesWatcher.Updated -= this.upgradesUpdated;

            disposed = true;
        }
    }
}
