using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Workflow;
using Post.CRM.WF.HELPER.SAP;
using System;
using System.Activities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.CRM.WF
{
    public class ManageSAPTransaction : CodeActivity
    {
        [RequiredArgument]
        [Input("SAP Transaction")]
        [ReferenceTarget("ainos_saptransaction")]
        public InArgument<EntityReference> SAPTransactionRef { get; set; }
        
        protected override void Execute(CodeActivityContext context)
        {
            IWorkflowContext workflowContext = context.GetExtension<IWorkflowContext>();
            IOrganizationServiceFactory serviceFactory = context.GetExtension<IOrganizationServiceFactory>();

            // Use the context service to create an instance of IOrganizationService.             
            IOrganizationService service = serviceFactory.CreateOrganizationService(workflowContext.InitiatingUserId);
            //Create the tracing service
            ITracingService tracingService = context.GetExtension<ITracingService>();

            //Use the tracing service
            tracingService.Trace("ManageSAPTransaction check begin...");

            //get SAP Transaction Param
            EntityReference er_SapTransaction = SAPTransactionRef.Get(context);

            //Create SAP Transaction Helper
            SAPTransaction_HELPER sap_th = new SAPTransaction_HELPER(service, tracingService, er_SapTransaction);

            //Start Process
            sap_th.Start(tracingService);
        }
    }
}
