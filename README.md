
# Understanding the .NET Ecosystem
It's equipped with everything you need – runtime, libraries, and languages – to develop apps for any platform, whether it's desktop, web, mobile, or more.


So, .NET is like a huge toolbox with:

- Languages: C#, F#, VB 
- Runtime: Common Language Runtime 
- CLI: Dotnet CLI
- Libraries: Base Class Libraries plus loads of third-party options via NuGet.

# .NET compilation process
💬 *Here's how it goes:*

- Developer writes C# code.
- C# compiler reviews the syntax and breaks down the code.
- Intermediate Language (IL) is produced (either as an EXE or DLL).
- CLR kicks in within a process and executes the entry point method (Main).
- JIT compiler transforms IL into native code.

![Process](https://res.cloudinary.com/practicaldev/image/fetch/s--Hvf9LEBQ--/c_limit%2Cf_auto%2Cfl_progressive%2Cq_auto%2Cw_880/https://dev-to-uploads.s3.amazonaws.com/uploads/articles/plpn3de3k6pn4eqg8zw8.png)


💬 **IL**, also known as *MSIL* or *CIL*, is a kind of code that's like a blueprint for your program. It's not specific to any CPU, so it's sort of like a universal language for .NET. When your program runs, IL gets turned into machine code based on the environment it's running in.

💬 **ILDASM** is a tool in Visual Studio that lets you peek into this **IL** code. You use it by opening the Visual Studio Command Prompt, typing "ildasm," and then exploring the code of any executable or library. It's like looking under the hood of your program.

💬 **CLR**, on the other hand, is like the engine of the .NET framework. It does four main jobs: garbage collection, security checks, translating **IL** into machine code, and making sure different programming languages can work together smoothly.

💬 **CTS** and **CLS** are like sets of rules that help different parts of your program understand each other. **CTS** makes sure data types from different languages play nicely together, while **CLS** makes sure your code follows certain guidelines so it can be used by any .NET language.

💬 Lastly, **JIT** is like the translator that turns **IL** into machine code just before your program runs. It's like having someone translate a language for you in real-time, making sure your program speaks the language of the machine it's running on.

# Manifest and Metadata
⚡️**Manifest**: Manifest describes assembly itself. Assembly Name, version number, culture, strong name, list of all files, Type references, and referenced assemblies.

⚡️**Metadata**: Metadata describes contents in an assembly classes, interfaces, enums, structs, etc., and their containing namespaces, the name of each type, its visibility/scope, its base class, the interfaces it implemented, its methods and their scope, and each method's parameters, type's properties, and so on.

# Reflection In C#
Accessing metadata at runtime is known as a reflection in C#.

When you write a C# program that uses reflection, you can use either the *TypeOf* operator or the *GetType()* method to get the object’s type.

```
int i = 15;
System.Type type = i.GetType();
System.Console.WriteLine(type);
```

The main class for reflection is the *System.Type* class, which is a partial abstract class representing a type in the *Common Type System* (CTS). When you use this class, you can find the types used in a module and namespace and also determine if a given type is a reference or value type. You can parse the corresponding metadata tables to look through these items:

💬 Fields
- Properties
- Methods
- Events

💬 *The System.Type* class also comes with several instance methods you can use to get information from a specific type. Here’s a list of some of the most important ones:

*GetConstructors()* – gets the constructors for the type as an array of *System.Reflection.ConstructorInfo*.

*GetMethods()* – gets the methods for the type as an array of System.Reflection.MethodInfo.

*GetMembers()* – gets the members for the type as an array of System.Reflection.MemberInfo.

The System.Reflection namespace, as the name suggests, holds several useful classes if you want to work with reflection. Some of those are the three ones you’ve just read about. Here are some more important ones:

- ParameterInfo
- Assembly
- AssemblyName
- PropertyInfo

**USES FOR REFLECTION C#**

- **Module:** Use Module to get all global and non-global methods defined in the module.
- **MethodInfo:** This provides details about methods, like their parameters, name, return type, access modifiers, and implementation.
- **EventInfo:** Useful for finding out details about events, such as their data type, name, declaring type, and any custom attributes.
- **ConstructorInfo**: Gives you info on constructors, like parameters, access modifiers, and implementation.
- **Assembly**: Allows you to load modules listed in the assembly manifest.
- **PropertyInfo:** Handy for getting info about properties, like their declaring type, data type, name, and whether they're writable. You can also use it to get or set property values.
- **CustomAttributeData**: Helps you learn about custom attributes or review attributes without creating more instances.

# Garbage Collection

In C#, when you make an object, CLR reserves memory for it from the heap. But since memory isn't endless, we need a way to free up space for new objects. That's where *garbage collection* comes in. The garbage collector keeps track of what objects are no longer needed by the program. It goes through the heap, finds these unused objects, and clears them out, making space available for new ones.


The *garbage collector* in C# operates on the managed heap, which is a block of memory dedicated to storing objects. When it kicks into action, it hunts down objects that are no longer needed or "dead." Then, it organizes the space occupied by live objects to free up more memory.

The managed heap is divided into different "Generations," each handling different types of objects:

- **Generation 0** (Zero): This holds short-lived objects, like temporary ones. Garbage collection happens frequently here.
- **Generation 1** (One): Acting as a buffer, it holds objects transitioning from short-lived to long-lived.
- **Generation 2** (Two): This is for long-lived objects, such as static or global variables. Objects that survive multiple rounds of garbage collection in lower generations end up here.

![Process](https://miro.medium.com/v2/resize:fit:4800/format:webp/1*oKBcjg1jwlH2MvXTDqpC2A.png)

🧠 Now let's get acquainted with the **methods** of the GC class.

**GC.Collect:** This method triggers the collection of unused objects in the heap and then calls the *Collect()* method to clean them from memory. While our program lets the GC handle collecting unused objects in the background, there might be cases where you know that many objects will soon become unused, and you can call this method.

**GC.CollectionCount:** This method indicates how many times garbage collection has occurred in a particular generation.

**GC.MaxGeneration:** This method shows the current number of generations in the system.

![Process](https://miro.medium.com/v2/resize:fit:720/format:webp/1*SKlnNfD57nLjj5uuT2Iv7w.png)

**GC.GetTotalMemory**: This method displays the amount of memory being held. In the example below, I added another object to the heap memory, and the value of TotalMemory changed accordingly.

**GC.GetGeneration:** This method shows which generation an object belongs to.

```C#
using System;
class BaseGC
{
    public void Display()
    {
        Console.WriteLine("Example Method");
    }
}
class GCExample2
{
    public static void Main(string[] args)
    {
        try
        {
            Console.WriteLine("Total Memory:" + GC.GetTotalMemory(false));
            BaseGC oBaseGC = new BaseGC(); 
            Console.WriteLine("BaseGC Generation is :" + GC.GetGeneration(oBaseGC)); //Returns the current generation number of an object.
            Console.WriteLine("Total Memory:" + GC.GetTotalMemory(false)); 
        }
        catch (Exception oEx)
        {
            Console.WriteLine("Error:" + oEx.Message);
        }
    }
}
```

![Process](https://miro.medium.com/v2/resize:fit:720/format:webp/1*sDn2U8CHXZ8VlnWLHn4MWg.png)


⚡️**FINALIZER**

*Finalize* also known as destructors, are methods that we cannot call ourselves; they are only called by the GC. Let me explain finalizers with the following code example:

Let's say you have a class called Student, and you create two objects from it. We assign the address of the first object to a pointer called "stu2". So, "stu1" no longer holds the address of the first object; instead, it holds the address of the second object. Since we can no longer access the first object through "stu1", there's no point in keeping it in memory. That's why it will be deleted by the GC. We don't know exactly when the GC mechanism will kick in; it works in the background.

A finalizer is a method that executes before an object is deleted by the GC. Its name must be the same as the class name and prefixed with the tilde (~) symbol.

![Process](https://miro.medium.com/v2/resize:fit:640/format:webp/1*T7qYLNzb3ZcBDBKahpOLYg.gif)


```C#
using System;

class Program
{
    static void Main()
    {
        Student stu1 = new Student();
        Student stu2 = new Student();
        stu1 = stu2;
    }
}
class Student
{
    public static int StCounter = 0;
    public string Name { get; set; }
    public Student()
    {
        StCounter++;
    }
    ~Student()   //Finalizer
    {
        StCounter--;
    }
}
```



# Threads

Before diving into threads, let's get familiar with some concepts.

**Multitasking** is the ability of a computer to execute multiple programs simultaneously, allowing users to perform multiple tasks at the same time. For example, a user can write a blog post while listening to music playing in the background. This allows the computer to handle many tasks for the user. Multitasking enables the CPU to perform multiple tasks simultaneously. We can consider each program as a process. However, the difference between a program and a process is that when a program is active, it becomes a process. For example, when we open Visual Studio, it becomes a process.

![Process](https://miro.medium.com/v2/resize:fit:640/format:webp/1*IT1r2bMD5n_d1xlmsrmudw.png)

MultiThreading allows multiple threads to execute within a single process. For example, in my program, I have several algorithms, and I can execute each one in a separate thread. As shown in the diagram...

![Process](https://miro.medium.com/v2/resize:fit:640/format:webp/1*A5_kOcgmKQfL1oqWTAO_xQ.png)

Each thread has its own set of registers and a stack. By default, there is one thread within a process. For example, if there are 3 threads within a process, each will have its own stack and registers (as shown in the diagram). We can consider the main difference between processes and threads as threads utilizing shared memory, while processes have their own memory space. In other words, a process cannot access the memory space of other processes.

![Process](https://miro.medium.com/v2/resize:fit:640/format:webp/1*EuPVNRE7TeAY10RYw9aMqA.png)


**Threads** are used when we want to execute something in parallel. In C# 7.0, to create a thread, we use the Thread class, which accepts a parameter (the WriteY() method, for example). We must always include the "*Start()*" method to trigger the thread. The other "*for*" loop is executed by the main thread. If we run this code block multiple times, we'll get different answers each time. This is because the CPU decides which thread to start first and when to switch to another thread after executing for a certain amount of time.

![Process](https://miro.medium.com/v2/resize:fit:640/format:webp/1*MUM6tyaZQ-c4LlTlXaCXKw.png)

In C#, there are two types of threads: **Foreground Threads** and **Background Threads**.

*Background Thread*: After the main thread finishes its work, a background thread also finishes its work. The lifetime of a background thread depends on the main thread. Normally, when IsBackground=false, it is equivalent to a background thread. If I set IsBackground=true for one of the threads, it becomes a background thread.

*Foreground Threads:* These threads are not dependent on the main thread. By default, IsBackground=false for foreground threads.

```C#
using System;
using System.Threading;

public class Program
{
    static void Main()
    {
        Thread t = new Thread(WriteY);
        t.IsBackground = true;
        t.Start();
        for (int i = 0; i < 100; i++) Console.WriteLine("x");
    }
    static void WriteY()
    {
        for (int i = 0; i < 1000; i++) Console.WriteLine("y");
    }
}
```

Let's discuss some **fundamental methods** used in threads.

**Abort()**: When called on a thread, it starts the process of terminating that thread by raising a ThreadAbortException. Generally, this method terminates the thread altogether.

![Process](https://miro.medium.com/v2/resize:fit:640/format:webp/1*vRHwiocZbGc_GWANJDaf0g.png)

**Join()**: This method causes one thread to wait for the execution of another thread to complete before proceeding.

![Process](https://miro.medium.com/v2/resize:fit:640/format:webp/1*0J4uxbTtF9D10Ljm6EevbA.png)

**Thread.Sleep()**: This method pauses the current thread for a specified number of milliseconds. In other words, it tells the current thread to wait for a certain amount of time.

![Process](https://miro.medium.com/v2/resize:fit:640/format:webp/1*_swJwCAyqdLYQ2SRsRyigg.png)

**Thread Priority**: Specifies how much execution time a thread gets relative to other active threads in the operating system. In the example below, I set the main thread's priority to "Lowest" and the other thread's priority to "Highest." Naturally, the thread labeled "t" will output more "x" characters to the screen.

![Process](https://miro.medium.com/v2/resize:fit:640/format:webp/1*NjajJmf7rVaMCS_Kg62YPQ.png)

Before we dive into the **ThreadPool**, let's understand the life cycle of a thread. When we create a thread object, it allocates 1 megabyte of memory for that thread, executes the assigned task, and then, upon completion, the thread is deleted by the garbage collector.

The *ThreadPool*, on the other hand, does not delete thread objects from memory after they finish their tasks; instead, it keeps them stored in the pool. Essentially, the ThreadPool is a collection of reusable threads. This reuse of threads helps in saving memory because we don't allocate 1 megabyte of memory every time we need a thread, and we can utilize existing threads whenever needed.

However, there are some drawbacks to using the *ThreadPool*. The threads in the ThreadPool are unmanaged, meaning we cannot assign names or priorities to them. They are primarily used for performing tasks and not for any specific thread management. In the example below, since the threads in the ThreadPool are background threads, after the main thread finishes its work, the threads in the ThreadPool are considered as dead threads.



```C#
using System;
using System.Threading;

class ThreadPoolSample
{
    // Background task   
    static void BackgroundTask(Object stateInfo)
    {
        Console.WriteLine("Hello! I'm a worker from ThreadPool");
    }

    static void Main(string[] args)
    {
        // Use ThreadPool for a worker thread        
        ThreadPool.QueueUserWorkItem(BackgroundTask);
        Console.WriteLine("Main thread does some work, then sleeps.");
        Console.WriteLine("Main thread exits.");
    }
}
```


Let's talk about **Tasks**, which are part of the *System.Threading* namespace. Tasks are used for parallel execution. The .NET framework provides us with the Threading.Tasks class, which allows us to create tasks and manage them asynchronously using the "async" and "await" keywords. Unlike threads, tasks can return a result when they complete their work. A task can handle multiple processes simultaneously, whereas threads can only execute one task at a time.

**Asynchronous** programming with the *"async"* and *"await"* keywords is widely popular. So, when might we need asynchronous programming? Imagine you are downloading a large file. In this scenario, the entire application must wait until the download completes. In other words, if any process in a synchronous application blocks, the entire application blocks, and the application doesn't respond until all tasks are completed. Asynchronous programming helps in such situations. By using asynchronous programming, the application can continue performing other tasks that are not dependent on the completion of the entire operation.

![Process](https://miro.medium.com/v2/resize:fit:640/format:webp/1*xj4xXzzInf_cB1W94ITQ3Q.png)

Now, let's create an **asynchronous application**. In this example, we will read all characters from a large text file asynchronously and obtain the total length of all characters in the text document. In the code below, we call the ReadFile method to read the content of the text file and obtain the total length of all characters in the text document. The ReadFile method begins reading the file, and during this time, the program continues its execution without any freezing. Once the reading is complete, the length of the characters in the file is recorded, and the program continues its execution.


```C#
using System;
using System.IO;
using System.Threading.Tasks;
class Program
{
    static void Main()
    {
        Task task = new Task(CallMethod);
        task.Start();
        task.Wait();
        Console.ReadLine();
    }

    static async void CallMethod()
    {
        string filePath = "D:\\simple.txt";
        Task<int> task = ReadFile(filePath);

        Console.WriteLine(" Other Work 1");
        Console.WriteLine(" Other Work 2");
        Console.WriteLine(" Other Work 3");

        int length = await task;
        Console.WriteLine(" Total length: " + length);

        Console.WriteLine(" After work 1");
        Console.WriteLine(" After work 2");
    }

    static async Task<int> ReadFile(string file)
    {
        int length = 0;

        Console.WriteLine(" File reading is stating");
        using (StreamReader reader = new StreamReader(file))
        {
            // Reads all characters from the current position to the end of the stream asynchronously    
            // and returns them as one string.    
            string s = await reader.ReadToEndAsync();

            length = s.Length;
        }
        Console.WriteLine(" File reading is completed");
        return length;
    }
}
```

![Process](https://miro.medium.com/v2/resize:fit:640/format:webp/1*Wo2FLsySkCGXsVTsy7wcpg.png)


# Paradigms

🧠 C# is a powerful and many-sided language due to its **multi-paradigm** nature. This means it allows programmers to approach problems from different angles using various programming styles. Here’s a breakdown of some paradigms C# supports:

C# supports various programming styles, making it versatile. Here are some key *paradigms* it offers:

**Object-Oriented** (OO): Focuses on classes and objects as the main building blocks. C# provides features like inheritance and polymorphism for this paradigm.

**Imperative**: Involves giving step-by-step instructions to the computer. C# excels in this with control flow statements and methods for data manipulation.

**Functional**: Emphasizes functions and immutable data. C# supports this with lambda expressions, higher-order functions, and LINQ.

**Generic**: Allows writing code that works with different data types without rewriting it. C# generics improve code reusability.

![Process](https://miro.medium.com/v2/resize:fit:1400/1*FFOmWawwaBGQi4hHE-sn6A.png)

C# also supports concurrent programming and metaprogramming concepts.

The benefit of C# being multi-paradigm is that you can choose the best approach for each problem. For example, you might use OO for designing user interfaces and functional programming for data processing tasks.

I'm planning to write a series of tutorials on the functional aspects of C#. Let's start by exploring its functional features, including LINQ, delegates, method extensions, immutability, and method chaining.

**FUNCTIONAL PROGRAMMING**


*Functional programming* in C# relies heavily on mathematical functions. It's all about using functions as the main building blocks of your code, treating them like variables that can be passed around, assigned, and returned.

💬 Here are some benefits and key elements:

**Benefits**:

- *Immutability*: Data shouldn't change after creation, reducing bugs and improving thread safety.
- *Pure Functions*: Functions with no side effects make behavior more predictable and testing easier.
- *Composability*: Small, clear functions can be combined to create complex operations, making code more reusable and maintainable.
- *Declarative Style*: Focuses on what the program needs to do rather than how to do it, improving readability and reducing boilerplate code.

**Key Elements** in C#:

- *Lambda Expressions*: Quick and concise anonymous functions for short logic blocks or callbacks.
- *Higher-order Functions*: Functions that take other functions as arguments or return functions as results, enabling powerful abstractions.
- *LINQ*: A functional approach to querying data using C# syntax, simplifying data processing tasks.
- *Immutable Collections*: Thread-safe collections that can't be modified after creation, aligning with functional principles.

One standout functional feature in C# is **LINQ**, short for Language Integrated Query. It's a powerful toolset that lets you query data directly within your C# code, using a syntax similar to SQL statements.

With LINQ, you can query various data sources like in-memory collections, databases, and XML files, all using the same approach. This simplifies development since you don't need to learn multiple query languages.

LINQ works directly with objects, which enhances code readability and maintainability compared to traditional string-based queries.

Another notable aspect of LINQ is its heavy use of extension methods, which are based on functional programming principles.

```C#
            List<Account> accounts = new List<Account>()
                {
                    new Account { Name = "John Doe", City = "New York" },
                    new Account { Name = "Jane Doe", City = "Los Angeles" },
                    new Account { Name = "Alice Smith", City = "New York" },
                    new Account { Name = "Bob Smith", City = "Chicago" }
                };

            // Group accounts by city and count the number of accounts in each group
            var accountGroups = accounts.GroupBy(account => account.City)
                                         .Select(cityGroup => new { City = cityGroup.Key, Count = cityGroup.Count() });

            // Print the city and account count for each group
            foreach (var group in accountGroups)
            {
                Console.WriteLine($"City: {group.City}, Count: {group.Count}");
            }


```

**DELEGATES**

*Delegates* in C# are like pointers to methods. They're essential for tasks like event handling and callbacks. *Delegates* reference methods with specific signatures, acting as a safer, more object-oriented version of C++ function pointers. In simpler terms, you can think of *delegates* as sending one method as an argument to another method.  
          
          Func<decimal, decimal> multiply = x => x * 5;
           multiply(45);


In practice, we usually don't create delegates every time. The .NET Framework itself provides two types of delegates: **Action** and **Func** delegates.

**Action delegate**: It doesn't have a return type, meaning it doesn't return any value. It can accept around 16 parameters. Using Action delegates reduces the length of the code and makes the program more readable.

```
using System;

namespace DelegateAction
{
    class Program
    {
        static void Main(string[] args)
        {
            Action<string, int> writer = delegate (string name, int age) {
                Console.WriteLine($"Your name: {name}, age: {age}");
            };
            //Lambda exp
            writer += (n, a) =>
            {
                Console.WriteLine($"new word: {n}, new number: {a}");
            };

            writer("Hamid", 19);
        }
    }
}
```
**Func delegate**: It has a return type and returns a single value at the end. Like Action delegates, it can also accept around 16 parameters. In the example below, we created a Func delegate with an input parameter of type "Person" and an output parameter (bool). When we run the program, we see that it returns "false" at the end because the last method executed returns false due to the absence of the "@" symbol in the email. The main purpose of Func delegates is to execute the program logic and return a single value at the end, so there's no need to write multiple methods here.

```
using System;

namespace DelegateFunc
{
    class Program
    {
        static void Main(string[] args)
        {
            Func<Person, bool> checker = CheckFromAzerbaijan; //check named method
            //anonymous
            checker += delegate (Person p) { return p.Name.Length > 2; };
            //lambda exp
            checker += person => person.Email.Contains("@");
            //call to delegate
            Console.WriteLine(checker(new Person
            {
                Name = "Hamid",
                Email = "hamidbaydamirov123.gmail.com",
                Country = "Azerbaijan",
            })) ;
        }
        //Named method
        static bool CheckFromAzerbaijan(Person p) { return p.Country == "Azerbaijan"; }

    }

    class Person
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public string Country { get; set; }

    }
}
```
**Event**: It allows one class or object to notify other classes or objects when something happens. For example, when you subscribe to a YouTube channel, you receive notifications when new videos are uploaded to the channel. An event occurs here. Events require methods that subscribe to this event. What type can be connected to multiple methods? For this, we'll use delegates.

```
using System;

namespace EventHandler
{
    class Program
    {
        static void Main()
        {
            var registerUser = new RegisterUser();
            registerUser.Name = "Hamid";

            registerUser.registerUserEvent += EmailVerification; //subscribe to an event  
            registerUser.registerUserEvent += SMSVerification; //subscribe to an event  
            registerUser.RegisterAUser(); // publisher  


            Console.ReadLine();
        }
        public static void EmailVerification(object source, RegisterUserEventArgs e)
        {
            Console.WriteLine($"Sent Email for Verification {e.NameArg}");
        }

        public static void SMSVerification(object source, RegisterUserEventArgs e)
        {
            Console.WriteLine($"Sent SMS for Verification {e.NameArg}");
        }

    }
    public class RegisterUser
    {
        public string Name { get; set; }
        //created Event
        public delegate void RegisterEventHandler(object sender, RegisterUserEventArgs e);
        //Our delegate sent to Event
        public event RegisterEventHandler  registerUserEvent;

        public void RegisterAUser()
        {
            Console.WriteLine($"{Name} registered");
            if (registerUserEvent != null)
            {
              
                registerUserEvent(this, new RegisterUserEventArgs(Name)); // call event  
            }
        }
    }
    // Created EventArgs 
    public class RegisterUserEventArgs : EventArgs
    {
        public string NameArg { get; set; }
        //constructor
        public RegisterUserEventArgs(string name)
        {
            NameArg = name;
        }
    }
}

```
