# Project Abyss: Abyssal Events

<details>
  <summary>Table of Contents</summary>
  <ul>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#get-started">Get Started</a>
      <ul>
        <li><a href="#preparation">Preparation</a></li>
      </ul>
    </li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
  </ul>
</details>

## About the Project

This is a simple ASP.NET MVC Web application. Simply, it is a blog website which you can add, edit and delete event posts. Also, you can like event posts from users. For image uploads, I have used Cloudinary.

## Built with

![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white)
![.Net](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
![MicrosoftSQLServer](https://img.shields.io/badge/Microsoft%20SQL%20Server-CC2927?style=for-the-badge&logo=microsoft%20sql%20server&logoColor=white)

## Get Started

### Preparation

Enter your connection string for SQL Database connection and your Cloudinary information for image upload in `appsettings.json` file.

```json
  "ConnectionStrings": {
    "AbyssalEventsConnectionString": "events-database-connection-string",
    "AbyssAuthConnectionString": "auth-database-connection-string"
  },
  "Cloudinary": {
    "CloudName": "your_cloudinary_cloudname",
    "ApiKey": "your_cloudinary_api-key",
    "ApiSecret": "your_cloudinary_api-secret"
  }
```

To able to add "categories" in the website, a seeded admin user needed. You can reconfigure settings below in the `AuthDbContext.cs` on Data folder.
(If you have login issues with the seeded user, try to write username in capital letters and/or set username same as email)

```cs
    var superAdminUser = new IdentityUser {
        UserName = "username",
        Email = "email",
        NormalizedEmail = "email".ToUpper(),
        NormalizedUserName = "username".ToUpper(),
        Id = superAdminId,
        };
    superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(superAdminUser, "password");
```

After these changes, open "Package Manager Console" and add these migrations to create tables and update the database.

```pm
Add-Migration "Initial migration" -Context "EventDbContext"
Update-Database -Context "EventDbContext"
Add-Migration "Create auth db" -Context "AuthDbContext"
Update-Database -Context "AuthDbContext"
```

And it is ready to run.

## License

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

MIT License

Copyright (c) 2023 Raiyader

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

## Contact

Mert Evirgen - evrgnmert@gmail.com<br><br>
[![LinkedIn](https://img.shields.io/badge/linkedin-%230077B5.svg?style=for-the-badge&logo=linkedin&logoColor=white)](https://www.linkedin.com/in/evirgenmert/)

Project Link: [https://github.com/rhayaden/abyssal-events](https://github.com/Raiyader/abyssal-events)
