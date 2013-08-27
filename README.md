#An Inbound Demonstration Application


This solution illustrates how to create an API endpoint to receive Inbound Hooks with the [Postmark](http://www.nuget.org/packages/Postmark/) library and ASP.NET MVC4. It is setup to receive an http JSON post from the Postmark application for [Inbound parsing](http://developer.postmarkapp.com/developer-inbound-parse.html) scenarios.

## Building from Scratch ##
1. Create a new ASP.NET MVC4 or WebApi application.
2. Add the Postmark library via Nuget
3. Add an API endpoint described in the Controller and API routing setup in the demonstration solution.

This solution sets up and endpoint at **http://yourappdomain/api/Inbound/Receive**

