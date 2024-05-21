using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NebbyEditorCecilPatcher.Patchers
{
    internal class PatchParser
    {
        public IPatcher[] Patchers { get; private set; }
        private string[] _patcherStringRepresentations;

        public PatchParser(string[] patcherStringRepresentations)
        {
            Patchers = new IPatcher[patcherStringRepresentations.Length];
            _patcherStringRepresentations = patcherStringRepresentations;
        }

        public bool TryParse()
        {
            int index = -1;
            try
            {
                for(int i = 0; i < _patcherStringRepresentations.Length; i++)
                {
                    index = i;
                    Patchers[i] = Parse(_patcherStringRepresentations[i]);
                }
                return true;
            }
            catch(Exception ex)
            {
                Program.Log($"Failed to parse patcher at index {index}. {ex}");
                return false;
            }
        }

        private IPatcher Parse(string stringRepresentation)
        {
            string[] allArguments = stringRepresentation.Split(',');
            string[] patcherArguments = new ArraySegment<string>(allArguments, 1, allArguments.Length).ToArray();
            string patcherType = allArguments[0];
            switch(patcherType)
            {
                case PatcherConstants.FIELD_PATCHER:
                    return new FieldPatcher().ParsePatcherArguments(patcherArguments);
                case PatcherConstants.METHOD_PATCHER:
                    return new MethodPatcher().ParsePatcherArguments(patcherArguments);
                case PatcherConstants.PROPERTY_PATCHER:
                    return new PropertyPatcher().ParsePatcherArguments(patcherArguments);
            }
            throw new ArgumentException($"patcherType string is set to an invalid value. Value={patcherType}");
        }

        public static class PatcherConstants
        {
            public const string FIELD_PATCHER = "patchType=FIELD";
            public const string METHOD_PATCHER = "patchType=METHOD";
            public const string PROPERTY_PATCHER = "patchType=PROPERTY";
        }
    }
}
