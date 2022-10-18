# Basket.API

## Kullanim

"Case Study" olarak istenen, sepet endpoint'lerini kullanabilmek icin asagidaki komutu calistirin.

```bash
docker-compose up -d --build
```
Komutu calistirdiktan sonra 3 adet container ayaklanacak. Sonrasinda "https://localhost:"PORT"/swagger/index.html" 'a giderek swagger uzerinden endpoint'leri test edebilirsiniz.

## Teknolojiler
- .Net Core 6 kullanilarak gelistirildi.
- "Basket" redis uzerinde tutuldu.
- Veritabani olarak mongodb kullanildi.

"PlaceOrder" endpointi ile de redis'de bulunan "Basket" mongodb'e siparis olusturuldugu dusunulerek kaydedildi.

Authentication dusunulmeden, Cookie ile kullanici bilgisi tutuldu.
