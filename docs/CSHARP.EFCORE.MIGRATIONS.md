[README 보기](../README.md)

# CSharp.Efcore.Migrations - .Net8 DataBase 프로젝트


## 사용된 NugetPackage
- Microsoft.EntityFrameworkCore: Entity Framework Core의 핵심 기능을 제공하는 패키지로, 데이터베이스와의 상호작용을 간단하게 만들어줍니다.
- Microsoft.EntityFrameworkCore.SqlServer: SQL Server 데이터베이스와의 통합을 가능하게 하는 확장 패키지입니다.
- Microsoft.EntityFrameworkCore.Tools: 마이그레이션 및 기타 데이터베이스 작업을 관리할 수 있는 도구를 제공합니다.

## 중점사항
작성 문법은 VisualStudio 패키지 관리자 콘솔 기준으로 작성 

### 최초 Model 생성 후 InitailCreateMigration 추가 
```
Add-Migration InitialCreate
```
### 개발 환경변경
```
$env:ASPNETCORE_ENVIRONMENT="Development"
```
### Live 환경변경
```
$env:ASPNETCORE_ENVIRONMENT="Production"
```
### 데이터베이스 업데이트
```
Update-Database
```


[EntityFramework core 참조 사이트](https://learn.microsoft.com/ko-kr/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli)
## 코딩규칙
해당 Repository 공통 코딩 규칙을 준수합니다.

[CONVENTIONS](CONVENTIONS.md)
