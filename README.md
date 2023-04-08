# ShoppingLists
.Net Mvc Core da Alışveriş Listesi projesi; alışverişe çıkmadan önce alınması planlanan ürünlerin listesinin oluşturulup takibinin yapılmasını sağlar
Proje techcareer.net BootCampında bitirme ödevi olarak verildi.

Kullanılan Teknolojiler:

	 * .NET Core 6.0
	  
	 * Asp.NET for Restful Api
	  
	 * MsSql
	  
	 * Entity FrameWork Core 6.0.13 
	  
	 * AutoFac
	  
	 * FluentValidation

Kullanılan Teknikler:

       * n-tier Architecture mimari yaklaşımı
	
       * Cookie Authentication
       
       * AOP Yapısı
	
       * IoC
	
       * Microsoft Built In Dependency Resolver
	

Projenin amacı, kullanıcıların alışveriş süreçlerini kolaylaştırmak adına almayı planladıkları ürünlerin listelerini oluşturabilmesi ve bu listelerin takiplerini yapabilmesidir.

Uygulamanın kullanılabilmesi için üye olmak gerekmektedir.
	• Kullanıcılar sisteme kayıtlı oldukları bilgilerle giriş yaptıktan sonra işlevsellikleri kullanabileceklerdir.
	• Kullanıcılar sisteme giriş yaptıktan sonra oluşturdukları alışveriş listelerini göreceklerdir. Buradan istedikleri listeyi seçip ürün ekleme ekrana geçebileceklerdir.
	• Ürün ekleyebilmeleri için listelenen ürünlerden istediklerine tıklayıp beklenen bilgileri girecek ve ekle diyeceklerdir.
	• Ürünlerin içerisinde ismiyle arama yapılabilecektir. İstenirse kategorisine göre de filtrelenecektir.
	• Kullanıcılar listeleri sadece ürün ekleyerek değil, ürün kaldırarak da güncelleyebileceklerdir.
	• Kullanıcı bir liste için “Alışverişe Çıkıyorum” seçeneğini işaretlediğinde artık o listeye ürün ekleyemeyecektir.
	• Kullanıcı ürünleri aldıkça liste üzerinden ilgili ürünü seçip “Aldım” diye işaretleyecektir. Liste için “Alışveriş Tamamlandı” seçeneğini işaretlediğinde liste müdaheleye açık hale gelecektir.
	• Sistemde yer alan ürünleri bilgileriyle beraber sistem yöneticisi ekleyecektir.

	----------------------------------

1-Katmanlar oluşturuldu.

2-EntityFramework kütüphanesi DataAccess ve Core katmanlarına yüklendi.

3-ShoppingListContext.cs bağlantı clası DataAccessde EntityFramework klasörüne oluşturuldu.

4-Core Katmanına--> IEntity,IDto,
	                IEntityRepository (CRUD işlemelerinin generic olrak oluşturuldu)
EfEntityRepositoryBase ((TEntity,TContext) handi tablo, hangi veritabanı verillirse ona göre CRUD işlemlerini yapacak base sınıfı oluşturuldu)

5-Entities altında DTOs klasörüne Joinlediğim tabloları oluşturdum

6-Core katmanında Results yapılandırması yapıldı. Business sınıflarını bu yapıya göre refactor edildi (kodu iyileştirme) 
  *Core->Utilities->Result klasörüne Success ve Error olma durumları ayrı ayrı yazıldı 
  *Business->Constants klasörü oluşturuldu.Proje sabitlerini burada tutacağım->Messages.cs classında mesajlarımız tutulacak
  
7-    
    *.FluentValidation desteği eklendi.
	   Kullanıcı adı boş geçilemez,Password alanı en az sekiz karakter, en az bir harf ve bir sayı içermelidir... gibi kurallar verildi.
	*.Core katmanına CrossCuttingConcerns klasörüne ValidationTool oluşturuldu.
	*.AOP yapısı için Autofac desteği eklendi.
	   Arka planda otomatik bağlantıları yapacak.Autofac bir IoC Containerdır.
	   
    *.AOP (Aspect Oriented Programming) desteği eklendi.Intercepter(arayaGirme) alt yapısını oluşturuldu.
	*.Validation; Aspect(AOP) yapıya taşındı.(Autofac kulanılarak)
      ValidationAspect  
	*!!! Burada client-side tarafta hataları gösteremeyince;
	     Business-> Extentions ->DependecyEnjection.cs clasında "AddBusinessService" isminde extention methot oluşturuldu.Ve program.cs ye kullanağı service "builder.Services.AddBusinessService()" eklendi.
    Burada hem Autofac hem ActionFilter(.Net'in kendi DependencyEnjection özellliği) yapısı aynı anda çalışır

8-Kullanıcı CRUD işlemleri yapıldı.(List,AddUserDto,EditUserDto,DeleteUserDto...)
  Kullanıcı Login,Register,Profile sayfaları yapıldı

9-Login sayfasında Role kontrolü CookieAuthentication ile yapıldı.
   --AdminController'a  [Authorize(Roles ="admin")] ___admin olanalar girsin
   --UserController'ın "Logout" ve "Profile" actionlarına yetkisi varsa(login olduysa) erişebilsin
   --Layout.cshtml kısmındada admin kişinin göreceği paneller ayarlandı

10-Ürünlere resim eklendi.
   ProductController da Create actionında ürün eklerken IFormFile tipinde file parametresiyle dosya ekleme işlemide eklendi. 

11-Kullanıcının; Alışveriş Listelerini listeleme, ürün ekleme ve ürün silme işlemleri yapıldı.ProductListController da ;
   --Login olan kullanıcının AlışverişListesini gösterme..... ProductListByUserId() actionı
   --AlışVerişListesi içindeki ürünleri gösterme............. ProductListShowInside(int productListId)
   --AlışVerişListesinin içine Ürünlistesinden ürün ekleme ve silme.... ProductAdd(int productListId,int productId), ProductDeleteFromProductList(int productListId,int productId)
       Hangi kullanıcının hangi alışverişlistesine hangi ürün eklenecek 3 parametre alır(userId,productListId,productId)
       ProductListDetails tablosu ara tablo olduğu için entity sınıfı yok.EntityFramework'den (context. deyip) erişemediğim için; execute SqlRaw kullanarak direk veritabanına erişip eklendi

12-Kullanıcı alışverişe çıkarken “Alışverişe çıkıyorum” seçeneğini seçerse listeye ürün ekleyememelidir.
   ProductListByUserId.cshtml view sayfasında checkbox eklendi. Checkbox ın onclick olması ajax post işlemi ile gerçekleştirildi.

13-Alışveriş listesinde alınan ürünlerin karşısına "Aldım" seçeneğini işaretlenerek tik işareti konmak isteniyor.
   Bunun için; 'Aldım' durumu veritabanında ProductListDetails tablosunda tutuluyor.
   ProductListShowInside.cshtml sayfasında checkbox oluşturulup, ajax-post işlemi gerçekleştirildi.Post ederken; hangi alışverişlistesi(productListId) hangi ürün(productId) ve checked de tuttuğu değerde post edildi. 
   
14-Kullanıcının Yeni AlışVeriş Listesi ekleme işlemi yapıldı.Hangi kullancının alışverişlistesi olduğunuda tutmak gerektiğinden claimde tutulan userId kullanıldı.

---------
BusinessRules Refactoring

15-Managerlarda iş kurallarını metotların içine yazmışdık.İş kuralları arttıkça karışmaması için "İş  Parçacıkları Metotları" oluşturuldu.
   Core katmanına-> Utilities-> Business -> "BuisnessRules.cs" clası oluşturuldu."Run" metotu ile birden fazla kural verileceği düşünülerek params kullanılarak iş kuralları başarılı ise null döncek şekilde ayarlandı.
  UserManager'da
    ** "CheckIfEmailExists" metotuyla "Aynı mail adresinden kayıt varsa eklemesin iş kuralı Parçacığı" oluşturup ayrı yazıyoruz
	** "CheckIfUserIdExists" metotuyla "Böyle bir kullanıcı sistemde kayıtlı mı iş kuralı Parçacığı" oluşturup ayrı yazıyoruz
    "AddUserDto", "DeleteUserDto","EditUserDto "metotları iş kurallarını Run ile çalıştırlıp kontrolü sağlanıyor.
 
