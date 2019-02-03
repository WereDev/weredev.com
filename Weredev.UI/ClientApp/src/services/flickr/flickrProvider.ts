import { FlickrCollectionInfoRequest, IFlickrCollectionInfo, IFlickrCollectionInfoResponse } from './models/collectionInfoModels';
import { FlickrCollectionRequest, IFlickrCollection, IFlickrCollectionResponse } from './models/collectionTreeModels';

const flickrUrl = 'https://api.flickr.com/services/rest/?';

export default class FlickrProvider {
  
  public getCollections() : Promise<IFlickrCollection[]> {
        
    const queryString = this.getQueryString(new FlickrCollectionRequest());
    const requestUrl = flickrUrl + queryString;

    return fetch(requestUrl)
    .then(response => response.json())
    .then(json => {
      const response = (json as IFlickrCollectionResponse);
      return response.collections.collection;
    });
  }
  
  public getCollectionInfo(collectionId : string) : Promise<IFlickrCollectionInfo> {
    const request = new FlickrCollectionInfoRequest(collectionId);
    const queryString = this.getQueryString(request);
    const requestUrl = flickrUrl + queryString;

    return fetch(requestUrl)
    .then(response => response.json())
    .then(json => {
      const response = (json as IFlickrCollectionInfoResponse);
      return response.collection;
    });
  }

  private getQueryString(object : any) {
    const queryString = Object.keys(object).map(key => key + '=' + object[key]).join('&');
    return queryString;
  }
}

