namespace Smart.Navigation.Generator.Helpers;

using System.Diagnostics;
using System.Globalization;
using System.Text;

[DebuggerDisplay("{ToString(),nq}")]
public sealed class SourceBuilder
{
    private readonly StringBuilder buffer;

    private int indentLevel;

    private string indent = string.Empty;

    public int Length
    {
        get => buffer.Length;
        set => buffer.Length = value;
    }

    public int IndentLevel
    {
        get => indentLevel;
        set
        {
            indentLevel = value;
            indent = new string(' ', indentLevel * 4);
        }
    }

    public SourceBuilder(int size = 8192)
    {
        buffer = new StringBuilder(size);
    }

    public override string ToString() => buffer.ToString();

    // ------------------------------------------------------------
    // Clear
    // ------------------------------------------------------------

    public void Clear()
    {
        buffer.Clear();
        indentLevel = 0;
        indent = string.Empty;
    }

    // ------------------------------------------------------------
    // Basic
    // ------------------------------------------------------------

    public SourceBuilder NewLine()
    {
        buffer.AppendLine();
        return this;
    }

    public SourceBuilder Indent()
    {
        buffer.Append(indent);
        return this;
    }

    public SourceBuilder Append(char value)
    {
        buffer.Append(value);
        return this;
    }

    public SourceBuilder Append(string value)
    {
        buffer.Append(value);
        return this;
    }

    public SourceBuilder Append<T>(T? value)
    {
        if (value is not null)
        {
            buffer.Append(value);
        }
        return this;
    }

    public SourceBuilder AppendFormat(string format, params object[] args)
    {
        buffer.AppendFormat(CultureInfo.InvariantCulture, format, args);
        return this;
    }

    public SourceBuilder AppendJoin<T>(IEnumerable<T> source, string separator)
    {
        foreach (var value in source)
        {
            buffer.Append(value);
            buffer.Append(separator);
        }

        buffer.Length -= separator.Length;
        return this;
    }
}
