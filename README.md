

#  Claims Management System  

A prototype web application built with **ASP.NET Core MVC** for managing **lecturer claims**. The system provides different dashboards for **Lecturers, Coordinators, and Academic Managers**, allowing claims to be submitted, reviewed, and approved in a structured workflow.  

---

##  Features  

###  Lecturer Dashboard  
- Submit new monthly claims.  
- View claim history with status updates.  
- Edit claims before final submission, cannot be edited once reviewed.  

###  Coordinator Dashboard  
- Review claims submitted by lecturers.  
- Verify claims and update claim statuses.  
- Filter claims by **Pending, Approved, or Rejected** using a tabbed view.  

###  Academic Manager Dashboard  
- View claims verified by coordinators.  
- Make the **final approval or rejection**.  
- Approved/rejected claims are locked and cannot be modified further.
- Edit claims before final submission, cannot be edited once reviewed.
   Filter claims by **Pending, Approved, or Rejected** using a tabbed view.  

 
###  Home Page  
- Serves as the **navigation hub**.  
- Provides quick access to Lecturer, Coordinator, and Manager dashboards.  
- Clean and professional UI with role-based cards.  

---

##  Technology Stack  

- **Framework:** ASP.NET Core MVC (C#)  
- **Frontend:** Razor Views, Bootstrap 5  
- **Data Storage:** In-memory Lists (prototype), can be extended to SQL Database in later phases  
- **Version Control:** Git + GitHub  

---
## 📅 Project Plan  

The project was developed in **phases**:  

1. **Phase 1 (Prototype)**  
   - Implement basic lecturer functionality (create, view, edit claims).  
   - Coordinator and Manager dashboards with placeholder review/approval.  
   - Static storage (Lists) to simulate claim submissions.  

2. **Phase 2 (Enhancement)**  
   - Introduce full database (SQL Server).  
   - User authentication (role-based access: Lecturer, Coordinator, Manager).  
   - Attach/upload supporting claim documents.  

3. **Phase 3 (Final)**  
   - Add reporting features (monthly claim reports, lecturer claim summaries).  
   - Email notifications on claim approvals/rejections.  
   - Deployment to cloud (Azure or AWS).  

---

## ⚡ Methodology  

The project followed an **Iterative (Agile-inspired) methodology**:  
- Start with a **working prototype** (Phase 1).  
- Expand functionality incrementally (Phase 2 & 3).  
- Frequent reviews and adjustments based on feedback .  

---

## 📋 Requirements  

- **.NET 6.0 SDK** or later
- MYSQL Wrokbench wil be used in part 2   
- Visual Studio 2022 / VS Code  
- Git (for version control)  

---

 #GitHub Repository:
   
    https://github.com/Maluleke-Khensani/PRO6212_CLaim_System_MVC.gitgit clone https://github.com/your-username/claims-system.git
   

