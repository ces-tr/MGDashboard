using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Reflection;

namespace MGDash
{
    [ComImport, CompilerGenerated, Guid("F935DC23-1CF0-11D0-ADB9-00C04FD58A0B"), DefaultMember("FullName"), TypeIdentifier]
    public interface IWshShortcut
    {
        [DispId(0x3e9)]
        string Description { [return: MarshalAs(UnmanagedType.BStr)] [DispId(0x3e9)] get; [param: In, MarshalAs(UnmanagedType.BStr)] [DispId(0x3e9)] set; }

        [DispId(0x3ed)]
        string TargetPath { [return: MarshalAs(UnmanagedType.BStr)] [DispId(0x3ed)] get; [param: In, MarshalAs(UnmanagedType.BStr)] [DispId(0x3ed)] set; }

        [DispId(0x3ef)]
        string WorkingDirectory { [return: MarshalAs(UnmanagedType.BStr)] [DispId(0x3ef)] get; [param: In, MarshalAs(UnmanagedType.BStr)] [DispId(0x3ef)] set; }
        
        [DispId(0x7d1)]
        void Save();
    }

    [ComImport, CoClass(typeof(object)), Guid("41904400-BE18-11D3-A28B-00104BD35090"), CompilerGenerated, TypeIdentifier]
    public interface GInterface3 : GInterface4, GInterface5, GInterface6
    {
    }

    [ComImport, Guid("41904400-BE18-11D3-A28B-00104BD35090"), CompilerGenerated, TypeIdentifier]
    public interface GInterface4 : GInterface5, GInterface6
    {
        

        [DispId(0x3ea)]
        object CreateShortcut([In] string string_0);
    }

    [ComImport, CompilerGenerated, Guid("24BE5A30-EDFE-11D2-B933-00104B365C9F"), TypeIdentifier]
    public interface GInterface5 : GInterface6
    {
    }

    [ComImport, TypeIdentifier, CompilerGenerated, Guid("F935DC21-1CF0-11D0-ADB9-00C04FD58A0B")]
    public interface GInterface6
    {
    }
}

