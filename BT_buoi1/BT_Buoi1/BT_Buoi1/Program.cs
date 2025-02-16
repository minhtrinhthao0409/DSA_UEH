/*
 * Cho 1 mảng 1000 số nguyên sinh ngẫu nhiên. Tìm số nguyên lớn nhất và nhỏ nhất.
 * Dùng kĩ thuật Timing để đo tgian thực thi
 */
using System;
using System.Diagnostics;


namespace BT_Buoi1
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();

            int[]  intArr = new int[1000];

            for (int i = 0; i < intArr.Length; i++)
            {
                intArr[i] = rnd.Next();
            }
            Stopwatch stopwatch = new Stopwatch();  // Khởi tạo Stopwatch
            stopwatch.Start();

            int max = intArr[0];  
            int min = intArr[0];

            for (int i = 1; i < intArr.Length; i++) 
            {
                if (intArr[i] > max)
                    max = intArr[i];

                if (intArr[i] < min)
                    min = intArr[i];
            }
            stopwatch.Stop();  // Dừng đo thời gian

            Console.WriteLine("Max: " + max);
            Console.WriteLine("Min: " + min);
            Console.WriteLine($"Thời gian thực thi: {stopwatch.ElapsedMilliseconds} ms");

            Console.ReadLine();
        }
    }
}
