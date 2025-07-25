# 🚀 Git Workflow

## 📌 Issue Guide

### **Issue 템플릿**

---

[Feat/Fix/Docs/Design/Refactor/Chore] 이슈 제목

Issue

- 작업할 내용

To-do

- [] 

---

## 📌 Branch Guide

- `main` : 완성된 게임을 배포 시 사용하는 브랜치
- `dev`: Feature 브랜치에서 작업된 작업들을 하나로 합치는 브랜치
- `Feat/#issueNum(-기능 단위)`: 작은 단위의 기능들을 수행하는 브랜치
    - `Feat` 란에는 목적에 맞는 태그(Feat, Fix, Chore 등)를 작성하면 됩니다.
    - e.g., feat/#20

**브랜치 사용 방법**

1. `Issue` 생성

2. `branch` 생성

  - Source branch를 반드시 `dev`로 설정

3. 작업 수행

  - 생성한 브랜치에서 작업

4. Commit & Push

  - 기능 단위로 commit, 완료 후 push

5. PR 생성

  - 대상: dev

6. Review 및 Merge

  - Merge 방식: Squash and Merge

## 📌 Commit message

**커밋 유형**

| 커밋 타입 | 기능 | 사용 예시 |
| --- | --- | --- |
| Feat | 새로운 기능 추가 | Feat: 화면 전환 기능 추가 |
| Fix | 버그 및 기능 수정(정책에 의한 수정, 인터페이스 변경) | Fix: 인터페이스 변경 |
| Docs | 문서 수정 | Doc: 리드미 파일 수정 |
| Design | 간단한 UI 수치 변경, 컴포넌트 추가 | Design: 메인화면의 bgColor의 hex 값 변경 |
| Refactor | 리팩터링 | Refactor: MVC에서 MVVM으로 구조 변경 |
| Chore | 기타 변경 사항 | Chore: PR 양식 수정 |
- 세부적인 기능을 하나 완료할 때마다 commit하기
- 필요한 기능을 모두 완성하여 commit하였을 경우 PR 날리기

[gitmoji](https://gitmoji.dev/)

## 📌 Pull Request 가이드

**PR 템플릿**

---

PR 제목

[Feat/Fix/Docs/Design/Refactor/ChoreE#이슈번호] 이슈 제목

![Untitled](https://prod-files-secure.s3.us-west-2.amazonaws.com/24556268-10a1-407c-b583-a2ab24db7894/04f66feb-3a08-4954-be26-2d60a8b73005/Untitled.png)

---

**좋은 PR의 조건**

**[Assignee]**

1. 커밋이 예상보다 많아진다면 중간에 한번 끊어서 보내기
2. 리뷰어가 PR만으로도 어떤 활동을 했는지, 어떤 고민이 있는지 한 눈에 알아볼 수 있도록 작성한다.
    - 줄글보다는 **간결**하게, 핸드폰으로 봐도 한 눈에 들어오도록, **bullet-point**를 사용한다.
3. PR 템플릿의 내용에 따라 작성한다.
4. `작업한 브랜치` 와 `작업 내용` : 작업한 브랜치와 어떤 작업을 했는지 간결하게 작성한다.
5. `참고 사항` 란에는 다음과 같은 내용을 적는다.
    - 사용법: 새로 추가한 기능이고, 다른 구성원들과 함께 공유해야 하는 상황이라면 코드와 간결한 설명을 첨부한다.
    - 참고 자료: 해당 기능을 구현하면서 참고했던 자료가 있다면 함께 공유한다.
    - 함께 논의하고 싶은 내용: 다른 개발자의 의견이 궁금하거나 / 반나절동안 고민했는데 해결하지 못한 문제가 있다면 PR을 올리며 아래 내용을 함께 적는다.
        - 무엇이 문제인지
        - 해당 문제를 해결하기 위해 나는 스스로 어디까지 고민해 봤는지
        - 함께 참고한 자료는 무엇인지
6. `스크린샷` : 구현된 내용과 관련된 이미지를 첨부한다.
7. `관련 이슈` : 관련 이슈 번호를 기재한다.

## 📌 PR Review Guide

리뷰 방법

- 구현 확인
    - 리뷰할 PR > File Changes 내의 Script를 확인
    - Fork에서 해당 PR의 branch를 track (이때 unity 꺼놓기!!!) → unity를 켜서 어떤 식으로 구현했는지 확인 (보통 scene에서)
- 리뷰 남기기
    - PR 내 add your review 클릭
    - 질문할 코드가 있으면 해당 코드를 누른 후 코멘트를 남기고 리뷰 추가
    - 구현한 내용을 확인 했을 때 문제가 없다면 코멘트를 적은 후 approve를 고른 다음 리뷰 추가
