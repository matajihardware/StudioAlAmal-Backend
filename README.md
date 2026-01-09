# Studio Al Amal - Photography Studio Backend

A modern **microservices-based backend system** for a photography studio website in Tunisia, built with **.NET 9**, **Entity Framework Core**, **JWT Authentication**, and **SQL Server**.

## 🎯 Project Overview

This backend powers the Studio Al Amal photography website, providing secure content management, user authentication, and client communication features through three independent microservices.

## 🚀 Features

### 🔐 Authentication Service (Port 5001)
- User registration and login system
- JWT token-based authentication (24-hour expiration)
- BCrypt password hashing for security
- Role-based authorization (Admin/SuperAdmin)
- Token validation endpoint

### 📸 Content Management Service (Port 5002)
- **Promotional Content**: Carousel management for homepage
- **Photo Gallery**: Organize photography work by categories
- **Video/Reels Management**: Short video edits showcase
- **About Us**: Studio information management
- Full CRUD operations with JWT authentication
- Display order control for content
- Active/inactive status toggle

### 📧 Communication Service (Port 5003)
- Public contact form submission (no authentication required)
- Admin dashboard for viewing messages
- Mark messages as read/unread
- Message deletion for admins
- Filter unread messages

## 🛠️ Technologies & Tools

**Backend Framework:**
- .NET 9 Web API
- C# 12

**Database & ORM:**
- Entity Framework Core 9.0
- SQL Server (LocalDB)
- Code-First migrations

**Security:**
- JWT (JSON Web Tokens)
- BCrypt.Net for password hashing
- CORS configuration

**Development Tools:**
- Visual Studio 2022
- SQL Server Management Studio (SSMS)
- Postman (API testing)
- Git & GitHub

## 📁 Project Architecture
```
StudioAlAmal/
├── .gitignore
├── README.md
└── Services/
    ├── AuthService/              # Port 5001
    │   ├── Controllers/
    │   ├── Data/                 # DbContext
    │   ├── DTOs/                 # Data Transfer Objects
    │   ├── Models/               # User model
    │   ├── Services/             # JWT Token Service
    │   └── Migrations/
    ├── ContentService/           # Port 5002
    │   ├── Controllers/
    │   ├── Data/
    │   ├── DTOs/
    │   ├── Models/               # Promo, Photo, Video, AboutUs
    │   └── Migrations/
    └── CommunicationService/     # Port 5003
        ├── Controllers/
        ├── Data/
        ├── DTOs/
        ├── Models/               # ContactSubmission
        └── Migrations/
```

## 🗄️ Database Schema

**Users Table** (Authentication)
- User credentials with hashed passwords
- Role-based access control
- Account status tracking

**Promos Table** (Content)
- Promotional carousel content
- Display order management
- Active/inactive toggle

**Photos Table** (Content)
- Photography portfolio
- Category organization
- Thumbnail support

**Videos Table** (Content)
- Video reels and edits
- Duration tracking
- Category organization

**AboutUs Table** (Content)
- Studio information
- Single-entry design

**ContactSubmissions Table** (Communication)
- Client inquiries
- Read/unread status
- Timestamp tracking



## 📝 What I Learned

Building this project taught me:
- Microservices architecture design patterns
- JWT authentication implementation
- Entity Framework Core migrations
- RESTful API best practices
- SQL Server database design
- Git version control
- Separation of concerns in backend development




## 📄 License

This project is open source and available under the [MIT License](LICENSE).

## 🙏 Acknowledgments

Built for **Studio Al Amal**, a photography studio in Tunisia specializing in weddings, portraits, and events.


---

⭐ **If you find this project helpful, please give it a star!**

💼 **Open to job opportunities** in full-stack development 
```

*I LOVE MY JOB !*

*Last modified January the 9th*
