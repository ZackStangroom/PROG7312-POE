# PROG7312-POE - Municipal Services Application

A comprehensive municipal services application built with ASP.NET Core MVC (.NET 8) that allows citizens to report issues, view service requests, and access local events and announcements.

## Table of Contents
- [Features](#features)
- [Technical Architecture](#technical-architecture)
- [Prerequisites](#prerequisites)
- [Installation & Setup](#installation--setup)
- [Compilation Instructions](#compilation-instructions)
- [Running the Application](#running-the-application)
- [How to Use the Application](#how-to-use-the-application)
- [Data Structures Implementation](#data-structures-implementation)
- [Project Structure](#project-structure)
- [Troubleshooting](#troubleshooting)

## Features

### 🎯 Core Functionality
- **Issue Reporting**: Citizens can report municipal issues with detailed descriptions, locations, and media attachments
- **Service Request Tracking**: View and track the status of reported issues
- **Local Events & Announcements**: Browse community events and municipal announcements
- **Real-time Progress Tracking**: Visual progress indicators for form completion and submission

### 📋 Issue Management
- Multiple issue categories (Water & Sanitation, Roads & Transport, Electricity, etc.)
- Priority-based issue classification (Emergency, High, Standard, Low)
- Status tracking (Received, Under Review, In Progress, Resolved)
- File upload support for images, videos, and documents
- Location-based issue reporting

## Technical Architecture

### Technology Stack
- **Framework**: ASP.NET Core MVC 8.0
- **Language**: C# 12.0
- **Target Framework**: .NET 8
- **Frontend**: Razor Views with HTML5, CSS3, JavaScript
- **Data Storage**: In-memory collections using custom LinkedList implementation
- **File Handling**: IFormFile for media uploads
- **Dependency Injection**: Built-in ASP.NET Core DI container

### Custom Data Structures
The application implements a **Custom LinkedList** data structure for efficient issue management, providing O(1) insertion and flexible traversal capabilities.

## Prerequisites

Before running this application, ensure you have the following installed:

### Required Software
- **Visual Studio 2022** (Version 17.8 or later) with ASP.NET and web development workload
- **IIS Express** (included with Visual Studio)

### Alternative IDEs
- **Visual Studio Code** with C# extension
- **JetBrains Rider** 2023.3 or later


## Installation & Setup

### Clone the Repository

https://github.com/ST10372065/PROG7312-POE.git

## Compilation Instructions

### Using Visual Studio 2022
1. Open `PROG7312-POE.sln` in Visual Studio
2. Select **Build > Rebuild Solution** (Ctrl+Shift+B)
3. Ensure build succeeds without errors
4. Check Output window for any warnings

## Running the Application

### Method 1: Visual Studio (Recommended)
1. Open the solution in Visual Studio 2022
2. Ensure `PROG7312-POE` is set as the startup project
3. Press **F5** or click **Debug > Start Debugging**
4. The application will launch in your default browser
5. Default URL: `https://localhost:7139` or `http://localhost:5139`

## How to Use the Application

### 🏠 Home Page
- **Navigation**: Use the main menu to access different features
- **Quick Actions**: Direct links to report issues and view services
- **Information Cards**: Community information and announcements

### 📝 Reporting Issues

#### Step-by-Step Process:
1. **Navigate to Report Issues**
   - Click "Report Issues" from the main menu
   - Or use the direct link from the home page

2. **Fill Required Information**
   - **Location** *(Required)*: Specify where the issue is located
     ```
     Example: "End of Church Street, Claremont"
     ```
   - **Category** *(Required)*: Select appropriate category
     - Water & Sanitation
     - Roads & Transport
     - Electricity
     - Waste Management
     - Parks & Recreation
     - Housing
     - Emergency Services
     - Public Safety
     - Other

3. **Provide Description** *(Required)*
   - Detailed explanation of the issue (max 500 characters)
   - Include relevant details like time of occurrence, severity, etc.

4. **Upload Media** *(Optional)*
   - Supported formats: Images (JPG, PNG, GIF), Videos (MP4, AVI), Audio, PDF, Word documents
   - Maximum file size: 10MB
   - Multiple files can be uploaded

5. **Track Progress**
   - Real-time progress bar shows completion status
   - Progress updates as you fill required fields
   - Green "Ready to Submit" when all required fields are complete

6. **Submit Report**
   - Click "Submit Issue Report"
   - Instant feedback: "Report Submitted! Thank you for helping our community."
   - Option to submit additional reports

### 📊 Progress Tracking
- **Visual Progress Bar**: Always visible, updates in real-time
- **Field Validation**: Required fields marked with red asterisk (*)
- **Character Counter**: Shows remaining characters for description
- **File Size Validation**: Automatic validation for upload limits

### 🔄 Issue Status Workflow
1. **Received**: Issue logged in system
2. **Under Review**: Team assessment and priority assignment
3. **In Progress**: Active work on resolution
4. **Resolved**: Issue completed and closed

### ⏱️ Expected Response Times
- **Emergency Issues**: Within 24 hours
- **High Priority**: 1-3 business days
- **Standard Issues**: 3-7 business days
- **Low Priority**: 1-2 weeks

## Data Structures Implementation

### Custom LinkedList Usage

The application implements a **custom LinkedList data structure** (`CustomLinkedList<T>`) for efficient issue management. This choice provides several advantages over standard collections:

#### Why LinkedList?
1. **Dynamic Size**: Grows and shrinks as needed without pre-allocation
2. **Efficient Insertion**: O(1) insertion at head/tail positions
3. **Memory Efficiency**: No wasted memory from unused array slots
4. **Flexible Traversal**: Forward and backward navigation capabilities


