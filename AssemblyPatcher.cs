using Mono.Cecil;
using NebbyEditorCecilPatcher.Patchers;

namespace NebbyEditorCecilPatcher;

public class AssemblyPatcher
{
    public AssemblyPatcherArguments Arguments { get; private set; }

    private IPatcher[] _patchers;
    private DefaultAssemblyResolver _resolver;
    private ReaderParameters _readerParameters;
    public bool Patch()
    {
        if (!SetupResolver())
            return false;

        PatchParser parser = new PatchParser(Arguments.PathToPatchesJSON);
        if (!parser.TryParse())
            return false;

        _patchers = parser.Patchers;

        Program.Log($"Trying to patch {Arguments.AssemblyToPatchPath} with {_patchers.Length} Patches");
        try
        {
            using var memoryStream = new MemoryStream(File.ReadAllBytes(Arguments.AssemblyToPatchPath));
            using var assemblyDefinition = AssemblyDefinition.ReadAssembly(memoryStream, _readerParameters);

            for(int i = 0; i < _patchers.Length; i++)
            {
                _patchers[i].DoPatch(assemblyDefinition);
            }

            // We write to a memory stream first to ensure that Mono.Cecil doesn't have any errors when producing the assembly.
            // Otherwise, if we're overwriting the same assembly and it fails, it will overwrite with a 0 byte file

            using var tempStream = new MemoryStream();
            assemblyDefinition.Write(tempStream);

            tempStream.Position = 0;
            using var outputStream = File.Open(Arguments.AssemblyOutputPath, FileMode.Create);

            tempStream.CopyTo(outputStream);
            return true;
        }
        catch(Exception ex)
        {
            Program.Log(ex);
        }

        return false;
    }

    private bool SetupResolver()
    {
        try
        {
            _resolver = new DefaultAssemblyResolver();

            //Adds the dependencies
            _resolver.AddSearchDirectory(Arguments.DependenciesPath);
            _readerParameters = new ReaderParameters()
            {
                AssemblyResolver = _resolver
            };

            //Adds the dll to patch
            _resolver.AddSearchDirectory(Path.GetDirectoryName(Arguments.AssemblyToPatchPath));
            return true;
        }
        catch(Exception ex)
        {
            Program.Log(ex);
        }
        return false;
    }

    public AssemblyPatcher(AssemblyPatcherArguments arguments)
    {
        Arguments = arguments;
    }
}