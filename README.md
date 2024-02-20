# Dynacoin
Crypto portfolio dashboard

To run the solution the following two projects need to be started:

1. Dynacoin.Web.API - server side API
2. dynacoin.client - React JS Web UI

By default the Web UI is available at https://localhost:5173/

# Architecture

 - Dynacoin.Domain - the core of the solution - contains domain models and service interface definitions 
(actual implementations should be outside the core)

 - Dynacoin.Services - actual implementations of the core service interfaces

 - Dynacoin.Web.API - Web API project

 - Dynacoin.Coinlore.Sdk - SDK to encapsulate api calls to the coinlore service

 - Dynacoin.Services.UnitTests - unit tests of the business logic in the Coinlore services

 - dynacoin.client - React JS WEB UI
     - after a portfolio upload the data would be refreshed automatically every 5 minutes, 
       you can adjust that setting in 'config.refreshIntervalSeconds'