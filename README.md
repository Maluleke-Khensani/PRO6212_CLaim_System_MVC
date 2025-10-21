# Claims Management System

A **web application** built with **ASP.NET Core MVC** for managing **lecturer claims**. The system provides **role-based dashboards** for Lecturers, Coordinators, and Academic Managers, allowing claims to be submitted, reviewed, and approved in a structured workflow.

The application now includes **SQL database integration SQLite database, file uploads with encryption**, session-based login, and a **unit tests** to ensure reliability.

---

## GitHub Repository

[https://github.com/Maluleke-Khensani/PRO6212_CLaim_System_MVC](https://github.com/Maluleke-Khensani/PRO6212_CLaim_System_MVC.git)

```bash
git clone https://github.com/Maluleke-Khensani/PRO6212_CLaim_System_MVC.git
```

---
## YouTube link 
[https://youtu.be/TO7bbwQlLtk?si=v20L3QSc8wul56K4]  (https://youtu.be/TO7bbwQlLtk?si=v20L3QSc8wul56K4)

---
## Features

### Lecturer Dashboard

* Submit new monthly claims.
* Attach up to **two supporting documents** (.pdf, .docx, .xlsx, max 5MB) securely with AES encryption.
* View claim history with status updates (Coordinator & Manager).
* Edit claims while **status is Pending**, cannot edit once approved/rejected.

### Coordinator Dashboard

* Review claims submitted by lecturers.
* Verify or reject claims.
* Filter claims by **Pending, Approved, or Rejected**.
* Download supporting documents (automatically decrypted).

### Academic Manager Dashboard

* View claims verified by coordinators.
* Make **final approval or rejection**.
* Approved/rejected claims are locked and cannot be modified.
* Filter claims by **Pending, Approved, or Rejected**.

### Home Page

* Serves as a **navigation hub**.
* Clean, neutral, and professional UI with role-based cards.
* Quick access to Lecturer, Coordinator, and Manager dashboards.

### Login Page

* Session-based login for Lecturers, Coordinators, and Managers.
* Credentials validated against the **Users table** in SQL database (`ClaimsDB.db`).
* Redirects to dashboard on success, displays friendly error on failure or expired session.

### Unit Testing

* Uses **xUnit + Moq** with an **in-memory SQLite database**.
* Tests include:

  1. Creating a claim (ensures encrypted documents are saved).
  2. Deleting a claim (ensures removal from DB).
  3. Retrieving claims (ensures correct claims returned).
  4. Controller tests (Index, ClaimForm, ViewClaim) for proper filtering, authorization, and error handling.

---

## Technology Stack

* **Framework:** ASP.NET Core MVC (C#)
* **Frontend:** Razor Views, Bootstrap 5
* **Database:** SQLite (local), In-memory database for tests
* **File Storage:** Encrypted file bytes stored in DB BLOBs
* **Version Control:** Git + GitHub

---

## Project Plan

The project was developed in **phases**:

1. **Phase 1 (Prototype)**

   * Implemented basic lecturer functionality: create, view, edit claims.
   * Coordinator and Manager dashboards with placeholder review/approval.
   * Static storage (Lists) to simulate claim submissions.

2. **Phase 2 (Enhancement)**

   * Full database integration (SQLite).
   * User authentication and session-based login.
   * Attach/upload supporting claim documents with encryption.
   * Implemented basic error handling and validation.

3. **Phase 3 is yet to come 

## Notes

* Encryption keys are currently in code for testing; move them to **environment variables or Key Vault** for production.
* Files use AES encryption, decrypted only when downloaded.
* Unit tests ensure key functionality and error handling work reliably.
