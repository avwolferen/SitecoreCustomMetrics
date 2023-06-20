using System.Collections.Generic;
using System.Linq;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights;
using Sitecore.Pipelines;
using Sitecore.Caching;
using Sitecore.Diagnostics;

namespace AlexVanWolferen.SitecoreCustomMetrics
{
  public class HealthMonitor
  {
    private TelemetryClient telemetryClient { get; } = new TelemetryClient(TelemetryConfiguration.Active);

    public IList<string> ignoreSites { get; } = new List<string>
    {
      "admin", "shell", "modules_shell", "publisher", "login", "service", "modules_website", "scheduler", "system", "unicorn", "coveo", "coveo_website"
    };

    public virtual void DumpCacheStatistics(PipelineArgs e)
    {
      if (!telemetryClient.IsEnabled())
      {
        return;
      }

      ICacheInfo[] allCaches = CacheManager.GetAllCaches();
      foreach (var cacheInfo in allCaches)
      {
        var cacheSize = new MetricTelemetry($"{cacheInfo.Name} - size", cacheInfo.Size);
        cacheSize.Max = cacheInfo.MaxSize;
        telemetryClient.TrackMetric(cacheSize);

        var cacheItems = new MetricTelemetry($"{cacheInfo.Name} - records", cacheInfo.Count);
        telemetryClient.TrackMetric(cacheItems);
      }
    }


    public virtual void DumpRenderingStatistics(PipelineArgs e)
    {
      if (!telemetryClient.IsEnabled())
      {
        return;
      }

      var statistics = Statistics.RenderingStatistics.Where(s => !ignoreSites.Contains(s.SiteName));
      foreach (var rs in statistics)
      {
        var renderingTelemetry = new MetricTelemetry(rs.TraceName, rs.RenderCount)
        {
          MetricNamespace = rs.SiteName,
          Sum = rs.TotalItemsAccessed,
          Min = rs.MinItemsAccessed,
          Max = rs.MaxItemsAccessed,
        };

        renderingTelemetry.Properties.Add("SiteName", rs.SiteName);
        renderingTelemetry.Properties.Add("UsedCache", rs.UsedCache.ToString());
        renderingTelemetry.Properties.Add("MinTime", rs.MinTime.TotalMilliseconds.ToString());
        renderingTelemetry.Properties.Add("MaxTime", rs.MaxTime.TotalMilliseconds.ToString());
        renderingTelemetry.Properties.Add("AverageTime", rs.AverageTime.TotalMilliseconds.ToString());
        renderingTelemetry.Properties.Add("MinItemsAccessed", rs.MinItemsAccessed.ToString());
        renderingTelemetry.Properties.Add("MaxItemsAccessed", rs.MaxItemsAccessed.ToString());
        renderingTelemetry.Properties.Add("TotalItemsAccessed", rs.TotalItemsAccessed.ToString());


        telemetryClient.TrackMetric(renderingTelemetry);
      }
    }
  }
}