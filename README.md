# f1 simulator

## Running the program

### Database 
_Currently a manual setup process, with views to automate the setup in the future_ 
- Connect to your local datasource (e.g. if using Rider, follow the instructions here https://www.jetbrains.com/help/rider/MySQL.html#connect-to-mysql-database)
- Create a database: `CREATE DATABASE f1_simulator;`

### Secrets/keys 
- Configure your api key wthin secrets.json 
- this must be within the Controller -> https://blog.jetbrains.com/dotnet/2023/01/17/securing-sensitive-information-with-net-user-secrets/ 
- 