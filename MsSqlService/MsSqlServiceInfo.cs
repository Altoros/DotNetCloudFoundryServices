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
        private readonly string[] _tags;
        private readonly ServiceMetadata _metadata;
        private readonly string[] _requires;
        private readonly List<MsSqlPlanEntity> _sqlPlans;

        public MsSqlServiceInfo()
        {
            _id = new Guid("78A2C443-F6E3-42C1-8004-BB0DC3C01A84");
            _name = "MsSql";
            _description = "MsSql service for Cloud Foundry by Altoros";
            _bindable = true;
            _requires = new string[0];
            _tags = new string[0];
            _metadata = new ServiceMetadata();

            _sqlPlans = new List<MsSqlPlanEntity>
            {
                new MsSqlPlanEntity { Id = new Guid("3D91602A-7F16-4E31-8FBB-89C894119EB2"), Name="Unlim", Description = "Fully unlimited plan", DatabaseSize = -1, NumberOfBindings = -1,
                    Metadata = new PlanMetadata {DisplayName = "Unlimited", Bullets = new []{"Unlimited database size", "Unlimited number of bindings (database users)"}, Costs = new []{new Cost{Unit="Monthly", Amount = new Amount{USD=1000.0f, EUR=800.0f}} }}},
                
                new MsSqlPlanEntity { Id = new Guid("81321EDE-8FC1-446C-A68C-51AFCAEDB903"), Name="OneGb", Description = "Plan that define database size limited with 1 Gb", DatabaseSize = 1024, NumberOfBindings = -1,
                    Metadata = new PlanMetadata {DisplayName = "1 Gb", Bullets = new []{"1 Gb Database size", "Unlimited number of bindings (database users)"}, Costs = new []{new Cost{Unit="Monthly", Amount = new Amount{USD=120.0f, EUR=96.0f}} }}},
                
                new MsSqlPlanEntity { Id = new Guid("A3E92441-EE18-4E18-A4D5-06BE46AA350D"), Name="FourGb", Description = "Plan that define database size limited with 4 Gb",  DatabaseSize = 4096, NumberOfBindings = -1,
                    Metadata = new PlanMetadata {DisplayName = "4 Gb", Bullets = new []{"4 Gb Database size", "Unlimited number of bindings (database users)"}, Costs = new []{new Cost{Unit="Monthly", Amount = new Amount{USD=400.0f, EUR=320.0f}} }}},
                
                new MsSqlPlanEntity { Id = new Guid("B3DF6358-1A0B-4DA4-B572-147DF264098A"), Name="SingleUser", Description = "Plan that define database that can be accessed only by single user",  DatabaseSize = -1, NumberOfBindings = 1,
                    Metadata = new PlanMetadata {DisplayName = "Single user", Bullets = new []{"Unlimited database size", "Single database user"}, Costs = new []{new Cost{Unit="Monthly", Amount = new Amount{USD=250.0f, EUR=200.0f}} }}},
                
                new MsSqlPlanEntity { Id = new Guid("2CA065F8-5B4E-4568-9755-A5977965FC42"), Name="ThreeUsers", Description = "Plan that define database that can be accessed by 3 users",  DatabaseSize = -1, NumberOfBindings = 3,
                    Metadata = new PlanMetadata {DisplayName = "3 users", Bullets = new []{"Unlimited database size", "Up to 3 database users"}, Costs = new []{new Cost{Unit="Monthly", Amount = new Amount{USD=670.0f, EUR=536.0f}} }}},
                
                new MsSqlPlanEntity { Id = new Guid("65A213D9-CE05-48DD-9C0D-CB4F1006EF6E"), Name="SingleUser1Gb", Description = "Plan with 1 Gb database for 1 user",  DatabaseSize = 1024, NumberOfBindings = 1,
                    Metadata = new PlanMetadata {DisplayName = "1 Gb + 1 user", Bullets = new []{"1 Gb Database size", "Single database user"}, Costs = new []{new Cost{Unit="Monthly", Amount = new Amount{USD=800.0f, EUR=640.0f}} }}},
                
                new MsSqlPlanEntity { Id = new Guid("C5B8CF1F-4055-4ACB-B050-B0E9DFD67B33"), Name="ThreeUsers4Gb", Description = "Plan with 4Gb database for 3 users",  DatabaseSize = 4096, NumberOfBindings = 3,
                    Metadata = new PlanMetadata {DisplayName = "4 Gb + 3 users", Bullets = new []{"4 Gb Database size", "Up to 3 database users"}, Costs = new []{new Cost{Unit="Monthly", Amount = new Amount{USD=175.0f, EUR=140.0f}} }}},
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
            get { return SqlPlans; }
        }
        internal IEnumerable<MsSqlPlanEntity> SqlPlans
        {
            get { return _sqlPlans; }
        }
    }
}
