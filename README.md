# Kiota Implementation

A sample project demonstrating how to use [Microsoft Kiota](https://github.com/microsoft/kiota) to generate and consume strongly-typed API clients in a microservices architecture using **.NET 8**.

---

## 📘 What is Kiota?

[Kiota](https://github.com/microsoft/kiota) is an open-source code generator developed by Microsoft that builds strongly typed SDKs from **OpenAPI specifications**.

Instead of manually writing `HttpClient` wrappers to call external or internal APIs, Kiota lets you generate a fully-typed API client that:

* Supports authentication, serialization, and error handling
* Avoids boilerplate code
* Improves maintainability

---

## ⚙️ How to Use Kiota

### ✅ Step 1: Install Kiota CLI

```bash
dotnet tool install --global Microsoft.OpenApi.Kiota
```

### ✅ Step 2: Generate an SDK from an OpenAPI spec

You need a running API with Swagger (OpenAPI) support.

```bash
kiota generate \
  -l CSharp \
  -d http://localhost:7000/swagger/v1/swagger.json \
  -c UserServiceClient \
  -n UserServiceSdk \
  -o ./ConsumerService/UserServiceSdk
```

### ✅ Step 3: Add required NuGet packages to your consuming project

```bash
dotnet add package Microsoft.Kiota.Abstractions

dotnet add package Microsoft.Kiota.Serialization.Json

dotnet add package Microsoft.Kiota.Http.HttpClientLibrary
```

### ✅ Step 4: Register the SDK in `Program.cs`

```csharp
builder.Services.AddSingleton(sp => {
    var adapter = new HttpClientRequestAdapter(new AnonymousAuthenticationProvider())
    {
        BaseUrl = "http://userservice:8080"
    };
    return new UserServiceClient(adapter);
});
```

### ✅ Step 5 (Optional): Publish SDK as NuGet Package

If you want to share the SDK across multiple services:

```bash
dotnet pack -c Release

dotnet nuget push bin/Release/UserServiceSdk.*.nupkg --source <your-nuget-feed>
```

Then in other services:

```bash
dotnet add package UserServiceSdk
```

---

## 📊 Before and After Kiota

### ❌ Without Kiota (Manual HTTP Calls)

```csharp
var httpClient = new HttpClient();
var response = await httpClient.GetAsync("http://userservice:8080/api/users");
var json = await response.Content.ReadAsStringAsync();
var users = JsonSerializer.Deserialize<List<User>>(json);
```

* Needs manual request building
* Needs manual deserialization
* No compile-time safety on endpoints
* Hard to reuse and maintain

### ✅ With Kiota (Generated SDK)

```csharp
var users = await _userServiceClient.Users.GetAsync();
```

* Strongly typed method and return types
* Auto serialization/deserialization
* Integrated retry, logging, middleware
* Cleaner code and better dev experience

---

## 📁 Project Structure

```
Kiota-Implementation/
├── docker-compose.yml
├── UserService/               # .NET 8 API providing user data
│   └── Controllers/
├── ConsumerService/          # .NET 8 API consuming UserService via Kiota SDK
│   └── UserServiceSdk/       # Kiota-generated client SDK
│   └── Controllers/
```

## 🚀 Running the Project

### Prerequisites:

* [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
* [Docker & Docker Compose](https://docs.docker.com/compose/install/)

### Commands:

```bash
git clone https://github.com/thekaranpargaie/Kiota-Implementation.git
cd Kiota-Implementation
docker-compose up --build
```

Swagger URLs:

* UserService: [http://localhost:7000/swagger](http://localhost:7000/swagger)
* ConsumerService: [http://localhost:7001/swagger](http://localhost:7001/swagger)

---

## 🧠 Why Use Kiota?

* ✨ Strong typing: Compile-time safety for APIs
* 🔁 Reusable SDKs across services
* 🧱 Cleaner architecture: Domain-driven interaction
* 🚀 Less boilerplate: No more manual serialization or endpoint string handling

