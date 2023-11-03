using OptimaJet.Workflow.Core.Model;
using OptimaJet.Workflow.Core.Runtime;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WorkflowLibrary
{
    internal class OrderRules : IWorkflowRuleProvider
    {
        #region Class RuleFunction
        private class RuleFunction
        {
            #region Properties
            public Func<ProcessInstance, WorkflowRuntime, string, IEnumerable<string>> GetFunction { get; set; }

            public Func<ProcessInstance, WorkflowRuntime, string, Order, bool> CheckFunction { get; set; }
            #endregion
        }
        #endregion

        #region Properties
        private readonly Dictionary<string, RuleFunction> _rules = new Dictionary<string, RuleFunction>();
        #endregion

        #region Constructor
        public OrderRules()
        {
            _rules.Add("OrderPriceRule", new RuleFunction { CheckFunction = CheckOrderPriceRule, GetFunction = GetOrderPriceRule });
            _rules.Add("OrderDateRule", new RuleFunction { CheckFunction = CheckOrderDateRule, GetFunction = GetOrderDateRule });
        }
        #endregion
        #region Methods
        public IEnumerable<string> GetOrderPriceRule(ProcessInstance processInstance, WorkflowRuntime runtime, string parameter)
        {
            return new List<string>();
        }

        public bool CheckOrderPriceRule(ProcessInstance processInstance, WorkflowRuntime runtime, string identityId, Order order)
        {
            if (order.Price > 10) return true;
            else return false;
        }
        public IEnumerable<string> GetOrderDateRule(ProcessInstance processInstance, WorkflowRuntime runtime, string parameter)
        {
            return new List<string>();
        }

        public bool CheckOrderDateRule(ProcessInstance processInstance, WorkflowRuntime runtime, string identityId, Order order)
        {
            if (order.Date <= DateTime.Now) return true;
            else return false;
        }

        public bool Check(ProcessInstance processInstance, WorkflowRuntime runtime, string identityId, string ruleName, string order)
        {
            //get my orderById
            Order _order = new Order();
            if (_rules.ContainsKey(ruleName))
                return _rules[ruleName].CheckFunction(processInstance, runtime, identityId, _order);
            else return false;
        }

        public IEnumerable<string> GetIdentities(ProcessInstance processInstance, WorkflowRuntime runtime, string ruleName, string parameter)
        {
            if (_rules.ContainsKey(ruleName))
                return _rules[ruleName].GetFunction(processInstance, runtime, parameter);
            throw new NotImplementedException();
        }
        public List<string> GetRules(string schemeCode, NamesSearchType namesSearchType)
        {
            return _rules.Keys.ToList();
        }

        #endregion

        #region Method Async
        public Task<bool> CheckAsync(ProcessInstance processInstance, WorkflowRuntime runtime, string identityId, string ruleName, string parameter, CancellationToken token)
        {
            throw new NotImplementedException();
        }
               
        public Task<IEnumerable<string>> GetIdentitiesAsync(ProcessInstance processInstance, WorkflowRuntime runtime, string ruleName, string parameter, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public bool IsCheckAsync(string ruleName, string schemeCode)
        {
            throw new NotImplementedException();
        }

        public bool IsGetIdentitiesAsync(string ruleName, string schemeCode)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
