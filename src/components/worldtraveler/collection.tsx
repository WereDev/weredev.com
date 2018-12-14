import * as React from 'react';
import { IFlickrCollection } from '../../services/flickr/collectionTreeModels';

class Collection extends React.Component<ICollectionProps, any, any> {
  
  public render() {
    return (
      <div className="col-lg-3 col-md-4 col-sm-6 col-12 mt-3">
        <img src={this.props.collection.iconlarge} />
        <h3>{this.props.collection.title}</h3>        
      </div>
    );
  }
}

export default Collection;

export interface ICollectionProps {
    collection: IFlickrCollection
}
