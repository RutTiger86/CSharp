[README ����](../README.md)

#CSharp.Authorization.OAuth - WPF Client OAuth ������Ʈ
WPF ������Ʈ�� Google OAuth ��������� �����մϴ�. 
�⺻������ MVVM(Model-View-Viewmodel) ���ϰ� DI(Dependency Injection) ������ �����մϴ�. 
�ΰ��� ����(clientid, clientsecret)�� App.config �� ����ϰ� ����ϰ� �Ǿ� �ֽ��ϴ�.
�ش� ������ Repository�� ������� ������ �ش� �ҽ��� ���� ���ؼ��� ������ ��� ����Ͻʽÿ�.
**�ΰ����� ����� App.config �� �ƴ� ������ ������ ���� ����ϱ� �����մϴ�.**

## ���� NugetPackage
- log4net : Apache Logging Services ������Ʈ�� ��ȯ����, �����ڰ� ���ø����̼ǿ� �α��� �����Ͽ� ����͸� �� ������� �� �ֵ��� ����Ǿ����ϴ�.
- CommunityToolkit.Mvvm :.NET Community Toolkit�� �Ϻη�, �������̰� ������ ������ MVVM(Model-View-ViewModel) ���̺귯���� �����մϴ�.
- Microsoft.AspNetCore.Cryptography.KeyDerivation : ��ȣ�� �����ϰ� �ؽ��ϴ� �� ���Ǵ� Ű �Ļ��� ���� API�� �����մϴ�.
- Microsoft.Extensions.DependencyInjection :  .NET Core �� .NET ���ø����̼ǰ� ȣȯ�Ǵ� ���Ӽ� ����(DI) �����ӿ�ũ�� �����մϴ�.
- Microsoft.Extensions.Hosting : ASP.NET Core �� ��, �ܼ� ��, ��׶��� ���� �� ���ø����̼��� ȣ�����ϱ� ���� ���� �������̽��� Ŭ������ �����մϴ�.
- Microsoft.Extensions.Hosting.Abstractions : ȣ���� API�� ���� �߻�ȭ�� �����Ͽ� �� �����ϰ� �׽�Ʈ ������ �ڵ带 �ۼ��� �� �ֵ��� �մϴ�.
- Microsoft.Xaml.Behaviors.Wpf : WPF ���ø����̼ǿ��� ������ �����ϱ� ���� ���̺귯����, ���� ������ ����� UI ��ҿ� ÷���� �� �ֵ��� �մϴ�.

## ��������
- OAuth �ҽ��� Google���� �����ϴ� �ҽ����� ũ�� �������� ���� ���·� ��� �մϴ�. 
- BaseModel�� �ΰ� ����ϸ� �޸� ������ ����� ǥ�� IDisposable ������ ���� �մϴ�.
- View���� ����� CommunityToolkit.Mvvm�� Message ����� ����Ͽ� ����մϴ�. 
- Modle, View, Viewmodel �̿ܿ��� Service �� �����Ͽ� ��ɱ����� Service�� �������� �����մϴ�. 

## �ڵ���Ģ
�ش� Repository ���� �ڵ� ��Ģ�� �ؼ��մϴ�.
[CONVENTIONS](CONVENTIONS.md)
