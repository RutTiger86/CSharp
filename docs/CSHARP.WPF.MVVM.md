[README 보기](../README.md)

# CSharp.WPF.MVVM - .Net8 Window WPF Client 프로젝트

Wpf 프로젝트로 CommunityToolkit을 통한 MVVM 패턴으로 LoginWindow 및 Client , View 전환 등을 구현합니다. 
View, Viewmodel, Model 을 구현하며 추가적으로 기능 부분은 Service를 생성하며 DI 패턴을 함께 구현합니다. 


## 사용된 NugetPackage

- CommunityToolkit.Mvvm: MVVM 패턴을 지원하는 Microsoft의 툴킷으로, .NET 애플리케이션의 MVVM 구현을 쉽게 만듭니다.
- log4net: .NET 애플리케이션에서 로그 기능을 제공하는 로그 프레임워크로, 다양한 출력 옵션과 유연한 설정을 지원합니다.
- Microsoft.Extensions.DependencyInjection: .NET 애플리케이션에서 종속성 주입(DI)을 쉽게 구현할 수 있게 해주는 라이브러리입니다.
- Microsoft.Extensions.Hosting: 애플리케이션 호스팅과 관련된 인프라 서비스를 제공하며, 콘솔 앱, 웹 호스트 등을 관리합니다.
- Microsoft.Extensions.Hosting.Abstractions: 호스팅 관련 인터페이스와 기본 클래스를 정의하여, 애플리케이션의 호스팅 환경을 추상화합니다.
- Microsoft.Xaml.Behaviors.Wpf: WPF 애플리케이션에서 XAML 기반의 행동을 정의하고 확장할 수 있는 기능을 제공합니다.

## 중점사항

- Model은 데이터 형식에 집중을 하도록 구현합니다.
- ViewModel은 View, Service와 연동되며 데이터를 가공하여 View로 바인딩 표현 되도록 합니다. 
- LoginWindow와 Client간의 연동은 Message 를 통해 진행되며 ViewModel은 바인딩을 통해 구현합니다.
- Repository는 구현되지 않았으며 필요하다면 Service - Repository 패턴이 추가될 수 있습니다. 

## 코딩규칙

해당 Repository 공통 코딩 규칙을 준수합니다.

[CONVENTIONS](CONVENTIONS.md)
