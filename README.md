# EUCore

## What is EUCore?

General Backend Architecture

-> Net core 3.1
-> MongoDb Driver
-> Dapper

# Getting Started #

1. Install the standard Nuget package into your ASP.NET Core application.

```
Package Manager : Install-Package EUCore
```
    
2. You must define the database connection string in AppSettings.json.

```c#
"DatabaseConfig": {
    "DatabaseType": "dapper",
    "ConnectionString": "CONNECTION_STRING"
}
```

3. Define Entity

```c#
public class UserEntity : EntityBase<int>
{
    public string Username { get; set; }
}
```    

4. Start using

```c#
private IRepository<UserEntity> _userRepository;
public WeatherForecastController(IRepository<UserEntity> userRepository)
{
    _userRepository = userRepository;
}

[HttpGet]
public IEnumerable<UserEntity> Get()
{
    return _userRepository.GetAll();
}
```
    
5.UnitOfWork

```c#
public class UnitOfWorkManager : UnitofWorkManagerBase
{
 
    public UnitOfWorkManager(IComponentContext context,AppSettings appSettings) : base(appSettings,context)
    {
      protected override DbConnection CreateConnection()
      {
          return new SqlConnection();
      }
    }
 
}
```     
   
6.Dependecy

```c#
builder.RegisterModule(new EUCoreModule(appSettings));
builder.RegisterType<UnitOfWorkManager>().As<IUnitOfWorkManager>().As<IRepositoryManager>().InstancePerLifetimeScope();
builder.Register(c => new RuntimeInitializer()
    .DefaultMapper(typeof(DefaultEntityMapper<>))
    .SetDialect<MySqlDialect>()//SqlServerDialect or MySqlDialect
).AutoActivate().AsSelf().SingleInstance();
```
