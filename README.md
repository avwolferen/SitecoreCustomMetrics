# Sitecore Health Monitor for Custom Metrics in Application Insights
This repository contains the implementation for integrating Cache Statistics and Rendering Statistics as custom metrics in Application Insights. These metrics can be tracked and viewed in Application Insights using the Metrics Explorer or by executing Kusto queries in the logs.

## Overview
The Health Monitor is designed to capture important cache statistics and rendering statistics from your application and send them to Application Insights as custom metrics. This allows you to monitor and analyze the performance of your cache and rendering processes.

## Features
Cache Statistics: Track cache hit rate, cache size, cache misses, and other relevant cache metrics.

![cache statistics items in cache](/metrics-cachestatistics-1.png)

![cache statistics sizes](/metrics-cachestatistics-cachesize-2.png)

Rendering Statistics: Monitor rendering time (max/min/avg), number of renders, items accessed (max/min/avg/total) and other rendering performance metrics.

![All renderings available for non-Sitecore internal sites](/metrics-renderingstatistics-1.png)

![All properties of renderingstatistics available](/metrics-renderingstatistics-customproperties.png)
![How many times is your rendering rendered](/metrics-renderingstatistics-timesrendered-2.png)

## Getting Started
To integrate the Health Monitor into your application and start tracking custom metrics in Application Insights, follow these steps:

### Clone the repository:
1. git clone https://github.com/avwolferen/SitecoreCustomMetrics
2. Make sure you have Application Insights working for you, please refer to [Application Insights 2.21 for Sitecore](https://github.com/avwolferen/SitecoreApplicationInsights)
4. No configuration file updates required for the custom metrics!
5. Build and deploy your application with the Health Monitor integrated.
6. Verify that the custom metrics are being tracked and sent to Application Insights.

View metrics in Application Insights:

Open your Application Insights resource in the Azure portal.
Navigate to the Metrics Explorer or use Kusto queries in the logs to view and analyze the custom metrics.


# Contributing
Contributions are welcome! If you would like to contribute to the Health Monitor repository, please follow the guidelines outlined in the CONTRIBUTING.md file.

# License
This project is licensed under the MIT License. Feel free to use, modify, and distribute the code as per the terms of the license.

# Acknowledgments
Special thanks to the contributors and maintainers of Sitecore, the Application Insights SDKs and the Azure platform for providing the necessary tools and documentation to enable this integration.