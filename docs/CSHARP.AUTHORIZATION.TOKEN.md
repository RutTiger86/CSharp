[README 보기](../README.md)

# CSharp.Authorization.Token - .Net8 RestAPI 프로젝트
Rest API 프로젝트로 Token 인증방식을 구현합니다. 
기본적으로  DI(Dependency Injection) 패턴을 적용하며 세션의 인증 정보를 검증하도록합니다. 
연결자에게 AccessToken과 RefashToken을 제공하며 AccessToken은 수명이 짧게 제공됩니다. 
AccessToken을 통해 api를 호출하며 AccessToken이 인증된 호출에 대해서만 정상동작합니다. 
AccessToken이 만료된경우 RefashToken을 통해 새로운 AccessToekn을 발급 받을 수 있습니다.


## 사용된 NugetPackage
- Microsoft.AspNetCore.Authentication.JwtBearer : ASP.NET Core 애플리케이션에서 JWT Bearer 토큰 인증을 지원합니다.
- Microsoft.AspNetCore.Authentication.Negotiate : Windows 인증 및 Kerberos 인증을 지원하는 ASP.NET Core 미들웨어입니다.
- Microsoft.IdentityModel.JsonWebTokens : JSON Web Token(JWT) 처리를 위한 라이브러리입니다.
- Microsoft.IdentityModel.Tokens : 다양한 암호화 알고리즘 및 토큰 유효성 검사를 지원하는 라이브러리입니다.
- Swashbuckle.AspNetCore : ASP.NET Core 애플리케이션에서 Swagger(OpenAPI) 문서를 자동으로 생성하고 UI를 제공합니다.


## 중점사항
- LoggingMiddleware를 통해 인증 과정을 로깅합니다. 
- AccessToken의 Claim 의 iis의 경우 JwtRegisteredClaimNames의 Bug 이슈로 사용하지 않습니다.
- RefashToken은 GUID 로 교체 가능하며 GUID 설정시 영구 사용가능합니다. 
- 현 프로젝트에서는 RefashToken의 경우 유한하며 만료시 갱신이 아닌 신규 발급 받아야 합니다. 

## 코딩규칙
해당 Repository 공통 코딩 규칙을 준수합니다.

[CONVENTIONS](CONVENTIONS.md)
