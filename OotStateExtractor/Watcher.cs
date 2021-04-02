using BizHawk.Client.Common;
using System;

namespace DevelWoutACause.OotStateExtractor {
    /**
     * Represents a watched object whose value may change each frame and is
     * emitted in the `Updated` event whenever its data changes.
     */
    internal interface Watcher<T> {
        // Updates the current state based on changes within the last frame.
        void Update(PreviousType previousType);
        
        // Event triggered whenever the value is updated. The emitted value may
        // not **necessarily** be different from its previous value.
        event EventHandler<T>? Updated;
    }

    /** Represents a watched object loaded directly from game memory. */
    internal abstract class MemoryWatcher<T> : Watcher<T> {
        protected delegate T Deserializer(int value);

        private readonly Watch watch;
        private readonly Deserializer deserialize;

        protected MemoryWatcher(Watch watch, Deserializer deserialize) {
            this.watch = watch;
            this.deserialize = deserialize;
        }

        /**
         * Updates this `Watcher` based on changes to current memory and
         * invokes the `Updated` event if applicable.
         */
        public void Update(PreviousType previousType) {
            watch.Update(previousType);
            if (watch.Value != watch.Previous) {
                Updated?.Invoke(this, deserialize(watch.Value));
            }
        }

        public event EventHandler<T>? Updated;
    }
}
