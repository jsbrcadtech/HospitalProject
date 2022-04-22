# SQL queries for users

## Insert Users

```
INSERT INTO HospitalDb.dbo.AspNetUsers (Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName)
VALUES (N'4e4d017c-5ccd-4ad0-bebc-b9f742ab3abf',N'staff@mail.com',0,N'AKNqLm8ixwlfiFRLX1IfHQFfRJyENMJaYcYhCxoQ0uoEQZGVD2MCbOPLv4TlO2/EsQ==',N'60184021-12b5-4136-bfbb-809968d65bc7',NULL,0,0,NULL,1,0,N'staff@mail.com');

INSERT INTO HospitalDb.dbo.AspNetUsers (Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName)
VALUES (N'82d38dd6-71d8-4423-95e8-7290986113d3',N'admin@mail.com',0,N'ANt8Pg950Aujc4wXjPKTtKIWEM+VsPWXbp9MDP9ff8kZAzUeWGTHkF4wuw5K7OkYKw==',N'bfdfc96b-b8bd-40ae-8c51-e1933ad58eb6',NULL,0,0,NULL,1,0,N'admin@mail.com');
```

## Insert Roles

```
INSERT INTO AspNetRoles (Id, Name) VALUES(1, 'admin');
INSERT INTO AspNetRoles (Id, Name) VALUES(2, 'staff');
```

## Insert User Role relation

```
INSERT INTO AspNetUserRoles (UserId, RoleId) VALUES('82d38dd6-71d8-4423-95e8-7290986113d3', '1'); -- admin
INSERT INTO AspNetUserRoles (UserId, RoleId) VALUES('4e4d017c-5ccd-4ad0-bebc-b9f742ab3abf', '2'); -- staff
```
-----------------------------------------------------
Pallav

# Inventory & Inventory ledgers

- api to add new item to inventory
- api to update inventory item
- api to get all inventory items (supports searh by item name & pagination)
- api to get item by id
- api to get ledger entries for an item (supports filter by date range)
- api to delete item and all ledger entries from inventory
- api to delete leadger entry for an item
- async requests from client side using javascript
- reusable JS methods for API calls
- reponsive features like search as user types
- client side validations using html5 and JS
- server side validations

-----------------------------------------------------

Justus

# ParkingSpot 

- api to add a new parking spot to the system
- api to update a parking spot in the system
- api to list all parking spots in the system 
- api to get a parking spot details by its id
- api to delete a parking spot from the system
- base authentication with no roles established yet (To be added)  

# Staff 

- api to add a new staff to the system
- api to update a staff in the system
- api to list all staff in the system 
- api to get a staff details, annoucements made, and parking spot associtaed by the staff id
- api to delete a staff from the system
- base authentication with no roles established yet (To be added)

 -----------------------------------------------------
 
 Karmugil

# Announcement 

- api to add a new announcement to the system
- api to update a announcement in the system
- api to list all announcement in the system 
- api to get an announcement details by its id
- api to delete an announcement from the system
- base authentication with no roles established for the update,add and delete routes.

# Prescreening 

- api to add a new prescreening form to the system if the condition is met.
- user table is connected to the prescreening api so that the appointment table will be using the last createdAt time to check the vaccination status.
- For a userId the existing prescreening data will be deleted and the new data will be added which can be tracked using the createAt column data.
- only vaccinated user be redirected to the appointment table.
- base authentication with no roles established so that only logged in user can access the presscreening form.  

 -----------------------------------------------------
 Divish
 
 # Specialization

- api to add a new specialization to the system
- api to search specialization by id
- api to update specialization in the system
- api to delete specialization in the system
- api to list specializations in system 

 # Patients

- api to add a new patient to the system
- api to list all patients in the system
- api to get patient by id
- api to delete patient in the system by id
- api to update patient in the system 
