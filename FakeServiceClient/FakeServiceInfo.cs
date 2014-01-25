using CloudFoundryServiceBroker.Interfaces;
using CloudFoundryServiceBroker.Interfaces.Entities;
using FakeServiceClient.Properties;
using System;
using System.Collections.Generic;

namespace FakeServiceClient
{
    public class FakeServiceInfo : ICloudFoundryServiceInfo
    {
        private readonly Guid _id;
        private readonly string _name;
        private readonly string _description;
        private readonly bool _bindable;
        private readonly string[] _tags;
        private readonly ServiceMetadata _metadata;
        private readonly string[] _requires;
        private readonly List<PlanEntity> _plans;

        public FakeServiceInfo() : 
            this(new List<string>(), 
            new List<string>(),
            new ServiceMetadata{DisplayName = "FakeService",ImageUrl = "https://d33na3ni6eqf5j.cloudfront.net/app_resources/18492/thumbs_112/img9069612145282015279.png",
            LongDescription = "Empty fake implementation of service used for test purpose",ProviderDisplayName = "ALTOROS",
            DocumentationUrl = "http://docs.cloudfoundry.com/docs/dotcom/marketplace/services/cloudamqp.html", SupportUrl = "http://www.cloudamqp.com/support.html"})
        {
            
        }
        public FakeServiceInfo(List<string> tags, List<string> requires, ServiceMetadata metadata)
        {
            _tags = (tags??new List<string>()).ToArray();
            _requires = (requires??new List<string>()).ToArray();
            //_metadata = null;
            _metadata = Settings.Default.UseMetadata?metadata:null; //There should be used object that can be serialized at proper JSON (expected by CF)
            _id = Settings.Default.FakeServiceUid;
            if(_id == Guid.Empty) _id = new Guid("6779BB74-360F-4BD2-83B6-CCA39E00687F");

            _name = "Fake service";
            _description = "Fake service description";
            _bindable = true;
            var planId = Settings.Default.FakePlanUid;
            if(planId == Guid.Empty) planId = new Guid("39ABF3C3-27F5-4CA8-854B-6DDAD64E5F09");
            _plans = new List<PlanEntity>
            {
                new PlanEntity { Description = "Plan description", Id = planId, Name = "Plan name", 
                    Metadata = Settings.Default.UseMetadata ? new PlanMetadata {Bullets = new []{"20 GB of messages","20 connections"},
                        Costs = new [] { new Cost{Amount = new Amount() { USD = 99.0f, EUR = 49.0f}, Unit = "MONTHLY"}, new Cost{Amount = new Amount{USD = 70.5f, EUR=41.3f, RUR = 2100f}, Unit = "1GB of messages over 20GB"}}} : null }
            };
        }

        public Guid Id
        {
            get { return _id; }
        }

        public string Name
        {
            get { return _name; }
        }

        public string Description
        {
            get { return _description; }
        }

        public bool Bindable
        {
            get { return _bindable; }
        }

        public string[] Tags
        {
            get { return _tags; }
        }

        public ServiceMetadata Metadata
        {
            get { return _metadata; }
        }

        public string[] Requires
        {
            get { return _requires; }
        }

        public IEnumerable<PlanEntity> Plans
        {
            get { return _plans; }
        }
    }
}
