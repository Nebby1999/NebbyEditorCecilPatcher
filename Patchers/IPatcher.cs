using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NebbyEditorCecilPatcher.Patchers;

public interface IPatcher
{
    string TypeNameToPatch { get; }
    string MemberName { get; }
    void DoPatch(AssemblyDefinition assemblyDefinition, TypeDefinition[] typeDefinitions);
    IPatcher ParsePatcherArguments(string[] arguments);
}
