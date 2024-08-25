[README 보기](../README.md)

# CSharp.SocketNetwork - .Net8 Console Socket 프로젝트

Console 프로젝트로 간단한 Socket 통신을 구현합니다.
Main에서 명령어를 입력 받아 Server, Client를 생성, 조회, 정지 합니다. 
Main에서 Send 명령어를 통해 Client 를 정하고 메세지를 보낸다.
Server의 경우 받은 Messgae를 되돌려 보낸다. 

## 사용된 NugetPackage

- Microsoft.Extensions.DependencyInjection: .NET 애플리케이션에서 종속성 주입(DI)을 쉽게 구현할 수 있게 해주는 라이브러리입니다.

## 중점사항
- Server는 최대한 붙을 수 있는 Client 수를 정할수 있다.
- Client는 보내는 메세지 맨앞에 길이를 전달하며 Server는 해당 길이만큼 받는다. 

## 코딩규칙
해당 Repository 공통 코딩 규칙을 준수합니다.

[CONVENTIONS](CONVENTIONS.md)
