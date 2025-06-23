# 🛒 ECommerceRestApi - ASP.NET Core E-Ticaret API Projesi

Bu proje, ASP.NET Core 8 ve Entity Framework Core kullanılarak geliştirilmiş bir RESTful e-ticaret API'sidir. Kullanıcı kimlik doğrulama, ürün yönetimi, kategori yönetimi, alışveriş sepeti ve sipariş işlemleri gibi temel e-ticaret özelliklerini içermektedir.

---

## 🧰 Kullanılan Teknolojiler

- ASP.NET Core 8
- Entity Framework Core (Code First)
- Microsoft SQL Server
- ASP.NET Identity (kullanıcı yönetimi)
- JWT Authentication (güvenli giriş)
- Repository - Service Pattern
- Swagger UI (API testleri için)

---


## 📁 Proje Yapısı

| Dosya / Klasör           | Açıklama                           |
|-------------------------|----------------------------------|
| `Controllers/`          | API uç noktaları                  |
| `Models/`               | Veritabanı varlık sınıfları      |
| `Services/`             | İş servisleri (Abstract + Concrete) |
| `Repository/`           | EF Core DbContext ve repository katmanı |
| `Dto/`                  | Veri transfer nesneleri (Login, Register vb.) |
| `Core/Utilities/`       | Result yapıları (Success, Error, DataResult) |
| `Migrations/`           | EF Core migration dosyaları      |
| `appsettings.json`      | Uygulama yapılandırması           |
| `Program.cs`            | Giriş noktası                    |

---


## ✅ Temel Özellikler

- 🔐 JWT ile kullanıcı girişi ve yetkilendirme
- 👤 Admin ve normal kullanıcı rolleri
- 🛍️ Ürün ve kategori yönetimi
- 🛒 Alışveriş sepeti işlemleri
- 📦 Sipariş oluşturma ve takip
- 📄 Swagger UI üzerinden test etme
- 📑 Sonuç yönetimi: `SuccessResult`, `ErrorResult`, `DataResult` yapıları

---

# 🚀 Projeyi Çalıştırma

```bash
git clone https://github.com/alitolga0/ECommerceApp.git

```

## 📌 Kayıt Olma

```bash
POST /api/Auth/register
Content-Type: application/json

{
  "userName": "tolga",
  "email": "tolga@example.com",
  "password": "Sifre123*",
  "name": "Ali Tolga",
  "surname": "Çakır"
}

```


## 🔑Giriş Yapma

```bash
POST /api/Auth/login
Content-Type: application/json

{
  "email": "tolga@example.com",
  "password": "Sifre123*"
}

```
---




## API Endpoint Örnekleri

| Metot | Endpoint                         | Açıklama                          |
|-------|---------------------------------|----------------------------------|
| POST  | `/api/Auth/register`             | Yeni kullanıcı kaydı oluşturur    |
| POST  | `/api/Auth/login`                | Kullanıcı girişi yapar (JWT token) |
| GET   | `/Product/GetAll`                | Tüm ürünleri listeler             |
| POST  | `/Product/Add`                  | Yeni ürün ekler                   |
| GET   | `/Category/GetAll`               | Tüm kategorileri listeler         |
| GET   | `/CartItem/GetAll`               | Kullanıcının sepetini getirir    |
| POST  | `/Order/Add`                    | Yeni sipariş oluşturur            |
| GET   | `/Order/GetAllByUserWithDetails`| Siparişleri detaylarıyla getirir  |


---
## 🧠 Katkıda Bulunmak

Pull request’ler her zaman açıktır. Büyük değişiklikler için lütfen önce neyi değiştirmek istediğinizi tartışmak üzere bir issue açın.

---

## 📧 İletişim

Bu proje hakkında sorularınız varsa veya katkıda bulunmak istiyorsanız benimle iletişime geçebilirsiniz.

📨 E-posta: [cakiralitolga@gmail.com](mailto:cakiralitolga@gmail.com)

--- 

Hazırlayan: **Ali Tolga Çakir**

--- 


