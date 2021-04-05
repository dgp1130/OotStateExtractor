using System;

namespace DevelWoutACause.OotStateExtractor.Common {
    /**
     * A subscribable event with the most recently emitted
     * value. Example usage:
     * 
     * ```cs
     * internal sealed class Example {
     *     private event EventHandler<string> emitString;
     * 
     *     private void useLatestEmission() {
     *         var latest = LatestEmission.Of<string>(
     *             initial: "",
     *             subscribe: (emit) => emitString += emit,
     *             unsubscribe: (emit) => emitString -= emit
     *         );
     * 
     *         Console.WriteLine(latest.Value); // ""
     * 
     *         emitString?.Invoke(this, "Hello");
     *         Console.WriteLine(latest.Value); // "Hello"
     * 
     *         emitString?.Invoke(this, "World");
     *         Console.WriteLine(latest.Value); // "World"
     * 
     *         latest.Dispose(); // Don't forget to dispose!
     *     }
     * }
     * ```
     */
    public sealed class EventWithLatest<T> : IDisposable {
        public delegate void Subscribe(EventHandler<T> handler);
        public delegate void Unsubscribe(EventHandler<T> handler);

        public T Value { get; private set; }
        private readonly Unsubscribe unsubscribe;
        public event EventHandler<T>? OnEmit;
        private bool disposed = false;

        private EventWithLatest(T value, Unsubscribe unsubscribe) {
            this.Value = value;
            this.unsubscribe = unsubscribe;
        }

        private void emit(object sender, T value) {
            this.Value = value;
            this.OnEmit?.Invoke(this, value);
        }

        public static EventWithLatest<T> Of(
            T initial,
            Subscribe subscribe,
            Unsubscribe unsubscribe
        ) {
            var latest = new EventWithLatest<T>(
                value: initial,
                unsubscribe: unsubscribe
            );

            subscribe(latest.emit);

            return latest;
        }

        public void Dispose() {
            if (disposed) return;

            unsubscribe(emit);

            disposed = true;
        }
    }
}
