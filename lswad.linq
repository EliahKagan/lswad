<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Windows.Forms.dll</Reference>
  <Namespace>System.IO.MemoryMappedFiles</Namespace>
  <Namespace>System.Windows.Forms</Namespace>
</Query>

[Serializable]
internal sealed class UnreachableException : NotSupportedException { }

internal static class Status {
    private static void Alert(string message)
            => Console.Error.WriteLine(Util.WithStyle(message, "color:red"));
    
    internal static void Die(string message)
    {
        Alert("ERROR: " + message);
        
        MessageBox.Show(message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
        
        Environment.Exit(1);
    }
    
    internal static void Warn(string message) => Alert("WARNING: " + message);
}

internal static class ArrayOfByteEx {
    internal static string AsName(this byte[] data)
    {
        var len = Array.FindLastIndex(data, b => b != 0) + 1;
        
        try { return Encoding.UTF8.GetString(data, 0, len); }
        catch (ArgumentException) { return null; } // indicates decoding error
    }
}

internal static class Int64Ex {
    internal static string AsName(this long data)
            => BitConverter.GetBytes(data).AsName();
}

internal struct RawEntry { // an entry in a WAD directory as it's really stored
    public int Position, Length;
    public long NameBytes;
}

internal struct Entry { // an entry in a WAD directory, the way we think of it
    public readonly int Position, Length;
    public readonly string Name;
    
    public Entry(RawEntry ent)
    {
        Position = ent.Position;
        Length = ent.Length;
        Name = ent.NameBytes.AsName();
    }
}

internal static class Program {
    private static string Path = @"C:\Users\ek\WADs (local)\DOOM.WAD";
    
    private static string Sanitize(string s) // TODO: do more than prevent vulns
            => s.Replace("<", "&lt;").Replace(">", "&gt;");
    
    private static void ChooseFile(bool use_default)
    {
        if (use_default) return;
        
        using (var dialog = new OpenFileDialog()) {
            dialog.Title = "Open WAD File";
            dialog.Filter = "Game data files (*.WAD)|*.WAD|All files (*.*)|*.*";
            if (dialog.ShowDialog() == DialogResult.Cancel) Environment.Exit(0);
            Path = dialog.FileName;
        }
    }
    
    private static string ReadWadHeader(this MemoryMappedViewAccessor accessor,
                                        out int dir_pos, out int dir_len)
    {
        try {
            var type_data = new byte[4];
            accessor.ReadArray(0L, type_data, 0, type_data.Length);
            var type = type_data.AsName();
            
            if (!(type == "IWAD" || type == "PWAD"))
                throw new ArgumentException();
            
            dir_len = accessor.ReadInt32(4); // length in 16-byte entries
            dir_pos = accessor.ReadInt32(8);
            return type;
        }
        catch (ArgumentException) {
            Status.Die($"\"{Path}\" doesn't seem like a WAD file.");
            throw new UnreachableException();
        }
    }
    
    private static IEnumerable<Entry> ReadWadDirectory(
            this MemoryMappedViewAccessor accessor, int dir_pos, int dir_len)
    {
        var raw_entries = new RawEntry[dir_len];
        
        int len;
        try {
            len = accessor.ReadArray(dir_pos, raw_entries, 0, dir_len);
        }
        catch (ArgumentOutOfRangeException) {
            Status.Die($"Directory position {dir_pos} is outside the file!");
            throw new UnreachableException();
        }
        
        if (len != dir_len)
            Status.Warn($"Only {len} of {dir_len} entries found.");
        
        return raw_entries.Take(len).Select(ent => new Entry(ent));
    }
    
    private static void ListWadEntries(this MemoryMappedViewAccessor accessor)
    {
        int dir_pos, dir_len;
        var type = accessor.ReadWadHeader(out dir_pos, out dir_len);
        
        Util.RawHtml($"<pre>Opened {type} <i>{Sanitize(Path)}</i>.</pre>")
            .Dump();
        
        accessor.ReadWadDirectory(dir_pos, dir_len).Dump();
    }
    
    private static void Main()
    {
        ChooseFile(false);
        
        using (var file = MemoryMappedFile.CreateFromFile(Path))
        using (var accessor = file.CreateViewAccessor(
                                    0L, 0L, MemoryMappedFileAccess.Read)) {
            accessor.ListWadEntries();
        }
    }
}
