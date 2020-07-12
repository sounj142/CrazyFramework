namespace CrazyFramework.BlazoriseClient.Pages.Tests
{
	public partial class FormsPage
	{
		private int[] numbers = { 1, 3, 4 };

		private DaysInWeek DaysInWeekLocal { get; set; } = DaysInWeek.Mon;

		public enum DaysInWeek
		{
			Mon,
			Tue,
			Wen,
			Thu,
			Fri,
			Sat,
			Sun,
		}

		//string fileContent;
		//int fileProgress;

		//async Task OnFileChanged( FileChangedEventArgs e )
		//{
		//    try
		//    {
		//        foreach ( var file in e.Files )
		//        {
		//            using ( var stream = new MemoryStream() )
		//            {
		//                await file.WriteToStreamAsync( stream );

		//                //stream.Seek( 0, SeekOrigin.Begin );

		//                //using ( var reader = new StreamReader( stream ) )
		//                //{
		//                //    fileContent = await reader.ReadToEndAsync();
		//                //}
		//            }
		//        }
		//    }
		//    catch ( Exception exc )
		//    {
		//        Console.WriteLine( exc.Message );
		//    }
		//    finally
		//    {
		//        this.StateHasChanged();
		//    }
		//}

		//void OnFileStarted( FileStartedEventArgs e )
		//{
		//    fileProgress = 0;
		//    Console.WriteLine( $"Started Name: {e.File.Name}" );
		//}

		//void OnFileEnded( FileEndedEventArgs e )
		//{
		//    Console.WriteLine( $"Finished Name: {e.File.Name}, Success: {e.Success}" );
		//}

		//void OnFileProgressed( FileProgressedEventArgs e )
		//{
		//    fileProgress = (int)e.Percentage;
		//    Console.WriteLine( $"Name: {e.File.Name} Progress: {e.Percentage}" );
		//}

		//void OnFileWritten( FileWrittenEventArgs e )
		//{
		//    Console.WriteLine( $"Name: {e.File.Name} Position: {e.Position} Data: {Convert.ToBase64String( e.Data )}" );
		//}
	}
}