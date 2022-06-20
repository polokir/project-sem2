# Store

## Функціонал:

1. На сторінці зроблений поділ на адмістратора і клієнта.
2. Адміністратор має доступ до БД, де він може створювати, оновлювати або видаляти поля.
3. Також адмінстратор може зареєструвати ще одного адміністратора
4. Клієнт може обирати товари,який додається в корзину.
5. В корзині зберігається доданий товар,який привязаний до акаунту.

## Про розробку
 Я використовува патерн MVC - архітектурний шаблон, який використовується під час проєктування та розробки програмного забезпечення.
 
 
 ![image](https://user-images.githubusercontent.com/90510727/174583852-6706ef42-ca42-4513-95aa-b7f30a6f318f.png)
 
### Model
**Model** є центральним компонентом шаблону MVC і відображає поведінку застосунку, незалежну від інтерфейсу користувача. Модель стосується прямого керування даними, логікою та правилами застосунку.
**Активна модель** - вигляд відстежує зміни в моделі та реагує на них.
**Пасивна модель** - вигляд оновлюється через контролер.

### View
**View** може являти собою будь-яке представлення інформації, одержуване на виході, наприклад графік чи діаграму. Одночасно можуть співіснувати кілька виглядів (представлень) однієї і тієї ж інформації, наприклад гістограма для керівництва компанії й таблиці для бухгалтерії.

### Controller
**Controller** одержує вхідні дані й перетворює їх на команди для моделі чи вигляду.

## Умовно-обов'язкові модифікації для реалізації MVC

Для реалізації схеми "Model-View-Controller" використовується досить велика кількість шаблонів проектування (залежно від складності архітектурного рішення), основні з яких - "Observer", "Strategy", "Composite".

### Observer
Найбільш типова реалізація — у якій уявлення відокремлено від моделі шляхом встановлення між ними протоколу взаємодії, що використовує «апарат подій» (позначення «подіями» певних ситуацій, що виникають у ході виконання програми, — і розсилання повідомлень про них усім, хто підписався на отримання) : при кожній особливій зміні внутрішніх даних у моделі (позначеній як «подія»), вона сповіщає про неї ті уявлення, що залежать від неї, які підписані на отримання такої оповіщення — і уявлення оновлюється. Так використовується шаблон "спостерігач".

### Strategy
При обробці реакції користувача - уявлення вибирає, залежно від реакції, потрібний контролер, який забезпечить той чи інший зв'язок із моделлю. Для цього використовується шаблон "Strategy", або натомість може бути модифікація з використанням шаблону "Command".

### Composite
Для можливості однотипного поводження з подоб'єктами складно-складового ієрархічного вигляду може використовуватися шаблон «Composite». Крім того, можуть використовуватися й інші шаблони проектування - наприклад, "Factory Method", який дозволить задати типовий контролер за замовчуванням для відповідного виду.

![image](https://user-images.githubusercontent.com/90510727/174591584-b6156623-a3fa-411b-811e-543fe8732893.png)


### Основні патерни ООП реалізовані за замовчуванням








