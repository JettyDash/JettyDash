![image](https://github.com/rsemihkoca/JettyDash/assets/82779464/bd9bd947-4758-4584-9cb7-7e296536b223)



# Setup

Here .env file content so you can build your own development environment. Make sure .env file and docker compose file is in same directory.

## App Configuration

```plaintext
ASPNETCORE_ENVIRONMENT=Development
ASPNETCORE_URLS=http://*:5000
CONNECTIONSTRINGS__DEFAULTCONNECTION=Host=172.17.0.1;Port=5432;Username=devUser;Password=devPassword;Database=jettydash;
```

## Database Configuration

```plaintext
DATABASE_HOST=postgres
DATABASE_PASSWORD=devPassword
DATABASE_USER=devUser
DATABASE_DB=jettydash
DATABASE_PORT=5432
```

## Jwt Configuration

```plaintext
JWTCONFIG__SECRET=BBED4C5A13C04AEA704D9D9C18D8F5B5D1C76FA148228871CE3C1B3FD208F7AA
JWTCONFIG__ISSUER=BackendApplication.Api
JWTCONFIG__AUDIENCE=BackendApplication.Api
JWTCONFIG__ACCESSTOKENEXPIRATION=20
JWTCONFIG__REFRESHTOKENEXPIRATION=10080
```

