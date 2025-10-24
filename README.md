# 🏨 Mini Hotel Management System (PRN212)

This repository contains **two assignments (A01 and A02)** for the PRN212 course, implemented using **C# WPF** with layered architecture.

---

## 📁 Project Structure

---

## 🧩 Assignment 01 — **WPF + LINQ (In-Memory)**

### 🔹 Overview
- Implements a mini hotel management system using **List<T>** as an in-memory database.
- Supports basic **CRUD** operations for:
  - Customers
  - Rooms

### 🧱 Architecture Layers
| Layer | Description |
|-------|--------------|
| `BusinessObjects` | Contains entity classes (`Customer`, `RoomInformation`) |
| `DataAccessObjects` | Simulates data access using List collections |
| `Repositories` | Acts as data abstraction layer |
| `Services` | Handles business logic |
| `HotelManagement` | WPF UI project with tabs for Customers and Rooms |

### 🚀 Features
- Login for Admin / Customer
- Add, edit, delete rooms and customers
- Data binding via LINQ collections
- Validation and confirmation dialogs
- Simple and clean WPF interface

---

## 🧩 Assignment 02 — **WPF + EF Core (Database)**

### 🔹 Overview
- Extended version using **Entity Framework Core (Database-First)** with SQL Server.
- Supports CRUD for:
  - Customers
  - Rooms
  - Bookings
- Includes reporting feature (Revenue report by date range)

### 🗄️ Database
**Database name:** `MiniHotelManagement`

**Tables included:**
- `Customer`
- `RoomInformation`
- `RoomType`
- `BookingReservation`
- `BookingDetail`

### 🔹 Connection String (in `appsettings.json`)
```json
{
  "ConnectionStrings": {
    "FUMiniHotelDB": "Server=localhost;Database=MiniHotelManagement;User Id=sa;Password=V@ndiep0210;TrustServerCertificate=True"
  },
  "Admin": {
    "Email": "DiepNV@FUMiniHotelSystem.com",
    "Password": "V@ndiep0210"
  }
}

# Initialize repository
git init

# Add all files
git add .

# Commit initial version
git commit -m "Add A01 (LINQ) and A02 (EF Core) solutions"

# Link to GitHub repo
git remote add origin https://github.com/<username>/MiniHotelManagement.git

# Push to main
git branch -M main
git push -u origin main
