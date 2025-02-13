# MIYCN Portal - Digital Training & Management System

## Overview
The **MIYCN Portal** (Maternal, Infant, and Young Child Nutrition) is a **digital training and management system** designed to streamline large-scale training processes. It provides an intuitive platform for trainers and participants to manage training sessions, track attendance, conduct evaluations, and generate reports.

## Tech Stack
- **Backend:** ASP.NET Core (C#), Entity Framework Core, MSSQL  
- **Frontend:** Razor Pages / MVC, HTML, CSS, JavaScript  
- **Database:** Microsoft SQL Server  
- **Authentication:** ASP.NET Identity, JWT-based authentication  
- **File Management:** AWS S3 for document and media uploads  
- **Reporting:** SSRS (SQL Server Reporting Services) / Excel Export  

---

## Features & Modules

### 1. **User Roles & Access Management**
- Role-based access for **Administrators, Trainers, and Participants**   

### 2. **Training Session Management**
- Trainers can **schedule sessions** with start and end dates  
- Participants can **register for sessions**  
- **Automated notifications & reminders** for upcoming training  

### 3. **Attendance Tracking**
- **Manual Check-in:** Digital verification for attendance  
- **Real-time dashboards** for monitoring participant engagement  
- **Audit logs** to track attendance history  

### 4. **Daily Evaluations & Activity Reporting**
- Trainers can submit **daily reports** on session progress  
- Participants can complete **feedback forms & assessments**  
- Reports are **automatically generated** for facilitators  


### 5. **Analytics & Performance Monitoring**
- **Dashboard with training metrics**: attendance rates, performance trends  
- **Exportable reports (CSV, PDF, Excel)**  
- Data-driven insights for decision-making  

---

## Installation & Setup

### **1. Clone the repository**
```sh
git clone https://github.com/SirP-TechHub-0904/MIYCN
cd miycn-portal
