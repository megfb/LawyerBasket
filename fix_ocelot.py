import json
import re

# Read the file
with open(r'LawyerBasket\LawyerBasket.Gateway\LawyerBasket.Gateway.Api\ocelot.Production.json', 'r', encoding='utf-8') as f:
    content = f.read()

# Replace localhost with container names based on port
content = re.sub(r'"Host": "localhost",\s+"Port": 7299', r'"Host": "authservice.api",\n                    "Port": 8080', content)
content = re.sub(r'"Host": "localhost",\s+"Port": 7141', r'"Host": "profileservice.api",\n                    "Port": 8080', content)
content = re.sub(r'"Host": "localhost",\s+"Port": 7254', r'"Host": "postservice.api",\n                    "Port": 8080', content)
content = re.sub(r'"Host": "localhost",\s+"Port": 7176', r'"Host": "socialservice.api",\n                    "Port": 8080', content)

# Replace https with http
content = content.replace('"DownstreamScheme": "https"', '"DownstreamScheme": "http"')

# Replace BaseUrl
content = content.replace('"BaseUrl": "https://localhost:7001"', '"BaseUrl": "http://localhost:7000"')

# Write back
with open(r'LawyerBasket\LawyerBasket.Gateway\LawyerBasket.Gateway.Api\ocelot.Production.json', 'w', encoding='utf-8') as f:
    f.write(content)

print("ocelot.Production.json updated successfully!")
