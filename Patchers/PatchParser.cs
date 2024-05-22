using Mono.Cecil;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace NebbyEditorCecilPatcher.Patchers
{
    public class PatchParser
    {
        public IPatcher[] Patchers { get; private set; }
        private string _pathToPatchesJSON;

        public PatchParser(string pathToPatchesJSON)
        {
            _pathToPatchesJSON = pathToPatchesJSON;
        }

        public bool TryParse()
        {
            try
            {
                string json = File.ReadAllText(_pathToPatchesJSON);
                PatchesMetadata metadata = JsonConvert.DeserializeObject<PatchesMetadata>(json);

                Patchers = new IPatcher[metadata.GetTotalPatchesCount()];
                for(int i = 0; i < metadata.fieldPatches.Length; i++)
                {
                    Patchers[i] = new FieldPatcher(metadata.fieldPatches[i]);
                }
                return true;
            }
            catch(Exception ex)
            {
                Program.Log(ex);
            }
            return false;
        }

        [Serializable]
        public class PatchesMetadata
        {
            public FieldPatchMetadata[] fieldPatches;

            public int GetTotalPatchesCount()
            {
                return fieldPatches.Length;
            }
        }

        [Serializable]
        public class FieldPatchMetadata
        {
            public string typeNameToPatch;
            public string memberName;
            public string fieldTypeAssemblyName;
            public string fieldTypeNamespaceName;
            public string fieldTypeName;

            [JsonConverter(typeof(StringEnumConverter))]
            public FieldAttributes fieldAttributes;
        }
    }
}
