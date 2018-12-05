import * as React from 'react';
import * as Collection from './collection';
import * as Flickr from './flickrProvider';
import './index.scss';

class Index extends React.Component<any, IndexState, any> {
  
  public componentWillMount() {
    this.setState({
      flickrCollections: [],
      flickrProvider: new Flickr.FlickrProvider()
    });
  }

  public componentDidMount() {
    this.state.flickrProvider.getCollections()
    .then(response => {
      this.setState({ flickrCollections: response });
    });
  }

  public render() {
    let sortedCollections : Flickr.IFlickrCollection[] = [];
    if (this.state.flickrCollections) {
      sortedCollections = this.state.flickrCollections.sort((n1, n2) => {
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

export default Index;

interface IndexState {
  flickrProvider: Flickr.FlickrProvider,
  flickrCollections: Flickr.IFlickrCollection[]
}