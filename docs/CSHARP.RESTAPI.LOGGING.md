[README 보기](../README.md)

# CSharp.RestAPI.Logging - .Net8 RestAPI 프로젝트
RestAPI 프로젝트로 Middleware를 통한 Logging 방식을 구현합니다. 
IApplicationBuilder에 확장 메서드를 추가하여 LoggingMiddleware를 생성합니다. 
LoggingMiddleware는 모든 들어오는 호출에대하여 Requst와 결과를 기록합니다.

## 사용된 NugetPackage
- Microsoft.Extensions.Logging.Log4Net.AspNetCore : ASP.NET Core 애플리케이션에서 Log4Net을 사용하여 로깅을 지원하는 확장 라이브러리입니다.
- Swashbuckle.AspNetCore : ASP.NET Core 애플리케이션에서 Swagger(OpenAPI) 문서를 자동으로 생성하고 UI를 제공합니다.

## 중점사항
- LoggingMiddleware에는 특정 Request 인자 값에 대해서는 Loging을 하지 않습니다. 이러한 기능은 비밀번호 및 민감정보를 보호하는데 사용됩니다. 
- LoggingMiddleware는 호출 시간과 응답 시기를 기록하여 응답 속도에 대한 정보도 기록합니다. 
- Response 값이 설정된 값보다 길경우 길이만 기록하며 값은 기록하지 않습니다. 

## 코딩규칙
해당 Repository 공통 코딩 규칙을 준수합니다.

[CONVENTIONS](CONVENTIONS.md)
