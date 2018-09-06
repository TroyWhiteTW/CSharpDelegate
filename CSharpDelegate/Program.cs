using System;

// http://uule.iteye.com/blog/1994916
namespace CSharpDelegate {
    internal class Program {
        public static void Main(string[] args) {
            // example 1
            // 创建delegate对象
            CompareDelegate cd = new CompareDelegate(Program.Compare);
            // 调用delegate
            cd(1, 2);
            // example 1

            // example 2
            // 创建delegate
            ReceiveDelegateArgsFunc(new MyTestDelegate(DelegateFunction));
            // example 2

            // example 3
            MyButton btn = new MyButton();

            //注册事件，把btn_OnClick方法压入事件队列，
            //可以+=多个，这里简单点就压入一个吧。
            btn.OnClick += new MyButton.ClickHandler(btn_OnClick);
            // example 3

            // example 4
            // 步骤2，创建delegate对象  
            MyDelegate md = new MyDelegate(Program.MyDelegateFunc);
            // 步骤3，调用delegate  
            md("sam1111");
            // example 4

            // example 5
            EventTest et = new EventTest();
            Console.Write("Please input 'a':");
            string s = Console.ReadLine();
            if (s == "a") {
                et.RaiseEvent();
            } else {
                Console.WriteLine("Error");
            }

            // example 5
        }

        // example 1
        // 声明delegate对象
        public delegate void CompareDelegate(int a, int b);

        // 欲传递的方法，它与CompareDelegate具有相同的参数和返回值类型 
        public static void Compare(int a, int b) {
            Console.WriteLine((a > b).ToString());
        }
        // example 1

        // example 2
        // 这个方法接收一个delegate类型的参数，也就是接收一个函数作为参数
        public static void ReceiveDelegateArgsFunc(MyTestDelegate func) {
            func(21);
        }

        // 欲传递的方法
        public static void DelegateFunction(int i) {
            System.Console.WriteLine("传过来的参数为: {0}.", i);
        }
        // example 2

        // example 3
        //怎么看到这个函数很熟悉吧，就是你原来双击button自动产生的代码
        public static void btn_OnClick(object sender, ButtonClickArgs e) {
            Console.WriteLine("真贱，我居然被ivy点击了！");
        }
        // example 3

        // example 4
        // 步骤1，声明delegate对象
        public delegate void MyDelegate(string name);

        // 这是我们欲传递的方法，它与MyDelegate具有相同的参数和返回值类型
        public static void MyDelegateFunc(string name) {
            Console.WriteLine("Hello, {0}", name);
        }

        // example 4
    }

    // example 2
    public delegate void MyTestDelegate(int i);
    // example 2

    // example 3
    //这里自定义一个EventArgs，因为我想知道Clicker
    public class ButtonClickArgs : EventArgs {
        public string Clicker;
    }

    public class MyButton {
        //定义一个delegate委托
        public delegate void ClickHandler(object sender, ButtonClickArgs e);

        //定义事件，类型为上面定义的ClickHandler委托
        public event ClickHandler OnClick;

        public void Click() {
            //...触发之前可能做了n多操作

            //这时触发Click事件，并传入参数Clicker为本博主ivy
            OnClick(this, new ButtonClickArgs() {Clicker = "ivy"});
        }
    }
    // example 3

    // example 5
    public class EventTest {
        // 步骤1，定义delegate对象
        public delegate void MyEventHandler(object sender, System.EventArgs e);

        // 步骤2省略
        public class MyEventCls {
            // 步骤3，定义事件处理方法，它与delegate对象具有相同的参数和返回值类型
            public void MyEventFunc(object sender, System.EventArgs e) {
                Console.WriteLine("My event is ok!");
            }
        }

        // 步骤4，用event关键字定义事件对象
        private event MyEventHandler myevent;

        private MyEventCls myecls;

        public EventTest() {
            myecls = new MyEventCls();
            // 步骤5，用+=操作符将事件添加到队列中
            this.myevent += new MyEventHandler(myecls.MyEventFunc);
        }

        // 步骤6，以调用delegate的方式写事件触发函数
        protected void OnMyEvent(System.EventArgs e) {
            if (myevent != null)
                myevent(this, e);
        }

        public void RaiseEvent() {
            EventArgs e = new EventArgs();
            // 步骤7，触发事件
            OnMyEvent(e);
        }
    }

    // example 5
}