[README ����](../README.md)

# Repository ���� �ڵ� ��Ģ 
�ش� Repository�� ���� ��ð� ������ ������ �ڵ� ��Ģ�� ����մϴ�.

## Ŭ���� �� ���ӽ����̽�
- Ŭ�������� 'UpperCamelCase'�� ����Ͽ� �ۼ��մϴ�. 
- ���ӽ����̽��� ������Ʈ ������ �ݿ��Ͽ� �ۼ��մϴ�. 
```csharp
namespace CSharp.Authorization.Session.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        // �ڵ� ����
    }
}
```
## ���� �� �ʵ� 
 - ���� ������ Ŭ���� �ʵ�� 'LowerCamelCase'�� ����մϴ�.  
 - ����� 'UpperCamelCase'�� ����մϴ�.

```csharp
private ILogger<AuthorizationController> logger;
public AuthorizationService(ILogger<AuthorizationController> logger)
{
    this.logger = logger;
}
```

## �޼���
 - �޼��� ���� 'UpperCamelCase'�� ����Ͽ� �ۼ��մϴ�. 
 - �޼��� ������ �ǹ��ľ��� �����ϵ��� �ϸ� ���� �ּ��� �����մϴ�. 
 
## ���ξ� �߰� 
- interface�� ��� �տ� i�� �Ӵϴ�. 
- ���� ��� interface�� �ش� Class ���� ��ġ �մϴ�. 

## ���� ��Ī 
- Class ������ Class ��� ���Ͻ� �մϴ�. 
- md ������ ��� ��� �빮�ڷ� �մϴ�.

## ��Ÿ 
- ���ʿ��� 'using'���� ���� �����մϴ�. 