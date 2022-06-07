# Floristai-server

This is a RESTful API for the flower shop. It involves user management and authentication system, flower ordering and fetching system as well as messaging features.

# Setup:

1) In order to run, there is a need to setup a appsettings.json file:
```
  "ConnectionStrings": {
    "DefaultConnection": "Server=------;Database=------;Uid=------;Pwd=------"
  },
  "ServiceEmailAccount": {
    "Email": "---",
    "Password": "---"
  },
  "JwtTokenKey": {
    "Key": "aaaa[aa]aa[aa]aa"
  },
  "Jwt": {
    "Key": "ThisismySecretKey",
    "Issuer": "Test.com"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

2) In package manager console type "Update-Database"
