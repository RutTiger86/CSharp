[README 보기](../README.md)

# CSharp.Protobuf, Server, Client  - WindowsService 형태의 Socket 서버 
CSharp.Protobuf 솔루션은 Protocol Buffers와 gRPC를 사용하여 .NET 8 환경에서 서버와 클라이언트를 구현하는 예제입니다.  
이 솔루션은 gRPC 서비스 정의, 서버 구현, 클라이언트 테스트로 구성된 3개의 프로젝트를 포함하며, 빌드 전 자동 코드 생성과 같은 기능을 제공합니다.  

## 사용된 NugetPackage
 - Google.Protobuf : Protocol Buffers 메시지 직렬화/역직렬화 라이브러리.  
 - Google.Protobuf.Tools : Protocol Buffers 컴파일 도구를 제공.  
 - Grpc.Tools : .proto 파일로부터 C# 코드를 생성하는 gRPC 플러그인.  
 - Grpc.AspNetCore : ASP.NET Core 기반의 gRPC 서버를 구현하는 데 사용.  
 - Grpc.Net.Client : HTTP/2를 기반으로 gRPC 클라이언트를 구현하는 라이브러리.  


## 프로젝트 별 중점사항
1. **CSharp.Protobuf**
    - **주요 역할**:  
      .proto 파일을 정의하고 빌드 전 이벤트로 .proto 파일을 C# 코드로 변환하는 핵심 프로젝트.  
    - **주요 특징**:  
      generate_protos.bat을 사용해 .proto 파일에서 자동으로 C# 코드를 생성.  
      메시지와 서비스 네임스페이스를 분리하여 코드 가독성을 향상.  
    - **.proto 파일**:  
      IAPService.proto → 서비스 정의.  
      Purchase.proto, Transaction.proto → 메시지 정의.  
2. **CSharp.Protobuf.Server**  
    - **주요 역할**:  
      gRPC 서버 구현.  
    - **주요 특징**:  
      CSharp.Protobuf 프로젝트를 참조하여 IAPService.IAPServiceBase를 구현.  
      gRPC 서버는 HTTP/2와 HTTPS를 통해 클라이언트 요청을 처리.  
3. **CSharp.Protobuf.Client**  
    - **주요 역할**:  
      gRPC 클라이언트 구현.
    - **주요 특징**:  
      CSharp.Protobuf 프로젝트를 참조하여 IAPService.IAPServiceClient를 사용.  
      서버에 연결하여 MakePurchase, GetPurchaseHistory 등 RPC 메서드를 호출해 동작 테스트.  

## 주요 기술 스택
**gRPC** : HTTP/2 기반의 고성능 원격 프로시저 호출(Remote Procedure Call) 프레임워크.  
**Protocol Buffers (Protobuf)** : 메시지 직렬화 포맷.  
**.NET 8** : 최신 .NET 런타임 환경.  

  
## 설치 및 실행  
1.**프로젝트 빌드**  
  각 프로젝트를 빌드하기 전에 .proto 파일에서 C# 코드를 생성합니다.  
  이 과정은 CSharp.Protobuf 프로젝트의 빌드 전 이벤트로 자동 처리됩니다.

	# CSharp.Protobuf 프로젝트 빌드  
	cd CSharp.Protobuf  
	dotnet buil  


2.**서버 실행**  
  `CSharp.Protobuf.Server` 프로젝트를 실행하여 gRPC 서버를 시작합니다.    

	# CSharp.Protobuf.Server 프로젝트 실행  
	cd CSharp.Protobuf.Server  
	dotnet run


3.**클라이언트 실행**  
  서버가 실행된 후 `CSharp.Protobuf.Client` 프로젝트를 실행하여 RPC 호출을 테스트합니다.

	# CSharp.Protobuf.Client 프로젝트 실행  
	cd CSharp.Protobuf.Client  
	dotnet run

 
## 서비스 흐름
1. **gRPC 서비스 정의**:  
CSharp.Protobuf의 .proto 파일에서 gRPC 서비스를 정의합니다.  
    - IAPService.proto: IAPService의 MakePurchase와 GetPurchaseHistory RPC 메서드를 정의.
    - Purchase.proto, Transaction.proto: 요청 및 응답 메시지를 정의.
2. **서버 구현**:  
CSharp.Protobuf.Server 프로젝트에서 IAPService.IAPServiceBase를 구현한 IAPServiceImpl을 통해 요청 처리 로직을 작성합니다.
3. **클라이언트 호출**:  
CSharp.Protobuf.Client에서 IAPService.IAPServiceClient를 사용하여 서버의 gRPC 메서드를 호출합니다.
이를 통해 서버와의 상호작용을 확인합니다.


## 향후 개선사항  
1. 자동화된 인증 추가  
    - JWT 또는 OAuth2와 같은 인증 방식을 추가하여 보안을 강화.  
2. gRPC-Web 지원  
    - HTTP/1.1 기반 환경(예: 브라우저)에서도 gRPC 서비스를 사용할 수 있도록 확장.  
3. 프로젝트 간 빌드 파이프라인 개선  
    - enerate_protos.bat 대신 MSBuild를 활용하여 .proto 파일 컴파일을 자동화.  
4. 로드 테스트 및 성능 최적화  
    - 다중 클라이언트 요청 시 서버의 안정성과 성능을 측정하고 개선.  
5. Docker 컨테이너화  
    - 서버 및 클라이언트를 Docker 컨테이너로 배포해 이식성을 높이고 운영 환경을 단순화.  

[CONVENTIONS](CONVENTIONS.md)
