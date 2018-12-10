import * as React from 'react';
import { IFlickrCollection } from './services/collectionTreeModels';

class Collection extends React.Component<ICollectionProps, any, any> {
  
  public render() {
    return (
      <div className="travel-collection">
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
