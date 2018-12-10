import { IFlickrCollectionInfoResponse } from './collectionInfoModels';
import { FlickrCollectionRequest, IFlickrCollectionResponse } from './collectionTreeModels';
import { FlickrCollectionInfoRequest } from './collectionInfoModels';


const flickrUrl = 'https://api.flickr.com/services/rest/?';

export class FlickrProvider {
  
  public getCollections() {

    const queryString = this.getQueryString(new FlickrCollectionRequest());
    const requestUrl = flickrUrl + queryString;

    return fetch(requestUrl)
    .then(response => response.json())
    .then(json => {
      const response = (json as IFlickrCollectionResponse);
      return response.collections.collection;
    });
  }
  
  public getCollectionInfo(collectionId : string) {

    const request = new FlickrCollectionInfoRequest(collectionId);
    const queryString = this.getQueryString(request);
    const requestUrl = flickrUrl + queryString;

    return fetch(requestUrl)
    .then(response => response.json())
    .then(json => {
      const response = (json as IFlickrCollectionInfoResponse);
      return response.collection;
    })
  }

  private getQueryString(object : any) {
    const queryString = Object.keys(object).map(key => key + '=' + object[key]).join('&');
    return queryString;
  }
}

