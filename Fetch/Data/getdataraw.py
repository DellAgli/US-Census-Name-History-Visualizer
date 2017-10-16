from google.cloud import bigquery
from google_auth_oauthlib import flow
import argparse
import time
import uuid

'''
https://cloud.google.com/bigquery/public-data/usa-names
https://cloud.google.com/bigquery/authentication
'''

def get(filename):
	def fetchData():
		def wait_for_job(job):
			while True:
				job.reload()  # Refreshes the state via a GET request.
				if job.state == 'DONE':
					if job.error_result:
						raise RuntimeError(job.errors)
					return
				time.sleep(1)

		def run_query(credentials, project, query):
			client = bigquery.Client(project=project, credentials=credentials)
			query_job = client.run_async_query(str(uuid.uuid4()), query)
			query_job.use_legacy_sql = False
			query_job.begin()

			wait_for_job(query_job)

			query_results = query_job.results()
			rows = query_results.fetch_data()

			return rows


		def authenticate_and_query(project, query, launch_browser=True):
			appflow = flow.InstalledAppFlow.from_client_secrets_file(
				'client_secret.json',
				scopes=['https://www.googleapis.com/auth/bigquery'])

			if launch_browser:
				appflow.run_local_server()
			else:
				appflow.run_console()

			return run_query(appflow.credentials, project, query)

		query = """
		#StandardSQL
		Select state,gender,year,name,number
		From `bigquery-public-data.usa_names.usa_1910_current`

		Order by name,year
		"""
		return authenticate_and_query("names-project-175500", query)


	out = fetchData()
	with open(filename, "w+") as f:
		f.write("state,gender,year,name,number")
		for row in out:
			f.write("{},{},{},{},{}".format(str(row[0]), str(row[1]), str(row[2]), str(row[3]), str(row[4])) + "\n")

	return filename

get("raw.csv")