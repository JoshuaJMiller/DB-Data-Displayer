# DB Data Displayer
Visually insert or search data in sample SQL Server

## Description

WPF Application (.NET Framework) using Dapper to execute stored procedures in local SQL server sample database
- Search by any column (first name, last name, email, etc.) to query and display sample personal data 
- Query full database
- Insert new rows of data

![DB Data Control Running](https://media.giphy.com/media/OBScdN1HH1UkRFw8AO/giphy.gif)

### Features
- Elegantly interactive custom buttoms, scrollviewer, scrollbars, and textboxes
- Simple, sanitized database insertion
- Intuitive database searching

### Coming Soon
- Edit existing database table items
- User experience improvements

## What I Learned
- Creation and execution of SQL stored procedures
- Execution of stored procedures from C# using Dapper
- XAML for GUI layer (Styles, ControlTemplates, etc.)
- Overeiding deafault WPF controls for more premium UX and feel
- Dynamic XAML interaction logic in C# (StackPanel.Children.Add based on number of forecast days return from API)
