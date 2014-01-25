using System.Configuration;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using CloudFoundryServiceBroker.Interfaces;

namespace CloudFoundryServiceBroker
{
    /// <summary>
    /// Custom credentials manager that delegates user validation to custom implementation provided by service plugin
    /// </summary>
    public class CustomCredentialsManager : UserNamePasswordValidator
    {
        /// <summary>
        /// Perform username and password validation
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        public override void Validate(string userName, string password)
        {
            var module = ModuleManager.GetModule<ICredentialsValidator>();
            if (module != null)
            {
                var isValid = module.Validate(userName, password);
                if (!isValid)
                {
                    throw new SecurityTokenValidationException("Invalid credentials!");
                }
                return;
            }

            //IWebOperationContext context = new WebOperationContextWrapper(WebOperationContext.Current); // Get the context
            //var context = WebOperationContext.Current;
            //var req = context.IncomingRequest;
            throw new ConfigurationErrorsException("ICredentialsValidation implementation is not found");
        }
    }
}
