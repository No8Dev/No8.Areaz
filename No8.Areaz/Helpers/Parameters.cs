﻿using System.Collections;
using System.ComponentModel;

namespace No8.Areaz.Helpers;

public class Parameters : IEnumerable<KeyValuePair<string?, string?>>
{
    public static readonly Parameters Empty = new();
    private readonly Dictionary<string, string?> _dict;

    public Parameters() { _dict = new Dictionary<string, string?>(); }

    public Parameters(IDictionary<string, string?> dictionary)
    {
        _dict = new Dictionary<string, string?>(dictionary);
    }

    public Parameters(string key, object? value)
    {
        _dict = new Dictionary<string, string?> { { key, value?.ToString() ?? "" } };
    }

    public Parameters(object? obj)
    {
        _dict = new Dictionary<string, string?>();

        if (obj == null)
            return;

        foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(obj))
        {
            var name = property.DisplayName;
            var value = property.GetValue(obj);

            if (name != null && value != null)
                _dict.Add(name, value.ToString());
        }
    }

    public void AddAll(IDictionary<string?, object?>? values)
    {
        if (values == null) return;

        foreach (var pair in values)
            if (pair.Key != null)
                _dict.Add(pair.Key, pair.Value?.ToString() ?? "");
    }

    public int Count => _dict.Count;

    public IEnumerator<KeyValuePair<string?, string?>> GetEnumerator() { return _dict.GetEnumerator(); }

    public override string ToString()
    {
        if (_dict.Count == 0)
            return "[]";
        var sb = new StringBuilder();
        sb.Append("[\n");
        foreach (var pair in this)
            sb.Append($"  \"{pair.Key}\", \"{pair.Value}\"\n");
        sb.Append(']');
        return sb.ToString();
    }

    IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

    public bool ContainsKey(string key) { return _dict.ContainsKey(key); }
}
