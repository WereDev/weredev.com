# weredev.com
Code for my main website.  You're viewing it right now.

## Technology
This is a pretty straight forward build using .Net Core 3.x.  The code is broken down into 3 basic tiers: UI, Domain (logic), and Repo/Providers.  I'm also using StyleCop for general code analysis and keeping stuff tidy.

### UI
The UI is built using ASP.Net with the MVC pattern.  The built in pipeline and dependency injection framework is used with navagation based on controller and action attributes, as opposed to Razor pages.

JQuery is used for basic JavaScript support.  The photo album usees a slightly modified version of Swipebox forked from [github.com/brutaldesign/swipebox](https://github.com/brutaldesign/swipebox).  Styling is done using SASS compiled using Node.js.

### Domain
This layer has basic logic and mapping, mostly just the mapping.  Caching is also implemented at this later to cache responses from the Repo/Provider layer.

### Repo / Provider
Both repos use HttpClients. The two biggest parts of this are pulling data from Flickr and from GitHub.  The Flickr provider was interesting as the way the Flickr organises photos is different from how I wanted to display them on this site.

## Deployment
I am tinkering with Azure Deveops Pipelines for deployments.  This is triggered automatically my checking in to the master branch.

## Dependencies
[StyleCop](https://github.com/StyleCop/StyleCop)
[Swipebox](https://github.com/WereDev/swipebox)
