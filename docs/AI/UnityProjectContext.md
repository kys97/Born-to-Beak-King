# Unity 프로젝트 컨텍스트

분석일: 2026-09-08. 프로젝트 루트에 Git 메타데이터가 없어 분석 커밋은 기록하지 못했다.

## 확인된 환경

- Unity 6000.5.8f1. `ProjectSettings/ProjectVersion.txt` 기준.
- Built-in Render Pipeline: `GraphicsSettings.asset`의 커스텀 파이프라인 참조가 비어 있고 manifest에 SRP 패키지가 없다.
- Legacy Input Manager: `ProjectSettings.asset`의 `activeInputHandler: 0`.
- 생성된 C# 프로젝트의 타깃은 WebGL이다. 배포 빌드의 성공 여부는 별도 검증 대상이다.
- UGUI, Unity 2D 패키지, Unity Test Framework 1.7.0, Git 기반 UnityGoogleSheets 패키지를 사용한다.
- 직접 작성한 테스트와 asmdef는 발견하지 못했다. 게임 코드는 `Assembly-CSharp`에 포함된다.
- 연결된 Unity MCP 도구는 없다. 로컬 소스와 Unity에 포함된 C# 컴파일러로 작업했다.

## 코드와 데이터

- `Assets/Scripts`: 게임 코드 14개. `Load` 하위 폴더는 씬별 UI 표시를 담당한다.
- `Assets/UGS.Generated/Scripts`: 시트에서 생성된 데이터 타입. 직접 수정하지 않는다.
- `Assets/Resources`: 사건, 뉴스, 엔딩 등의 이미지와 UGS 설정.
- `Assets/Scenes`: 화면별 씬.
- `GameManager`: 씬 사이에 유지되는 싱글턴. 연도, 자원, 지지도, 사건 추첨, 선택 기록, 엔딩 해금 상태를 관리한다.
- `ChangeScene`: 버튼 콜백, 선택 적용, 씬 전환, 자원 부족 메시지를 담당한다.
- `ShowResult`: 엔딩 분기와 설명 문구를 담당한다.

## 시작과 화면 이동

활성 빌드 씬은 Main, Room, Scrolls, Opinion, MainStory_Result, News, MainStory_Event, Result이다. `GameManager.Start`가 데이터를 로드하고 첫해를 설정한다. `ChangeScene`에서 이름으로 씬을 로드한다. 일반 경로는 Main → Room → Scrolls → Opinion → Scrolls이며 이후 News와 메인 스토리로 진행한다. 후속 씬은 초기화된 GameManager를 전제로 한다.

## 수정 시 유지할 계약

- public 필드와 `[SerializeField]` 필드명, public 버튼 콜백 이름을 유지한다.
- `.meta` GUID와 씬의 직렬화 참조를 보존한다.
- 기존 스크립트에는 CP949 인코딩이 사용된다. UTF-8로 잘못 읽은 문자열을 저장하지 않는다.
- 소규모 MonoBehaviour 중심 구조이며 전역 네임스페이스를 사용한다.
- 사건 데이터는 현재 리스트 순서와 정수 인덱스로 연결되어 있다.
- 정적 화면은 다음 씬 로드까지 같은 데이터를 보여 준다. 런타임 데이터 변경을 도입한다면 화면 갱신 경로도 함께 변경해야 한다.

## 검증 범위와 한계

소스 14개, 패키지 manifest, 생성 데이터 타입 일부, 빌드 씬 설정, 그래픽/입력 설정, 생성된 C# 프로젝트를 확인했다. Unity 캐시의 응답 파일과 설치된 Roslyn으로 게임 어셈블리 컴파일을 확인했다. Play Mode, 전체 씬의 필수 참조, 시각적 결과, 성능 측정과 WebGL 플레이어 빌드는 직접 검증하지 않았다.
