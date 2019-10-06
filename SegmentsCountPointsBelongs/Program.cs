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
            var pointsAndSegmentsAmount = sr.ReadLine()?.Split();
            if (pointsAndSegmentsAmount == null || pointsAndSegmentsAmount.Length == 0) return;
            
            var segmentsAmount = int.Parse(pointsAndSegmentsAmount[0]);
            var pointsAmount = int.Parse(pointsAndSegmentsAmount[1]);
            _segByBegin = new Segment[segmentsAmount];
            for (var i = 0; i < segmentsAmount; i++)
            {
                var strSegment = sr.ReadLine()?.Split();
                if (strSegment == null || strSegment.Length == 0) break;
                _segByBegin[i] = new Segment(int.Parse(strSegment[0]), int.Parse(strSegment[1]));
            }
            Array.Sort(_segByBegin, 0, _segByBegin.Length);
            
            _segByEnd = _segByBegin.OrderBy(s => s.End).ToArray();
            var tokens = sr.ReadLine()?.Split();
            if (tokens == null || tokens.Length == 0) return;
            
            var points = new int[pointsAmount];
            for (var i = 0; i < pointsAmount; i++)
                points[i] = int.Parse(tokens[i]);

            Print(points.Select(CountOne));
            sr.Close();
        }

        private static void Print(IEnumerable<int> amountForAllPoints)
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
            var correctBeginAmount = GetFirstNotFitByBeginIndex(point);
            if (correctBeginAmount < 1) return 0;
            var wrongEndAmount = GetFirstFitByEndIndex(point);
            return correctBeginAmount - wrongEndAmount;
        }

        private int GetFirstFitByEndIndex(int point)
        {
            var l = -1;
            var r = _segByEnd.Length;
            while (r > l + 1)
            {
                var m = (l + r) / 2;
                if (_segByEnd[m].End < point) l = m;
                else r = m;
            }
            return r;
        }

        private int GetFirstNotFitByBeginIndex(int point)
        {
            var l = -1;
            var r = _segByBegin.Length;
            while (r > l + 1)
            {
                var m = (l + r) / 2;
                if (_segByBegin[m].Begin <= point) l = m;
                else r = m;
            }
            return l + 1;
        }

        private struct Segment : IComparable<Segment>
        {
            internal readonly int Begin;
            internal readonly int End;

            public Segment(int begin, int end)
            {
                Begin = begin;
                End = end;
            }

            public int CompareTo(Segment other)
                => Begin.CompareTo(other.Begin);
        }
    }
}