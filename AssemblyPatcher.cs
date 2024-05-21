using Mono.Cecil;

namespace NebbyEditorCecilPatcher
{
    public class AssemblyPatcher
    {
        private PatchData[] _patchData;
        private AssemblyDefinition _assembly;
        public void Patch()
        {
            foreach(PatchData patchData in _patchData)
            {
                patchData.DoPatch();
            }
        }

        public AssemblyPatcher(string[] patches, ReaderParameters readerParams, AssemblyDefinition assemblyDefinition)
        {
            _assembly = assemblyDefinition;
            _patchData = new PatchData[patches.Length];
            for(int i = 0; i < patches.Length; i++)
            {
                _patchData[i] = new PatchData(patches[i], assemblyDefinition);
            }
        }

        private class PatchData
        {
            public TypeDefinition TargetTypeDefinition { get; set; }

            public PatchType TypeOfPatch { get; set; }
            public void DoPatch()
            {
                switch (TypeOfPatch)
                {
                    case PatchType.Invalid:
                        break;
                    case PatchType.Field:
                        PatchField();
                        break;
                    case PatchType.Property:
                        PatchProperty();
                        break;
                    case PatchType.Method:
                        PatchMethod();
                        break;
                }
            }

            private void PatchField()
            {

            }

            private void PatchProperty()
            {

            }

            private void PatchMethod()
            {

            }

            public PatchData()
            {

            }

            public PatchData(string patch, AssemblyDefinition definition)
            {
                string[] patchAsArray = patch.Split(',');

                TargetTypeDefinition = definition.MainModule.GetType(patchAsArray[0]);

                PatchType patchType = PatchType.Invalid;
                switch(patchAsArray[1])
                {
                    case "FIELD":
                        patchType = PatchType.Field;
                        break;
                    case "PROPERTY":
                        patchType = PatchType.Property;
                        break;
                    case "METHOD":
                        patchType = PatchType.Method;
                        break;
                }
                TypeOfPatch = patchType;
            }

            public enum PatchType
            {
                Invalid,
                Field,
                Property,
                Method
            }
        }
    }
}