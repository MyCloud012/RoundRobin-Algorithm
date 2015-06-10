using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


class FileManager
{

    public void WriteIntoFile(string data)
    {
        try
        {
            FileStream fs = new FileStream("myfile.txt", FileMode.Append, FileAccess.Write);
            StreamWriter sr = new StreamWriter(fs);
            /*The second argument (true) specifies an append operation. 
            The first string is appended to it. If it is empty, the file begins with that string.*/
            sr.WriteLine(data, true);
            sr.Flush();
            sr.Close();
        }
        catch (IOException e)
        {
            // Extract some information from this exception, and then 
            // throw it to the parent method.
            if (e.Source != null)
                Console.WriteLine("IOException source: {0}", e.Source);
            throw;

        }
    }



    public void readFile(string fileName)
    {
        try
        {
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);

            while (true)
            {

                string line = sr.ReadLine();
                if (line == null)
                    break;
                Console.WriteLine(line);
            }
        }


        catch (IOException e)
        {
            if (e.Source != null)
                Console.WriteLine("IOException source: {0}", e.Source);
            throw;

        }
    }
}
