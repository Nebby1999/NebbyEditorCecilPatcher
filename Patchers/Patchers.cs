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
    public bool IsArray { get; init; }
    public string? FieldTypeAssemblyName { get; init; }
    public string? FieldTypeNamespaceName { get; init; }
    public string? FieldTypeName { get; init; }
    public System.Reflection.Metadata.PrimitiveTypeCode PrimitiveType { get; init; } 
    public FieldAttributes FieldAttributes { get; init; }

    private bool _hasDataForFindingTypeRef;
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
        if(_hasDataForFindingTypeRef)
        {
            var scope = definition.MainModule.AssemblyReferences.OrderByDescending(a => a.Version).FirstOrDefault(a => a.Name == FieldTypeAssemblyName!);
            var typeRef = new TypeReference(FieldTypeNamespaceName!, FieldTypeName!, definition.MainModule, scope);
            return IsArray ? new ArrayType(typeRef) : typeRef;
        }
        else
        {
            TypeReference typeReference = default;
            TypeSystem typeSystem = definition.MainModule.TypeSystem;
            switch (PrimitiveType)
            {
                case System.Reflection.Metadata.PrimitiveTypeCode.Boolean:
                    typeReference = typeSystem.Boolean;
                    break;
                case System.Reflection.Metadata.PrimitiveTypeCode.Byte:
                    typeReference = typeSystem.Byte;
                    break;
                case System.Reflection.Metadata.PrimitiveTypeCode.Char:
                    typeReference = typeSystem.Char;
                    break;
                case System.Reflection.Metadata.PrimitiveTypeCode.Double:
                    typeReference = typeSystem.Double;
                    break;
                case System.Reflection.Metadata.PrimitiveTypeCode.Int16:
                    typeReference = typeSystem.Int16;
                    break;
                case System.Reflection.Metadata.PrimitiveTypeCode.Int32:
                    typeReference = typeSystem.Int32;
                    break;
                case System.Reflection.Metadata.PrimitiveTypeCode.Int64:
                    typeReference = typeSystem.Int64;
                    break;
                case System.Reflection.Metadata.PrimitiveTypeCode.IntPtr:
                    typeReference = typeSystem.IntPtr;
                    break;
                case System.Reflection.Metadata.PrimitiveTypeCode.Object:
                    typeReference = typeSystem.Object;
                    break;
                case System.Reflection.Metadata.PrimitiveTypeCode.SByte:
                    typeReference = typeSystem.SByte;
                    break;
                case System.Reflection.Metadata.PrimitiveTypeCode.Single:
                    typeReference = typeSystem.Single;
                    break;
                case System.Reflection.Metadata.PrimitiveTypeCode.String:
                    typeReference = typeSystem.String;
                    break;
                case System.Reflection.Metadata.PrimitiveTypeCode.TypedReference:
                    typeReference = typeSystem.TypedReference;
                    break;
                case System.Reflection.Metadata.PrimitiveTypeCode.UInt16:
                    typeReference = typeSystem.UInt16;
                    break;
                case System.Reflection.Metadata.PrimitiveTypeCode.UInt32:
                    typeReference = typeSystem.UInt32;
                    break;
                case System.Reflection.Metadata.PrimitiveTypeCode.UInt64:
                    typeReference = typeSystem.UInt64;
                    break;
                case System.Reflection.Metadata.PrimitiveTypeCode.UIntPtr:
                    typeReference = typeSystem.UIntPtr;
                    break;
                case System.Reflection.Metadata.PrimitiveTypeCode.Void:
                    typeReference = typeSystem.Void;
                    break;
            }
            return IsArray ? new ArrayType(typeReference) : typeReference!;
        }
    }

    public FieldPatcher(PatchParser.FieldPatchMetadata metadata)
    {
        TypeNameToPatch = metadata.typeNameToPatch;
        FieldName = metadata.memberName;
        FieldAttributes = metadata.fieldAttributes;

        IsArray = metadata.isArray;

        _hasDataForFindingTypeRef = (metadata.fieldTypeAssemblyName is not null && metadata.fieldTypeNamespaceName is not null && metadata.fieldTypeName is not null);
        if(_hasDataForFindingTypeRef)
        {
            FieldTypeAssemblyName = metadata.fieldTypeAssemblyName!;
            FieldTypeNamespaceName = metadata.fieldTypeNamespaceName!;
            FieldTypeName = metadata.fieldTypeName!;
        }
        PrimitiveType = metadata.primitiveType;
    }
}
