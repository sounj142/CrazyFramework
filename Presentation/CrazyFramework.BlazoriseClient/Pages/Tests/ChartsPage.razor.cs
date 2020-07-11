using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazorise.Charts;

namespace CrazyFramework.BlazoriseClient.Pages.Tests
{
	public partial class ChartsPage
	{
		private LineChart<double> lineChart;
		private Chart<double> barChart;
		private Chart<double> pieChart;
		private Chart<double> doughnutChart;
		private Chart<double> polarAreaChart;
		private Chart<double> radarChart;

		private string[] Labels = { "Red", "Blue", "Yellow", "Green", "Purple", "Orange" };
		private List<string> backgroundColors = new List<string> { ChartColor.FromRgba(255, 99, 132, 0.2f), ChartColor.FromRgba(54, 162, 235, 0.2f), ChartColor.FromRgba(255, 206, 86, 0.2f), ChartColor.FromRgba(75, 192, 192, 0.2f), ChartColor.FromRgba(153, 102, 255, 0.2f), ChartColor.FromRgba(255, 159, 64, 0.2f) };
		private List<string> borderColors = new List<string> { ChartColor.FromRgba(255, 99, 132, 1f), ChartColor.FromRgba(54, 162, 235, 1f), ChartColor.FromRgba(255, 206, 86, 1f), ChartColor.FromRgba(75, 192, 192, 1f), ChartColor.FromRgba(153, 102, 255, 1f), ChartColor.FromRgba(255, 159, 64, 1f) };

		private bool isAlreadyInitialised;

		private Random random = new Random(DateTime.Now.Millisecond);

		protected override async Task OnAfterRenderAsync(bool firstRender)
		{
			if (!isAlreadyInitialised)
			{
				isAlreadyInitialised = true;

				await Task.WhenAll(
					HandleRedraw(lineChart, GetLineChartDataset),
					HandleRedraw(barChart, GetBarChartDataset),
					HandleRedraw(pieChart, GetPieChartDataset),
					HandleRedraw(doughnutChart, GetDoughnutChartDataset),
					HandleRedraw(polarAreaChart, GetPolarAreaChartDataset),
					HandleRedraw(radarChart, GetRadarChartDataset));
			}
		}

		private async Task HandleRedraw<TDataSet, TItem, TOptions, TModel>(Blazorise.Charts.BaseChart<TDataSet, TItem, TOptions, TModel> chart, Func<TDataSet> getDataSet)
			where TDataSet : ChartDataset<TItem>
			where TOptions : ChartOptions
			where TModel : ChartModel
		{
			await chart.Clear();

			await chart.AddLabelsDatasetsAndUpdate(Labels, getDataSet());
		}

		private ChartDataset<double> GetChartDataset()
		{
			return new ChartDataset<double>
			{
				Label = "# of randoms",
				Data = RandomizeData(),
				BackgroundColor = backgroundColors,
				BorderColor = borderColors
			};
		}

		private LineChartDataset<double> GetLineChartDataset()
		{
			return new LineChartDataset<double>
			{
				Label = "# of randoms",
				Data = RandomizeData(),
				BackgroundColor = backgroundColors,
				BorderColor = borderColors,
				Fill = true,
				PointRadius = 3,
				BorderWidth = 1,
				PointBorderColor = Enumerable.Repeat(borderColors.First(), 6).ToList()
			};
		}

		private BarChartDataset<double> GetBarChartDataset()
		{
			return new BarChartDataset<double>
			{
				Label = "# of randoms",
				Data = RandomizeData(),
				BackgroundColor = backgroundColors,
				BorderColor = borderColors,
				BorderWidth = 1
			};
		}

		private PieChartDataset<double> GetPieChartDataset()
		{
			return new PieChartDataset<double>

			{
				Label = "# of randoms",
				Data = RandomizeData(),
				BackgroundColor = backgroundColors,
				BorderColor = borderColors,
				BorderWidth = 1
			};
		}

		private DoughnutChartDataset<double> GetDoughnutChartDataset()
		{
			return new DoughnutChartDataset<double>
			{
				Label = "# of randoms",
				Data = RandomizeData(),
				BackgroundColor = backgroundColors,
				BorderColor = borderColors,
				BorderWidth = 1
			};
		}

		private PolarAreaChartDataset<double> GetPolarAreaChartDataset()
		{
			return new PolarAreaChartDataset<double>
			{
				Label = "# of randoms",
				Data = RandomizeData(),
				BackgroundColor = backgroundColors,
				BorderColor = borderColors,
				BorderWidth = 1
			};
		}

		private RadarChartDataset<double> GetRadarChartDataset()
		{
			return new RadarChartDataset<double>
			{
				Label = "custom radar",
				Data = RandomizeData(),
				BackgroundColor = backgroundColors,
				BorderColor = borderColors,
				LineTension = 0.0f,
				BorderWidth = 1
			};
		}

		private List<double> RandomizeData()
		{
			return new List<double> { random.Next(3, 50) * random.NextDouble(), random.Next(3, 50) * random.NextDouble(), random.Next(3, 50) * random.NextDouble(), random.Next(3, 50) * random.NextDouble(), random.Next(3, 50) * random.NextDouble(), random.Next(3, 50) * random.NextDouble() };
		}
	}
}