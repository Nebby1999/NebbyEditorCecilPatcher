using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NebbyEditorCecilPatcher.Patchers;


public class FieldPatcher : IPatcher
{
    public string TypeNameToPatch { get; init; }
    public string FieldName { get; init; }
    public string FieldTypeAssemblyName { get; init; }
    public string FieldTypeNamespaceName { get; init; }
    public string FieldTypeName { get; init; }
    public FieldAttributes FieldAttributes { get; init; }
    public void DoPatch(AssemblyDefinition assemblyDefinition)
    {
        TypeDefinition typeToPatch = assemblyDefinition.MainModule.GetType(TypeNameToPatch);

        if (typeToPatch == null)
            return;

        FieldDefinition newField = new FieldDefinition(FieldName, FieldAttributes, FindTypeReference(assemblyDefinition));
        typeToPatch.Fields.Add(newField);
    }

    private TypeReference FindTypeReference(AssemblyDefinition definition)
    {
        var scope = definition.MainModule.AssemblyReferences.OrderByDescending(a => a.Version).FirstOrDefault(a => a.Name == FieldTypeAssemblyName);
        var typeRef = new TypeReference(FieldTypeNamespaceName, FieldTypeName, definition.MainModule, scope);
        return typeRef;
    }

    public FieldPatcher(PatchParser.FieldPatchMetadata metadata)
    {
        TypeNameToPatch = metadata.typeNameToPatch;
        FieldName = metadata.memberName;
        FieldTypeAssemblyName = metadata.fieldTypeAssemblyName;
        FieldTypeNamespaceName = metadata.fieldTypeNamespaceName;
        FieldTypeName = metadata.fieldTypeName;
        FieldAttributes = metadata.fieldAttributes;
    }
}
