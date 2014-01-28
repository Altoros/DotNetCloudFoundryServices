#.Net Cloud Founrdy Service

Dot Net Cloud Foundry Service is a Microsoft .Net based service broker for a Cloud Foundry v2.0. It is allow easily develop new services for Cloud Foundry using Microsoft technology stack and get them up and running

#Microsoft SQL Server Service

Dot Net Cloud Foundry Service broker destributed with one default service implementation that provide possibility to use Microsoft SQL Server withing your Cloud Foundry hosted applications.

## Who can benefit from it?

- Allow use Microsoft SQL Server with Cloud Foundry
- Provide easy way to support unsupported databases. You should implement only one interface.

## How to use it

Before you begin you should install `cf` Command Line Interface. Installation instructions are available [here](http://docs.cloudfoundry.com/docs/using/managing-apps/cf/index.html). Alternatively you can use `gcf` client, which is a new modern version of `cf` tool.

1. Installing Dot Net Cloud Foundry Service Broker
 
 * You should install `BrokerServiceSetup.exe` installation package on target machine.
 
 * Configure your network environment to make service be accessible from the Cloud Foundry.
 
 * Update application configuration file with proper settings (connection string to Microsoft SQL server, it's public address and etc.) Default configuration file location is `C:\Program Files (x86)\Altoros\SqlServiceBroker\BrokerWindowsService.exe.config`. 
 
 * Restart service using services console to apply all configuration changes.
 

2. Registering Dot Net Cloud Foundry Service Broker within Cloud Foundry

 * Set the target cloud, choose organization and space `cf target [your cloud foundry api url]`

 * Authenticate with the target `cf login`. You have to use administrative account with proper access rights here.

 * View existing service-brokers `cf service-brokers`

 * Register Dot Net Cloud Foundry Service Broker withing Cloud Foundry `cf add-service-broker TestDotNetServiceBroker --username s --password p --url [your BrokerWindowsService url]`.
 Registering a broker causes cloud controller to fetch and validate the catalog information from the broker, 
 and save the catalog to cloud controller database.

 * View existing service-brokers `cf service-brokers`

3. Making a plan public.
   Before you will be able to use your service you have to register at least one service plan as a public.  

 * Get the service plan guid `cf services --marketplace --trace`

 * Make service plan public running the following command using the service plan guid `cf curl PUT /v2/service_plans/[plan_guid_obtained_on_previous_step] -b '{"public":'true'}'`

 * To verify that the the plan was set to public, re-run this command and check the 'public' field `cf services --marketplace --trace`

4. Making service up and running

 * Create a new service instance, choose a service type and set service name and plan `cf create-service`

 * Display information about service instances in the current space `cf services`

5. Deploying and running application that uses service

 * Change current directory to directory with app for deploying (manifest file should be available)

 * Deploy a new application `cf push`

 * Bind a service to an application `cf bind-service`. Choose deployed application and service during command execution.

 * Restart application `cf restart [deployed app]`

 * Application ready to use your service

6. Removing links 

 * Remove a service binding from an application `cf unbind-service`. Choose deployed application and service during command execution.

 * Delete a service `cf delete-service`. Choose deployed application and service during command execution.



## Roadmap

- Sharded deployment
- Reshaping cluster

## Collaborate

You are welcome to contribute via
[pull request](https://help.github.com/articles/using-pull-requests).


