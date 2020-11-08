using RetroGraph.Application.HiddenLine.Model;
using RetroGraph.Application.Mathmatics;
using System.Collections.Generic;

namespace RetroGraph.Application.HiddenLine.Services
{
    public static class LineCutting
    {
        public static IEnumerable<LineHL> CutLines(this IEnumerable<LineHL> lines)
        {
            var source = new Stack<LineHL>(lines);
            var target = new Stack<LineHL>();
            var cutLines = new Stack<LineHL>();

            while (source.Count > 0)
            {
                var first = source.Pop();

                // Testcode
                while (source.Count > 0)
                {
                    var second = source.Pop();
                    var (hasIntersection, x, y) = IntersectionMath.Check2DLineWithLine(first.Start.X, first.Start.Y, first.End.X, first.End.Y, second.Start.X, second.Start.Y, second.End.X, second.End.Y);
                    if (hasIntersection)
                    {
                        var intersection = new PointHL(x, y);
                        var first1 = new LineHL { Start = first.Start, End = intersection, Edge = first.Edge };
                        var first2 = new LineHL { Start = intersection, End = first.End, Edge = first.Edge };
                        var second1 = new LineHL { Start = second.Start, End = intersection, Edge = second.Edge };
                        var second2 = new LineHL { Start = intersection, End = second.End, Edge = second.Edge };
                        target.Push(first2);
                        target.Push(second1);
                        target.Push(second2);
                        first = first1;
                    }
                    else
                    {
                        target.Push(second);
                    }
                }
                cutLines.Push(first);

                var help = source;
                source = target;
                target = help;
            }

            return cutLines;
        }
    }
}
