using System;
namespace cat_a_logB.Data
{
    public class UnexpectedDataScenarioException : Exception
    {
        public UnexpectedDataScenarioException() { }

        public UnexpectedDataScenarioException(string message) : base(message) { }

        public UnexpectedDataScenarioException(string message, Exception innerException) : base(message, innerException) { }
    }
}