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

    ```
    "DatabaseConfig": {
        "DatabaseType": "dapper",
        "ConnectionString": "CONNECTION_STRING"
    }
    ```
3. Define Entity
    ```
    public class UserEntity : **EntityBase<int>**
    {
        public string Username { get; set; }
    }
    ```    

4. Start using
    ```
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
