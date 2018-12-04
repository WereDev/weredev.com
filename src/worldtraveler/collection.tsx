import * as React from 'react';
import * as Flickr from './flickrProvider';

class Collection extends React.Component<ICollectionProps, any, any> {
  
  public render() {
    return (
      <div className="travel-collection">
        <img src={this.props.collection.iconlarge} />
        <h2>{this.props.collection.title}</h2>        
      </div>
    );
  }
}

export default Collection;

export interface ICollectionProps {
    collection: Flickr.IFlickrCollection
}
