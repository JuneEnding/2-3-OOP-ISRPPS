using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection; //Типы для получ. сведений о сборках, модулях, членах, параметрах... путем обработки их метаданных (технология опред. метода, из объекта в памяти (можно опр атриб объ по его определению?))

namespace lab_i_5
{
    public partial class Form1 : Form {
        private static Form1 f1     = null; // STEP 2 (агр по значению, .... атр доступа д б статич)
        AbstractFactory      bank   = null;                                 //????????????????????????????????
        Client               client = null;
        TestClass            testclass = null;

        // Запечатанный конструктор по умолчанию
        private Form1() {               // STEP 1
            InitializeComponent();
            bank = new BankSber();
            testclass  = Singleton<TestClass>.CreatorInstance;
        }

        // Свойство, регулирующие жизнь единственного экземплара Form1 f1
        public static Form1 fA{ get { // Создается только 1 раз!
                if (f1 == null)
                    f1 =  new Form1(); 
                return f1;
            } 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.client = new Client(new BankSber());
            this.client.Run(); // нужна для перессылки счетов
            this.label1.Text = this.client.abstractProductA.account.ToString(); // Вывод счета
            // можно добавлять деньги, брать
            //this.bank = new BankSber(); // Можно было и независимо созд 
            //    bank.CreateProductA();
        }
        private void button2_Click(object sender, EventArgs e){
            this.client = new Client(new BankVTB());
            this.client.Run();
            this.label2.Text = this.client.abstractProductB.account.ToString();
        }
        private void TestSingleton_Click(object sender, EventArgs e){
            MessageBox.Show( fA.testclass.TestProc());
        }
        private void label1_Click(object sender, EventArgs e){}

        private void label2_Click(object sender, EventArgs e){}

    }// end Form1
    public class Singleton<T> where T : class
    {
        protected Singleton() { MessageBox.Show("Конструктор Singleton<T> сработал");  } // защищенный констр!!
        // +++++++++++++++ (отлож инициализация . созд отложенного объекта.. стр 9)

        // класс запечатан и не виден и не наследуем - sealed
        // S - как получ - берем тип от класса .. (S) (преобр типа)typeof(S).GetConstructor (получение конструктора)
        private sealed class SingletonCreator<S> where S : class // s - класс конкретизации
        {
            // GetConstructor выполняет поиск конструктора с параметрами, соответствующими указанным 
            // модификаторам и типам аргументов, с учетом заданных ограничений по привязке и соглашений о вызовах.
            public static S Instance { get; } = (S)typeof(S).GetConstructor(// получ объекта класса конкретизации, которую узнаем
                BindingFlags.Instance | BindingFlags.NonPublic, // (связывающий флаг) Побитовое сочетание значений перечисления, указывающих способ проведения поиска.
                                                                // Instance - классы экземпляров инструментария управления. 
                null,                                           // Возвращает ссылку на связыватель по умолчанию
                new Type[0], // Массив объектов Type, предоставляющих число, порядок и тип параметров нужного конструктора
                new ParameterModifier[0]).Invoke(null); // Атрибуты, связанные с соответствующим элементом в массиве типов параметра.
                // Получили ссылку,  Invoke - вызвали этот констр. по ссылке
        }// end class SingletonCreator
        public static T CreatorInstance { get {return SingletonCreator<T>.Instance; }}

    }// end Singleton <T>
    public class TestClass : Singleton<TestClass> 
    {
        /// Вызовет защищенный конструктор класса Singleton
        // Скрытый конструктор
        private TestClass() { MessageBox.Show("Конструктор TestClass сработал"); }
        public string TestProc(){return "Hello World"; } // как его создать - public static S CreatorInstance{ get{ return instance; } }, где он созд, а она не статик!
    }

    //*************************************************************************************
    // "AbstractFactory"
    //*************************************************************************************
    abstract class AbstractFactory {
        int Balance { get; set; }
        public abstract AbstractProductA CreateProductA();
        public abstract AbstractProductB CreateProductB();
    }
    // м. б. разный набор методов
    // "AbstractProductA"
    abstract class AbstractProductA {
        public int account { set; get; } = 222;
        //public abstract void Transfer(AbstractProductB b, int fund_to_trnsfr);
        //public abstract void Trnsfr_to_anotherbankA(AbstractProductA a, int fund_to_trnsfr);

    }
    // "AbstractProductB"
    abstract class AbstractProductB {
        public int account { set; get; } = 555;
        public abstract void Interact(AbstractProductA a); // специфика б взаимодейств с а - м б и без этого
        //public abstract void Transfer(AbstractProductA a, int fund_to_trnsfr);
        //public abstract void Trnsfr_to_anotherbankB(AbstractProductB b, int fund_to_trnsfr);

    }
    // Step 2
    // Нужно созд конкретную фабрику
    // "ConcreteFactory1"
    class BankSber : AbstractFactory{ // специф опред - опред 1 фабрику
        public override AbstractProductA CreateProductA() { // ЗАМЕЩЕНИЕ
            return new CardA1(); // подстав. карточку А
        }
        public override AbstractProductB CreateProductB(){
            return new CardB1();
        }
    }
    // карточку а нужно создать
    // "CardA1"
    class CardA1 : AbstractProductA {}
    // "CardB1"
    class CardB1 : AbstractProductB{
        public override void Interact(AbstractProductA a){
            MessageBox.Show(this.GetType().Name +
            " interacts with " + a.GetType().Name);
        }
    }
    class BankVTB : AbstractFactory { // специф опред - опред 1 фабрику
        public override AbstractProductA CreateProductA() { // замещаемый мтеод
            return new CardVTBA(); // подстав. карточку А
        }
        public override AbstractProductB CreateProductB(){
            return new CardB1();
        }
    }
    class CardVTBA : AbstractProductA { }

    // "Client" - the interaction environment of the products
    // Продукты автоматом создаются
    class Client{
        public AbstractProductA abstractProductA { set; get; }
        public AbstractProductB abstractProductB { set; get; }
        // Constructor
        public Client(AbstractFactory factory) { //  использование интерфейса создания продуктов AF
            abstractProductB = factory.CreateProductB();
            abstractProductA = factory.CreateProductA();
        }
        public void Run(){
            abstractProductB.Interact(abstractProductA);
        }
    }
}

