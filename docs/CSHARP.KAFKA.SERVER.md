[README 보기](../README.md)

# CSharp.Kafka.Server - Kafka TestServer 프로젝트

Console Client 로 Server를 구현하고 Kafka를 통해 통신하는 예제 프로젝트 입니다.
Kafka 구축 및 설정 방법 까지 기술 합니다.

## 사용된 NugetPackage

- Confluent.Kafka : .NET 애플리케이션에서 Kafka 클러스터와의 상호작용을 간단하게 할 수 있도록 다양한 기능을 제공합니다.

# Kafka Docker Compose

Docker Compose를 사용하여 Kafka와 Zookeeper를 설정하는 방법을 설명합니다.

## Docker Compose 설정 내용

./docker/kafka 폴더내의 `docker-compose.yml` 을 사용 합니다.

### Zookeeper 서비스

- **image**: `bitnami/zookeeper:latest` (최신 버전의 Zookeeper 이미지를 사용합니다)
- **ports**: `2181:2181` (호스트의 2181 포트를 컨테이너의 2181 포트로 매핑합니다)
- **environment**:
  - `ALLOW_ANONYMOUS_LOGIN`: 익명 로그인을 설정합니다 (`yes`로 설정하면 익명 로그인이 허용됩니다)

### Kafka 서비스

- **image**: `bitnami/kafka:latest` (최신 버전의 Kafka 이미지를 사용합니다)
- **ports**: `9092:9092` (호스트의 9092 포트를 컨테이너의 9092 포트로 매핑합니다)
- **environment**:
  - `KAFKA_BROKER_ID`: 브로커 ID를 정의합니다. 지정하지 않으면 자동으로 지정되지만 추적이 어려울 수 있습니다.
  - `KAFKA_ADVERTISED_LISTENERS`: Kafka 브로커가 광고할 리스너 주소를 지정합니다. 내부(INSIDE)와 외부(OUTSIDE) 주소를 지정합니다.
  - `KAFKA_LISTENER_SECURITY_PROTOCOL_MAP`: 리스너 이름과 보안 프로토콜을 매핑합니다.
  - `KAFKA_LISTENERS`: Kafka 브로커가 수신할 리스너 주소를 지정합니다.
  - `KAFKA_ZOOKEEPER_CONNECT`: Kafka가 연결할 Zookeeper 주소를 지정합니다.
- **volumes**: Docker 소켓 파일을 마운트합니다. 이는 Kafka 컨테이너가 Docker 엔진에 접근할 수 있도록 합니다.

## Docker Compose 실행

`docker-compose.yml` 파일이 있는 디렉터리로 이동한 후, 다음 명령어를 실행하여 컨테이너를 백그라운드에서 실행합니다.

```sh
docker-compose up -d
```

## Docker Compose 명령어 및 옵션

### `docker-compose up` 옵션

- `-d`: detached mode (터미널 사용 가능, 백그라운드 실행, 로그 확인 가능)

## Docker 상태 확인

```sh
docker ps
```

## Docker 컨테이너 다운,정지

Docker에서 정지(`stop`)와 다운(`down`)은 다릅니다.

### 모든 컨테이너 다운

모든 Docker Compose 서비스를 중지하고 제거합니다.

```sh
docker-compose down
```

### 모든 컨테이너 정지

현재 실행 중인 모든 컨테이너를 정지합니다.

```sh
docker stop $(docker ps -q)
```

### 개별 컨테이너 정지

특정 컨테이너를 정지합니다.

```sh
docker stop <container_id_or_name>
```

예시

```sh
docker stop kafka
docker stop zookeeper
```

## docker 컨테이너 로그 확인

### 전체 로그확인

모든 Docker Compose 서비스의 로그를 확인합니다.

```sh
docker-compose logs
```

### 특정 컨테이너 로그확인

특정 컨테이너의 로그를 확인합니다.

```sh
docker-compose logs kafka
```

# Kafka Test Server

## Kafka Topic 생성

kafka에서 메시지를 전송하기위해서는 먼저 Topic 이 생성되어야 합니다.
Topic 생성을 위해서는 kafka 컨테이너에 접속 해야 합니다.

## Kafka 컨테이너 접속

```sh
docker exec -it doker-kafka-1 /bin/bash
```

## Topic 생성

```sh
kafka-topics.sh --create --topic test-topic --bootstrap-server localhost:9092 --partitions 1 --replication-factor 1
```

- --tpoic : 토픽 명칭을 설정합니다.
- --bootstrap-server : 연결 주소를 설정합니다.
- --partitions : 토픽에 파티션 수를 설정합니다.
- --replication-factor : 각 파티션의 복제본 수를 지정한다. 만약 3이라면 3개의 복제본을 가지며 이는 3개의 브로커에 걸쳐 복제된다는 의미입니다.

## 생성된 Topic 리스트 확인

```sh
kafka-topics.sh --list --bootstrap-server localhost:9092
```

## Topic 삭제

```sh
kafka-topics.sh --bootstrap-server localhost:9092 --delete --topic test-topic
```

# TestServer 실행

서버역할을 하기위해 Producer와 Consumer를 동시에 가지고 있습니다.
메세지 전송온 Producer를 통해 정해진 Topic (예 : A2B)를 통해서 하며
메세지 수신은 Consumer를 통해 정해진 Topic (예 : B2A)를 통해서 받습니다.
여기서 수신 받은 메세지에 대한 결과 값은 다시 전송용 Topic(예 A2B)를 통해 전달합니다.

## Topic 생성

```sh
kafka-topics.sh --create --topic A2B --bootstrap-server localhost:9092 --partitions 1 --replication-factor 1
kafka-topics.sh --create --topic B2A --bootstrap-server localhost:9092 --partitions 1 --replication-factor 1
```

## TestServer A, B 실행

inputtopic의 경우 내가 받는 topic 이며 outputtopic의 경우 내가 보내는 toptic이며 실제 상황에서는 outputtopic이 여러개일 가능성이 있습니다.

```sh
.\TestServer.exe -bootstrap-server "localhost:9092" -inputtopic "B2A" -outputtopic "A2B" -groupid "A"
```

```sh
.\TestServer.exe -bootstrap-server "localhost:9092" -inputtopic "A2B" -outputtopic "B2A" -groupid "B"
```

## TestServer 구성

- 각각 A서버 B 서버로 칭해서 설명하면 A 서버는 A2B 토픽을 통해 B 서버로 메세지를 보내며 `[Sent]`라는 표식으로 Console 출력합니다.
- B서버는 A2B 토픽을 통해 메세지를 받으며 `[Message]` 라는 표식으로 받은 메세지를 Console에 출력합니다.
- B서버는 받은 메세지의 앞에 `[RESULT]`를 추가하여 B2A 토픽을 통해 A에게 메세지를 전달합니다.
- A서버는 B2A 토픽을 통해 메세지를 받으며 처음 글자가 `[RESULT]` 일 경우 응답으로 간주하고 Console 출력만 진행합니다.
- B에서 A서버도 같은 방식으로 통신한다.

## 코딩규칙

해당 Repository 공통 코딩 규칙을 준수합니다.

[CONVENTIONS](CONVENTIONS.md)
