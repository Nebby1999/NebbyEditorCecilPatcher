using Mono.Cecil;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

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
            //Parse the arguments
            if (!AssemblyPatcherArguments.TryParse(args, out var patcherArguments))
            {
                Log("AssemblyPatcherArguments parsing has failed...");
                return;
            }

            //Initialize a new patcher using said arguments.
            var patcher = new AssemblyPatcher(patcherArguments!);

            if(!patcher.Patch())
            {
                Log("The AssemblyPatcher has thrown an exception, nothing will be written in the output.");
                return;
            }

            Log("Finished!");
        }
    }
}