[README 보기](../README.md)

# Repository 공통 코딩 규칙 
해당 Repository는 별도 명시가 없는한 본문의 코딩 규칙을 사용합니다.

## 클래스 및 네임스페이스
- 클래스명은 'UpperCamelCase'를 사용하여 작성합니다. 
- 네임스페이스는 프로젝트 구조를 반영하여 작성합니다. 
```csharp
namespace CSharp.Authorization.Session.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        // 코드 내용
    }
}
```
## 변수 및 필드 
 - 로컬 변수와 클래스 필드는 'LowerCamelCase'를 사용합니다.  
 - 상수는 'UpperCamelCase'를 사용합니다.

```csharp
private ILogger<AuthorizationController> logger;
public AuthorizationService(ILogger<AuthorizationController> logger)
{
    this.logger = logger;
}
```

## 메서드
 - 메서드 명은 'UpperCamelCase'를 사용하여 작성합니다. 
 - 메서드 명으로 의미파악이 가능하도록 하며 설명 주석은 지양합니다. 
 
## 접두어 추가 
- interface의 경우 앞에 i를 둡니다. 
- 단일 사용 interface는 해당 Class 위에 배치 합니다. 

## 파일 명칭 
- Class 파일은 Class 명과 동일시 합니다. 
- md 파일의 경우 모두 대문자로 합니다.

## 기타 
- 불필요한 'using'문은 필히 제거합니다. 