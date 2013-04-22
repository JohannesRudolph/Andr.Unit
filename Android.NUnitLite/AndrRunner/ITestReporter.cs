using System;
using NUnit.Framework.Api;
using NUnit.Framework.Internal;

namespace Android.NUnitLite
{
	public interface ITestReporter : ITestListener
	{
		void TestSuiteStarted (TestSuite ts);
		void TestSuiteFinished (TestSuite ts);
	}
}

