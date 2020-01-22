docker build -t pinellas-ale-trail .

docker tag pinellas-ale-trail registry.heroku.com/pinellas-ale-trail/web

docker push registry.heroku.com/pinellas-ale-trail/web

heroku container:release web -a pinellas-ale-trail