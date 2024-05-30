[README 보기](../README.md)

#Repository 공통 코딩 규칙 
해당 Repository는 별도 명시가 없는한 본문의 코딩 규칙을 사용합니다.

##선정 코딩 규칙(Camel)
* 여러 단어로 이루어진 이름을 공백 업싱 하나의 단어로 작성합니다. 
* 각 단어의 첫글자를 대문자로 사용하여 구분합니다. 
* Class 는 UppercamelCase를 사용합니다. 
* 변수는 LowerCamelCase를 사용합니다. 

```cs
public class CustomerOrder {
  public string OrderId { get; set; }
  public DateTime OrderDate { get; set; }
  // methods and logic here
}

## 접두어 추가 
- interface의 경우 앞에 i를 둡니다. 
- 단일 사용 interface는 해당 Class 위에 배치 합니다. 

## 파일 명칭 
- Class 파일은 Class 명과 동일시 합니다. 
- md 파일의 경우 모두 대문자로 합니다.