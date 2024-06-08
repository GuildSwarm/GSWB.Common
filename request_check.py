import requests
import base64
import os


username = os.environ.get('USER')
token   = os.environ.get('TOKEN')
env     = os.environ.get('ENV')
url_base  = 'https://jenkins.guildswarm.org'
url_crumb = f'{url_base}/crumbIssuer/api/json'
url_jobs  = f'{url_base}/job/base-images/job/backend/job/{env}/job/microservices/api/json'

#Building the list of jobs obtained
def builder(dataDict):
    jobsToBuild = []

    for job in dataDict.get('jobs', []):
       url = job.get('url')
       url_build=f'{url}job/{env}/build'
       jobsToBuild.append(url_build)
    return jobsToBuild
       
def jobBuild(list, headersRequest):

    for job in list:
        try:
            jobBuildReponse=requests.post(job, headers=headersRequest)
            print(jobBuildReponse)
        except requests.exceptions.RequestException as e:
            print(f"Error: {e}")
# Get Crumb issuer
def getCrumb(username, token):
    credentials = f'{username}:{token}'
    base64_credentials = base64.b64encode(credentials.encode('utf-8')).decode('utf-8')
    headers = {
        'Authorization': f'Basic {base64_credentials}',
        'Content-Type': 'application/json'
    }
    try:
        responseCrumb = requests.get(url_crumb, headers=headers)
        data = responseCrumb.json()
        crumbField = data.get("crumb")
        return crumbField, base64_credentials
    except requests.exceptions.RequestException as e:
        print(f"Error: {e}")
        return None, None
# Main :)
def main():
    crumb, creds=getCrumb(username, token)
    headersRequest = {
        'Authorization': f'Basic {creds}',
        'Jenkins-Crumb': f'{crumb}',
        'Content-Type' : 'application/json'
    }
    try:
        responseRequest = requests.get(url_jobs, headers=headersRequest)
        dataDict = responseRequest.json()
        list=builder(dataDict)
        jobBuild(list, headersRequest)
        
            
    except requests.exceptions.RequestException as e:
        print(f"Error: {e}")

main()