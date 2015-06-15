using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncChart
{
    interface IPointsFunc
    {
        PointF[] CreatePoints(int amount, float range, float xBase, float yBase, long time);
    }

    class PointsFuncSin : IPointsFunc{

        public PointF[] CreatePoints(int amount, float range, float xBase, float yBase, long time)
        {
            PointF[] pts = new PointF[amount];
            float x = xBase;
            float dx = range / amount;
            for (int i = 0; i < amount;i++, x+=dx)
            {
                pts[i] = new PointF(x, (float)(Math.Sin(xBase + x + time) * 2 + yBase));
            }
            return pts;
        }
    }

    class PointsFuncRandom : IPointsFunc
    {
        protected static Random rnd = new Random();
        protected float disp;

        public PointsFuncRandom(float disp = 2)
        {
            this.disp = disp;
        }

        public virtual PointF[] CreatePoints(int amount, float range, float xBase, float yBase, long time)
        {
            PointF[] pts = new PointF[amount];
            float x = xBase;
            float dx = range / amount;
            for (int i = 0; i < amount; i++, x+=dx)
            {
                pts[i] = new PointF(x, (rnd.Next((int)(yBase - disp), (int)(yBase + disp))));
            }
            return pts;
        }
    }

    class PointsFuncDigital : PointsFuncRandom
    {
        
        public PointsFuncDigital(float disp = 2) : base(disp) {}
        public override PointF[] CreatePoints(int amount, float range, float xBase, float yBase, long time)
        {
            PointF[] pts = new PointF[amount];
            float x = xBase;
            float dx = range / amount;
            for (int i = 0; i < amount; i++, x+=dx)
            {
                pts[i] = new PointF(x, (rnd.Next(0, 2) * 2 + yBase));
            }
            return pts;
        }
    }
}
