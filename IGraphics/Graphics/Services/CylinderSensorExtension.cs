using IGraphics.Mathmatics;

namespace IGraphics.Graphics.Services
{
    public static class CylinderSensorExtension
    {


        //        /// <summary>
        //        /// Processes the event.
        //        /// </summary>
        //        /// <param name="inputEvent">Event, that is processed</param>
        //        /// <returns>bool  true=event is used, false=not used</returns>
        //        override protected bool Process(ViewEvent inputEvent)
        //        {
        //            bool r_isUsed = false;
        //            Position3D startOffset = new Position3D();
        //            Vector3D startDirection = new Vector3D();
        //            double startX = 0.0;
        //            double startY = 0.0;
        //            Position3D endOffset = new Position3D();
        //            Vector3D endDirection = new Vector3D();
        //            double endX;
        //            double endY;
        //            if (inputEvent.GetPrevPosition(out startX, out startY, out startOffset, out startDirection))
        //            {
        //                if (inputEvent.GetNextPosition(out endX, out endY, out endOffset, out endDirection))
        //                {
        //                    var iview = inputEvent.View;
        //                    View3D view = iview as View3D;
        //                    startOffset = view.WindowToWorld(startX, startY);
        //                    endOffset = view.WindowToWorld(endX, endY);
        //                    double sign = CalculateAngleSign(startOffset, endOffset);
        //                    double angle = sign * CalculateAngle(inputEvent);
        //                    Rotate(angle);
        //                    r_isUsed = true;
        //                }
        //            }
        //            return r_isUsed;
        //        }

        ///// <summary>
        ///// Calculates the direction of rotation.
        ///// </summary>
        ///// <param name="start">Startposition of mouse click</param>
        ///// <param name="end">Endposition of mouse click</param>
        ///// <returns></returns>
        //private double CalculateAngleSign(Position3D start, Position3D end)
        //{
        //    Matrix3D frame = AxisFrame;
        //    Matrix3D invframe = AxisFrame.Inverse();
        //    start = invframe * start;
        //    end = invframe * end;
        //    double xstart = start.X;
        //    double ystart = start.Y;
        //    double alpha = MathLib.VectorToAngle(xstart, ystart);
        //    double xend = end.X;
        //    double yend = end.Y;
        //    double beta = MathLib.VectorToAngle(xend, yend);
        //    alpha = alpha.RadToDeg();
        //    beta = beta.RadToDeg();
        //    alpha = alpha.RadToDeg();
        //    beta = beta.RadToDeg();
        //    double sign = 1.0;
        //    if (alpha > beta)
        //    {
        //        sign = -1.0;
        //    }
        //    return sign;
        //}

        //        /// <summary>
        //        /// Calculates the rotation angle.
        //        /// </summary>
        //        /// <returns></returns>
        //        private double CalculateAngle(ViewEvent inputEvent)
        //        {
        //            double angle = 0.0;
        //            Position3D startOffset = new Position3D();
        //            Vector3D startDirection = new Vector3D();
        //            double startX = 0.0;
        //            double startY = 0.0;
        //            Position3D endOffset = new Position3D();
        //            Vector3D endDirection = new Vector3D();
        //            double endX;
        //            double endY;
        //            if (inputEvent.GetPrevPosition(out startX, out startY, out startOffset, out startDirection))
        //            {
        //                if (inputEvent.GetNextPosition(out endX, out endY, out endOffset, out endDirection))
        //                {
        //                    Vector3D axis = AxisFrame * new Vector3D(0.0, 0.0, 1.0);
        //                    var iview = inputEvent.View;
        //                    View3D view = iview as View3D;
        //                    Camera3D camera = view.Camera;
        //                    Matrix3D camTrans = camera.Transform;
        //                    var camOffset = camTrans.Translation;
        //                    var camEx = camTrans.Ex;
        //                    var camEy = camTrans.Ey;
        //                    var camEz = camTrans.Ez;
        //                    double limitAngle = 25.0;
        //                    double alpha = axis.AngleWith(camEy);
        //                    alpha = alpha.Modulo2Pi();
        //                    alpha = alpha.RadToDeg();
        //                    if ((Math.Abs(alpha) < limitAngle) || (Math.Abs(alpha) > (180.0 - limitAngle)))
        //                    {
        //                        double delta = Math.Sqrt((endX - startX) * (endX - startX) + (endY - startY) * (endY - startY));
        //                        angle = delta.DegToRad();
        //                    }
        //                    else
        //                    {
        //                        var direction = (camEy & axis).Normalised() * 10000.0;
        //                        var offset = view.WindowToWorld(startX, startY);
        //                        var p1 = offset - direction;
        //                        var p2 = offset + direction;
        //                        Position3D plump = new Position3D();
        //                        if (MathLib.CalculatePerpendicularPoint(p1, p2, endOffset, endDirection, out plump))
        //                        {
        //                            view.WorldToWindow(plump, out endX, out endY);
        //                            double delta = Math.Sqrt((endX - startX) * (endX - startX) + (endY - startY) * (endY - startY));
        //                            angle = delta.DegToRad();
        //                        }
        //                    }
        //                }
        //            }
        //            return angle;
        //        }



        private static void Rotate(Body body, Vector3D axis, double angle)
{
    var rotation = Matrix44D.CreateRotation(axis, angle);
    body.Frame = rotation * body.Frame;
}

    }
}
