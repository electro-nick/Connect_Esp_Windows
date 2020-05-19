# Connect_Esp_Windows

Для начала необходимо инициализировать Класс UDP для общения с ESP.
Первое что необходимо это прописать айпи и порт для общения. 
Айпи мы прописывали в arduino IDE.

### Инициализация:
`UDP udp = new UDP("192.168.0.187", 4210);`

Этот класс позволяет использовать 3 метода и 1 перегрузку.
**Перегрузка** в c#: это методы, которые имеют одно и то же имя, но разные входные параметры. В данном случае это метод **Send**.

### Методы:
1. `void Send(String key, int value);`
2. `void Send(String key, String value);`
3. `String GetData(String key);`
4. `ServerData GetServerData();`

### Описание:
1. Отправка числа на ESP
2. Отправка строки на ESP
3. Получение строки из ESP
4. Получение модели данных из ESP. Отличие от верхнего в том что мы получаем данные и преобразуем их в класс, после чего можем удобно пользоваться им.

### Примеры:

1. `udp.Send("power", 1);` - Включить лампу.
2. `udp.Send("brightness", 60);` - Установить яркость лампы. от 0 до 255.
3. `udp.Send("mode", 2);` - Установить 2 режим работы лампы.
4. `udp.GetServerData();` - Тут уже сложнее. Более подробно описано ниже.

Данный метод необходимо вызывать в отдельном потоке.
Для начала инициализируем классс таймера.

* `Timer timer = new Timer();`
* `timer.Interval = 1000;`
* `timer.Tick += new EventHandler(TimerEventProcessor);`
* `timer.Start();`

Далее прописываем функцию, которая будет вызываться каждую 1000 миллисекунду. То есть 1 секунду.

* `private void TimerEventProcessor(object sender, EventArgs e)`

После чего в ней получаем данные из ESP.

* `ServerData serverData = udp.GetServerData();`

Более наглядно видно в коде:

![](https://github.com/electro-nick/Connect_Esp_Windows/blob/master/images/code3.png)
