using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SeShell.Test.Enums;

namespace SeShell.Test.Core
{
    public sealed class TestCaseAsserts
    {
        public int AssertionCount;

        private List<string> assertionMessageList;

        public void AddBooleanAssert(Action<bool, string> current, bool flag, string assertionMessage)
        {
            AssertionCount++;
            AddMessage(assertionMessage);
            current.Invoke(flag, assertionMessage);        
        }

        public void AddNullAssert(Action<object, string> current, object anObject, string assertionMessage)
        {
            AssertionCount++;
            AddMessage(assertionMessage);
            current.Invoke(anObject, assertionMessage);
        }

        public string GetAssertionMessage()
        {
            return assertionMessageList[AssertionCount - 1];
        }

        private void AddMessage(string assertionMessage)
        {
            if (assertionMessageList == null)
            {
                assertionMessageList = new List<string>();
            }

            assertionMessageList.Add(assertionMessage);
        }
    }
}
