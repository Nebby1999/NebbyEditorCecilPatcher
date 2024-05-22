using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NebbyEditorCecilPatcher;
public class AssemblyPatcherArguments
{
    public string AssemblyToPatchPath { get; private set; }
    public string AssemblyOutputPath { get; private set; }
    public string DependenciesPath { get; private set; }
    public string PathToPatchesJSON { get; private set; }
    /*
    * 0 = Path of assembly to patch
    * 1 = Output path
    * 2 = Path to directory with required game assemblies
    * 3 = Path of JSON data to patch.
    */
    public static bool TryParse(string[] arguments, out AssemblyPatcherArguments? patcherArguments)
    {
        try
        {
            patcherArguments = new AssemblyPatcherArguments();
            patcherArguments.AssemblyToPatchPath = arguments[0];
            patcherArguments.AssemblyOutputPath = arguments[1];
            patcherArguments.DependenciesPath = arguments[2];
            patcherArguments.PathToPatchesJSON = arguments[3];
            return true;
        }
        catch(Exception e)
        {
            Program.Log(e);
            patcherArguments = null;
        }
        return false;
    }

    private AssemblyPatcherArguments()
    {

    }
}