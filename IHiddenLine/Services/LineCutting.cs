using IGraphics.Mathmatics;
using IGraphics.Mathmatics.Extensions;
using IHiddenLineGraphics.Model;
using System.Collections.Generic;

namespace IHiddenLineGraphics.Services
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
                    var x1 = first.Start.X;
                    var y1 = first.Start.Y;
                    var x2 = first.End.X;
                    var y2 = first.End.Y;
                    var x3 = second.Start.X;
                    var y3 = second.Start.Y;
                    var x4 = second.End.X;
                    var y4 = second.End.Y;
                    if (IntersectionMath.AreLinesBoundedBoxesOverlapped(x1, y1,x2,y2,x3,y3,x4,y4))
                    {
                        var (hasIntersection, x, y) = IntersectionMath.Check2DLineWithLine(x1, y1, x2, y2, x3, y3, x4, y4);
                        if (hasIntersection)
                        {
                            var intersection = new PointHL(x, y);
                            var first1 = new LineHL { Start = first.Start, End = intersection, Edge = first.Edge };
                            var first2 = new LineHL { Start = intersection, End = first.End, Edge = first.Edge };
                            var second1 = new LineHL { Start = second.Start, End = intersection, Edge = second.Edge };
                            var second2 = new LineHL { Start = intersection, End = second.End, Edge = second.Edge };

                            if(!first2.Length.EqualsTo(0.0))
                            {
                                if(!first1.Length.EqualsTo(0.0))
                                {
                                    target.Push(first2);
                                    first = first1;
                                }
                                else
                                {
                                    first = first2;
                                }
                            }
                            else
                            {
                                first = first1;
                            }

                            if (!second1.Length.EqualsTo(0.0)) target.Push(second1);
                            if (!second2.Length.EqualsTo(0.0)) target.Push(second2);
                        }
                        else
                        {
                            target.Push(second);
                        }
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