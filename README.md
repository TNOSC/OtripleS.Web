# OtripleS.Client.Web

![.NET](https://img.shields.io/badge/.NET-9.0-blueviolet?logo=dotnet)
![Build](https://img.shields.io/github/actions/workflow/status/TNOSC/OtripleS.Web/dotnet.yml?branch=main&label=build&logo=github)
![License](https://img.shields.io/github/license/TNOSC/OtripleS.Api?color=green)

This repository is a **rewrite of the [Otriples.Portal](https://github.com/hassanhabib/Otriples.Portal) project** using the **latest version of .NET**.  
The main goal is not only to modernize the project but also to take the opportunity to **apply and master software architecture standards** with a strong focus on the **web part**.

---

## 🎯 Objectives

- Upgrade the project to the latest **.NET** ecosystem.
- Refactor with a **clean and modular architecture**.
- Apply **best practices and standards** for building robust and scalable web applications.

---

## 🚀 Why This Rewrite?

The original Otriples.Portal served its purpose, but software evolves fast.  
By rewriting it, we:
- Embrace the **latest .NET features** (performance, better tooling).
- Ensure the codebase follows **industry standards**.
- Provide a foundation that is **easier to maintain and extend**.
- Create a **reference project** for others who want to learn modern .NET practices.

---

## 📚 Learning & Mastery

This repository will also serve as a **playground** to experiment and apply:
- Coding conventions and design principles.
- Best practices from community leaders (e.g., Hassan Habib’s Standards).
- DevOps & deployment with Docker and Kubernetes.
- Observability (logging, monitoring, tracing).

---

## 🛠️ Tech Stack

- **.NET 9**
- **Blazor Fluent UI**


---

## 📌 Status

🚧 **Work in progress**: currently setting up the base architecture and aligning it with standards.

---

## 📚 Running the Project

To run the project locally:

```bash
git clone https://github.com/TNOSC/OtripleS.Web.git
cd OtripleS.Web/src
dotnet restore
dotnet run
```

Alternatively, you can run the project using Docker Compose:

```bash
git clone https://github.com/TNOSC/OtripleS.Web.git
cd OtripleS.Web
docker-compose -f docker-compose.yml -p tnosc-otriples-web up -d
```
After starting the application, it will be available at:
```bash
http://localhost:8080
```

> **Note:**  
> During development, I faced an issue when trying to display data grids using an abstract `GridBase` component with **Blazor stream rendering** (`@attribute [StreamRendering]`).  
> Because of rendering inconsistencies, I decided to continue working with **FluentGrid** instead, which provides stable rendering and the required features for my use case.
