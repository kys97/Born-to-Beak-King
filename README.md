# 왕이 될 상이로새

Unity 기반 선택형 게임 프로젝트입니다.

## 개발 환경

- Unity **6000.5.8f1**
- Built-in Render Pipeline, UGUI, Legacy Input Manager
- UnityGoogleSheets: `Packages/manifest.json`의 Git 의존성

## 실행

1. 저장소를 복제하고 Unity Hub에서 프로젝트 폴더를 추가합니다.
2. Unity 6000.5.8f1로 열고 패키지 설치 및 임포트가 완료될 때까지 기다립니다.
3. Google Sheets 연동 설정은 프로젝트 소유자에게 별도로 받아 `Assets/Resources/UGSettingObject.asset`에 구성합니다. 이 파일은 비밀번호를 포함하므로 Git에서 제외됩니다.
4. `Assets/Scenes/Main.unity`에서 플레이를 시작합니다.

## 구성

- `Assets/Scripts`: 게임 로직 및 화면 표시
- `Assets/UGS.Generated`: 생성된 시트 데이터와 타입
- `Assets/Resources`, `Assets/Assets`: 이미지 등 게임 에셋
- `Packages`, `ProjectSettings`: 재현에 필요한 Unity 설정
- `docs/AI`: 프로젝트 분석과 리팩토링 기록

Unity 캐시, IDE 생성 파일, 빌드 결과물과 로컬 비밀번호 설정은 버전 관리하지 않습니다. 새로운 환경에서의 실행과 플랫폼 빌드는 별도로 확인해야 합니다.
