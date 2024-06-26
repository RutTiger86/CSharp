[README 보기](../README.md)

# CSharp.RestAPI.Repository - .Net8 RestAPI 프로젝트
RestAPI 프로젝트로 Repository 패턴 방식을 구현합니다. 
Controller/Service/Repository로  Category와 해당 Category를 가진 Product용 RestAPI를 구현합니다. 
Controller 는 인자값의 인증 및 검사를 진행하며 Service는 데이터 프로세스를 구현합니다.
Repository는 데이터의 검색 및 저장 을 구현합니다.
Repository에 ORM 사용을 권장하나 예제에서는 DataSet을 통한 데이터 예제를 사용합니다. 

## 사용된 NugetPackage
- Swashbuckle.AspNetCore : ASP.NET Core 애플리케이션에서 Swagger(OpenAPI) 문서를 자동으로 생성하고 UI를 제공합니다.

## 중점사항
- Repository는 DataContext에서 Linq를 통한 검색 데이터 생성만 진행합니다. 
- Service의 경우 각 Repository를 통해 데이터를 받아 Response 값을 생성 Controller에게 전달합니다. 
- Contorller 는 Request에 대한 검증 이후 Service를 통해 전달받은 Response를 반환합니다. 
- 모든 Controller,Service, Repository는 각각의 BaseClass를 상속 받습니다. 
- Controller와 Service에 대하여 UnitTest를 작성합니다.(..src/tests/CSharp.RestAPI.RepositoryTests)


## 코딩규칙
해당 Repository 공통 코딩 규칙을 준수합니다.

[CONVENTIONS](CONVENTIONS.md)
