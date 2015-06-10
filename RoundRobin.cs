using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace RoundRobin
{
    class RoundRobin
    {
        private static Process firstProcess = new Process(1,2);
        private static Process secondProcess = new Process(2,4);
        private static Process thirdProcess = new Process(3,6);
        private static Process fourthProcess = new Process(4,8);
        private static Process fifthProcess = new Process(5,11);


        private Thread firstThread = new Thread(() => firstProcess.processWriteIntoFile(firstProcess.getNumberOfWords()));
        private Thread secondThread = new Thread(() => secondProcess.processWriteIntoFile(secondProcess.getNumberOfWords()));
        private Thread thirdTread = new Thread(() => thirdProcess.processWriteIntoFile(thirdProcess.getNumberOfWords()));
        private Thread fourthThread = new Thread(() => fourthProcess.processWriteIntoFile(fourthProcess.getNumberOfWords()));
        private Thread fifthThread = new Thread(() => fifthProcess.processWriteIntoFile(fifthProcess.getNumberOfWords()));



        // Queue of Processes that wants to work.
        private Queue<Process> queueOfProcesses = new Queue<Process>();
        // Queue of Threads which will handle each process (each process shall run on an independent thread).
        private Queue<Thread> queueOfThreads = new Queue<Thread>();
        // Lock object, something to contest about.
        private object lockObj = new object();


        private bool ProcessesDone()
        {
            return firstProcess.isDone() && secondProcess.isDone() && thirdProcess.isDone() && fourthProcess.isDone() && fifthProcess.isDone();
        }

        private void ContinueProcessJob(Process working, Thread workingThread)
        {
            workingThread = new Thread(() => working.processWriteIntoFile(working.getNumberOfWords()));
            workingThread.Start();
        }

        private void StartProcess(Process working, Thread workingThread)
        {
            // If the process did not yet finish its current work, let it continue
            if (!working.getTempDone())
            {
                // In the very 1st iteration this if condition not necessary, but starting from iteration N+1 it matters
                // Usually the thread goes into the "Stopped" state once it finishes its task within the assoicated time slice
                // So, if it stopped we instaninate a new one and let it work, otherwise its a brand new thread so we just start it for the 1st time
                if (workingThread.ThreadState != ThreadState.Stopped)
                {
                    Console.WriteLine("Thread of process: " + working.getProcessID() + " state before starting: " + workingThread.ThreadState);
                    workingThread.Start();
                    Console.WriteLine("Thread of process #: " + working.getProcessID() + " is starting.");
                }

                else if (workingThread.ThreadState == ThreadState.Stopped)
                {
                    ContinueProcessJob(working, workingThread);
                }
            }
        }

        private void DoProcessJob(Process working, Thread workingThread)
        {
            // If this thread did not exceed its time quantum limit, go a head and give it a chance to work
            if (working.getTimeQuantum() > 0)
            {
                // Secure this working zone so no other threads can interupt us
                lock (lockObj)
                {
                    StartProcess(working, workingThread);
                }
            }
        }


        private void EnqueueProcesses()
        {
            queueOfProcesses.Enqueue(firstProcess);
            queueOfProcesses.Enqueue(secondProcess);
            queueOfProcesses.Enqueue(thirdProcess);
            queueOfProcesses.Enqueue(fourthProcess);
            queueOfProcesses.Enqueue(fifthProcess);
        }

        private void EnqueueThreads()
        {

            queueOfThreads.Enqueue(firstThread);
            queueOfThreads.Enqueue(secondThread);
            queueOfThreads.Enqueue(thirdTread);
            queueOfThreads.Enqueue(fourthThread);
            queueOfThreads.Enqueue(fifthThread);
        }

        private void SetProcessTimeQuantum(Process working, int time)
        {
            // Basically here i'm assigning a Time quantum for this thread of "2".
            working.setTimeQuantum(working.getTimeQuantum() + time);
            working.setStartPoint(working.getTimeQuantum() - time);
        }

        private void SleepOurMainThread(Process working)
        {
            // This disables the Main Thread from working while the sub-thread is working
            // Because the main thread will iterate this entire loop and dequeue another working process & thread if we did not tell him
            // We are busy, so wait and stay here till we are really done.
            while (!working.getTempDone())
            {
                Thread.Sleep(1000);
            }
        }

        private void CheckIfProcessStillHaveJobLeft(Process working, Thread workingThread, Queue<Process> queueOfProcesses, Queue<Thread> queueOfThreads)
        {
            // Here we check if have done our time sliced job, but yet, we are not completely done (we need more time slices)
            // Put the process at the end of working processes queue, also put the Thread which handle our process at the end of Threads queue.
            // Otherwise, the thread time slice was equal or less than its burst time (numberOfWordsToType), so we dont schedule it again.
            if (working.getTempDone() && !working.isDone())
            {
                queueOfProcesses.Enqueue(working);
                queueOfThreads.Enqueue(workingThread);
                Console.WriteLine("Enqueuing: " + working.getProcessID() + "\n");
            }
        }

        public void doRoundRobin()
        {
            EnqueueProcesses();
            EnqueueThreads();

            Process working;
            Thread workingThread;

            while (!ProcessesDone())
            {
                working = queueOfProcesses.Dequeue();
                workingThread = queueOfThreads.Dequeue();

                SetProcessTimeQuantum(working, 2);
                DoProcessJob(working, workingThread);
                SleepOurMainThread(working);
                CheckIfProcessStillHaveJobLeft(working, workingThread, queueOfProcesses, queueOfThreads);

            }
        }

    }

}
