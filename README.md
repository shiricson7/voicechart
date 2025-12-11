# VoiceChart (skeleton)

Windows 데스크톱(WPF) 앱 구조 초안입니다. 음성 녹음 → STT → 요약/SOAP → 클립보드/자동입력 흐름을 빠르게 시도할 수 있도록 최소 뼈대를 넣었습니다.

## 구조
- `VoiceChart.Core` : 도메인 로직 (녹음, STT, 노트 생성, 저장, 모델)
- `VoiceChart.App` : WPF UI, 핫키/클립보드/자동입력 구현 자리
- `VoiceChart.sln` : 솔루션 파일

## 필수 요구사항
- .NET 8 SDK (미설치 시 https://dotnet.microsoft.com/download)

## 빌드/실행
```powershell
dotnet restore
dotnet build
dotnet run --project VoiceChart.App
```

## 구현 메모
- 오디오: `StubAudioRecorder` → 실제 마이크 캡처와 WAV 저장으로 교체
- STT: `WhisperTranscriber` → whisper.cpp 래퍼 또는 Azure Speech 호출
- LLM: `StubNoteProcessor` → LLM 프롬프트 호출로 SOAP/요약 생성
- 저장소: `FileVisitRepository`는 JSON 저장. 필요하면 암호화/DB(LiteDB/SQLite)로 교체
- 내보내기: 기본 `ClipboardExportService`, 자동 타이핑은 `AutoTypeService`
- 핫키: `HotkeyService`에 `RegisterHotKey` / `WM_HOTKEY` 처리 추가 예정

## 다음 단계
1) 실제 오디오 녹음/종료 구현, 핫키에서 Start/Stop 트리거
2) STT 엔진 연동 및 발화자 구분 옵션
3) LLM 프롬프트 설계 (요약 + SOAP), 오프라인/온라인 선택
4) 자동입력(선택) 또는 클립보드 복사 최종 다듬기
