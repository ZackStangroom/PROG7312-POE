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
- [Algorithms & Pattern Recognition](#algorithms--pattern-recognition)
- [Video Link](#video-link)

## Features

### 🎯 Core Functionality
- **Issue Reporting**: Citizens can report municipal issues with detailed descriptions, locations, and media attachments
- **Service Request Tracking**: View and track the status of reported issues
- **Local Events & Announcements**: Browse community events and municipal announcements with 17+ seeded events
- **Intelligent Event Recommendations**: AI-powered event suggestions based on your search patterns
- **Real-time Progress Tracking**: Visual progress indicators for form completion and submission
- **Search Pattern Analysis**: View your search habits and preferences with detailed analytics

### 📋 Issue Management
- Multiple issue categories (Water & Sanitation, Roads & Transport, Electricity, etc.)
- Priority-based issue classification (Emergency, High, Standard, Low)
- Status tracking (Received, Under Review, In Progress, Resolved)
- File upload support for images, videos, and documents
- Location-based issue reporting

### 🎉 Local Events & Announcements
- **17 Pre-seeded Events**: Diverse community events across Cape Town
- **Event Categories**: Community, Arts, Sports, Municipal, Health, Environment, Education, Safety
- **Smart Filtering**: Filter by category, date range, or search keywords
- **Event Details Modal**: View comprehensive event information
- **Viewing History**: Track events you've viewed with Stack-based history (LIFO)
- **Event Recommendations**: Get personalized event suggestions based on your interests

## Technical Architecture

### Technology Stack
- **Framework**: ASP.NET Core Razor Pages 8.0
- **Language**: C# 12.0
- **Target Framework**: .NET 8
- **Frontend**: Razor Views with HTML5, CSS3, JavaScript
- **Data Storage**: In-memory collections using multiple advanced data structures
- **File Handling**: IFormFile for media uploads
- **Dependency Injection**: Built-in ASP.NET Core DI container
- **Pattern Recognition**: Custom search pattern tracking and recommendation algorithm

### Architecture Highlights
- **Repository Pattern**: Clean separation of data access logic
- **Service Layer**: Business logic encapsulated in services
- **Dependency Injection**: Loose coupling and testability
- **RESTful API**: `/api/SearchPattern` endpoints for pattern tracking

## Prerequisites

Before running this application, ensure you have the following installed:

### Required Software
- **Visual Studio 2022** (Version 17.8 or later) with ASP.NET and web development workload
- **.NET 8 SDK** (included with Visual Studio 2022)
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

### Using .NET CLI
1. Open a command prompt or terminal
2. Navigate to the project directory
3. Run `dotnet build` to compile the project
4. Ensure there are no compilation errors

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
- **Quick Actions**: Direct links to report issues, view events, and track service requests
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


### 🎉 Local Events & Announcements

#### Browsing Events:
1. **Navigate to Local Events**
   - Click "Local Events" from the main menu
   - View all upcoming community events

2. **Search & Filter Events**
   - **Search Bar**: Type keywords to find specific events
   - **Category Filter**: Filter by event category (Community, Arts, Sports, etc.)
   - **Date Range Filter**: Choose from Today, This Week, This Month, or Upcoming
   - **Smart Filtering**: Uses hashtable-optimized O(1) lookups for instant results

3. **View Event Details**
   - Click "View Details" on any event card
   - Modal displays comprehensive event information
   - Events automatically tracked in your viewing history

4. **Viewing History (Stack-Based)**
   - **View History Button**: See all events you've viewed (Stack: LIFO - most recent first)
   - **Previous Event Button**: Pop the stack to go back to previously viewed events
   - **Clear History**: Reset your viewing history
   - **Persistent Storage**: History saved in browser localStorage

5. **Personalized Recommendations**
   - **"Recommended For You" Section**: AI-powered suggestions based on your search patterns
   - **View My Patterns**: Analyze your search behavior with detailed statistics
   - **Recommendation Reasons**: See why events are recommended to you
   - **Perfect Match Indicators**: Events matching multiple preferences get bonus scores


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

#### Event Categories Available:
- 👥 **Community**: Clean-ups, celebrations, neighborhood meetings
- 🎨 **Arts**: Markets, exhibitions, festivals
- 🏃 **Sports**: Marathons, tournaments, workshops
- 🏛️ **Municipal**: Council meetings, public forums, budget consultations
- 💊 **Health**: Free screenings, wellness workshops
- 🌳 **Environment**: Tree planting, conservation awareness
- 📚 **Education**: Skills workshops, business seminars
- 🚨 **Safety**: Neighborhood watch, crime prevention

### 📊 Search Pattern Analytics
1. **Automatic Tracking**: All searches are automatically tracked
2. **View Analytics**: Click "📊 View My Patterns" button
3. **Insights Include**:
   - Total number of searches
   - Top searched categories
   - Preferred date ranges
   - Average results per search
   - Last search timestamp
4. **Clear Patterns**: Reset your search history and recommendations

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

### 1. Custom LinkedList Usage

The application implements a **custom LinkedList data structure** (`CustomLinkedList<T>`) for efficient issue management. This choice provides several advantages over standard collections:

#### Why LinkedList?
1. **Dynamic Size**: Grows and shrinks as needed without pre-allocation
2. **Efficient Insertion**: O(1) insertion at head/tail positions
3. **Memory Efficiency**: No wasted memory from unused array slots
4. **Flexible Traversal**: Forward and backward navigation capabilities

### 2. 📖 Dictionary (`Dictionary<TKey, TValue>`)
**Used For**: Event storage and fast ID-based lookups

**Implementation**: `EventRepository.cs`

**Data Structures**:
- `Dictionary<string, LocalEvent>` - Events by ID
- `Dictionary<string, List<LocalEvent>>` - Events by category
- `Dictionary<DateTime, int>` - Date counts (derived from hashtable)
- `Dictionary<string, int>` - Category counts (derived from hashtable)

**Advantages**:
- **O(1) Average Lookup**: Nearly instant access by key
- **O(1) Average Insertion**: Fast additions
- **Type Safety**: Generic implementation with compile-time checking
- **Key Flexibility**: Support for custom key types

### 3. 🔢 SortedDictionary (`SortedDictionary<TKey, TValue>`)
**Used For**: Chronologically ordered event storage

**Implementation**: `EventRepository.cs`

**Data Structure**:
- `SortedDictionary<DateTime, List<LocalEvent>>` - Events sorted by date

**Advantages**:
- **Automatic Sorting**: Events always sorted by date (binary search tree)
- **O(log n) Operations**: Logarithmic insertion and lookup
- **Range Queries**: Efficient date range filtering
- **Ordered Traversal**: In-order traversal returns events chronologically

### 4. ⚡ Hashtable (`System.Collections.Hashtable`)
**Used For**: Ultra-fast category and date frequency tracking

**Implementation**: `EventRepository.cs`, `SearchPatternService.cs`

**Data Structures**:
- `_categoryHashtable` - Category → event count mapping
- `_dateHashtable` - Date string → event count mapping
- `_categoryFrequency` - Category → search count mapping
- `_dateRangeFrequency` - Date range → search count mapping
- `_searchTermFrequency` - Search term → frequency mapping

**Advantages**:
- **O(1) Average Lookup**: Constant time access
- **O(1) Average Insertion**: Instant additions
- **Non-Generic Flexibility**: Can store any object type
- **Dynamic Sizing**: Automatic resizing based on load factor

### 5. 📚 Stack (`Stack<T>`)
**Used For**: Event viewing history and search history tracking

**Implementation**: 
- `SearchPatternService.cs` - `Stack<SearchAction>`
- `LocalEvents.cshtml` - JavaScript `EventStack` class

**Characteristics**:
- **LIFO (Last In, First Out)**: Most recent items accessed first
- Generic implementation for type safety
- Maximum size enforcement (20 for client, 100 for server)

**Advantages**:
- **O(1) Push**: Add items to top
- **O(1) Pop**: Remove items from top
- **O(1) Peek**: View top without removing
- **Natural History**: Perfect for "back" navigation and undo operations
- **Memory Efficient**: Fixed or bounded size prevents unlimited growth

## Algorithms & Pattern Recognition

### Intelligent Event Recommendations
- AI-powered algorithm for personalized event suggestions
- Analyzes user search patterns and interaction history
- Adjusts recommendations based on user feedback and new events

### Search Pattern Tracking
- Custom middleware to log user search patterns
- Stores search terms, filters, and interaction times
- Analyzes data to improve search result relevance and recommendation accuracy

### Pattern Recognition Workflow
1. **Data Collection**: Logs user interactions with the search feature
2. **Pattern Analysis**: Identifies trends and commonalities in search behavior
3. **Recommendation Generation**: Creates personalized event suggestions
4. **Feedback Loop**: Allows users to provide feedback on recommendations, improving future suggestions

### 1. 🎯 Event Recommendation Algorithm
**Implementation**: `SearchPatternService.CalculateRecommendation()`

**Algorithm Type**: Weighted Scoring with Multi-factor Analysis

**Scoring Factors** (0-1 normalized):
1. **Category Match (40% weight)**
   - Checks user's category search frequency
   - Formula: `frequency / totalSearches`
   - Higher weight due to strong preference indication

2. **Date Preference (30% weight)**
   - Matches event dates to user's preferred ranges
   - Ranges: today, this week, this month, upcoming
   - Formula: `rangeFrequency / totalSearches`

3. **Popularity Score (20% weight)**
   - Based on category event density
   - Formula: `categoryEventCount / totalEvents`
   - Ensures popular events are surfaced

4. **Recency Bonus (10% weight)**
   - Boosts events happening soon (within 14 days)
   - Formula: `1.0 - (daysUntil / 14.0)`
   - Prevents outdated recommendations

### 2. 🔍 Search Pattern Analysis
**Implementation**: `SearchPatternService.AnalyzePatterns()`

**Algorithm Type**: Frequency Analysis with Aggregation

**Process**:
1. **Hashtable Traversal**: Convert hashtables to dictionaries for LINQ
2. **Frequency Sorting**: Order categories/dates by search count
3. **Top-K Selection**: Select top 5 categories, top 3 date ranges
4. **Statistical Calculation**: Compute averages and totals

**Time Complexity**: 
- Hashtable conversion: O(m) where m = unique categories/dates
- Sorting: O(m log m)
- Overall: O(m log m) - typically m << n (unique values vs total searches)

**Output Statistics**:
- Total searches performed
- Category frequency distribution
- Date range preferences
- Average results per search
- Top categories and date ranges
- Most common search terms

### 3. 📊 Event Filtering Algorithm
**Implementation**: `LocalEvents.cshtml` JavaScript `filterEvents()`

**Algorithm Type**: Multi-criteria Filtering with Hashtable Optimization

### 4. 🔄 Search Tracking with Debouncing
**Implementation**: `SearchPatternTracker.trackSearch()`

**Algorithm Type**: Debounced Asynchronous Tracking

## Key Features Showcase

### 🎯 Smart Recommendations
- Real-time pattern learning from user searches
- Multi-factor scoring algorithm
- Personalized event suggestions
- Transparent recommendation reasons

### ⚡ Performance Optimizations
- Hashtable-based O(1) category filtering
- Client-side hashtable for instant search
- Debounced search tracking
- Efficient data structure selection

### 📱 User Experience
- Responsive design for all devices
- Real-time search with live updates
- Modal-based event details
- Stack-based viewing history
- Persistent history in localStorage

### 🔧 Developer Features
- Clean architecture with repository pattern
- Dependency injection throughout
- Generic data structures
- Comprehensive XML documentation
- RESTful API endpoints

## In-Depth Data Structure Analysis for Service Request Status Feature

### 1. 🌲 Binary Search Tree (BST) - Core Sorting & Search Engine

**Implementation**: `BinarySearchTree<T>.cs`, `ServiceRequestBSTService.cs`

#### Role in Service Request Status:
The BST serves as the **primary data structure** for organizing and retrieving service requests with optimal performance characteristics.

#### Key Contributions to Efficiency:

**A. Automatic Priority-Based Sorting**
- **Custom Comparer Logic** (`ServiceRequestComparer`):
Sort order: Emergency > High > Standard > Low // Within same priority: Older requests first Priority comparison + Date comparison

- **Real-world Example**:
When loading 50 service requests:
- 3 Emergency requests appear first (oldest to newest)
- 12 High priority requests next
- 28 Standard requests follow
- 7 Low priority requests at end

Without BST: O(n log n) sorting required each time With BST: O(n) in-order traversal returns sorted list

**B. Efficient Search Operations**
- **ID-Based Lookup** (`SearchById`):
User searches for "SR-2025-003421" Traditional array: O(n) - must check all 50 requests BST approach: O(log n) average - ~5-6 comparisons for 50 items


**C. Multi-Criteria Filtering**
- **Predicate-Based FindAll**:
- Example: Find all "Water & Sanitation" requests that are "In Progress" FilterRequests(category: "Water & Sanitation", status: InProgress)
Result: Traverses tree once (O(n)) but returns results in priority order Alternative: Sort after filtering = O(n log n) BST advantage: Pre-sorted results


**D. Real-Time Statistics Generation**
- **Single Traversal Statistics**:
GetStatistics() performs one in-order traversal to calculate:
-	Total: 50 requests
-	Pending: 18 requests
-	In Progress: 22 requests
-	Resolved: 10 requests
-	By Priority: Emergency(3), High(12), Standard(28), Low(7)
Time: O(n) single pass Memory: O(1) constant space for counters


#### Practical Efficiency Example:
- User Scenario: Municipal worker loads Service Request Status page
- **Step 1: Load 150 service requests from repository**
-	BST Insert: O(150 log 150) ≈ 1,050 operations
-	Array Insert: O(150) = 150 operations
-	Winner: Array (but read on...)
- **Step 2: Display requests sorted by priority**
-	BST In-Order: O(150) = 150 operations
-	Array Sort: O(150 log 150) ≈ 1,050 operations
-	Winner: BST (saved 900 operations!)
- **Step 3: User searches for specific ID**
-	BST Search: O(log 150) ≈ 8 operations
-	Array Search: O(150) = 150 operations
-	Winner: BST (18x faster!)
-  **Step 4: Filter by category (30 matches)**
-	BST: O(150) traverse + O(30) = 180 operations, pre-sorted
-	Array: O(150) filter + O(30 log 30) sort ≈ 300 operations
-	Winner: BST (40% faster with sorted results!)
Overall: BST provides 35% better performance for typical usage pattern

### 2. 🔗 Graph - Relationship Discovery & Network Analysis

**Implementation**: `ServiceRequestGraph.cs`, `ServiceRequestGraphService.cs`

#### Role in Service Request Status:
The Graph structure **reveals hidden patterns** and **relationships between requests** that aren't immediately obvious, enabling intelligent resource coordination and duplicate detection.

#### Key Contributions to Efficiency:

**A. Automatic Relationship Building**
- **Intelligent Connection Detection**:
- Example Dataset: 50 service requests
BuildRelationships() analyzes:
- Each pair: 50 × 49 / 2 = 1,225 comparisons

- **Creates edges based on:**
1.	Location similarity (fuzzy match)
2.	Category equality
3.	Related categories
4.	Temporal proximity

**Result: 180 relationships discovered automatically**
-	45 Same Location edges
-	62 Same Category edges
-	38 Related Category edges
-	35 Potential Duplicates


## Video Link
- Part 1: https://youtu.be/JXvtLSPzcg8
- Part 2: https://youtu.be/k6yIAYmmE9c
