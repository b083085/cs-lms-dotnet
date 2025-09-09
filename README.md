# 🚀 Capstone - Library Management System

The Library Management System (LMS) is a capstone project developed using .NET that aims to modernize and streamline the process of managing library resources. It provides an efficient, user-friendly, and secure way for librarians, students, and administrators to handle common library tasks such as book cataloging, borrowing, returning, user management, and report generation.

This system eliminates the limitations of traditional manual library operations by introducing automation, centralized data storage, and real-time access. It ensures improved accuracy, reduces paperwork, and enhances the overall user experience.

Key stakeholders of the system include:
- Librarians – manage books, monitor transactions, and generate reports.
- Students / Borrowers – search the catalog, borrow and return books, and check availability.
- Administrators – oversee user accounts, system configurations, and usage analytics.

The system is designed with a modular architecture, ensuring scalability and maintainability. It follows software engineering best practices and implements role-based access control to ensure secure operations.

### Objectives
1. To provide a digital platform for managing library resources effectively.
2. To automate the process of borrowing, returning, and tracking books.
3. To improve accuracy in maintaining book inventories and member records.
4. To generate useful reports and analytics for administrators.
5. To reduce the workload of librarians and improve service for students.

---

## 📖 Table of Contents
- [Features](#-features)
- [Tech Stack](#-tech-stack)
- [Getting Started](#-getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
- [Usage](#-usage)
- [Configuration](#-configuration)
- [Testing](#-testing)
- [Project Structure](#-project-structure)
- [Contributing](#-contributing)
- [License](#-license)

---

## ✨ Features
- 📚 Book Management – Add, update, delete, and categorize books.
- 👤 User Management – Manage student, librarian, and admin accounts.
- 🔍 Search & Catalog – Search books by title, author, ISBN, or category.
- 🔄 Borrowing & Returning – Track issued books and due dates.
- 📊 Reports & Analytics – Generate overdue, borrowed, and inventory reports.
- 🔒 Role-Based Access Control – Different dashboards for librarians, students, and admins.
- 🌐 Online Access – Web-based interface for convenient access.

---

## 🛠 Tech Stack
- [.NET 8](https://dotnet.microsoft.com/)
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)
- [SQL Server / PostgreSQL] (choose your DB)
- [Swagger](https://swagger.io/) for API documentation
- [xUnit / NUnit / MSTest] for unit testing

---

## ⚡ Getting Started

### Prerequisites
Make sure you have the following installed:
- [.NET SDK 8.0+](https://dotnet.microsoft.com/en-us/download)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [Rider](https://www.jetbrains.com/rider/)
- [Docker](https://www.docker.com/) *(optional, if containerized)*

### Installation
```bash
# Clone the repo
git clone https://github.com/b083085/cs-lms-dotnet.git
cd project-name

# Restore dependencies
dotnet restore

# Build the project
dotnet build
