using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Widget;
using System;


using OxyPlot.Xamarin.Android;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;


namespace equacoes2grau
{
    

    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]

    

    public class MainActivity : AppCompatActivity
    {

        TextView equacao;
        TextView vertice;
        TextView raizes;
        TextView concavidade;
        TextView delta;

        EditText A;
        EditText B;
        EditText C;

        Button botao1;

        double a = 0, b = 0, c = 0, deltacalc = 0, raiz1 = 0, raiz2 = 0;
        double verticex = 0, verticey = 0;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            


            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            botao1 = FindViewById<Button>(Resource.Id.botao1);
            botao1.Click += botao1Calcular_Click;

        }
        double Funcao(double _ent )
        {
            return (a * _ent * _ent + b * _ent + c);
        }


        private PlotModel CreatePlotModel(double a, double b, double c, double concx)
        {
            var plotModel = new PlotModel { Title = "Equação de 2º Grau:" };

            //plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom });
            //plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Maximum = 10, Minimum = 0 });

            plotModel.Axes.Add(new OxyPlot.Axes.LinearAxis { Position = OxyPlot.Axes.AxisPosition.Bottom, ExtraGridlines = new double[] { 0 }, ExtraGridlineThickness = 1, ExtraGridlineColor = OxyColors.Blue, });

            plotModel.Axes.Add(new OxyPlot.Axes.LinearAxis { Position = OxyPlot.Axes.AxisPosition.Left, ExtraGridlines = new double[] { 0 }, ExtraGridlineThickness = 1, ExtraGridlineColor = OxyColors.Red, });
            
            
            var series1 = new LineSeries
            {
                MarkerType = MarkerType.Circle,
                MarkerSize = 4,
                MarkerStroke = OxyColors.White
                
            };
            series1.Points.Clear();

            double saida; 
            for (int i = -5; i < 5; i++)
            {
                saida = Funcao(concx+i);
                series1.Points.Add(new DataPoint(concx + i, saida));

            }
           

            plotModel.Series.Add(series1);

            return plotModel;
        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        void botao1Calcular_Click(object sender, System.EventArgs e)
        {
            
            equacao = FindViewById<TextView>(Resource.Id.equacao);
            vertice = FindViewById<TextView>(Resource.Id.vertice);
            raizes = FindViewById<TextView>(Resource.Id.raizes);
            delta = FindViewById<TextView>(Resource.Id.delta);
            concavidade = FindViewById<TextView>(Resource.Id.concavidade);
            A = FindViewById<EditText>(Resource.Id.coefA);
            B = FindViewById<EditText>(Resource.Id.coefB);
            C = FindViewById<EditText>(Resource.Id.coefC);


            a = Convert.ToDouble(A.Text);
            b = Convert.ToDouble(B.Text);
            c = Convert.ToDouble(C.Text);

            equacao.Text = a.ToString() + "x^2 + " + b.ToString() + " x + " + c.ToString();

            deltacalc = (b * b) - (4 * a * c);
            delta.Text = deltacalc.ToString();

            if (deltacalc < 0)
            {

                raizes.Text = " Sem raízes Reais ";
            }
            else
            {
                /// calcula as raízes 
                /// 
                raiz1 = ((-1 * b) + Math.Sqrt(deltacalc)) / (2 * a);
                raiz2 = ((-1 * b) - Math.Sqrt(deltacalc)) / (2 * a);
                raizes.Text = " R1 = " + raiz1.ToString() + "  R2 = " + raiz2.ToString();
            }

            verticex = (-1 * b) / (2 * a);
            verticey = (-1 * deltacalc) / (4 * a); 
            vertice.Text = " vx = " + verticex.ToString() + "  vy = " + verticey.ToString();

            if (a > 0)
                concavidade.Text = "Concava para cima";
            else
                concavidade.Text = "Concava para baixo";

            PlotView view = FindViewById<PlotView>(Resource.Id.plot_view);
            view.Model = CreatePlotModel(a, b, c, verticex); 
           

        }





    }
}