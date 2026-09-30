# Diagnostics

| ID | Severity | Description | How to fix |
|---|---|---|---|
| SNV0001 | ❌ Error | `[ViewSource]` method is not `static partial`, or has an implementation written | Declare the method as `static partial` without an implementation |
| SNV0002 | ❌ Error | `[ViewSource]` method has parameters | Remove the parameters from the method |
| SNV0003 | ❌ Error | `[ViewSource]` method does not return `IEnumerable<KeyValuePair<ViewId, Type>>` | Change the return type to `IEnumerable<KeyValuePair<ViewId, Type>>` |
| SNV0004 | ❌ Error | `[View]` class is file-local or nested in a `private` / `protected` type, so the generated registration cannot refer to it; it is not registered | Make the class visible in the assembly |
| SNV0005 | ❌ Error | `[ViewSource]` method or type name differs only in case from another one, so the generated file names collide; only the first (in ordinal order) is generated | Rename one of the methods or types |
