import { IFlickrCollectionInfo } from './models/collectionInfoModels';
import { IFlickrCollection } from './models/collectionTreeModels';

export default class FlickrTestProvider {
  
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
    
  }
  
  public getCollectionInfo(collectionId : string) : Promise<IFlickrCollectionInfo> {
    const collectionInfo : IFlickrCollectionInfo = {
        child_count: 0,
        datecreate: Date.now.toString(),
        description: {
            _content: 'test desription'
        },
        iconlarge: 'icon large',
        iconphotos: [],
        iconsmall: 'icon small',
        id: collectionId,
        secret: 'shhh',
        server: 'this one',
        title: {
            _content: 'test collection info'
        }
    };

    return Promise.resolve(collectionInfo);
  }

}

