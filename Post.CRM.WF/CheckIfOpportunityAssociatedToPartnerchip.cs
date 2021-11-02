using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;

namespace Post.CRM.WF
{
    public class CheckIfOpportunityAssociatedToPartnerchip : CodeActivity
    {
 


        [Output("Result")]
        public OutArgument<bool> Result { get; set; }
        


        protected override void Execute(CodeActivityContext executionContext)
        {
            ITracingService tracer = executionContext.GetExtension<ITracingService>();
            IWorkflowContext context = executionContext.GetExtension<IWorkflowContext>();
            IOrganizationServiceFactory serviceFactory = executionContext.GetExtension<IOrganizationServiceFactory>();
            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);

            try
            {
                Entity target = (Entity)context.InputParameters["Target"];
                string fetchXML = $@"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='ainos_partnership'>
                                <attribute name='ainos_opportunity' />
                                <filter type='and'>
                                  <condition attribute='ainos_opportunity' operator='eq' value='{target.Id}'/>
                                </filter>
                              </entity>
                            </fetch>" ;

              EntityCollection  relations =  service.RetrieveMultiple(new FetchExpression(fetchXML));
                if (relations.Entities.Count > 0)
                {
                    this.Result.Set(executionContext, true);
                }
                else
                {
                    this.Result.Set(executionContext, false);
                   // throw new Exception("You need to Add Partnership to continue");
                }


            }
            catch (Exception e)
            {
                throw new InvalidPluginExecutionException(e.Message);
            }
        }
    }
}