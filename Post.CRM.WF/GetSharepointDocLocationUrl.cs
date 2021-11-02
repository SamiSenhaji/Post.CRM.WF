using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;

namespace Post.CRM.WF
{


    public class GetSharepointDocLocationUrl : CodeActivity
    {

        [RequiredArgument]
        [Input("Opportunity")]
        [ReferenceTarget("opportunity")]
        public InArgument<EntityReference> Opportunity { get; set; }

        [Output("url_")]
        public OutArgument<string> url_ { get; set; }

        protected override void Execute(CodeActivityContext executionContext)
        {
            ITracingService tracer = executionContext.GetExtension<ITracingService>();
            IWorkflowContext context = executionContext.GetExtension<IWorkflowContext>();
            IOrganizationServiceFactory serviceFactory = executionContext.GetExtension<IOrganizationServiceFactory>();
            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);

            try
            {
                Guid id = Opportunity.Get(executionContext).Id;


                string fetchXML = string.Empty;
                EntityCollection locationColl = null;


                //contruct the fetch query
                var fetchData = new
                {
                    locationtype = "0",
                    servicetype = "0",
                    regardingobjectid = id
                };

                fetchXML = $@"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='sharepointdocumentlocation'>
                                <attribute name='name' />
                                <attribute name='regardingobjectid' />
                                <attribute name='parentsiteorlocation' />
                                <attribute name='relativeurl' />
                                <attribute name='absoluteurl' />
                                <filter type='and'>
                                  <condition attribute='regardingobjectid' operator='eq' value='{fetchData.regardingobjectid}'/>
                                </filter>
                              </entity>
                            </fetch>";

                locationColl = service.RetrieveMultiple(new FetchExpression(fetchXML));

                RetrieveAbsoluteAndSiteCollectionUrlRequest retrieveRequest = new RetrieveAbsoluteAndSiteCollectionUrlRequest
                {
                    Target = new EntityReference("sharepointdocumentlocation", locationColl[0].Id)
                };
                RetrieveAbsoluteAndSiteCollectionUrlResponse retriveResponse = (RetrieveAbsoluteAndSiteCollectionUrlResponse)service.Execute(retrieveRequest);
                string url = retriveResponse.AbsoluteUrl.ToString();
                url_.Set(executionContext, url);

            }
            catch (Exception e)
            {
                throw new InvalidPluginExecutionException(e.Message);
            }
        }
    }
}