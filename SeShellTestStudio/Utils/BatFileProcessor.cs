using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;

namespace SeShellTestStudio.Utils
{
    public sealed class BatFileProcessor
    {
        public static bool TestsBeingExecuted()
        {
            return System.Diagnostics.Process.GetProcesses().Any(p => p.ProcessName == "nunit-console" || p.ProcessName == "SeShell.Test.XMLTestResult");
        }
    }
}