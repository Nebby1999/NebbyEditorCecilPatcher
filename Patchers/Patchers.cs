using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NebbyEditorCecilPatcher.Patchers;

internal class FieldPatcher : IPatcher
{
    public string TypeNameToPatch { get; private set; }
    public string MemberName { get; private set; }
    public string FieldTypeName { get; private set; }
    public FieldAttributes FieldAttributes { get; private set; }
    public void DoPatch(AssemblyDefinition assemblyDefinition, TypeDefinition[] typeDefinitions)
    {
        foreach(var definition in typeDefinitions)
        {
            if (definition.FullName != TypeNameToPatch)
                continue;

            definition.Fields.Add(new FieldDefinition(MemberName, FieldAttributes, GetFieldType(typeDefinitions)));
        }
    }

    private TypeReference GetFieldType(TypeDefinition[] definitions)
    {
        foreach(var definition in definitions)
        {
            if(definition.FullName == FieldTypeName)
            {
                return definition;
            }
        }
        return null;
    }

    public IPatcher ParsePatcherArguments(string[] arguments)
    {
        TypeNameToPatch = arguments[0];
        MemberName = arguments[1];
        return this;
    }
}

internal class PropertyPatcher : IPatcher
{
    public string TypeNameToPatch => throw new NotImplementedException();

    public string MemberName => throw new NotImplementedException();

    public void DoPatch(AssemblyDefinition assemblyDefinition, TypeDefinition[] typeDefinitions)
    {
        throw new NotImplementedException();
    }

    public IPatcher ParsePatcherArguments(string[] arguments)
    {
        throw new NotImplementedException();
    }
}

internal class MethodPatcher : IPatcher
{
    public string TypeNameToPatch => throw new NotImplementedException();

    public string MemberName => throw new NotImplementedException();

    public void DoPatch(AssemblyDefinition assemblyDefinition, TypeDefinition[] typeDefinitions)
    {
        throw new NotImplementedException();
    }

    public IPatcher ParsePatcherArguments(string[] arguments)
    {
        throw new NotImplementedException();
    }
}