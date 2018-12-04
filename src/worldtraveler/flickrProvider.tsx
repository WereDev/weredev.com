const flickrUrl = 'https://api.flickr.com/services/rest/?api_key=d15bbd08117fc0ecf6bc7ef1a66a0240&user_id=29735288%40N05&format=json&nojsoncallback=1&method=';
const getCollectionsMethod = "flickr.collections.getTree";

export class FlickrProvider {
  
  private collections : IFlickrCollection[];

  public getCollections() {
    const t = this;  

      return fetch(flickrUrl + getCollectionsMethod)
      .then(response => response.json())
      .then(json => {
        const thing = (json as IFlickrCollectionResponse);
        t.collections = thing.collections.collection;
        return t.collections;
      });
  }
}

export interface IFlickrCollectionResponse {
  collections: IFlickrCollections,
  stat: string
}

export interface IFlickrCollections {
  collection: IFlickrCollection[]
}

export interface IFlickrCollection {
  id: string,
  title: string,
  description: string,
  iconlarge: string,
  iconsmall: string,
  set: IFlickrSet[]
}

export interface IFlickrSet {
  id: string,
  title: string,
  description: string
}
