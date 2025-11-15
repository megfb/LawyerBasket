# LawyerBasket.Gateway.Api

Ocelot Gateway kullanarak tüm mikro servis isteklerini merkezi olarak yöneten API Gateway projesi.

## Özellikler

- **Ocelot Gateway**: Tüm mikro servisler için merkezi routing
- **HTTPS**: Tüm iletişim HTTPS üzerinden
- **API Composition Pattern**: Controller bazlı aggregation desteği
- **CORS**: Angular uygulaması için CORS yapılandırması

## Servisler

### AuthService
- **Port**: 7299
- **Base Path**: `/api/auth`, `/api/account`, `/api/role`
- **Endpoints**: Register, Login, ChangePassword, DeleteUser, Role CRUD

### ProfileService
- **Port**: 7141
- **Base Path**: `/api/userprofile`, `/api/lawyerprofile`, `/api/academy`, vb.
- **Endpoints**: UserProfile, LawyerProfile, Academy, Address, Certificate, Contact, Experience, vb. CRUD işlemleri

### PostService
- **Port**: 7254
- **Base Path**: `/api/post`, `/api/comment`, `/api/likes`
- **Endpoints**: Post, Comment, Likes CRUD işlemleri

## Gateway Port

- **HTTPS**: 7001
- **HTTP**: 5001

## Route Yapısı

Gateway'den gelen istekler aşağıdaki şekilde downstream servislere yönlendirilir:

- `/api/auth/*` → AuthService (port 7299)
- `/api/profile/*` → ProfileService (port 7141)
- `/api/userprofile/*` → ProfileService (port 7141)
- `/api/post/*` → PostService (port 7254)
- `/api/comment/*` → PostService (port 7254)
- `/api/likes/*` → PostService (port 7254)

## Aggregation Controller

### UserProfileWithPostsController

API Composition Pattern kullanarak UserProfile ve Post verilerini birleştiren örnek controller.

**Endpoint**: `GET /api/UserProfileWithPosts/GetUserProfileWithPosts/{userId}`

Bu endpoint:
1. ProfileService'den kullanıcı profil bilgilerini çeker
2. PostService'den tüm postları çeker
3. Kullanıcıya ait postları filtreler
4. Birleştirilmiş sonucu döner

## Kullanım

1. Tüm mikro servislerin çalıştığından emin olun
2. Gateway projesini çalıştırın
3. Gateway üzerinden tüm API isteklerini yapın

## Örnek İstekler

### AuthService üzerinden Login
```
POST https://localhost:7001/api/auth/login
```

### ProfileService üzerinden UserProfile Get
```
GET https://localhost:7001/api/userprofile/GetUserProfile/{id}
```

### PostService üzerinden Posts Get
```
GET https://localhost:7001/api/post/GetPosts
```

### Aggregation Endpoint
```
GET https://localhost:7001/api/UserProfileWithPosts/GetUserProfileWithPosts/{userId}
```

