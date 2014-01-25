using CloudFoundryServiceBroker.Interfaces;
using CloudFoundryServiceBroker.Interfaces.Entities;
using System;
using System.Collections.Generic;

namespace MsSqlService
{
    public class MsSqlServiceInfo : ICloudFoundryServiceInfo
    {
        private readonly Guid _id;
        private readonly string _name;
        private readonly string _description;
        private readonly bool _bindable;
        private string[] _tags;
        private ServiceMetadata _metadata;
        private string[] _requires;
        private List<PlanEntity> _plans;

        public MsSqlServiceInfo()
        {
            _id = new Guid("78A2C443-F6E3-42C1-8004-BB0DC3C01A84");
            _name = "MsSql";
            _description = "MsSql service for Cloud Foundry by Altoros";
            _bindable = true;
            _plans = new List<PlanEntity> { new PlanEntity { Description = "Ms SQL Plan description", Id = new Guid("3D91602A-7F16-4E31-8FBB-89C894119EB2"), Name = "MSSQLPLAN" } };
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
