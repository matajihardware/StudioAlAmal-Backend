Studio Al Amal API Documentation
Complete API reference for the Studio Al Amal Photography Website Backend.

Base URLs

Auth Service: http://localhost:5001
Content Service: http://localhost:5002
Communication Service: http://localhost:5003


Authentication
All protected endpoints require a JWT token in the Authorization header:
Authorization: Bearer {your-jwt-token}
Token Duration: 24 hours
How to get a token:

Register a user via /api/Auth/register
Login via /api/Auth/login
Use the returned token in subsequent requests


Auth Service Endpoints
Base URL: http://localhost:5001

POST /api/Auth/register
Create a new admin user account.
Authentication Required:  No
Request Body:
json{
  "username": "string (3-50 characters, required)",
  "email": "string (valid email, required)",
  "password": "string (6-100 characters, required)",
  "role": "Admin or SuperAdmin (required)"
}
Example Request:
json{
  "username": "admin1",
  "email": "admin1@studioalamal.tn",
  "password": "Admin@123",
  "role": "SuperAdmin"
}
Success Response (200 OK):
json{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "username": "admin1",
  "email": "admin1@studioalamal.tn",
  "role": "SuperAdmin",
  "expiresAt": "2026-01-10T15:30:00Z"
}
Error Responses:
400 Bad Request - Username already exists:
json{
  "message": "Username already exists"
}
400 Bad Request - Email already exists:
json{
  "message": "Email already exists"
}
400 Bad Request - Invalid role:
json{
  "message": "Invalid role. Must be 'Admin' or 'SuperAdmin'"
}
400 Bad Request - Validation error:
json{
  "errors": {
    "Username": ["The Username field is required."],
    "Password": ["The field Password must be a string with a minimum length of 6."]
  }
}

POST /api/Auth/login
Login with existing credentials and receive JWT token.
Authentication Required: ❌ No
Request Body:
json{
  "username": "string (required)",
  "password": "string (required)"
}
Example Request:
json{
  "username": "admin1",
  "password": "Admin@123"
}
Success Response (200 OK):
json{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "username": "admin1",
  "email": "admin1@studioalamal.tn",
  "role": "SuperAdmin",
  "expiresAt": "2026-01-10T15:30:00Z"
}
Error Responses:
401 Unauthorized - Invalid credentials:
json{
  "message": "Invalid username or password"
}
401 Unauthorized - Account deactivated:
json{
  "message": "Account is deactivated"
}
```

---

### GET /api/Auth/validate

Validate a JWT token.

**Authentication Required:** ✅ Yes

**Headers:**
```
Authorization: Bearer {token}
Success Response (200 OK):
json{
  "message": "Token is valid",
  "claims": [
    {
      "type": "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier",
      "value": "1"
    },
    {
      "type": "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name",
      "value": "admin1"
    },
    {
      "type": "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress",
      "value": "admin1@studioalamal.tn"
    },
    {
      "type": "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",
      "value": "SuperAdmin"
    }
  ]
}
Error Responses:
401 Unauthorized - No token provided:
json{
  "message": "No token provided"
}
401 Unauthorized - Invalid token:
json{
  "message": "Invalid token"
}

Content Service Endpoints
Base URL: http://localhost:5002

Promos Endpoints

GET /api/Promos
Get all promotional content (supports filtering).
Authentication Required: ❌ No
Query Parameters:

activeOnly (boolean, optional) - If true, returns only active promos

Examples:

Get all promos: GET /api/Promos
Get active only: GET /api/Promos?activeOnly=true

Success Response (200 OK):
json[
  {
    "id": 1,
    "title": "Winter Special - 20% Off Wedding Packages",
    "description": "Book your wedding photography for winter 2026 and get 20% off",
    "imageUrl": "https://example.com/winter-promo.jpg",
    "displayOrder": 1,
    "isActive": true,
    "createdAt": "2026-01-09T10:30:00Z",
    "updatedAt": null
  },
  {
    "id": 2,
    "title": "Corporate Event Photography Special",
    "description": "Professional event coverage at discounted rates",
    "imageUrl": "https://example.com/corporate-promo.jpg",
    "displayOrder": 2,
    "isActive": true,
    "createdAt": "2026-01-09T11:00:00Z",
    "updatedAt": "2026-01-09T14:30:00Z"
  }
]

GET /api/Promos/{id}
Get a specific promo by ID.
Authentication Required: ❌ No
URL Parameters:

id (integer, required) - Promo ID

Example: GET /api/Promos/1
Success Response (200 OK):
json{
  "id": 1,
  "title": "Winter Special - 20% Off Wedding Packages",
  "description": "Book your wedding photography for winter 2026 and get 20% off",
  "imageUrl": "https://example.com/winter-promo.jpg",
  "displayOrder": 1,
  "isActive": true,
  "createdAt": "2026-01-09T10:30:00Z",
  "updatedAt": null
}
Error Responses:
404 Not Found:
json{
  "message": "Promo not found"
}
```

---

### POST /api/Promos

Create a new promo.

**Authentication Required:** ✅ Yes

**Headers:**
```
Authorization: Bearer {token}
Content-Type: application/json
Request Body:
json{
  "title": "string (max 200 characters, required)",
  "description": "string (optional)",
  "imageUrl": "string (required)",
  "displayOrder": "integer (default: 0)",
  "isActive": "boolean (default: true)"
}
Example Request:
json{
  "title": "Summer Photography Special",
  "description": "15% off all outdoor photography sessions",
  "imageUrl": "https://example.com/summer-promo.jpg",
  "displayOrder": 1,
  "isActive": true
}
Success Response (201 Created):
json{
  "id": 3,
  "title": "Summer Photography Special",
  "description": "15% off all outdoor photography sessions",
  "imageUrl": "https://example.com/summer-promo.jpg",
  "displayOrder": 1,
  "isActive": true,
  "createdAt": "2026-01-09T15:00:00Z",
  "updatedAt": null
}
Error Responses:
401 Unauthorized - No token or invalid token
400 Bad Request - Validation error:
json{
  "errors": {
    "Title": ["The Title field is required."],
    "ImageUrl": ["The ImageUrl field is required."]
  }
}
```

---

### PUT /api/Promos/{id}

Update an existing promo (partial update supported).

**Authentication Required:** ✅ Yes

**Headers:**
```
Authorization: Bearer {token}
Content-Type: application/json
URL Parameters:

id (integer, required) - Promo ID

Request Body (all fields optional):
json{
  "title": "string (max 200 characters, optional)",
  "description": "string (optional)",
  "imageUrl": "string (optional)",
  "displayOrder": "integer (optional)",
  "isActive": "boolean (optional)"
}
Example Request: PUT /api/Promos/1
json{
  "title": "UPDATED: Winter Special - 25% Off!",
  "isActive": false
}
Success Response (204 No Content)
No response body.
Error Responses:
401 Unauthorized - No token or invalid token
404 Not Found:
json{
  "message": "Promo not found"
}
```

---

### DELETE /api/Promos/{id}

Delete a promo.

**Authentication Required:** ✅ Yes

**Headers:**
```
Authorization: Bearer {token}
URL Parameters:

id (integer, required) - Promo ID

Example: DELETE /api/Promos/1
Success Response (204 No Content)
No response body.
Error Responses:
401 Unauthorized - No token or invalid token
404 Not Found:
json{
  "message": "Promo not found"
}

Photos Endpoints

GET /api/Photos
Get all photos (supports filtering).
Authentication Required: ❌ No
Query Parameters:

activeOnly (boolean, optional) - If true, returns only active photos
category (string, optional) - Filter by category (e.g., "Wedding", "Portrait", "Event")

Examples:

Get all photos: GET /api/Photos
Get active only: GET /api/Photos?activeOnly=true
Get by category: GET /api/Photos?category=Wedding
Combined: GET /api/Photos?activeOnly=true&category=Wedding

Success Response (200 OK):
json[
  {
    "id": 1,
    "title": "Romantic Beach Wedding Ceremony",
    "description": "Beautiful sunset wedding at Hammamet beach",
    "imageUrl": "https://example.com/wedding1.jpg",
    "thumbnailUrl": "https://example.com/wedding1-thumb.jpg",
    "category": "Wedding",
    "displayOrder": 1,
    "isActive": true,
    "createdAt": "2026-01-09T10:00:00Z"
  },
  {
    "id": 2,
    "title": "Professional Corporate Headshot",
    "description": "Executive portrait for business profiles",
    "imageUrl": "https://example.com/portrait1.jpg",
    "thumbnailUrl": "https://example.com/portrait1-thumb.jpg",
    "category": "Portrait",
    "displayOrder": 1,
    "isActive": true,
    "createdAt": "2026-01-09T11:00:00Z"
  }
]

GET /api/Photos/{id}
Get a specific photo by ID.
Authentication Required: ❌ No
URL Parameters:

id (integer, required) - Photo ID

Example: GET /api/Photos/1
Success Response (200 OK):
json{
  "id": 1,
  "title": "Romantic Beach Wedding Ceremony",
  "description": "Beautiful sunset wedding at Hammamet beach",
  "imageUrl": "https://example.com/wedding1.jpg",
  "thumbnailUrl": "https://example.com/wedding1-thumb.jpg",
  "category": "Wedding",
  "displayOrder": 1,
  "isActive": true,
  "createdAt": "2026-01-09T10:00:00Z"
}
Error Responses:
404 Not Found:
json{
  "message": "Photo not found"
}
```

---

### POST /api/Photos

Create a new photo.

**Authentication Required:** ✅ Yes

**Headers:**
```
Authorization: Bearer {token}
Content-Type: application/json
Request Body:
json{
  "title": "string (max 200 characters, required)",
  "description": "string (optional)",
  "imageUrl": "string (required)",
  "thumbnailUrl": "string (optional)",
  "category": "string (optional)",
  "displayOrder": "integer (default: 0)",
  "isActive": "boolean (default: true)"
}
Example Request:
json{
  "title": "Garden Wedding Portrait",
  "description": "Bride and groom in botanical garden",
  "imageUrl": "https://example.com/garden-wedding.jpg",
  "thumbnailUrl": "https://example.com/garden-wedding-thumb.jpg",
  "category": "Wedding",
  "displayOrder": 2,
  "isActive": true
}
Success Response (201 Created):
json{
  "id": 3,
  "title": "Garden Wedding Portrait",
  "description": "Bride and groom in botanical garden",
  "imageUrl": "https://example.com/garden-wedding.jpg",
  "thumbnailUrl": "https://example.com/garden-wedding-thumb.jpg",
  "category": "Wedding",
  "displayOrder": 2,
  "isActive": true,
  "createdAt": "2026-01-09T16:00:00Z"
}
```

**Error Responses:**

**401 Unauthorized** - No token or invalid token

**400 Bad Request** - Validation error

---

### PUT /api/Photos/{id}

Update an existing photo (partial update supported).

**Authentication Required:** ✅ Yes

**Headers:**
```
Authorization: Bearer {token}
Content-Type: application/json
URL Parameters:

id (integer, required) - Photo ID

Request Body (all fields optional):
json{
  "title": "string (optional)",
  "description": "string (optional)",
  "imageUrl": "string (optional)",
  "thumbnailUrl": "string (optional)",
  "category": "string (optional)",
  "displayOrder": "integer (optional)",
  "isActive": "boolean (optional)"
}
Success Response (204 No Content)
Error Responses:
401 Unauthorized - No token or invalid token
404 Not Found:
json{
  "message": "Photo not found"
}
```

---

### DELETE /api/Photos/{id}

Delete a photo.

**Authentication Required:** ✅ Yes

**Headers:**
```
Authorization: Bearer {token}
URL Parameters:

id (integer, required) - Photo ID

Success Response (204 No Content)
Error Responses:
401 Unauthorized - No token or invalid token
404 Not Found:
json{
  "message": "Photo not found"
}

Videos Endpoints

GET /api/Videos
Get all videos (supports filtering).
Authentication Required: ❌ No
Query Parameters:

activeOnly (boolean, optional) - If true, returns only active videos
category (string, optional) - Filter by category

Examples:

Get all videos: GET /api/Videos
Get active only: GET /api/Videos?activeOnly=true
Get by category: GET /api/Videos?category=Wedding

Success Response (200 OK):
json[
  {
    "id": 1,
    "title": "Wedding Highlights - Amira & Karim",
    "description": "3-minute wedding highlights reel",
    "videoUrl": "https://www.youtube.com/watch?v=abc123",
    "thumbnailUrl": "https://example.com/video-thumb.jpg",
    "duration": 180,
    "category": "Wedding",
    "displayOrder": 1,
    "isActive": true,
    "createdAt": "2026-01-09T12:00:00Z"
  }
]

GET /api/Videos/{id}
Get a specific video by ID.
Authentication Required: ❌ No
URL Parameters:

id (integer, required) - Video ID

Success Response (200 OK):
json{
  "id": 1,
  "title": "Wedding Highlights - Amira & Karim",
  "description": "3-minute wedding highlights reel",
  "videoUrl": "https://www.youtube.com/watch?v=abc123",
  "thumbnailUrl": "https://example.com/video-thumb.jpg",
  "duration": 180,
  "category": "Wedding",
  "displayOrder": 1,
  "isActive": true,
  "createdAt": "2026-01-09T12:00:00Z"
}
Error Responses:
404 Not Found:
json{
  "message": "Video not found"
}
```

---

### POST /api/Videos

Create a new video.

**Authentication Required:** ✅ Yes

**Headers:**
```
Authorization: Bearer {token}
Content-Type: application/json
Request Body:
json{
  "title": "string (max 200 characters, required)",
  "description": "string (optional)",
  "videoUrl": "string (required)",
  "thumbnailUrl": "string (optional)",
  "duration": "integer (seconds, optional)",
  "category": "string (optional)",
  "displayOrder": "integer (default: 0)",
  "isActive": "boolean (default: true)"
}
Example Request:
json{
  "title": "Corporate Event Recap 2026",
  "description": "Full event coverage and highlights",
  "videoUrl": "https://vimeo.com/123456789",
  "thumbnailUrl": "https://example.com/corporate-video-thumb.jpg",
  "duration": 240,
  "category": "Corporate",
  "displayOrder": 1,
  "isActive": true
}
Success Response (201 Created):
json{
  "id": 2,
  "title": "Corporate Event Recap 2026",
  "description": "Full event coverage and highlights",
  "videoUrl": "https://vimeo.com/123456789",
  "thumbnailUrl": "https://example.com/corporate-video-thumb.jpg",
  "duration": 240,
  "category": "Corporate",
  "displayOrder": 1,
  "isActive": true,
  "createdAt": "2026-01-09T17:00:00Z"
}
```

**Error Responses:**

**401 Unauthorized** - No token or invalid token

**400 Bad Request** - Validation error

---

### PUT /api/Videos/{id}

Update an existing video (partial update supported).

**Authentication Required:** ✅ Yes

**Headers:**
```
Authorization: Bearer {token}
Content-Type: application/json
URL Parameters:

id (integer, required) - Video ID

Request Body (all fields optional):
json{
  "title": "string (optional)",
  "description": "string (optional)",
  "videoUrl": "string (optional)",
  "thumbnailUrl": "string (optional)",
  "duration": "integer (optional)",
  "category": "string (optional)",
  "displayOrder": "integer (optional)",
  "isActive": "boolean (optional)"
}
Success Response (204 No Content)
Error Responses:
401 Unauthorized - No token or invalid token
404 Not Found:
json{
  "message": "Video not found"
}
```

---

### DELETE /api/Videos/{id}

Delete a video.

**Authentication Required:** ✅ Yes

**Headers:**
```
Authorization: Bearer {token}
URL Parameters:

id (integer, required) - Video ID

Success Response (204 No Content)
Error Responses:
401 Unauthorized - No token or invalid token
404 Not Found:
json{
  "message": "Video not found"
}

About Us Endpoints

GET /api/AboutUs
Get the About Us content.
Authentication Required: ❌ No
Success Response (200 OK):
json{
  "id": 1,
  "content": "Studio Al Amal is a premier photography studio based in Tunis, Tunisia. Founded in 2020, we specialize in wedding photography, corporate portraits, and special events.",
  "imageUrl": "https://example.com/studio-photo.jpg",
  "updatedAt": "2026-01-09T14:00:00Z"
}
Error Responses:
404 Not Found:
json{
  "message": "About Us content not found"
}
```

---

### PUT /api/AboutUs

Update the About Us content (creates if doesn't exist).

**Authentication Required:** ✅ Yes

**Headers:**
```
Authorization: Bearer {token}
Content-Type: application/json
Request Body:
json{
  "content": "string (required)",
  "imageUrl": "string (optional)"
}
Example Request:
json{
  "content": "Studio Al Amal is a premier photography studio based in Tunis, Tunisia. Founded in 2020, we specialize in wedding photography, corporate portraits, and special events. Our team of experienced photographers combines artistic vision with technical expertise.",
  "imageUrl": "https://example.com/updated-studio-photo.jpg"
}
Success Response (204 No Content)
No response body.
Error Responses:
401 Unauthorized - No token or invalid token
400 Bad Request - Validation error:
json{
  "errors": {
    "Content": ["The Content field is required."]
  }
}

Communication Service Endpoints
Base URL: http://localhost:5003

Contact Endpoints

POST /api/Contact
Submit a contact form (public endpoint, no authentication required).
Authentication Required: ❌ No
Request Body:
json{
  "fullName": "string (max 100 characters, required)",
  "email": "string (valid email, required)",
  "phone": "string (max 20 characters, optional)",
  "subject": "string (max 200 characters, required)",
  "message": "string (required)"
}
Example Request:
json{
  "fullName": "Ahmed Ben Salah",
  "email": "ahmed.salah@example.com",
  "phone": "+216 20 123 456",
  "subject": "Wedding Photography Inquiry",
  "message": "Hello, I'm interested in booking your services for my wedding on June 15, 2026. Could you please provide more information about your packages and pricing?"
}
Success Response (201 Created):
json{
  "id": 1,
  "fullName": "Ahmed Ben Salah",
  "email": "ahmed.salah@example.com",
  "phone": "+216 20 123 456",
  "subject": "Wedding Photography Inquiry",
  "message": "Hello, I'm interested in booking your services for my wedding on June 15, 2026. Could you please provide more information about your packages and pricing?",
  "isRead": false,
  "submittedAt": "2026-01-09T18:00:00Z",
  "readAt": null
}
Error Responses:
400 Bad Request - Validation error:
json{
  "errors": {
    "Email": ["The Email field is not a valid e-mail address."],
    "Subject": ["The Subject field is required."]
  }
}
```

---

### GET /api/Contact

Get all contact submissions (admin only).

**Authentication Required:** ✅ Yes

**Headers:**
```
Authorization: Bearer {token}
Query Parameters:

unreadOnly (boolean, optional) - If true, returns only unread messages

Examples:

Get all messages: GET /api/Contact
Get unread only: GET /api/Contact?unreadOnly=true

Success Response (200 OK):
json[
  {
    "id": 1,
    "fullName": "Ahmed Ben Salah",
    "email": "ahmed.salah@example.com",
    "phone": "+216 20 123 456",
    "subject": "Wedding Photography Inquiry",
    "message": "Hello, I'm interested in booking your services...",
    "isRead": false,
    "submittedAt": "2026-01-09T18:00:00Z",
    "readAt": null
  },
  {
    "id": 2,
    "fullName": "Fatma Trabelsi",
    "email": "fatma.trabelsi@example.com",
    "phone": "+216 22 987 654",
    "subject": "Corporate Event Photography",
    "message": "We're organizing a corporate conference...",
    "isRead": true,
    "submittedAt": "2026-01-09T19:00:00Z",
    "readAt": "2026-01-09T20:00:00Z"
  }
]
```

**Error Responses:**

**401 Unauthorized** - No token or invalid token

---

### GET /api/Contact/{id}

Get a specific contact submission by ID (admin only).

**Authentication Required:** ✅ Yes

**Headers:**
```
Authorization: Bearer {token}
URL Parameters:

id (integer, required) - Submission ID

Example: GET /api/Contact/1
Success Response (200 OK):
json{
  "id": 1,
  "fullName": "Ahmed Ben Salah",
  "email": "ahmed.salah@example.com",
  "phone": "+216 20 123 456",
  "subject": "Wedding Photography Inquiry",
  "message": "Hello, I'm interested in booking your services for my wedding on June 15, 2026. Could you please provide more information about your packages and pricing?",
  "isRead": false,
  "submittedAt": "2026-01-09T18:00:00Z",
  "readAt": null
}
Error Responses:
401 Unauthorized - No token or invalid token
404 Not Found:
json{
  "message": "Submission not found"
}
```

---

### PUT /api/Contact/{id}/mark-read

Mark a contact submission as read (admin only).

**Authentication Required:** ✅ Yes

**Headers:**
```
Authorization: Bearer {token}
URL Parameters:

id (integer, required) - Submission ID

Example: PUT /api/Contact/1/mark-read
Success Response (204 No Content)
No response body.
Error Responses:
401 Unauthorized - No token or invalid token
404 Not Found:
json{
  "message": "Submission not found"
}
```

---

### DELETE /api/Contact/{id}

Delete a contact submission (admin only).

**Authentication Required:** ✅ Yes

**Headers:**
```
Authorization: Bearer {token}
URL Parameters:

id (integer, required) - Submission ID

Example: DELETE /api/Contact/1
Success Response (204 No Content)
No response body.
Error Responses:
401 Unauthorized - No token or invalid token
404 Not Found:
json{
  "message": "Submission not found"
}

Common Error Responses
400 Bad Request
Validation errors or malformed requests.
Example:
json{
  "errors": {
    "Field1": ["Error message 1"],
    "Field2": ["Error message 2"]
  }
}

401 Unauthorized
Missing or invalid JWT token.
Example:
json{
  "message": "Invalid token"
}
Or no response body (just HTTP 401 status).

404 Not Found
Resource not found.
Example:
json{
  "message": "Resource not found"
}

500 Internal Server Error
Server error.
Example:
json{
  "message": "An error occurred while processing your request"
}

Data Types Reference
Common Field Types

string - Text data
integer - Whole numbers
boolean - true or false
datetime - ISO 8601 format: 2026-01-09T18:00:00Z

Common Constraints

required - Field must be provided
optional - Field can be omitted
max length - Maximum characters allowed
min length - Minimum characters required
valid email - Must be valid email format
phone - Phone number format (flexible)


Rate Limiting
Currently not implemented. All endpoints can be called without rate limits during development.
Production considerations:

Consider implementing rate limiting for public endpoints
Especially important for registration and contact form endpoints


CORS Configuration
All services are configured with AllowAll CORS policy for development:

All origins allowed
All methods allowed
All headers allowed

Production note: Update CORS policy to allow only specific frontend origins.

Database
Database Name: StudioAlAmalDB
Server: (localdb)\MSSQLLocalDB (development)
Tables:

Users
Promos
Photos
Videos
AboutUs
ContactSubmissions


****


Environment Configuration & HTTPS Setup
1. Use Environment Variables for Sensitive Data

What we did: Removed secrets (JWT keys, database connection strings) from appsettings.json and stored them securely using User Secrets (for development) and Environment Variables (for production).

Why: Keeps sensitive information out of source control and reduces security risks.

Production config: appsettings.Production.json contains only non-sensitive settings; secrets are supplied via environment variables at runtime.

2. Add HTTPS Support

What we did:

Installed a trusted development HTTPS certificate (dotnet dev-certs https --trust).

Updated launchSettings.json for each service (AuthService, ContentService, CommunicationService) to include an HTTPS profile with dedicated HTTPS ports (7001–7003).

Why: Enables secure communication during local development, ensures features relying on HTTPS (e.g., secure cookies, JWT validation) behave correctly, and mirrors production behavior.

3. Running the Services

To run a service with HTTPS locally, use the HTTPS launch profile:

dotnet run --launch-profile https


Swagger UI can then be accessed securely via the HTTPS URL.

****

Global Error Handling

What we did:

Created a GlobalExceptionMiddleware for all three services (AuthService, ContentService, CommunicationService).

This middleware catches unhandled exceptions in the pipeline, logs them, and returns a consistent JSON error response.

Registered the middleware in Program.cs before authentication/authorization.

Why we did it:

Centralized error handling → developers don’t need to wrap every controller action in try/catch.

Consistent API responses → clients always receive JSON with a message and optional details (shown only in development).

Better logging → all unhandled exceptions are logged automatically for easier debugging and monitoring.

Security → sensitive exception details are hidden in production environments, preventing accidental data leaks.

Example response:

{
  "message": "An error occurred while processing your request",
  "details": "Optional detailed exception message in development"
}


This ensures a clean, predictable API behavior and makes debugging much easier during development.

****


Health Check Endpoints
Each service exposes a /health endpoint to verify that it is running and can connect to its database. This helps monitor service availability and readiness. Connections now use the LocalDB instance pipe name to ensure EF Core can reliably connect in development. Example: GET /health → Healthy.

****

Validation:
All incoming DTOs are validated using FluentValidation to ensure required fields, proper formats, and constraints (e.g., email format, password strength, max lengths). This prevents invalid data from reaching the database or business logic.

Rate Limiting:
Rate limiting is applied globally and on sensitive endpoints to prevent abuse:

AuthService: max 5 login/register requests per minute per IP

CommunicationService: max 3 contact form submissions per hour per IP

All services: global limit of 100 requests per minute per user/IP

This protects the APIs from brute-force attacks, spam, and overuse.

