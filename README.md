<div align="center">

  <img src="https://raw.githubusercontent.com/devicons/devicon/master/icons/dotnetcore/dotnetcore-original.svg" width="100" height="100" alt="dotnet logo" />

  # 🛡️ Gradi.Security.Identity
  **An Enterprise-Grade Security Framework for Modern .NET APIs**

  [![NuGet](https://img.shields.io/badge/NuGet-v1.0.0-blue.svg?style=for-the-badge&logo=nuget)](https://github.com/codewithgradi/Gradi.Security.Jwt/packages)
  [![Platform](https://img.shields.io/badge/.NET-10.0-purple.svg?style=for-the-badge&logo=dotnet)](https://dotnet.microsoft.com/)
  [![License](https://img.shields.io/badge/License-MIT-green.svg?style=for-the-badge)](https://opensource.org/licenses/MIT)

  ---

  [Overview](#-overview) • [Key Features](#-key-features) • [Installation](#-installation) • [Quick Start](#-quick-start) • [Architecture](#-architecture)

</div>

## 📖 Overview

`Gradi.Security.Identity` was born out of a need to eliminate boilerplate in microservice development. It centralizes **JWT Authentication** and **ASP.NET Core Identity** logic into a single, generic package. 

Building **Simply** or **HireMe**? Don't spend hours on auth—plug in this library and focus on your business logic.

---

## ✨ Key Features

| Feature | Description |
| :--- | :--- |
| **Generic TUser** | Seamlessly integrates with any class inheriting from `IdentityUser`. |
| **Token Rotation** | Automatic generation of stateless Access Tokens and stateful Refresh Tokens. |
| **Cookie Auth** | Built-in middleware to handle JWTs stored in `HTTP-Only` cookies. |
| **HmacSha512** | Standardized high-security hashing for token signatures. |
| **Clean Architecture** | Zero coupling between your Controllers and Token logic. |

---

## 🚀 Installation

Add the GitHub package source and install via the .NET CLI:

```bash
dotnet add package Gradi.Security.Jwt --version 1.0.0 --source github
---

## 🛠️ Quick Start

### 1. Define Settings
Add your credentials to your API's `appsettings.json`. The library will automatically bind these values.

```json
{
  "JwtSettings": {
    "SigningKey": "REPLACE_WITH_A_SECURE_LONG_KEY_AT_LEAST_32_CHARS",
    "Issuer": "GradiAuth",
    "Audience": "GradiUsers",
    "DurationInMinutes": 15
  }
}

