using SeShell.Test.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeShell.Test.PageObjects
{
    public class DummyPage
    {
        public static string Element()
        {
            return DummyPageResources.ResourceKey;
        }
    }
}
