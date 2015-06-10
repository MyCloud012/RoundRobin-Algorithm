# RoundRobin-Algorithm
RoundRobin Schedulting Algorithm in C#
This implementation is based on reading from http://en.wikipedia.org/wiki/Round-robin_scheduling

Task was to :

1.	Create a file for reading and for writing.
1.	Create 5 threads; such that the first thread will write 5 words [one, two, three, four, five] in the created file in step 1. The second thread will write 10 words [one, two, three, .., ten]. The third thread will write 15 words [one, …, fifteen]. The fourth thread will write 20 words [one, .., twenty]. Finally, the fifth thread will write 25 word [one, .. twentyFive]
2.	The word written by each thread in the file should be concatenated by the thread number.
3.	In the main program, write two function for two Scheduling algorithm, FCFS, First-come-first-served (Non-Preempt) and RR : Round robin (Preempt). The quantum in the round robin is only 2 steps (words to be written). 
4.	The functions will manage the processing these five threads.
