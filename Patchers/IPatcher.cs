﻿using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NebbyEditorCecilPatcher.Patchers;

public interface IPatcher
{
    void DoPatch(AssemblyDefinition assemblyDefinition);
}
