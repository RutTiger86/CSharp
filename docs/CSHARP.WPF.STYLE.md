[README 보기](../README.md)

# CSharp.WPF.Style - .Net8 Window WPF Client 프로젝트

Wpf 프로젝트로 간단한 Style 작성 및 BasedOn을 통한 Style 관리를 구현합니다. 
공통적인 프로세스에서 사용될 Button , TextBox에 대하여 기능적인 Style을 구현 합니다. 
ResourceDictionary 를 통해 통합적인 Style 사용을 지원합니다. 


## 중점사항

- Style은 App.xaml에 Application.Resource로 등록 되어있으며 필요시 위치 변경 혹은 분리하여 필요한 부분만 적용할 수 있습니다.
- 색감 관련(Color, Gradient 등) 은 BaseStyles에 선정하고 다른 Style은 해당 정의를 바인딩하여 사용합니다. 
- Font에 대한 정의를 small, Medium, large 로 선정 해놓아 사용하여 통일성을 갖추며 필요시 추가 정의 합니다. (md의 Header 등과 같이)
- 데이터 표출 및 입력값 전달 기능은 Style에서 외부로 Binding 될수 있도록 구현합니다. 

## 코딩규칙

해당 Repository 공통 코딩 규칙을 준수합니다.

[CONVENTIONS](CONVENTIONS.md)
