import * as React from 'react';
import * as Collection from './collection';
import * as Flickr from './flickrProvider';

class Collections extends React.Component<ICollectionsProps, any, any> {
  
  public render() {
    let sortedCollections : Flickr.IFlickrCollection[] = [];
    if (this.props.collections) {
      sortedCollections = this.props.collections.sort((n1, n2) => {
        if (n1.title > n2.title) { return 1; }
        if (n1.title < n2.title) { return -1; }
        return 0;
      })
    }

    const collectionTags : any[] = [];
    sortedCollections.forEach(element => {
          collectionTags.push(<Collection.default collection={element} />)
    });

    return (
      <div className="travel-collections">{ collectionTags }</div>
    );
  }
}

export default Collections;

export interface ICollectionsProps {
    collections: Flickr.IFlickrCollection[]
}
