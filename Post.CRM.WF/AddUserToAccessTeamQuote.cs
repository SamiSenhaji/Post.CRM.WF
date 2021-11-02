
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;

namespace Post.CRM.WF
{
   public  class AddUserToAccessTeamQuote : CodeActivity
    {
        [RequiredArgument]
        [Input("Quote")]
        [ReferenceTarget("quote")]
        public InArgument<EntityReference> Quote { get; set; }

        [RequiredArgument]
        [Input("teamTemplateId")]
        public InArgument<string> teamTemplateId { get; set; }

        [RequiredArgument]
        [Input("SecondAccountManager")]
        [ReferenceTarget("systemuser")]
        public InArgument<EntityReference> SecondAccountManager { get; set; }

        protected override void Execute(CodeActivityContext executionContext)
        {
            ITracingService tracer = executionContext.GetExtension<ITracingService>();
            IWorkflowContext context = executionContext.GetExtension<IWorkflowContext>();
            IOrganizationServiceFactory serviceFactory = executionContext.GetExtension<IOrganizationServiceFactory>();
            IOrganizationService service = serviceFactory.CreateOrganizationService(context.InitiatingUserId);

            tracer.Trace(SecondAccountManager.Get(executionContext).Id.ToString());
            tracer.Trace(Quote.Get(executionContext).Id.ToString());
            tracer.Trace(teamTemplateId.Get<string>(executionContext).ToString());
            Guid UserId_ = SecondAccountManager.Get(executionContext).Id;
            tracer.Trace("UserId_ : " + SecondAccountManager.Get(executionContext).Id);
            Guid TeamTemplateId_ = new Guid(teamTemplateId.Get<string>(executionContext));
            try
            {
                AddUserToRecordTeamRequest adduser = new AddUserToRecordTeamRequest();
                adduser.SystemUserId = UserId_;
                adduser.TeamTemplateId = TeamTemplateId_;
                adduser.Record = Quote.Get(executionContext);
                service.Execute(adduser);
            }
            catch (Exception e)
            {
                throw new InvalidPluginExecutionException(e.Message);
            }
        }
    }
}
