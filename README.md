<div align="center">
  
![Untitled (15)](https://github.com/NailBawoomUnityCamp/A09/assets/127918879/2283c481-90fc-4372-b0da-46ebfc5bfef1)



<img src="https://img.shields.io/badge/Unity-000000?style=flat-square&logo=unity&logoColor=white"/><img src="https://img.shields.io/badge/C sharp-512BD4?style=flat-square&logo=csharp&logoColor=white"/>

</div>

# 📖목차

1. [프로젝트 소개](#프로젝트-소개)
2. [팀소개](#팀소개)
3. [프로젝트 계기](#프로젝트-계기)
4. [주요기능](#주요기능)
5. [개발기간](#개발기간)
6. [기술스택](#기술스택)
7. [서비스 구조](#서비스-구조)
8. [와이어프레임](#와이어프레임)
9. [개발 일정](#개발-일정)
11. [프로젝트 파일 구조](#프로젝트-파일-구조)
12. [Trouble Shooting](#trouble-shooting)
13. [시연영상](#시연영상)
    
<br>
<br>
<br>
<div align="center">
  
# 프로젝트 소개

### 게임 이름

#### **`ROBO BLASTER`**

<br>

### 게임 컨셉

####   **`횡스크롤 탄막 슈팅게임`**
   
<br>
<br>
<br>

# 팀소개
### 내일배움캠프 Unity 4기 입문 프로젝트 A09조 9 to 9

<br>

### 👨‍👨‍👦 **멤버구성**

팀장 : **이상수**<br>
팀원 : **이인호**<br>
팀원 : **김태형A**<br>
팀원 : **김영선**

<br>
<br>
<br>

# 프로젝트 계기
3가지의 고전 게임 중 게임의 확장성이 넓고 <br>다양한 종류의 게임으로 만들어 낼 수 있는 점을 착안하여 Dodge를 선택했습니다.<br><br>
기존의 Dodge와는 다르게 적이 추가되어 슈팅게임으로 전환하였고, 많은 사람들이 어린시절 즐겼던 게임 스타일로 레트로 감성을 자극 하여 향수를 불러일으킬 수 있는 횡스크롤로 바꾸어 제작하였습니다.

<br>
<br>
<br>

# 주요기능
### 멀티 플레이 선택
![멀티버튼](https://github.com/NailBawoomUnityCamp/A09/assets/127918879/931d30b4-0160-41f6-8e21-8a6bef8c4fb0)
![싱글버튼](https://github.com/NailBawoomUnityCamp/A09/assets/127918879/f2723db7-f17b-4354-8333-24d212d28361)

### 난이도 선택
![난이도 선택](https://github.com/NailBawoomUnityCamp/A09/assets/127918879/1e80d13c-a4bc-4f7e-8489-ac68dd85e8ad)

### 볼륨 조절
![볼륨조절](https://github.com/NailBawoomUnityCamp/A09/assets/127918879/23341dcc-649f-42c2-a8a9-b5773c1b8ce4)
### 리더 보드
![image](https://github.com/NailBawoomUnityCamp/A09/assets/127918879/bb6f4edb-4204-4d2b-9084-17b0f0e8fa22)
### 스킬
![스킬샤용](https://github.com/NailBawoomUnityCamp/A09/assets/127918879/657c0a57-f4b9-477c-899e-b77d1491bd1a)
### 플레이어 강화 포션
![포션들](https://github.com/NailBawoomUnityCamp/A09/assets/127918879/c73258bc-aae4-4424-84a6-b0efd1bde3a2)

### 조작법

#### Player1

**이동**: `W` (위), `A` (왼쪽), `S` (아래), `D` (오른쪽)<br>
**스킬 사용**: `F` 키<br>
**공격**: `G` 키

#### Player2

**이동**: `↑` (위), `←` (왼쪽), `↓` (아래), `→` (오른쪽)<br>
**스킬 사용**: `.` 키<br>
**공격**: `/` 키  

<br>
<br>
<br>

# 개발기간
24.05.15 ~ 24.05.22

<br>
<br>
<br>

# 기술스택
### 언어
**C#**

### 프레임워크 및 라이브러리
**Unity**: 게임 개발 프레임워크<br>
**Newtonsoft.Json**: JSON 데이터 처리 라이브러리

### 개발 도구
**Visual Studio**: 통합 개발 환경(IDE)<br>
**GitHub**: 버전 관리 및 소스 코드 저장소

<br>
<br>
<br>

# 서비스 구조
### 시스템 아키텍처 다이어그램
![image](https://github.com/NailBawoomUnityCamp/A09/assets/127918879/47af7c3d-f907-4c7a-8d00-fc7c321788c8)
> 시스템 아키텍처 다이어그램은 전체 시스템의 구성을 시각적으로 보여줍니다.

<br><br>
### 주요 구성 요소
**클라이언트**<br> Unity 엔진을 사용하여 개발된 게임 클라이언트입니다. 사용자가 직접 상호작용하며 게임을 플레이합니다.<br><br>
**데이터 저장소**<br> 게임 데이터는 로컬 파일 시스템에 JSON 형식으로 저장됩니다. 이는 게임 점수 데이터를 포함합니다.<br>
<br><br>
### 데이터 흐름
사용자가 게임을 시작하면, 클라이언트는 초기 설정을 로드합니다.
사용자는 게임을 플레이하며 점수를 획득합니다.
게임 플레이가 완료되면, 클라이언트는 최종 점수를 로컬 저장소에 JSON 형식으로 저장합니다.
<br><br>
### 상호작용 시퀀스
#### 게임 시작
사용자가 게임 아이콘을 클릭하여 게임을 실행합니다.
클라이언트가 초기 설정을 로드합니다.
초기 화면이 표시되고, 사용자는 게임을 시작합니다.

#### 게임 진행
사용자가 게임을 플레이하면서 점수를 획득합니다.
점수는 실시간으로 메모리에 유지됩니다.

#### 게임 종료 및 점수 저장
사용자가 게임을 완료하면, 최종 점수가 계산됩니다.
클라이언트는 최종 점수를 로컬 저장소에 JSON 형식으로 저장합니다.
저장이 완료되면 게임이 종료됩니다.
<br><br>
### 기술 스택과의 관계
**Unity**: 클라이언트 개발에 사용되었습니다.
**C#**: 게임 로직과 데이터 처리에 사용되었습니다.
**Visual Studio**: 전체 개발 환경으로 사용되었습니다.
**GitHub**: 코드 버전 관리 및 협업을 위한 플랫폼으로 사용되었습니다.
**Newtonsoft.Json**: 로컬 저장소에 데이터를 JSON 형식으로 저장하는 데 사용되었습니다.


<br><br><br>

# 와이어프레임
![image](https://github.com/NailBawoomUnityCamp/A09/assets/127918879/8f2a247e-84ef-4063-8a1f-bf61e15f20b2)

# 개발 일정
![image](https://github.com/NailBawoomUnityCamp/A09/assets/127918879/6eaaccea-69e5-4646-915e-02dc1f7100ba)


<br><br><br>

# 프로젝트 파일 구조
</div>

```plaintext
─Assets
  ├─Animation
  │  ├─EnemyAnim
  │  ├─ItemsAnim
  │  └─PlayerAnim
  ├─Assets
  │  ├─64 Potions
  │  ├─Robot Shooting Game
  │  ├─Sounds
  │  └─UI
  ├─Input
  ├─Prefabs
  │  ├─Enemy
  │  ├─Player
  │  ├─Potions
  │  └─UI
  ├─Scenes
  ├─Scripts
  ├─Enemy
  ├─Item
  ├─Player
  ├─Potions
  ├─UI
  └─Util

```

<br><br><br>

<div align="center">
  
# Trouble Shooting
</div>

### HomingSpreadAttackShip이 공격을 할 때 3갈래로 나눠져서 날라가지 않는 오류

예상 : homingbullet에서 player를 향하도록 설정한 것이 문제라 추측

해결 : 어떻게 해도 총알이 중복되어서 날아가는 문제 때문에 결국 homingbullet에서 목표를 설정하지 않고 attackShip에서 목표를 설정하는 방법으로 수정

HomingBullet의 Aim을 SetDirection으로 수정, Player태그를 찾아 방향을 설정하는 것을 삭제

```csharp
using UnityEngine;

public class HomingBullet : MonoBehaviour, IBullet
{
    ...

    public void SetDirection(Vector2 direction)
    {
        _direction = direction.normalized;
    }

    ...
}

```

HomingBullet을 사용하는 HomingSpreadAttackShip과 HomingAttackShip에서 플레이어 태그를 가진 오브젝트를 탐색하고 방향 설정하도록 수정

```csharp
using UnityEngine;

public class HomingSpreadAttackShip : MonoBehaviour, IEnemy
{
    ...

    public void Attack()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;

        Vector2 playerDirection = (player.transform.position - firePoint.position).normalized;
        ShootBullet(playerDirection);

        float minAngle = -(numberOfProjectilesPerShot / 2f) * multipleProjectilesAngle + 0.5f * multipleProjectilesAngle;

        for (int i = 0; i < numberOfProjectilesPerShot; i++)
        {
            float angle = minAngle + multipleProjectilesAngle * i;
            Vector2 spreadDirection = Quaternion.Euler(0, 0, angle) * playerDirection;
            ShootBullet(spreadDirection);
        }
    }

    ...
}
```
### 적이 생성될 때, 같은 지점에서 생성될 경우 위/아래로 밀리는 오류

겹치지 않도록 적 생성을 담당하는 EnemyManager에서 IgnoreLayerCollision을 사용하여 layer충돌 무시 설정

```csharp
void Start()
{
    int enemyLayer = LayerMask.NameToLayer("Enemy");

    Physics2D.IgnoreLayerCollision(enemyLayer, enemyLayer, true);
}
```
### 처치된 적이 다시 오브젝트 풀에서 생성될 때, 움직이지 않음

추측 : 파괴될 때 속도가 0으로 되어 움직이지 않는 것 같다고 추측

해결 : 각 적 개체의 활성화 시 속도를 재설정

```csharp
void OnEnable()
{
    Hp = 1;
    Speed = 5.0f;
    BulletType = BulletType.Regular;
    _attackTimer = 0f;
    isDead = false;
    isHit = false;
    animator.SetBool("isDestroyed", false);
    rigidbody.velocity = Vector2.left.normalized;
}
```
### 보스의 레이저 공격 시 레이저의 중심부분 부터 레이저 발사 위치에 소환되는 문제 발생

추측 : 피봇위치가 중심이라서 그런 것으로 추측

시도 : 피봇 위치 변경 > 실패

해결 : 보스의 하위 오브젝트로 넣어서 collider와 renderer를 활성화/비활성화 하는 방식으로 수정

```csharp
 
 private void OnEnable()
 {
     Hp = 50; // 보스의 HP 설정
     isDead = false;
     isHit = false;
     _attackTimer = 0f;
     _currentBurstCount = burstCount;
     animator.SetBool("isDestroyed", false);
     laserBeam.SetActive(false); // 레이저빔을 비활성화 상태로 시작
     StartCoroutine(MovePattern());
     StartCoroutine(HomingSpreadAttackRoutine());
     StartCoroutine(RegularSpreadAttackRoutine());
     StartCoroutine(LaserAttackRoutine());
 }

 private IEnumerator LaserAttackRoutine()
 {
     while (!isDead)
     {
         yield return new WaitForSeconds(laserAttackInterval);
         if (!isDead)
         {
             StartCoroutine(LaserAttack());
         }
     }
 }
```
### 보스가 화면 밖으로 나가는 문제

좌표를 지정하고, clamp를 하여 지정된 좌표를 벗어나지 못하게 했으나 최소/최대 좌표를 벗어나는 문제가 발생

디버그 로그를 이용해 확인해봤지만 clamp는 잘 이루어지는 것을 확인.

움직임의 문제인 것을 확인하고 움직임을 담당하는 부분을 FixedUpdate로 옮김

```csharp
private void FixedUpdate()
{
    if (_isMoving)
    {
        Vector2 newPosition = Vector2.MoveTowards(_rigidbody.position, _targetPosition, Speed * Time.fixedDeltaTime);
        newPosition.x = Mathf.Clamp(newPosition.x, _minX, _maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, _minY, _maxY);

        _rigidbody.MovePosition(newPosition);

        if (Vector2.Distance(_rigidbody.position, _targetPosition) < 0.1f)
        {
            _isMoving = false;
            _rigidbody.velocity = Vector2.zero;
        }
    }
}

private IEnumerator MovePattern()
{
    Vector2[] directions = new Vector2[] {
        Vector2.up, Vector2.down, Vector2.left, Vector2.right
    };

    while (!_isDead)
    {
        Vector2 moveDirection = directions[Random.Range(0, directions.Length)];
        _targetPosition = (Vector2)transform.position + moveDirection * Speed * MoveDuration;

        _targetPosition.x = Mathf.Clamp(_targetPosition.x, _minX, _maxX);
        _targetPosition.y = Mathf.Clamp(_targetPosition.y, _minY, _maxY);

        _isMoving = true;
        yield return new WaitForSeconds(MoveDuration + BaseAttackInterval);
    }
}
```

### VS에서 breakpoint가 적용 안되는 에러


- **function에 직접 break point를 지정하는 방법**

지금까지는 VS에서 단순히 원하는 지점 왼쪽에 break point를 찍는 지점에 마우스로 찍어서 사용해 왔는데, 당황스럽게도 VS코드에서 해당 지점을 찾을 수 없다는 에러가 발생

여러가지 방법을 인터넷에서 찾아보다가 결국 찾은 방법은 무조건 되는 방법이라고 하던데 바로 function name에 break point를 거는 방법. 이 방법을 사용하니 마우스로 찍을 때는 못찾던 function들도 실행중에 마주치면 알아서 break point가 걸려서 임시방편으로 사용 가능

- **솔루션 재빌드**

VS에서 solution explorer에 solution을 우클릭하면 rebuild 옵션이 있는데 이를 rebuild하면 해결 가능

slack에 다른 같은 오류를 겪는 사람들과 토의해보고자 찾은 해결법을 올렸고, 그에 대해 답변으로 튜터님들과 매니저님께서 도와주셔서 해결법을 발견

![image (8)](https://github.com/NailBawoomUnityCamp/A09/assets/127918879/4f3c1091-b6e0-4fc6-80c3-ad6a62aa7c72)


이렇게 하면 function이름에 break point를 거는 것이 가능

![image (9)](https://github.com/NailBawoomUnityCamp/A09/assets/127918879/b6f8ac7b-b6fd-4bd8-b450-674f4fce2c55)


그러면 이렇게 창이 뜨는데 여기서 function 이름을 치면 해당 function을 만나면 알아서 breakpoint가 걸림

### InputSystem에 대한 정확한 이해

플레이어의 스킬을 구현할 때 겪은 일. InputSystem에서 플레이어의 스킬 발동키를 등록하기 위해 Skill이라는 액션을 만들고 이를 F에 등록 . 이때는 스킬을 만들생각을 막연히 하고만 있었을 뿐 어떤 형식으로 만들지 어떤 효과를 구현할지 생각하지 않았고 코드 작성에서 정하고자 함.

그렇게 코드를 작성했는데 분명 로직에 이상은 없는데 작동하지를 않음. 다음은 작성한 코드.

```csharp
/// == 생략
public class TopDownController : MonoBehaviour // 호출할 이벤트 작성
{
///    
    public event Action OnShiledEvent;
    protected bool isShiled {  get; set; }

///
    public void CallSheledEvent()
    {
        OnShiledEvent?.Invoke();
    }
///    
}
------
///
public class PlayerInputController : TopDownController
{
///
    public void OnShiled(InputValue value)
    {
        isShiled = value.isPressed;
    }
///
}
```

코드를 작성하면서 스킬의 효과를 방어막 같은 식으로 구현하고자 했고, 이를 식별하기 쉽게 Shiled라는 이름으로 메서드들을 정의하고자 함. 그런데 로직을 검토해도 이상이 없는데 아무리 수정을 해봐도 작동하지가 않음.

그러다가 Fire이벤트랑 비교를 해본 결과 InputSystem에 등록된 Fire액션과 코드에서 사용한 각 메서드들의 이름이 일치한다는 것을 발견, 혹시 몰라 모든 Shiled를 Skill로 바꿧더니 그때서야 실행. 그 후 InputSystem에 대해 조금더 자세히 공부하였고 왜 그런지를 발견.

유니티의 InputSystem에서는 특정 이름을 가지는 메서드를 자동으로 호출하는 기능을 제공. 이를 통해 별도의 코드 없이도 액션과과 메서드가 자동으로 연결가능. 그렇기에 액션을 Skill로 등록한다면 이 액션은 메서드로 OnSkill을 기다리지, 다른 명칭의 메서드를 등록 해봤자, 액션은 이 메서드를 감지하지 못함. 

즉 액션이 정의되었더라도, 이를 처리할 메서드와 연결하지 않으면, 액션은 작동하지 않고, 연결할 메서드의 이름은 임의로 만드는 것이 아닌, 액션의 이름을 기반으로 만들어야 한다는 것.(엄밀히 말하자면 임의로 만들어도 되나, 

```csharp
playerInput.Player.Skill.performed += OnShiled;
playerInput.Player.Skill.canceled += OnShiled
```

와 같이 추가적인 연결이 필요한 것은 변하지 않음.) 이 일을 이후 InputSystem에 대해 보다 깊게 이해할 수 있게 되었고, InputSystem을 더욱 잘 다루게 됨.

<div align="center">
  
# 시연영상

<br><br><br>
