namespace Smart.Navigation.Components
{
    using System;
    using System.Collections.Generic;

    public class PageContextStorage
    {
        private sealed class ScopeEntry
        {
            private Dictionary<int, object>? map;

            public Dictionary<int, object> Map => map ??= new();

            public int Counter { get; set; }
        }

        private readonly Dictionary<string, ScopeEntry> entries = new();

        public void Push(string name)
        {
            if (!entries.TryGetValue(name, out var entry))
            {
                entry = new ScopeEntry();
                entries[name] = entry;
            }

            entry.Counter++;
        }

        public void Pop(string name)
        {
            if (!entries.TryGetValue(name, out var entry))
            {
                return;
            }

            entry.Counter--;
            if (entry.Counter > 0)
            {
                return;
            }

            foreach (var obj in entry.Map.Values)
            {
                (obj as IDisposable)?.Dispose();
            }

            entries.Remove(name);
        }

        public object Resolve(string name, int key, Func<object> factory)
        {
            if (entries.TryGetValue(name, out var entry))
            {
                return entry.Map[key];
            }

            entry = new ScopeEntry();
            entries[name] = entry;

            var component = factory();
            entry.Map[key] = component;

            return component;
        }
    }
}
