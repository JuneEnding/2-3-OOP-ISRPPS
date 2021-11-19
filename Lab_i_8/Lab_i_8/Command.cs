using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

// В этой реализ. не созд. класс для каждой команды - а созд. один общий и созд объект для поданных команд
namespace Lab_i_8
{
    // Интерфейс, позволяющий Invoker-у обрабатывать РАЗНЫЕ объекты-действия,
    // Зная о них только то, что в них реализован данный интерфейс
    interface Command {
        void Execute();     // eksɪkjuːt - выполнить
        void UnExecute();   // undo      - откатить выполненное
    }
    class ConcretCommand : Command{
        // Атрибуты, необходимые обработчику: "правый операнд" и оператор
        char        @operator; 
        int         operand;   
        Calculator  calculator; // Агрегация по ссылке - ссылка на обработчик
        // здесь будет создан объект-операция
        public ConcretCommand(Calculator calculator, char @operator, int operand){
            this.calculator = calculator;
            this.@operator = @operator;
            this.operand = operand;
        } 
        // Атриб. доступа
        public char Operator{ set { @operator = value; } }
        public int  Operand{ set { operand = value; } }
        // interface 
        // обращение к калькулятору и вып. операции
        public void Execute() {
            calculator.Operation(@operator, operand);
        }
        // Вып. в другую сторону
        // Обращение к калькулятору, в нем д.б. операция выполнить
        public void UnExecute() {
            calculator.Operation(Undo(@operator), operand);
        }
        // Private helper function : приватные вспомогательные функции
        private char Undo(char @operator){
            char undo;
            switch (@operator){   // Присваиваем символы!
                case '+':
                    undo = '-';
                    break;
                case '-':
                    undo = '+';
                    break;
                case '*':
                    undo = '/';
                    break;
                case '/':
                    undo = '*';
                    break;
                default:
                    undo = ' ';
                    break;
            }
            return undo;
        }
    }// end class ConcretCommand

    // Класс, в котором выполняются вычисления
    class Calculator {
        public int curr = 0;  // Результат | "левый operand" калькулятора: (2) + 3
        public void Operation (char @operator, int operand){  // операции, которые вып ,int operand правый операнд 2 + (3), 
            switch (@operator){
                case '+':
                    curr += operand; // Прогрессивные операции 
                    break;
                case '-':
                    curr -= operand;
                    break;
                case '*':
                    curr *= operand;
                    break;
                case '/':
                    curr /= operand;
                    break;
            }
            //MessageBox.Show(" "+ curr +" , " + @operator + " , " + operand);// опер конкатенации строк
        }
    }// end class Сalculator

    // "Invoker" : вызывающий
    class User {
        // Initializers
        private Calculator _calculator = new Calculator(); // созд калькулятор
        // Настраивается на команды - интерфейс
        private List<Command> _commands = new List<Command>(); // лучше стэк! Тут через список и листание, сохр
        private int _current = 0; // Номер текущего элемента "стека" (списка)
        public int getResult() { return _calculator.curr; }
        // Делаем возврат операций
        public void Redo(int levels){
            for (int i = 0; i < levels; i++)
                if (_current < _commands.Count)
                    _commands[_current++].Execute(); // увелич. на единицу операцию - изменилось текущее значение current
        }
        // Делаем отмену операций
        public void Undo(int levels){
            for (int i = 0; i < levels; i++)
                if (_current > 0)
                    _commands[--_current].UnExecute(); // декремент current
        }
        // Вычисление операции и создание объекта-операции
        public void Compute(char @operator, int operand) {
            // Создаем команду операции и выполняем её
            Command command = new ConcretCommand(_calculator, @operator, operand);
            command.Execute();
            // Добавляем операцию к списку отмены
            _commands.Add(command);
            _current++;
        }
    }// end User

}
