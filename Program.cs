using Mono.Cecil;
using System.Diagnostics;

namespace NebbyEditorCecilPatcher
{
    internal class Program
    {
        internal static void Log(Exception e) => Log(e.ToString());
        internal static void Log(string message)
        {
            Console.WriteLine(message);
        }

        private static void Main(string[] args)
        {
            if (!AssemblyPatcherArguments.TryParse(args, out var patcherArguments))
            {
                Log("AssemblyPatcherArguments parsing has failed...");
                return;
            }
        }
        /*static void Main(string[] args)
        {
            ProgramArguments arguments = new ProgramArguments(args);

            var resolver = new DefaultAssemblyResolver();

            resolver.AddSearchDirectory(arguments.pathToRequiredAssembliesDirectory);

            var readerParams = new ReaderParameters()
            {
                AssemblyResolver = resolver,
            };

            if(File.Exists(arguments.pathOfAssemblyToPatch))
            {
                resolver.AddSearchDirectory(Path.GetDirectoryName(arguments.pathOfAssemblyToPatch));

                string fileOutputPath = arguments.outputPath;
                if(!PatchAssembly(arguments.pathOfAssemblyToPatch, fileOutputPath, arguments.patchesArray, readerParams))
                {
                    //Idk something i guess.
                }
            }
        }

        private static bool PatchAssembly(string assemblyPath, string outputPath, string[] patches, ReaderParameters readerParams)
        {
            try
            {
                using var memoryStream = new MemoryStream(File.ReadAllBytes(assemblyPath));
                using var assemblyDefinition = AssemblyDefinition.ReadAssembly(memoryStream, readerParams);

                AssemblyPatcher patcher = new AssemblyPatcher(assemblyPath, outputPath, patches, assemblyDefinition);
                patcher.Patch();

                using var tempStream = new MemoryStream();

                assemblyDefinition.Write(tempStream);

            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return true;

        }


        private class ProgramArguments
        {
            public string pathOfAssemblyToPatch;
            public string outputPath;
            public string pathToRequiredAssembliesDirectory;
            public string[] patchesArray;
            
            public ProgramArguments(string[] arguments)
            {
                pathOfAssemblyToPatch = arguments[0];
                outputPath = arguments[1];
                pathToRequiredAssembliesDirectory = arguments[2];

                //Obtain the total length of the arguments.
                var argumentsLength = arguments.Length;
                //Reduce by 3, to calculate size of the patches array
                argumentsLength -= 3;
                patchesArray = new string[argumentsLength];

                for(int i = 3; i < argumentsLength + 1; i++)
                {
                    patchesArray[i] = arguments[i];
                }
            }

            public ProgramArguments(string pathOfAssemblyToPatch, string outputPath, string pathToRequiredAssembliesDirectoru, params string[] patchesArray)
            {
                this.pathOfAssemblyToPatch = pathOfAssemblyToPatch;
                this.outputPath = outputPath;
                this.pathToRequiredAssembliesDirectory = pathToRequiredAssembliesDirectoru;
                this.patchesArray = patchesArray;
            }
        }*/
    }
}