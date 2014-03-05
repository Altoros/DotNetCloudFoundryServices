using CloudFoundryServiceBroker.Interfaces.Entities;

namespace MsSqlService
{
    class MsSqlPlanEntity : PlanEntity
    {
        /// <summary>
        /// Size of Database in Mb
        /// </summary>
        public int DatabaseSize { get; set; }
        /// <summary>
        /// Number of bindings/users
        /// </summary>
        public int NumberOfBindings { get; set; }
    }
}
