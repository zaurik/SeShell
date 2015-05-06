using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using NUnit.Framework;
using SeShell.Test.Core;

namespace SeShell.Test.TestCases
{
    [TestFixture]
    class TestSuite
    {
        [Suite]
        public static IEnumerable Suite
        {
            get
            {
                var suite = Configuration.ClassesToExecute;
                return suite;
            }
        }
    }
}