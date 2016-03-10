using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionsBase.FunctionalUI.Tests
{
    using System.Threading;

    static class DemoHelper
    {
        // Slow down browser automation so can see it in recorded Pluralsight video 
        public static void Wait(int ms = 2000)
        {
            Thread.Sleep(ms);
        }
    }
}
