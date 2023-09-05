eStore Microservis Architect

.NET Core Kullanarak Domain-Driven Design (DDD) ve CQRS ile eTicaret Uygulaması Oluşturma

Bu proje, .NET Core kullanılarak geliştirilecek bir eTicaret uygulamasını oluşturmayı amaçlar ve bu uygulamada Domain-Driven Design (DDD) ve Command Query Responsibility Segregation (CQRS) gibi modern yazılım tasarım prensiplerini uygulamayı hedefler.

Proje Amaçları:

Mikroservis Tabanlı Mimariden Yararlanma: Uygulama, mikroservisler olarak tasarlanacaktır. Her bir mikroservis, özgün bir işlevi yerine getirecek ve bağımsızca dağıtılabilir olacaktır.

Domain-Driven Design (DDD) İlkelerini Uygulama: Uygulama, DDD prensiplerini kullanarak iş mantığını ve varlıkları tanımlayacak, sınırlı bağlamları düzgün bir şekilde ayıracak ve zengin domain modelleri oluşturacaktır.

Command Query Responsibility Segregation (CQRS) Uygulama: Uygulama, CQRS prensibini benimseyecektir. Bu, yazma (Command) ve okuma (Query) işlemlerini ayrı tutmayı ve performansı artırmayı sağlar.

Asenkron İletişim ve Etkinlik Tabanlı Mimari: Mikroservisler arasındaki iletişim, asenkron RabbitMQ veya gRPC gibi teknolojilerle sağlanacak ve olay tabanlı bir mimari kullanılacaktır. Bu, servisler arasında esnek ve verimli iletişim sağlar.

Dağıtık İşlemleri Yönetme: Dağıtık işlemleri yönetmek için Sage (Saga) deseni veya diğer uygun desenler kullanılacaktır. Bu, iş süreçlerinin güvenilir ve tutarlı bir şekilde yönetilmesini sağlar.

Olay Kaynaklı Mimarileri Kullanma: Veri değişiklikleri ve olaylar, olay kaynaklı mimari kullanılarak kaydedilecektir. Bu, denetlenebilirlik, geçmiş sorguları ve günlükleme için olanak sağlar.

En İyi Uygulama ve Tasarım Şablonlarının Kullanma: Proje, en iyi uygulama prensiplerine ve tasarım şablonlarına uygun olarak geliştirilecektir. Bu, kodun sürdürülebilirliği ve geliştirilebilirliği için önemlidir.

Bu hedefler doğrultusunda, eTicaret uygulamanızı daha güçlü, ölçeklenebilir ve sürdürülebilir bir şekilde geliştirmeyi amaçlıyoruz.
![image](https://github.com/dikmenonur/eStore/assets/3075597/f5a1ca1a-1923-42b2-a2cf-d4b148db8381)

```
eStore/
├── src/
│   ├── apigateways
│   │   ├── eStore.Ocelot.API      # Ocelot API Ağ Geçidi
│   ├── SagaOrchestrator
│   │   ├── eStore.Orchestrator
│   ├── Services
│   │   ├── Customer
│   │   │   ├── eStore.Customer.Data/        # Customer Uygulama için Servisleri ve Komutları
│   │   │   ├── eStore.Customer.Core/        # DDD ile ilgili Varlıklar ve Aggregates
│   │   │   ├── eStore.Customer.Services/    # Veri Erişim Katmanı, CQRS İçin Event Store
│   │   │   ├── eStore.Customer.API/         # Web API Katmanı
│   │   ├── Order
│   │   │   ├── eStore.Order.Data/           # Uygulama Servisleri ve Komutları
│   │   │   ├── eStore.Order.Core/           # DDD ile ilgili Varlıklar ve Aggregates
│   │   │   ├── eStore.Order.Services/       # Veri Erişim Katmanı, CQRS İçin Event Store
│   │   │   ├── eStore.Order.API/            # Web API Katmanı
│   │   ├── Identity
│   │   │   ├── eStore.Identity.Data/        # Uygulama Servisleri ve Komutları
│   │   │   ├── eStore.Identity.Core/        # DDD ile ilgili Varlıklar ve Aggregates
│   │   │   ├── eStore.Identity.Services/    # Veri Erişim Katmanı, CQRS İçin Event Store
│   │   │   ├── eStore.API/                  # Web API Katmanı
│   │   ├── Product
│   │   │   ├── eStore.Product.Data/         # Uygulama Servisleri ve Komutları
│   │   │   ├── eStore.Product.Core/         # DDD ile ilgili Varlıklar ve Aggregates
│   │   │   ├── eStore.Product.Services/     # Veri Erişim Katmanı, CQRS İçin Event Store
│   │   │   ├── eStore.Product.API/          # Web API Katmanı
│   ├── Tools
│   │   ├── eStore.Shared/               # Ortak Kütüphane ve Araçlar
├── tests/
│   ├── eStore.Tests/             # Test Projeleri
└── eStore.sln                     # Visual Studio Solution
```

# 1. The Purposes of This Project
Microservices ile DDD Uygulaması
Sınırlı Bağlamların Doğru Bir Şekilde Ayrılması
Sınırlı Bağlamlar Arasındaki İletişim: Asenkron RabbitMQ ve gRPC ile
Basit CQRS Uygulaması ve Olay Odaklı Mimarinin Örneği
Dağıtık İşlemlerin Yönetilmesi: Sage Deseni ile
Denetlenebilirlik ve Zaman İçi Sorgular için Olay Kaynaklı
En İyi Uygulama ve Tasarım Şablonlarının Kullanılması

# 2. Technologies and Libraries
- ✔️ **[`.NET Core 6`](https://dotnet.microsoft.com/download)**.NET Core 6
- ✔️ **[`EF Core`](https://github.com/dotnet/efcore)** .NET için modern nesne-veritaban eşlemesi. LINQ sorgularını, değişiklik takibini, güncellemeleri ve şema taşınmalarını destekler ve Code First yaklaşımını benimser.
- ✔️ **[`RabbitMQ`](https://masstransit.io/)** - RabbitMQ
- ✔️ **[`FluentValidation`](https://github.com/FluentValidation/FluentValidation)** - .NET için popüler bir doğrulama kütüphanesi, güçlü türden doğrulama kuralları oluşturmak için kullanılır.
- ✔️ **[`Swagger & Swagger UI`](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)** - ASP.NET Core üzerinde oluşturulan API'leri belgelemek için kullanılan Swagger araçları.
- ✔️ **[`Serilog`](https://github.com/serilog/serilog)** - Tamamen yapılandırılabilir olaylarla basit .NET günlükleme.
- ✔️ **[`Ocelot`](https://github.com/ThreeMammals/Ocelot)**  - .NET Core kullanılarak oluşturulan bir API ağ geçidi.
- ✔️ **[`Identity Server 6`](https://duendesoftware.com/products/identityserver)** or **  **[`JSON Web Tokens`](https://jwt.io/)** - Kimlik hizmeti için kimlik sunucusunu uygulayarak yetkilendirme, kimlik doğrulama ve tek oturum açma (SSO) sağlar.
- ✔️ **[`Docker`](https://www.docker.com)**  - Docker ve Konteynerleştirme Hizmetleri


# 3. Structure of services
Her bir servisimiz, temiz bir mimariye sahiptir ve bu mimari dört temel bölümden oluşur:

- **API** - Katmanı: Bu katman, mikroservisi .NET Core Web API üzerinde barındırma görevini üstlenir ve aynı zamanda dokümantasyon için Swagger'ı kullanır.

- **Data** Katmanı: Uygulama katmanı, modülle ilgili kullanım senaryolarının uygulanmasını içerir. İsteklerin işlenmesi bu katmanın sorumluluğundadır. Ayrıca, bu katman kullanım senaryolarını, domain olaylarını, entegrasyon olaylarını ve bunların sözleşmelerini, iç komutları içerir.

- **Core** Katmanı: Domain Katmanı, Domain-Driven Design (DDD) terimleriyle ifade edilen, ilgili Sınırlı Bağlamı temsil eder. Bu katmanda, uygulamanın iş mantığını temsil eden domain varlıkları ve kuralları bulunur.

- **Services** Katmanı: Altyapı katmanı, dışsal bağımlılıklarla iletişim kurmak için kullanılan sekonder adaptörlerin uygulandığı katmandır. Sekonder adaptörler, dış bağımlılıklarla iletişimden sorumludur. Ayrıca, altyapı kodu modülün başlatılmasından, arka planda işlem yapılmasından, veri erişiminden, Olay Otobüsü ile iletişim kurmaktan ve diğer harici bileşenler veya sistemlerle iletişim kurmaktan sorumludur.

Bu düzenleme, her katmanın görevlerini ve işlevlerini daha açık bir şekilde tanımlar ve okuyucuların projenizin yapısını daha iyi anlamalarına yardımcı olur.

# 4. Solution Architect
