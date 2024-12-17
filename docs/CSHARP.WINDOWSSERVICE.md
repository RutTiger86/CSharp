[README 보기](../README.md)

# CSharp.WindowsService - WindowsService 형태의 Socket 서버 
소켓 통신을 기반으로 클라이언트와의 네트워크 연결을 처리하는 Windows 서비스입니다.  
Windows 서비스로 실행되며, 클라이언트와의 TCP 연결을 수락하고 데이터를 주고받을 수 있습니다.  
비동기 소켓 통신과 Dependency Injection(DI) 구조를 활용하여 확장 가능하고 유지보수가 용이한 서비스를 구현했습니다.  

## 사용된 NugetPackage

 - log4net :  로그 출력을 다양한 출력 대상에 기록하는 도구. 애플리케이션의 문제를 디버깅하거나 로그를 분석할 때 유용합니다.  
 - Microsoft.Extensions.DependencyInjection : 의존성 주입(Dependency Injection)을 구현하는 .NET용 기본 라이브러리입니다.  
 - Microsoft.Extensions.Hosting : 애플리케이션의 호스팅 및 시작 인프라를 제공합니다.  
 - Microsoft.Extensions.Hosting.WindowsServices : Windows 서비스용 .NET 호스팅 인프라를 제공한다.  
 - Microsoft.Extensions.Logging.Log4Net.AspNetCore : ASP.NET Core 애플리케이션에서 log4net을 Microsoft Extensions 로깅 시스템과 함께 사용할 수 있게 해줍니다.  
 - System.ServiceProcess.ServiceController : Windows 서비스를 제어하고 관리하는 클래스를 제공합니다.  


## 중점사항
 - **Windows 서비스로 동작**  
   서비스로 등록 후 백그라운드에서 실행되며, 명령어 기반 실행 및 디버그 모드를 지원합니다.
 - **TCP 소켓 통신**  
   클라이언트와의 연결을 수락하고 메시지를 주고받습니다.
   각 클라이언트는 비동기로 처리됩니다.  
 - **DI 기반 설계**  
   ISocketService 인터페이스를 사용해 서버 구현을 추상화.  
   Microsoft.Extensions.DependencyInjection으로 확장 가능.  
 - **안전한 서비스 종료**  
   서비스 종료 요청(sc stop) 시 **CancellationToken**을 통해 안전하게 연결을 정리.  
   모든 클라이언트 연결과 서버 리소스를 올바르게 해제.  
 - **로깅 시스템**  
   log4net 기반으로 파일 및 콘솔 로그를 기록.  
   서비스 상태, 클라이언트 연결/해제, 예외 등을 로깅하여 문제를 추적.  

## 주요 기술 스택
**C#** (.NET 8.0)  
**Windows Service** (Microsoft.Extensions.Hosting)  
**TCP 소켓 통신**  
**Dependency Injection (DI)**: Microsoft.Extensions.DependencyInjection  
**Log4Net**: 로깅 시스템

  
##설치 및 실행  
1.프로젝트 빌드  
  이 프로젝트를 Visual Studio 또는 .NET CLI로 빌드합니다.

	dotnet build -c Release

  빌드 결과는 bin\Release\net8.0 디렉터리에 생성됩니다.

2.서비스 설치  
  Windows 서비스로 등록하려면 sc.exe 명령어를 사용하거나 프로젝트에서 제공하는 옵션을 사용합니다.  
  2-1.명령어  
   - 서비스 설치  

	CSharp.WindowsService.exe -install {ServiceName}

   - 서비스 시작

	sc start MyService

   - 서비스 중지

	sc stop MyService

   - 서비스 제거
    (별도 명령 제공하지 않음, sc delete 명령어 사용)

	sc delete MyService

3.디버그 모드 실행  
  디버그 모드로 실행하여 콘솔에서 서비스를 테스트할 수 있습니다.

	CSharp.WindowsService.exe -debug

##설정 파일  
  서버 설정값은 Server.config 파일에서 관리됩니다.
  **설정 파일 구조**

	<ServerConfig>  
        <ServerIp>127.0.0.1</ServerIp>  
        <Port>8080</Port>  
        <MaxClients>10</MaxClients>  
        <MaxBytesPerRequest>4096</MaxBytesPerRequest>  
    </ServerConfig>  


  **주요 설정 항목**  
 - `ServerIp`: 서버가 바인딩할 IP 주소.  
 - `Port`: 서버가 사용할 포트 번호.   
 - `MaxClients`: 동시 접속 가능한 최대 클라이언트 수.  
 - `MaxBytesPerRequest`: 클라이언트가 한 번에 전송할 수 있는 최대 바이트 크기.  
 
  **서비스 흐름**
1. 서비스 실행  
Windows 서비스가 시작되면 SocketServer의 ExecuteAsync가 호출됩니다.
2. 서버 시작  
ServerService가 실행되며, 지정된 IP와 포트에서 클라이언트 연결을 대기합니다.
3. 클라이언트 연결 처리  
클라이언트가 연결되면 IP와 포트를 로그로 기록.  
메시지를 주고받으며 클라이언트 요청에 응답(Echo)합니다.  
4. 서비스 종료  
서비스 종료 요청(sc stop) 시, 모든 클라이언트 연결과 소켓 리소스를 정리한 후 안전하게 종료됩니다.  


**향후 개선 사항**
1. 암호화 통신  
TLS/SSL을 사용한 데이터 전송 보안.
3. 로드밸런싱 지원  
다중 서버를 지원하도록 구조 확장.
4. 설정 관리 개선  
JSON 형식의 설정 파일(appsettings.json)로 변경하여 관리 용이성 향상.  

[CONVENTIONS](CONVENTIONS.md)
