
# OfflineTicketingSystem
# پروژه: سیستم تیکت آفلاین (Offline Ticketing System)

یک پروژه ساده برای مدیریت تیکت‌های داخلی (آفلاین) سازمانی.

---

## توضیحات

این مخزن شامل کد backend مربوط به سیستم تیکت آفلاین-(شرکت برقتو-چالش استخدامی) است. هدف پروژه فراهم کردن یک ساختار پایه‌ای برای ایجاد، مدیریت و پیگیری تیکت‌ها در داخل سازمان است.

## امکانات کلیدی

* مدیریت کاربران و نقش‌ها (Admin، Employee)
* ذخیره‌سازی داده در SQL Server
* آماده برای اجرا با استفاده از Migrations (EF Core)

---

## کاربران پیش‌فرض

اطلاعات کاربران اولیه (در صورت نیاز می‌توانید با اجرای مایگریشن یا ری‌استور کردن بکاپ آنها را در دیتابیس داشته باشید):

* **Admin User**

  * FullName: `Admin User`
  * Email: `admin@company.com`
  * PasswordHash: `admin123`
  * Role: `Admin`

---

* **Saeid doabi**

  * FullName: `Saeid doabi`
  * Email: `Saeid@company.com`
  * PasswordHash: `emp123`
  * Role: `Employee`

---

* **Reza Employee**

  * FullName: `Reza Employee`
  * Email: `Reza@company.com`
  * PasswordHash: `emp123`
  * Role: `Employee`



---

## پیش‌نیازها

* Visual Studio 2022
* .NET SDK مناسب پروژه (مطابق فایل‌های پروژه)
* SQL Server 2022

---

## راه‌اندازی محلی (مراحل سریع)

1. مخزن را کلون کنید و پروژه را در Visual Studio 2022 باز کنید.
2. در **Package Manager Console** مراحل زیر را انجام دهید:

   * از قسمت `Default project` پروژه **Persistence** را انتخاب کنید.
   * دستور زیر را اجرا کنید:

```package manager console
PM> Update-Database
```

این دستور مایگریشن‌ها را اجرا و دیتابیس را ایجاد/به‌روز می‌کند.

3. (جایگزین) اگر تمایل دارید می‌توانید از فایل بکاپ دیتابیس استفاده کنید: فایل بکاپ با نام `OfflineTicketingSystemDb.bak` موجود است — آن را در SQL Server بازیابی (restore) کنید.

4. پس از آماده شدن دیتابیس، پروژه وب (وب‌اپ یا API) را به‌عنوان Startup انتخاب کرده و اجرا کنید.

---


---


---

## تست و کار با حساب‌ها

* با ورود به سیستم به‌عنوان `admin@company.com` می‌توانید دسترسی‌های مدیریتی را بررسی کنید.
* حساب‌های کارکنان (`Saeid@company.com` و `Reza@company.com`) با نقش Employee برای تست عملکردهای معمولی کاربردی هستند.

---



---



---
