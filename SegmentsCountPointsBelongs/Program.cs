using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace SegmentsCountPointsBelongs
{
    internal class Program
    {
        private Segment[] _segments;
        private int[] _points;

        public static void Main()
        {
            var watch = Stopwatch.StartNew();
            new Program().Run();
            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds);
        }

        private void Run()
        {
            ReadIncoming();
            Print(CountSegmentBelongings());
        }

        private void Print(int[] amountForAllPoints)
        {
            var sb = new StringBuilder();
            foreach (var pointAmount in amountForAllPoints)
            {
                sb.Append(pointAmount);
                sb.Append(" ");
            }

            Console.WriteLine(sb);
        }

        private int[] CountSegmentBelongings()
        {
            return _points.Select(p => _segments.Count(s => p >= s.Begin && p <= s.End)).ToArray();
        }

        private void ReadIncoming()
        {
            var sr = new StreamReader(@"..\..\Test.txt");
            //var pointsAndSegmentsAmount = Console.ReadLine().Split();
            var pointsAndSegmentsAmount = sr.ReadLine()?.Split();
            
            var segmentsAmount = int.Parse(pointsAndSegmentsAmount[0]);
            var pointsAmount = int.Parse(pointsAndSegmentsAmount[1]);
            _segments = new Segment[segmentsAmount];
            for (var i = 0; i < segmentsAmount; i++)
            {
                //var strSegment = Console.ReadLine().Split();
                var strSegment = sr.ReadLine()?.Split();
                _segments[i] = new Segment(int.Parse(strSegment[0]), int.Parse(strSegment[1]));
            }

            //var strPoints = Console.ReadLine().Split(); 
            var strPoints = sr.ReadLine().Split(); 
            _points = new int[pointsAmount];
            for (var i = 0; i < pointsAmount; i++)
            {
                _points[i] = int.Parse(strPoints[i]);
            }
            //
            sr.Close();
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