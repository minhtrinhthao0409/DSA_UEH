﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace GiaiThuatTimKiem
{
    class Program
    {
        static int SeqSearch(int[] arr, int value)
        {
            int i = 0;
            while (arr[i] != value)
            {
                i++;
            }
            return i;
        }

        static int RecuSearch(int[] arr, int from, int value)
        {
            if (arr[from] == value)
                return from;
            else
                return RecuSearch(arr, from + 1, value);
        }

        static int RecuSearchList(List<int> arr, int value)
        {
            //Nên sao chép arr sang list mới để thao tác
            if (arr[0] == value)
                return 0;
            else
            {
                arr.RemoveAt(0);
                return 1 + RecuSearchList(arr, value);
            }
        }

        static int RecuSearchArray(Array arr, int value)
        {
            if ((int)arr.GetValue(arr.GetLowerBound(0)) == value)
                return arr.GetLowerBound(0);
            else
            {
                Array temp = Array.CreateInstance(typeof(int),
                        new int[] { arr.Length - 1 },
                        new int[] { arr.GetLowerBound(0) + 1 });
                //Array.Copy(arr, arr.GetLowerBound(0)+1, temp, temp.GetLowerBound(0), temp.Length-1);
                for (int i = arr.GetLowerBound(0) + 1;
                                    i < arr.Length; i++)
                {
                    temp.SetValue((int)arr.GetValue(i), i);
                }
                return RecuSearchArray(temp, value);
            }
        }
        //B1. Viết lại SentSearch dùng đệ quy
        static int SentSearch(int[] arr, int value)
        {
            int i = 0;
            int lastele = arr[arr.Length - 1];
            arr[arr.Length - 1] = value;
            while (arr[i] != value)
            {
                i++;
            }
            arr[arr.Length - 1] = lastele;
            if (i < arr.Length - 1)
                return i;
            else if (lastele == value)
                return arr.Length - 1;
            else
                return -1;
        }
        // B1. Sentinel Search dùng đệ quy
        static int SentSearchRecursive(int[] arr, int value, int index = 0, bool isInitialCall = true)
        {
            if (isInitialCall)
            {
                int lastEle = arr[arr.Length - 1];
                arr[arr.Length - 1] = value;
                int result = SentSearchRecursive(arr, value, 0, false);
                arr[arr.Length - 1] = lastEle;
                if (result < arr.Length - 1 || (result == arr.Length - 1 && lastEle == value))
                    return result;
                return -1;
            }

            if (index >= arr.Length)
                return arr.Length - 1;
            if (arr[index] == value)
                return index;
            return SentSearchRecursive(arr, value, index + 1, false);
        }

            
        //BinSearch
        static int BinSearch(int[] sortedarr, int value)
        {
            int left = 0, right = sortedarr.Length - 1;
            while (left <= right)
            {
                int mid = (left + right) / 2;
                if (sortedarr[mid] == value)
                    return mid;
                else if (sortedarr[mid] > value)
                {
                    //tìm bên trái
                    right = mid - 1;
                }
                else
                {
                    //tìm bên phải
                    left = mid + 1;
                }
            }
            return -1;
        }
        // B2. Binary Search dùng đệ quy
        static int BinSearchRecursive(int[] sortedarr, int value, int left, int right)
        {
            if (left > right)
                return -1;

            int mid = (left + right) / 2;

            if (sortedarr[mid] == value)
                return mid;
            else if (sortedarr[mid] > value)
                return BinSearchRecursive(sortedarr, value, left, mid - 1);
            else
                return BinSearchRecursive(sortedarr, value, mid + 1, right);
        }

        // B3. Binary Search với mid ngẫu nhiên có kiểm soát
        static int BinSearchRandomMid(int[] sortedarr, int value, int left, int right)
        {
            if (left > right)
                return -1;

            Random rand = new Random();
            // Tạo mid ngẫu nhiên trong khoảng left và right
            int mid = left + rand.Next(0, right - left + 1);

            // Đảm bảo mid vẫn giúp chia đôi không gian tìm kiếm
            if (sortedarr[mid] == value)
                return mid;
            else if (sortedarr[mid] > value)
                return BinSearchRandomMid(sortedarr, value, left, mid - 1);
            else
                return BinSearchRandomMid(sortedarr, value, mid + 1, right);
        }

        // B4. Binary Search với 2 phần tử làm mốc
        static int BinSearchTwoPivots(int[] sortedarr, int value, int left, int right)
        {
            if (left > right)
                return -1;

            if (right - left < 2) // Khi chỉ còn 1 hoặc 2 phần tử
            {
                if (sortedarr[left] == value)
                    return left;
                if (right > left && sortedarr[right] == value)
                    return right;
                return -1;
            }


            // Chia mảng thành 3 phần bằng 2 mốc
            int segment = (right - left + 1) / 3;
            int mid1 = left + segment;
            int mid2 = right - segment;

            if (sortedarr[mid1] == value)
                return mid1;
            if (sortedarr[mid2] == value)
                return mid2;

            if (value < sortedarr[mid1])
                return BinSearchTwoPivots(sortedarr, value, left, mid1 - 1);
            else if (value > sortedarr[mid2])
                return BinSearchTwoPivots(sortedarr, value, mid2 + 1, right);
            else
                return BinSearchTwoPivots(sortedarr, value, mid1 + 1, mid2 - 1);
        }



        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            
            Random rnd = new Random();
            Timing timer = new Timing();

            int size = rnd.Next(5, 10);
            int[] arr = new int[size];

            for (int i = 0; i < size; i++)
            {
                arr[i] = rnd.Next(0, 100);
            }

            Console.WriteLine("Các phần tử có trong mảng là: ");
            foreach (int num in arr)
            {
                Console.Write(num+ "\t");
            }
            Console.WriteLine("\n");
            // Chọn một giá trị  ngẫu nhiên trong mảng để tìm
            int val = arr[rnd.Next(0, size)];
            Console.WriteLine("Giá trị cần tìm:" + val);

            // 1. Đo thời gian Sequential Search
            int[] arr1 = (int[])arr.Clone();
            Console.WriteLine(new string('-', 30));
            Console.WriteLine("Sequential Search:");
            timer.startTime();
            int seqResult = SeqSearch(arr1, val);
            timer.StopTime();
            Console.WriteLine($"Vị trí: {seqResult}, Thời gian: {timer.Result().TotalMilliseconds} ms");

            Console.WriteLine();

            // 2.Đo thời gian Recursive Search với mảng
            int[] arr2 = (int[])arr.Clone();
            
            Console.WriteLine("Recursive Search:");
            timer.startTime();
            int recuResult = RecuSearch(arr2, 0, val);
            timer.StopTime();
            Console.WriteLine($"Vị trí: {recuResult}, Thời gian: {timer.Result().TotalMilliseconds} ms");


            // 3.Đo thời gian Sentinel Search
            Console.WriteLine(new string('-', 30));
            Console.WriteLine("Sentinel Search:");
            int[] arr3 = (int[])arr.Clone();
            timer.startTime();
            int sentResult = SentSearch(arr3, val);
            timer.StopTime();
            Console.WriteLine($"Vị trí: {sentResult}, Thời gian: {timer.Result().TotalMilliseconds} ms");

            Console.WriteLine();

            Console.WriteLine("RecuSentinel Search:");
            timer.startTime();
            int recuSenResult = SentSearchRecursive(arr3, val, 0);
            timer.StopTime();
            Console.WriteLine($"Vị trí: {recuSenResult}, Thời gian: {timer.Result().TotalMilliseconds} ms");

            // 4. Đo thời gian Binary Search (dùng mảng đã sắp xếp)
            Console.WriteLine(new string('-', 30));
            int[] sortedArr = (int[])arr.Clone();
            int[] indexArr = new int[size]; // tạo mảng lưu các chỉ số trong mảng gốc

            for (int i = 0; i < size; i++)
            {

                indexArr[i] = i;
            }

            Array.Sort(sortedArr, indexArr); // sắp xếp các số trong mảng, đồng thời sắp xếp các số trong mảng chỉ số
            
            Console.WriteLine("Binary Search:");
            timer.startTime();
            int binResult = BinSearch(sortedArr, val);
            timer.StopTime();
            Console.WriteLine($"Vị trí: {indexArr[binResult]}, Thời gian: {timer.Result().TotalMilliseconds} ms");

            Console.WriteLine("RecuBinary Search:");
            timer.startTime();
            int RecuBinResult = BinSearchRecursive(sortedArr, val, 0, sortedArr.Length - 1);
            timer.StopTime();
            Console.WriteLine($"Vị trí: {indexArr[RecuBinResult]}, Thời gian: {timer.Result().TotalMilliseconds} ms");

            Console.ReadLine();

        }
    }
}
