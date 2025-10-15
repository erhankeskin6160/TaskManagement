Bu proje, .NET 6+ ile geliştirilmiş bir Web API (backend) ve Angular ile geliştirilmiş bir frontend (client) içeren "Görev Takip ve Yönetim Sistemi"dir. Kullanıcılar görev oluşturabilir, güncelleyebilir, silebilir ve görevleri durumlarına göre filtreleyip listeleyebilirler. Proje Clean Architecture, CQRS ve MediatR yaklaşımlarıyla organize edilmişti

Kullanılan Teknolojiler

Backend: .NET 6+ Web API

ORM: Entity Framework Core (Code First)

Mimari: Clean Architecture (API, Application, Domain, Infrastructure)

Pattern: CQRS (Command / Query ayrımı)

MediatR ile Command/Query handler'lar

Doğrulama: FluentValidation (opsiyonel ama önerilir)

Mapping: AutoMapper (opsiyonel)

Veritabanı: SQL Server

Dokümantasyon: Swagger / Swashbuckle

Frontend: Angular (CLI, TypeScript)

Proje Kapsamı / Özellikler

Task (Görev) modeli CRUD işlemlerini destekler:

Başlık

Açıklama

Oluşturulma tarihi

Son teslim tarihi

Durum (Bekliyor, Devam Ediyor, Tamamlandı)

Entity Framework Code-First ile veritabanı

CQRS & MediatR ile Command / Query ayrımı

Katmanlı mimari: API, Application, Domain, Infrastructure

Swagger UI ile API test etme

Angular tabanlı basit bir UI (görev listesi, oluşturma, düzenleme, filtreleme)

FluentValidation ile veri doğrulama
