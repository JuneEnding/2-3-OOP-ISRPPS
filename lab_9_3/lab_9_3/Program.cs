using System;

namespace lab_9_3
{
    //~
    // СОБЫТИЙНОЕ УПРАВЛЕНИЕ ПРОГРАММОЙ
    //Делегат - событие: событию просто добавляем event, разница в ЛОГИКЕ системы, они взаимосвязаны (семантика)
    // 2 event там, а delegate сверху инкапсулируется - недоступны операции над ним/с ним
    //стандартный событийный класс в с#
    //~


    // Класс данных события Message
    // Обработчику события нужно передать информацию о событии
    // Наследуется от EventArgs - базовый класс для классов, содержащих данные событий, 
    // (и предоставляет значение для событий, не содержащих данных)
    public class Message : EventArgs
    {
        // Выводимое сообщение
        public string message { set; get; }
        // Ключевое слово base используется для доступа к членам базового из производного класса 
        // В данном случае, для определения конструктора базового класса, который должен вызываться 
        // при создании экземпляров производного класса
        // !!! Сначала сработает конструктор базового класса, потом производного
        public Message(string message) : base()
        {
            this.message = message;
        }
    }
    // Класс - издатель
    public class Publisher
    {
        // Определение делегата PublisherEventHandler - обработчик событий издателя,который принимает один параметр типа Message
        public delegate void PublisherEventHandler(Message message); //обработчик событий издательства. Сообщение в кач-ве параметра
        // Определяется событие с именем Changed, которое ПРЕДСТАВЛЯЕТ делегат PublisherEventHandler
        public event PublisherEventHandler Changed; // без ивент аналог объявления сигнатуры ф-и. с event теперь ссылка на событие
        public Publisher() { } // Конструктор
        // *Книжки пришли*
        public void EventForPublicher(Message message) // Оповещение, входной параметр типа класса данных события Message
        {
            Console.WriteLine($"Event for all subscribers: {message.message}"); // Событие для всех подписчиков
            Changed(message); // "Изменено" - вывод события
        }
    } 

    // Класс - подписчик
    public class Subscriber
    {
        int QRC { set; get; }       // По нему можно инициализировать, какой подписчик
        public Subscriber(int QRC)  // Конструктор
        {
            this.QRC = QRC;
        }
        // Подписаться
        public void subscribe(Message message)
        {//+= (делегат?). подписка
            Console.WriteLine($"Subscriber {this.QRC}! {message.message}");
        }
    }
        class Program
        {
            static void Main(string[] args)
            {
                Console.WriteLine($"Hello Publisher!");
                Publisher   publisher = new Publisher();        // Создадим издателя
                Subscriber  subscriber_1 = new Subscriber(1);   // И подписчиков 
                Subscriber  subscriber_2 = new Subscriber(2);
                // . . .
                Subscriber subscriber_n = new Subscriber(8); //перевенутая бесконечность

                // Установим ""подписку"" - связь подписчика и издательства
                // STEP 2 subscriber

                //Подписываем подписчиков на издательство - на событие
                // Событию Changed, представляющий его делегат PublisherEventHandler инкрементируем методом subscribe класса subscriber
                // ~~~ Ассоциация один ко мноним
                Console.WriteLine($"Step 1");
                publisher.Changed += subscriber_1.subscribe; // этой штукой?' public event PublisherEventHandler Changed;
                publisher.Changed += subscriber_2.subscribe;
                publisher.Changed += subscriber_n.subscribe;
                // Вызыв оповещения для ПОДПИСАННЫХ подписсчиков о том, что книги прибыли
                // Вызыв делегата с параметром тапа Message, который создается прямо в параметре
                publisher.EventForPublicher(new Message("Number number 5 are ready!"));
                Console.ReadKey();

                Console.WriteLine($"\nStep 2");
                // Убрали с подписки
                publisher.Changed -= subscriber_n.subscribe;
                // Вызыв оповещения для ПОДПИСАННЫХ подписсчиков о том, что книги прибыли
                // Вызыв метода EventForPublicher, с параметром тапа Message, который создается прямо в параметре, который
                // в свою очередь создается конструктором с 1 пораметром, инициализируя атрибутут message
                // В методе выводится сообщение, а следом вызывается событие Changed, которое вызывает представляющий его делегат
                // Вызываются операции из списка вызова делегата события, каждой операции передается сообщение message 
                publisher.EventForPublicher(new Message("Number number 5 are ready!"));
                Console.ReadKey();

            }
        }
}
