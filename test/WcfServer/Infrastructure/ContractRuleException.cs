using System;

namespace WcfServer
{
    public class ContractRuleException : Exception
    {
        public ContractRuleException(string message)
            :base(message)
        {

        }
    }
}