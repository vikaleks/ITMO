using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.WindowsForms;

namespace PhysicsModeling2;
public partial class Form1 : Form
{
    private TabControl tabControl;

    public Form1()
    {
        Text = "Движение электрона в цилиндрическом конденсаторе";
        Width = 1000;
        Height = 800;

        tabControl = new TabControl { Dock = DockStyle.Fill };
        Controls.Add(tabControl);

        AddPlotTab("y(x)", CreateYxPlotModel());
        AddPlotTab("Vy(t)", CreateVyTPlotModel());
        AddPlotTab("ay(t)", CreateAyTPlotModel());
        AddPlotTab("y(t)", CreateYTPlotModel());
    }

    private void AddPlotTab(string title, PlotModel model)
    {
        var tabPage = new TabPage { Text = title };
        var plotView = new PlotView { Dock = DockStyle.Fill, Model = model };
        tabPage.Controls.Add(plotView);
        tabControl.TabPages.Add(tabPage);
    }

    private PlotModel CreateYxPlotModel()
    {
        double r = 0.065; 
        double R = 0.14; 
        double V0 = 3e6;
        double L = 0.22;
        double e = 1.6e-19;
        double m = 9.11e-31;
        double d = R - r;
        
        double Umin = (m/e)*(d*V0/L)*(d*V0/L); // минимальная разность потенциалов
        Console.WriteLine("Минимальная разность потенциалов: " + Math.Round(Umin, 2) + " В");
        
        double tMax = L / V0;
        Console.WriteLine("Время полета: " + tMax + " с");
        
        double E = Umin / d; // Напряженность поля
        double ay = e * E / m; // Ускорение
        double Vy = ay * tMax;
        double Vkon = Math.Sqrt(Math.Pow(V0,2) + Math.Pow(Vy,2));
        Console.WriteLine("Конечная скорость: " + Math.Round(Vkon, 2) + " м/с");
        

        // y(x)
        var yx = new LineSeries { Title = "y(x)" };
        double timeStep = 1e-11;

        for (double t = 0; t <= tMax; t += timeStep)
        {
            double x = V0 * t;
            double y = 0.5 * ay * t * t;
            yx.Points.Add(new DataPoint(x, y));
        }

        var plotModel = new PlotModel { Title = "График y(x)" };
        plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "x (м)" });
        plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "y (м)" });
        plotModel.Series.Add(yx);

        return plotModel;
    }

    private PlotModel CreateVyTPlotModel()
    {
        double r = 0.065;
        double R = 0.14;
        double V0 = 3e6;
        double L = 0.22;
        double e = 1.6e-19;
        double m = 9.11e-31;
        double d = R - r;
        double Umin = (m/e)*(d*V0/L)*(d*V0/L);
        double tMax = L / V0;
        double E = Umin / d;
        double ay = e * E / m;
        double timeStep = 1e-11;

        // Vy(t)
        var vy_t = new LineSeries { Title = "Vy(t)" };
        for (double t = 0; t <= tMax; t += timeStep)
        {
            double vy = ay * t;
            vy_t.Points.Add(new DataPoint(t, vy));
        }

        var plotModel = new PlotModel { Title = "График Vy(t)" };
        plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "t (с)" });
        plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Vy (м/с)" });
        plotModel.Series.Add(vy_t);

        return plotModel;
    }

    private PlotModel CreateAyTPlotModel()
    {
        double r = 0.065;
        double R = 0.14;
        double V0 = 3e6;
        double e = 1.6e-19;
        double m = 9.11e-31;
        double L = 0.22;
        double d = R - r;
        
        double Umin = (m/e)*(d*V0/L)*(d*V0/L);
        double E = Umin / d;
        double ay = -(e/m) * (Umin/Math.Log(R/r));
        

        // ay(t)
        var ay_t = new LineSeries { Title = "ay(t)" };
        double timeStep = 1e-11;
        double tMax = L/V0;
        for (double t = 0; t <= tMax; t += timeStep)
        {
            ay_t.Points.Add(new DataPoint(t, ay));
        }

        var plotModel = new PlotModel { Title = "График ay(t)" };
        plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "t (с)" });
        plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "ay (м/с²)" });
        plotModel.Series.Add(ay_t);

        return plotModel;
    }

    private PlotModel CreateYTPlotModel()
    {
        double r = 0.065;
        double R = 0.14;
        double V0 = 3e6;
        double L = 0.22;
        double e = 1.6e-19;
        double m = 9.11e-31;

        double d = R - r;
        double Umin = (m / e) * (d * V0 / L) * (d * V0 / L);
        double timeStep = 1e-11;
        double tMax = L / V0;
        double E = Umin / d;
        double ay = e * E / m;
        
        // y(t)
        var yt = new LineSeries { Title = "y(t)" };
        for (double t = 0; t <= tMax; t += timeStep)
        {
            double y = 0.5 * ay * t * t;
            yt.Points.Add(new DataPoint(t, y));
        }

        var plotModel = new PlotModel { Title = "График y(t)" };
        plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "t (с)" });
        plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "y (м)" });
        plotModel.Series.Add(yt);

        return plotModel;
    }
}
