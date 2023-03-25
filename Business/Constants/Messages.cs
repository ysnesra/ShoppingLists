using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    //sabit olduğu için newlememek için ->static oluşturuldu
    public static class Messages
    {
        public static string ProductsListed = "Ürünler listelendi";
        public static string ProductAdded = "Ürün eklendi";
        public static string ProductDetail = "Ürünün detay bilgilerini getirdi";
        public static string ProductNameInvalid = "Ürün ismi geçersiz";
        public static string ProductUpdated = "Ürün bilgileri güncellendi";
        public static string ProductDeleted = "Ürün silindi";

        public static string MaintenanceTime = "Sistem bakımda!";

        public static string BrandsListed = "Markalar listelendi";
        public static string BrandAdded = "Marka eklendi";
        public static string BrandDetail = "Markanın detay bilgilerini getirdi";
        public static string BrandNameInvalid = "Marka ismi geçersiz";
        public static string BrandUpdated = "Marka bilgileri güncellendi";
        public static string BrandDeleted = "Marka silindi";

        public static string ImagesListed = "Markalar listelendi";
        public static string ImageAdded = "Marka eklendi";
        public static string ImageDetail = "Markanın detay bilgilerini getirdi";
        public static string ImageUpdated = "Marka bilgileri güncellendi";
        public static string ImageDeleted = "Marka silindi";

        public static string CategoriesListed = "Kategoriler listelendi";
        public static string CategoryAdded = "Kategori eklendi";
        public static string CategoryDetail = "Kategorinin detay bilgilerini getirdi";
        public static string CategoryNameInvalid = "Kategorinin ismi geçersiz";
        public static string CategoryUpdated = "Kategori bilgileri güncellendi";
        public static string CategoryDeleted = "Kategori silindi";

        public static string ProductListListed = "AlışverişListeleri listelendi";
        public static string ProductListAdded = "AlışverişListesi eklendi";
        public static string ProductListDetail = "AlışverişListesinin detay bilgilerini getirdi";
        public static string ProductListNameInvalid = "AlışverişListesinin ismi geçersiz";
        public static string ProductListUpdated = "AlışverişListesinin bilgileri güncellendi";
        public static string ProductListDeleted = "AlışverişListesi silindi";

        public static string ProductListDetailListed = "AlışverişListeleri ayrıntılı listelendi";
        public static string EmailInvalid = "Email adresi geçersiz";
        
        public static string UsersListed = "Kullanıcılar listelendi";
        public static string UserDetail = "Kullanıcının detay bilgilerini getirdi";
        public static string UserNameInvalid = "Kullanıcı ismi geçersiz";
        public static string UserWithCustomerListed = "Kullanıcılarla birlikte Müşteri bilgisinide getirdi";
        public static string UserAdded = "Kullanıcı eklendi";      
        public static string UserUpdated = "Kullanıcı bilgileri güncellendi";
        public static string UserDeleted = "Kullanıcı sistemden silindi";

        public static string NoAdded = "Ekleme işlemi başarısız ";

        public static string EmailAlreadyExists ="Aynı email adresinde zaten kayıt bulunmaktadır.";
        public static string UserIdNoExists="Bu kullanıcının sistemde kayıdı bulunmamaktadır.";
    }
}
