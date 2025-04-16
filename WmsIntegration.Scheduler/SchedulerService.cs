namespace WmsIntegration.Scheduler
{
    public class SchedulerService
    {
        private readonly ILogger<SchedulerService> _logger;
        private readonly XmlProcessor _xmlProcessor;
        private readonly string _directoryPath = "./XmlFiles";

        public SchedulerService(ILogger<SchedulerService> logger, XmlProcessor xmlProcessor)
        {
            _logger = logger;
            _xmlProcessor = xmlProcessor;
        }

        public async Task RunAsync()
        {
            _logger.LogInformation("Checking for new XML files to process...");
            var xmlFiles = Directory.GetFiles(_directoryPath, "*.xml");

            foreach (var file in xmlFiles)
            {
                try
                {
                    await _xmlProcessor.ProcessXmlFileAsync(file);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error processing file {file}: {ex.Message}");
                }
            }
        }
    }
}
