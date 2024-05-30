[README 보기](../README.md)

#CSharp.Authorization.OAuth - WPF Client OAuth 프로젝트
WPF 프로젝트로 Google OAuth 인증방식을 구현합니다. 
기본적으로 MVVM(Model-View-Viewmodel) 패턴과 DI(Dependency Injection) 패턴을 적용합니다. 
민감한 정보(clientid, clientsecret)는 App.config 에 기록하고 사용하게 되어 있습니다.
해당 정보는 Repository에 기록하지 않으며 해당 소스를 사용기 위해서는 별도로 등록 사용하십시오.
**민감정보 저장시 App.config 가 아닌 별도의 공간에 저장 사용하길 권장합니다.**

## 사용된 NugetPackage
- log4net : Apache Logging Services 프로젝트의 일환으로, 개발자가 애플리케이션에 로깅을 삽입하여 모니터링 및 디버깅할 수 있도록 설계되었습니다.
- CommunityToolkit.Mvvm :.NET Community Toolkit의 일부로, 현대적이고 빠르며 모듈식인 MVVM(Model-View-ViewModel) 라이브러리를 제공합니다.
- Microsoft.AspNetCore.Cryptography.KeyDerivation : 암호를 안전하게 해시하는 데 사용되는 키 파생을 위한 API를 제공합니다.
- Microsoft.Extensions.DependencyInjection :  .NET Core 및 .NET 애플리케이션과 호환되는 종속성 주입(DI) 프레임워크를 제공합니다.
- Microsoft.Extensions.Hosting : ASP.NET Core 웹 앱, 콘솔 앱, 백그라운드 서비스 등 애플리케이션을 호스팅하기 위한 공통 인터페이스와 클래스를 제공합니다.
- Microsoft.Extensions.Hosting.Abstractions : 호스팅 API를 위한 추상화를 제공하여 더 유연하고 테스트 가능한 코드를 작성할 수 있도록 합니다.
- Microsoft.Xaml.Behaviors.Wpf : WPF 애플리케이션에서 동작을 구현하기 위한 라이브러리로, 재사용 가능한 기능을 UI 요소에 첨부할 수 있도록 합니다.

## 중점사항
- OAuth 소스는 Google에서 제공하는 소스에서 크게 수정되지 않은 상태로 사용 합니다. 
- BaseModel을 두고 사용하며 메모리 누수에 대비해 표준 IDisposable 패턴을 구현 합니다.
- View간의 통신의 CommunityToolkit.Mvvm의 Message 기능을 사용하여 통신합니다. 
- Modle, View, Viewmodel 이외에도 Service 를 구현하여 기능구현은 Service에 구현함을 지향합니다. 

## 코딩규칙
해당 Repository 공통 코딩 규칙을 준수합니다.
[CONVENTIONS](CONVENTIONS.md)
