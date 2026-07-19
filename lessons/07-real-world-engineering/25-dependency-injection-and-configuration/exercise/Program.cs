// TODO 1: Reuse IMessageSender/EmailSender/NotificationService from
// Lesson 10. Register EmailSender as the IMessageSender implementation
// and NotificationService, both as Singleton, using Host.CreateApplicationBuilder.
// Resolve NotificationService from the built host and call Notify().

// TODO 2: Add a NotificationOptions class with a DefaultSender string
// property. Bind it from an in-memory configuration dictionary (you can
// use builder.Configuration.AddInMemoryCollection(...) instead of a
// real appsettings.json file for this exercise) with key
// "Notification:DefaultSender". Inject IOptions<NotificationOptions>
// into NotificationService and print _options.DefaultSender from within
// Notify().

// TODO 3 (bug hunt): explain in a comment what goes wrong if a service
// holding PER-USER mutable state (e.g. `CurrentUserId`) is registered
// as Singleton instead of Scoped/Transient in a multi-user app, then
// fix the registration.
