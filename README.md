#US Census Name History Visualizer

![screenshot](/localhost_50819.png)

This is a web application to to visualize Male/Female distributions of names  in the US over time. Uses data aquired from United States Census data from a [Google BigQuery public dataset](https://cloud.google.com/bigquery/public-data/us-census). Also uses pronouciation data from [CMUSphinx](https://cmusphinx.github.io/) to determine names which are homonyms ([dictionary file](https://github.com/cmusphinx/cmudict)). Graphs constructed using [billboard.js](https://naver.github.io/billboard.js/) and data is managed by SQL and ASP.NET WebServices.