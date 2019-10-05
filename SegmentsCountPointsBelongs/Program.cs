using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace SegmentsCountPointsBelongs
{
    internal class Program
    {
        private Segment[] _segByBegin;
        private Segment[] _segByEnd;

        public static void Main()
        {
            var watch = Stopwatch.StartNew();
            new Program().Run();
            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds);
        }

        private void Run()
        {
            var sr = new StreamReader(@"..\..\Test.txt");
            var pointsAndSegmentsAmount = sr.ReadLine().Split();
            
            var segmentsAmount = int.Parse(pointsAndSegmentsAmount[0]);
            var pointsAmount = int.Parse(pointsAndSegmentsAmount[1]);
            var segments = new Segment[segmentsAmount];
            for (var i = 0; i < segmentsAmount; i++)
            {
                var strSegment = sr.ReadLine().Split();
                segments[i] = new Segment(int.Parse(strSegment[0]), int.Parse(strSegment[1]));
            }

            _segByBegin = segments.OrderBy(s => s.Begin).ToArray();
            _segByEnd = segments.OrderBy(s => s.End).ToArray();
            var tokens = sr.ReadLine().Split();
            var points = new int[pointsAmount];
            for (var i = 0; i < pointsAmount; i++)
            {
                points[i] = int.Parse(tokens[i]);
            }

            var result = points.Select(CountOne);
            Print(result);
            sr.Close();
        }

        private void Print(IEnumerable<int> amountForAllPoints)
        {
            var sb = new StringBuilder();
            foreach (var pointAmount in amountForAllPoints)
            {
                sb.Append(pointAmount);
                sb.Append(" ");
            }

            Console.WriteLine(sb);
        }

        private int CountOne(int point)
        {
            var correctBeginAmount = 0;
            for (; correctBeginAmount < _segByBegin.Length; correctBeginAmount++)
                if (_segByBegin[correctBeginAmount].Begin > point) break;
            var wrongEndAmount = 0;
            for (;wrongEndAmount  < _segByEnd.Length; wrongEndAmount++)
                if (_segByEnd[wrongEndAmount].End > point) break;

            return correctBeginAmount - wrongEndAmount;
        }

        private struct Segment
        {
            internal readonly int Begin;
            internal readonly int End;

            public Segment(int begin, int end)
            {
                Begin = begin;
                End = end;
            }
        }
    }
}