using Newtonsoft.Json;
using OptimaJet.Workflow.Core.Model;
using OptimaJet.Workflow.Core.Runtime;
using Platform.ReferencialData.Business.business_services.RestaurantData;
using Platform.ReferentialData.BusinessModel.RestaurantData;
using Platform.ReferentialData.DtoModel.RestaurantData;
using Platform.Shared.Enum;

namespace Workflow
{
    public class OrderActionsProvider : IWorkflowActionProvider
    {
        #region Attributes
        private readonly Dictionary<string, Action<ProcessInstance, WorkflowRuntime, string>> _actions = new Dictionary<string, Action<ProcessInstance, WorkflowRuntime, string>>();
        private readonly Dictionary<string, Func<ProcessInstance, WorkflowRuntime, string, bool>> _conditions = new Dictionary<string, Func<ProcessInstance, WorkflowRuntime, string, bool>>();

        private readonly Dictionary<string, Func<ProcessInstance, WorkflowRuntime, string, CancellationToken, Task>> _asyncActions = new Dictionary<string, Func<ProcessInstance, WorkflowRuntime, string, CancellationToken, Task>>();
        private readonly Dictionary<string, Func<ProcessInstance, WorkflowRuntime, string, CancellationToken, Task<bool>>> _asyncConditions = new Dictionary<string, Func<ProcessInstance, WorkflowRuntime, string, CancellationToken, Task<bool>>>();
        #endregion
        private readonly IRestaurantService _restaurantService;
        #region Constructor
        public OrderActionsProvider(IRestaurantService restaurantService)
        {
            _actions.Add("PriceAction", OrderAction);
            _conditions.Add("PriceCondition", OrderCondition);
            _conditions.Add("RestaurantStatus", VerifierRestaurantStatusCondition);
            _restaurantService = restaurantService;
        }
        #endregion
        #region Methods

        #region Action
        //Action
        
        private bool VerifierRestaurantStatusCondition(ProcessInstance processInstance, WorkflowRuntime runtime, string actionParameter)
        {
            Order order = JsonConvert.DeserializeObject<Order>(actionParameter);
            if (order != null)
            {
                RestaurantDto restaurant = _restaurantService.Get(order.RestaurantId);
                return restaurant.IsOpen;
            }
            return false;
        }
        private void OrderAction(ProcessInstance processInstance, WorkflowRuntime runtime, string actionParameter)
        {
            // Execute your synchronous code here
            if (OrderCondition(processInstance, runtime, actionParameter))
            {
                //Action Payer
            }
        }
        #endregion
        //Condition
        private bool OrderCondition(ProcessInstance processInstance, WorkflowRuntime runtime, string actionParameter)
        {
            Order order = JsonConvert.DeserializeObject<Order>(actionParameter);
            if (order.PaymentMode == PaymentMode.Cash && order.Total > 200)
                return false;
            return true;
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
                return await _asyncConditions[name].Invoke(processInstance, runtime, actionParameter, token);
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