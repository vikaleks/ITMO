using OxyPlot;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using System.Timers;

namespace PhysicsModeling1
{
    public partial class Form1 : Form
    {
        private PlotView plotView;
        private ScatterSeries movingPointSeries;
        private List<DataPoint> trajectoryPoints;
        private List<DataPoint> arcPoints;
        private int currPoint;
        private System.Timers.Timer timer;

        public Form1()
        {
            Width = 800;
            Height = 600;

            plotView = new PlotView();
            plotView.Dock = DockStyle.Fill;

            arcPoints = CalculateArcPoints();
            trajectoryPoints = CalculateTrajectoryPoints();

            var plotModel = new PlotModel { Title = "Траектория движения тела" };
            plotModel.Series.Add(CreateLineSeries(arcPoints, "Дуга", OxyColors.Green));
            plotModel.Series.Add(CreateLineSeries(trajectoryPoints, "Траектория падения", OxyColors.Blue, LineStyle.Dash));

            movingPointSeries = new ScatterSeries
            {
                MarkerType = MarkerType.Circle,
                MarkerSize = 6,
                MarkerFill = OxyColors.Red,
                Title = "Тело"
            };
            plotModel.Series.Add(movingPointSeries);

            plotView.Model = plotModel;
            Controls.Add(plotView);

            timer = new System.Timers.Timer(100);
            timer.Elapsed += Update;
            timer.Start();
        }

        private List<DataPoint> CalculateArcPoints()
        {
            List<DataPoint> points = new List<DataPoint>();
            double R = 2.0;
            double centerX = 2.0;
            double centerY = 2.0;

            for (double theta = Math.PI / 2; theta <= 4*Math.PI / 3; theta += 0.1)
            {
                double arcX = centerX + R * Math.Cos(theta);
                double arcY = centerY - R * Math.Sin(theta);
                points.Add(new DataPoint(arcX, arcY));
            }
            return points;
        }

        private List<DataPoint> CalculateTrajectoryPoints()
        {
            List<DataPoint> points = new List<DataPoint>();

            double m = 3.0;
            double R = 2.0;
            double frictionCoeff = 0.02;
            double g = 9.81;

            double theta = Math.PI/6;
            double a = g * Math.Cos(theta);

            double V = Math.Sqrt(R * a);
            double Ek = 0.5 * m * V * V;

            double F = m * g;
            double frictionF = frictionCoeff * F;
            double L = R * (Math.PI + Math.PI / 3);
            double frictionA = frictionF * L;
            double h = R * (1 - Math.Cos(Math.PI + Math.PI / 3));
            double Amg = F * h;
            
            double V0 = Math.Sqrt(2 * (Ek + Amg - frictionA) / m);
            Console.WriteLine(V0);
          

            double vx = V * Math.Cos(theta);
            double vy = V * Math.Sin(theta);

            
            double timeStep = 0.1;
            double time = 0;

            double x = arcPoints.Last().X;
            double y = arcPoints.Last().Y;

            while (y >= 0)
            {
                x = arcPoints.Last().X + vx * time;
                y = arcPoints.Last().Y + vy * time - 0.5 * g * time * time;
                if (y >= 0) points.Add(new DataPoint(x, y));
                time += timeStep;
            }

            return points;
        }

        private LineSeries CreateLineSeries(List<DataPoint> points, string title, OxyColor color, LineStyle lineStyle = LineStyle.Solid)
        {
            var series = new LineSeries
            {
                Title = title,
                Color = color,
                StrokeThickness = 2,
                LineStyle = lineStyle
            };
            foreach (var point in points)
            {
                series.Points.Add(point);
            }
            return series;
        }
        private void Update(object sender, ElapsedEventArgs e)
        {
            if (currPoint < arcPoints.Count)
            {
                var currentArcPoint = arcPoints[currPoint];
                movingPointSeries.Points.Clear();
                movingPointSeries.Points.Add(new ScatterPoint(currentArcPoint.X, currentArcPoint.Y));
            }
            else if (currPoint < arcPoints.Count + trajectoryPoints.Count)
            {
                var currentTrajectoryPoint = trajectoryPoints[currPoint - arcPoints.Count];
                movingPointSeries.Points.Clear();
                movingPointSeries.Points.Add(new ScatterPoint(currentTrajectoryPoint.X, currentTrajectoryPoint.Y));
            }
            else
            {
                timer.Stop();
                return;
            }

            plotView.InvalidatePlot(true);
            currPoint++;
        }
    }
}