using Camunda.Api.Client;
using Camunda.Api.Client.ProcessDefinition;
using Server.Kafka.Message;

namespace Platform.ReferencialData.Business.Bpmn
{
    public class BpmnService
    {
        private readonly CamundaClient camunda;

        public BpmnService(string camundaRestApiUri)
        {
            this.camunda = CamundaClient.Create(camundaRestApiUri);
        }

        public async Task<string> StartProcessFor(ReferentialDataMessage message)
        {
            var businessKey = Guid.NewGuid().ToString();
            var processParams = new StartProcessInstance()
                .SetVariable("Message", VariableValue.FromObject(message));

            processParams.BusinessKey = businessKey;

            var processStartResult = await
                camunda.ProcessDefinitions.ByKey("Process_Checkout_Management").StartProcessInstance(processParams);
            return processStartResult.Id;
        }
    }
}
