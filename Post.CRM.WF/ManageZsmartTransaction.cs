using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Workflow;
using Post.CRM.WF.HELPER.ZSMART;
using System;
using System.Activities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.CRM.WF
{
    public class ManageZsmartTransaction : CodeActivity
    {
        [RequiredArgument]
        [Input("Zsmart Transaction")]
        [ReferenceTarget("ainos_zsmarttransaction")]
        public InArgument<EntityReference> ZSmartTransactionRef { get; set; }
        
        protected override void Execute(CodeActivityContext context)
        {
            IWorkflowContext workflowContext = context.GetExtension<IWorkflowContext>();
            IOrganizationServiceFactory serviceFactory = context.GetExtension<IOrganizationServiceFactory>();

            // Use the context service to create an instance of IOrganizationService.             
            IOrganizationService service = serviceFactory.CreateOrganizationService(workflowContext.InitiatingUserId);
            //Create the tracing service
            ITracingService tracingService = context.GetExtension<ITracingService>();

            //Use the tracing service
            tracingService.Trace("ManageZsmartTransaction check begin...");

            //get Zsmart Transaction Param
            EntityReference er_zSmartTransaction = ZSmartTransactionRef.Get(context);

            //Create Zsmart Transaction Helper
            ZsmartTransaction_HELPER zth = new ZsmartTransaction_HELPER(service, tracingService, er_zSmartTransaction);

            //Start Process
            zth.Start();
        }
    }
}
