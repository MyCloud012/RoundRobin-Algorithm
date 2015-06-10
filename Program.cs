using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SawafOS
{
    class Program
    {
        private FileManager fileManager = new FileManager();
        private object lockObj = new object();


        public void FCFS()
        {
            Thread firstThread = new Thread(FirstThread);
            firstThread.Start();

            Thread secondThread = new Thread(SecondThread);
            secondThread.Start();

            Thread thirdThread = new Thread(ThirdThread);
            thirdThread.Start();

            Thread fourthThread = new Thread(FourthThread);
            fourthThread.Start();

            Thread fifthThread = new Thread(FifthThread);
            fifthThread.Start();

        }

        private void FirstThread()
        {
            lock (lockObj)
            {
                for (int i = 0; i < 5; i++)
                {
                    fileManager.WriteIntoFile(i + " Written by Thread #1");
                }
            }
        }


        private void SecondThread()
        {
            lock (lockObj)
            {
                fileManager.WriteIntoFile("\n");
                for (int i = 0; i < 10; i++)
                {
                    fileManager.WriteIntoFile(i + " Written by Thread #2");
                }
            }
        }


        private void ThirdThread()
        {
            lock (lockObj)
            {
                fileManager.WriteIntoFile("\n");
                for (int i = 0; i < 15; i++)
                {
                    fileManager.WriteIntoFile(i + " Written by Thread #3");
                }
            }
        }


        private void FourthThread()
        {
            lock (lockObj)
            {
                fileManager.WriteIntoFile("\n");
                for (int i = 0; i < 20; i++)
                {
                    fileManager.WriteIntoFile(i + " Written by Thread #4");
                }
            }
        }


        private void FifthThread()
        {
            lock (lockObj)
            {
                fileManager.WriteIntoFile("\n");
                for (int i = 0; i < 25; i++)
                {
                    fileManager.WriteIntoFile(i + " Written by Thread #5");
                }
            }
        }

        static void Main(string[] args)
        {

            //Program program = new Program();
            //program.FCFS();
            RoundRobin rr = new RoundRobin();
            rr.doRoundRobin();
            Console.ReadLine();
        
        }
    }
}
