import FlickrRequestBase from './flickrModelsBase';

export class FlickrCollectionRequest extends FlickrRequestBase {
    public method = "flickr.collections.getTree";
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