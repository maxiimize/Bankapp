BTS Banken – Adminsystem för bankpersonal

Detta är ett administrativt banksystem byggt med ASP.NET Core Razor Pages och Entity Framework Core (Database First). Applikationen är avsedd för bankpersonal – inte kunder – och erbjuder funktionalitet för att hantera kunder, konton, transaktioner och användare med rollbaserad inloggning.

Funktioner:

Inloggning med ASP.NET Core Identity

Rollbaserad åtkomstkontroll (Admin, Cashier)

Statistikpanel på startsidan (öppen för alla)

Sökfunktion för kunder med paginering

Kunddetaljsida med konton och totala saldot

Kontosida med transaktionshistorik och lazy loading via AJAX

Möjlighet att utföra insättningar, uttag och överföringar

Bootstrap-design med responsiv layout (gratis tema)

Skydd mot ogiltig inmatning och otillåten åtkomst

Driftsatt på Azure (Försökte)

Roller och behörigheter:

Admin – Full åtkomst inklusive användarhantering och registrering
Cashier – Endast kund- och kontohantering
Anonym – Endast tillgång till startsidan

Seedade användare:

richard.chalk@admin.se | Lösenord: Abc123# | Roll: Admin
richard.chalk@cashier.se | Lösenord: Abc123# | Roll: Cashier

Tekniker som används:

ASP.NET Core Razor Pages

Entity Framework Core (Database First)

SQL Server

ASP.NET Core Identity

Bootstrap 5

JavaScript / AJAX

Git och GitHub


Azure: Försökte driftsätta till Azure men fick inte till det, 500.30. 