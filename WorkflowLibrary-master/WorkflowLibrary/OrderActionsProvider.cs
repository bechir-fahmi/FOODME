using Newtonsoft.Json;
using OptimaJet.Workflow.Core.Model;
using OptimaJet.Workflow.Core.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WorkflowLibrary
{
    internal class OrderActionsProvider : IWorkflowActionProvider
    {
        #region Attributes
        private readonly Dictionary<string, Action<ProcessInstance, WorkflowRuntime, string>> _actions = new Dictionary<string, Action<ProcessInstance, WorkflowRuntime, string>>() ;
        private readonly Dictionary<string, Func<ProcessInstance, WorkflowRuntime, string, bool>> _conditions=new Dictionary<string, Func<ProcessInstance, WorkflowRuntime, string, bool>>();

        private readonly Dictionary<string, Func<ProcessInstance, WorkflowRuntime, string, CancellationToken, Task>> _asyncActions = new Dictionary<string, Func<ProcessInstance, WorkflowRuntime, string, CancellationToken, Task>>();
        private readonly Dictionary<string, Func<ProcessInstance, WorkflowRuntime, string, CancellationToken, Task<bool>>> _asyncConditions = new Dictionary<string, Func<ProcessInstance, WorkflowRuntime, string, CancellationToken, Task<bool>>>();
        #endregion

        #region Constructor
        public OrderActionsProvider()
        {
            _actions.Add("PriceAction", OrderAction);
            _conditions.Add("PriceCondition", OrderCondition);
        }
        #endregion
        #region Methods
        //Action
        private void OrderAction(ProcessInstance processInstance, WorkflowRuntime runtime, string actionParameter)
        {
            // Execute your synchronous code here
        }
        //Condition
        private bool OrderCondition(ProcessInstance processInstance, WorkflowRuntime runtime, string actionParameter)
        {
            Order person = JsonConvert.DeserializeObject<Order>(actionParameter);
            return false;
        }
        #region Implementation of IWorkflowActionProvider
        public void ExecuteAction(string name, ProcessInstance processInstance, WorkflowRuntime runtime, string actionParameter)
        {
            throw new NotImplementedException();
        }
        public bool ExecuteCondition(string conditionName, ProcessInstance processInstance, WorkflowRuntime runtime, string actionParameter)
        {
            if (_conditions.ContainsKey(conditionName))
            {
                return _conditions[conditionName].Invoke(processInstance, runtime, actionParameter);
            }

            throw new NotImplementedException($"Condition with name {conditionName} isn't implemented");
        }
        public List<string> GetActions(string schemeCode, NamesSearchType namesSearchType)
        {
            return _actions.Keys.Union(_actions.Keys).ToList();
        }
        public List<string> GetConditions(string schemeCode, NamesSearchType namesSearchType)
        {
            return _conditions.Keys.Union(_conditions.Keys).ToList();
        }
        #endregion
       
        #endregion

        #region Async Methods
        public Task ExecuteActionAsync(string name, ProcessInstance processInstance, WorkflowRuntime runtime, string actionParameter, CancellationToken token)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> ExecuteConditionAsync(string name, ProcessInstance processInstance, WorkflowRuntime runtime, string actionParameter, CancellationToken token)
        {
            if (_asyncConditions.ContainsKey(name))
            {
                return await _asyncConditions[name].Invoke(processInstance, runtime, actionParameter,token);
            }

            throw new NotImplementedException($"Async Condition with name {name} isn't implemented");
        }         

        public bool IsActionAsync(string name, string schemeCode)
        {
            return _asyncActions.ContainsKey(name);
        }

        public bool IsConditionAsync(string name, string schemeCode)
        {
            return _asyncConditions.ContainsKey(name);
        }
        #endregion
    }
}
