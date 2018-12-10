import * as Base from './flickrModelsBase';

export class FlickrCollectionInfoRequest extends Base.FlickrRequestBase {    
    public method = "flickr.collections.getInfo";
    public collection_id : string;

    constructor(collectionId: string) {
        super();
        this.collection_id = collectionId;
    }
}

export interface IFlickrCollectionInfoResponse {
    collection: IFlickrCollectionInfo;
    stat: string;
}

export interface IFlickrCollectionInfo {
    id: string;
    title: IFlickrCollectionTitle;
    description: IFlickrCollectionDescription;
    child_count: number;
    datecreate: string;
    iconlarge: string;
    iconsmall: string;
    iconphotos: IFlickrCollectionIconPhoto[];
    server: string;
    secret: string;
}

export interface IFlickrCollectionTitle {
    _content: string;
}

export interface IFlickrCollectionDescription {
    _content: string;
}

export interface IFlickrCollectionIconPhoto {
    photo: IFlickrCollectionPhoto[];
}

export interface IFlickrCollectionPhoto {
    id: string;
    owner: string;
    secret: string;
    server: string;
    farm: number;
    title: string;
    ispublic: number;
    isfriend: number;
    isfamily: number;
}