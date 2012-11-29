﻿using System;
using NUnit.Framework.Api;

namespace NUnit.Framework.Internal
{
    /// <summary>
    /// The FailureSite enum indicates the stage of a test
    /// in which an error or failure occured.
    /// </summary>
    public enum FailureSite
    {
        /// <summary>
        /// Failure in the test itself
        /// </summary>
        Test,

        /// <summary>
        /// Failure in the SetUp method
        /// </summary>
        SetUp,

        /// <summary>
        /// Failure in the TearDown method
        /// </summary>
        TearDown,

        /// <summary>
        /// Failure of a parent test
        /// </summary>
        Parent,

        /// <summary>
        /// Failure of a child test
        /// </summary>
        Child
    }

    /// <summary>
    /// Represents the result of running a test suite
    /// </summary>
    public class TestSuiteResult : TestResult
    {
        private int passCount = 0;
        private int failCount = 0;
        private int skipCount = 0;
        private int inconclusiveCount = 0;

        /// <summary>
        /// Construct a TestSuiteResult base on a TestSuite
        /// </summary>
        /// <param name="suite">The TestSuite to which the result applies</param>
        public TestSuiteResult(TestSuite suite) : base(suite) { }

        /// <summary>
        /// Gets the number of test cases that failed
        /// when running the test and all its children.
        /// </summary>
        public override int FailCount
        {
            get { return this.failCount; }
        }

        /// <summary>
        /// Gets the number of test cases that passed
        /// when running the test and all its children.
        /// </summary>
        public override int PassCount
        {
            get { return this.passCount; }
        }

        /// <summary>
        /// Gets the number of test cases that were skipped
        /// when running the test and all its children.
        /// </summary>
        public override int SkipCount
        {
            get { return this.skipCount; }
        }

        /// <summary>
        /// Gets the number of test cases that were inconclusive
        /// when running the test and all its children.
        /// </summary>
        public override int InconclusiveCount
        {
            get { return this.inconclusiveCount; }
        }

        /// <summary>
        /// Add a child result
        /// </summary>
        /// <param name="result">The child result to be added</param>
        public override void AddResult(TestResult result)
        {
            base.AddResult(result);

            this.passCount += result.PassCount;
            this.failCount += result.FailCount;
            this.skipCount += result.SkipCount;
            this.inconclusiveCount += result.InconclusiveCount;
        }

        /// <summary>
        /// Set the test result based on the type of exception thrown
        /// and the site of the Failure.
        /// </summary>
        /// <param name="ex">The exception that was thrown</param>
        /// <param name="site">The FailureSite</param>
        public override void RecordException(Exception ex, FailureSite site)
        {
            RecordException(ex);

            if (site == FailureSite.SetUp)
            {
                switch (ResultState.Status)
                {
                    case TestStatus.Skipped:
                        this.skipCount = this.test.TestCaseCount;
                        break;

                    case TestStatus.Failed:
                        this.failCount = this.test.TestCaseCount;
                        break;
                }
            }
        }
    }
}
