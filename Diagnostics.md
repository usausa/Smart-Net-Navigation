# Diagnostics

| ID | Severity | Description | How to fix |
|---|---|---|---|
| SNV0001 | ❌ Error | `[ViewSource]` method is not a partial extension method | Declare the method as a `partial` extension method |
| SNV0002 | ❌ Error | `[ViewSource]` method has parameters | Remove the parameters from the method |
| SNV0003 | ❌ Error | `[ViewSource]` method does not return `IEnumerable<KeyValuePair<ViewId, Type>>` | Change the return type to `IEnumerable<KeyValuePair<ViewId, Type>>` |
