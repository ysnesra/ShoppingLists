using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;

//ProductTest();

//CategoryTest();

//ProductListDetailTest();

//ProductListTest();

//BrandTest();

//UserTest();

//static void ProductTest()
//{
//    //Hangi veri yöntemiyle çalıştığını new leyerek söyleriz 
//    ProductManager productManager = new ProductManager(new EfProductDal());

//    Console.WriteLine("------Ürün Ekleme-------------------");
//    var addedproduct = productManager.Add(new Product { ProductName = "Muz", UnitInStock = 10, BrandId = 1002, CategoryId = 1 });

//    foreach (var product in productManager.GetAll().Data)  //productManagerdeki ürünlerin listesinin içinde tek tek dolaş
//    {
//        Console.WriteLine(product.ProductName);
//    }

//    Console.WriteLine("--------------------------------");

//    foreach (var product in productManager.GetAllByCategoryId(1).Data)
//    {
//        Console.WriteLine(product.ProductName);
//    }

//    Console.WriteLine("--------------------------------");

//    var result2 = productManager.GetProductDetails();
//    if (result2.Success)
//    {
//        foreach (var product in result2.Data)
//        {
//            Console.WriteLine(product.ProductName + " / " + product.CategoryName);
//        }
//    }
//    else
//    {
//        Console.WriteLine(result2.Message);
//    }


//    Console.WriteLine("------Ürün ismine göre ürün detayını getirme--------------------");

//    var result = productManager.GetProductDetailByProductName("elma").Data;
//    Console.WriteLine("Ürünün ismi: {0} /  Kategorisi {1} / Markası {2} / Stoktaki miktarı {3}", result.ProductName, result.CategoryName, result.BrandName, result.UnitInStock);


//}

static void CategoryTest()
{
    CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
    foreach (var category in categoryManager.GetAll().Data)
    {
        Console.WriteLine(category.CategoryName);
    }
    Console.WriteLine("----Kategori ekleme---------");
    var result3 = categoryManager.Add(new Category { CategoryName = "Gıda" });
    if (result3.Success)
    {
        foreach (var category in categoryManager.GetAll().Data)
        {
            Console.WriteLine(category.CategoryName);
        }
        Console.WriteLine(result3.Message);
    }
    else
    {
        Console.WriteLine(result3.Message);
    }
}

static void ProductListTest()
{
    ProductListManager productListManager = new ProductListManager(new EfProductListDal());

    Console.WriteLine("------Mail adresine göre Alışveriş listelerini getirme ------------");
    var productlists = productListManager.GetProductListDetailByEmail("ysn@gmail.com").Data;
    foreach (var product in productlists)
    {
        Console.WriteLine(" AlışverişListesi: {0}/ Oluşturulma Zamanı: {1} / Listedeki Toplam ÜrünMiktarı: {2}", product.ProductListName, product.CreateDate, product.TotalAmount);
    }

    Console.WriteLine("------Alışveriş listesi oluşturma(ekleme) ------------");
    //var result = productListManager.Add(new ProductList
    //{
    //    ProductListName = "HaftasonuİçinAlışVeriş",
    //    UserId = 2,
    //    CreateDate = DateTime.Now
    //});
    //foreach (var item in productListManager.GetAll().Data)
    //{
    //    Console.WriteLine(item.ProductListName);
    //}
}


static void UserTest()
{
    UserManager userManager = new UserManager(new EfUserDal());

    Console.WriteLine("-----Kullanıcı Ekleme-----");
    var addedUser = userManager.Add(new User { FirstName = "Hatice", LastName = "Kutlu", Email = "hatice@gmail.com", Password = "123123123" });

    foreach (var user in userManager.GetAll().Data)
    {
        Console.WriteLine(user.FirstName + " " + user.LastName + " " + user.Email);
    }
    Console.WriteLine("-----Kullanıcıları mail adresine göre listeleme-----");
    var user1 = userManager.GetUserDetailsByEmail("hatice@gmail.com").Data;
    Console.WriteLine(user1.FirstName + " " + user1.LastName + " " + user1.Password);
}