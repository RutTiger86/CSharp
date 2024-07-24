[README 보기](../README.md)

# CSharp.Authorization.Session - .Net8 RestAPI 프로젝트

Rest API 프로젝트로 세션 인증방식을 구현합니다.
기본적으로 DI(Dependency Injection) 패턴을 적용하며 세션의 인증 정보를 검증하도록합니다.
각 세션에 암호화된 SessinKey를 부여하고 해당 Key 검증을 통한 인증을 합니다.

**다중 API 서버에 대한 Redis 분산 캐시 설정은 진행되지 않았으며 필요시 직접 구현해야 합니다.**

## 사용된 NugetPackage

- Swashbuckle.AspNetCore : ASP.NET Core 애플리케이션에서 Swagger(OpenAPI) 문서를 자동으로 생성하고 UI를 제공합니다.

## 중점사항

- AuthorizeAttribute를 통해 세션에 암호화된 SessionKey를 추가합니다.
- SessionKey 에는 인증된 ID와 만료시간이 존재합니다.
- AllowPublicAttribute를 가지지 않은 모든 호출에대하여 인증을 진행합니다.
- AllowPublicAttribute의 함수는 최초 SessionKey 발급시 사용됩니다.

## 코딩규칙

해당 Repository 공통 코딩 규칙을 준수합니다.

[CONVENTIONS](CONVENTIONS.md)
