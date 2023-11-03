using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OptimaJet.Workflow.Core.Model;
using OptimaJet.Workflow.Core.Runtime;
using Workflow;

namespace AFCO_Process.Pages
{
    public class IndexModel : PageModel
    {
        #region attribute
        public readonly Guid _instanceId = new Guid();
        private const string SchemeCode = "orderProcess11";
        #endregion

        #region Properties
        public Guid InstanceId { get { return _instanceId; } }
        #endregion

        #region Constructor    
        public IndexModel()
        {
            _instanceId = Guid.NewGuid();
            WorkflowInit.Runtime.CreateInstance(SchemeCode, _instanceId);
          
        }

        #endregion

        #region Methods
        public void OnGet()
        {
      
        }
        public void OnPostHandleCommand(String command) {
            //var test = WorkflowInit.Runtime.SwitchAutoUpdateSchemeBeforeGetAvailableCommandsOff();
              var res = WorkflowInit.Runtime.GetCurrentActivityName(_instanceId);
            var _availableCommands =  WorkflowInit.Runtime.GetAvailableCommands(_instanceId, String.Empty);
            var theCommand = _availableCommands.Where(c=>c.CommandName== command).FirstOrDefault();

          
            if (theCommand!=null)
            {
                WorkflowInit.Runtime.ExecuteCommand(theCommand,string.Empty,string.Empty);
            }
        


        }
        #endregion
    }
}