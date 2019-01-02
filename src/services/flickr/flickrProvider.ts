import { FlickrCollectionInfoRequest, IFlickrCollectionInfo, IFlickrCollectionInfoResponse } from './collectionInfoModels';
import { IFlickrCollection } from './collectionTreeModels';

const flickrUrl = 'https://api.flickr.com/services/rest/?';

export default class FlickrProvider {
  
  public getCollections() : Promise<IFlickrCollection[]> {

    const collection : IFlickrCollection[] = [];
    collection.push({
      description: "Chicgo Illinois",
      iconlarge: "chicago large",
      iconsmall: "chicago small",
      id: "1",
      set: [],
      title: "Chicago, IL, US"
    });
    collection.push({
      description: "Zion National Park",
      iconlarge: "park large",
      iconsmall: "park small",
      id: "2",
      set: [],
      title: "Zion National Park, UT, US"
    });
    collection.push({
      description: "New Zealand",
      iconlarge: "nz large",
      iconsmall: "nz small",
      id: "3",
      set: [],
      title: "New Zealand"
    });
    
    return Promise.resolve(collection);
    
    // const queryString = this.getQueryString(new FlickrCollectionRequest());
    // const requestUrl = flickrUrl + queryString;

    // return fetch(requestUrl)
    // .then(response => response.json())
    // .then(json => {
    //   const response = (json as IFlickrCollectionResponse);
    //   return response.collections.collection;
    // });
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

