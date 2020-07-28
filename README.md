# A humble attempt at making an api using CQRS ES

An application for managing shopping cart with some contrived product operations to understand the apparent flow of CQRS and event sourcing. 

The primary components of the architecture:
- Domain (These are made as part of the aggregate)
	- Product
	- Cart
- Event store	
	> The primary source of truth for all domain values (i.e objects and their props). A simple sql table has been used as event store.


- Commands
	> The initiator of any data modification operations (i.e create/update/delete :p)


- Events
	> The log of operations done by commands. These events hold the data that was either created or modified and kept as a source of record for rehydration and a boatload of other possible interesting operations. 


- Queries
	> Initiator for reads from the read model

Instead of using Kafka, Azure event hub or any other form of production grade event bus this project uses a very simplified raw event handling using MediatR.

### Project startup:
The project runs on dotnet core 3.1. To start the project after cloning the code:
- Run the script in SQL server
- Change **AppDbString** in appsettings.json

The following links helped tremendously into making this project:
1. [https://www.exceptionnotfound.net/implementing-cqrs-in-net-part-1-architecting-the-application/](https://www.exceptionnotfound.net/implementing-cqrs-in-net-part-1-architecting-the-application/) [The whole series]
2. [https://www.davideguida.com/event-sourcing-in-net-core-part-1-a-gentle-introduction/](https://www.davideguida.com/event-sourcing-in-net-core-part-1-a-gentle-introduction/)
3. [https://danielwhittaker.me/2020/02/20/cqrs-step-step-guide-flow-typical-application/](https://danielwhittaker.me/2020/02/20/cqrs-step-step-guide-flow-typical-application/)
4. [https://github.com/gregoryyoung/m-r](https://github.com/gregoryyoung/m-r)
5. And a hell of a lot of google, SO and *Greg young* talks
