﻿using System;
using System.IO;

namespace SleepData
{
    class Program
    {
        static void Main(string[] args)
        {
             // ask for input
            Console.WriteLine("Enter 1 to create data file.");
            Console.WriteLine("Enter 2 to parse data.");
            Console.WriteLine("Enter anything else to quit.");
            // input response
            string resp = Console.ReadLine();

            if (resp == "1")
            {
                // create data file

                 // ask a question
                Console.WriteLine("How many weeks of data is needed?");
                // input the response (convert to int)
                int weeks = int.Parse(Console.ReadLine());

                // determine start and end date
                DateTime today = DateTime.Now;
                // we want full weeks sunday - saturday
                DateTime dataEndDate = today.AddDays(-(int)today.DayOfWeek);
                // subtract # of weeks from endDate to get startDate
                DateTime dataDate = dataEndDate.AddDays(-(weeks * 7));
                Console.WriteLine(dataDate);

                // random number generator
                Random rnd = new Random();

                // create file
                StreamWriter sw = new StreamWriter("data.txt");

                // loop for the desired # of weeks
                while (dataDate < dataEndDate)
                {
                    // 7 days in a week
                    int[] hours = new int[7];
                    for (int i = 0; i < hours.Length; i++)
                    {
                        // generate random number of hours slept between 4-12 (inclusive)
                        hours[i] = rnd.Next(4, 13);
                    }
                    // M/d/yyyy,#|#|#|#|#|#|#
                    //Console.WriteLine($"{dataDate:M/d/yy},{string.Join("|", hours)}");
                    sw.WriteLine($"{dataDate:M/d/yyyy},{string.Join("|", hours)}");

                    // add 1 week to date
                    dataDate = dataDate.AddDays(7); 
                }
                sw.Close();
            }
            else if (resp == "2")
            {
                // TODO: parse data file
                //Stream reader to get data off file
                StreamReader sr = new StreamReader("data.txt"); 
                
                while(!sr.EndOfStream){
                    // split into week and hours
                    string line = sr.ReadLine(); 
                    string[] sWeek = line.Split(",");

                    // split hours into days
                    string[] sHours = sWeek[1].Split("|"); 
                    
                    //format week and hours
                    DateTime week = DateTime.Parse(sWeek[0]); 
                    double[] hours = new double[7]; 
                    for(int i = 0; i < sHours.Length; i++){
                        hours[i] = Double.Parse(sHours[i]); 
                    }
                    // Extra Credit
                    // get total and average
                    double total = 0; 
                    foreach(double hour in hours){
                        total += hour; 
                    }
                    double avg = total/7; 

                    //print   
                    Console.WriteLine($"Week of {week:MMM}, {week:dd}, {week:yyyy}"); 
                    Console.WriteLine(" Mo Tu We Th Fr Sa Su Tot Avg\n -- -- -- -- -- -- -- --- ---"); 
                    foreach(double num in hours){
                        Console.Write($"{num, 3}"); 
                    }
                    Console.WriteLine($"{total, 4} {avg, 3:f1}\n"); 
                }
                
            }
        }
    }
}
