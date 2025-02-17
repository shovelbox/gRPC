# gRPC Chat Application

이 프로젝트는 gRPC를 사용하여 간단한 채팅 애플리케이션을 구현한 예제입니다. 클라이언트와 서버 간의 양방향 스트리밍을 통해 실시간으로 메시지를 주고받을 수 있습니다.

## 프로젝트 구조

- **gRPC_Client**: gRPC 클라이언트 애플리케이션
- **gRPC_Server**: gRPC 서버 애플리케이션

## 주요 기능

### gRPC_Client

- 서버와의 연결을 설정하고 채팅 메시지를 주고받습니다.
- 사용자가 입력한 메시지를 서버로 전송하고, 서버로부터 받은 메시지를 출력합니다.

### gRPC_Server

- 클라이언트로부터 메시지를 수신하고, 수신한 메시지를 다시 클라이언트로 전송합니다.
- 여러 클라이언트와의 동시 연결을 지원합니다.

## 사용된 기술

- **.NET 8**
- **gRPC**: 고성능 원격 프로시저 호출 (RPC) 프레임워크
- **Google.Protobuf**: Protocol Buffers (protobuf) 라이브러리
